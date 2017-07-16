using System;

namespace TIM.T_KERNEL.TimException
{
	public class RowIndexException : Exception
	{
		public RowIndexException() : base("不存在记录行.")
		{
		}

		public RowIndexException(int rowIndex) : base(string.Format("不存在记录列，行{{0}}.", rowIndex.ToString()))
		{
		}
	}
}
