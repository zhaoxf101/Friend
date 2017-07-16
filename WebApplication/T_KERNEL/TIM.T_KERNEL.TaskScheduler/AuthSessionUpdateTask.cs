using System;
using TIM.T_KERNEL.DbTableCache;

namespace TIM.T_KERNEL.TaskScheduler
{
	internal class AuthSessionUpdateTask : ITask
	{
		private DateTime _AuthSessionUpdateTaskNextTime = DateTime.MinValue;

		public int MdId
		{
			get
			{
				return 101000001;
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
			bool flag = this._AuthSessionUpdateTaskNextTime > AppRuntime.ServerDateTime;
			if (!flag)
			{
				AuthUtils.LogicSessionUpdateFromAuthSessionTask();
				this._AuthSessionUpdateTaskNextTime = AppRuntime.ServerDateTime.AddMinutes(60.0);
			}
		}

		public void Init()
		{
		}
	}
}
