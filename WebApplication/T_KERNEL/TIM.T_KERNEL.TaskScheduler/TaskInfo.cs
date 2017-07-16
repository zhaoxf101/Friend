using System;

namespace TIM.T_KERNEL.TaskScheduler
{
	internal class TaskInfo
	{
		private Type m_taskType = null;

		private string m_taskName = string.Empty;

		private string m_dbId = string.Empty;

		private string m_userId = string.Empty;

		private ITask m_taskObject = null;

		public Type TaskType
		{
			get
			{
				return this.m_taskType;
			}
			set
			{
				this.m_taskType = value;
			}
		}

		public string TaskName
		{
			get
			{
				return this.m_taskName;
			}
			set
			{
				this.m_taskName = value;
			}
		}

		public string DbId
		{
			get
			{
				return this.m_dbId;
			}
			set
			{
				this.m_dbId = value;
			}
		}

		public string UserId
		{
			get
			{
				return this.m_userId;
			}
			set
			{
				this.m_userId = value;
			}
		}

		internal ITask TaskObject
		{
			get
			{
				return this.m_taskObject;
			}
			set
			{
				this.m_taskObject = value;
			}
		}

		internal TaskInfo(string taskName, Type type, string userId, string dbId)
		{
			this.TaskName = taskName;
			this.TaskType = type;
			this.DbId = dbId;
			this.UserId = userId;
			this.TaskObject = (Activator.CreateInstance(type) as ITask);
		}
	}
}
