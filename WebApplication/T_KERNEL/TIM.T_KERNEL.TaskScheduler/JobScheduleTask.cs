using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Helper;
using TIM.T_KERNEL.Log;

namespace TIM.T_KERNEL.TaskScheduler
{
	internal class JobScheduleTask : ITask
	{
		private ArrayList m_TaskQueue;

		public int MdId
		{
			get
			{
				return 101000004;
			}
		}

		public string ComId
		{
			get
			{
				return "T_KERNEL";
			}
		}

		public ArrayList TaskQueue
		{
			get
			{
				return this.m_TaskQueue;
			}
		}

		public JobScheduleTask()
		{
			this.m_TaskQueue = ArrayList.Synchronized(new ArrayList());
		}

		private void UpdateServiceState()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Raw = true;
			sql.Add(" UPDATE JOB SET JOB_STATUS = 'C'");
			sql.Add(" WHERE JOB_STATUS = 'B'");
			database.ExecSQL(sql);
		}

		private Assembly[] FindAssembliesByNameKeyInAppDomain(string fileNamePrefix)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Clear();
			FileInfo[] files = new DirectoryInfo(HostingEnvironment.MapPath("~\\bin")).GetFiles("*.dll");
			for (int i = 0; i < files.Length; i++)
			{
				FileInfo fileInfo = files[i];
				bool flag = fileInfo.Name.IndexOf(fileNamePrefix) != -1;
				if (flag)
				{
					try
					{
						Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
						arrayList.Add(assembly);
					}
					catch
					{
					}
				}
			}
			return (Assembly[])arrayList.ToArray(typeof(Assembly));
		}

		private Type[] FindTypesByBaseInAssemblies(Assembly[] assemblies, Type baseType)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Clear();
			for (int i = 0; i < assemblies.Length; i++)
			{
				Assembly assembly = assemblies[i];
				try
				{
					Type[] types = assembly.GetTypes();
					for (int j = 0; j < types.Length; j++)
					{
						Type type = types[j];
						bool flag = type.IsClass && type.BaseType.Equals(baseType);
						if (flag)
						{
							arrayList.Add(type);
						}
					}
				}
				catch
				{
				}
			}
			return (Type[])arrayList.ToArray(typeof(Type));
		}

		private DateTime AddZQ(DateTime curTime, DateTime thisTime, string cycleUnit, int cycleValue, string cycleXQZXR, string cycleYFZXR, string cycleZXSJ)
		{
			int year = thisTime.Year;
			int month = thisTime.Month;
			int day = thisTime.Day;
			int hour = thisTime.Hour;
			int minute = thisTime.Minute;
			int second = thisTime.Second;
			int millisecond = thisTime.Millisecond;
			cycleXQZXR = cycleXQZXR.PadRight(7, '0');
			cycleYFZXR = cycleYFZXR.PadRight(32, '0');
			DateTime dateTime = default(DateTime);
			bool flag = cycleUnit.Trim() == "MONTH";
			DateTime result;
			if (flag)
			{
				dateTime = thisTime;
				string[] strArray = cycleZXSJ.Split(new char[]
				{
					','
				});
				ArrayList arrayList = new ArrayList();
				for (int index = 0; index < strArray.Length; index++)
				{
					bool flag2 = !string.IsNullOrEmpty(strArray[index]);
					if (flag2)
					{
						arrayList.Add(strArray[index]);
					}
				}
				arrayList.Sort();
				bool flag3 = !string.IsNullOrEmpty(cycleYFZXR.Substring(dateTime.Day - 1).TrimEnd(new char[]
				{
					'0'
				}));
				int num;
				if (flag3)
				{
					num = ((DateTime.Compare(dateTime.Date.AddDays((double)(DateTime.DaysInMonth(year, month) - day)).AddHours((double)((string)arrayList[arrayList.Count - 1]).Split(new char[]
					{
						':'
					})[0].ToInt()).AddMinutes((double)((string)arrayList[arrayList.Count - 1]).Split(new char[]
					{
						':'
					})[1].ToInt()), thisTime) != 0) ? 1 : 0);
				}
				else
				{
					num = 0;
				}
				bool flag4 = num == 0;
				if (flag4)
				{
					dateTime = dateTime.Date.AddDays((double)(DateTime.DaysInMonth(year, month) - day + 1)).AddMonths(cycleValue - 1);
				}
				for (int startIndex = dateTime.Day - 1; startIndex < DateTime.DaysInMonth(dateTime.Year, dateTime.Month); startIndex++)
				{
					bool flag5 = cycleYFZXR.Substring(startIndex, 1).Equals("1");
					if (flag5)
					{
						foreach (string str in arrayList)
						{
							bool flag6 = DateTime.Compare(dateTime.Date.AddHours((double)str.Split(new char[]
							{
								':'
							})[0].ToInt()).AddMinutes((double)str.Split(new char[]
							{
								':'
							})[1].ToInt()), thisTime) > 0;
							if (flag6)
							{
								result = dateTime.Date.AddHours((double)str.Split(new char[]
								{
									':'
								})[0].ToInt()).AddMinutes((double)str.Split(new char[]
								{
									':'
								})[1].ToInt());
								return result;
							}
						}
					}
					bool flag7 = dateTime.Day != DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
					if (flag7)
					{
						dateTime = dateTime.AddDays(1.0);
					}
				}
				bool flag8 = cycleYFZXR.Substring(31, 1).Equals("1");
				if (flag8)
				{
					foreach (string str2 in arrayList)
					{
						bool flag9 = DateTime.Compare(dateTime.Date.AddHours((double)str2.Split(new char[]
						{
							':'
						})[0].ToInt()).AddMinutes((double)str2.Split(new char[]
						{
							':'
						})[1].ToInt()), thisTime) > 0;
						if (flag9)
						{
							result = dateTime.Date.AddHours((double)str2.Split(new char[]
							{
								':'
							})[0].ToInt()).AddMinutes((double)str2.Split(new char[]
							{
								':'
							})[1].ToInt());
							return result;
						}
					}
				}
				dateTime = dateTime.Date.AddDays((double)(DateTime.DaysInMonth(dateTime.Year, dateTime.Month) - dateTime.Day + 1)).AddMonths(cycleValue - 1);
				for (int startIndex2 = dateTime.Day - 1; startIndex2 < DateTime.DaysInMonth(dateTime.Year, dateTime.Month); startIndex2++)
				{
					bool flag10 = cycleYFZXR.Substring(startIndex2, 1).Equals("1");
					if (flag10)
					{
						foreach (string str3 in arrayList)
						{
							bool flag11 = DateTime.Compare(dateTime.Date.AddHours((double)str3.Split(new char[]
							{
								':'
							})[0].ToInt()).AddMinutes((double)str3.Split(new char[]
							{
								':'
							})[1].ToInt()), thisTime) > 0;
							if (flag11)
							{
								result = dateTime.Date.AddHours((double)str3.Split(new char[]
								{
									':'
								})[0].ToInt()).AddMinutes((double)str3.Split(new char[]
								{
									':'
								})[1].ToInt());
								return result;
							}
						}
					}
					bool flag12 = dateTime.Day != DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
					if (flag12)
					{
						dateTime = dateTime.AddDays(1.0);
					}
				}
			}
			else
			{
				bool flag13 = cycleUnit.Trim() == "WEEK";
				if (flag13)
				{
					dateTime = thisTime;
					string[] strArray2 = cycleZXSJ.Split(new char[]
					{
						','
					});
					ArrayList arrayList2 = new ArrayList();
					for (int index2 = 0; index2 < strArray2.Length; index2++)
					{
						bool flag14 = !string.IsNullOrEmpty(strArray2[index2]);
						if (flag14)
						{
							arrayList2.Add(strArray2[index2]);
						}
					}
					arrayList2.Sort();
					bool flag15 = !string.IsNullOrEmpty(cycleXQZXR.Substring((int)(((int)dateTime.DayOfWeek + 6) % 7)).TrimEnd(new char[]
					{
						'0'
					}));
					int num2;
					if (flag15)
					{
						num2 = ((DateTime.Compare(dateTime.Date.AddDays((double)((7 - (int)dateTime.DayOfWeek) % 7)).AddHours((double)((string)arrayList2[arrayList2.Count - 1]).Split(new char[]
						{
							':'
						})[0].ToInt()).AddMinutes((double)((string)arrayList2[arrayList2.Count - 1]).Split(new char[]
						{
							':'
						})[1].ToInt()), thisTime) != 0) ? 1 : 0);
					}
					else
					{
						num2 = 0;
					}
					bool flag16 = num2 == 0;
					if (flag16)
					{
						dateTime = dateTime.Date.AddDays((double)((7 - (int)dateTime.DayOfWeek) % 7 + 1 + (cycleValue - 1) * 7));
					}
					for (int startIndex3 = (int)(((int)dateTime.DayOfWeek + 6) % 7); startIndex3 < 7; startIndex3++)
					{
						bool flag17 = cycleXQZXR.Substring(startIndex3, 1).Equals("1");
						if (flag17)
						{
							foreach (string str4 in arrayList2)
							{
								bool flag18 = DateTime.Compare(dateTime.Date.AddHours((double)str4.Split(new char[]
								{
									':'
								})[0].ToInt()).AddMinutes((double)str4.Split(new char[]
								{
									':'
								})[1].ToInt()), thisTime) > 0;
								if (flag18)
								{
									result = dateTime.Date.AddHours((double)str4.Split(new char[]
									{
										':'
									})[0].ToInt()).AddMinutes((double)str4.Split(new char[]
									{
										':'
									})[1].ToInt());
									return result;
								}
							}
						}
						bool flag19 = startIndex3 != 6;
						if (flag19)
						{
							dateTime = dateTime.AddDays(1.0);
						}
					}
					dateTime = dateTime.Date.AddDays((double)((7 - (int)dateTime.DayOfWeek) % 7 + 1 + (cycleValue - 1) * 7));
					for (int startIndex4 = (int)(((int)dateTime.DayOfWeek + 6) % 7); startIndex4 < 7; startIndex4++)
					{
						bool flag20 = cycleXQZXR.Substring(startIndex4, 1).Equals("1");
						if (flag20)
						{
							foreach (string str5 in arrayList2)
							{
								bool flag21 = DateTime.Compare(dateTime.Date.AddHours((double)str5.Split(new char[]
								{
									':'
								})[0].ToInt()).AddMinutes((double)str5.Split(new char[]
								{
									':'
								})[1].ToInt()), thisTime) > 0;
								if (flag21)
								{
									result = dateTime.Date.AddHours((double)str5.Split(new char[]
									{
										':'
									})[0].ToInt()).AddMinutes((double)str5.Split(new char[]
									{
										':'
									})[1].ToInt());
									return result;
								}
							}
						}
						bool flag22 = startIndex4 != 6;
						if (flag22)
						{
							dateTime = dateTime.AddDays(1.0);
						}
					}
				}
				else
				{
					bool flag23 = cycleUnit.Trim() == "DAY";
					if (flag23)
					{
						dateTime = thisTime;
						string[] strArray3 = cycleZXSJ.Split(new char[]
						{
							','
						});
						ArrayList arrayList3 = new ArrayList();
						for (int index3 = 0; index3 < strArray3.Length; index3++)
						{
							bool flag24 = !string.IsNullOrEmpty(strArray3[index3]);
							if (flag24)
							{
								arrayList3.Add(strArray3[index3]);
							}
						}
						arrayList3.Sort();
						bool flag25 = DateTime.Compare(dateTime.Date.AddHours((double)((string)arrayList3[arrayList3.Count - 1]).Split(new char[]
						{
							':'
						})[0].ToInt()).AddMinutes((double)((string)arrayList3[arrayList3.Count - 1]).Split(new char[]
						{
							':'
						})[1].ToInt()), thisTime) <= 0;
						if (flag25)
						{
							dateTime = thisTime.AddDays((double)cycleValue);
						}
						foreach (string str6 in arrayList3)
						{
							bool flag26 = DateTime.Compare(dateTime.Date.AddHours((double)str6.Split(new char[]
							{
								':'
							})[0].ToInt()).AddMinutes((double)str6.Split(new char[]
							{
								':'
							})[1].ToInt()), thisTime) > 0;
							if (flag26)
							{
								result = dateTime.Date.AddHours((double)str6.Split(new char[]
								{
									':'
								})[0].ToInt()).AddMinutes((double)str6.Split(new char[]
								{
									':'
								})[1].ToInt());
								return result;
							}
						}
					}
					else
					{
						bool flag27 = cycleUnit.Trim() == "MINUTE";
						if (flag27)
						{
							dateTime = thisTime.AddMinutes((double)cycleValue);
						}
					}
				}
			}
			result = dateTime;
			return result;
		}

		private bool UpdateNextTime(int jobId, DateTime execTime, DateTime nextExecTime)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add(" update JOB set JOB_NEXTEXECTIME = :NEXTEXECTIME ");
			sql.Add(" where JOB_JOBID = :JOB_JOBID ");
			sql.Add(" and (JOB_NEXTEXECTIME IS NULL or JOB_NEXTEXECTIME = :EXECTIME)");
			sql.ParamByName("JOB_JOBID").Value = jobId;
			sql.ParamByName("NEXTEXECTIME").Value = nextExecTime;
			sql.ParamByName("EXECTIME").Value = execTime;
			sql.ParamByName("EXECTIME").ParamterType = TimDbType.DateTime;
			return database.ExecSQL(sql) > 0;
		}

		private Array GetTasksInfo()
		{
			TaskBase[] Result = null;
			DateTime dtNextTime = default(DateTime);
			DateTime dtCurTime = AppRuntime.ServerDateTime.ToString().ToDateTime();
			bool executed = false;
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Add("select JOB_JOBID,JOB_JOBNAME,JOB_MDID,JOB_STATUS,JOB_CYCLEUNIT,JOB_CYCLEVALUE");
			hsql.Add(",JOB_EXECTIME,JOB_EXECWEEK,JOB_EXECMONTH,JOB_BEGINTIME,JOB_ENDTIME,JOB_EXECUSERID,JOB_FAILNOTICEUSERID");
			hsql.Add(",JOB_PREEXECTIME,JOB_PREEXECSTATUS,JOB_PREEXECDESC,JOB_NEXTEXECTIME");
			hsql.Add(",JOB_STARTTIME,JOB_STOPTIME,JOB_REMARKS,MODULE_COMID,MODULE_MDNAME");
			hsql.Add(" from JOB left join MODULE on MODULE_MDID = JOB_MDID ");
			hsql.Add(" where JOB_STATUS = 'D'");
			hsql.Add(" and JOB_BEGINTIME <= :NOWEXECTIME and JOB_BEGINTIME is not NULL and (JOB_ENDTIME > :NOWEXECTIME or JOB_ENDTIME IS NULL) ");
			hsql.Add(" and (JOB_NEXTEXECTIME < :NOWEXECTIME or JOB_NEXTEXECTIME is NULL)  ");
			hsql.Add("order by JOB_NEXTEXECTIME");
			hsql.ParamByName("NOWEXECTIME").Value = dtCurTime;
			DataSet ds = db.OpenDataSet(hsql);
			bool flag = ds.Tables[0].Rows.Count > 0;
			if (flag)
			{
				Result = new TaskBase[ds.Tables[0].Rows.Count];
			}
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				int JOB_JOBID = ds.Tables[0].Rows[i]["JOB_JOBID"].ToString().ToInt();
				string JOB_JOBNAME = ds.Tables[0].Rows[i]["JOB_JOBNAME"].ToString().Trim();
				int JOB_MDID = ds.Tables[0].Rows[i]["JOB_MDID"].ToString().ToInt();
				string MDNAME = ds.Tables[0].Rows[i]["MODULE_MDNAME"].ToString().Trim();
				string COMID = ds.Tables[0].Rows[i]["MODULE_COMID"].ToString().Trim();
				string JOB_EXECUSERID = ds.Tables[0].Rows[i]["JOB_EXECUSERID"].ToString().Trim();
				string JOB_FAILNOTICEUSERID = ds.Tables[0].Rows[i]["JOB_FAILNOTICEUSERID"].ToString().Trim();
				string JOB_NEXTEXECTIME = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["JOB_NEXTEXECTIME"].ToString()) ? ds.Tables[0].Rows[i]["JOB_BEGINTIME"].ToString() : ds.Tables[0].Rows[i]["JOB_NEXTEXECTIME"].ToString();
				string JOB_CYCLEUNIT = ds.Tables[0].Rows[i]["JOB_CYCLEUNIT"].ToString().Trim();
				int JOB_CYCLEVALUE = ds.Tables[0].Rows[i]["JOB_CYCLEVALUE"].ToString().Trim().ToInt();
				string JOB_EXECTIME = ds.Tables[0].Rows[i]["JOB_EXECTIME"].ToString().Trim();
				string JOB_EXECWEEK = ds.Tables[0].Rows[i]["JOB_EXECWEEK"].ToString().Trim();
				string JOB_EXECMONTH = ds.Tables[0].Rows[i]["JOB_EXECMONTH"].ToString().Trim();
				string JOB_BEGINTIME = ds.Tables[0].Rows[i]["JOB_BEGINTIME"].ToString().Trim();
				bool flag2 = string.IsNullOrEmpty(JOB_CYCLEUNIT) || JOB_CYCLEVALUE <= 0;
				if (!flag2)
				{
					string a = JOB_CYCLEUNIT;
					if (!(a == "MONTH"))
					{
						if (!(a == "WEEK"))
						{
							if (!(a == "DAY"))
							{
								if (!(a == "MINUTE"))
								{
									goto IL_6D3;
								}
							}
							else
							{
								bool flag3 = string.IsNullOrEmpty(JOB_EXECTIME);
								if (flag3)
								{
									goto IL_6D3;
								}
							}
						}
						else
						{
							bool flag4 = string.IsNullOrEmpty(JOB_EXECWEEK) || string.IsNullOrEmpty(JOB_EXECTIME) || JOB_EXECMONTH.Length != 7;
							if (flag4)
							{
								goto IL_6D3;
							}
						}
					}
					else
					{
						bool flag5 = string.IsNullOrEmpty(JOB_EXECMONTH) || string.IsNullOrEmpty(JOB_EXECTIME) || JOB_EXECMONTH.Length != 32;
						if (flag5)
						{
							goto IL_6D3;
						}
					}
					bool flag6 = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["JOB_NEXTEXECTIME"].ToString());
					if (flag6)
					{
						JOB_NEXTEXECTIME = this.AddZQ(dtCurTime, JOB_BEGINTIME.ToDateTime(), JOB_CYCLEUNIT, JOB_CYCLEVALUE, JOB_EXECWEEK, JOB_EXECMONTH, JOB_EXECTIME).ToString();
					}
					dtNextTime = this.AddZQ(dtCurTime, JOB_NEXTEXECTIME.ToDateTime(), JOB_CYCLEUNIT, JOB_CYCLEVALUE, JOB_EXECWEEK, JOB_EXECMONTH, JOB_EXECTIME);
					bool flag7 = dtNextTime.Equals(default(DateTime));
					if (!flag7)
					{
						TaskBase obj = new TaskBase();
						bool flag8 = DateTime.Compare(dtNextTime, dtCurTime) < 0;
						if (flag8)
						{
							while (DateTime.Compare(this.AddZQ(dtCurTime, dtNextTime, JOB_CYCLEUNIT, JOB_CYCLEVALUE, JOB_EXECWEEK, JOB_EXECMONTH, JOB_EXECTIME), dtCurTime) < 0)
							{
								dtNextTime = this.AddZQ(dtCurTime, dtNextTime, JOB_CYCLEUNIT, JOB_CYCLEVALUE, JOB_EXECWEEK, JOB_EXECMONTH, JOB_EXECTIME);
							}
							bool flag9 = !this.UpdateNextTime(JOB_JOBID, JOB_NEXTEXECTIME.ToDateTime(), dtNextTime);
							if (flag9)
							{
								executed = true;
							}
							obj.ExecuteTime = dtNextTime;
							obj.NextExecuteTime = this.AddZQ(dtCurTime, dtNextTime, JOB_CYCLEUNIT, JOB_CYCLEVALUE, JOB_EXECWEEK, JOB_EXECMONTH, JOB_EXECTIME);
							bool flag10 = executed;
							if (flag10)
							{
								obj.TaskResult = TaskExecResult.Executed;
								executed = false;
							}
						}
						else
						{
							obj.ExecuteTime = JOB_NEXTEXECTIME.ToDateTime();
							obj.NextExecuteTime = dtNextTime;
						}
						obj.JobId = JOB_JOBID;
						obj.JobName = JOB_JOBNAME;
						obj.MdId = JOB_MDID;
						obj.MdName = MDNAME;
						obj.ComId = COMID;
						obj.ExecUserId = JOB_EXECUSERID;
						obj.FailNoticeUserId = JOB_FAILNOTICEUSERID;
						obj.RequiredExecuteTime = (string.IsNullOrEmpty(JOB_NEXTEXECTIME) ? dtCurTime : JOB_NEXTEXECTIME.ToDateTime());
						obj.CycleOption.CycleUnit = JOB_CYCLEUNIT;
						obj.CycleOption.CycleValue = JOB_CYCLEVALUE;
						obj.CycleOption.CycleExecTime = JOB_EXECTIME;
						obj.CycleOption.CycleExecWeek = JOB_EXECWEEK;
						obj.CycleOption.CycleExecMonth = JOB_EXECMONTH;
						Result[i] = obj;
					}
				}
				IL_6D3:;
			}
			return Result;
		}

		private void AddTask(TaskBase task)
		{
			object syncRoot = this.m_TaskQueue.SyncRoot;
			lock (syncRoot)
			{
				this.m_TaskQueue.Add(task);
			}
		}

		private void PrepareTaskQueue()
		{
			this.m_TaskQueue.Clear();
			Type[] baseInAssemblies = this.FindTypesByBaseInAssemblies(this.FindAssembliesByNameKeyInAppDomain("T_"), typeof(TaskBase));
			Array tasksInfo = this.GetTasksInfo();
			bool flag = tasksInfo == null;
			if (flag)
			{
				AppEventLog.Debug("task info is null");
			}
			else
			{
				Type[] array = baseInAssemblies;
				for (int i = 0; i < array.Length; i++)
				{
					Type type = array[i];
					foreach (TaskBase taskBase in tasksInfo)
					{
						bool flag2 = taskBase == null || taskBase.TaskResult == TaskExecResult.Executed;
						if (flag2)
						{
							AppEventLog.Debug("task executed, pre task queue");
						}
						else
						{
							TaskBase task = (TaskBase)Activator.CreateInstance(type);
							task.Init();
							bool flag3 = taskBase.MdId.ToString() == task.MdId.ToString() && taskBase.ComId == task.ComId;
							if (flag3)
							{
								task.JobId = taskBase.JobId;
								task.JobName = taskBase.JobName;
								task.MdId = taskBase.MdId;
								task.MdName = taskBase.MdName;
								task.ComId = taskBase.ComId;
								task.ExecUserId = taskBase.ExecUserId;
								task.FailNoticeUserId = taskBase.FailNoticeUserId;
								task.RequiredExecuteTime = taskBase.RequiredExecuteTime;
								task.ExecuteTime = taskBase.ExecuteTime;
								task.NextExecuteTime = taskBase.NextExecuteTime;
								task.CycleOption = taskBase.CycleOption;
								this.AddTask(task);
								string str = taskBase.MdId.ToString();
								string str2 = "|";
								string str3 = task.MdId.ToString();
								AppEventLog.Debug(str + str2 + str3);
								AppEventLog.Debug(taskBase.ComId + "|" + task.ComId);
								AppEventLog.Debug("task  mdid or comid compare");
							}
						}
					}
				}
			}
		}

		public void Init()
		{
			this.UpdateServiceState();
			this.PrepareTaskQueue();
		}

		private TaskBase[] DequeueTasks(DateTime pExecuteTime)
		{
			ArrayList arrayList = new ArrayList();
			object syncRoot = this.m_TaskQueue.SyncRoot;
			lock (syncRoot)
			{
				for (int local_ = this.m_TaskQueue.Count - 1; local_ >= 0; local_--)
				{
					bool flag2 = ((TaskBase)this.m_TaskQueue[local_]).ExecuteTime.CompareTo(pExecuteTime) <= 0;
					if (flag2)
					{
						arrayList.Add(this.m_TaskQueue[local_]);
						this.m_TaskQueue.RemoveAt(local_);
					}
				}
			}
			arrayList.Sort(new TTaskQueueComparer());
			return (TaskBase[])arrayList.ToArray(typeof(TaskBase));
		}

		public void Execute()
		{
			DateTime now = DateTime.Now;
			this.Init();
			TaskBase[] taskBaseArray = this.DequeueTasks(DateTime.Now);
			TaskBase[] array = taskBaseArray;
			for (int i = 0; i < array.Length; i++)
			{
				TaskBase taskBase = array[i];
				bool flag = !this.UpdateNextTime(taskBase.JobId, taskBase.ExecuteTime, taskBase.NextExecuteTime);
				if (flag)
				{
					taskBase.TaskResult = TaskExecResult.Executed;
				}
			}
			AppEventLog.Debug("tasks:" + taskBaseArray.Length.ToString());
			TaskBase[] array2 = taskBaseArray;
			for (int j = 0; j < array2.Length; j++)
			{
				TaskBase task = array2[j];
				try
				{
					bool flag2 = task.TaskResult == TaskExecResult.Executed;
					if (flag2)
					{
						AppEventLog.Debug("TaskResult:true.");
					}
					bool flag3 = task.TaskResult != TaskExecResult.Executed;
					if (flag3)
					{
						DateTime dateTime = this.AddZQ(now, task.RequiredExecuteTime, task.CycleOption.CycleUnit, task.CycleOption.CycleValue, task.CycleOption.CycleExecWeek, task.CycleOption.CycleExecMonth, task.CycleOption.CycleExecTime);
						bool flag4 = DateTime.Compare(dateTime, now) < 0;
						if (flag4)
						{
							bool skipExecute = task.SkipExecute;
							if (skipExecute)
							{
								task.TaskResult = TaskExecResult.Skip;
								List<string> list = task.LogInfoDetail;
								string format = "{0} C|跳过 {1} 作业计划已过期，跳过该次运行。";
								string str = DateTime.Now.ToString();
								string str2 = task.RequiredExecuteTime.ToString();
								string str3 = string.Format(format, str, str2);
								list.Add(str3);
								while (DateTime.Compare(this.AddZQ(now, dateTime, task.CycleOption.CycleUnit, task.CycleOption.CycleValue, task.CycleOption.CycleExecWeek, task.CycleOption.CycleExecMonth, task.CycleOption.CycleExecTime), now) < 0)
								{
									task.RequiredExecuteTime = dateTime;
									dateTime = this.AddZQ(now, dateTime, task.CycleOption.CycleUnit, task.CycleOption.CycleValue, task.CycleOption.CycleExecWeek, task.CycleOption.CycleExecMonth, task.CycleOption.CycleExecTime);
									task.TaskResult = TaskExecResult.Skip;
									List<string> list2 = task.LogInfoDetail;
									string format2 = "{0} C|跳过 {1} 作业计划已过期，跳过该次运行。";
									string str4 = DateTime.Now.ToString();
									string str5 = task.RequiredExecuteTime.ToString();
									string str6 = string.Format(format2, str4, str5);
									list2.Add(str6);
								}
								JobLogUtils.WriteServiceLog(task);
							}
							else
							{
								task.NextExecuteTime = dateTime;
								TimerJob timerJob = new TimerJob();
								timerJob.TaskModule = task;
								TimerManager.RunTask(timerJob, task.ExecUserId, AppConfig.DefaultDbId);
								while (DateTime.Compare(this.AddZQ(now, dateTime, task.CycleOption.CycleUnit, task.CycleOption.CycleValue, task.CycleOption.CycleExecWeek, task.CycleOption.CycleExecMonth, task.CycleOption.CycleExecTime), now) < 0)
								{
									task.RequiredExecuteTime = dateTime;
									dateTime = this.AddZQ(now, dateTime, task.CycleOption.CycleUnit, task.CycleOption.CycleValue, task.CycleOption.CycleExecWeek, task.CycleOption.CycleExecMonth, task.CycleOption.CycleExecTime);
									task.NextExecuteTime = dateTime;
									timerJob.TaskModule = task;
									TimerManager.RunTask(timerJob, task.ExecUserId, AppConfig.DefaultDbId);
								}
								bool flag5 = task.TaskResult != TaskExecResult.Failure && task.TaskResult != TaskExecResult.Exception;
								if (flag5)
								{
									task.TaskResult = TaskExecResult.Success;
								}
								JobLogUtils.WriteServiceLog(task);
							}
							bool flag6 = DateTime.Compare(dateTime, now) < 0;
							if (flag6)
							{
								task.RequiredExecuteTime = dateTime;
								task.NextExecuteTime = this.AddZQ(now, dateTime, task.CycleOption.CycleUnit, task.CycleOption.CycleValue, task.CycleOption.CycleExecWeek, task.CycleOption.CycleExecMonth, task.CycleOption.CycleExecTime);
								TimerManager.RunTask(new TimerJob
								{
									TaskModule = task
								}, task.ExecUserId, AppConfig.DefaultDbId);
								bool flag7 = task.TaskResult != TaskExecResult.Failure && task.TaskResult != TaskExecResult.Exception;
								if (flag7)
								{
									task.TaskResult = TaskExecResult.Success;
								}
								JobLogUtils.WriteServiceLog(task);
							}
						}
						else
						{
							TimerManager.RunTask(new TimerJob
							{
								TaskModule = task
							}, task.ExecUserId, AppConfig.DefaultDbId);
							bool flag8 = task.TaskResult != TaskExecResult.Failure && task.TaskResult != TaskExecResult.Exception;
							if (flag8)
							{
								task.TaskResult = TaskExecResult.Success;
							}
							JobLogUtils.WriteServiceLog(task);
						}
					}
				}
				catch (Exception ex_4E3)
				{
					task.TaskResult = TaskExecResult.Exception;
					JobLogUtils.WriteServiceLog(task);
				}
			}
		}
	}
}
