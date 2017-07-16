using System;
using System.Collections.Generic;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	public class RdRows
	{
		private RdDocument m_document = null;

		private Dictionary<int, RdRow> m_list = null;

		internal RdDocument Document
		{
			get
			{
				return this.m_document;
			}
			set
			{
				this.m_document = value;
			}
		}

		internal RdRow this[int rowIndex]
		{
			get
			{
				bool flag = this.m_list.ContainsKey(rowIndex);
				RdRow result;
				if (flag)
				{
					result = this.m_list[rowIndex];
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public int RowCount
		{
			get
			{
				return this.m_list.Count - 1;
			}
			set
			{
				bool flag = value < 0;
				if (!flag)
				{
					int _rowCount = this.RowCount;
					bool flag2 = _rowCount == value;
					if (!flag2)
					{
						for (int i = 0; i <= this.Document.Columns.ColumnCount; i++)
						{
							this.Document.Columns[i].AdjustRowCount(value, false);
						}
						bool flag3 = _rowCount > value;
						if (flag3)
						{
							for (int j = value + 1; j <= _rowCount; j++)
							{
								bool flag4 = this.m_list.ContainsKey(j);
								if (flag4)
								{
									this.m_list.Remove(j);
								}
							}
						}
						else
						{
							int _columnCount = this.Document.Columns.ColumnCount;
							for (int k = _rowCount + 1; k <= value; k++)
							{
								RdRow _row = new RdRow(this.Document, k);
								this.m_list.Add(k, _row);
							}
							for (int l = 1; l <= _columnCount; l++)
							{
								this.Document.GetCell(_rowCount + 1, l).Style.TopBorderStyle = this.Document.GetCell(_rowCount, l).Style.BottomBorderStyle;
								this.Document.GetCell(_rowCount + 1, l).Style.TopBorderStyle = this.Document.GetCell(_rowCount, l).Style.BottomBorderStyle;
								this.Document.GetCell(_rowCount + 1, l).Style.TopBorderStyle = this.Document.GetCell(_rowCount, l).Style.BottomBorderStyle;
							}
						}
						this.Document.Changed();
					}
				}
			}
		}

		internal RdRows(RdDocument document)
		{
			this.m_document = document;
			this.m_list = new Dictionary<int, RdRow>();
		}

		internal void Load(XmlNode node)
		{
			bool hasChildNodes = node.HasChildNodes;
			if (hasChildNodes)
			{
				for (XmlNode rowNode = node.FirstChild; rowNode != null; rowNode = rowNode.NextSibling)
				{
					bool flag = rowNode.Name == "Row";
					if (flag)
					{
						int _rowNo = Utils.GetAttrInt(rowNode, "Row", 0);
						bool flag2 = _rowNo > 0;
						if (flag2)
						{
							bool flag3 = this.Document.Rows.RowCount < _rowNo;
							if (flag3)
							{
								this.Document.Rows.RowCount = _rowNo;
							}
							this.m_list[_rowNo].Load(rowNode);
						}
					}
				}
			}
		}

		internal string GetXml()
		{
			string ret = string.Empty;
			foreach (KeyValuePair<int, RdRow> value in this.m_list)
			{
				ret += value.Value.GetXml();
			}
			return ret = "<Rows>" + ret + "</Rows>";
		}

		internal int GetHeight(int rowIndex)
		{
			RdRow _row = this[rowIndex];
			bool flag = _row != null;
			int result;
			if (flag)
			{
				result = _row.Height;
			}
			else
			{
				result = this.Document.DefaultRowHeight;
			}
			return result;
		}

		internal void SetHeight(int rowIndex, int height)
		{
			bool flag = this.GetHeight(rowIndex) != height;
			if (flag)
			{
				bool flag2 = rowIndex > this.Document.Rows.RowCount;
				if (flag2)
				{
					this.Document.Rows.RowCount = rowIndex;
				}
				this[rowIndex].Height = height;
			}
		}

		internal void Clear()
		{
			this.m_list.Clear();
		}

		internal void Pack()
		{
			for (int rowIndex = this.Document.Rows.RowCount; rowIndex > 0; rowIndex--)
			{
				int columnIndex;
				for (columnIndex = 0; columnIndex <= this.Document.Columns.ColumnCount; columnIndex++)
				{
					bool flag = this.Document.GetCell(rowIndex, columnIndex).HiddenBy != null || this.Document.GetCell(rowIndex, columnIndex).GetXml() != "";
					if (flag)
					{
						break;
					}
				}
				bool flag2 = columnIndex <= this.Document.Columns.ColumnCount;
				if (flag2)
				{
					bool flag3 = rowIndex != this.Document.Rows.RowCount;
					if (flag3)
					{
						this.Document.Rows.RowCount = rowIndex;
					}
					break;
				}
			}
		}
	}
}
