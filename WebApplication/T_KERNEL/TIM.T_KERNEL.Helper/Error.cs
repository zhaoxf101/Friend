using System;

namespace TIM.T_KERNEL.Helper
{
	public class Error : Exception
	{
		public Error()
		{
		}

		public Error(string message) : base(message)
		{
		}

		public Error(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
