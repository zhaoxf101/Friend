using System;
using System.Collections.Generic;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RmRows
	{
		private RmRow m_row = null;

		private RmDataSet m_dataset = null;

		private Dictionary<int, RmRow> m_items = new Dictionary<int, RmRow>();

		private int columnHeaderStart = -1;

		private int columnHeaderEnd = -1;

		private int columnFooterStart = -1;

		private int columnFooterEnd = -1;

		private int m_rowStart = -1;

		private int m_rowEnd = -1;

		internal RmRow Row
		{
			get
			{
				return this.m_row;
			}
			set
			{
				this.m_row = value;
			}
		}

		internal RmDataSet Dataset
		{
			get
			{
				return this.m_dataset;
			}
			set
			{
				this.m_dataset = value;
			}
		}

		internal Dictionary<int, RmRow> Items
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

		public int ColumnHeaderStart
		{
			get
			{
				return this.columnHeaderStart;
			}
			set
			{
				this.columnHeaderStart = value;
			}
		}

		public int ColumnHeaderEnd
		{
			get
			{
				return this.columnHeaderEnd;
			}
			set
			{
				this.columnHeaderEnd = value;
			}
		}

		public int ColumnFooterStart
		{
			get
			{
				return this.columnFooterStart;
			}
			set
			{
				this.columnFooterStart = value;
			}
		}

		public int ColumnFooterEnd
		{
			get
			{
				return this.columnFooterEnd;
			}
			set
			{
				this.columnFooterEnd = value;
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

		public RmRows(RmRow row)
		{
			this.m_row = row;
			this.m_items = new Dictionary<int, RmRow>();
		}
	}
}
