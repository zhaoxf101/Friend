using System;

namespace TIM.T_KERNEL.TaskScheduler
{
	internal class TimerJob : ITask
	{
		private int m_mdId = 0;

		private string m_comId = string.Empty;

		private TaskBase m_taskModule = new TaskBase();

		public int MdId
		{
			get
			{
				return this.m_mdId;
			}
		}

		public string ComId
		{
			get
			{
				return this.m_comId;
			}
		}

		public TaskBase TaskModule
		{
			get
			{
				return this.m_taskModule;
			}
			set
			{
				this.m_taskModule = value;
				this.m_mdId = this.m_taskModule.MdId;
				this.m_comId = this.m_taskModule.ComId;
			}
		}

		public void Init()
		{
		}

		public void Execute()
		{
			this.TaskModule.Execute();
		}
	}
}
