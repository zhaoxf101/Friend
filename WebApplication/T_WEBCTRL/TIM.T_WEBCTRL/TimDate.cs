using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ToolboxData("<{0}:TimDate runat=server></{0}:TimDate>")]
	public class TimDate : TextBox
	{
		private string m_dbField = string.Empty;

		private TimCtrlEndSymbol m_symbol = TimCtrlEndSymbol.Null;

		[Category("Behavior"), Description("数据字段")]
		public string DbField
		{
			get
			{
				return this.m_dbField;
			}
			set
			{
				this.m_dbField = value;
			}
		}

		[Category("Behavior"), Description("文本之后的符号")]
		public TimCtrlEndSymbol Symbol
		{
			get
			{
				return this.m_symbol;
			}
			set
			{
				this.m_symbol = value;
			}
		}

		public DateTime Value
		{
			get
			{
				DateTime result = TimCtrlUtils.UltDateTime;
				bool flag = string.IsNullOrEmpty(this.Text);
				DateTime result2;
				if (flag)
				{
					result2 = result;
				}
				else
				{
					DateTime.TryParse(this.Text, out result);
					result2 = result;
				}
				return result2;
			}
			set
			{
				this.Text = value.ToShortDateString();
			}
		}

		public TimDate()
		{
			this.Width = new Unit(200);
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool flag = this.TextMode != TextBoxMode.MultiLine;
			if (flag)
			{
				writer.AddAttribute("onkeydown", "Enter2Tab(this,event);");
			}
			base.AddAttributesToRender(writer);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "Wdate");
			bool flag = !this.Enabled || this.ReadOnly;
			if (flag)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#F5FFFA");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "WdatePicker({readOnly:true});");
			}
			base.Render(writer);
			TimCtrlUtils.WriteEndSymbol(writer, this.Symbol);
		}
	}
}
