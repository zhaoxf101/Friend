using System;

namespace TIM.T_KERNEL.Compute
{
	public class ComputeEngineParamException : ApplicationException
	{
		public ComputeEngineParamException(string message) : base(message)
		{
		}

		public override string ToString()
		{
			return this.Message;
		}
	}
}
