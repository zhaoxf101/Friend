using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	public class RmReportingMaker
	{
		internal delegate object TOnUnknownFunction(string functionName, ArrayList paramList);

		internal delegate object TOnUnknownVariable(string variable);

		public delegate void GtReportPubGetValue(string funName, string paramOne, string paramTwo, ref string valueOne, ref string valueTwo);

		private RmRow curRow = null;

		private RmRows curRows = null;

		private List<List<RdRow>> rowExpanded = new List<List<RdRow>>();

		private List<RdCell> waitToProcess = new List<RdCell>();

		private List<RdCell> computeOrder = new List<RdCell>();

		private bool m_firstGrpHead = false;

		private RdCell grpHeadCell = null;

		private RmReportingMakerScriptOutput grpFootNode;

		private bool isExistsGrpFooter = false;

		private Dictionary<int, string> m_grpField = new Dictionary<int, string>();

		private List<object> m_grpHeadValue = new List<object>();

		private List<object> m_grpFootValue = null;

		private bool m_grpValFount = false;

		private string m_userName = string.Empty;

		private string m_reportName = string.Empty;

		private RdCell m_curComputeCell = null;

		private RdDocument m_template = new RdDocument();

		private RdDocument m_outputReport = new RdDocument();

		private Expressions m_reportExpression = new Expressions();

		private RmDataSets m_dataSets = null;

		private RmImages m_images = null;

		private RmGroups m_groups = null;

		private int m_outputRowCount = 0;

		private int m_outputRowNo = 0;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal event RmReportingMaker.TOnUnknownFunction FOnUnknownFunction;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal event RmReportingMaker.TOnUnknownVariable FOnUnknownVariable;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event RmReportingMaker.GtReportPubGetValue OnGtReportPubGetValue;

		internal bool FirstGrpHead
		{
			get
			{
				return this.m_firstGrpHead;
			}
			set
			{
				this.m_firstGrpHead = value;
			}
		}

		internal Dictionary<int, string> GrpField
		{
			get
			{
				return this.m_grpField;
			}
			set
			{
				this.m_grpField = value;
			}
		}

		internal List<object> GrpHeadValue
		{
			get
			{
				return this.m_grpHeadValue;
			}
			set
			{
				this.m_grpHeadValue = value;
			}
		}

		internal List<object> GrpFootValue
		{
			get
			{
				return this.m_grpFootValue;
			}
			set
			{
				this.m_grpFootValue = value;
			}
		}

		internal bool GrpValFount
		{
			get
			{
				return this.m_grpValFount;
			}
			set
			{
				this.m_grpValFount = value;
			}
		}

		internal string UserName
		{
			get
			{
				return this.m_userName;
			}
			set
			{
				this.m_userName = value;
			}
		}

		internal string ReportName
		{
			get
			{
				return this.m_reportName;
			}
			set
			{
				this.m_reportName = value;
			}
		}

		internal RdCell CurComputeCell
		{
			get
			{
				return this.m_curComputeCell;
			}
			set
			{
				this.m_curComputeCell = value;
			}
		}

		internal RdDocument Template
		{
			get
			{
				return this.m_template;
			}
			set
			{
				this.m_template = value;
			}
		}

		internal RdDocument OutputReport
		{
			get
			{
				return this.m_outputReport;
			}
			set
			{
				this.m_outputReport = value;
			}
		}

		internal Expressions ReportExpression
		{
			get
			{
				return this.m_reportExpression;
			}
			set
			{
				this.m_reportExpression = value;
			}
		}

		internal RmDataSets DataSets
		{
			get
			{
				return this.m_dataSets;
			}
			set
			{
				this.m_dataSets = value;
			}
		}

		internal RmImages Images
		{
			get
			{
				return this.m_images;
			}
			set
			{
				this.m_images = value;
			}
		}

		internal RmGroups Groups
		{
			get
			{
				return this.m_groups;
			}
			set
			{
				this.m_groups = value;
			}
		}

		internal int OutputRowCount
		{
			get
			{
				return this.m_outputRowCount;
			}
			set
			{
				this.m_outputRowCount = value;
			}
		}

		internal int OutputRowNo
		{
			get
			{
				return this.m_outputRowNo;
			}
			set
			{
				this.m_outputRowNo = value;
			}
		}

		internal void OnGetDataSet(string datasetName, string sql)
		{
			string valueOne = string.Empty;
			string valueTwo = string.Empty;
			this.OnGtReportPubGetValue("OnGetDataSet", datasetName, sql, ref valueOne, ref valueTwo);
		}

		internal void OnDataSetFirst(string datasetName, string sql)
		{
			string valueOne = string.Empty;
			string valueTwo = string.Empty;
			this.OnGtReportPubGetValue("OnDataSetFirst", datasetName, sql, ref valueOne, ref valueTwo);
		}

		internal void OnDataSetNext(string datasetName, string sql)
		{
			string valueOne = string.Empty;
			string valueTwo = string.Empty;
			this.OnGtReportPubGetValue("OnDataSetNext", datasetName, sql, ref valueOne, ref valueTwo);
		}

		internal void OnDataSetEOF(string datasetName, ref string eof)
		{
			string valueOne = string.Empty;
			string valueTwo = string.Empty;
			this.OnGtReportPubGetValue("OnDataSetEOF", datasetName, "", ref eof, ref valueTwo);
		}

		internal void OnDataSetRead(string datasetName, string fieldName, ref string valueType, ref string value)
		{
			string valueOne = string.Empty;
			string valueTwo = string.Empty;
			this.OnGtReportPubGetValue("OnDataSetRead", datasetName, fieldName, ref valueType, ref value);
		}

		internal void OnUnknownVariable(string sql, string variable, ref string valueType, ref string value)
		{
			string valueOne = string.Empty;
			string valueTwo = string.Empty;
			this.OnGtReportPubGetValue("OnUnknownVariable", sql, variable, ref valueType, ref value);
		}

		internal void OnUnknownFunction(string funcName, string paramStr, ref string valueType, ref string value)
		{
			string valueOne = string.Empty;
			string valueTwo = string.Empty;
			this.OnGtReportPubGetValue("OnUnknownFunction", funcName, paramStr, ref valueType, ref value);
		}

		public RmReportingMaker()
		{
			this.m_dataSets = new RmDataSets(this);
			this.m_images = new RmImages(this);
			this.m_groups = new RmGroups(this);
			this.ReportExpression.OnUnknownVariable += new Expressions.TOnUnknownVariable(this.ReportExpression_OnUnknownVariable);
			this.ReportExpression.OnUnknownFunction += new Expressions.TOnUnknownFunction(this.ReportExpression_OnUnknownFunction);
		}

		private object ReportExpression_OnUnknownFunction(string functionName, ArrayList paramList)
		{
			bool flag = string.IsNullOrEmpty(functionName);
			object result2;
			if (flag)
			{
				result2 = "";
			}
			else
			{
				bool flag2 = functionName.ToUpper() == "READIMAGEFROMFILE" || functionName.ToUpper() == "READIMAGEFROMDB";
				object result;
				if (flag2)
				{
					bool flag3 = paramList.Count > 0;
					if (flag3)
					{
						try
						{
							RdImage img = this.m_images.NewImage(this.Template, this.CurComputeCell, paramList);
							bool flag4 = img != null;
							if (flag4)
							{
								this.Template.Images.List.Add(img.Name, img);
							}
						}
						catch
						{
						}
					}
					result = "";
				}
				else
				{
					bool flag5 = functionName.ToUpper() == "GENCHART";
					if (flag5)
					{
						RdImage img2 = this.m_images.GenChartImage(this.Template, this.CurComputeCell, paramList);
						bool flag6 = img2 != null;
						if (flag6)
						{
							this.Template.Images.List.Add(img2.Name, img2);
						}
						result = "";
					}
					else
					{
						string paramStr = "";
						string valueType = "S";
						string value = "";
						for (int i = 0; i < paramList.Count; i++)
						{
							paramStr = paramStr + paramList[i].ToString() + ",";
						}
						paramStr = paramStr.TrimEnd(new char[]
						{
							','
						});
						try
						{
							this.OnUnknownFunction(functionName, paramStr, ref valueType, ref value);
						}
						catch
						{
							throw new ReportingException(string.Format("未知函数：{0}", functionName));
						}
						bool flag7 = valueType == "N";
						if (flag7)
						{
							result = Utils.Str2Double(value, 0.0);
						}
						else
						{
							result = value;
						}
					}
				}
				result2 = result;
			}
			return result2;
		}

		internal object GetCellValue(string cellId)
		{
			int R = -1;
			int C = -1;
			Utils.DecodeCellId(cellId, ref R, ref C);
			RdCell tmpCell = this.OutputReport.GetCell(R, C);
			bool flag = tmpCell == null;
			object result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				RdDataType autoDataType = tmpCell.AutoDataType;
				if (autoDataType != RdDataType.dtString)
				{
					if (autoDataType != RdDataType.dtNumber)
					{
						result = tmpCell.GetAsString();
					}
					else
					{
						result = tmpCell.GetAsNumber();
					}
				}
				else
				{
					result = tmpCell.GetAsString();
				}
			}
			return result;
		}

		internal object FindVars(string name, ref bool foundIt)
		{
			object result = "";
			RmVars CurVars = (RmVars)this.CurComputeCell.Builder;
			foreach (KeyValuePair<string, bool> item in CurVars.Vars)
			{
				bool flag = name == item.Key;
				if (flag)
				{
					bool flag2 = CurVars.VarIsCell(name);
					if (flag2)
					{
						string S = CurVars.VarValue[name].ToString();
						S = S.Trim(new char[]
						{
							','
						});
						string[] listVar = S.Split(new char[]
						{
							','
						});
						bool flag3 = listVar.Length == 0;
						if (flag3)
						{
							result = 0.0;
						}
						else
						{
							bool flag4 = listVar.Length == 1;
							if (flag4)
							{
								result = this.GetCellValue(listVar[0]);
							}
							else
							{
								ArrayList listVarValue = new ArrayList();
								for (int i = 0; i < listVar.Length; i++)
								{
									listVarValue.Add(this.GetCellValue(listVar[i]));
								}
								result = this.ReportExpression.DirectCompute("SUM", listVarValue);
							}
						}
					}
					else
					{
						result = CurVars.VarValue[name];
					}
				}
				foundIt = true;
			}
			return result;
		}

		private object ReportExpression_OnUnknownVariable(string variable)
		{
			bool FoundIt = false;
			object ret = null;
			this.GrpValFount = true;
			bool flag = this.CurComputeCell != null && this.CurComputeCell.Builder != null;
			if (flag)
			{
				ret = this.FindVars(variable, ref FoundIt);
			}
			bool flag2 = FoundIt;
			object result;
			if (flag2)
			{
				result = ret;
			}
			else
			{
				bool flag3 = this.CurComputeCell != null && this.OutputReport.Rows[this.CurComputeCell.Row].RowType == RdRowType.rtGroupHeader;
				if (flag3)
				{
					bool flag4 = this.GrpField != null && this.GrpField.Count == this.GrpHeadValue.Count;
					if (flag4)
					{
						for (int i = 0; i < this.GrpField.Count; i++)
						{
							bool flag5 = variable.IndexOf("." + this.GrpField[i]) >= 0;
							if (flag5)
							{
								ret = this.GrpHeadValue[i];
							}
						}
					}
					else
					{
						this.GrpValFount = false;
						ret = "=" + variable;
					}
				}
				else
				{
					bool flag6 = this.CurComputeCell != null && this.OutputReport.Rows[this.CurComputeCell.Row].RowType == RdRowType.rtGroupFooter;
					if (flag6)
					{
						bool flag7 = this.GrpField != null && this.GrpField.Count == this.GrpFootValue.Count;
						if (flag7)
						{
							for (int j = 0; j < this.GrpField.Count; j++)
							{
								bool flag8 = variable.IndexOf("." + this.GrpField[j]) >= 0;
								if (flag8)
								{
									ret = this.GrpFootValue[j];
								}
							}
						}
						else
						{
							this.GrpValFount = false;
							ret = "=" + variable;
						}
					}
					else
					{
						bool flag9 = variable.IndexOf(".") >= 0;
						if (flag9)
						{
							ret = this.GetFieldValue(variable);
						}
						else
						{
							string valueType = "S";
							string value = string.Empty;
							RdVariable tmpVariable = this.Template.Variables.FindVariable(variable);
							string sql = (tmpVariable == null) ? "" : tmpVariable.Formula;
							try
							{
								this.OnUnknownVariable(sql, variable, ref valueType, ref value);
							}
							catch
							{
								throw new ReportingException(string.Format("未知变量：{0}", variable));
							}
							bool flag10 = valueType == "N";
							if (flag10)
							{
								ret = Utils.Str2Double(value, 0.0);
							}
							else
							{
								ret = value;
							}
						}
					}
				}
				result = ret;
			}
			return result;
		}

		private object GetFieldValue(string variable)
		{
			string datasetName = string.Empty;
			string fieldName = string.Empty;
			datasetName = variable.Split(new char[]
			{
				'.'
			})[0];
			fieldName = variable.Split(new char[]
			{
				'.'
			})[1];
			return this.DataSets.FindDataset(datasetName).Read(fieldName);
		}

		public void SetTemplate(string value)
		{
			this.Template.SetReport(value);
		}

		public static string GetDefaultTemplate(string value)
		{
			RdDocument doc = new RdDocument();
			doc.SetReport("");
			doc.Rows.RowCount = 10;
			int columnCount = 1;
			doc.Columns.ColumnCount = columnCount;
			bool flag = string.IsNullOrEmpty(value);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				XmlDocument document = new XmlDocument();
				document.LoadXml(value);
				XmlElement rootNode = document.DocumentElement;
				string title = Utils.GetAttrString(rootNode, "Title", "新报表");
				XmlNode datasetNode = rootNode.ChildNodes[0];
				XmlNode varNode = rootNode.ChildNodes[1];
				bool flag2 = varNode.Name == "VARPARAM";
				if (flag2)
				{
					bool hasChildNodes = varNode.HasChildNodes;
					if (hasChildNodes)
					{
						for (XmlNode variableNode = varNode.FirstChild; variableNode != null; variableNode = variableNode.NextSibling)
						{
							RdVariable variable = new RdVariable(doc);
							variable.Name = variableNode.Name;
							variable.Type = ((variableNode.Attributes["TYPE"].Value == "N") ? RdFieldType.gfNumeric : RdFieldType.gfString);
							variable.Comment = variableNode.Attributes["NAME"].Value;
							doc.Variables.List.Add(variable.Name, variable);
						}
					}
				}
				bool hasChildNodes2 = datasetNode.HasChildNodes;
				if (hasChildNodes2)
				{
					RdDataSet dataset = new RdDataSet(doc);
					dataset.Name = datasetNode.Name;
					doc.DataSets.List.Add(dataset.Name, dataset);
					XmlNode fieldNode = datasetNode.FirstChild;
					while (fieldNode != null)
					{
						dataset.AddField(string.Concat(new string[]
						{
							fieldNode.Name,
							"|",
							fieldNode.Attributes["NAME"].Value,
							"|",
							(fieldNode.Attributes["TYPE"].Value == "S") ? "0" : "1"
						}));
						doc.Columns.ColumnCount = columnCount;
						doc.GetCell(2, columnCount).SetAsString(fieldNode.Attributes["NAME"].Value);
						doc.GetCell(2, columnCount).SetHAlignment(RdHAlignment.haCenter);
						doc.GetCell(2, columnCount).SetFontBold(true);
						doc.GetCell(2, columnCount).SetLeftBorderStyle(RdLineStyle.lsThinSolid);
						doc.GetCell(2, columnCount).SetRightBorderStyle(RdLineStyle.lsThinSolid);
						doc.GetCell(2, columnCount).SetTopBorderStyle(RdLineStyle.lsThinSolid);
						doc.GetCell(2, columnCount).SetBottomBorderStyle(RdLineStyle.lsThinSolid);
						doc.GetCell(3, columnCount).SetAsString("=" + dataset.Name + "." + fieldNode.Name);
						doc.GetCell(3, columnCount).SetFontBold(false);
						doc.GetCell(3, columnCount).SetLeftBorderStyle(RdLineStyle.lsThinSolid);
						doc.GetCell(3, columnCount).SetRightBorderStyle(RdLineStyle.lsThinSolid);
						doc.GetCell(3, columnCount).SetTopBorderStyle(RdLineStyle.lsThinSolid);
						doc.GetCell(3, columnCount).SetBottomBorderStyle(RdLineStyle.lsThinSolid);
						fieldNode = fieldNode.NextSibling;
						columnCount++;
					}
					doc.Rows[3].DataSet = dataset.Name;
				}
				doc.Join(1, 1, columnCount - 1, 1);
				doc.GetCell(1, 1).SetAsString(title);
				doc.GetCell(1, 1).SetHAlignment(RdHAlignment.haCenter);
				doc.GetCell(1, 1).SetFontBold(true);
				doc.GetCell(1, 1).SetFontSize(24);
				result = doc.GetReport();
			}
			return result;
		}

		internal void OnOutputRow(RdRow templateRow, RdRow row)
		{
		}

		internal void OnOutputCell(RdCell templateCell, RdCell cell)
		{
		}

		internal void OnComputeCell(RdCell cell)
		{
		}

		public string Generate()
		{
			List<RmReportingMakerScriptOutput> outputScript = new List<RmReportingMakerScriptOutput>();
			this.m_outputRowCount = 0;
			this.m_outputRowNo = 0;
			this.m_grpHeadValue = new List<object>();
			this.m_grpFootValue = new List<object>();
			this.m_grpValFount = true;
			this.m_images.Clear();
			this.m_dataSets.Refresh();
			this.m_groups.Refresh();
			this.InitCellState();
			this.isExistsGrpFooter = false;
			outputScript = this.GenOutputScript();
			RmRow rowList = new RmRow(null);
			this.InitRowExpanded(this.rowExpanded);
			this.curRow = rowList;
			this.curRows = null;
			this.m_outputReport = new RdDocument();
			this.m_outputReport.UserName = this.UserName;
			this.m_outputReport.Name = this.ReportName;
			this.m_outputReport.Template = this.Template.Name;
			this.Template.CopyReport(ref this.m_outputReport);
			RdCell zeroCell = this.OutputReport.GetCell(0, 0);
			this.Template.GetCell(0, 0).CopyNode(ref zeroCell);
			this.ProcessReportRemarks();
			this.SetColumns();
			this.m_outputRowNo = 0;
			this.m_outputRowCount = 0;
			this.OutputRows(outputScript);
			this.computeOrder = this.SortWaitToProcess();
			this.ComputeCells();
			this.OutputImages();
			this.OutputReport.Rows.RowCount = this.OutputRowNo;
			return this.OutputReport.GetReport();
		}

		internal void OutputImages()
		{
			this.OutputReport.Images = this.Template.Images;
		}

		internal void InitCellState()
		{
			int R = -1;
			int C = -1;
			int R2 = -1;
			int C2 = -1;
			int R3 = -1;
			int C3 = -1;
			RdCell curCell = null;
			Dictionary<string, bool> vars = null;
			bool hasCell = false;
			this.m_curComputeCell = null;
			for (R = 1; R <= this.Template.Rows.RowCount; R++)
			{
				for (C = 1; C <= this.Template.Columns.ColumnCount; C++)
				{
					curCell = this.Template.GetCell(R, C);
					curCell.Pointer = null;
					bool flag = curCell.AutoDataType == RdDataType.dtFormula;
					if (flag)
					{
						vars = new Dictionary<string, bool>();
						hasCell = false;
						this.ReportExpression.Expression = curCell.GetAsFormula();
						foreach (string curVar in this.ReportExpression.ExpVariables)
						{
							bool flag2 = !this.Template.Variables.List.ContainsKey(curVar);
							if (flag2)
							{
								bool flag3 = this.Template.AliasList.List.ContainsKey(curVar) || (curVar.IndexOf(':') >= 0 && Utils.DecodeRangeId(curVar, ref C2, ref R2, ref C3, ref R3)) || Utils.DecodeCellId(curVar, ref R2, ref C2);
								if (flag3)
								{
									hasCell = true;
									vars.Add(curVar, true);
								}
								else
								{
									vars.Add(curVar, false);
								}
							}
						}
						bool flag4 = hasCell;
						if (flag4)
						{
							RmVars _tmpVars = new RmVars(curCell);
							curCell.Builder = _tmpVars;
							_tmpVars.Vars = vars;
						}
					}
				}
			}
		}

		internal List<RdRow> SortRows()
		{
			List<RdRow> result = new List<RdRow>();
			for (int R = 1; R <= this.Template.Rows.RowCount; R++)
			{
				RdRow row = this.Template.Rows[R];
				bool flag = row.RowType == RdRowType.rtDetailData || ((row.RowType == RdRowType.rtColumnHeader || row.RowType == RdRowType.rtColumnFooter || row.RowType == RdRowType.rtGroupHeader || row.RowType == RdRowType.rtGroupFooter) && string.IsNullOrEmpty(row.DataSet));
				if (flag)
				{
					bool flag2 = row.Data == null;
					if (flag2)
					{
						result.Add(row);
					}
					else
					{
						RmDataSet rootDataset = ((RmDataSet)row.Data).Root;
						int first = 0;
						while (first < result.Count && (result[first].Data == null || rootDataset != ((RmDataSet)result[first].Data).Root))
						{
							first++;
						}
						bool flag3 = first >= result.Count;
						if (flag3)
						{
							result.Add(row);
						}
						else
						{
							int last = first;
							while (last < result.Count && (result[last].Data == null || rootDataset == ((RmDataSet)result[first].Data).Root))
							{
								last++;
							}
							last--;
							int i;
							for (i = last; i >= first; i--)
							{
								bool flag4 = ((RmDataSet)row.Data).IsMasterOf((RmDataSet)result[i].Data);
								if (flag4)
								{
									break;
								}
							}
							bool flag5 = i < first;
							if (flag5)
							{
								result.Insert(last + 1, row);
							}
							else
							{
								result.Insert(i + 1, row);
							}
						}
					}
				}
			}
			for (int R2 = 1; R2 <= this.Template.Rows.RowCount; R2++)
			{
				RdRow row = this.Template.Rows[R2];
				bool flag6 = row.RowType == RdRowType.rtReportHeader || row.RowType == RdRowType.rtPageHeader || row.RowType == RdRowType.rtReportFooter || row.RowType == RdRowType.rtPageFooter;
				if (flag6)
				{
					int resultCount = result.Count;
					for (int j = 0; j < resultCount; j++)
					{
						bool flag7 = result[j].RowType > row.RowType;
						if (flag7)
						{
							result.Insert(j, row);
							row = null;
							break;
						}
					}
					bool flag8 = row != null;
					if (flag8)
					{
						result.Add(row);
					}
				}
			}
			return result;
		}

		internal RmReportingMakerScriptOutput NewNode(RmConst action, object pointer, int next, ref List<RmReportingMakerScriptOutput> fScriptList)
		{
			RmReportingMakerScriptOutput result = new RmReportingMakerScriptOutput();
			result.Action = action;
			result.Pointer = pointer;
			result.Next = next;
			fScriptList.Add(result);
			return result;
		}

		internal void GenDataSetScript(RmDataSet dataset, int first, ref int last, List<RdRow> detailRows, ref List<RmReportingMakerScriptOutput> fScriptList)
		{
			int tempI = 0;
			last = first;
			while (last < detailRows.Count && dataset.IsMasterOf((RmDataSet)detailRows[last].Data))
			{
				last++;
			}
			bool flag = !dataset.FgtrDataSet.SingleRecord;
			if (flag)
			{
				this.NewNode(RmConst.CosFirst, dataset, -1, ref fScriptList);
			}
			this.NewNode(RmConst.CosRowsStart, dataset, -1, ref fScriptList);
			bool flag2 = dataset.ColumnHeader.Count > 0;
			if (flag2)
			{
				foreach (KeyValuePair<int, RdRow> obj in dataset.ColumnHeader)
				{
					this.NewNode(RmConst.CosOutput, obj.Value, -1, ref fScriptList);
				}
				this.NewNode(RmConst.CosColumnHeader, dataset, -1, ref fScriptList);
			}
			RmReportingMakerScriptOutput loopStart = this.NewNode(RmConst.CosEOF, dataset, 0, ref fScriptList);
			this.NewNode(RmConst.CosRowStart, dataset, -1, ref fScriptList);
			tempI = first;
			while (tempI < last)
			{
				RdRow row = detailRows[tempI];
				bool flag3 = row.Data == dataset;
				if (flag3)
				{
					this.NewNode(RmConst.CosOutput, row, -1, ref fScriptList);
					tempI++;
				}
				else
				{
					RmDataSet DDataset = (RmDataSet)row.Data;
					while (DDataset.Master != null && DDataset.Master != dataset)
					{
						DDataset = DDataset.Master;
					}
					bool flag4 = DDataset.Master == dataset;
					if (flag4)
					{
						this.GenDataSetScript(DDataset, tempI, ref tempI, detailRows, ref fScriptList);
					}
					else
					{
						tempI++;
					}
				}
			}
			this.NewNode(RmConst.CosRowEnd, dataset, -1, ref fScriptList);
			bool flag5 = !dataset.FgtrDataSet.SingleRecord;
			if (flag5)
			{
				this.NewNode(RmConst.CosNext, dataset, -1, ref fScriptList);
				RmReportingMakerScriptOutput loopEnd = this.NewNode(RmConst.CosJump, null, fScriptList.IndexOf(loopStart), ref fScriptList);
				loopStart.Next = fScriptList.IndexOf(loopEnd) + 1;
			}
			bool flag6 = dataset.ColumnFooter.Count > 0;
			if (flag6)
			{
				foreach (KeyValuePair<int, RdRow> obj2 in dataset.ColumnFooter)
				{
					this.NewNode(RmConst.CosOutput, obj2.Value, -1, ref fScriptList);
				}
				this.NewNode(RmConst.CosColumnFooter, dataset, -1, ref fScriptList);
			}
			this.NewNode(RmConst.CosRowsEnd, dataset, -1, ref fScriptList);
		}

		internal List<RmReportingMakerScriptOutput> GenOutputScript()
		{
			List<RmReportingMakerScriptOutput> FScriptList = new List<RmReportingMakerScriptOutput>();
			List<RdRow> detailRows = new List<RdRow>();
			detailRows = this.SortRows();
			int i = 0;
			while (i < detailRows.Count)
			{
				RdRow row = detailRows[i];
				bool flag = row.RowType != RdRowType.rtDetailData || row.Data == null;
				if (flag)
				{
					bool flag2 = row.RowType == RdRowType.rtGroupHeader;
					if (flag2)
					{
						RmReportingMakerScriptOutput script = this.NewNode(RmConst.CosGroupHeader, row, -1, ref FScriptList);
					}
					else
					{
						bool flag3 = row.RowType == RdRowType.rtGroupFooter;
						if (flag3)
						{
							this.grpFootNode = this.NewNode(RmConst.CosGroupFooter, row, -1, ref FScriptList);
							this.isExistsGrpFooter = true;
						}
						else
						{
							RmReportingMakerScriptOutput script = this.NewNode(RmConst.CosOutput, row, -1, ref FScriptList);
						}
					}
					i++;
				}
				else
				{
					this.GenDataSetScript(((RmDataSet)row.Data).Root, i, ref i, detailRows, ref FScriptList);
				}
			}
			return FScriptList;
		}

		internal void InitRowExpanded(List<List<RdRow>> rowExpanded)
		{
			for (int i = 0; i < this.Template.Rows.RowCount + 1; i++)
			{
				rowExpanded.Add(new List<RdRow>());
			}
		}

		internal string Process(string value)
		{
			StringBuilder ret = new StringBuilder();
			string[] tempValue = value.Split(new char[]
			{
				'\r',
				'\n'
			});
			for (int i = 0; i < tempValue.Length; i++)
			{
				bool flag = tempValue[i].IndexOf("=") == 0;
				if (flag)
				{
					this.ReportExpression.Expression = tempValue[i].Substring(1);
					ret.AppendLine(this.ReportExpression.Value.ToString());
				}
				else
				{
					ret.AppendLine(tempValue[i]);
				}
			}
			return ret.ToString();
		}

		internal void ProcessReportRemarks()
		{
			this.CurComputeCell = null;
			this.OutputReport.PageHeader = this.Process(this.OutputReport.PageHeader);
			this.OutputReport.PageFooter = this.Process(this.OutputReport.PageFooter);
			this.OutputReport.PageLeftRemark = this.Process(this.OutputReport.PageLeftRemark);
			this.OutputReport.PageRightRemark = this.Process(this.OutputReport.PageRightRemark);
		}

		internal void SetColumns()
		{
			this.OutputReport.Columns.ColumnCount = this.Template.Columns.ColumnCount;
			for (int i = 1; i <= this.Template.Columns.ColumnCount; i++)
			{
				this.OutputReport.Columns[i].ColumnType = this.Template.Columns[i].ColumnType;
				this.OutputReport.Columns[i].Width = this.Template.Columns[i].Width;
				this.OutputReport.Columns[i].PageBreak = this.Template.Columns[i].PageBreak;
				RdCell cell = this.OutputReport.GetCell(0, i);
				this.Template.GetCell(0, i).CopyNode(ref cell);
			}
		}

		internal void OutputRows(List<RmReportingMakerScriptOutput> outputScript)
		{
			RmReportingMakerScriptOutput ghNode = null;
			RmReportingMakerScriptOutput gfNode = null;
			object dsValue = null;
			List<object> sValue = new List<object>();
			List<object> sCur = new List<object>();
			string[] groupByList = null;
			bool flag = this.isExistsGrpFooter;
			if (flag)
			{
				gfNode = this.grpFootNode;
				string groupBy = ((RdRow)this.grpFootNode.Pointer).GroupBy;
				groupByList = groupBy.Split(new char[]
				{
					','
				});
				for (int i = 0; i < groupByList.Length; i++)
				{
					this.GrpField.Add(i, groupByList[i]);
				}
			}
			int IP = 0;
			while (IP < outputScript.Count)
			{
				this.m_firstGrpHead = false;
				RmReportingMakerScriptOutput Node = outputScript[IP];
				IP++;
				switch (Node.Action)
				{
				case RmConst.CosOutput:
					this.OutputRow((RdRow)Node.Pointer);
					break;
				case RmConst.CosFirst:
					((RmDataSet)Node.Pointer).First();
					break;
				case RmConst.CosNext:
					this.CheckForceNewPage((RmDataSet)Node.Pointer);
					((RmDataSet)Node.Pointer).Next();
					break;
				case RmConst.CosEOF:
				{
					bool flag2 = ((RmDataSet)Node.Pointer).IsEOF();
					if (flag2)
					{
						IP = Node.Next;
					}
					break;
				}
				case RmConst.CosJump:
					IP = Node.Next;
					break;
				case RmConst.CosRowsStart:
					this.DoRowsStart((RmDataSet)Node.Pointer);
					break;
				case RmConst.CosRowsEnd:
					this.DoRowsEnd((RmDataSet)Node.Pointer);
					break;
				case RmConst.CosRowStart:
				{
					bool flag3 = groupByList != null;
					if (flag3)
					{
						sCur.Clear();
						bool bFlag = true;
						bool flag4 = dsValue == null;
						if (flag4)
						{
							for (int j = 0; j < groupByList.Length; j++)
							{
								bool flag5 = !string.IsNullOrEmpty(groupByList[j]);
								if (flag5)
								{
									dsValue = ((RmDataSet)Node.Pointer).Read(groupByList[j]);
								}
								else
								{
									dsValue = "";
								}
								bool flag6 = dsValue == null;
								if (flag6)
								{
									dsValue = "";
								}
								sValue.Add(dsValue);
								this.GrpHeadValue.Clear();
								this.GrpHeadValue.AddRange(sValue.GetRange(0, sValue.Count));
								bool flag7 = this.grpHeadCell != null;
								if (flag7)
								{
									this.ComputeCell(this.grpHeadCell);
								}
							}
						}
						for (int k = 0; k < groupByList.Length; k++)
						{
							bool flag8 = !string.IsNullOrEmpty(groupByList[k]);
							object dsCur;
							if (flag8)
							{
								dsCur = ((RmDataSet)Node.Pointer).Read(groupByList[k]);
							}
							else
							{
								dsCur = "";
							}
							bool flag9 = dsCur == null;
							if (flag9)
							{
								dsCur = "";
							}
							sCur.Add(dsCur);
							bool flag10 = dsCur.ToString() != sValue[k].ToString();
							if (flag10)
							{
								bFlag = false;
							}
						}
						this.GrpHeadValue.Clear();
						this.GrpHeadValue.AddRange(sCur.GetRange(0, sCur.Count));
						this.GrpFootValue.Clear();
						this.GrpFootValue.AddRange(sValue.GetRange(0, sValue.Count));
						bool flag11 = !bFlag;
						if (flag11)
						{
							bool flag12 = gfNode != null;
							if (flag12)
							{
								this.OutputRow((RdRow)gfNode.Pointer);
							}
							this.DoRowStart((RmDataSet)Node.Pointer);
							bool flag13 = ghNode != null;
							if (flag13)
							{
								this.OutputRow((RdRow)ghNode.Pointer);
							}
							sValue.Clear();
							for (int l = 0; l < groupByList.Length; l++)
							{
								dsValue = ((RmDataSet)Node.Pointer).Read(groupByList[l]);
								bool flag14 = dsValue != null;
								if (flag14)
								{
									sValue.Add("");
								}
								else
								{
									sValue.Add(dsValue);
								}
							}
						}
					}
					else
					{
						this.DoRowStart((RmDataSet)Node.Pointer);
					}
					break;
				}
				case RmConst.CosRowEnd:
					this.DoRowEnd((RmDataSet)Node.Pointer);
					break;
				case RmConst.CosColumnHeader:
					this.DoColumnHeader((RmDataSet)Node.Pointer);
					break;
				case RmConst.CosColumnFooter:
					this.DoColumnFooter((RmDataSet)Node.Pointer);
					break;
				case RmConst.CosGroupHeader:
				{
					this.m_firstGrpHead = true;
					this.OutputRow((RdRow)Node.Pointer);
					ghNode = Node;
					bool flag15 = !this.isExistsGrpFooter;
					if (flag15)
					{
						string groupBy = ((RdRow)Node.Pointer).GroupBy;
						groupByList = groupBy.Split(new char[]
						{
							','
						});
					}
					break;
				}
				case RmConst.CosGroupFooter:
					this.m_grpFootValue.Clear();
					this.m_grpFootValue.AddRange(sCur.GetRange(0, sCur.Count));
					this.OutputRow((RdRow)Node.Pointer);
					gfNode = Node;
					break;
				}
			}
		}

		internal bool CheckOutputCondition(RdRow row, ref bool copyCondition)
		{
			bool ret = true;
			string condition = row.OutputCondition;
			copyCondition = true;
			bool flag = string.IsNullOrEmpty(condition) || condition.IndexOf('@') >= 0;
			bool result;
			if (flag)
			{
				result = ret;
			}
			else
			{
				copyCondition = false;
				this.CurComputeCell = null;
				this.ReportExpression.Expression = condition;
				object ExpResult = this.ReportExpression.Value;
				bool flag2 = ExpResult is string;
				if (flag2)
				{
					result = (ExpResult.ToString() != "0");
				}
				else
				{
					result = (double.Parse(ExpResult.ToString()) != 0.0);
				}
			}
			return result;
		}

		internal void OutputRow(RdRow row)
		{
			bool copyCondition = false;
			bool flag = !this.CheckOutputCondition(row, ref copyCondition);
			if (!flag)
			{
				int outputRowNo = this.OutputRowNo;
				this.OutputRowNo = outputRowNo + 1;
				bool flag2 = this.OutputRowNo > this.OutputRowCount;
				if (flag2)
				{
					this.OutputRowCount++;
					this.OutputReport.Rows.RowCount = this.OutputRowCount;
				}
				RdRow ORow = this.OutputReport.Rows[this.OutputRowNo];
				ORow.ExpandedBy = row;
				ORow.RowType = row.RowType;
				ORow.AutoHeight = row.AutoHeight;
				ORow.Height = row.Height;
				ORow.PageBreak = row.PageBreak;
				ORow.CanBreakTwoPage = row.CanBreakTwoPage;
				bool flag3 = ORow.RowType == RdRowType.rtDetailData;
				if (flag3)
				{
					ORow.Data = this.curRow;
				}
				else
				{
					ORow.Data = this.curRows;
				}
				bool flag4 = copyCondition;
				if (flag4)
				{
					ORow.OutputCondition = row.OutputCondition;
				}
				RdCell _cell = this.OutputReport.GetCell(this.OutputRowNo, 0);
				row.GetCell(0).CopyNode(ref _cell);
				this.rowExpanded[row.Row].Add(ORow);
				this.DoOnOutputRow(row, ORow);
				for (int i = 1; i <= this.Template.Columns.ColumnCount; i++)
				{
					this.OutputCell(row.GetCell(i));
					this.DoOnOutputCell(row.GetCell(i), this.OutputReport.GetCell(this.OutputRowNo, i));
				}
			}
		}

		internal void OutputCell(RdCell cell)
		{
			RdCell OCell = this.OutputReport.GetCell(this.OutputRowNo, cell.Column);
			cell.CopyNode(ref OCell);
			OCell.GeneratedBy = Utils.EncodeCellId(cell.Row, cell.Column);
			OCell.Builder = null;
			OCell.Data = cell;
			bool flag = cell.HiddenBy != null && cell.HiddenBy.Row != cell.Row && cell.HiddenBy.Column == cell.Column;
			if (flag)
			{
				OCell = this.OutputReport.GetCell(this.OutputRowNo - 1, cell.Column);
				bool flag2 = OCell.HiddenBy != null;
				if (flag2)
				{
					OCell = OCell.HiddenBy;
				}
				OCell.Height++;
			}
			else
			{
				OCell.Height = 1;
				bool flag3 = cell.Builder == null;
				if (flag3)
				{
					this.ComputeCell(OCell);
				}
				else
				{
					RmVars AVars = new RmVars(OCell);
					AVars.CopyVars(((RmVars)cell.Builder).Vars);
					foreach (KeyValuePair<string, bool> item in AVars.Vars)
					{
						bool flag4 = !item.Value;
						if (flag4)
						{
							AVars.SetVarValue(item.Key, this.ReportExpression_OnUnknownVariable(item.Key));
						}
					}
					OCell.Builder = AVars;
					this.waitToProcess.Add(OCell);
				}
			}
		}

		internal void ComputeCell(RdCell cell)
		{
			bool flag = cell.AutoDataType != RdDataType.dtFormula || cell.GetAsFormula() == "";
			if (!flag)
			{
				this.CurComputeCell = cell;
				string cellStr = cell.GetAsFormula();
				bool firstGrpHead = this.FirstGrpHead;
				if (firstGrpHead)
				{
					this.grpHeadCell = cell;
				}
				bool flag2 = cellStr.IndexOf("=") == 0;
				if (flag2)
				{
					cellStr = cellStr.Substring(1);
				}
				object value;
				try
				{
					this.ReportExpression.Expression = cellStr;
					value = this.ReportExpression.Value;
					bool flag3 = cellStr.ToUpper().IndexOf("GENCHART") == 0;
					if (flag3)
					{
					}
				}
				catch (ReportingException repExp)
				{
					value = repExp.Message;
				}
				bool grpValFount = this.GrpValFount;
				if (grpValFount)
				{
					bool flag4 = value == null;
					if (flag4)
					{
						cell.SetAsText("ERROR:" + cellStr + "！");
					}
					else
					{
						bool flag5 = value is int || value is float || value is double;
						if (flag5)
						{
							cell.SetAsNumber(Utils.Str2Double(value.ToString(), 0.0));
						}
						else
						{
							cell.SetAsText(value.ToString());
						}
					}
					this.DoOnComputeCell(cell);
				}
				this.CurComputeCell = null;
			}
		}

		internal List<RdCell> SortWaitToProcess()
		{
			int C3;
			int R3;
			int C2;
			int R2;
			int R;
			int C = R = (R2 = (C2 = (R3 = (C3 = -1))));
			string S = string.Empty;
			List<RdCell> ret = new List<RdCell>();
			foreach (RdCell cell in this.waitToProcess)
			{
				RdCell CurCell = cell;
				RdRow _CurRow = this.OutputReport.Rows[CurCell.Row];
				RmRows CurRowsObj = null;
				RmRow CurRowObj = null;
				bool flag = _CurRow.Data != null;
				if (flag)
				{
					bool flag2 = _CurRow.Data is RmRow;
					if (flag2)
					{
						CurRowObj = (RmRow)_CurRow.Data;
						CurRowsObj = CurRowObj.Rows;
					}
					else
					{
						CurRowsObj = (RmRows)_CurRow.Data;
					}
				}
				Utils.DecodeCellId(CurCell.GeneratedBy, ref R, ref C);
				RdRow TemplateRow = this.Template.Rows[R];
				RmDataSet TemplateDataset = (TemplateRow.Data == null) ? new RmDataSet(null) : ((RmDataSet)TemplateRow.Data);
				RmVars CurVars = (CurCell.Builder == null) ? null : ((RmVars)CurCell.Builder);
				foreach (KeyValuePair<string, bool> item in CurVars.Vars)
				{
					bool value = item.Value;
					if (value)
					{
						RdAlias Alias = this.Template.AliasList.FindAlias(item.Key);
						bool flag3 = Alias != null;
						if (flag3)
						{
							R2 = Alias.Top;
							R3 = Alias.Bottom;
							C2 = Alias.Left;
							C3 = Alias.Right;
						}
						else
						{
							bool flag4 = !Utils.DecodeRangeId(item.Key, ref C2, ref R2, ref C3, ref R3);
							if (flag4)
							{
								Utils.DecodeCellId(item.Key, ref R2, ref C2);
								R3 = R2;
								C3 = C2;
							}
						}
						S = ",";
						for (R = R2; R <= R3; R++)
						{
							RdRow CurTemplateRow = this.Template.Rows[R];
							RmDataSet CurTemplateDataset = (CurTemplateRow.Data == null) ? null : ((RmDataSet)CurTemplateRow.Data);
							for (C = C2; C <= C3; C++)
							{
								bool flag5 = R == TemplateRow.Row;
								if (flag5)
								{
									S = S + Utils.EncodeCellId(_CurRow.Row, C) + ",";
								}
								else
								{
									bool flag6 = TemplateRow.RowType == RdRowType.rtReportHeader || TemplateRow.RowType == RdRowType.rtReportFooter || TemplateRow.RowType == RdRowType.rtPageHeader || TemplateRow.RowType == RdRowType.rtPageFooter || CurTemplateRow.RowType == RdRowType.rtReportHeader || CurTemplateRow.RowType == RdRowType.rtReportFooter || CurTemplateRow.RowType == RdRowType.rtPageHeader || CurTemplateRow.RowType == RdRowType.rtPageFooter;
									if (flag6)
									{
										S += this.GetAllExpandedRows(CurTemplateRow, C);
									}
									else
									{
										switch (TemplateRow.RowType)
										{
										case RdRowType.rtColumnHeader:
										case RdRowType.rtGroupHeader:
										case RdRowType.rtDetailData:
										case RdRowType.rtGroupFooter:
										case RdRowType.rtColumnFooter:
											switch (CurTemplateRow.RowType)
											{
											case RdRowType.rtColumnHeader:
											case RdRowType.rtColumnFooter:
											{
												bool flag7 = CurTemplateDataset == null;
												if (flag7)
												{
													S += this.GetAllExpandedRows(CurTemplateRow, C);
												}
												else
												{
													bool flag8 = TemplateDataset == CurTemplateDataset;
													if (flag8)
													{
														bool flag9 = _CurRow.Data != null;
														if (flag9)
														{
															S += this.GetColumnHeaderOrFooterRows(CurTemplateRow, CurRowsObj, C);
														}
													}
													else
													{
														bool flag10 = CurTemplateDataset.IsMasterOf(TemplateDataset);
														if (flag10)
														{
															RmRows ARows = CurRowsObj;
															while (ARows != null && ARows.Dataset != CurTemplateDataset && ARows.Row != null)
															{
																ARows = ARows.Row.Rows;
															}
															bool flag11 = ARows != null && ARows.Dataset == CurTemplateDataset;
															if (flag11)
															{
																S += this.GetColumnHeaderOrFooterRows(CurTemplateRow, ARows, C);
															}
														}
														else
														{
															bool flag12 = TemplateDataset.IsMasterOf(CurTemplateDataset);
															if (flag12)
															{
																bool flag13 = CurRowObj != null;
																if (flag13)
																{
																	S += this.GetChildrenColumnHeaderOrFooterRows(CurTemplateRow, CurRowObj, C);
																}
																else
																{
																	S += this.GetChildrenColumnHeaderOrFooterRowsEx(CurTemplateRow, CurRowsObj, C);
																}
															}
															else
															{
																S += this.GetAllExpandedRows(CurTemplateRow, C);
															}
														}
													}
												}
												break;
											}
											case RdRowType.rtDetailData:
											{
												bool flag14 = CurTemplateDataset == null;
												if (flag14)
												{
													S += this.GetAllExpandedRows(CurTemplateRow, C);
												}
												else
												{
													bool flag15 = TemplateDataset == CurTemplateDataset;
													if (flag15)
													{
														bool flag16 = CurRowObj != null;
														if (flag16)
														{
															S += this.GetAllRow(CurTemplateRow, CurRowObj, C, false);
														}
														else
														{
															S += this.GetAllRowEx(CurTemplateRow, CurRowsObj, C, false);
														}
													}
													else
													{
														bool flag17 = CurTemplateDataset.IsMasterOf(TemplateDataset);
														if (flag17)
														{
															S += this.GetAllRow(CurTemplateRow, this.FindMasterRow(CurRowsObj, CurTemplateDataset), C, false);
														}
														else
														{
															bool flag18 = TemplateDataset.IsMasterOf(CurTemplateDataset);
															if (flag18)
															{
																S += this.GetAllRow(CurTemplateRow, CurRowObj, C, true);
															}
															else
															{
																bool flag19 = TemplateRow.RowType == RdRowType.rtGroupFooter;
																if (flag19)
																{
																	S += this.GetGroupExpandedRows(CurTemplateRow, C);
																}
																else
																{
																	S += this.GetAllExpandedRows(CurTemplateRow, C);
																}
															}
														}
													}
												}
												break;
											}
											}
											break;
										}
									}
								}
							}
						}
						CurVars.SetVarValue(item.Key, S);
					}
				}
				this.InsertIntoComputeOrder(ret, CurCell);
			}
			return ret;
		}

		internal RmRow FindMasterRow(RmRows ARows, RmDataSet ADataset)
		{
			RmRow ret = ARows.Row;
			while (ret != null)
			{
				RmRows tmpRows = ret.Rows;
				bool flag = tmpRows == null;
				if (flag)
				{
					ret = null;
				}
				else
				{
					bool flag2 = tmpRows.Dataset == ADataset;
					if (flag2)
					{
						break;
					}
					ret = tmpRows.Row;
				}
			}
			return ret;
		}

		internal string GetGroupExpandedRows(RdRow row, int columnNo)
		{
			return string.Empty;
		}

		internal string GetAllExpandedRows(RdRow row, int columnNo)
		{
			string ret = string.Empty;
			List<RdRow> Expanded = this.rowExpanded[row.Row];
			for (int i = 0; i < Expanded.Count; i++)
			{
				ret = ret + Utils.EncodeCellId(Expanded[i].Row, columnNo) + ",";
			}
			return ret;
		}

		internal string GetColumnHeaderOrFooterRows(RdRow row, RmRows rowsObj, int columnNo)
		{
			string ret = string.Empty;
			bool flag = rowsObj.ColumnHeaderStart > 0;
			if (flag)
			{
				for (int rowIndex = rowsObj.ColumnHeaderStart; rowIndex <= rowsObj.ColumnHeaderEnd; rowIndex++)
				{
					bool flag2 = this.OutputReport.Rows[rowIndex].ExpandedBy == row;
					if (flag2)
					{
						ret = ret + Utils.EncodeCellId(rowIndex, columnNo) + ",";
					}
				}
			}
			bool flag3 = rowsObj.ColumnFooterStart > 0;
			if (flag3)
			{
				for (int rowIndex2 = rowsObj.ColumnFooterStart; rowIndex2 <= rowsObj.ColumnFooterEnd; rowIndex2++)
				{
					bool flag4 = this.OutputReport.Rows[rowIndex2].ExpandedBy == row;
					if (flag4)
					{
						ret = ret + Utils.EncodeCellId(rowIndex2, columnNo) + ",";
					}
				}
			}
			return ret;
		}

		internal string GetChildrenColumnHeaderOrFooterRows(RdRow row, RmRow rowObj, int columnNo)
		{
			string ret = string.Empty;
			bool flag = rowObj.Items == null;
			string result;
			if (flag)
			{
				result = ret;
			}
			else
			{
				foreach (KeyValuePair<int, RmRows> item in rowObj.Items)
				{
					RmRows tmpRows = item.Value;
					ret = ret + this.GetColumnHeaderOrFooterRows(row, tmpRows, columnNo) + this.GetChildrenColumnHeaderOrFooterRowsEx(row, tmpRows, columnNo);
				}
				result = ret;
			}
			return result;
		}

		internal string GetChildrenColumnHeaderOrFooterRowsEx(RdRow row, RmRows rowsObj, int columnNo)
		{
			string ret = string.Empty;
			bool flag = rowsObj.Items == null;
			string result;
			if (flag)
			{
				result = ret;
			}
			else
			{
				foreach (KeyValuePair<int, RmRow> item in rowsObj.Items)
				{
					ret += this.GetChildrenColumnHeaderOrFooterRows(row, item.Value, columnNo);
				}
				result = ret;
			}
			return result;
		}

		internal string GetAllRow(RdRow row, RmRow rowObj, int columnNo, bool res)
		{
			string ret = string.Empty;
			bool flag = rowObj == null || rowObj.RowStart == 0 || rowObj.RowEnd == 0;
			string result;
			if (flag)
			{
				result = ret;
			}
			else
			{
				for (int rowIndex = rowObj.RowStart; rowIndex <= rowObj.RowEnd; rowIndex++)
				{
					bool flag2 = this.OutputReport.Rows[rowIndex].ExpandedBy == row;
					if (flag2)
					{
						ret = ret + Utils.EncodeCellId(rowIndex, columnNo) + ",";
					}
				}
				bool flag3 = res && rowObj.Items != null;
				if (flag3)
				{
					foreach (KeyValuePair<int, RmRows> item in rowObj.Items)
					{
						ret += this.GetAllRowEx(row, item.Value, columnNo, res);
					}
				}
				result = ret;
			}
			return result;
		}

		internal string GetAllRowEx(RdRow row, RmRows rowsObj, int columnNo, bool res)
		{
			string ret = string.Empty;
			bool flag = rowsObj.Items != null;
			if (flag)
			{
				foreach (KeyValuePair<int, RmRow> item in rowsObj.Items)
				{
					ret += this.GetAllRow(row, item.Value, columnNo, res);
				}
			}
			return ret;
		}

		internal bool IsReferences(RdCell cell1, RdCell cell2)
		{
			string S = "," + Utils.EncodeCellId(cell2.Row, cell2.Column) + ",";
			bool flag = cell1.Builder != null;
			bool result;
			if (flag)
			{
				RmVars vars = (RmVars)cell1.Builder;
				foreach (KeyValuePair<string, bool> item in vars.Vars)
				{
					bool flag2 = vars.VarIsCell(item.Key) && vars.VarValue[item.Key].ToString().IndexOf(S) >= 0;
					if (flag2)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		internal void InsertIntoComputeOrder(List<RdCell> sortList, RdCell cell)
		{
			int CellPos = sortList.Count - 1;
			while (CellPos >= 0 && !this.IsReferences(cell, sortList[CellPos]))
			{
				CellPos--;
			}
			CellPos++;
			sortList.Insert(CellPos, cell);
			List<RdCell> AllRefs = new List<RdCell>();
			AllRefs.Add(cell);
			for (int RefPos = 0; RefPos < AllRefs.Count; RefPos++)
			{
				RdCell CurCell = AllRefs[RefPos];
				for (int i = 0; i < CellPos; i++)
				{
					bool flag = AllRefs.IndexOf(sortList[i]) < 0 && this.IsReferences(sortList[i], CurCell);
					if (flag)
					{
						AllRefs.Add(sortList[i]);
					}
				}
			}
			for (int j = CellPos - 1; j >= 0; j--)
			{
				bool flag2 = AllRefs.IndexOf(sortList[j]) >= 0;
				if (flag2)
				{
					RdCell tmpCell = sortList[j];
					sortList.Insert(CellPos + 1, tmpCell);
					sortList.RemoveAt(j);
				}
			}
		}

		internal void ComputeCells()
		{
			for (int i = 0; i < this.computeOrder.Count; i++)
			{
				this.ComputeCell(this.computeOrder[i]);
			}
		}

		internal void CheckForceNewPage(RmDataSet dataset)
		{
			bool flag = dataset.Eof || (dataset.FgtrDataSet.OutputLines != 0 && dataset.RecNo > dataset.FgtrDataSet.OutputLines) || dataset.FgtrDataSet.ForceNewPage == 0 || dataset.RecNo % dataset.FgtrDataSet.ForceNewPage != 0;
			if (!flag)
			{
				this.OutputReport.Rows[this.OutputRowNo].PageBreak = true;
			}
		}

		internal void DoRowsStart(RmDataSet dataset)
		{
			RmRows ARows = new RmRows(this.curRow);
			ARows.Dataset = dataset;
			ARows.RowStart = this.OutputRowNo + 1;
			bool flag = this.curRow != null;
			if (flag)
			{
				this.curRow.Items.Add(this.curRow.Items.Count, ARows);
			}
			this.curRows = ARows;
			this.curRow = null;
		}

		internal void DoRowsEnd(RmDataSet dataset)
		{
			bool flag = this.curRows != null;
			if (flag)
			{
				this.curRow = this.curRows.Row;
				this.curRows.RowEnd = this.OutputRowNo;
				bool flag2 = this.curRow == null;
				if (flag2)
				{
					this.curRows = null;
				}
				else
				{
					this.curRows = this.curRow.Rows;
				}
			}
		}

		internal void DoRowStart(RmDataSet dataset)
		{
			RmRow ARow = new RmRow(this.curRows);
			ARow.RowStart = this.OutputRowNo + 1;
			bool flag = this.curRows != null;
			if (flag)
			{
				this.curRows.Items.Add(this.curRows.Items.Count, ARow);
			}
			this.curRow = ARow;
		}

		internal void DoRowEnd(RmDataSet dataset)
		{
			bool flag = this.curRow != null;
			if (flag)
			{
				this.curRow.RowEnd = this.OutputRowNo;
				this.curRow = null;
			}
		}

		internal void DoColumnHeader(RmDataSet dataset)
		{
			bool flag = this.curRows != null;
			if (flag)
			{
				this.curRows.ColumnHeaderStart = this.OutputRowNo - dataset.ColumnHeader.Count + 1;
				this.curRows.ColumnHeaderEnd = this.OutputRowNo;
			}
		}

		internal void DoColumnFooter(RmDataSet dataset)
		{
			bool flag = this.curRows != null;
			if (flag)
			{
				this.curRows.ColumnFooterStart = this.OutputRowNo - dataset.ColumnFooter.Count + 1;
				this.curRows.ColumnFooterEnd = this.OutputRowNo;
			}
		}

		internal void DoOnOutputRow(RdRow TemplateRow, RdRow oRow)
		{
		}

		internal void DoOnOutputCell(RdCell TemplateCell, RdCell oCell)
		{
		}

		internal void DoOnComputeCell(RdCell cell)
		{
		}
	}
}
