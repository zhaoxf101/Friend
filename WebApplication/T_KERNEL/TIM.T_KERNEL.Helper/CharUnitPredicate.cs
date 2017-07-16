using System;

namespace TIM.T_KERNEL.Helper
{
	internal class CharUnitPredicate
	{
		private char ExpectedChar;

		internal CharUnitPredicate(char ch)
		{
			this.ExpectedChar = ch;
		}

		internal bool Match(CharUnit charUnit)
		{
			return charUnit.Char == this.ExpectedChar;
		}
	}
}
