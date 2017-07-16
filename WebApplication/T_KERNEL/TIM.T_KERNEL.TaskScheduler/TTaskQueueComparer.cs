using System;
using System.Collections;

namespace TIM.T_KERNEL.TaskScheduler
{
	public class TTaskQueueComparer : IComparer
	{
		int IComparer.Compare(object x, object y)
		{
			return ((TaskBase)x).ExecuteTime.CompareTo(((TaskBase)y).ExecuteTime);
		}
	}
}
