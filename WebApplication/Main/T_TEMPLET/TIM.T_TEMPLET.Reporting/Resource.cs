using System;

namespace TIM.T_TEMPLET.Reporting
{
	internal class Resource
	{
		public const float ReportingVersion = 1f;

		public const string ReportingDefaultName = "报表";

		public const string ReportingDefaultFontFamily = "宋体;9;#000000;134";

		public const string ReportingDefaultFontName = "宋体";

		public const string ReportingDefaultRowName = "行[{0}]";

		public const string ReportingDefaultColumnName = "列[{0}]";

		public const string ReportingParseError = "报表样式无法解析！";

		public const string ReportingDefaultShortDateFormat = "yyyy-MM-dd";

		public const string ReportingDefaultTimeFormat = "HH:mm:ss";

		public const string ReportingDefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

		public const RdRowType CDefaultRowType = RdRowType.rtDetailData;

		public const RdColumnType CDefaultColumnType = RdColumnType.ctDetailData;

		public const int CDefaultPaper = 9;

		public const string CDefaultPaperName = "A4";

		public const RdPaperOrientation CDefaultPaperOrientation = RdPaperOrientation.roPortrait;

		public const int CDefaultPageLeftMargin = 748;

		public const int CDefaultPageTopMargin = 984;

		public const int CDefaultPageRightMargin = 748;

		public const int CDefaultPageBottomMargin = 984;

		public const RdHAlignment CDefaultPageHAlignment = RdHAlignment.haLeft;

		public const RdVAlignment CDefaultPageVAlignment = RdVAlignment.vaTop;

		public const bool CDefaultHorzCompress = false;

		public const bool CDefaultVertCompress = false;

		public const int CDefaultPageHeaderPos = 591;

		public const int CDefaultPageFooterPos = 591;

		public const int CDefaultPageLeftRemarkPos = 591;

		public const int CDefaultPageRightRemarkPos = 591;

		public const bool CDefaultPageHeaderLine = false;

		public const bool CDefaultPageFooterLine = false;

		public const bool CDefaultPageLeftRemarkLine = false;

		public const bool CDefaultPageRightRemarkLine = false;

		public const bool CDefaultNoPageHeaderInPageOne = false;

		public const bool CDefaultNoPageFooterInPageOne = false;

		public const bool CDefaultNoPageLeftRemarkInPageOne = false;

		public const bool CDefaultNoPageRightRemarkInPageOne = false;

		public const int CDefaultRowsPerPage = 0;

		public const bool CDefaultFullDraw = false;

		public const bool CDefaultExtendLastRow = false;

		public const int CDefaultStartPageNo = 1;

		public const bool CDefaultHeaderVisible = true;

		public const bool CDefaultGridLineVisible = true;

		public const bool CDefaultMultiCols = false;

		public const bool CDefaultAutoCalc = false;

		public const bool CDefaultShowFormula = true;

		public const RdDataType CDefaultCellDataType = RdDataType.dtAuto;

		public const int CDefaultCellFontSize = 9;

		public const int CDefaultCellFontColor = 0;

		public const bool CDefaultCellFontBold = false;

		public const bool CDefaultCellFontItalic = false;

		public const bool CDefaultCellFontUnderline = false;

		public const bool CDefaultCellFontStrikeout = false;

		public const string CDefaultCellDisplayFormat = "";

		public const bool CDefaultCellLeftBorder = true;

		public const RdLineStyle CDefaultCellLeftBorderStyle = RdLineStyle.lsThinSolid;

		public const int CDefaultCellLeftBorderColor = 0;

		public const int CDefaultCellLeftBorderWidth = 0;

		public const bool CDefaultCellTopBorder = true;

		public const RdLineStyle CDefaultCellTopBorderStyle = RdLineStyle.lsThinSolid;

		public const int CDefaultCellTopBorderColor = 0;

		public const int CDefaultCellTopBorderWidth = 0;

		public const bool CDefaultCellRightBorder = true;

		public const RdLineStyle CDefaultCellRightBorderStyle = RdLineStyle.lsThinSolid;

		public const int CDefaultCellRightBorderColor = 0;

		public const int CDefaultCellRightBorderWidth = 0;

		public const bool CDefaultCellBottomBorder = true;

		public const RdLineStyle CDefaultCellBottomBorderStyle = RdLineStyle.lsThinSolid;

		public const int CDefaultCellBottomBorderColor = 0;

		public const int CDefaultCellBottomBorderWidth = 0;

		public const RdLineStyle CDefaultCellDiagLT2RBStyle = RdLineStyle.lsNone;

		public const int CDefaultCellDiagLT2RBColor = 0;

		public const int CDefaultCellDiagLT2RBWidth = 0;

		public const RdLineStyle CDefaultCellDiagLB2RTStyle = RdLineStyle.lsNone;

		public const int CDefaultCellDiagLB2RTColor = 0;

		public const int CDefaultCellDiagLB2RTWidth = 0;

		public const bool CDefaultCellThreePartText = false;

		public const int CDefaultCellColor = 16777215;

		public const bool CDefaultCellTransparent = false;

		public const int CDefaultCellPattern = 1;

		public const int CDefaultCellPatternColor = 0;

		public const RdHAlignment CDefaultCellHAlignment = RdHAlignment.haAuto;

		public const RdVAlignment CDefaultCellVAlignment = RdVAlignment.vaCenter;

		public const int CDefaultCellLeftMargin = 39;

		public const int CDefaultCellRightMargin = 39;

		public const int CDefaultCellTopMargin = 39;

		public const int CDefaultCellBottomMargin = 39;

		public const RdTextControl CDefaultCellTextControl = RdTextControl.tcCut;

		public const int CDefaultCellLineSpace = 1;

		public const bool CDefaultCellLocked = false;

		public const bool CDefaultCellPreview = true;

		public const bool CDefaultCellPrint = true;

		public const bool CDefaultCellSmallFontWordWrap = false;

		public const bool CDefaultRowAutoHeight = true;

		public const string CTagReport = "Report";

		public const string CTagDataSets = "DataSets";

		public const string CTagDataSet = "DataSet";

		public const string CTagVariables = "Variables";

		public const string CTagVariable = "Variable";

		public const string CTagAliasList = "AliasList";

		public const string CTagAlias = "Alias";

		public const string CTagImages = "Images";

		public const string CTagImage = "Image";

		public const string CTagRows = "Rows";

		public const string CTagRow = "Row";

		public const string CTagColumns = "Columns";

		public const string CTagColumn = "Column";

		public const string CTagCells = "Cells";

		public const string CTagStyle = "Style";

		public const string CTagCell = "Cell";

		public const string CTagRegion = "Region";

		public const string CAttrName = "Name";

		public const string CAttrCreatedTime = "Created";

		public const string CAttrCreatedBy = "CreatedBy";

		public const string CAttrModifiedTime = "Modified";

		public const string CAttrModifiedBy = "ModifiedBy";

		public const string CAttrTemplate = "Template";

		public const string CAttrRowCount = "RowCount";

		public const string CAttrColumnCount = "ColumnCount";

		public const string CAttrPaperName = "PaperName";

		public const string CAttrPaper = "Paper";

		public const string CAttrPaperWidth = "PaperWidth";

		public const string CAttrPaperHeight = "PaperHeight";

		public const string CAttrPaperOrientation = "PaperOrientation";

		public const string CAttrPageLeftMargin = "PageLeftMargin";

		public const string CAttrPageTopMargin = "PageTopMargin";

		public const string CAttrPageRightMargin = "PageRightMargin";

		public const string CAttrPageBottomMargin = "PageBottomMargin";

		public const string CAttrPageHAlignment = "PageHAlignment";

		public const string CAttrPageVAlignment = "PageVAlignment";

		public const string CAttrHorzCompress = "HorzCompress";

		public const string CAttrVertCompress = "VertCompress";

		public const string CAttrPageHeader = "PageHeader";

		public const string CAttrPageHeaderPos = "PageHeaderPos";

		public const string CAttrPageHeaderLine = "PageHeaderLine";

		public const string CAttrNoPageHeaderInPageOne = "NoPageHeaderInPageOne";

		public const string CAttrPageHeaderFont = "PageHeaderFont";

		public const string CAttrPageFooter = "PageFooter";

		public const string CAttrPageFooterPos = "PageFooterPos";

		public const string CAttrPageFooterLine = "PageFooterLine";

		public const string CAttrNoPageFooterInPageOne = "NoPageFooterInPageOne";

		public const string CAttrPageFooterFont = "PageFooterFont";

		public const string CAttrPageLeftRemark = "PageLeftRemark";

		public const string CAttrPageLeftRemarkPos = "PageLeftRemarkPos";

		public const string CAttrPageLeftRemarkLine = "PageLeftRemarkLine";

		public const string CAttrNoPageLeftRemarkInPageOne = "NoPageLeftRemarkInPageOne";

		public const string CAttrPageLeftRemarkFont = "PageLeftRemarkFont";

		public const string CAttrPageRightRemark = "PageRightRemark";

		public const string CAttrPageRightRemarkPos = "PageRightRemarkPos";

		public const string CAttrPageRightRemarkLine = "PageRightRemarkLine";

		public const string CAttrNoPageRightRemarkInPageOne = "NoPageRightRemarkInPageOne";

		public const string CAttrPageRightRemarkFont = "PageRightRemarkFont";

		public const string CAttrVersion = "Version";

		public const string CAttrAutoCalc = "AutoCalc";

		public const string CAttrShowFormula = "ShowFormula";

		public const string CAttrGeneratedBy = "GB";

		public const string CAttrSQL = "SQL";

		public const string CAttrExtendLastRow = "ExtendLastRow";

		public const string CAttrRow = "Row";

		public const string CAttrColumn = "Column";

		public const string CAttrHeight = "Height";

		public const string CAttrWidth = "Width";

		public const string CAttrRowType = "RowType";

		public const string CAttrColumnType = "ColumnType";

		public const string CAttrDataType = "Type";

		public const string CAttrValue = "Value";

		public const string CAttrTag = "Tag";

		public const string CAttrFont = "Font";

		public const string CAttrFontName = "FontName";

		public const string CAttrFontSize = "FontSize";

		public const string CAttrFontColor = "FontColor";

		public const string CAttrFontBold = "FontBold";

		public const string CAttrFontItalic = "FontItalic";

		public const string CAttrFontUnderline = "FontUnderline";

		public const string CAttrFontStrikeout = "FontStrikeout";

		public const string CAttrColor = "Color";

		public const string CAttrDisplayFormat = "DisplayFormat";

		public const string CAttrHAlignment = "HAlignment";

		public const string CAttrVAlignment = "VAlignment";

		public const string CAttrCellBorders = "Border";

		public const string CAttrLeftBorder = "LeftBorder";

		public const string CAttrLeftBorderStyle = "LeftBorderStyle";

		public const string CAttrLeftBorderColor = "LeftBorderColor";

		public const string CAttrLeftBorderWidth = "LeftBorderWidth";

		public const string CAttrTopBorder = "TopBorder";

		public const string CAttrTopBorderStyle = "TopBorderStyle";

		public const string CAttrTopBorderColor = "TopBorderColor";

		public const string CAttrTopBorderWidth = "TopBorderWidth";

		public const string CAttrRightBorder = "RightBorder";

		public const string CAttrRightBorderStyle = "RightBorderStyle";

		public const string CAttrRightBorderColor = "RightBorderColor";

		public const string CAttrRightBorderWidth = "RightBorderWidth";

		public const string CAttrBottomBorder = "BottomBorder";

		public const string CAttrBottomBorderStyle = "BottomBorderStyle";

		public const string CAttrBottomBorderColor = "BottomBorderColor";

		public const string CAttrBottomBorderWidth = "BottomBorderWidth";

		public const string CAttrDiagLT2RBStyle = "DiagLT2RBStyle";

		public const string CAttrDiagLT2RBColor = "DiagLT2RBColor";

		public const string CAttrDiagLT2RBWidth = "DiagLT2RBWidth";

		public const string CAttrDiagLB2RTStyle = "DiagLB2RTStyle";

		public const string CAttrDiagLB2RTColor = "DiagLB2RTColor";

		public const string CAttrDiagLB2RTWidth = "DiagLB2RTWidth";

		public const string CAttrLeftMargin = "LeftMargin";

		public const string CAttrTopMargin = "TopMargin";

		public const string CAttrRightMargin = "RightMargin";

		public const string CAttrBottomMargin = "BottomMargin";

		public const string CAttrThreePartText = "ThreePartText";

		public const string CAttrFixedRows = "FixedRows";

		public const string CAttrFixedColumns = "FixedColumns";

		public const string CAttrTransparent = "Transparent";

		public const string CAttrPattern = "Pattern";

		public const string CAttrPatternColor = "PatternColor";

		public const string CAttrStyle = "Style";

		public const string CAttrDefinedStyles = "Defined";

		public const string CAttrEmptyCells = "EmptyCells";

		public const string CAttrDataSet = "DataSet";

		public const string CAttrFieldCount = "FieldCount";

		public const string CAttrField = "Field";

		public const string CAttrComment = "Comment";

		public const string CAttrTextControl = "TextControl";

		public const string CAttrLineSpace = "LineSpace";

		public const string CAttrLocked = "Locked";

		public const string CAttrSmallFontWordWrap = "SmallFontWordWrap";

		public const string CAttrPreview = "Preview";

		public const string CAttrPrint = "Print";

		public const string CAttrLeft = "Left";

		public const string CAttrTop = "Top";

		public const string CAttrRight = "Right";

		public const string CAttrBottom = "Bottom";

		public const string CAttrAutoHeight = "AutoHeight";

		public const string CAttrPageBreak = "PageBreak";

		public const string CAttrPageRows = "PageRows";

		public const string CAttrPageColumns = "PageColumns";

		public const string CAttrVarType = "VarType";

		public const string CAttrRowsPerPage = "RowsPerPage";

		public const string CAttrFullDraw = "FullDraw";

		public const string CAttrRegionType = "RegionType";

		public const string CAttrGraphic = "Graphic";

		public const string CAttrImageControl = "ImageControl";

		public const string CAttrImageType = "ImageType";

		public const string CAttrXDPI = "XDPI";

		public const string CAttrYDPI = "YDPI";

		public const string CAttrStartPageNo = "StartPageNo";

		public const string CAttrHeaderVisible = "HeaderVisible";

		public const string CAttrGridLineVisible = "GridLineVisible";

		public const string CAttrMultiCols = "MultiCols";

		public const string CAttrSingleRecord = "SingleRecord";

		public const string CAttrOutputLines = "OutputLines";

		public const string CAttrForceNewPage = "ForceNewPage";

		public const string CAttrFillWithBlank = "FillWithBlank";

		public const string CAttrResult = "Result";

		public const string CAttrOutputCondition = "OutputCondition";

		public const string CAttrOutputConditionResult = "OutputConditionResult";

		public const string CAttrGroupBy = "GroupBy";

		public const string CAttrCanBreakTwoPage = "CanBreakTwoPage";

		public const string CAttrFormula = "Formula";

		public const string CMsgUnknownVariable = "未知变量：{0}";

		public const string CMsgUnknownFunction = "未知函数：{0}";
	}
}
