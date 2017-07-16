using System;
using System.ComponentModel;

namespace TIM.T_WEBCTRL
{
	[AttributeUsage(AttributeTargets.All)]
	internal class ANPCategoryAttribute : CategoryAttribute
	{
		internal ANPCategoryAttribute(string name) : base(name)
		{
		}

		protected override string GetLocalizedString(string value)
		{
			string cat = base.GetLocalizedString(value);
			bool flag = cat == null;
			if (flag)
			{
				cat = SR.GetString(value);
			}
			return cat;
		}
	}
}
