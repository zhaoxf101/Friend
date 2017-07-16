using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdStyle : RdContentNode
	{
		private int m_id = 0;

		private Dictionary<int, RdCell> m_list = null;

		public int Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		internal Dictionary<int, RdCell> List
		{
			get
			{
				return this.m_list;
			}
			set
			{
				this.m_list = value;
			}
		}

		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		public RdCell this[int index]
		{
			get
			{
				bool flag = this.m_list.ContainsKey(index);
				RdCell result;
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

		public RdStyle(RdDocument document) : base(document)
		{
			this.m_list = new Dictionary<int, RdCell>();
		}

		public RdMergeCellsStyle GetStyle()
		{
			bool flag = this.Count == 0;
			RdMergeCellsStyle ret;
			if (flag)
			{
				ret = base.Document.RootCell.Style;
			}
			else
			{
				ret = this.m_list[0].Style;
			}
			return ret;
		}

		internal override void Load(XmlNode node)
		{
			RdMergeCellsStyle _curStyle = new RdMergeCellsStyle();
			Utils.StyleRecLoad(node, ref _curStyle, base.Document.DefaultStyle);
			this.LoadEmptyCells(node, _curStyle);
			bool hasChildNodes = node.HasChildNodes;
			if (hasChildNodes)
			{
				for (XmlNode cellNode = node.FirstChild; cellNode != null; cellNode = cellNode.NextSibling)
				{
					bool flag = cellNode.Name == "Cell";
					if (flag)
					{
						int _rowNo = Utils.GetAttrInt(cellNode, "Row", -1);
						int _columnNo = Utils.GetAttrInt(cellNode, "Column", -1);
						bool flag2 = _rowNo > base.Document.Rows.RowCount;
						if (flag2)
						{
							base.Document.Rows.RowCount = _rowNo;
						}
						bool flag3 = _columnNo > base.Document.Columns.ColumnCount;
						if (flag3)
						{
							base.Document.Columns.ColumnCount = _columnNo;
						}
						RdCell _cell = base.Document.GetCell(_rowNo, _columnNo);
						bool flag4 = _cell == null;
						if (flag4)
						{
							break;
						}
						bool flag5 = _cell != null;
						if (flag5)
						{
							_cell.Load(cellNode);
						}
						bool flag6 = _cell.HiddenBy == null;
						if (flag6)
						{
							RdMergeCellsStyle tmpStyle = new RdMergeCellsStyle();
							Utils.CopyStyleRec(ref tmpStyle, _curStyle);
							_cell.Style = tmpStyle;
						}
						bool flag7 = _cell.Height > 1 && _rowNo + _cell.Height - 1 > base.Document.Rows.RowCount;
						if (flag7)
						{
							base.Document.Rows.RowCount = _rowNo + _cell.Height - 1;
						}
						bool flag8 = _cell.Width > 1 && _columnNo + _cell.Width - 1 > base.Document.Columns.ColumnCount;
						if (flag8)
						{
							base.Document.Columns.ColumnCount = _columnNo + _cell.Width - 1;
						}
					}
				}
			}
		}

		internal override string GetAttributes()
		{
			string ret = base.GetAttributes();
			bool flag = this.Count > 0;
			if (flag)
			{
				ret += Utils.StyleRecGetAttributes(this.m_list[0].Style, base.Document.DefaultStyle);
			}
			return ret;
		}

		internal override string GetXml()
		{
			StringBuilder ret = new StringBuilder(5000);
			StringBuilder EmptyCells = new StringBuilder(500);
			StringBuilder Attributes = new StringBuilder(500);
			StringBuilder cells = new StringBuilder(3000);
			bool UseEmptyCells = false;
			foreach (KeyValuePair<int, RdCell> value in this.m_list)
			{
				string cellXml = value.Value.GetXml();
				bool flag = string.IsNullOrEmpty(cellXml);
				if (flag)
				{
					bool flag2 = UseEmptyCells;
					if (flag2)
					{
						EmptyCells.Append(";");
						EmptyCells.Append(string.Concat(new string[]
						{
							"(",
							value.Value.Row.ToString(),
							",",
							value.Value.Column.ToString(),
							")"
						}));
					}
					else
					{
						EmptyCells.Append(string.Concat(new string[]
						{
							"(",
							value.Value.Row.ToString(),
							",",
							value.Value.Column.ToString(),
							")"
						}));
						UseEmptyCells = true;
					}
				}
				else
				{
					cells.Append(cellXml);
				}
			}
			Attributes.Append(this.GetAttributes());
			bool flag3 = this.Id != 0 & UseEmptyCells;
			if (flag3)
			{
				Attributes.Append(Utils.MakeAttribute("EmptyCells", EmptyCells.ToString()));
			}
			ret.Append("<Style" + Attributes.ToString() + ">");
			ret.Append(cells);
			ret.Append("</Style>");
			return ret.ToString();
		}

		private void LoadEmptyCells(XmlNode node, RdMergeCellsStyle style)
		{
			string _emptyCells = Utils.GetXmlNodeAttribute(node, "EmptyCells");
			bool flag = !string.IsNullOrEmpty(_emptyCells);
			if (flag)
			{
				string rowColumn = string.Empty;
				int rowNo = -1;
				int columnNo = -1;
				string[] array = _emptyCells.Split(new char[]
				{
					';'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string value = array[i];
					bool flag2 = string.IsNullOrEmpty(value);
					if (flag2)
					{
						break;
					}
					rowColumn = value.Replace("(", "").Replace(")", "");
					rowNo = Utils.Str2Int(rowColumn.Split(new char[]
					{
						','
					})[0], rowNo);
					columnNo = Utils.Str2Int(rowColumn.Split(new char[]
					{
						','
					})[1], columnNo);
					bool flag3 = rowNo > base.Document.Rows.RowCount;
					if (flag3)
					{
						base.Document.Rows.RowCount = rowNo;
					}
					bool flag4 = columnNo > base.Document.Columns.ColumnCount;
					if (flag4)
					{
						base.Document.Columns.ColumnCount = columnNo;
					}
					RdMergeCellsStyle tmpStyle = new RdMergeCellsStyle();
					Utils.CopyStyleRec(ref tmpStyle, style);
					base.Document.GetCell(rowNo, columnNo).Style = tmpStyle;
				}
			}
		}
	}
}
