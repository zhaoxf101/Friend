using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ToolboxData("<{0}:TimDropDownList runat=server></{0}:TimDropDownList>")]
	public class TimDropDownList : DropDownList
	{
		public override string SelectedValue
		{
			get
			{
				return base.SelectedValue;
			}
			set
			{
				ListItem listItem = this.Items.FindByValue(value);
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					value = " ";
				}
				listItem = this.Items.FindByValue(value);
				bool flag2 = listItem != null;
				if (flag2)
				{
					base.SelectedValue = value;
					this.OnSelectedIndexChanged(null);
				}
			}
		}

		public TimDropDownList()
		{
			this.Height = new Unit(22);
			this.Width = new Unit(204);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			bool flag = !this.Enabled;
			if (flag)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#F5FFFA");
			}
			base.Render(writer);
		}
	}
}
