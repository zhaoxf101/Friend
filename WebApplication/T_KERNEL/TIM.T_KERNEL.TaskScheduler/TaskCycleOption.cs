using System;

namespace TIM.T_KERNEL.TaskScheduler
{
	public class TaskCycleOption
	{
		private string m_cycleExecWeek = string.Empty;

		private string m_cycleExecMonth = string.Empty;

		private string m_cycleExecTime = string.Empty;

		private string m_cycleUnit;

		private int m_cycleValue;

		public string CycleUnit
		{
			get
			{
				return this.m_cycleUnit;
			}
			set
			{
				this.m_cycleUnit = value;
			}
		}

		public int CycleValue
		{
			get
			{
				return this.m_cycleValue;
			}
			set
			{
				this.m_cycleValue = value;
			}
		}

		public string CycleExecWeek
		{
			get
			{
				return this.m_cycleExecWeek;
			}
			set
			{
				this.m_cycleExecWeek = value;
			}
		}

		public string CycleExecMonth
		{
			get
			{
				return this.m_cycleExecMonth;
			}
			set
			{
				this.m_cycleExecMonth = value;
			}
		}

		public string CycleExecTime
		{
			get
			{
				return this.m_cycleExecTime;
			}
			set
			{
				this.m_cycleExecTime = value;
			}
		}
	}
}
