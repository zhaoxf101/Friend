using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ToolboxData("<{0}:TimImageButton runat=server></{0}:TimImageButton>")]
	public class TimImageButton : ImageButton
	{
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute("onkeydown", "Enter2Tab(this,event);");
			base.AddAttributesToRender(writer);
		}
	}
}
