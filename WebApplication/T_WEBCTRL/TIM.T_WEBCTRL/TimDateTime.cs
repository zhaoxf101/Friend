using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ToolboxData("<{0}:TimDateTime runat=server></{0}:TimDateTime>")]
	public class TimDateTime : TextBox
	{
		private string m_dbField = string.Empty;

		private TimCtrlEndSymbol m_symbol = TimCtrlEndSymbol.Null;

		private bool m_inputEnabled = true;

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

		[Category("Behavior"), Description("是否允许手工输入日期时间")]
		public bool InputEnabled
		{
			get
			{
				return this.m_inputEnabled;
			}
			set
			{
				this.m_inputEnabled = value;
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
				bool flag = value == DateTime.MaxValue || value == DateTime.MinValue;
				if (flag)
				{
					this.Text = "";
				}
				else
				{
					this.Text = value.ToString();
				}
			}
		}

		public TimDateTime()
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
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',readOnly:" + (this.InputEnabled ? "false" : "true") + "});");
			}
			base.Render(writer);
			TimCtrlUtils.WriteEndSymbol(writer, this.Symbol);
		}
	}
}
