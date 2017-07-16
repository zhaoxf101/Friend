using System;

namespace TIM.T_TEMPLET.Page
{
	public static class PageStateHelper
	{
        internal static uint ComputeStringHash(string s)
        {
            uint num = 0;
            if (s != null)
            {
                num = 0x811c9dc5;
                for (int i = 0; i < s.Length; i++)
                {
                    num = (s[i] ^ num) * 0x1000193;
                }
            }
            return num;
        }

        public static PageState ToPageState(this string value)
		{
			PageState result = PageState.VIEW;
			uint num = ComputeStringHash(value);
			if (num <= 429297080u)
			{
				if (num != 0u)
				{
					if (num != 125705233u)
					{
						if (num != 429297080u)
						{
							return result;
						}
						if (!(value == "VIEW"))
						{
							return result;
						}
					}
					else
					{
						if (!(value == "EDIT"))
						{
							return result;
						}
						result = PageState.EDIT;
						return result;
					}
				}
				else if (value != null)
				{
					return result;
				}
			}
			else if (num <= 963632676u)
			{
				if (num != 582295364u)
				{
					if (num != 963632676u)
					{
						return result;
					}
					if (!(value == "NULL"))
					{
						return result;
					}
					result = PageState.NULL;
					return result;
				}
				else
				{
					if (!(value == "COPY"))
					{
						return result;
					}
					result = PageState.COPY;
					return result;
				}
			}
			else if (num != 2166136261u)
			{
				if (num != 2767009640u)
				{
					return result;
				}
				if (!(value == "INSERT"))
				{
					return result;
				}
				result = PageState.INSERT;
				return result;
			}
			else if (value == null || value.Length != 0)
			{
				return result;
			}
			result = PageState.VIEW;
			return result;
		}
	}
}
