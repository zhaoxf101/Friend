using System;
using System.Collections.Generic;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdColumn : RdNode
	{
		private Dictionary<int, RdCell> m_cells = null;

		private int m_column = 0;

		private RdColumnType m_columnType = RdColumnType.ctDetailData;

		private int m_width = 0;

		private bool m_pageBreak = false;

		public int Column
		{
			get
			{
				return this.m_column;
			}
			set
			{
				this.m_column = value;
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

		public int Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				bool flag = this.m_width != value;
				if (flag)
				{
					this.m_width = value;
					for (int i = 1; i <= base.Document.Rows.RowCount; i++)
					{
						base.Document.Rows[i].CalcHeight = -1;
					}
					this.DoOnWidthChanged();
				}
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

		public RdCell this[int rowIndex]
		{
			get
			{
				RdCell ret = null;
				bool flag = rowIndex >= 0 && rowIndex < this.m_cells.Count;
				if (flag)
				{
					ret = this.m_cells[rowIndex];
				}
				return ret;
			}
		}

		protected void DoOnWidthChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnPageBreakChanged()
		{
			base.Document.Changed();
		}

		public RdColumn(RdDocument document, int columnIndex) : base(document)
		{
			this.Column = columnIndex;
			this.ColumnType = RdColumnType.ctDetailData;
			this.Width = base.Document.DefaultColumnWidth;
			this.PageBreak = false;
			this.m_cells = new Dictionary<int, RdCell>();
		}

		protected override void DoOnGetName()
		{
			base.Name = string.Format("列[{0}]", this.Column);
		}

		internal override void Load(XmlNode node)
		{
			base.Load(node);
			this.Width = Utils.GetAttrInt(node, "Width", base.Document.DefaultColumnWidth);
			this.ColumnType = Utils.Str2ColumnType(Utils.GetXmlNodeAttribute(node, "ColumnType"), RdColumnType.ctDetailData);
			this.PageBreak = Utils.GetAttrBool(node, "PageBreak", false);
		}

		internal override string GetAttributes()
		{
			string ret = base.GetAttributes();
			bool flag = this.Width != base.Document.DefaultColumnWidth;
			if (flag)
			{
				ret += Utils.MakeAttribute("Width", this.Width.ToString());
			}
			bool flag2 = this.ColumnType != RdColumnType.ctDetailData;
			if (flag2)
			{
				ret += Utils.MakeAttribute("ColumnType", Utils.ColumnType2Str(this.ColumnType));
			}
			bool pageBreak = this.PageBreak;
			if (pageBreak)
			{
				ret += Utils.MakeAttribute("PageBreak", Utils.Bool2Str(this.PageBreak));
			}
			return ret;
		}

		internal override string GetXml()
		{
			string ret = this.GetAttributes();
			bool flag = !string.IsNullOrEmpty(ret);
			if (flag)
			{
				ret = "<Column" + Utils.MakeAttribute("Column", this.Column.ToString()) + ret + "/>";
			}
			return ret;
		}

		public RdCell GetCell(int rowIndex)
		{
			RdCell ret = null;
			bool flag = rowIndex >= 0 && rowIndex < this.m_cells.Count;
			if (flag)
			{
				ret = this.m_cells[rowIndex];
			}
			return ret;
		}

		public void Clear()
		{
			this.m_cells.Clear();
		}

		public void AdjustRowCount(int rowCount, bool copyFromRowHeader)
		{
			int _curRowCount = this.m_cells.Count - 1;
			bool flag = rowCount < 0;
			if (!flag)
			{
				bool flag2 = _curRowCount > rowCount;
				if (flag2)
				{
					for (int i = rowCount + 1; i <= _curRowCount; i++)
					{
						bool flag3 = this.m_cells.ContainsKey(i);
						if (flag3)
						{
							this.m_cells.Remove(i);
						}
					}
				}
				else
				{
					for (int j = _curRowCount + 1; j <= rowCount; j++)
					{
						RdCell _cell = new RdCell(base.Document, j, this.Column);
						RdMergeCellsStyle tmpStyle = new RdMergeCellsStyle();
						bool flag4 = j == 0 && this.Column == 0;
						if (flag4)
						{
							Utils.CopyStyleRec(ref tmpStyle, base.Document.DefaultStyle);
							_cell.Style = tmpStyle;
						}
						else
						{
							bool flag5 = j == 0 || this.Column == 0;
							if (flag5)
							{
								Utils.CopyStyleRec(ref tmpStyle, base.Document.GetCell(0, 0).Style);
								_cell.Style = tmpStyle;
							}
							else if (copyFromRowHeader)
							{
								Utils.CopyStyleRec(ref tmpStyle, base.Document.GetCell(j, 0).Style);
								_cell.Style = tmpStyle;
							}
							else
							{
								Utils.CopyStyleRec(ref tmpStyle, base.Document.GetCell(0, this.Column).Style);
								_cell.Style = tmpStyle;
							}
						}
						this.m_cells.Add(j, _cell);
					}
				}
			}
		}
	}
}
