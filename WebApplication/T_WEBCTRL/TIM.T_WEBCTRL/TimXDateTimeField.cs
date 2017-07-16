using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class TimXDateTimeField : BoundField
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
			return new TimXDateTimeField();
		}

		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			TimDateTime dtEdit = new TimDateTime();
			dtEdit.ID = "dt" + this.DataField;
			bool flag = base.Visible && base.ItemStyle.Width.Value > 2.0;
			if (flag)
			{
				dtEdit.Width = new Unit(base.ItemStyle.Width.Value - 2.0);
			}
			else
			{
				dtEdit.Width = base.ItemStyle.Width;
			}
			dtEdit.Enabled = this.Enabled;
			bool flag2 = this.TextChanged != null;
			if (flag2)
			{
				dtEdit.AutoPostBack = true;
				dtEdit.TextChanged += new EventHandler(this.TextChanged.Invoke);
			}
			dtEdit.DataBinding += new EventHandler(this.Edit_DataBinding);
			cell.Controls.Add(dtEdit);
		}

		private void Edit_DataBinding(object sender, EventArgs e)
		{
			TimDateTime dtEdit = (TimDateTime)sender;
			object value = this.GetValue(dtEdit.NamingContainer);
			dtEdit.Text = this.FormatDataValue(value, this.HtmlEncode);
		}
	}
}
