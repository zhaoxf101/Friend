using System;
using System.Collections.Generic;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	public class RdDataSet : RdNode
	{
		private RdDataSet m_masterDataSet = null;

		private string m_comment = string.Empty;

		private int m_fieldCount = 1;

		private Dictionary<string, RdDataSetField> m_fields = new Dictionary<string, RdDataSetField>();

		private int m_pageRows = 0;

		private int m_pageColumns = 1;

		private bool m_singleRecord = false;

		private int m_outputLines = 0;

		private int m_forceNewPage = 0;

		private bool m_fillWithBlank = true;

		private string m_sql = string.Empty;

		internal RdDataSet MasterDataSet
		{
			get
			{
				return this.m_masterDataSet;
			}
			set
			{
				bool flag = this.m_masterDataSet != value;
				if (flag)
				{
					this.m_masterDataSet = value;
					this.DoOnMasterDataSetChanged();
				}
			}
		}

		public string Comment
		{
			get
			{
				return this.m_comment;
			}
			set
			{
				bool flag = this.m_comment != value;
				if (flag)
				{
					this.m_comment = value;
					this.DoOnCommentChanged();
				}
			}
		}

		internal int FieldCount
		{
			get
			{
				return this.m_fieldCount;
			}
		}

		internal int PageRows
		{
			get
			{
				return this.m_pageRows;
			}
			set
			{
				bool flag = this.m_pageRows != value;
				if (flag)
				{
					this.m_pageRows = value;
					this.DoOnPageRowsChanged();
				}
			}
		}

		internal int PageColumns
		{
			get
			{
				return this.m_pageColumns;
			}
			set
			{
				bool flag = this.m_pageColumns != value;
				if (flag)
				{
					this.m_pageColumns = value;
					this.DoOnPageColumnsChanged();
				}
			}
		}

		public bool SingleRecord
		{
			get
			{
				return this.m_singleRecord;
			}
			set
			{
				bool flag = this.m_singleRecord != value;
				if (flag)
				{
					this.m_singleRecord = value;
					this.DoOnSingleRecordChanged();
				}
			}
		}

		public int OutputLines
		{
			get
			{
				return this.m_outputLines;
			}
			set
			{
				bool flag = this.m_outputLines != value;
				if (flag)
				{
					this.m_outputLines = value;
					this.DoOnOutputLinesChanged();
				}
			}
		}

		public int ForceNewPage
		{
			get
			{
				return this.m_forceNewPage;
			}
			set
			{
				bool flag = this.m_forceNewPage != value;
				if (flag)
				{
					this.m_forceNewPage = value;
					this.DoOnForceNewPageChanged();
				}
			}
		}

		public bool FillWithBlank
		{
			get
			{
				return this.m_fillWithBlank;
			}
			set
			{
				bool flag = this.m_fillWithBlank != value;
				if (flag)
				{
					this.m_fillWithBlank = value;
					this.DoOnFillWithBlankChanged();
				}
			}
		}

		public string Sql
		{
			get
			{
				return this.m_sql;
			}
			set
			{
				bool flag = this.m_sql != value;
				if (flag)
				{
					this.m_sql = value;
					this.DoOnSQLChanged();
				}
			}
		}

		protected void DoOnCommentChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnMasterDataSetChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnSingleRecordChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnOutputLinesChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnForceNewPageChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnFillWithBlankChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnFieldChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnPageColumnsChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnPageRowsChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnSQLChanged()
		{
			base.Document.Changed();
		}

		public RdDataSet(RdDocument document) : base(document)
		{
			this.m_pageRows = 0;
			this.m_pageColumns = 1;
			this.m_singleRecord = false;
			this.m_outputLines = 0;
			this.m_forceNewPage = 0;
			this.m_fillWithBlank = true;
		}

		internal void AddField(string fieldValue)
		{
			bool flag = string.IsNullOrEmpty(fieldValue);
			if (!flag)
			{
				string[] listFieldValue = fieldValue.Split(new char[]
				{
					'|'
				});
				RdDataSetField dataSetField = new RdDataSetField();
				dataSetField.Id = listFieldValue[0];
				dataSetField.Name = listFieldValue[1];
				dataSetField.Type = (listFieldValue[2].Equals("0") ? RdFieldType.gfString : RdFieldType.gfNumeric);
				bool flag2 = !this.m_fields.ContainsKey(dataSetField.Id);
				if (flag2)
				{
					this.m_fields.Add(dataSetField.Id, dataSetField);
				}
			}
		}

		public void AddField(string fieldId, string fieldName, RdFieldType fieldType)
		{
			bool flag = string.IsNullOrEmpty(fieldId) || string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(fieldId.Trim()) || string.IsNullOrEmpty(fieldName.Trim());
			if (!flag)
			{
				RdDataSetField dataSetField = new RdDataSetField();
				dataSetField.Id = fieldId.Trim();
				dataSetField.Name = fieldName.Trim();
				dataSetField.Type = fieldType;
				bool flag2 = !this.m_fields.ContainsKey(dataSetField.Id);
				if (flag2)
				{
					this.m_fieldCount++;
					this.m_fields.Add(dataSetField.Id, dataSetField);
				}
			}
		}

		internal override void Load(XmlNode node)
		{
			base.Load(node);
			this.m_comment = Utils.GetXmlNodeAttribute(node, "Comment");
			this.m_singleRecord = Utils.GetXmlNodeAttribute(node, "SingleRecord").Equals("Y");
			this.m_outputLines = Utils.Str2Int(Utils.GetXmlNodeAttribute(node, "OutputLines"), 0);
			this.m_forceNewPage = Utils.Str2Int(Utils.GetXmlNodeAttribute(node, "ForceNewPage"), 0);
			this.m_fillWithBlank = Utils.GetXmlNodeAttribute(node, "FillWithBlank").Equals("Y");
			this.m_pageRows = Utils.Str2Int(Utils.GetXmlNodeAttribute(node, "PageRows"), 0);
			this.m_pageColumns = Utils.Str2Int(Utils.GetXmlNodeAttribute(node, "PageColumns"), 1);
			this.m_sql = Utils.GetXmlNodeAttribute(node, "SQL");
			this.m_fieldCount = Utils.Str2Int(Utils.GetXmlNodeAttribute(node, "FieldCount"), 0);
			for (int i = 1; i <= this.m_fieldCount; i++)
			{
				this.AddField(Utils.GetXmlNodeAttribute(node, "Field" + i.ToString()));
			}
			bool hasChildNodes = node.HasChildNodes;
			if (hasChildNodes)
			{
				for (XmlNode nodeChild = node.FirstChild; nodeChild != null; nodeChild = nodeChild.NextSibling)
				{
					bool flag = nodeChild.Name == "DataSet";
					if (flag)
					{
						RdDataSet gtrDataSet = new RdDataSet(base.Document);
						gtrDataSet.MasterDataSet = this;
						gtrDataSet.Load(nodeChild);
						base.Document.DataSets.List.Add(gtrDataSet.Name, gtrDataSet);
					}
				}
			}
		}

		internal override string GetXml()
		{
			string result = base.GetXml();
			foreach (KeyValuePair<string, RdDataSet> value in base.Document.DataSets.List)
			{
				bool flag = value.Value.MasterDataSet == this;
				if (flag)
				{
					result += value.Value.GetXml();
				}
			}
			result = string.Concat(new string[]
			{
				"<DataSet",
				this.GetAttributes(),
				">",
				result,
				"</DataSet>"
			});
			return result;
		}

		internal override string GetAttributes()
		{
			string result = base.GetAttributes();
			bool flag = !string.IsNullOrEmpty(this.m_comment);
			if (flag)
			{
				result = result + " Comment=\"" + this.m_comment + "\"";
			}
			bool singleRecord = this.m_singleRecord;
			if (singleRecord)
			{
				result = result + " SingleRecord=\"" + Utils.Bool2Str(this.m_singleRecord) + "\"";
			}
			bool flag2 = this.m_outputLines != 0;
			if (flag2)
			{
				result = result + " OutputLines=\"" + this.m_outputLines.ToString() + "\"";
			}
			bool flag3 = this.m_forceNewPage != 0;
			if (flag3)
			{
				result = result + " ForceNewPage=\"" + this.m_forceNewPage.ToString() + "\"";
			}
			bool fillWithBlank = this.m_fillWithBlank;
			if (fillWithBlank)
			{
				result = result + " FillWithBlank=\"" + Utils.Bool2Str(this.m_fillWithBlank) + "\"";
			}
			bool flag4 = this.m_pageRows != 0;
			if (flag4)
			{
				result = result + " PageRows=\"" + this.m_pageRows.ToString() + "\"";
			}
			bool flag5 = this.m_pageColumns != 1;
			if (flag5)
			{
				result = result + " PageColumns=\"" + this.m_pageColumns.ToString() + "\"";
			}
			bool flag6 = !string.IsNullOrEmpty(this.m_sql);
			if (flag6)
			{
				result = result + " SQL=\"" + this.m_sql + "\"";
			}
			bool flag7 = this.FieldCount > 0;
			if (flag7)
			{
				result = result + " FieldCount=\"" + this.FieldCount.ToString() + "\"";
				int i = 1;
				foreach (KeyValuePair<string, RdDataSetField> value in this.m_fields)
				{
					bool flag8 = value.Key != "RECNO";
					if (flag8)
					{
						result = string.Concat(new object[]
						{
							result,
							" Field",
							i.ToString(),
							"=\"",
							value.Value.Id,
							"|",
							value.Value.Name,
							"|",
							(int)value.Value.Type,
							"\""
						});
						i++;
					}
				}
			}
			return result;
		}
	}
}
