using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdCells
	{
		private RdDocument m_document = null;

		private Dictionary<int, RdStyle> m_list = null;

		public RdDocument Document
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

		internal Dictionary<int, RdStyle> List
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

		public RdStyle this[int index]
		{
			get
			{
				bool flag = this.m_list.ContainsKey(index);
				RdStyle result;
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

		public RdCells(RdDocument document)
		{
			this.m_document = document;
			this.m_list = new Dictionary<int, RdStyle>();
		}

		public void Clear()
		{
			this.m_list.Clear();
		}

		public void Load(XmlNode node)
		{
			RdStyle _style = new RdStyle(this.Document);
			bool hasChildNodes = node.HasChildNodes;
			if (hasChildNodes)
			{
				for (XmlNode styleOrCellNode = node.FirstChild; styleOrCellNode != null; styleOrCellNode = styleOrCellNode.NextSibling)
				{
					bool flag = styleOrCellNode.Name == "Style";
					if (flag)
					{
						_style.Load(styleOrCellNode);
					}
					else
					{
						bool flag2 = styleOrCellNode.Name == "Cell";
						if (flag2)
						{
							int _rowNo = Utils.GetAttrInt(styleOrCellNode, "Row", -1);
							int _columnNo = Utils.GetAttrInt(styleOrCellNode, "Column", -1);
							bool flag3 = _rowNo > -1 && _columnNo >= -1;
							if (flag3)
							{
								bool flag4 = _rowNo > this.Document.Rows.RowCount;
								if (flag4)
								{
									this.Document.Rows.RowCount = _rowNo;
								}
								bool flag5 = _columnNo > this.Document.Columns.ColumnCount;
								if (flag5)
								{
									this.Document.Columns.ColumnCount = _columnNo;
								}
								this.Document.GetCell(_rowNo, _columnNo).Load(styleOrCellNode);
							}
						}
					}
				}
			}
		}

		public string GetXml()
		{
			string ret = string.Empty;
			this.ReBuild();
			int sleepCount = 0;
			foreach (KeyValuePair<int, RdStyle> value in this.m_list)
			{
				ret += value.Value.GetXml();
				sleepCount++;
				bool flag = sleepCount == 1000;
				if (flag)
				{
					sleepCount = 0;
					Thread.Sleep(1);
				}
			}
			return ret = "<Cells>" + ret + "</Cells>";
		}

		public void ReBuild()
		{
			this.Clear();
			RdCell _cell = null;
			bool _styleFount = false;
			for (int i = 0; i <= this.Document.Rows.RowCount; i++)
			{
				for (int j = 0; j <= this.Document.Columns.ColumnCount; j++)
				{
					_cell = this.Document.GetCell(i, j);
					_styleFount = false;
					foreach (KeyValuePair<int, RdStyle> value in this.m_list)
					{
						bool flag = Utils.StyleRecEqual(value.Value.GetStyle(), _cell.Style);
						if (flag)
						{
							value.Value.List.Add(value.Value.List.Count, _cell);
							_styleFount = true;
							break;
						}
					}
					bool flag2 = !_styleFount;
					if (flag2)
					{
						RdStyle _style = new RdStyle(this.Document);
						_style.Id = this.m_list.Count + 1;
						_style.List.Add(_style.List.Count, _cell);
						this.m_list.Add(this.m_list.Count + 1, _style);
					}
				}
			}
		}

		public int GetCellStyleID(RdCell cell)
		{
			int ret = -1;
			foreach (KeyValuePair<int, RdStyle> value in this.m_list)
			{
			}
			return ret;
		}
	}
}
