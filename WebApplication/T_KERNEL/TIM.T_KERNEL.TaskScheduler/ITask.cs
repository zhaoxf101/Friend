using System;

namespace TIM.T_KERNEL.TaskScheduler
{
	internal interface ITask
	{
		int MdId
		{
			get;
		}

		string ComId
		{
			get;
		}

		void Init();

		void Execute();
	}
}
