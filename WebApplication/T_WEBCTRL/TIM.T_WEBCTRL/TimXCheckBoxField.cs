using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class TimXCheckBoxField : BoundField
	{
		private bool m_enabled = true;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event EventHandler CheckedChanged;

		public bool Enabled
		{
			get
			{
				return this.m_enabled;
			}
			set
			{
				this.m_enabled = value;
			}
		}

		public TimXCheckBoxField()
		{
			base.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
		}

		protected override DataControlField CreateField()
		{
			return new TimXCheckBoxField();
		}

		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			TimCheckBox chkEdit = new TimCheckBox();
			chkEdit.ID = "chk" + this.DataField;
			bool flag = base.Visible && base.ItemStyle.Width.Value > 2.0;
			if (flag)
			{
				chkEdit.Width = new Unit(base.ItemStyle.Width.Value - 2.0);
			}
			else
			{
				chkEdit.Width = base.ItemStyle.Width;
			}
			chkEdit.Enabled = this.Enabled;
			bool flag2 = this.CheckedChanged != null;
			if (flag2)
			{
				chkEdit.AutoPostBack = true;
				chkEdit.CheckedChanged += new EventHandler(this.CheckedChanged.Invoke);
			}
			chkEdit.DataBinding += new EventHandler(this.Edit_DataBinding);
			cell.Controls.Add(chkEdit);
		}

		private void div_DataBinding(object sender, EventArgs e)
		{
			HtmlGenericControl div = (HtmlGenericControl)sender;
			object value = this.GetValue(div.NamingContainer);
			div.InnerText = this.FormatDataValue(value, this.HtmlEncode);
		}

		private void Edit_DataBinding(object sender, EventArgs e)
		{
			TimCheckBox chkEdit = (TimCheckBox)sender;
			object value = this.GetValue(chkEdit.NamingContainer);
			chkEdit.Text = "";
			bool flag = string.IsNullOrWhiteSpace(this.FormatDataValue(value, this.HtmlEncode)) || this.FormatDataValue(value, this.HtmlEncode) == "N";
			if (flag)
			{
				chkEdit.Checked = false;
			}
			else
			{
				chkEdit.Checked = true;
			}
		}
	}
}
