using System;
using System.Drawing;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdRegion : RdContentNode
	{
		private RdNodeStyle m_definedStyles;

		private Rectangle m_boundary;

		private RdLineStyle m_innerHorzBorderStyle;

		private Color m_innerHorzBorderColor;

		private int m_innerHorzBorderWidth;

		private RdLineStyle m_innerVertBorderStyle;

		private Color m_innerVertBorderColor;

		private int m_innerVertBorderWidth;

		private RdRowType m_rowType;

		private string m_rowDataSet;

		private int m_rowHeight;

		private bool m_autoHeight;

		private string m_outputCondition;

		private string m_groupBy;

		private bool m_canBreakTwoPage;

		private RdColumnType m_columnType;

		private int m_columnWidth;

		private bool m_pageBreak;

		private RdRegionType m_regionType = RdRegionType.rtAll;

		public RdNodeStyle DefinedStyles
		{
			get
			{
				return this.m_definedStyles;
			}
			set
			{
				this.m_definedStyles = value;
			}
		}

		public Rectangle Boundary
		{
			get
			{
				return this.m_boundary;
			}
			set
			{
				this.m_boundary = value;
			}
		}

		public RdLineStyle InnerHorzBorderStyle
		{
			get
			{
				return this.m_innerHorzBorderStyle;
			}
			set
			{
				this.m_innerHorzBorderStyle = value;
			}
		}

		public Color InnerHorzBorderColor
		{
			get
			{
				return this.m_innerHorzBorderColor;
			}
			set
			{
				this.m_innerHorzBorderColor = value;
			}
		}

		public int InnerHorzBorderWidth
		{
			get
			{
				return this.m_innerHorzBorderWidth;
			}
			set
			{
				this.m_innerHorzBorderWidth = value;
			}
		}

		public RdLineStyle InnerVertBorderStyle
		{
			get
			{
				return this.m_innerVertBorderStyle;
			}
			set
			{
				this.m_innerVertBorderStyle = value;
			}
		}

		public Color InnerVertBorderColor
		{
			get
			{
				return this.m_innerVertBorderColor;
			}
			set
			{
				this.m_innerVertBorderColor = value;
			}
		}

		public int InnerVertBorderWidth
		{
			get
			{
				return this.m_innerVertBorderWidth;
			}
			set
			{
				this.m_innerVertBorderWidth = value;
			}
		}

		public RdRowType RowType
		{
			get
			{
				return this.m_rowType;
			}
			set
			{
				this.m_rowType = value;
			}
		}

		public string RowDataSet
		{
			get
			{
				return this.m_rowDataSet;
			}
			set
			{
				this.m_rowDataSet = value;
			}
		}

		public int RowHeight
		{
			get
			{
				return this.m_rowHeight;
			}
			set
			{
				this.m_rowHeight = value;
			}
		}

		public bool AutoHeight
		{
			get
			{
				return this.m_autoHeight;
			}
			set
			{
				this.m_autoHeight = value;
			}
		}

		public string OutputCondition
		{
			get
			{
				return this.m_outputCondition;
			}
			set
			{
				this.m_outputCondition = value;
			}
		}

		public string GroupBy
		{
			get
			{
				return this.m_groupBy;
			}
			set
			{
				this.m_groupBy = value;
			}
		}

		public bool CanBreakTwoPage
		{
			get
			{
				return this.m_canBreakTwoPage;
			}
			set
			{
				this.m_canBreakTwoPage = value;
			}
		}

		public RdColumnType ColumnType
		{
			get
			{
				return this.m_columnType;
			}
			set
			{
				this.m_columnType = value;
			}
		}

		public int ColumnWidth
		{
			get
			{
				return this.m_columnWidth;
			}
			set
			{
				this.m_columnWidth = value;
			}
		}

		public bool PageBreak
		{
			get
			{
				return this.m_pageBreak;
			}
			set
			{
				this.m_pageBreak = value;
			}
		}

		public RdRegionType RegionType
		{
			get
			{
				return this.m_regionType;
			}
			set
			{
				this.m_regionType = value;
			}
		}

		private void DoOnDataTypeChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDisplayFormatChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontNameChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontSizeChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontBoldChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontItalicChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontUnderlineChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontStrikeoutChanged()
		{
			base.Document.Changed();
		}

		private void DoOnCellBordersChanged()
		{
			base.Document.Changed();
		}

		private void DoOnLeftBorderStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnLeftBorderColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnLeftBorderWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTopBorderStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTopBorderColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTopBorderWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnRightBorderStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnRightBorderColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnRightBorderWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnBottomBorderStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnBottomBorderColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnBottomBorderWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLT2RBStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLT2RBColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLT2RBWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLB2RTStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLB2RTColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLB2RTWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTransparentChanged()
		{
			base.Document.Changed();
		}

		private void DoOnPatternChanged()
		{
			base.Document.Changed();
		}

		private void DoOnPatternColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnHAlignmentChanged()
		{
			base.Document.Changed();
		}

		private void DoOnVAlignmentChanged()
		{
			base.Document.Changed();
		}

		private void DoOnLeftMarginChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTopMarginChanged()
		{
			base.Document.Changed();
		}

		private void DoOnRightMarginChanged()
		{
			base.Document.Changed();
		}

		private void DoOnBottomMarginChanged()
		{
			base.Document.Changed();
		}

		private void DoOnThreePartTextChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTextControlChanged()
		{
			base.Document.Changed();
		}

		private void DoOnLineSpaceChanged()
		{
			base.Document.Changed();
		}

		private void DoOnLockedChanged()
		{
			base.Document.Changed();
		}

		private void DoOnPreviewChanged()
		{
			base.Document.Changed();
		}

		private void DoOnPrintChanged()
		{
			base.Document.Changed();
		}

		private void DoOnSmallFontWordWrapChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnInnerHorzBorderStyleChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnInnerHorzBorderColorChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnInnerHorzBorderWidthChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnInnerVertBorderStyleChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnInnerVertBorderColorChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnInnerVertBorderWidthChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnRowTypeChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnRowDataSetChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnRowHeightChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnAutoHeightChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnOutputConditionChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnGroupByChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnCanBreakTwoPageChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnColumnTypeChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnColumnWidthChanged()
		{
			base.Document.Changed();
		}

		protected virtual void DoOnPageBreakChanged()
		{
			base.Document.Changed();
		}

		public RdRegion(RdDocument document) : base(document)
		{
			this.m_boundary = new Rectangle(1, 1, 1, 1);
		}

		public RdRegionType GetRegionType()
		{
			bool flag = this.m_boundary.Right == 2147483647 && this.m_boundary.Bottom == 2147483647;
			RdRegionType result;
			if (flag)
			{
				result = RdRegionType.rtAll;
			}
			else
			{
				bool flag2 = this.m_boundary.Right == 2147483647;
				if (flag2)
				{
					result = RdRegionType.rtRows;
				}
				else
				{
					bool flag3 = this.m_boundary.Bottom == 2147483647;
					if (flag3)
					{
						result = RdRegionType.rtColumns;
					}
					else
					{
						result = RdRegionType.rtRect;
					}
				}
			}
			return result;
		}

		public void SetBoundary(Rectangle value)
		{
		}

		public new string DoOnGetName()
		{
			string ret = string.Empty;
			bool flag = this.Boundary.Right != 2147483647 && this.Boundary.Bottom != 2147483647;
			if (flag)
			{
				ret = Utils.EncodeRangeId(this.Boundary.Left, this.Boundary.Top, this.Boundary.Right, this.Boundary.Bottom);
			}
			return ret;
		}

		public void RefreshStyles()
		{
			this.Init();
			switch (this.RegionType)
			{
			case RdRegionType.rtAll:
				this.Refresh(1, 1, base.Document.Columns.ColumnCount + 1, base.Document.Rows.RowCount + 1);
				break;
			case RdRegionType.rtRows:
				this.Refresh(1, this.Boundary.Top, base.Document.Columns.ColumnCount + 1, this.Boundary.Bottom);
				this.RefreshRowAttributes();
				break;
			case RdRegionType.rtColumns:
				this.Refresh(this.Boundary.Left, 1, this.Boundary.Right, base.Document.Rows.RowCount + 1);
				this.RefreshColumnAttributes();
				break;
			case RdRegionType.rtRect:
				this.Refresh(this.Boundary.Left, this.Boundary.Top, this.Boundary.Right, this.Boundary.Bottom);
				break;
			}
		}

		private void Init()
		{
			RdMergeCellsStyle _style = base.Document.GetCellStyle(this.Boundary.Top, this.Boundary.Left);
		}

		private void Refresh(int left, int top, int right, int bottom)
		{
		}

		private void RefreshRowAttributes()
		{
		}

		private void RefreshColumnAttributes()
		{
		}
	}
}
