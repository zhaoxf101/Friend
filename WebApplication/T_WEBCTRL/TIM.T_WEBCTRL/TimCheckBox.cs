using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ToolboxData("<{0}:TimCheckBox runat=server></{0}:TimCheckBox>")]
	public class TimCheckBox : CheckBox
	{
		public string Value
		{
			get
			{
				return this.Checked ? "Y" : "N";
			}
			set
			{
				this.Checked = (value == "Y");
			}
		}

		public TimCheckBox()
		{
			this.Text = "是(√) / 否(□)";
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute("onkeydown", "Enter2Tab(this,event);");
			base.AddAttributesToRender(writer);
		}
	}
}
