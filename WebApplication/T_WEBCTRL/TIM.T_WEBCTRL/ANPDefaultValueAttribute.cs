using System;
using System.ComponentModel;

namespace TIM.T_WEBCTRL
{
	[AttributeUsage(AttributeTargets.All)]
	internal class ANPDefaultValueAttribute : DefaultValueAttribute
	{
		private bool localized;

		public override object Value
		{
			get
			{
				bool flag = !this.localized;
				object result;
				if (flag)
				{
					this.localized = true;
					string defValue = (string)base.Value;
					bool flag2 = !string.IsNullOrEmpty(defValue);
					if (flag2)
					{
						result = SR.GetString(defValue);
						return result;
					}
				}
				result = base.Value;
				return result;
			}
		}

		public ANPDefaultValueAttribute(string name) : base(name)
		{
			this.localized = false;
		}
	}
}
