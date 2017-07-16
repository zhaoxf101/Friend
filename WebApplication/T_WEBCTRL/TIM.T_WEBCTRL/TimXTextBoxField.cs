using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class TimXTextBoxField : BoundField
	{
		private bool m_enabled = true;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event EventHandler TextChanged;

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

		protected override DataControlField CreateField()
		{
			return new TimXTextBoxField();
		}

		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			TimTextBox txtEdit = new TimTextBox();
			txtEdit.ID = "txt" + this.DataField;
			bool flag = base.Visible && base.ItemStyle.Width.Value > 2.0;
			if (flag)
			{
				txtEdit.Width = new Unit(base.ItemStyle.Width.Value - 2.0);
			}
			else
			{
				txtEdit.Width = base.ItemStyle.Width;
			}
			txtEdit.Enabled = this.Enabled;
			bool flag2 = this.TextChanged != null;
			if (flag2)
			{
				txtEdit.AutoPostBack = true;
				txtEdit.TextChanged += new EventHandler(this.TextChanged.Invoke);
			}
			txtEdit.DataBinding += new EventHandler(this.txtEdit_DataBinding);
			cell.Controls.Add(txtEdit);
		}

		private void div_DataBinding(object sender, EventArgs e)
		{
			HtmlGenericControl div = (HtmlGenericControl)sender;
			object value = this.GetValue(div.NamingContainer);
			div.InnerText = this.FormatDataValue(value, this.HtmlEncode);
		}

		private void txtEdit_DataBinding(object sender, EventArgs e)
		{
			TimTextBox txtEdit = (TimTextBox)sender;
			object value = this.GetValue(txtEdit.NamingContainer);
			txtEdit.Text = this.FormatDataValue(value, this.HtmlEncode);
		}
	}
}
