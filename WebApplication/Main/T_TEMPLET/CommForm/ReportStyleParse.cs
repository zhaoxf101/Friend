using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Helper;
using TIM.T_TEMPLET.Page;
using TIM.T_TEMPLET.Reporting;
using TIM.T_TEMPLET.Reporting.ReportDLL;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.CommForm
{
	public class ReportStyleParse
	{
		private class Report_Field
		{
			public string sDataSetName;

			public string sID;

			public string sName;

			public string sType;
		}

		public class StrMap
		{
			private string _sID = "";

			private string _sValue = "";

			private string _sType = "";

			public string sID
			{
				get
				{
					return this._sID;
				}
				set
				{
					this._sID = value;
				}
			}

			public string sValue
			{
				get
				{
					return this._sValue;
				}
				set
				{
					this._sValue = value;
				}
			}

			public string sType
			{
				get
				{
					return this._sType;
				}
				set
				{
					this._sType = value;
				}
			}

			public StrMap()
			{
			}

			public StrMap(string TmpID, string TmpValue, string TmpType)
			{
				this.sID = TmpID;
				this.sValue = TmpValue;
				this.sType = TmpType;
			}
		}

		private class Report_Table
		{
			public string sDataSetName;

			public HSQL sSQL;
		}

		public class OpenReport_Table
		{
			public string sDataSetName;

			public DataSet dsDataSet;

			public int iRecordCount;

			public int iDQJL;
		}

		private class OpenSubReport_Table
		{
			public string sDataSetName;

			public string sFatherDataSetName;

			public string sSQL;
		}

		public delegate void TOnReportUnknownFunction(string Name, string Params, ref string ValueType, ref string Value);

		public delegate void TOnReportUnknownVariable(string Name, ref string ValueType, ref string Value);

		public delegate void TOnReportDocUnknownFunction(string Name, string Params, ref string ValueType, ref string Value);

		public delegate void TOnReportDocUnknownVariablen(string Name, ref string ValueType, ref string Value);

		private string m_title = string.Empty;

		private Dictionary<string, ReportStyleParse.StrMap> _AllVariable = new Dictionary<string, ReportStyleParse.StrMap>();

		private string _ReportXMLDataSets = "";

		private ArrayList ReportDataSet = new ArrayList();

		private Dictionary<string, string> DicCustomVar = new Dictionary<string, string>();

		private ArrayList ReportTable = new ArrayList();

		private Dictionary<string, ReportStyleParse.StrMap> _AllVariableValue = new Dictionary<string, ReportStyleParse.StrMap>();

		private Dictionary<string, DataSet> UserReportDataSet = new Dictionary<string, DataSet>();

		private Dictionary<string, HSQL> UserReportDataSetSQL = new Dictionary<string, HSQL>();

		public ArrayList OpenReportTable = new ArrayList();

		private ArrayList OpenSubReportTable = new ArrayList();

		private string _run = "";

		private bool _ChooseTemplate = false;

		//[method: CompilerGenerated]
		////[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event ReportStyleParse.TOnReportUnknownFunction OnReportUnknownFunction;

		//[method: CompilerGenerated]
		////[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event ReportStyleParse.TOnReportUnknownVariable OnReportUnknownVariable;

		//[method: CompilerGenerated]
		////[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event ReportStyleParse.TOnReportDocUnknownFunction OnReportDocUnknownFunction;

		[method: CompilerGenerated]
		////[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event ReportStyleParse.TOnReportDocUnknownVariablen OnReportDocUnknownVariable;

		public string Title
		{
			get
			{
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		public Dictionary<string, ReportStyleParse.StrMap> AllVariable
		{
			get
			{
				return this._AllVariable;
			}
		}

		public string ReportXMLDataSets
		{
			get
			{
				return this._ReportXMLDataSets;
			}
		}

		public Dictionary<string, ReportStyleParse.StrMap> AllVariableValue
		{
			get
			{
				return this._AllVariableValue;
			}
		}

		public string run
		{
			get
			{
				return this._run;
			}
		}

		public bool ChooseTemplate
		{
			get
			{
				return this._ChooseTemplate;
			}
		}

		public void AddReportField(string sTable, string sFieldName, string sFieldDesc, string sFieldType)
		{
			ReportStyleParse.Report_Field bbField = new ReportStyleParse.Report_Field();
			bbField.sDataSetName = sTable;
			bbField.sID = sFieldName;
			bbField.sName = sFieldDesc;
			bbField.sType = sFieldType;
			this.ReportDataSet.Add(bbField);
		}

		public void ReportCustomVarAdd(string VarID, string VarnName)
		{
			this.DicCustomVar.Add(VarID, VarnName);
		}

		public void ReportDataSetAdd(string datasetName, EntityManager entity, Dictionary<int, XGridSetMap> listXGridSetMap)
		{
			int count = entity.Fields.Count;
			bool flag = count != 0;
			if (flag)
			{
				foreach (KeyValuePair<string, FieldMapAttribute> de in entity.Fields)
				{
					bool flag2 = !de.Value.PrintVisible;
					if (!flag2)
					{
						bool flag3 = de.Value.DbType == TimDbType.Float;
						if (flag3)
						{
							this.AddReportField(datasetName, de.Value.DbField, de.Value.Desc, "N");
							this._AllVariable.Add(de.Value.DbField, new ReportStyleParse.StrMap(de.Value.DbField, de.Value.Desc, "N"));
						}
						else
						{
							this.AddReportField(datasetName, de.Value.DbField, de.Value.Desc, "S");
							this._AllVariable.Add(de.Value.DbField, new ReportStyleParse.StrMap(de.Value.DbField, de.Value.Desc, "S"));
						}
					}
				}
			}
			foreach (KeyValuePair<int, XGridSetMap> tab in listXGridSetMap)
			{
				TimXGrid grid = tab.Value.XGrid;
				bool flag4 = grid != null;
				if (flag4)
				{
					for (int i = 0; i < grid.Columns.Count; i++)
					{
						DataControlField column = grid.Columns[i];
						bool flag5 = column.Visible && column.ItemStyle.Width.Value > 0.0 && !(column is TemplateField);
						if (flag5)
						{
							bool flag6 = column is TimXNumericTextBoxField;
							if (flag6)
							{
								this.AddReportField(tab.Value.GridEntity.Entity.Table, ((BoundField)column).DataField, column.HeaderText, "N");
							}
							else
							{
								this.AddReportField(tab.Value.GridEntity.Entity.Table, ((BoundField)column).DataField, column.HeaderText, "S");
							}
						}
					}
				}
			}
		}

		public void ReportDataSetAdd(string datasetName, EntityManager entity)
		{
			int count = entity.Fields.Count;
			bool flag = count != 0;
			if (flag)
			{
				foreach (KeyValuePair<string, FieldMapAttribute> de in entity.Fields)
				{
					bool flag2 = !de.Value.PrintVisible;
					if (!flag2)
					{
						bool flag3 = de.Value.DbType == TimDbType.Float;
						if (flag3)
						{
							this.AddReportField(datasetName, de.Value.DbField, de.Value.Desc, "N");
							this._AllVariable.Add(de.Value.DbField, new ReportStyleParse.StrMap(de.Value.DbField, de.Value.Desc, "N"));
						}
						else
						{
							this.AddReportField(datasetName, de.Value.DbField, de.Value.Desc, "S");
							this._AllVariable.Add(de.Value.DbField, new ReportStyleParse.StrMap(de.Value.DbField, de.Value.Desc, "S"));
						}
					}
				}
			}
		}

		public void PrepareSetMode()
		{
			this.GetReportDataSet();
		}

		private bool Exists(int[] array, int iS)
		{
			int i = array.Length;
			bool flag = iS == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int j = 0; j < i; j++)
				{
					bool flag2 = iS == array[j];
					if (flag2)
					{
						result = true;
						return result;
					}
				}
				result = false;
			}
			return result;
		}

		private void GetReportDataSet()
		{
			string sRecordString = "";
			using (StringWriter sw = new StringWriter())
			{
				XmlWriter writer = XmlWriter.Create(sw, new XmlWriterSettings
				{
					OmitXmlDeclaration = true,
					Encoding = Encoding.UTF8
				});
				writer.WriteStartDocument();
				writer.WriteStartElement("Report");
				bool flag = string.IsNullOrWhiteSpace(this.Title);
				if (flag)
				{
					writer.WriteAttributeString("Title", "NULL");
				}
				else
				{
					writer.WriteAttributeString("Title", this.Title);
				}
				int iCount = this.ReportDataSet.Count;
				int[] array = new int[iCount];
				int i = 0;
				for (int j = 0; j < iCount; j++)
				{
					bool flag2 = !this.Exists(array, j);
					if (flag2)
					{
						string sDataSet = ((ReportStyleParse.Report_Field)this.ReportDataSet[j]).sDataSetName;
						writer.WriteStartElement(sDataSet);
						writer.WriteStartElement(((ReportStyleParse.Report_Field)this.ReportDataSet[j]).sID);
						writer.WriteAttributeString("NAME", ((ReportStyleParse.Report_Field)this.ReportDataSet[j]).sName);
						writer.WriteAttributeString("TYPE", ((ReportStyleParse.Report_Field)this.ReportDataSet[j]).sType);
						writer.WriteEndElement();
						array[i] = j;
						i++;
						for (int k = j + 1; k < iCount; k++)
						{
							bool flag3 = !this.Exists(array, k);
							if (flag3)
							{
								bool flag4 = ((ReportStyleParse.Report_Field)this.ReportDataSet[k]).sDataSetName == sDataSet;
								if (flag4)
								{
									writer.WriteStartElement(((ReportStyleParse.Report_Field)this.ReportDataSet[k]).sID);
									writer.WriteAttributeString("NAME", ((ReportStyleParse.Report_Field)this.ReportDataSet[k]).sName);
									writer.WriteAttributeString("TYPE", ((ReportStyleParse.Report_Field)this.ReportDataSet[k]).sType);
									writer.WriteEndElement();
									array[i] = k;
									i++;
								}
							}
						}
						writer.WriteEndElement();
					}
				}
				writer.WriteStartElement("Variables");
				foreach (KeyValuePair<string, ReportStyleParse.StrMap> myVar in this._AllVariable)
				{
					bool flag5 = myVar.Key != null;
					if (flag5)
					{
						writer.WriteStartElement(myVar.Key);
						bool flag6 = myVar.Value != null;
						if (flag6)
						{
							writer.WriteAttributeString("NAME", myVar.Value.sValue);
						}
						else
						{
							writer.WriteAttributeString("NAME", "NULL");
						}
						writer.WriteAttributeString("TYPE", myVar.Value.sType);
						writer.WriteEndElement();
					}
				}
				foreach (KeyValuePair<string, string> myVar2 in this.DicCustomVar)
				{
					bool flag7 = myVar2.Key != null;
					if (flag7)
					{
						writer.WriteStartElement(myVar2.Key);
						bool flag8 = myVar2.Value != null;
						if (flag8)
						{
							writer.WriteAttributeString("NAME", myVar2.Value);
						}
						else
						{
							writer.WriteAttributeString("NAME", "NULL");
						}
						writer.WriteAttributeString("TYPE", "S");
						writer.WriteEndElement();
					}
				}
				writer.WriteEndElement();
				writer.WriteEndElement();
				writer.WriteEndDocument();
				writer.Flush();
				writer.Close();
				sRecordString = "<?xml version=\"1.0\" encoding=\"gb2312\"?>" + sw.ToString().Replace("'", "\"").Replace("\\", "\\\\");
				sRecordString = sRecordString.Replace("\r\n", " ");
			}
			this._ReportXMLDataSets = sRecordString;
		}

		public void AddReportTable(string datasetName, HSQL hsql)
		{
			ReportStyleParse.Report_Table bbTable = new ReportStyleParse.Report_Table();
			bbTable.sDataSetName = datasetName;
			bbTable.sSQL = hsql;
			this.ReportTable.Add(bbTable);
		}

		private void AddBillVarRecord()
		{
			Database db = LogicContext.GetDatabase();
			DataSet ds = db.OpenDataSet(((ReportStyleParse.Report_Table)this.ReportTable[0]).sSQL);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                foreach (DataColumn column in ds.Tables[0].Columns)
                {
                    string sText = row[column].ToString().Trim();
                    bool flag = sText == null || sText == "&nbsp;";
                    if (flag)
                    {
                        sText = "";
                    }
                    bool flag2 = column.DataType.ToString().Trim() == "System.Double";
                    if (flag2)
                    {
                        this._AllVariableValue.Add(column.ColumnName, new ReportStyleParse.StrMap(column.ColumnName, sText, "N"));
                    }
                    else
                    {
                        this._AllVariableValue.Add(column.ColumnName, new ReportStyleParse.StrMap(column.ColumnName, sText, "S"));
                    }
                }

            }

			//using (IEnumerator enumerator = ds.Tables[0].Rows.GetEnumerator())
			//{
			//	if (enumerator.MoveNext())
			//	{
			//		DataRow row = (DataRow)enumerator.Current;
					
			//	}
			//}
		}

		private void AddUserRecord()
		{
			foreach (KeyValuePair<string, DataSet> ds in this.UserReportDataSet)
			{
				this.AddReportTable(ds.Key.ToString(), null);
			}
			foreach (KeyValuePair<string, HSQL> sql in this.UserReportDataSetSQL)
			{
				this.AddReportTable(sql.Key.ToString(), sql.Value);
			}
		}

		public void ReportRecordSetAdd(string datasetName, EntityManager entity, HSQL hsql, Dictionary<int, XGridSetMap> listXGridSetMap)
		{
			this.AddReportTable(datasetName, hsql);
			this.AddBillVarRecord();
			foreach (KeyValuePair<int, XGridSetMap> tab in listXGridSetMap)
			{
				tab.Value.GridEntity.RecordSetSql = tab.Value.GridEntity.BuildRecordSetSql();
				foreach (KeyValuePair<string, RelatedEntityAttribute> item in entity.RelatedObject)
				{
					string[] keyList = item.Value.SlaveMainKey.Split(new char[]
					{
						','
					});
					for (int keyIndex = 0; keyIndex < keyList.Length; keyIndex++)
					{
						bool flag = tab.Value.GridEntity.RecordSetSql.ParamByName(keyList[keyIndex]) != null;
						if (flag)
						{
							tab.Value.GridEntity.RecordSetSql.AddParam(keyList[keyIndex], entity.Fields[item.Value.MasterMainKey.Split(new char[]
							{
								','
							})[keyIndex]].DbType, entity.Fields[item.Value.MasterMainKey.Split(new char[]
							{
								','
							})[keyIndex]].Len, entity.GetField(item.Value.MasterMainKey.Split(new char[]
							{
								','
							})[keyIndex]));
						}
					}
				}
				this.AddReportTable(tab.Value.GridEntity.Entity.Table, tab.Value.GridEntity.RecordSetSql);
			}
			this.AddUserRecord();
		}

		public void ReportRecordSetAdd(string datasetName, EntityManager entity, HSQL hsql)
		{
			this.AddReportTable(datasetName, hsql);
			this.AddBillVarRecord();
			this.AddUserRecord();
		}

		public void PreparePrintOrPreview(string styleId, string styleOrder, string PrintOrPreview)
		{
			bool flag = PrintOrPreview == "Print" || PrintOrPreview == "Preview";
			if (flag)
			{
				Database db = LogicContext.GetDatabase();
				this.GetReportStyle(styleId, styleOrder);
				this.OpenSubReportTable.Clear();
				this.OpenReportTable.Clear();
				for (int i = 0; i < this.ReportTable.Count; i++)
				{
					ReportStyleParse.OpenReport_Table dsOpen = new ReportStyleParse.OpenReport_Table();
					dsOpen.sDataSetName = ((ReportStyleParse.Report_Table)this.ReportTable[i]).sDataSetName;
					bool flag2 = ((ReportStyleParse.Report_Table)this.ReportTable[i]).sSQL == null;
					if (flag2)
					{
						foreach (KeyValuePair<string, DataSet> ds in this.UserReportDataSet)
						{
							bool flag3 = ds.Key == dsOpen.sDataSetName;
							if (flag3)
							{
								dsOpen.dsDataSet = ds.Value;
								dsOpen.iRecordCount = dsOpen.dsDataSet.Tables[0].Rows.Count;
								dsOpen.iDQJL = 0;
							}
						}
					}
					else
					{
						dsOpen.dsDataSet = db.OpenDataSet(((ReportStyleParse.Report_Table)this.ReportTable[i]).sSQL);
						dsOpen.iRecordCount = dsOpen.dsDataSet.Tables[0].Rows.Count;
						dsOpen.iDQJL = 0;
					}
					this.OpenReportTable.Add(dsOpen);
				}
				string styleResult = this.GetPrintOrPreviewReport(this.ChooseTemplate, this._ReportXMLDataSets);
				bool flag4 = PrintOrPreview.ToUpper() == "PREVIEW";
				if (flag4)
				{
					string datasetXml = string.Format(";document.all.ReportData.CompressUtoReport = '{0}'", styleResult);
					string LinkString = string.Format(";document.all.ReportData.UtoPrintOrPreview = '{0}';", "Preview");
					this._run = datasetXml + LinkString;
				}
				else
				{
					bool flag5 = PrintOrPreview.ToUpper() == "PRINT";
					if (flag5)
					{
						string datasetXml2 = string.Format(";document.all.ReportData.CompressUtoReport = '{0}'", styleResult);
						string LinkString2 = string.Format(";document.all.ReportData.UtoPrintOrPreview = '{0}';", "Print");
						this._run = datasetXml2 + LinkString2;
					}
				}
			}
		}

		public string PreparePrintOrPreviewHtml(string styleId, string styleOrder, string PrintOrPreview)
		{
			string HtmlFile = "";
			bool flag = PrintOrPreview == "Print" || PrintOrPreview == "Preview";
			if (flag)
			{
				Database db = LogicContext.GetDatabase();
				this.GetReportStyle(styleId, styleOrder);
				this.OpenSubReportTable.Clear();
				this.OpenReportTable.Clear();
				for (int i = 0; i < this.ReportTable.Count; i++)
				{
					ReportStyleParse.OpenReport_Table dsOpen = new ReportStyleParse.OpenReport_Table();
					dsOpen.sDataSetName = ((ReportStyleParse.Report_Table)this.ReportTable[i]).sDataSetName;
					bool flag2 = ((ReportStyleParse.Report_Table)this.ReportTable[i]).sSQL == null;
					if (flag2)
					{
						foreach (KeyValuePair<string, DataSet> ds in this.UserReportDataSet)
						{
							bool flag3 = ds.Key == dsOpen.sDataSetName;
							if (flag3)
							{
								dsOpen.dsDataSet = ds.Value;
								dsOpen.iRecordCount = dsOpen.dsDataSet.Tables[0].Rows.Count;
								dsOpen.iDQJL = 0;
							}
						}
					}
					else
					{
						dsOpen.dsDataSet = db.OpenDataSet(((ReportStyleParse.Report_Table)this.ReportTable[i]).sSQL);
						dsOpen.iRecordCount = dsOpen.dsDataSet.Tables[0].Rows.Count;
						dsOpen.iDQJL = 0;
					}
					this.OpenReportTable.Add(dsOpen);
				}
				HtmlFile = this.GetPrintOrPreviewReportHtml(this.ChooseTemplate, this._ReportXMLDataSets);
			}
			return HtmlFile;
		}

		public string GetPrintOrPreviewReportHtml(bool ChooseTemplate, string ReportXMLDataSets)
		{
			string sRtpFileTmpName = "";
			try
			{
				string reportResult;
				if (ChooseTemplate)
				{
					reportResult = UtoReport.GenerateReport(ReportXMLDataSets, new RmReportingMaker.GtReportPubGetValue(this.ReportCom_OnUtoReportCom));
				}
				else
				{
					reportResult = UtoReport.GenerateTemplete(ReportXMLDataSets);
					reportResult = UtoReport.GenerateReport(reportResult, new RmReportingMaker.GtReportPubGetValue(this.ReportCom_OnUtoReportCom));
				}
				bool flag = reportResult.IndexOf("ERROR:文件不存在！") > 0;
				if (flag)
				{
					reportResult = reportResult.Replace("ERROR:文件不存在！", " ");
				}
				string sPath = AppRuntime.AppRootPath + "RptFiles\\" + DateTime.Today.ToShortDateString();
				bool flag2 = !Directory.Exists(sPath);
				if (flag2)
				{
					Directory.CreateDirectory(sPath);
				}
				sRtpFileTmpName = "rpt" + DateTime.Now.ToString("Hmsfffffff") + ".html";
				string sRptFile = sPath + "\\" + sRtpFileTmpName;
				ISReport.TranRpt(reportResult, sRptFile);
			}
			catch (Exception e_E8)
			{
			}
			return sRtpFileTmpName;
		}

		public string GetPrintOrPreviewReport(bool ChooseTemplate, string ReportXMLDataSets)
		{
			string reportResult = "";
			try
			{
				if (ChooseTemplate)
				{
					reportResult = UtoReport.GenerateReport(ReportXMLDataSets, new RmReportingMaker.GtReportPubGetValue(this.ReportCom_OnUtoReportCom));
				}
				else
				{
					reportResult = UtoReport.GenerateTemplete(ReportXMLDataSets);
					reportResult = UtoReport.GenerateReport(reportResult, new RmReportingMaker.GtReportPubGetValue(this.ReportCom_OnUtoReportCom));
				}
				bool flag = reportResult.IndexOf("ERROR:文件不存在！") > 0;
				if (flag)
				{
					reportResult = reportResult.Replace("ERROR:文件不存在！", " ");
				}
				string sRptFiles = AppRuntime.AppRootPath + "\\RptFiles\\rtp.html";
				reportResult = DelphiTempletDesUtils.GetEncryZipBase64(reportResult);
			}
			catch (Exception e_7D)
			{
			}
			return reportResult;
		}

		private void ReportCom_OnUtoReportCom(string FunName, string Param1, string Param2, ref string Value1, ref string Value2)
		{
			bool flag = FunName.ToUpper() == "ONDATASETREAD";
			if (flag)
			{
				this.ReportOnDataSetRead(Param1, Param2, ref Value1, ref Value2);
			}
			else
			{
				bool flag2 = FunName.ToUpper() == "ONUNKNOWNFUNCTION";
				if (flag2)
				{
					this.ReportOnUnknownFunction(Param1, Param2, ref Value1, ref Value2);
				}
				else
				{
					bool flag3 = FunName.ToUpper() == "ONUNKNOWNVARIABLE";
					if (flag3)
					{
						this.ReportOnUnknownVariable(Param1, Param2, ref Value1, ref Value2);
					}
					else
					{
						bool flag4 = FunName.ToUpper() == "ONDATASETNEXT";
						if (flag4)
						{
							this.ReportOnDataSetNext(Param1, Param2, ref Value1, ref Value2);
						}
						else
						{
							bool flag5 = FunName.ToUpper() == "ONDATASETFIRST";
							if (flag5)
							{
								this.ReportOnDataSetFirst(Param1, Param2, ref Value1, ref Value2);
							}
							else
							{
								bool flag6 = FunName.ToUpper() == "ONDATASETEOF";
								if (flag6)
								{
									this.ReportOnDataSetEOF(Param1, Param2, ref Value1, ref Value2);
								}
								else
								{
									bool flag7 = FunName.ToUpper() == "ONDOCUNKNOWNFUNCTION";
									if (flag7)
									{
										this.ReportOnDocUnknownFunction(Param1, Param2, ref Value1, ref Value2);
									}
									else
									{
										bool flag8 = FunName.ToUpper() == "ONDOCUNKNOWNVARIABLE";
										if (flag8)
										{
											this.ReportOnDocUnknownVariable(Param1, Param2, ref Value1, ref Value2);
										}
										else
										{
											bool flag9 = FunName.ToUpper() == "ONGETDATASET";
											if (flag9)
											{
												this.ReportOnGetDataSet(Param1, Param2, ref Value1, ref Value2);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void ReportOnDataSetRead(string DataSetname, string FieldName, ref string ValueType, ref string Value)
		{
			try
			{
				ValueType = "S";
				Value = "NULL";
				bool bFindDataSet = false;
				bool bFindColumns = false;
				for (int i = 0; i < this.OpenReportTable.Count; i++)
				{
					bool flag = DataSetname.ToUpper() == ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).sDataSetName.ToUpper();
					if (flag)
					{
						bFindDataSet = true;
						bool flag2 = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Columns.Contains(FieldName);
						if (flag2)
						{
							int iRow = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iDQJL;
							bFindColumns = true;
							bool flag3 = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Columns[FieldName].DataType.Name == "Double" || ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Columns[FieldName].DataType.Name == "Decimal";
							if (flag3)
							{
								ValueType = "N";
								string sNubmer = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Rows[iRow][FieldName].ToString();
								bool flag4 = sNubmer.Trim().Equals("");
								if (flag4)
								{
									Value = "0";
								}
								else
								{
									Value = sNubmer;
								}
							}
							else
							{
								bool flag5 = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Columns[FieldName].DataType.Name == "Byte[]";
								if (flag5)
								{
									MemoryStream oMy = new MemoryStream((byte[])((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Rows[iRow][FieldName]);
									ValueType = "S";
									Value = "";
								}
								else
								{
									bool flag6 = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Columns[FieldName].DataType.Name == "DateTime";
									if (flag6)
									{
										ValueType = "S";
										bool flag7 = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Rows[iRow][FieldName].ToString().Trim().Equals("");
										if (flag7)
										{
											Value = "";
										}
										else
										{
											DateTime dtTmp = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Rows[iRow][FieldName].ToString().Trim().ToDateTime();
											bool flag8 = dtTmp.Hour == 0 && dtTmp.Minute == 0 && dtTmp.Second == 0;
											if (flag8)
											{
												Value = dtTmp.ToString("yyyy-MM-dd");
											}
											else
											{
												Value = dtTmp.ToString("yyyy-MM-dd HH:mm:ss");
											}
										}
									}
									else
									{
										ValueType = "S";
										Value = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Rows[iRow][FieldName].ToString();
									}
								}
							}
						}
					}
				}
				bool flag9 = !bFindDataSet;
				if (flag9)
				{
					ValueType = "S";
					Value = "";
				}
				bool flag10 = !bFindColumns;
				if (flag10)
				{
					ValueType = "S";
					Value = "";
				}
			}
			catch
			{
				ValueType = "S";
				Value = "";
			}
			Value = Value.TrimEnd(new char[0]);
		}

		private void ReportOnUnknownFunction(string Name, string Params, ref string ValueType, ref string Value)
		{
			this.OnReportUnknownFunction(Name, Params, ref ValueType, ref Value);
		}

		private void ReportOnUnknownVariable(string sSQL, string Name, ref string ValueType, ref string Value)
		{
			ValueType = "S";
			Value = string.Empty;
			this.OnReportUnknownVariable(Name, ref ValueType, ref Value);
			ReportStyleParse.StrMap sParVal = new ReportStyleParse.StrMap();
			bool flag = this._AllVariableValue.TryGetValue(Name, out sParVal);
			if (flag)
			{
				ValueType = sParVal.sType;
				Value = sParVal.sValue;
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(sSQL);
				if (flag2)
				{
					Database db = LogicContext.GetDatabase();
					HSQL sqlVar = new HSQL(db);
					sqlVar.Clear();
					sSQL = this.GetVarSql(sSQL);
					sqlVar.Add(sSQL);
					bool flag3 = sqlVar.ParamList.Count > 0;
					if (flag3)
					{
						for (int p = 0; p < sqlVar.ParamList.Count; p++)
						{
							string sParamName = sqlVar.ParamList[p].ToString();
							bool flag4 = sParamName.IndexOf("__") >= 0;
							if (flag4)
							{
								string sDataSet = sParamName.Substring(0, sParamName.IndexOf("__"));
								string sField = sParamName.Substring(sParamName.IndexOf("__") + 2);
								for (int i = 0; i < this.OpenReportTable.Count; i++)
								{
									bool flag5 = sDataSet.ToUpper() == ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).sDataSetName.ToUpper();
									if (flag5)
									{
										int iRow = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iDQJL;
										sqlVar.ParamByName(sParamName).Value = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Rows[iRow][sField].ToString();
									}
								}
							}
							else
							{
								string sField = sParamName;
								bool flag6 = this._AllVariableValue.TryGetValue(sField, out sParVal);
								if (flag6)
								{
									sqlVar.ParamByName(sParamName).Value = sParVal.sValue;
								}
								else
								{
									sqlVar.ParamByName(sParamName).Value = "";
								}
							}
						}
					}
					DataSet dsVar = db.OpenDataSet(sqlVar);
					bool flag7 = dsVar.Tables[0].Rows.Count > 0;
					if (flag7)
					{
						bool flag8 = dsVar.Tables[0].Columns[0].DataType.Name == "Double" || dsVar.Tables[0].Columns[0].DataType.Name == "Decimal";
						if (flag8)
						{
							ValueType = "N";
						}
						else
						{
							ValueType = "S";
						}
						Value = dsVar.Tables[0].Rows[0][0].ToString().Trim();
					}
				}
			}
		}

		public string GetVarSql(string sSQL)
		{
			string sSubSql = sSQL;
			bool flag = sSubSql.IndexOf(":") != 0;
			if (flag)
			{
				int iInd = sSubSql.IndexOf(":");
				string sStaSQL = sSubSql.Substring(0, iInd + 1);
				string sEndSQL = sSubSql.Substring(iInd + 1);
				sEndSQL = sEndSQL.Replace(".", "__");
				sSubSql = sStaSQL + sEndSQL;
			}
			return sSubSql;
		}

		private void ReportOnDataSetNext(string DataSetName, string Param2, ref string Value1, ref string Value2)
		{
			bool bDataSetNameEOF = false;
			for (int i = 0; i < this.OpenReportTable.Count; i++)
			{
				bool flag = DataSetName.ToUpper() == ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).sDataSetName.ToUpper();
				if (flag)
				{
					((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iDQJL = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iDQJL + 1;
					bool flag2 = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iDQJL >= ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iRecordCount;
					if (flag2)
					{
						bDataSetNameEOF = true;
					}
				}
			}
			bool flag3 = !bDataSetNameEOF;
			if (flag3)
			{
				for (int j = 0; j < this.OpenSubReportTable.Count; j++)
				{
					bool flag4 = DataSetName.ToUpper() == ((ReportStyleParse.OpenSubReport_Table)this.OpenSubReportTable[j]).sFatherDataSetName.ToUpper();
					if (flag4)
					{
						this.UpdateCustomSubDataSet(((ReportStyleParse.OpenSubReport_Table)this.OpenSubReportTable[j]).sDataSetName);
					}
				}
			}
		}

		private void ReportOnDataSetFirst(string DataSetName, string Param2, ref string Value1, ref string Value2)
		{
			for (int i = 0; i < this.OpenReportTable.Count; i++)
			{
				bool flag = DataSetName.ToUpper() == ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).sDataSetName.ToUpper();
				if (flag)
				{
					((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iDQJL = 0;
				}
			}
		}

		private void ReportOnDataSetEOF(string DataSetName, string Param2, ref string sEOF, ref string Value2)
		{
			sEOF = "N";
			bool bFind = false;
			for (int i = 0; i < this.OpenReportTable.Count; i++)
			{
				bool flag = DataSetName.ToUpper() == ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).sDataSetName.ToUpper();
				if (flag)
				{
					bool flag2 = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iRecordCount == 0;
					if (flag2)
					{
						sEOF = "Y";
					}
					else
					{
						bool flag3 = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iRecordCount == ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iDQJL;
						if (flag3)
						{
							sEOF = "Y";
						}
					}
					bFind = true;
				}
			}
			bool flag4 = !bFind;
			if (flag4)
			{
				sEOF = "Y";
			}
		}

		private string FormatDateToStr(string sRQ)
		{
			DateTime DT = default(DateTime);
			string result;
			try
			{
				DT = Convert.ToDateTime(sRQ);
			}
			catch
			{
				result = " ";
				return result;
			}
			result = string.Format("{0:yyyy-MM-dd}", DT);
			return result;
		}

		private string FormatMinuteToHHMM(string sMinute)
		{
			int iMM = 0;
			string result;
			try
			{
				iMM = Convert.ToInt32(sMinute);
			}
			catch
			{
				result = " ";
				return result;
			}
			int iRHH = iMM / 60;
			int iRMM = iMM % 60;
			string sHHMM = Convert.ToString(iRHH) + "小时" + Convert.ToString(iRMM) + "分钟";
			result = sHHMM;
			return result;
		}

		private string SplitName(string sParams)
		{
			string[] sParam = sParams.Trim().Split(new char[]
			{
				','
			});
			bool flag = sParam.Length != 0;
			string result;
			if (flag)
			{
				string sSplitString = sParam[0].Trim();
				string[] arryTMP = sSplitString.Split(new char[]
				{
					'|'
				});
				int Index = Convert.ToInt32(sParam[1].Trim());
				bool flag2 = arryTMP.Length != 0;
				if (flag2)
				{
					bool flag3 = Index <= arryTMP.Length - 1;
					if (flag3)
					{
						result = arryTMP[Index];
					}
					else
					{
						result = arryTMP[0];
					}
				}
				else
				{
					result = sSplitString;
				}
			}
			else
			{
				result = " ";
			}
			return result;
		}

		private void ReportOnGetDataSet(string DataSetName, string sSQL, ref string Value1, ref string Value2)
		{
			try
			{
				string FatherDataSetName = "";
				Database db = LogicContext.GetDatabase();
				HSQL sqlVar = new HSQL(db);
				sqlVar.Clear();
				sSQL = this.GetVarSql(sSQL);
				sqlVar.Add(sSQL);
				bool flag = sqlVar.ParamList.Count > 0;
				if (flag)
				{
					for (int p = 0; p < sqlVar.ParamList.Count; p++)
					{
						string sParamName = sqlVar.ParamList[p].ToString();
						string sDataSet = "";
						string sField = "";
						bool flag2 = sParamName.IndexOf("__") >= 0;
						if (flag2)
						{
							sDataSet = sParamName.Substring(0, sParamName.IndexOf("__"));
							sField = sParamName.Substring(sParamName.IndexOf("__") + 2);
						}
						ReportStyleParse.StrMap sParVal = new ReportStyleParse.StrMap();
						bool flag3 = this._AllVariableValue.TryGetValue(sParamName, out sParVal);
						if (flag3)
						{
							sqlVar.ParamByName(sParamName).Value = sParVal.sValue;
						}
						else
						{
							bool flag4 = sDataSet != null;
							if (flag4)
							{
								FatherDataSetName = sDataSet;
								for (int i = 0; i < this.OpenReportTable.Count; i++)
								{
									bool flag5 = sDataSet.ToUpper() == ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).sDataSetName.ToUpper();
									if (flag5)
									{
										int iRow = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iDQJL;
										bool flag6 = iRow < ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).iRecordCount;
										if (flag6)
										{
											sqlVar.ParamByName(sParamName).Value = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[i]).dsDataSet.Tables[0].Rows[iRow][sField].ToString();
										}
										else
										{
											sqlVar.ParamByName(sParamName).Value = "0";
										}
									}
								}
							}
						}
					}
				}
				DataSet dsVar = db.OpenDataSet(sqlVar);
				ReportStyleParse.OpenReport_Table dsOpen = new ReportStyleParse.OpenReport_Table();
				dsOpen.sDataSetName = DataSetName;
				dsOpen.dsDataSet = dsVar;
				dsOpen.iRecordCount = dsVar.Tables[0].Rows.Count;
				dsOpen.iDQJL = 0;
				this.OpenReportTable.Add(dsOpen);
				bool flag7 = FatherDataSetName != null;
				if (flag7)
				{
					ReportStyleParse.OpenSubReport_Table dsSubOpen = new ReportStyleParse.OpenSubReport_Table();
					dsSubOpen.sDataSetName = DataSetName;
					dsSubOpen.sFatherDataSetName = FatherDataSetName;
					dsSubOpen.sSQL = sSQL;
					this.OpenSubReportTable.Add(dsSubOpen);
				}
			}
			catch
			{
			}
		}

		private void UpdateCustomSubDataSet(string sDataSetName)
		{
			Database db = LogicContext.GetDatabase();
			for (int i = 0; i < this.OpenSubReportTable.Count; i++)
			{
				bool flag = sDataSetName.ToUpper() == ((ReportStyleParse.OpenSubReport_Table)this.OpenSubReportTable[i]).sDataSetName.ToUpper();
				if (flag)
				{
					HSQL sqlVar = new HSQL(db);
					sqlVar.Clear();
					sqlVar.Add(((ReportStyleParse.OpenSubReport_Table)this.OpenSubReportTable[i]).sSQL);
					bool flag2 = sqlVar.ParamList.Count > 0;
					if (flag2)
					{
						for (int p = 0; p < sqlVar.ParamList.Count; p++)
						{
							string sParamName = sqlVar.ParamList[p].ToString();
							string sDataSet = "";
							string sField = "";
							bool flag3 = sParamName.IndexOf("__") >= 0;
							if (flag3)
							{
								sDataSet = sParamName.Substring(0, sParamName.IndexOf("__"));
								sField = sParamName.Substring(sParamName.IndexOf("__") + 2);
							}
							for (int q = 0; q < this.OpenReportTable.Count; q++)
							{
								bool flag4 = sDataSet.ToUpper() == ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[q]).sDataSetName.ToUpper();
								if (flag4)
								{
									int iRow = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[q]).iDQJL;
									sqlVar.ParamByName(sParamName).Value = ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[q]).dsDataSet.Tables[0].Rows[iRow][sField].ToString();
								}
							}
						}
					}
					DataSet dsVar = db.OpenDataSet(sqlVar);
					for (int j = 0; j < this.OpenReportTable.Count; j++)
					{
						bool flag5 = sDataSetName.ToUpper() == ((ReportStyleParse.OpenReport_Table)this.OpenReportTable[j]).sDataSetName.ToUpper();
						if (flag5)
						{
							((ReportStyleParse.OpenReport_Table)this.OpenReportTable[j]).dsDataSet = dsVar;
							((ReportStyleParse.OpenReport_Table)this.OpenReportTable[j]).iRecordCount = dsVar.Tables[0].Rows.Count;
							((ReportStyleParse.OpenReport_Table)this.OpenReportTable[j]).iDQJL = 0;
						}
					}
				}
			}
		}

		private void ReportOnDocUnknownFunction(string Name, string Params, ref string ValueType, ref string Value)
		{
			this.OnReportDocUnknownFunction(Name, Params, ref ValueType, ref Value);
		}

		private void ReportOnDocUnknownVariable(string Param1, string Name, ref string ValueType, ref string Value)
		{
			this.OnReportDocUnknownVariable(Name, ref ValueType, ref Value);
		}

		private void GetReportStyle(string styleId, string styleOrder)
		{
			Database db = LogicContext.GetDatabase();
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add("SELECT REPORTSTYLE_STYLE FROM REPORTSTYLE");
				hsql.Add("WHERE REPORTSTYLE_STYLEID = :REPORTSTYLE_STYLEID");
				hsql.Add("AND REPORTSTYLE_ORDER = :REPORTSTYLE_ORDER");
				hsql.ParamByName("REPORTSTYLE_STYLEID").Value = styleId;
				hsql.ParamByName("REPORTSTYLE_ORDER").Value = styleOrder;
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["REPORTSTYLE_STYLE"].ToString() != "";
				if (flag)
				{
					byte[] bybuf = new byte[0];
					bybuf = (byte[])ds.Tables[0].Rows[0]["REPORTSTYLE_STYLE"];
					this._ReportXMLDataSets = DelphiTempletDesUtils.GetDeBase64ZipEncry(Encoding.Default.GetString(bybuf, 0, bybuf.Length));
					this._ChooseTemplate = true;
					return;
				}
				this._ReportXMLDataSets = "";
			}
			catch
			{
				this._ReportXMLDataSets = "";
			}
			this.GetReportDataSet();
		}
	}
}
