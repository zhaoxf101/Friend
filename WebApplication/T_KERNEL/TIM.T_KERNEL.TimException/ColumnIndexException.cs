using System;

namespace TIM.T_KERNEL.TimException
{
	public class ColumnIndexException : Exception
	{
		public ColumnIndexException() : base("不存在记录列.")
		{
		}

		public ColumnIndexException(string field) : base(string.Format("不存在记录列{{0}}.", field))
		{
		}
	}
}
