using System;
using System.Drawing;

namespace TIM.T_TEMPLET.Reporting
{
	public class UtoReport
	{
		public static string ReportDifference(string reportXml, string priorReportXml)
		{
			string result = string.Empty;
			RdDocument repPrior = new RdDocument();
			RdDocument repCurrent = new RdDocument();
			repPrior.SetReport(priorReportXml);
			repCurrent.SetReport(reportXml);
			for (int rowIndex = 1; rowIndex <= repCurrent.Rows.RowCount; rowIndex++)
			{
				for (int columnIndex = 1; columnIndex <= repCurrent.Columns.ColumnCount; columnIndex++)
				{
					RdCell cellPrior = repPrior.GetCell(rowIndex, columnIndex);
					RdCell cellCurrent = repCurrent.GetCell(rowIndex, columnIndex);
					bool flag = cellPrior == null || cellPrior.GetAsString() != cellCurrent.GetAsString();
					if (flag)
					{
						cellCurrent.SetColor(Color.Red);
					}
				}
			}
			return repCurrent.GetReport();
		}

		public static string CalculateReportFile(string reportStyle, RdDocument.GtReportPubGetValue onUnknownFunctionAndVariable)
		{
			string result = string.Empty;
			RdDocument doc = new RdDocument();
			doc.OnGtReportPubGetValue += onUnknownFunctionAndVariable;
			doc.SetReport(reportStyle);
			doc.ShowFormula = false;
			doc.ReCalcDoc();
			return doc.GetReport();
		}

		public static string GetReportCellResult(string reportXml, int rowIndex, int columnIndex)
		{
			string result = string.Empty;
			RdDocument doc = new RdDocument();
			doc.SetReport(reportXml);
			RdCell cell = doc.GetCell(rowIndex, columnIndex);
			bool flag = cell != null;
			if (flag)
			{
				bool flag2 = cell.AutoDataType == RdDataType.dtFormula;
				if (flag2)
				{
					result = cell.GetAsText();
				}
				else
				{
					result = cell.GetAsString();
				}
			}
			return result;
		}

		public static string GenerateTemplete(string rawXml)
		{
			return RmReportingMaker.GetDefaultTemplate(rawXml);
		}

		public static string GenerateReport(string reportXml, RmReportingMaker.GtReportPubGetValue onUnknownFunctionAndVariable)
		{
			RmReportingMaker builder = new RmReportingMaker();
			builder.OnGtReportPubGetValue += onUnknownFunctionAndVariable;
			builder.SetTemplate(reportXml);
			return builder.Generate();
		}
	}
}
