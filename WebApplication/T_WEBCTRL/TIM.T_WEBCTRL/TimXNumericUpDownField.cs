using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class TimXNumericUpDownField : BoundField
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
			return new TimXNumericUpDownField();
		}

		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			TimNumericUpDown nupEdit = new TimNumericUpDown();
			nupEdit.ID = "nup" + this.DataField;
			bool flag = base.Visible && base.ItemStyle.Width.Value > 2.0;
			if (flag)
			{
				nupEdit.Width = new Unit(base.ItemStyle.Width.Value - 2.0);
			}
			else
			{
				nupEdit.Width = base.ItemStyle.Width;
			}
			nupEdit.Enabled = this.Enabled;
			bool flag2 = this.TextChanged != null;
			if (flag2)
			{
				nupEdit.AutoPostBack = true;
				nupEdit.TextChanged += new EventHandler(this.TextChanged.Invoke);
			}
			nupEdit.DataBinding += new EventHandler(this.Edit_DataBinding);
			cell.Controls.Add(nupEdit);
		}

		private void Edit_DataBinding(object sender, EventArgs e)
		{
			TimNumericUpDown nupEdit = (TimNumericUpDown)sender;
			object value = this.GetValue(nupEdit.NamingContainer);
			nupEdit.Text = this.FormatDataValue(value, this.HtmlEncode);
		}
	}
}
