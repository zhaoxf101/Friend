using System;
using System.Globalization;
using System.Web.UI.Design;

namespace TIM.T_WEBCTRL
{
	internal class TimButtonMenuDesigner : ControlDesigner
	{
		public override string GetDesignTimeHtml()
		{
			TimButtonMenu ctrl = (TimButtonMenu)base.Component;
			return string.Format(CultureInfo.InvariantCulture, "<div style=\"width:{0};height:{1};background-color:blue \" id=\"{2}\">{3}</div>", new object[]
			{
				ctrl.Width,
				ctrl.Height,
				ctrl.ID,
				ctrl.Text
			});
		}
	}
}
