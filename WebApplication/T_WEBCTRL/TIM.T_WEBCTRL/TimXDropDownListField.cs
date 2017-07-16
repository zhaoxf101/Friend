using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class TimXDropDownListField : BoundField
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
			return new TimXDropDownListField();
		}

		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			TimDropDownList ddlEdit = new TimDropDownList();
			ddlEdit.ID = "ddl" + this.DataField;
			bool flag = base.Visible && base.ItemStyle.Width.Value > 2.0;
			if (flag)
			{
				ddlEdit.Width = new Unit(base.ItemStyle.Width.Value - 2.0);
			}
			else
			{
				ddlEdit.Width = base.ItemStyle.Width;
			}
			ddlEdit.Enabled = this.Enabled;
			bool flag2 = this.TextChanged != null;
			if (flag2)
			{
				ddlEdit.AutoPostBack = true;
				ddlEdit.TextChanged += new EventHandler(this.TextChanged.Invoke);
			}
			ddlEdit.DataBinding += new EventHandler(this.Edit_DataBinding);
			cell.Controls.Add(ddlEdit);
		}

		private void Edit_DataBinding(object sender, EventArgs e)
		{
			TimDropDownList ddlEdit = (TimDropDownList)sender;
			object value = this.GetValue(ddlEdit.NamingContainer);
			ddlEdit.Text = this.FormatDataValue(value, this.HtmlEncode);
		}
	}
}
