using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class TimXNumericTextBoxField : BoundField
	{
		private bool m_enabled = true;

		private double m_min = 0.0;

		private double m_max = 0.0;

		private int m_decimalPlaces = 2;

		private bool m_showZero = false;

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

		public double Min
		{
			get
			{
				return this.m_min;
			}
			set
			{
				this.m_min = value;
			}
		}

		public double Max
		{
			get
			{
				return this.m_max;
			}
			set
			{
				this.m_max = value;
			}
		}

		public int DecimalPlaces
		{
			get
			{
				return this.m_decimalPlaces;
			}
			set
			{
				this.m_decimalPlaces = value;
			}
		}

		public bool ShowZero
		{
			get
			{
				return this.m_showZero;
			}
			set
			{
				this.m_showZero = true;
			}
		}

		protected override DataControlField CreateField()
		{
			return new TimXNumericTextBoxField();
		}

		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			TimNumericTextBox ntEdit = new TimNumericTextBox();
			ntEdit.ID = "nt" + this.DataField;
			bool flag = base.Visible && base.ItemStyle.Width.Value > 2.0;
			if (flag)
			{
				ntEdit.Width = new Unit(base.ItemStyle.Width.Value - 2.0);
			}
			else
			{
				ntEdit.Width = base.ItemStyle.Width;
			}
			ntEdit.Enabled = this.Enabled;
			ntEdit.Min = this.Min;
			ntEdit.Max = this.Max;
			ntEdit.DecimalPlaces = this.DecimalPlaces;
			ntEdit.ShowZero = this.ShowZero;
			bool flag2 = this.TextChanged != null;
			if (flag2)
			{
				ntEdit.AutoPostBack = true;
				ntEdit.TextChanged += new EventHandler(this.TextChanged.Invoke);
			}
			ntEdit.DataBinding += new EventHandler(this.txtEdit_DataBinding);
			cell.Controls.Add(ntEdit);
		}

		private void div_DataBinding(object sender, EventArgs e)
		{
			HtmlGenericControl div = (HtmlGenericControl)sender;
			object value = this.GetValue(div.NamingContainer);
			div.InnerText = this.FormatDataValue(value, this.HtmlEncode);
		}

		private void txtEdit_DataBinding(object sender, EventArgs e)
		{
			TimNumericTextBox ntEdit = (TimNumericTextBox)sender;
			object value = this.GetValue(ntEdit.NamingContainer);
			ntEdit.Text = this.FormatDataValue(value, this.HtmlEncode);
		}
	}
}
