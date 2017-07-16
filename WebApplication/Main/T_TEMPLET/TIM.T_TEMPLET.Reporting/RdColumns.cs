using System;
using System.Collections.Generic;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	public class RdColumns
	{
		private RdDocument m_document = null;

		private Dictionary<int, RdColumn> m_list = null;

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

		internal RdColumn this[int index]
		{
			get
			{
				bool flag = this.m_list.ContainsKey(index);
				RdColumn result;
				if (flag)
				{
					result = this.m_list[index];
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public int ColumnCount
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
					int _columnCount = this.ColumnCount;
					bool flag2 = _columnCount == value;
					if (!flag2)
					{
						this.Document.Changed();
						bool flag3 = _columnCount > value;
						if (flag3)
						{
							for (int i = value + 1; i <= _columnCount; i++)
							{
								bool flag4 = this.m_list.ContainsKey(i);
								if (flag4)
								{
									this.m_list.Remove(i);
								}
							}
						}
						else
						{
							int _rowCount = this.Document.Rows.RowCount;
							for (int j = _columnCount + 1; j <= value; j++)
							{
								RdColumn _column = new RdColumn(this.Document, j);
								_column.AdjustRowCount(_rowCount, true);
								this.m_list.Add(j, _column);
							}
							for (int k = 1; k <= _rowCount; k++)
							{
								this.Document.GetCell(k, _columnCount + 1).Style.LeftBorderStyle = this.Document.GetCell(k, _columnCount).Style.RightBorderStyle;
								this.Document.GetCell(k, _columnCount + 1).Style.LeftBorderColor = this.Document.GetCell(k, _columnCount).Style.RightBorderColor;
								this.Document.GetCell(k, _columnCount + 1).Style.LeftBorderWidth = this.Document.GetCell(k, _columnCount).Style.RightBorderWidth;
							}
						}
					}
				}
			}
		}

		internal RdColumns(RdDocument document)
		{
			this.m_document = document;
			this.m_list = new Dictionary<int, RdColumn>();
		}

		internal void Load(XmlNode node)
		{
			bool hasChildNodes = node.HasChildNodes;
			if (hasChildNodes)
			{
				for (XmlNode columnNode = node.FirstChild; columnNode != null; columnNode = columnNode.NextSibling)
				{
					bool flag = columnNode.Name == "Column";
					if (flag)
					{
						int _columnNo = Utils.GetAttrInt(columnNode, "Column", 0);
						bool flag2 = _columnNo > 0;
						if (flag2)
						{
							bool flag3 = this.Document.Columns.ColumnCount < _columnNo;
							if (flag3)
							{
								this.Document.Columns.ColumnCount = _columnNo;
							}
							this.m_list[_columnNo].Load(columnNode);
						}
					}
				}
			}
		}

		internal string GetXml()
		{
			string ret = string.Empty;
			foreach (KeyValuePair<int, RdColumn> value in this.m_list)
			{
				ret += value.Value.GetXml();
			}
			return ret = "<Columns>" + ret + "</Columns>";
		}

		internal void Clear()
		{
			this.m_list.Clear();
		}

		internal int GetWidth(int columnIndex)
		{
			RdColumn _column = this[columnIndex];
			bool flag = _column != null;
			int result;
			if (flag)
			{
				result = _column.Width;
			}
			else
			{
				result = this.Document.DefaultColumnWidth;
			}
			return result;
		}

		internal void SetWidth(int columnIndex, int width)
		{
			bool flag = this.GetWidth(columnIndex) != width;
			if (flag)
			{
				bool flag2 = columnIndex > this.Document.Columns.ColumnCount;
				if (flag2)
				{
					this.Document.Columns.ColumnCount = columnIndex;
				}
				this[columnIndex].Width = width;
			}
		}

		internal void Pack()
		{
			for (int _columnIndex = this.Document.Columns.ColumnCount; _columnIndex > 0; _columnIndex--)
			{
				int _rowIndex;
				for (_rowIndex = 0; _rowIndex <= this.Document.Rows.RowCount; _rowIndex++)
				{
					bool flag = this.Document.GetCell(_rowIndex, _columnIndex).HiddenBy != null || !string.IsNullOrEmpty(this.Document.GetCell(_rowIndex, _columnIndex).GetXml());
					if (flag)
					{
						break;
					}
				}
				bool flag2 = _rowIndex <= this.Document.Rows.RowCount;
				if (flag2)
				{
					bool flag3 = _columnIndex != this.Document.Columns.ColumnCount;
					if (flag3)
					{
						this.Document.Columns.ColumnCount = _columnIndex;
					}
					break;
				}
			}
		}
	}
}
