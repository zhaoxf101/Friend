using System;
using System.Globalization;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	internal class TimPagingBarDesigner : ControlDesigner
	{
		public override string GetDesignTimeHtml()
		{
			TimPagingBar ctrl = (TimPagingBar)base.Component;
			return string.Format(CultureInfo.InvariantCulture, "<div style=\"width:{0};height:{1};background-color:blue \" id=\"{2}\">{3}</div>", new object[]
			{
				new Unit(400.0, UnitType.Pixel),
				new Unit(20.0, UnitType.Pixel),
				ctrl.ID,
				"分页控件"
			});
		}
	}
}
