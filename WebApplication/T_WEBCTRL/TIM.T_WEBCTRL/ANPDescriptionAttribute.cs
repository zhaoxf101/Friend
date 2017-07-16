using System;
using System.ComponentModel;

namespace TIM.T_WEBCTRL
{
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ANPDescriptionAttribute : DescriptionAttribute
	{
		private bool replaced;

		public override string Description
		{
			get
			{
				bool flag = !this.replaced;
				if (flag)
				{
					this.replaced = true;
					base.DescriptionValue = SR.GetString(base.DescriptionValue);
				}
				return base.Description;
			}
		}

		public ANPDescriptionAttribute(string text) : base(text)
		{
			this.replaced = false;
		}
	}
}
