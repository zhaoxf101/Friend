using System;

namespace TIM.T_KERNEL.TaskScheduler
{
	public enum TaskExecResult
	{
		Success,
		Failure,
		Skip,
		Executed,
		Exception,
		StartExecute
	}
}
