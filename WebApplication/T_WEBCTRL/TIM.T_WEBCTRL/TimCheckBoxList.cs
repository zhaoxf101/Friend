using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ToolboxData("<{0}:TimCheckBoxList runat=server></{0}:TimCheckBoxList>")]
	public class TimCheckBoxList : CheckBoxList
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

		public string CheckedValue
		{
			get
			{
				string result = string.Empty;
				foreach (ListItem item in this.Items)
				{
					bool selected = item.Selected;
					if (selected)
					{
						result += "1";
					}
					else
					{
						result += "0";
					}
				}
				return result;
			}
			set
			{
				string result = value.PadRight(this.Items.Count, '0');
				for (int i = 0; i < this.Items.Count; i++)
				{
					this.Items[i].Selected = (result.Substring(i, 1) == "1");
				}
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			TimCtrlUtils.WriteEndSymbol(writer, this.Symbol);
		}
	}
}
