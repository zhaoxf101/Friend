using System;

namespace TIM.T_KERNEL.Compute
{
	public class ComputeEngineParserException : ApplicationException
	{
		public ComputeEngineParserException(string message) : base(message)
		{
		}

		public override string ToString()
		{
			return this.Message;
		}
	}
}
