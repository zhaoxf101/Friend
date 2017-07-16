using System;
using System.Collections.Generic;

namespace TIM.T_KERNEL.TaskScheduler
{
	public class TaskBase
	{
		private int m_jobId = 0;

		private string m_jobName = string.Empty;

		private int m_mdId = 0;

		private string m_mdName = string.Empty;

		private string m_comId = string.Empty;

		private string m_failNoticeUserId = string.Empty;

		private string m_execUserId = string.Empty;

		private bool m_skipExecute = true;

		private TaskCycleOption m_cycleOption = new TaskCycleOption();

		internal List<string> LogInfoDetail = new List<string>();

		private DateTime m_executeTime;

		private DateTime m_nextExecuteTime;

		private DateTime m_requiredExecuteTime;

		private TaskExecResult m_taskResult;

		public int JobId
		{
			get
			{
				return this.m_jobId;
			}
			set
			{
				this.m_jobId = value;
			}
		}

		public string JobName
		{
			get
			{
				return this.m_jobName;
			}
			set
			{
				this.m_jobName = value;
			}
		}

		public int MdId
		{
			get
			{
				return this.m_mdId;
			}
			set
			{
				this.m_mdId = value;
			}
		}

		public string MdName
		{
			get
			{
				return this.m_mdName;
			}
			set
			{
				this.m_mdName = value;
			}
		}

		public string ComId
		{
			get
			{
				return this.m_comId;
			}
			set
			{
				this.m_comId = value;
			}
		}

		public string FailNoticeUserId
		{
			get
			{
				return this.m_failNoticeUserId;
			}
			set
			{
				this.m_failNoticeUserId = value;
			}
		}

		public string ExecUserId
		{
			get
			{
				return this.m_execUserId;
			}
			set
			{
				this.m_execUserId = value;
			}
		}

		public DateTime ExecuteTime
		{
			get
			{
				return this.m_executeTime;
			}
			set
			{
				this.m_executeTime = value;
			}
		}

		public DateTime NextExecuteTime
		{
			get
			{
				return this.m_nextExecuteTime;
			}
			set
			{
				this.m_nextExecuteTime = value;
			}
		}

		public DateTime RequiredExecuteTime
		{
			get
			{
				return this.m_requiredExecuteTime;
			}
			set
			{
				this.m_requiredExecuteTime = value;
			}
		}

		public bool SkipExecute
		{
			get
			{
				return this.m_skipExecute;
			}
			set
			{
				this.m_skipExecute = value;
			}
		}

		public TaskCycleOption CycleOption
		{
			get
			{
				return this.m_cycleOption;
			}
			set
			{
				this.m_cycleOption = value;
			}
		}

		public TaskExecResult TaskResult
		{
			get
			{
				return this.m_taskResult;
			}
			set
			{
				this.m_taskResult = value;
			}
		}

		public TaskBase()
		{
			this.CycleOption = new TaskCycleOption();
			this.TaskResult = TaskExecResult.Success;
		}

		internal void Init()
		{
		}

		public void Execute()
		{
			this.Start();
		}

		public virtual void Start()
		{
		}

		protected void AddServiceLog(bool completed, string logInfo)
		{
			if (completed)
			{
				bool flag = this.TaskResult != TaskExecResult.Failure && this.TaskResult != TaskExecResult.Exception;
				if (flag)
				{
					this.TaskResult = TaskExecResult.Success;
				}
				this.LogInfoDetail.Add(string.Format("{0} A|成功 ", DateTime.Now.ToString()) + logInfo);
			}
			else
			{
				bool flag2 = this.TaskResult != TaskExecResult.Exception;
				if (flag2)
				{
					this.TaskResult = TaskExecResult.Failure;
				}
				this.LogInfoDetail.Add(string.Format("{0} B|失败 ", DateTime.Now.ToString()) + logInfo);
			}
		}
	}
}
