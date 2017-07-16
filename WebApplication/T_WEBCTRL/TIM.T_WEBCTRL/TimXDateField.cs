using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class TimXDateField : BoundField
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
			return new TimXDateField();
		}

		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			TimDate datEdit = new TimDate();
			datEdit.ID = "dat" + this.DataField;
			bool flag = base.Visible && base.ItemStyle.Width.Value > 2.0;
			if (flag)
			{
				datEdit.Width = new Unit(base.ItemStyle.Width.Value - 2.0);
			}
			else
			{
				datEdit.Width = base.ItemStyle.Width;
			}
			datEdit.Enabled = this.Enabled;
			bool flag2 = this.TextChanged != null;
			if (flag2)
			{
				datEdit.AutoPostBack = true;
				datEdit.TextChanged += new EventHandler(this.TextChanged.Invoke);
			}
			datEdit.DataBinding += new EventHandler(this.Edit_DataBinding);
			cell.Controls.Add(datEdit);
		}

		private void Edit_DataBinding(object sender, EventArgs e)
		{
			TimDate datEdit = (TimDate)sender;
			object value = this.GetValue(datEdit.NamingContainer);
			datEdit.Text = this.FormatDataValue(value, this.HtmlEncode);
		}
	}
}
