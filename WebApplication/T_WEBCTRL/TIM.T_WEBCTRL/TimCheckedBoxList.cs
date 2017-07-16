using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ToolboxData("<{0}:TimCheckedBoxList runat=server></{0}:TimCheckedBoxList>")]
	public class TimCheckedBoxList : CheckBoxList
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
						result = result + item.Value + ",";
					}
				}
				result = result.TrimEnd(new char[]
				{
					','
				});
				return result;
			}
			set
			{
				List<string> result = value.Split(new char[]
				{
					','
				}).ToList<string>();
				foreach (string item in result)
				{
					ListItem selectedItem = this.Items.FindByValue(item);
					bool flag = selectedItem != null;
					if (flag)
					{
						selectedItem.Selected = true;
					}
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
