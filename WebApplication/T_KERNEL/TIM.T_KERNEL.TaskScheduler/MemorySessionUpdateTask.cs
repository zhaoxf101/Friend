using System;
using TIM.T_KERNEL.DbTableCache;

namespace TIM.T_KERNEL.TaskScheduler
{
	internal class MemorySessionUpdateTask : ITask
	{
		private DateTime _MemorySessionUpdateTaskNextTime = DateTime.MinValue;

		public int MdId
		{
			get
			{
				return 101000002;
			}
		}

		public string ComId
		{
			get
			{
				return "T_KERNEL";
			}
		}

		public void Execute()
		{
			bool flag = this._MemorySessionUpdateTaskNextTime > AppRuntime.ServerDateTime;
			if (!flag)
			{
				AuthUtils.LogicSessionUpdateFromMemorySessionTask();
				this._MemorySessionUpdateTaskNextTime = AppRuntime.ServerDateTime.AddMinutes(5.0);
			}
		}

		public void Init()
		{
		}
	}
}
