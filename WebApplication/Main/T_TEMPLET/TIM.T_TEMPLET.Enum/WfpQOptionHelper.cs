using System;

namespace TIM.T_TEMPLET.Enum
{
	public static class WfpQOptionHelper
	{
		public static WfpQOption ToWfpQOption(this string value)
		{
			WfpQOption result = WfpQOption.None;
			string a = value.ToUpper();
			if (!(a == "T"))
			{
				if (!(a == "D"))
				{
					if (a == "A")
					{
						result = WfpQOption.A;
					}
				}
				else
				{
					result = WfpQOption.D;
				}
			}
			else
			{
				result = WfpQOption.T;
			}
			return result;
		}
	}
}
