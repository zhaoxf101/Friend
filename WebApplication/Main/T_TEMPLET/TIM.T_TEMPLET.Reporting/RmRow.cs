using System;
using System.Collections.Generic;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RmRow
	{
		private RmRows m_rows = null;

		private Dictionary<int, RmRows> m_items = new Dictionary<int, RmRows>();

		private int m_rowStart = -1;

		private int m_rowEnd = -1;

		internal RmRows Rows
		{
			get
			{
				return this.m_rows;
			}
			set
			{
				this.m_rows = value;
			}
		}

		internal Dictionary<int, RmRows> Items
		{
			get
			{
				return this.m_items;
			}
			set
			{
				this.m_items = value;
			}
		}

		public int RowStart
		{
			get
			{
				return this.m_rowStart;
			}
			set
			{
				this.m_rowStart = value;
			}
		}

		public int RowEnd
		{
			get
			{
				return this.m_rowEnd;
			}
			set
			{
				this.m_rowEnd = value;
			}
		}

		public RmRow(RmRows rows)
		{
			this.m_rows = rows;
			this.m_items = new Dictionary<int, RmRows>();
		}
	}
}
