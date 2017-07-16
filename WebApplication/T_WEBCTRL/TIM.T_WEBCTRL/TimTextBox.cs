using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ToolboxData("<{0}:TimTextBox runat=server></{0}:TimTextBox>")]
	public class TimTextBox : TextBox
	{
		private string m_dbField = string.Empty;

		private TimCtrlEndSymbol m_symbol = TimCtrlEndSymbol.Null;

		private TimCharCase m_charCase = TimCharCase.Null;

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

		public TimCharCase CharCase
		{
			get
			{
				return this.m_charCase;
			}
			set
			{
				this.m_charCase = value;
			}
		}

		public override string Text
		{
			get
			{
				string inputText = base.Text;
				TimCharCase charCase = this.CharCase;
				if (charCase != TimCharCase.Upper)
				{
					if (charCase == TimCharCase.Lower)
					{
						inputText = inputText.ToLower();
					}
				}
				else
				{
					inputText = inputText.ToUpper();
				}
				return inputText;
			}
			set
			{
				string inputText = value.TrimEnd(new char[0]);
				bool flag = this.MaxLength > 0 && !string.IsNullOrEmpty(inputText);
				if (flag)
				{
					base.Text = this.GetIndexLength(value, this.MaxLength);
				}
				else
				{
					base.Text = inputText;
				}
			}
		}

		public TimTextBox()
		{
			this.Width = new Unit(200);
		}

		private string GetIndexLength(string value, int length)
		{
			string result = string.Empty;
			byte[] inputByte = Encoding.Default.GetBytes(value);
			bool flag = inputByte.Length > length;
			if (flag)
			{
				bool flag2 = inputByte[length - 1] != 63;
				if (flag2)
				{
					result = Regex.Replace(Encoding.Default.GetString(inputByte, 0, length), "\\?$", "");
				}
				else
				{
					result = Encoding.Default.GetString(inputByte, 0, length);
				}
			}
			else
			{
				result = value;
			}
			return result;
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool flag = this.TextMode != TextBoxMode.MultiLine && this.TextMode != TextBoxMode.Password;
			if (flag)
			{
				writer.AddAttribute("onkeydown", "Enter2Tab(this,event);");
			}
			base.AddAttributesToRender(writer);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			bool flag = !this.Enabled || this.ReadOnly;
			if (flag)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#F5FFFA");
			}
			bool flag2 = this.Rows == 0;
			if (flag2)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "20px");
			}
			else
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Rows * 20 + "px");
			}
			TimCharCase charCase = this.CharCase;
			if (charCase != TimCharCase.Upper)
			{
				if (charCase == TimCharCase.Lower)
				{
					writer.AddStyleAttribute("TEXT-TRANSFORM", "lowercase");
				}
			}
			else
			{
				writer.AddStyleAttribute("TEXT-TRANSFORM", "uppercase");
			}
			base.Render(writer);
			TimCtrlUtils.WriteEndSymbol(writer, this.Symbol);
		}
	}
}
