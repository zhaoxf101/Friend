using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	public class RdDocument
	{
		public delegate void GtReportPubGetValue(string funName, string paramOne, string paramTwo, ref CellCalcType valueType, ref string valueTwo);

		private bool m_calComplete = true;

		private RdDataSets m_dataSets;

		private RdRows m_rows;

		private RdColumns m_columns;

		private RdVariables m_variables;

		private RdAliasList m_aliasList;

		private RdImages m_images;

		private List<RdCell> m_formulaCells;

		private Expressions m_cellExp = new Expressions();

		private int m_defaultRowHeight;

		private int m_defaultColumnWidth;

		private string m_paperName;

		private int m_paper;

		private int m_paperWidth;

		private int m_paperHeight;

		private RdPaperOrientation m_paperOrientation;

		private int m_pageLeftMargin;

		private int m_pageTopMargin;

		private int m_pageRightMargin;

		private int m_pageBottomMargin;

		private RdHAlignment m_pageHAlignment;

		private RdVAlignment m_pageVAlignment;

		private bool m_horzCompress;

		private bool m_vertCompress;

		private int m_pageHeaderPos;

		private string m_pageHeader;

		private bool m_pageHeaderLine;

		private bool m_noPageHeaderInPageOne;

		private string m_pageHeaderFont;

		private int m_pageFooterPos;

		private string m_pageFooter;

		private bool m_pageFooterLine;

		private bool m_noPageFooterInPageOne;

		private string m_pageFooterFont;

		private int m_pageLeftRemarkPos;

		private string m_pageLeftRemark;

		private bool m_pageLeftRemarkLine;

		private bool m_noPageLeftRemarkInPageOne;

		private string m_pageLeftRemarkFont;

		private int m_pageRightRemarkPos;

		private string m_pageRightRemark;

		private bool m_pageRightRemarkLine;

		private bool m_noPageRightRemarkInPageOne;

		private string m_pageRightRemarkFont;

		private int m_rowsPerPage;

		private bool m_fullDraw;

		private bool m_extendLastRow;

		private int m_startPageNo;

		private RdCell m_rootCell;

		private RdRegion m_selection;

		private string m_userName;

		private string m_name;

		private DateTime m_createdTime;

		private string m_createdBy;

		private DateTime m_modifiedTime;

		private string m_modifiedBy;

		private string m_template;

		private int m_fixedRows;

		private int m_fixedColumns;

		private bool m_headerVisible;

		private bool m_gridLineVisible;

		private bool m_multiCols;

		private bool m_autoCalc;

		private bool m_showFormula;

		private float m_reportVersion = 1f;

		private ArrayList m_regionBuffers;

		private RdMergeCellsStyle m_defaultStyle = new RdMergeCellsStyle();

		private bool m_modified = false;

		private string m_saveReport = string.Empty;

		internal RdCell _computeCell = null;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal event RdDocument.GtReportPubGetValue OnGtReportPubGetValue;

		internal bool CalComplete
		{
			get
			{
				return this.m_calComplete;
			}
			set
			{
				this.m_calComplete = value;
			}
		}

		public RdDataSets DataSets
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

		public RdRows Rows
		{
			get
			{
				return this.m_rows;
			}
			set
			{
				this.m_rows = value;
			}
		}

		public RdColumns Columns
		{
			get
			{
				return this.m_columns;
			}
			set
			{
				this.m_columns = value;
			}
		}

		internal RdVariables Variables
		{
			get
			{
				return this.m_variables;
			}
			set
			{
				this.m_variables = value;
			}
		}

		internal RdAliasList AliasList
		{
			get
			{
				return this.m_aliasList;
			}
			set
			{
				this.m_aliasList = value;
			}
		}

		internal RdImages Images
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

		internal List<RdCell> FormulaCells
		{
			get
			{
				return this.m_formulaCells;
			}
			set
			{
				this.m_formulaCells = value;
			}
		}

		internal Expressions CellExp
		{
			get
			{
				return this.m_cellExp;
			}
			set
			{
				this.m_cellExp = value;
			}
		}

		internal int DefaultRowHeight
		{
			get
			{
				return this.m_defaultRowHeight;
			}
			set
			{
				this.m_defaultRowHeight = value;
			}
		}

		internal int DefaultColumnWidth
		{
			get
			{
				return this.m_defaultColumnWidth;
			}
			set
			{
				this.m_defaultColumnWidth = value;
			}
		}

		internal string PaperName
		{
			get
			{
				return this.m_paperName;
			}
			set
			{
				this.m_paperName = value;
			}
		}

		internal int Paper
		{
			get
			{
				return this.m_paper;
			}
			set
			{
				bool flag = this.m_paper != value;
				if (flag)
				{
					this.m_paper = value;
					this.PaperName = Utils.Paper2Str(this.m_paper);
					bool flag2 = this.m_paper != 0;
					if (flag2)
					{
						this.PaperWidth = Utils.FindPaperWidth(this.m_paper);
						this.PaperHeight = Utils.FindPaperHeight(this.m_paper);
					}
				}
			}
		}

		internal int PaperWidth
		{
			get
			{
				return this.m_paperWidth;
			}
			set
			{
				this.m_paperWidth = value;
			}
		}

		internal int PaperHeight
		{
			get
			{
				return this.m_paperHeight;
			}
			set
			{
				this.m_paperHeight = value;
			}
		}

		internal RdPaperOrientation PaperOrientation
		{
			get
			{
				return this.m_paperOrientation;
			}
			set
			{
				this.m_paperOrientation = value;
			}
		}

		internal int PageLeftMargin
		{
			get
			{
				return this.m_pageLeftMargin;
			}
			set
			{
				this.m_pageLeftMargin = value;
			}
		}

		internal int PageTopMargin
		{
			get
			{
				return this.m_pageTopMargin;
			}
			set
			{
				this.m_pageTopMargin = value;
			}
		}

		internal int PageRightMargin
		{
			get
			{
				return this.m_pageRightMargin;
			}
			set
			{
				this.m_pageRightMargin = value;
			}
		}

		internal int PageBottomMargin
		{
			get
			{
				return this.m_pageBottomMargin;
			}
			set
			{
				this.m_pageBottomMargin = value;
			}
		}

		internal RdHAlignment PageHAlignment
		{
			get
			{
				return this.m_pageHAlignment;
			}
			set
			{
				this.m_pageHAlignment = value;
			}
		}

		internal RdVAlignment PageVAlignment
		{
			get
			{
				return this.m_pageVAlignment;
			}
			set
			{
				this.m_pageVAlignment = value;
			}
		}

		internal bool HorzCompress
		{
			get
			{
				return this.m_horzCompress;
			}
			set
			{
				this.m_horzCompress = value;
			}
		}

		internal bool VertCompress
		{
			get
			{
				return this.m_vertCompress;
			}
			set
			{
				this.m_vertCompress = value;
			}
		}

		internal int PageHeaderPos
		{
			get
			{
				return this.m_pageHeaderPos;
			}
			set
			{
				this.m_pageHeaderPos = value;
			}
		}

		internal string PageHeader
		{
			get
			{
				return this.m_pageHeader;
			}
			set
			{
				this.m_pageHeader = value;
			}
		}

		internal bool PageHeaderLine
		{
			get
			{
				return this.m_pageHeaderLine;
			}
			set
			{
				this.m_pageHeaderLine = value;
			}
		}

		internal bool NoPageHeaderInPageOne
		{
			get
			{
				return this.m_noPageHeaderInPageOne;
			}
			set
			{
				this.m_noPageHeaderInPageOne = value;
			}
		}

		internal string PageHeaderFont
		{
			get
			{
				return this.m_pageHeaderFont;
			}
			set
			{
				this.m_pageHeaderFont = value;
			}
		}

		internal int PageFooterPos
		{
			get
			{
				return this.m_pageFooterPos;
			}
			set
			{
				this.m_pageFooterPos = value;
			}
		}

		internal string PageFooter
		{
			get
			{
				return this.m_pageFooter;
			}
			set
			{
				this.m_pageFooter = value;
			}
		}

		internal bool PageFooterLine
		{
			get
			{
				return this.m_pageFooterLine;
			}
			set
			{
				this.m_pageFooterLine = value;
			}
		}

		internal bool NoPageFooterInPageOne
		{
			get
			{
				return this.m_noPageFooterInPageOne;
			}
			set
			{
				this.m_noPageFooterInPageOne = value;
			}
		}

		internal string PageFooterFont
		{
			get
			{
				return this.m_pageFooterFont;
			}
			set
			{
				this.m_pageFooterFont = value;
			}
		}

		internal int PageLeftRemarkPos
		{
			get
			{
				return this.m_pageLeftRemarkPos;
			}
			set
			{
				this.m_pageLeftRemarkPos = value;
			}
		}

		internal string PageLeftRemark
		{
			get
			{
				return this.m_pageLeftRemark;
			}
			set
			{
				this.m_pageLeftRemark = value;
			}
		}

		internal bool PageLeftRemarkLine
		{
			get
			{
				return this.m_pageLeftRemarkLine;
			}
			set
			{
				this.m_pageLeftRemarkLine = value;
			}
		}

		internal bool NoPageLeftRemarkInPageOne
		{
			get
			{
				return this.m_noPageLeftRemarkInPageOne;
			}
			set
			{
				this.m_noPageLeftRemarkInPageOne = value;
			}
		}

		internal string PageLeftRemarkFont
		{
			get
			{
				return this.m_pageLeftRemarkFont;
			}
			set
			{
				this.m_pageLeftRemarkFont = value;
			}
		}

		internal int PageRightRemarkPos
		{
			get
			{
				return this.m_pageRightRemarkPos;
			}
			set
			{
				this.m_pageRightRemarkPos = value;
			}
		}

		internal string PageRightRemark
		{
			get
			{
				return this.m_pageRightRemark;
			}
			set
			{
				this.m_pageRightRemark = value;
			}
		}

		internal bool PageRightRemarkLine
		{
			get
			{
				return this.m_pageRightRemarkLine;
			}
			set
			{
				this.m_pageRightRemarkLine = value;
			}
		}

		internal bool NoPageRightRemarkInPageOne
		{
			get
			{
				return this.m_noPageRightRemarkInPageOne;
			}
			set
			{
				this.m_noPageRightRemarkInPageOne = value;
			}
		}

		internal string PageRightRemarkFont
		{
			get
			{
				return this.m_pageRightRemarkFont;
			}
			set
			{
				this.m_pageRightRemarkFont = value;
			}
		}

		internal int RowsPerPage
		{
			get
			{
				return this.m_rowsPerPage;
			}
			set
			{
				this.m_rowsPerPage = value;
			}
		}

		internal bool FullDraw
		{
			get
			{
				return this.m_fullDraw;
			}
			set
			{
				this.m_fullDraw = value;
			}
		}

		internal bool ExtendLastRow
		{
			get
			{
				return this.m_extendLastRow;
			}
			set
			{
				this.m_extendLastRow = value;
			}
		}

		internal int StartPageNo
		{
			get
			{
				return this.m_startPageNo;
			}
			set
			{
				this.m_startPageNo = value;
			}
		}

		internal RdCell RootCell
		{
			get
			{
				return this.m_rootCell;
			}
			set
			{
				this.m_rootCell = value;
			}
		}

		internal RdRegion Selection
		{
			get
			{
				return this.m_selection;
			}
			set
			{
				this.m_selection = value;
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

		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		internal DateTime CreatedTime
		{
			get
			{
				return this.m_createdTime;
			}
			set
			{
				this.m_createdTime = value;
			}
		}

		internal string CreatedBy
		{
			get
			{
				return this.m_createdBy;
			}
			set
			{
				this.m_createdBy = value;
			}
		}

		internal DateTime ModifiedTime
		{
			get
			{
				return this.m_modifiedTime;
			}
			set
			{
				this.m_modifiedTime = value;
			}
		}

		internal string ModifiedBy
		{
			get
			{
				return this.m_modifiedBy;
			}
			set
			{
				this.m_modifiedBy = value;
			}
		}

		internal string Template
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

		internal int FixedRows
		{
			get
			{
				return this.m_fixedRows;
			}
			set
			{
				this.m_fixedRows = value;
			}
		}

		internal int FixedColumns
		{
			get
			{
				return this.m_fixedColumns;
			}
			set
			{
				this.m_fixedColumns = value;
			}
		}

		internal bool HeaderVisible
		{
			get
			{
				return this.m_headerVisible;
			}
			set
			{
				this.m_headerVisible = value;
			}
		}

		internal bool GridLineVisible
		{
			get
			{
				return this.m_gridLineVisible;
			}
			set
			{
				this.m_gridLineVisible = value;
			}
		}

		internal bool MultiCols
		{
			get
			{
				return this.m_multiCols;
			}
			set
			{
				this.m_multiCols = value;
			}
		}

		internal bool AutoCalc
		{
			get
			{
				return this.m_autoCalc;
			}
			set
			{
				this.m_autoCalc = value;
			}
		}

		internal bool ShowFormula
		{
			get
			{
				return this.m_showFormula;
			}
			set
			{
				this.m_showFormula = value;
			}
		}

		internal float ReportVersion
		{
			get
			{
				return this.m_reportVersion;
			}
			set
			{
				this.m_reportVersion = value;
			}
		}

		internal ArrayList RegionBuffers
		{
			get
			{
				return this.m_regionBuffers;
			}
			set
			{
				this.m_regionBuffers = value;
			}
		}

		internal RdMergeCellsStyle DefaultStyle
		{
			get
			{
				return this.m_defaultStyle;
			}
			set
			{
				this.m_defaultStyle = value;
			}
		}

		internal bool Modified
		{
			get
			{
				return this.m_modified;
			}
			set
			{
				this.m_modified = value;
			}
		}

		internal string SaveReport
		{
			get
			{
				return this.m_saveReport;
			}
			set
			{
				this.m_saveReport = value;
			}
		}

		internal void Changed()
		{
		}

		public RdCell GetCell(int rowIndex, int columnIndex)
		{
			RdCell _cell = null;
			RdColumn _column = this.Columns[columnIndex];
			bool flag = _column != null;
			if (flag)
			{
				_cell = _column[rowIndex];
			}
			return _cell;
		}

		internal RdMergeCellsStyle GetCellStyle(int rowIndex, int columnIndex)
		{
			bool flag = rowIndex > this.Rows.RowCount && columnIndex > this.Columns.ColumnCount;
			RdMergeCellsStyle ret;
			if (flag)
			{
				ret = this.RootCell.Style;
			}
			else
			{
				bool flag2 = rowIndex > this.Rows.RowCount;
				if (flag2)
				{
					ret = this.GetCell(0, columnIndex).Style;
				}
				else
				{
					bool flag3 = columnIndex > this.Columns.ColumnCount;
					if (flag3)
					{
						ret = this.GetCell(rowIndex, 0).Style;
					}
					else
					{
						RdCell _cell = this.GetCell(rowIndex, columnIndex);
						bool flag4 = _cell.HiddenBy != null;
						if (flag4)
						{
							_cell = _cell.HiddenBy;
						}
						ret = _cell.Style;
					}
				}
			}
			return ret;
		}

		internal void Clear()
		{
			this.RootCell = null;
			this.Columns.Clear();
			this.Rows.Clear();
			this.DataSets.Clear();
			this.Variables.Clear();
			this.AliasList.Clear();
			this.Images.Clear();
			this.FormulaCells.Clear();
		}

		public RdDocument()
		{
			this.CalComplete = true;
			this.Selection = new RdRegion(this);
			this.Rows = new RdRows(this);
			this.Columns = new RdColumns(this);
			this.DataSets = new RdDataSets(this);
			this.Variables = new RdVariables(this);
			this.AliasList = new RdAliasList(this);
			this.Images = new RdImages(this);
			this.FormulaCells = new List<RdCell>();
			this.CellExp = new Expressions();
			this.CellExp.OnUnknownVariable += new Expressions.TOnUnknownVariable(this.CellExp_OnUnknownVariable);
			this.CellExp.OnUnknownFunction += new Expressions.TOnUnknownFunction(this.CellExp_OnUnknownFunction);
			this.RegionBuffers = new ArrayList();
			this.DefaultStyle = new RdMergeCellsStyle();
			this.Init();
		}

		internal void Init()
		{
			this.Clear();
			Utils.StyleRecInit(ref this.m_defaultStyle);
			this.DefaultStyle.LeftBorderStyle = RdLineStyle.lsNone;
			this.DefaultStyle.TopBorderStyle = RdLineStyle.lsNone;
			this.DefaultStyle.RightBorderStyle = RdLineStyle.lsNone;
			this.DefaultStyle.BottomBorderStyle = RdLineStyle.lsNone;
			this.ReportVersion = 1f;
			this.Name = "报表";
			this.CreatedTime = DateTime.Now;
			this.CreatedBy = this.UserName;
			this.ModifiedTime = this.CreatedTime;
			this.ModifiedBy = this.UserName;
			this.Template = "";
			this.FixedRows = 0;
			this.FixedColumns = 0;
			this.Paper = 9;
			this.PaperOrientation = RdPaperOrientation.roPortrait;
			this.PageLeftMargin = 748;
			this.PageTopMargin = 984;
			this.PageRightMargin = 748;
			this.PageBottomMargin = 984;
			this.PageHAlignment = RdHAlignment.haLeft;
			this.PageVAlignment = RdVAlignment.vaTop;
			this.HorzCompress = false;
			this.VertCompress = false;
			this.PageHeaderPos = 591;
			this.PageHeaderLine = false;
			this.NoPageHeaderInPageOne = false;
			this.PageHeaderFont = "宋体;9;#000000;134";
			this.PageFooterPos = 591;
			this.PageFooterLine = false;
			this.NoPageFooterInPageOne = false;
			this.PageFooterFont = "宋体;9;#000000;134";
			this.PageLeftRemarkPos = 591;
			this.PageLeftRemarkLine = false;
			this.NoPageLeftRemarkInPageOne = false;
			this.PageLeftRemarkFont = "宋体;9;#000000;134";
			this.PageRightRemarkPos = 591;
			this.PageRightRemarkLine = false;
			this.NoPageRightRemarkInPageOne = false;
			this.PageRightRemarkFont = "宋体;9;#000000;134";
			this.HeaderVisible = true;
			this.GridLineVisible = true;
			this.MultiCols = false;
			this.AutoCalc = false;
			this.ShowFormula = true;
			this.ExtendLastRow = false;
			this.RowsPerPage = 0;
			this.FullDraw = false;
			this.StartPageNo = 1;
			this.PageHeader = "";
			this.PageFooter = "";
			this.PageLeftRemark = "";
			this.PageRightRemark = "";
			this.DefaultColumnWidth = 1000;
			Font font = new Font("宋体", 9f, FontStyle.Regular);
			this.DefaultRowHeight = 203;
			this.Rows.RowCount = 0;
			this.Columns.ColumnCount = 0;
			this.RootCell = this.GetCell(0, 0);
			this.Selection.Boundary = new Rectangle(1, 1, 1, 1);
			this.Selection.RefreshStyles();
			this.Modified = false;
		}

		private object CellExp_OnUnknownFunction(string functionName, ArrayList paramList)
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
					result = "";
				}
				else
				{
					bool flag3 = functionName.ToUpper() == "GENCHART";
					if (flag3)
					{
						RdImage img = this.m_images.GenChartImage(this, this._computeCell, paramList);
						bool flag4 = img != null;
						if (flag4)
						{
							this.m_images.List.Add(img.Name, img);
						}
						result = "";
					}
					else
					{
						string paramStr = "";
						CellCalcType valueType = CellCalcType.S;
						string value = "";
						for (int i = 0; i < paramList.Count; i++)
						{
							bool flag5 = i == 0;
							if (flag5)
							{
								paramStr = paramList[i].ToString();
							}
							else
							{
								paramStr = paramStr + "," + paramList[i].ToString();
							}
						}
						try
						{
							this.OnGtReportPubGetValue("OnDocUnknownFunction", functionName, paramStr, ref valueType, ref value);
						}
						catch
						{
							throw new ReportingException(string.Format("未知函数：{0}", functionName));
						}
						bool flag6 = valueType == CellCalcType.N;
						if (flag6)
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

		private object GetCellValue(RdCell cell)
		{
			bool flag = cell == null;
			object result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				RdDataType autoDataType = cell.AutoDataType;
				if (autoDataType != RdDataType.dtNumber)
				{
					if (autoDataType != RdDataType.dtFormula)
					{
						result = cell.GetAsString();
					}
					else
					{
						bool flag2 = Utils.IsNumber(cell.Result);
						if (flag2)
						{
							result = Utils.Str2Double(cell.Result, 0.0);
						}
						else
						{
							result = cell.Result;
						}
					}
				}
				else
				{
					result = cell.GetAsNumber();
				}
			}
			return result;
		}

		private object CellExp_OnUnknownVariable(string variable)
		{
			int C2;
			int R2;
			int R;
			int C = R = (R2 = (C2 = 0));
			bool flag = Utils.DecodeRangeId(variable, ref C, ref R, ref C2, ref R2);
			object result;
			if (flag)
			{
				ArrayList listVarValue = new ArrayList();
				for (int rowIndex = R; rowIndex <= R2; rowIndex++)
				{
					for (int columnIndex = C; columnIndex <= C2; columnIndex++)
					{
						listVarValue.Add(this.GetCellValue(this.GetCell(rowIndex, columnIndex)));
					}
				}
				result = this.CellExp.DirectCompute("SUM", listVarValue);
			}
			else
			{
				bool flag2 = Utils.DecodeCellId(variable, ref R, ref C);
				if (flag2)
				{
					result = this.GetCellValue(this.GetCell(R, C));
				}
				else
				{
					CellCalcType valueType = CellCalcType.S;
					string value = "";
					RdVariable tmpVariable = this.Variables.FindVariable(variable);
					string sql = (tmpVariable == null) ? "" : tmpVariable.Formula;
					try
					{
						this.OnGtReportPubGetValue("OnDocUnknownVariable", variable, sql, ref valueType, ref value);
					}
					catch
					{
						throw new ReportingException(string.Format("未知变量：{0}", variable));
					}
					result = value;
				}
			}
			return result;
		}

		public void SetReport(string value)
		{
			bool flag = string.IsNullOrEmpty(value);
			if (!flag)
			{
				XmlDocument document = new XmlDocument();
				document.LoadXml(value);
				XmlElement rootNode = document.DocumentElement;
				bool flag2 = rootNode.Name != "Report";
				if (!flag2)
				{
					this.ReportVersion = Utils.GetAttrFloat(rootNode, "Version", 1f);
					this.LoadDefaultProperties(rootNode);
					this.LoadReport(rootNode);
					this.LoadDataSets(rootNode);
					this.LoadVariables(rootNode);
					this.LoadAliasList(rootNode);
					this.LoadImages(rootNode);
					this.LoadRows(rootNode);
					this.LoadColumns(rootNode);
					this.LoadCells(rootNode);
					this.Selection.Boundary = new Rectangle(1, 1, 1, 1);
					this.Selection.RefreshStyles();
					this.SaveReport = value;
					this.Modified = false;
					this.ReportVersion = 1f;
				}
			}
		}

		private void Pack()
		{
			this.Rows.Pack();
			this.Columns.Pack();
		}

		public string GetReport()
		{
			this.Pack();
			return string.Concat(new string[]
			{
				"<?xml version=\"1.0\" encoding=\"GB2312\"?><Report",
				this.ReportXML(),
				">",
				this.DataSets.GetXml(),
				this.Variables.GetXml(),
				this.Images.GetXml(),
				this.Rows.GetXml(),
				this.Columns.GetXml(),
				this.CellsXml(),
				"</Report>"
			});
		}

		public string GetReportHtml()
		{
			return this.GetReport();
		}

		private string ReportXML()
		{
			string ret = string.Empty;
			ret = Utils.MakeAttribute("Version", this.ReportVersion.ToString());
			bool flag = !string.IsNullOrEmpty(this.Name);
			if (flag)
			{
				ret += Utils.MakeAttribute("Name", this.Name);
			}
			ret += Utils.MakeAttribute("Created", this.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
			bool flag2 = !string.IsNullOrEmpty(this.CreatedBy);
			if (flag2)
			{
				ret += Utils.MakeAttribute("CreatedBy", this.CreatedBy);
			}
			ret += Utils.MakeAttribute("Modified", this.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss"));
			bool flag3 = !string.IsNullOrEmpty(this.ModifiedBy);
			if (flag3)
			{
				ret += Utils.MakeAttribute("ModifiedBy", this.ModifiedBy);
			}
			bool flag4 = !string.IsNullOrEmpty(this.Template);
			if (flag4)
			{
				ret += Utils.MakeAttribute("Template", this.Template);
			}
			bool flag5 = this.FixedRows != 0;
			if (flag5)
			{
				ret += Utils.MakeAttribute("FixedRows", this.FixedRows.ToString());
			}
			bool flag6 = this.FixedColumns != 0;
			if (flag6)
			{
				ret += Utils.MakeAttribute("FixedColumns", this.FixedColumns.ToString());
			}
			bool flag7 = this.PaperName != "A4";
			if (flag7)
			{
				ret += Utils.MakeAttribute("PaperName", this.PaperName);
			}
			bool flag8 = this.Paper != 9;
			if (flag8)
			{
				ret += Utils.MakeAttribute("Paper", this.Paper.ToString());
			}
			ret += Utils.MakeAttribute("PaperWidth", this.PaperWidth.ToString());
			ret += Utils.MakeAttribute("PaperHeight", this.PaperHeight.ToString());
			bool flag9 = this.PaperOrientation > RdPaperOrientation.roPortrait;
			if (flag9)
			{
				ret += Utils.MakeAttribute("PaperOrientation", Utils.PaperOrientation2Str(this.PaperOrientation));
			}
			bool flag10 = this.PageLeftMargin != 748;
			if (flag10)
			{
				ret += Utils.MakeAttribute("PageLeftMargin", this.PageLeftMargin.ToString());
			}
			bool flag11 = this.PageTopMargin != 984;
			if (flag11)
			{
				ret += Utils.MakeAttribute("PageTopMargin", this.PageTopMargin.ToString());
			}
			bool flag12 = this.PageRightMargin != 748;
			if (flag12)
			{
				ret += Utils.MakeAttribute("PageRightMargin", this.PageRightMargin.ToString());
			}
			bool flag13 = this.PageBottomMargin != 984;
			if (flag13)
			{
				ret += Utils.MakeAttribute("PageBottomMargin", this.PageBottomMargin.ToString());
			}
			bool flag14 = this.PageHAlignment != RdHAlignment.haLeft;
			if (flag14)
			{
				ret += Utils.MakeAttribute("PageHAlignment", Utils.HAlignment2Str(this.PageHAlignment));
			}
			bool flag15 = this.PageVAlignment > RdVAlignment.vaTop;
			if (flag15)
			{
				ret += Utils.MakeAttribute("PageVAlignment", Utils.VAlignment2Str(this.PageVAlignment));
			}
			bool horzCompress = this.HorzCompress;
			if (horzCompress)
			{
				ret += Utils.MakeAttribute("HorzCompress", Utils.Bool2Str(this.HorzCompress));
			}
			bool vertCompress = this.VertCompress;
			if (vertCompress)
			{
				ret += Utils.MakeAttribute("VertCompress", Utils.Bool2Str(this.VertCompress));
			}
			bool flag16 = !string.IsNullOrEmpty(this.PageHeader);
			if (flag16)
			{
				ret += Utils.MakeAttribute("PageHeader", this.PageHeader);
			}
			bool flag17 = this.PageHeaderPos != 591;
			if (flag17)
			{
				ret += Utils.MakeAttribute("PageHeaderPos", this.PageHeaderPos.ToString());
			}
			bool pageHeaderLine = this.PageHeaderLine;
			if (pageHeaderLine)
			{
				ret += Utils.MakeAttribute("PageHeaderLine", Utils.Bool2Str(this.PageHeaderLine));
			}
			bool noPageHeaderInPageOne = this.NoPageHeaderInPageOne;
			if (noPageHeaderInPageOne)
			{
				ret += Utils.MakeAttribute("NoPageHeaderInPageOne", Utils.Bool2Str(this.NoPageHeaderInPageOne));
			}
			bool flag18 = this.PageHeaderFont != "宋体;9;#000000;134";
			if (flag18)
			{
				ret += Utils.MakeAttribute("PageHeaderFont", this.PageHeaderFont);
			}
			bool flag19 = !string.IsNullOrEmpty(this.PageFooter);
			if (flag19)
			{
				ret += Utils.MakeAttribute("PageFooter", this.PageFooter);
			}
			bool flag20 = this.PageFooterPos != 591;
			if (flag20)
			{
				ret += Utils.MakeAttribute("PageFooterPos", this.PageFooterPos.ToString());
			}
			bool pageFooterLine = this.PageFooterLine;
			if (pageFooterLine)
			{
				ret += Utils.MakeAttribute("PageFooterLine", Utils.Bool2Str(this.PageFooterLine));
			}
			bool noPageFooterInPageOne = this.NoPageFooterInPageOne;
			if (noPageFooterInPageOne)
			{
				ret += Utils.MakeAttribute("NoPageFooterInPageOne", Utils.Bool2Str(this.NoPageFooterInPageOne));
			}
			bool flag21 = this.PageFooterFont != "宋体;9;#000000;134";
			if (flag21)
			{
				ret += Utils.MakeAttribute("PageFooterFont", this.PageFooterFont);
			}
			bool flag22 = !string.IsNullOrEmpty(this.PageLeftRemark);
			if (flag22)
			{
				ret += Utils.MakeAttribute("PageLeftRemark", this.PageLeftRemark);
			}
			bool flag23 = this.PageLeftRemarkPos != 591;
			if (flag23)
			{
				ret += Utils.MakeAttribute("PageLeftRemarkPos", this.PageLeftRemarkPos.ToString());
			}
			bool pageLeftRemarkLine = this.PageLeftRemarkLine;
			if (pageLeftRemarkLine)
			{
				ret += Utils.MakeAttribute("PageLeftRemarkLine", Utils.Bool2Str(this.PageLeftRemarkLine));
			}
			bool noPageLeftRemarkInPageOne = this.NoPageLeftRemarkInPageOne;
			if (noPageLeftRemarkInPageOne)
			{
				ret += Utils.MakeAttribute("NoPageLeftRemarkInPageOne", Utils.Bool2Str(this.NoPageLeftRemarkInPageOne));
			}
			bool flag24 = this.PageLeftRemarkFont != "宋体;9;#000000;134";
			if (flag24)
			{
				ret += Utils.MakeAttribute("PageLeftRemarkFont", this.PageLeftRemarkFont);
			}
			bool flag25 = !string.IsNullOrEmpty(this.PageRightRemark);
			if (flag25)
			{
				ret += Utils.MakeAttribute("PageRightRemark", this.PageRightRemark);
			}
			bool flag26 = this.PageRightRemarkPos != 591;
			if (flag26)
			{
				ret += Utils.MakeAttribute("PageRightRemarkPos", this.PageRightRemarkPos.ToString());
			}
			bool pageRightRemarkLine = this.PageRightRemarkLine;
			if (pageRightRemarkLine)
			{
				ret += Utils.MakeAttribute("PageRightRemarkLine", Utils.Bool2Str(this.PageRightRemarkLine));
			}
			bool noPageRightRemarkInPageOne = this.NoPageRightRemarkInPageOne;
			if (noPageRightRemarkInPageOne)
			{
				ret += Utils.MakeAttribute("NoPageRightRemarkInPageOne", Utils.Bool2Str(this.NoPageRightRemarkInPageOne));
			}
			bool flag27 = this.PageRightRemarkFont != "宋体;9;#000000;134";
			if (flag27)
			{
				ret += Utils.MakeAttribute("PageRightRemarkFont", this.PageRightRemarkFont);
			}
			bool flag28 = this.RowsPerPage != 0;
			if (flag28)
			{
				ret += Utils.MakeAttribute("RowsPerPage", this.RowsPerPage.ToString());
			}
			bool fullDraw = this.FullDraw;
			if (fullDraw)
			{
				ret += Utils.MakeAttribute("FullDraw", Utils.Bool2Str(this.FullDraw));
			}
			bool extendLastRow = this.ExtendLastRow;
			if (extendLastRow)
			{
				ret += Utils.MakeAttribute("ExtendLastRow", Utils.Bool2Str(this.ExtendLastRow));
			}
			bool flag29 = this.StartPageNo != 1;
			if (flag29)
			{
				ret += Utils.MakeAttribute("StartPageNo", this.StartPageNo.ToString());
			}
			bool flag30 = !this.HeaderVisible;
			if (flag30)
			{
				ret += Utils.MakeAttribute("HeaderVisible", Utils.Bool2Str(this.HeaderVisible));
			}
			bool flag31 = !this.GridLineVisible;
			if (flag31)
			{
				ret += Utils.MakeAttribute("GridLineVisible", Utils.Bool2Str(this.GridLineVisible));
			}
			bool multiCols = this.MultiCols;
			if (multiCols)
			{
				ret += Utils.MakeAttribute("MultiCols", Utils.Bool2Str(this.MultiCols));
			}
			bool autoCalc = this.AutoCalc;
			if (autoCalc)
			{
				ret += Utils.MakeAttribute("AutoCalc", Utils.Bool2Str(this.AutoCalc));
			}
			bool flag32 = !this.ShowFormula;
			if (flag32)
			{
				ret += Utils.MakeAttribute("ShowFormula", Utils.Bool2Str(this.ShowFormula));
			}
			ret += Utils.MakeAttribute("RowCount", this.Rows.RowCount.ToString());
			return ret + Utils.MakeAttribute("ColumnCount", this.Columns.ColumnCount.ToString());
		}

		private string CellsXml()
		{
			string ret = string.Empty;
			RdCells Cells = new RdCells(this);
			return Cells.GetXml();
		}

		private void LoadDefaultProperties(XmlNode rootNode)
		{
			this.RootCell.Load(rootNode);
			XmlNodeList nodeList = rootNode.SelectNodes("Cells/Style");
			bool flag = nodeList != null && nodeList.Count > 0;
			if (flag)
			{
				XmlNode _node = nodeList[0];
				RdMergeCellsStyle styleRec = this.RootCell.Style;
				Utils.StyleRecLoad(_node, ref styleRec, this.DefaultStyle);
			}
			RdMergeCellsStyle tmpStyleRec = new RdMergeCellsStyle();
			Utils.CopyStyleRec(ref tmpStyleRec, this.RootCell.Style);
			this.DefaultStyle = tmpStyleRec;
			this.DefaultColumnWidth = 1000;
			Font font = new Font(this.RootCell.Style.FontName, (float)this.RootCell.Style.FontSize);
			this.DefaultRowHeight = 203;
		}

		private void LoadReport(XmlNode rootNode)
		{
			this.Name = Utils.GetXmlNodeAttribute(rootNode, "Name");
			bool flag = Utils.GetAttrInt(rootNode, "RowCount", -1) != -1;
			if (flag)
			{
				this.Rows.RowCount = Utils.GetAttrInt(rootNode, "RowCount", -1);
			}
			bool flag2 = Utils.GetAttrInt(rootNode, "ColumnCount", -1) != -1;
			if (flag2)
			{
				this.Columns.ColumnCount = Utils.GetAttrInt(rootNode, "ColumnCount", -1);
			}
			this.CreatedTime = Utils.GetAttrDateTime(rootNode, "Created", DateTime.MinValue);
			this.CreatedBy = Utils.GetXmlNodeAttribute(rootNode, "CreatedBy");
			this.ModifiedTime = Utils.GetAttrDateTime(rootNode, "Modified", DateTime.MinValue);
			this.ModifiedBy = Utils.GetXmlNodeAttribute(rootNode, "ModifiedBy");
			this.Template = Utils.GetXmlNodeAttribute(rootNode, "Template");
			this.FixedRows = Utils.GetAttrInt(rootNode, "FixedRows", this.FixedRows);
			this.FixedColumns = Utils.GetAttrInt(rootNode, "FixedColumns", this.FixedColumns);
			this.PaperName = Utils.GetXmlNodeAttribute(rootNode, "PaperName");
			bool flag3 = Utils.Str2Paper(this.PaperName) != -2147483648;
			if (flag3)
			{
				this.Paper = Utils.Str2Paper(this.PaperName);
			}
			bool flag4 = !string.IsNullOrEmpty(this.PaperName) || this.Paper == -2147483648;
			if (flag4)
			{
				string tmpPaper = Utils.GetXmlNodeAttribute(rootNode, "Paper");
				bool flag5 = !string.IsNullOrEmpty(tmpPaper);
				if (flag5)
				{
					bool flag6 = Utils.IsNumber(tmpPaper);
					if (flag6)
					{
						this.Paper = Utils.Str2Int(tmpPaper, 0);
					}
					else
					{
						bool flag7 = tmpPaper.ToUpper() == "CUSTOM";
						if (flag7)
						{
							this.Paper = 0;
						}
						else
						{
							this.Paper = Utils.Str2Paper(tmpPaper);
						}
					}
				}
				this.PaperName = Utils.Paper2Str(this.Paper);
			}
			this.PaperWidth = Utils.GetAttrInt(rootNode, "PaperWidth", this.PaperWidth);
			this.PaperHeight = Utils.GetAttrInt(rootNode, "PaperHeight", this.PaperHeight);
			bool flag8 = this.Paper != 0;
			if (flag8)
			{
				RdPaper curPaper = Utils.FindPaper(this.Paper);
				bool flag9 = curPaper == null || Math.Abs(this.PaperWidth - curPaper.Width) > 500 || Math.Abs(this.PaperHeight - curPaper.Height) > 500;
				if (flag9)
				{
					this.PaperName = "";
					this.Paper = 0;
				}
				else
				{
					this.PaperWidth = curPaper.Width;
					this.PaperHeight = curPaper.Height;
				}
			}
			this.PaperOrientation = Utils.Str2PaperOrientation(Utils.GetXmlNodeAttribute(rootNode, "PaperOrientation"), this.PaperOrientation);
			this.PageLeftMargin = Utils.GetAttrInt(rootNode, "PageLeftMargin", this.PageLeftMargin);
			this.PageTopMargin = Utils.GetAttrInt(rootNode, "PageTopMargin", this.PageTopMargin);
			this.PageRightMargin = Utils.GetAttrInt(rootNode, "PageRightMargin", this.PageRightMargin);
			this.PageBottomMargin = Utils.GetAttrInt(rootNode, "PageBottomMargin", this.PageBottomMargin);
			this.PageHAlignment = Utils.Str2HAlignment(Utils.GetXmlNodeAttribute(rootNode, "PageHAlignment"), this.PageHAlignment);
			this.PageVAlignment = Utils.Str2VAlignment(Utils.GetXmlNodeAttribute(rootNode, "PageVAlignment"), this.PageVAlignment);
			this.HorzCompress = Utils.GetAttrBool(rootNode, "HorzCompress", this.HorzCompress);
			this.VertCompress = Utils.GetAttrBool(rootNode, "VertCompress", this.VertCompress);
			this.PageHeader = Utils.GetAttrString(rootNode, "PageHeader", this.PageHeader);
			this.PageHeaderPos = Utils.GetAttrInt(rootNode, "PageHeaderPos", this.PageHeaderPos);
			this.PageHeaderLine = Utils.GetAttrBool(rootNode, "PageHeaderLine", this.PageHeaderLine);
			this.NoPageHeaderInPageOne = Utils.GetAttrBool(rootNode, "NoPageHeaderInPageOne", this.NoPageHeaderInPageOne);
			this.PageHeaderFont = Utils.GetAttrString(rootNode, "PageHeaderFont", this.PageHeaderFont);
			this.PageFooter = Utils.GetAttrString(rootNode, "PageFooter", this.PageFooter);
			this.PageFooterPos = Utils.GetAttrInt(rootNode, "PageFooterPos", this.PageFooterPos);
			this.PageFooterLine = Utils.GetAttrBool(rootNode, "PageFooterLine", this.PageFooterLine);
			this.NoPageFooterInPageOne = Utils.GetAttrBool(rootNode, "NoPageFooterInPageOne", this.NoPageFooterInPageOne);
			this.PageFooterFont = Utils.GetAttrString(rootNode, "PageFooterFont", this.PageFooterFont);
			this.PageLeftRemark = Utils.GetAttrString(rootNode, "PageLeftRemark", this.PageLeftRemark);
			this.PageLeftRemarkPos = Utils.GetAttrInt(rootNode, "PageLeftRemarkPos", this.PageLeftRemarkPos);
			this.PageLeftRemarkLine = Utils.GetAttrBool(rootNode, "PageLeftRemarkLine", this.PageLeftRemarkLine);
			this.NoPageLeftRemarkInPageOne = Utils.GetAttrBool(rootNode, "NoPageLeftRemarkInPageOne", this.NoPageLeftRemarkInPageOne);
			this.PageLeftRemarkFont = Utils.GetAttrString(rootNode, "PageLeftRemarkFont", this.PageLeftRemarkFont);
			this.PageRightRemark = Utils.GetAttrString(rootNode, "PageRightRemark", this.PageRightRemark);
			this.PageRightRemarkPos = Utils.GetAttrInt(rootNode, "PageRightRemarkPos", this.PageRightRemarkPos);
			this.PageRightRemarkLine = Utils.GetAttrBool(rootNode, "PageRightRemarkLine", this.PageRightRemarkLine);
			this.NoPageRightRemarkInPageOne = Utils.GetAttrBool(rootNode, "NoPageRightRemarkInPageOne", this.NoPageRightRemarkInPageOne);
			this.PageRightRemarkFont = Utils.GetAttrString(rootNode, "PageRightRemarkFont", this.PageRightRemarkFont);
			this.RowsPerPage = Utils.GetAttrInt(rootNode, "RowsPerPage", this.RowsPerPage);
			this.FullDraw = Utils.GetAttrBool(rootNode, "FullDraw", this.FullDraw);
			this.StartPageNo = Utils.GetAttrInt(rootNode, "StartPageNo", this.StartPageNo);
			this.HeaderVisible = Utils.GetAttrBool(rootNode, "HeaderVisible", this.HeaderVisible);
			this.GridLineVisible = Utils.GetAttrBool(rootNode, "GridLineVisible", this.GridLineVisible);
			this.MultiCols = Utils.GetAttrBool(rootNode, "MultiCols", this.MultiCols);
			this.AutoCalc = Utils.GetAttrBool(rootNode, "AutoCalc", this.AutoCalc);
			this.ShowFormula = Utils.GetAttrBool(rootNode, "ShowFormula", this.ShowFormula);
			this.ExtendLastRow = Utils.GetAttrBool(rootNode, "ExtendLastRow", this.ExtendLastRow);
		}

		private void LoadDataSets(XmlNode rootNode)
		{
			XmlNodeList nodeList = rootNode.SelectNodes("DataSets");
			bool flag = nodeList != null && nodeList.Count > 0;
			if (flag)
			{
				this.DataSets.Load(nodeList[0]);
			}
		}

		private void LoadVariables(XmlNode rootNode)
		{
			XmlNodeList nodeList = rootNode.SelectNodes("Variables");
			bool flag = nodeList != null && nodeList.Count > 0;
			if (flag)
			{
				this.Variables.Load(nodeList[0]);
			}
		}

		private void LoadAliasList(XmlNode rootNode)
		{
			XmlNodeList nodeList = rootNode.SelectNodes("AliasList");
			bool flag = nodeList != null && nodeList.Count > 0;
			if (flag)
			{
				this.AliasList.Load(nodeList[0]);
			}
		}

		private void LoadImages(XmlNode rootNode)
		{
			XmlNodeList nodeList = rootNode.SelectNodes("Images");
			bool flag = nodeList != null && nodeList.Count > 0;
			if (flag)
			{
				this.Images.Load(nodeList[0]);
			}
		}

		private void LoadRows(XmlNode rootNode)
		{
			XmlNodeList nodeList = rootNode.SelectNodes("Rows");
			bool flag = nodeList != null && nodeList.Count > 0;
			if (flag)
			{
				this.Rows.Load(nodeList[0]);
			}
		}

		private void LoadColumns(XmlNode rootNode)
		{
			XmlNodeList nodeList = rootNode.SelectNodes("Columns");
			bool flag = nodeList != null && nodeList.Count > 0;
			if (flag)
			{
				this.Columns.Load(nodeList[0]);
			}
		}

		private void LoadCells(XmlNode rootNode)
		{
			XmlNodeList nodeList = rootNode.SelectNodes("Cells");
			RdCells _cells = new RdCells(this);
			bool flag = nodeList != null && nodeList.Count > 0;
			if (flag)
			{
				_cells.Load(nodeList[0]);
			}
		}

		private void LoadDefaultValue()
		{
		}

		internal void CopyReport(ref RdDocument R)
		{
			RdMergeCellsStyle tmpStyle = new RdMergeCellsStyle();
			Utils.CopyStyleRec(ref tmpStyle, this.DefaultStyle);
			R.DefaultStyle = tmpStyle;
			R.Name = this.Name;
			R.Paper = this.Paper;
			bool flag = R.Paper == 0;
			if (flag)
			{
				R.PaperWidth = this.PaperWidth;
				R.PaperHeight = this.PaperHeight;
			}
			R.PaperOrientation = this.PaperOrientation;
			R.PageLeftMargin = this.PageLeftMargin;
			R.PageRightMargin = this.PageRightMargin;
			R.PageTopMargin = this.PageTopMargin;
			R.PageBottomMargin = this.PageBottomMargin;
			R.PageHAlignment = this.PageHAlignment;
			R.PageVAlignment = this.PageVAlignment;
			R.HorzCompress = this.HorzCompress;
			R.VertCompress = this.VertCompress;
			R.PageHeaderPos = this.PageHeaderPos;
			R.PageHeader = this.PageHeader;
			R.PageHeaderLine = this.PageHeaderLine;
			R.NoPageHeaderInPageOne = this.NoPageHeaderInPageOne;
			R.PageHeaderFont = this.PageHeaderFont;
			R.PageFooterPos = this.PageFooterPos;
			R.PageFooter = this.PageFooter;
			R.PageFooterLine = this.PageFooterLine;
			R.NoPageFooterInPageOne = this.NoPageFooterInPageOne;
			R.PageFooterFont = this.PageFooterFont;
			R.PageLeftRemarkPos = this.PageLeftRemarkPos;
			R.PageLeftRemark = this.PageLeftRemark;
			R.PageLeftRemarkLine = this.PageLeftRemarkLine;
			R.NoPageLeftRemarkInPageOne = this.NoPageLeftRemarkInPageOne;
			R.PageLeftRemarkFont = this.PageLeftRemarkFont;
			R.PageRightRemarkPos = this.PageRightRemarkPos;
			R.PageRightRemark = this.PageRightRemark;
			R.PageRightRemarkLine = this.PageRightRemarkLine;
			R.NoPageRightRemarkInPageOne = this.NoPageRightRemarkInPageOne;
			R.PageRightRemarkFont = this.PageRightRemarkFont;
			R.RowsPerPage = this.RowsPerPage;
			R.FullDraw = this.FullDraw;
			R.ExtendLastRow = this.ExtendLastRow;
			R.StartPageNo = this.StartPageNo;
			R.HeaderVisible = this.HeaderVisible;
			R.GridLineVisible = this.GridLineVisible;
			R.MultiCols = this.MultiCols;
			R.AutoCalc = this.AutoCalc;
			R.ShowFormula = this.ShowFormula;
			R.FixedRows = this.FixedRows;
			R.FixedColumns = this.FixedColumns;
			foreach (KeyValuePair<string, RdImage> obj in this.Images.List)
			{
				RdImage CurImage = obj.Value;
				RdImage CopyImage = new RdImage(R);
				CopyImage.Name = CurImage.Name;
				CopyImage.Left = CurImage.Left;
				CopyImage.Top = CurImage.Top;
				CopyImage.Right = CurImage.Right;
				CopyImage.Bottom = CurImage.Bottom;
				CopyImage.Width = CurImage.Width;
				CopyImage.Height = CurImage.Height;
				CopyImage.Print = CurImage.Print;
				CopyImage.Preview = CurImage.Preview;
				CopyImage.Type = CurImage.Type;
				CopyImage.Graphic = CurImage.Graphic;
				CopyImage.HAlignment = CurImage.HAlignment;
				CopyImage.VAlignment = CurImage.VAlignment;
				CopyImage.Transparent = CurImage.Transparent;
				CopyImage.ImageControl = CurImage.ImageControl;
				CopyImage.XDPI = CurImage.XDPI;
				CopyImage.YDPI = CurImage.YDPI;
				R.Images.List.Add(CopyImage.Name, CopyImage);
			}
		}

		internal void Join(int left, int top, int right, int bottom)
		{
			Rectangle rec = this.CreateCellRect(left, top, right, bottom);
			bool flag = rec.Left == rec.Right && rec.Top == rec.Bottom;
			if (!flag)
			{
				bool flag2 = rec.Right > this.Columns.ColumnCount;
				if (flag2)
				{
					this.Columns.ColumnCount = rec.Right;
				}
				bool flag3 = rec.Bottom > this.Rows.RowCount;
				if (flag3)
				{
					this.Rows.RowCount = rec.Bottom;
				}
				RdCell coverCell = this.GetCell(rec.Top, rec.Left);
				for (int rowIndex = rec.Top; rowIndex <= rec.Bottom; rowIndex++)
				{
					for (int columnIndex = rec.Left; columnIndex <= rec.Right; columnIndex++)
					{
						RdCell cell = this.GetCell(rowIndex, columnIndex);
						cell.Width = 1;
						cell.Height = 1;
						cell.HiddenBy = coverCell;
					}
				}
				coverCell.HiddenBy = null;
				coverCell.Width = rec.Right - rec.Left + 1;
				coverCell.Height = rec.Bottom - rec.Top + 1;
			}
		}

		internal Rectangle CreateCellRect(int left, int top, int right, int bottom)
		{
			Rectangle result = default(Rectangle);
			bool flag = left > right;
			if (flag)
			{
				int tmp = left;
				left = right;
				right = tmp;
			}
			bool flag2 = top > bottom;
			if (flag2)
			{
				int tmp = top;
				top = bottom;
				bottom = tmp;
			}
			bool flag3 = left < 1;
			if (flag3)
			{
				left = 1;
			}
			bool flag4 = top < 1;
			if (flag4)
			{
				top = 1;
			}
			bool flag5 = right < 1;
			if (flag5)
			{
				right = 1;
			}
			bool flag6 = bottom < 1;
			if (flag6)
			{
				bottom = 1;
			}
			result.X = left;
			result.Y = top;
			result.Width = right - left;
			result.Height = bottom - top;
			return result;
		}

		internal void RegisterFormulaCell(RdCell cell)
		{
			bool flag = this.FormulaCells.IndexOf(cell) == -1;
			if (flag)
			{
				this.FormulaCells.Add(cell);
			}
		}

		internal void UnregisterFormulaCell(RdCell cell)
		{
			this.FormulaCells.Remove(cell);
		}

		internal void ReCalcDoc()
		{
			List<RdCell> sortCells = new List<RdCell>();
			List<RdCell> chCells = new List<RdCell>();
			this.SortComputeOrder(sortCells);
			for (int i = 0; i < sortCells.Count; i++)
			{
				bool flag = sortCells[i].Formula.ToUpper().IndexOf("GENCHART") >= 0;
				if (flag)
				{
					chCells.Add(sortCells[i]);
				}
				else
				{
					this.Compute(sortCells[i]);
				}
			}
			for (int j = 0; j < chCells.Count; j++)
			{
				this.Compute(chCells[j]);
				chCells[j].Result = "";
			}
			this.RecalcRowOutputCondition();
		}

		private void Compute(RdCell cell)
		{
			bool flag = cell.AutoDataType != RdDataType.dtFormula;
			if (flag)
			{
				cell.Result = "";
			}
			else
			{
				this._computeCell = cell;
				this.CellExp.Expression = cell.Formula;
				try
				{
					cell.Result = this.CellExp.Value.ToString();
				}
				catch (ReportingException repExp)
				{
					cell.Result = repExp.Message;
				}
			}
		}

		private void SortComputeOrder(List<RdCell> sortCells)
		{
			for (int i = 0; i < this.FormulaCells.Count; i++)
			{
				RdCell cell = this.FormulaCells[i];
				bool flag = !this.InsertIntoComputeOrder(sortCells, cell);
				if (flag)
				{
					cell.SetDataType(RdDataType.dtString);
					cell.Result = string.Format("单元格{0}的公式中包含对其自身的循环引用！", Utils.EncodeCellId(cell.Row, cell.Column));
				}
			}
		}

		private bool InsertIntoComputeOrder(List<RdCell> sortCells, RdCell cell)
		{
			bool result = true;
			bool flag = cell.AffectedBy.Count == 0;
			bool result2;
			if (flag)
			{
				sortCells.Insert(0, cell);
				result2 = true;
			}
			else
			{
				int pos;
				for (pos = 0; pos < sortCells.Count; pos++)
				{
					bool flag2 = this.IsAffectedBy(sortCells[pos], cell);
					if (flag2)
					{
						break;
					}
				}
				sortCells.Insert(pos, cell);
				List<RdCell> movedCells = new List<RdCell>();
				movedCells.Add(cell);
				for (int i = 0; i < movedCells.Count; i++)
				{
					pos = sortCells.IndexOf(movedCells[i]);
					for (int j = pos + 1; j < sortCells.Count; j++)
					{
						bool flag3 = this.IsAffectedBy(movedCells[i], sortCells[j]);
						if (flag3)
						{
							bool flag4 = movedCells.IndexOf(sortCells[j]) == -1;
							if (flag4)
							{
								movedCells.Add(sortCells[j]);
							}
							else
							{
								result = false;
							}
							RdCell tmpCell = sortCells[j];
							sortCells.Insert(pos, tmpCell);
							sortCells.RemoveAt(j + 1);
							pos++;
						}
					}
				}
				result2 = result;
			}
			return result2;
		}

		private bool IsAffectedBy(RdCell cell1, RdCell cell2)
		{
			int C2;
			int R2;
			int R;
			int C = R = (R2 = (C2 = 0));
			bool result = false;
			for (int i = 0; i < cell1.AffectedBy.Count; i++)
			{
				bool flag = cell1.AffectedBy[i].IndexOf(':') >= 0;
				if (flag)
				{
					Utils.DecodeRangeId(cell1.AffectedBy[i], ref C, ref R, ref C2, ref R2);
					bool flag2 = cell2.Row >= R && cell2.Row <= R2 && cell2.Column >= C && cell2.Column <= C2;
					if (flag2)
					{
						result = true;
						break;
					}
				}
				else
				{
					Utils.DecodeCellId(cell1.AffectedBy[i], ref R, ref C);
					bool flag3 = cell2.Row == R && cell2.Column == C;
					if (flag3)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		private void RecalcRowOutputCondition()
		{
			for (int i = 1; i <= this.Rows.RowCount; i++)
			{
				RdRow curRow = this.Rows[i];
				bool flag = !string.IsNullOrEmpty(curRow.OutputCondition) && curRow.OutputCondition.IndexOf('@') == -1;
				if (flag)
				{
					this.CellExp.Expression = curRow.OutputCondition;
					try
					{
						object ExpResult = this.CellExp.Value;
						bool flag2 = ExpResult is int || ExpResult is float || ExpResult is double;
						if (flag2)
						{
							curRow.OutputConditionResult = ExpResult.ToString();
						}
						else
						{
							bool flag3 = ExpResult is string;
							if (flag3)
							{
								curRow.OutputConditionResult = ExpResult.ToString();
								bool flag4 = string.IsNullOrEmpty(curRow.OutputConditionResult);
								if (flag4)
								{
									curRow.OutputConditionResult = "0";
								}
							}
							else
							{
								curRow.OutputConditionResult = "0";
							}
						}
					}
					catch
					{
						curRow.OutputConditionResult = "0";
					}
				}
			}
		}
	}
}
