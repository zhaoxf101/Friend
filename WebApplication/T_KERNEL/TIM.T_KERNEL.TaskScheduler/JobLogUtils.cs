using System;
using System.Text;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Log;
using TIM.T_KERNEL.Utils;

namespace TIM.T_KERNEL.TaskScheduler
{
	internal class JobLogUtils
	{
		public static void WriteServiceLog(TaskBase task)
		{
			Database database = LogicContext.GetDatabase();
			try
			{
				database.BeginTrans();
				HSQL sql = new HSQL(database);
				sql.Add("insert into JOBLOG(JOBLOG_JOBID, JOBLOG_LOGNO, JOBLOG_LOGTIME, JOBLOG_OPERTYPE, JOBLOG_EXECRESULT, JOBLOG_DESC, JOBLOG_DETAIL)");
				sql.Add("values(:JOBLOG_JOBID, :JOBLOG_LOGNO, :JOBLOG_LOGTIME, :JOBLOG_OPERTYPE, :JOBLOG_EXECRESULT, :JOBLOG_DESC, :JOBLOG_DETAIL)");
				sql.ParamByName("JOBLOG_JOBID").Value = task.JobId;
				sql.ParamByName("JOBLOG_LOGNO").Value = TimIdUtils.GenUtoId("JOBLOGNO");
				sql.ParamByName("JOBLOG_LOGTIME").Value = AppRuntime.ServerDateTime;
				sql.ParamByName("JOBLOG_OPERTYPE").Value = "E";
				sql.ParamByName("JOBLOG_DETAIL").Value = string.Empty;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string str in task.LogInfoDetail)
				{
					stringBuilder.AppendLine(str);
				}
				string str2 = stringBuilder.ToString();
				string str3 = string.Empty;
				bool flag = task.TaskResult == TaskExecResult.Success;
				if (flag)
				{
					str3 = string.Format("{0} 作业计划执行成功。", task.RequiredExecuteTime.ToString());
					bool flag2 = string.IsNullOrEmpty(str2);
					if (flag2)
					{
						str2 = str3;
					}
					sql.ParamByName("JOBLOG_EXECRESULT").Value = "A";
					sql.ParamByName("JOBLOG_DESC").Value = str3;
					sql.AddParam("JOBLOG_DETAIL", TimDbType.Blob, 0, str2);
				}
				else
				{
					bool flag3 = task.TaskResult == TaskExecResult.Failure;
					if (flag3)
					{
						str3 = string.Format("{0} 作业计划执行失败。", task.RequiredExecuteTime.ToString());
						bool flag4 = string.IsNullOrEmpty(str2);
						if (flag4)
						{
							str2 = str3;
						}
						sql.ParamByName("JOBLOG_EXECRESULT").Value = "B";
						sql.ParamByName("JOBLOG_DESC").Value = str3;
						sql.AddParam("JOBLOG_DETAIL", TimDbType.Blob, 0, str2);
						JobLogUtils.SendErrorMsgToMail(task);
					}
					else
					{
						bool flag5 = task.TaskResult == TaskExecResult.Skip;
						if (flag5)
						{
							str3 = "部分作业计划已过期，跳过运行。";
							bool flag6 = string.IsNullOrEmpty(str2);
							if (flag6)
							{
								str2 = str3;
							}
							sql.ParamByName("JOBLOG_EXECRESULT").Value = "C";
							sql.ParamByName("JOBLOG_DESC").Value = str3;
							sql.AddParam("JOBLOG_DETAIL", TimDbType.Blob, 0, str2);
						}
						else
						{
							bool flag7 = task.TaskResult == TaskExecResult.Exception;
							if (flag7)
							{
								str3 = string.Format("{0} 作业计划执行错误。", task.RequiredExecuteTime.ToString());
								bool flag8 = string.IsNullOrEmpty(str2);
								if (flag8)
								{
									str2 = str3;
								}
								sql.ParamByName("JOBLOG_EXECRESULT").Value = "D";
								sql.ParamByName("JOBLOG_DESC").Value = str3;
								sql.AddParam("JOBLOG_DETAIL", TimDbType.Blob, 0, str2);
								JobLogUtils.SendErrorMsgToMail(task);
							}
							else
							{
								bool flag9 = task.TaskResult == TaskExecResult.StartExecute;
								if (flag9)
								{
									str3 = string.Format("{0} 作业计划开始执行。", task.RequiredExecuteTime.ToString());
									bool flag10 = string.IsNullOrEmpty(str2);
									if (flag10)
									{
										str2 = str3;
									}
									sql.ParamByName("JOBLOG_EXECRESULT").Value = "A";
									sql.ParamByName("JOBLOG_DESC").Value = str3;
									sql.AddParam("JOBLOG_DETAIL", TimDbType.Blob, 0, str2);
								}
							}
						}
					}
				}
				database.ExecSQL(sql);
				task.LogInfoDetail.Clear();
				sql.Clear();
				sql.Add("update JOB set JOB_PREEXECTIME = :JOB_PREEXECTIME,JOB_PREEXECSTATUS = :JOB_PREEXECSTATUS,JOB_PREEXECDESC = :JOB_PREEXECDESC ");
				sql.Add(" where JOB_JOBID = :JOB_JOBID");
				sql.ParamByName("JOB_PREEXECSTATUS").Value = ((task.TaskResult != TaskExecResult.Success) ? ((task.TaskResult != TaskExecResult.Failure) ? ((task.TaskResult != TaskExecResult.Exception) ? "C" : "D") : "B") : "A");
				sql.ParamByName("JOB_PREEXECTIME").Value = task.ExecuteTime;
				sql.ParamByName("JOB_PREEXECDESC").Value = str3;
				sql.ParamByName("JOB_JOBID").Value = task.JobId;
				database.ExecSQL(sql);
				database.CommitTrans();
			}
			catch (Exception ex)
			{
				database.RollbackTrans();
				AppEventLog.Error("记录作业计划日志失败，原因：" + ex.Message);
			}
		}

		private static void SendErrorMsgToMail(TaskBase task)
		{
			try
			{
				bool flag = string.IsNullOrEmpty(task.FailNoticeUserId);
				if (!flag)
				{
					SmsUtils.Send(task.FailNoticeUserId, string.Concat(new string[]
					{
						"作业信息：<br/>应执行时间：",
						task.RequiredExecuteTime.ToString(),
						"<br/>实际执行时间：",
						DateTime.Now.ToString(),
						"<br/>执行失败!"
					}));
				}
			}
			catch
			{
			}
		}
	}
}
