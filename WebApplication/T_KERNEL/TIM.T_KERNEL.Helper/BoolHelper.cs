using System;

namespace TIM.T_KERNEL.Helper
{
	public static class BoolHelper
	{
		public static string ToYOrN(this bool value)
		{
			return value ? "Y" : "N";
		}

		public static string ToYesOrNo(this bool value)
		{
			return value ? "Yes" : "No";
		}

		public static string ToTrueOrFalse(this bool value)
		{
			return value ? "True" : "Flase";
		}

		public static string ToZeroOrOne(this bool value)
		{
			return value ? "1" : "0";
		}
	}
}
