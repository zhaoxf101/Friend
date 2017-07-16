using System;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdRow : RdNode
	{
		private int m_row = 0;

		private RdRowType m_rowType = RdRowType.rtDetailData;

		private int m_height = 1;

		private bool m_autoHeight = false;

		private int m_calcHeight = -1;

		private string m_dataSet = string.Empty;

		private string m_groupBy = string.Empty;

		private string m_outputCondition = string.Empty;

		private string m_outputConditionResult = string.Empty;

		private bool m_pageBreak = false;

		private bool m_canBreakTwoPage = false;

		private RdRow m_expandedBy = null;

		public int Row
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

		public int Height
		{
			get
			{
				bool autoHeight = this.AutoHeight;
				int result;
				if (autoHeight)
				{
					bool flag = this.m_calcHeight == -1;
					if (flag)
					{
						this.m_calcHeight = this.AutoCalcHeight();
					}
					result = this.m_calcHeight;
				}
				else
				{
					result = this.m_height;
				}
				return result;
			}
			set
			{
				bool flag = !this.AutoHeight && this.m_height != value;
				if (flag)
				{
					this.m_height = value;
					this.DoOnHeightChanged();
				}
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
				bool flag = this.m_autoHeight != value;
				if (flag)
				{
					this.m_autoHeight = value;
					if (value)
					{
						this.CalcHeight = -1;
					}
					this.DoOnHeightChanged();
				}
			}
		}

		public int CalcHeight
		{
			get
			{
				return this.m_calcHeight;
			}
			set
			{
				this.m_calcHeight = value;
			}
		}

		public string DataSet
		{
			get
			{
				return this.m_dataSet;
			}
			set
			{
				this.m_dataSet = value;
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
				bool flag = !this.m_groupBy.Equals(value);
				if (flag)
				{
					this.m_groupBy = value;
					this.DoOnGroupByChanged();
				}
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
				bool flag = !this.m_outputCondition.Equals(value);
				if (flag)
				{
					this.m_outputCondition = value;
					this.DoOnOutputConditionChanged();
				}
			}
		}

		public string OutputConditionResult
		{
			get
			{
				return this.m_outputConditionResult;
			}
			set
			{
				this.m_outputConditionResult = value;
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
				bool flag = this.m_pageBreak != value;
				if (flag)
				{
					this.m_pageBreak = value;
					this.DoOnPageBreakChanged();
				}
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
				bool flag = this.m_canBreakTwoPage != value;
				if (flag)
				{
					this.m_canBreakTwoPage = value;
					this.DoOnCanBreakTwoPageChanged();
				}
			}
		}

		internal RdRow ExpandedBy
		{
			get
			{
				return this.m_expandedBy;
			}
			set
			{
				this.m_expandedBy = value;
			}
		}

		protected void DoOnHeightChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnPageBreakChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnOutputConditionChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnCanBreakTwoPageChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnGroupByChanged()
		{
			base.Document.Changed();
		}

		public RdRow(RdDocument document, int rowIndex) : base(document)
		{
			this.Row = rowIndex;
			this.RowType = RdRowType.rtDetailData;
			this.Height = base.Document.DefaultRowHeight;
			this.PageBreak = false;
			this.CalcHeight = -1;
			this.AutoHeight = true;
		}

		protected override void DoOnGetName()
		{
			base.Name = string.Format("行[{0}]", this.Row);
		}

		internal override void Load(XmlNode node)
		{
			base.Load(node);
			this.AutoHeight = Utils.GetAttrBool(node, "AutoHeight", true);
			this.Height = Utils.GetAttrInt(node, "Height", base.Document.DefaultRowHeight);
			this.RowType = Utils.Str2RowType(Utils.GetXmlNodeAttribute(node, "RowType"), RdRowType.rtDetailData);
			bool flag = this.RowType == RdRowType.rtDetailData || this.RowType == RdRowType.rtColumnHeader || this.RowType == RdRowType.rtColumnFooter;
			if (flag)
			{
				this.DataSet = Utils.GetXmlNodeAttribute(node, "DataSet");
			}
			else
			{
				this.DataSet = "";
			}
			this.PageBreak = Utils.GetAttrBool(node, "PageBreak", false);
			this.OutputCondition = Utils.GetXmlNodeAttribute(node, "OutputCondition");
			this.OutputConditionResult = Utils.GetXmlNodeAttribute(node, "OutputConditionResult");
			this.CanBreakTwoPage = Utils.GetAttrBool(node, "CanBreakTwoPage", false);
			this.GroupBy = Utils.GetXmlNodeAttribute(node, "GroupBy");
		}

		internal override string GetAttributes()
		{
			string ret = base.GetAttributes();
			bool flag = this.Height != base.Document.DefaultRowHeight;
			if (flag)
			{
				ret += Utils.MakeAttribute("Height", this.Height.ToString());
			}
			bool flag2 = this.RowType != RdRowType.rtDetailData;
			if (flag2)
			{
				ret += Utils.MakeAttribute("RowType", Utils.RowType2Str(this.RowType));
			}
			bool flag3 = !string.IsNullOrEmpty(this.DataSet);
			if (flag3)
			{
				ret += Utils.MakeAttribute("DataSet", this.DataSet);
			}
			bool flag4 = !this.AutoHeight;
			if (flag4)
			{
				ret += Utils.MakeAttribute("AutoHeight", Utils.Bool2Str(this.AutoHeight));
			}
			bool pageBreak = this.PageBreak;
			if (pageBreak)
			{
				ret += Utils.MakeAttribute("PageBreak", Utils.Bool2Str(this.PageBreak));
			}
			bool flag5 = !string.IsNullOrEmpty(this.OutputCondition);
			if (flag5)
			{
				ret += Utils.MakeAttribute("OutputCondition", this.OutputCondition);
			}
			bool flag6 = !string.IsNullOrEmpty(this.OutputConditionResult);
			if (flag6)
			{
				ret += Utils.MakeAttribute("OutputConditionResult", this.OutputConditionResult);
			}
			bool canBreakTwoPage = this.CanBreakTwoPage;
			if (canBreakTwoPage)
			{
				ret += Utils.MakeAttribute("CanBreakTwoPage", Utils.Bool2Str(this.CanBreakTwoPage));
			}
			bool flag7 = !string.IsNullOrEmpty(this.GroupBy);
			if (flag7)
			{
				ret += Utils.MakeAttribute("GroupBy", this.GroupBy);
			}
			return ret;
		}

		internal override string GetXml()
		{
			string ret = this.GetAttributes();
			bool flag = !string.IsNullOrEmpty(ret);
			if (flag)
			{
				ret = "<Row" + Utils.MakeAttribute("Row", this.Row.ToString()) + ret + "/>";
			}
			return ret;
		}

		public RdDataSet GetDataSetObject()
		{
			return null;
		}

		public RdCell GetCell(int columnIndex)
		{
			return base.Document.GetCell(this.Row, columnIndex);
		}

		public int AutoCalcHeight()
		{
			return 203;
		}
	}
}
