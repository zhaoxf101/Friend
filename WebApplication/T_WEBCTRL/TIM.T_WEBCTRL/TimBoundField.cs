using System;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class TimBoundField : BoundField
	{
		private BoundFieldMode m_mode = BoundFieldMode.String;

		private int m_decimalPlaces = 2;

		private bool m_ShowZero = false;

		private bool m_tips = true;

		private bool m_wordBreak = false;

		public BoundFieldMode Mode
		{
			get
			{
				return this.m_mode;
			}
			set
			{
				this.m_mode = value;
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
				return this.m_ShowZero;
			}
			set
			{
				this.m_ShowZero = value;
			}
		}

		public bool Tips
		{
			get
			{
				return this.m_tips;
			}
			set
			{
				this.m_tips = value;
			}
		}

		public bool WordBreak
		{
			get
			{
				return this.m_wordBreak;
			}
			set
			{
				this.m_wordBreak = value;
			}
		}
	}
}
