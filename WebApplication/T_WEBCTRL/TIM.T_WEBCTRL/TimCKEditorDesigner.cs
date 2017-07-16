using System;
using System.Globalization;
using System.Web.UI.Design;

namespace TIM.T_WEBCTRL
{
	public class TimCKEditorDesigner : ControlDesigner
	{
		public override string GetDesignTimeHtml()
		{
			TimCKEditor control = (TimCKEditor)base.Component;
			return string.Format(CultureInfo.InvariantCulture, "<table width=\"{0}\" height=\"{1}\" bgcolor=\"#f5f5f5\" bordercolor=\"#c7c7c7\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\"><tr><td valign=\"middle\" align=\"center\"><h3>CKEditor ASP.NET Control - <em>'{2}'</em></h3></td></tr></table>", new object[]
			{
				(control.Width.Value == 0.0) ? "100%" : control.Width.ToString(),
				control.Height,
				control.ID
			});
		}
	}
}
