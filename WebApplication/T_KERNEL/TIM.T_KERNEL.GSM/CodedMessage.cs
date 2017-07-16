using System;

namespace TIM.T_KERNEL.GSM
{
	internal class CodedMessage
	{
		public readonly int Length;

		public readonly string PduCode;

		public CodedMessage(string Code)
		{
			this.PduCode = Code;
			this.Length = (Code.Length - Convert.ToInt32(Code.Substring(0, 2), 16) * 2 - 2) / 2;
		}
	}
}
