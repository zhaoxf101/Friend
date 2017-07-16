using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using TIM.T_KERNEL.Common;
using TIM.T_WEBCTRL;
using TIM.T_ZIPLIB;

namespace TIM.T_TEMPLET.Utils
{
	public class ExcelUtils
	{
		public const int ExportRecords = 20000;

		private const int RowsPerFile = 20000;

		private const int XlsColumnCount = 256;

		private static void SetSummaryInformation(HSSFWorkbook workbook)
		{
			DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
			dsi.Company = "源悦科技";
			workbook.DocumentSummaryInformation = dsi;
			SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
			si.Author = "源悦科技";
			si.RemoveApplicationName();
			si.ApplicationName = "Excel Export";
			si.LastAuthor = "源悦科技";
			si.Comments = "";
			si.Title = "";
			si.Subject = "";
			si.CreateDateTime = new DateTime?(DateTime.Now);
			workbook.SummaryInformation = si;
		}

		private static string WriteExcelFile(IWorkbook workbook, string fileName)
		{
			string result = string.Empty;
			bool flag = string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(fileName.Trim());
			if (flag)
			{
				result = Guid.NewGuid().ToString() + ".xls";
			}
			else
			{
				result = fileName;
			}
			string filePath = RunDirectory.LinkTempPath(result);
			bool flag2 = File.Exists(filePath);
			if (flag2)
			{
				File.Delete(filePath);
			}
			FileStream file = new FileStream(filePath, FileMode.Create);
			workbook.Write(file);
			file.Close();
			workbook = null;
			return result;
		}

		private static string ZipXlsFiles(List<string> fileList)
		{
			string zipFileName = Guid.NewGuid().ToString() + ".zip";
			string zipFilePath = RunDirectory.LinkTempPath(zipFileName);
			ZipCompress zip = new ZipCompress();
			zip.BeginZip(zipFilePath);
			try
			{
				foreach (string xlsFileName in fileList)
				{
					string filePath = RunDirectory.LinkTempPath(xlsFileName);
					try
					{
						zip.AddFileToZip(filePath, xlsFileName);
					}
					finally
					{
						File.Delete(filePath);
					}
				}
			}
			finally
			{
				zip.FinishZip();
			}
			return zipFileName;
		}

        internal static uint ComputeStringHash(string s)
        {
            uint num = 0;
            if (s != null)
            {
                num = 0x811c9dc5;
                for (int i = 0; i < s.Length; i++)
                {
                    num = (s[i] ^ num) * 0x1000193;
                }
            }
            return num;
        }

        private static string Export(DataTable dt, string fileName)
		{
			HSSFWorkbook workbook = new HSSFWorkbook();
			ExcelUtils.SetSummaryInformation(workbook);
			IDataFormat format = workbook.CreateDataFormat();
			ICellStyle cellStyleDefault = workbook.CreateCellStyle();
			ICellStyle cellStyleDate = workbook.CreateCellStyle();
			ICellStyle cellStyleDateTime = workbook.CreateCellStyle();
			ICellStyle cellStyleNumber = workbook.CreateCellStyle();
			IFont font = workbook.CreateFont();
			font.FontHeightInPoints = 9;
			font.FontName = "宋体";
			cellStyleDefault.SetFont(font);
			cellStyleDate.SetFont(font);
			cellStyleDateTime.SetFont(font);
			cellStyleNumber.SetFont(font);
			cellStyleDate.DataFormat = format.GetFormat("yyyy-MM-dd");
			cellStyleDateTime.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");
			cellStyleNumber.DataFormat = format.GetFormat("0.0000");
			int columnCount = dt.Columns.Count;
			int rowCount = dt.Rows.Count;
			ISheet sheet = workbook.CreateSheet("Sheet1");
			for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
			{
				sheet.SetColumnWidth(columnIndex, 5120);
			}
			for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
			{
				IRow row = sheet.CreateRow(rowIndex);
				int columnIndex2 = 0;
				while (columnIndex2 < columnCount)
				{
					DataColumn column = dt.Columns[columnIndex2];
					ICell cell = row.CreateCell(columnIndex2);
					string drValue = dt.Rows[rowIndex][columnIndex2].ToString();
					string text = column.DataType.ToString();
					uint num = ComputeStringHash(text);
					if (num <= 1697786220u)
					{
						if (num <= 531277785u)
						{
							if (num != 347085918u)
							{
								if (num != 531277785u)
								{
									goto IL_3A5;
								}
								if (!(text == "System.DBNull"))
								{
									goto IL_3A5;
								}
								cell.CellStyle = cellStyleDefault;
								cell.SetCellValue("");
							}
							else
							{
								if (!(text == "System.Boolean"))
								{
									goto IL_3A5;
								}
								bool boolV = false;
								bool.TryParse(drValue, out boolV);
								cell.CellStyle = cellStyleDefault;
								cell.SetCellValue(boolV);
							}
						}
						else if (num != 848225627u)
						{
							if (num != 1541528931u)
							{
								if (num != 1697786220u)
								{
									goto IL_3A5;
								}
								if (!(text == "System.Int16"))
								{
									goto IL_3A5;
								}
								goto IL_33F;
							}
							else
							{
								if (!(text == "System.DateTime"))
								{
									goto IL_3A5;
								}
								cell.CellStyle = cellStyleDateTime;
								cell.SetCellValue(drValue);
							}
						}
						else
						{
							if (!(text == "System.Double"))
							{
								goto IL_3A5;
							}
							goto IL_362;
						}
					}
					else if (num <= 1764058053u)
					{
						if (num != 1741144581u)
						{
							if (num != 1764058053u)
							{
								goto IL_3A5;
							}
							if (!(text == "System.Int64"))
							{
								goto IL_3A5;
							}
							goto IL_33F;
						}
						else
						{
							if (!(text == "System.Decimal"))
							{
								goto IL_3A5;
							}
							goto IL_362;
						}
					}
					else if (num != 3079944380u)
					{
						if (num != 4180476474u)
						{
							if (num != 4201364391u)
							{
								goto IL_3A5;
							}
							if (!(text == "System.String"))
							{
								goto IL_3A5;
							}
							cell.CellStyle = cellStyleDefault;
							cell.SetCellValue(drValue);
						}
						else
						{
							if (!(text == "System.Int32"))
							{
								goto IL_3A5;
							}
							goto IL_33F;
						}
					}
					else
					{
						if (!(text == "System.Byte"))
						{
							goto IL_3A5;
						}
						goto IL_33F;
					}
					IL_3BD:
					columnIndex2++;
					continue;
					IL_33F:
					int intV = 0;
					int.TryParse(drValue, out intV);
					cell.CellStyle = cellStyleDefault;
					cell.SetCellValue((double)intV);
					goto IL_3BD;
					IL_362:
					double doubV = 0.0;
					double.TryParse(drValue, out doubV);
					cell.CellStyle = cellStyleNumber;
					cell.SetCellValue(doubV);
					goto IL_3BD;
					IL_3A5:
					cell.CellStyle = cellStyleDefault;
					cell.SetCellValue("");
					goto IL_3BD;
				}
			}
			return ExcelUtils.WriteExcelFile(workbook, fileName);
		}

		private static void Export(DataSet ds, string fileName)
		{
		}

		public static string Export(TimGridView grid, string fileName)
		{
			return ExcelUtils.ExportMultiFile(grid, fileName);
		}

		private static string ExportMultiFile(TimGridView grid, string fileName)
		{
			string ret = string.Empty;
			List<string> fileList = new List<string>();
			int columnCount = (grid.Columns.Count > 256) ? 256 : grid.Columns.Count;
			int rowCount = grid.Rows.Count;
			int visibleColumnIndex = 0;
			bool flag = rowCount == 0;
			string result;
			if (flag)
			{
				HSSFWorkbook workbook = new HSSFWorkbook();
				ExcelUtils.SetSummaryInformation(workbook);
				IDataFormat format = workbook.CreateDataFormat();
				ICellStyle cellHeadStyleDefault = workbook.CreateCellStyle();
				IFont fontHead = workbook.CreateFont();
				fontHead.FontHeightInPoints = 9;
				fontHead.FontName = "宋体";
				cellHeadStyleDefault.SetFont(fontHead);
				cellHeadStyleDefault.Alignment = HorizontalAlignment.Center;
				cellHeadStyleDefault.FillForegroundColor = 22;
				cellHeadStyleDefault.FillPattern = FillPattern.SolidForeground;
				ISheet sheet = workbook.CreateSheet();
				IRow row = sheet.CreateRow(0);
				for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
				{
					BoundField Column = grid.Columns[columnIndex] as BoundField;
					bool flag2 = Column != null && Column.Visible;
					if (flag2)
					{
						sheet.SetColumnWidth(visibleColumnIndex, ExcelUtils.Pixel2ExcelWidth(Column.ItemStyle.Width.Value));
						ICell cell = row.CreateCell(visibleColumnIndex);
						cell.CellStyle = cellHeadStyleDefault;
						cell.SetCellValue(Column.HeaderText);
						visibleColumnIndex++;
					}
				}
				result = ExcelUtils.WriteExcelFile(workbook, fileName);
			}
			else
			{
				int filesCount = rowCount / 20000;
				bool flag3 = rowCount % 20000 != 0;
				if (flag3)
				{
					filesCount++;
				}
				for (int fileIndex = 0; fileIndex < filesCount; fileIndex++)
				{
					HSSFWorkbook workbook2 = new HSSFWorkbook();
					ExcelUtils.SetSummaryInformation(workbook2);
					IDataFormat format2 = workbook2.CreateDataFormat();
					ICellStyle cellHeadStyleDefault2 = workbook2.CreateCellStyle();
					ICellStyle cellStyleDefault = workbook2.CreateCellStyle();
					ICellStyle cellStyleBold = workbook2.CreateCellStyle();
					ICellStyle cellStyleDate = workbook2.CreateCellStyle();
					ICellStyle cellStyleDateTime = workbook2.CreateCellStyle();
					ICellStyle cellStyleNumber = workbook2.CreateCellStyle();
					IFont fontHead2 = workbook2.CreateFont();
					fontHead2.FontHeightInPoints = 10;
					fontHead2.FontName = "宋体";
					cellHeadStyleDefault2.SetFont(fontHead2);
					cellHeadStyleDefault2.Alignment = HorizontalAlignment.Center;
					cellHeadStyleDefault2.FillForegroundColor = 22;
					cellHeadStyleDefault2.FillPattern = FillPattern.SolidForeground;
					cellHeadStyleDefault2.VerticalAlignment = VerticalAlignment.Top;
					IFont font = workbook2.CreateFont();
					font.FontHeightInPoints = 9;
					font.FontName = "宋体";
					cellStyleDefault.SetFont(font);
					cellStyleDefault.WrapText = true;
					cellStyleDefault.VerticalAlignment = VerticalAlignment.Top;
					cellStyleDate.SetFont(font);
					cellStyleDateTime.SetFont(font);
					cellStyleNumber.SetFont(font);
					cellStyleNumber.Alignment = HorizontalAlignment.Right;
					cellStyleDate.DataFormat = format2.GetFormat("yyyy-MM-dd");
					cellStyleDateTime.DataFormat = format2.GetFormat("yyyy-MM-dd HH:mm:ss");
					cellStyleNumber.DataFormat = format2.GetFormat("0.0000");
					IFont fontBold = workbook2.CreateFont();
					fontBold.FontHeightInPoints = 10;
					fontBold.FontName = "宋体";
					fontBold.Boldweight = 700;
					cellStyleBold.SetFont(fontBold);
					cellStyleBold.WrapText = true;
					cellStyleBold.VerticalAlignment = VerticalAlignment.Top;
					ISheet sheet2 = workbook2.CreateSheet();
					visibleColumnIndex = 0;
					int visibleRowIndex = 1;
					bool UseCheckBox = grid.CheckBox;
					string drValue = string.Empty;
					IRow row2 = sheet2.CreateRow(0);
					for (int columnIndex2 = 0; columnIndex2 < columnCount; columnIndex2++)
					{
						TimBoundField Column2 = grid.Columns[columnIndex2] as TimBoundField;
						bool flag4 = Column2 != null && Column2.Visible;
						if (flag4)
						{
							sheet2.SetColumnWidth(visibleColumnIndex, ExcelUtils.Pixel2ExcelWidth(Column2.ItemStyle.Width.Value));
							ICell cell2 = row2.CreateCell(visibleColumnIndex);
							cell2.CellStyle = cellHeadStyleDefault2;
							cell2.SetCellValue(Column2.HeaderText);
							visibleColumnIndex++;
						}
					}
					int begin = fileIndex * 20000;
					int end = fileIndex * 20000 + 20000;
					end = ((end > rowCount) ? rowCount : end);
					for (int rowIndex = begin; rowIndex < end; rowIndex++)
					{
						bool flag5 = grid.Rows[rowIndex].RowType != DataControlRowType.DataRow;
						if (!flag5)
						{
							row2 = sheet2.CreateRow(visibleRowIndex);
							visibleColumnIndex = 0;
							for (int columnIndex3 = 0; columnIndex3 < columnCount; columnIndex3++)
							{
								TimBoundField Column2 = grid.Columns[columnIndex3] as TimBoundField;
								bool flag6 = Column2 != null && Column2.Visible;
								if (flag6)
								{
									ICell cell2 = row2.CreateCell(visibleColumnIndex);
									TableCell gvTableCell = grid.Rows[rowIndex].Cells[UseCheckBox ? (columnIndex3 + 1) : columnIndex3];
									drValue = gvTableCell.Text.ToString();
									BoundFieldMode editType = Column2.Mode;
									bool bold = gvTableCell.Font.Bold;
									if (bold)
									{
										ExcelUtils.SetCellValue(cellStyleBold, cellStyleDate, cellStyleDateTime, cellStyleNumber, cell2, drValue, editType);
									}
									else
									{
										ExcelUtils.SetCellValue(cellStyleDefault, cellStyleDate, cellStyleDateTime, cellStyleNumber, cell2, drValue, editType);
									}
									visibleColumnIndex++;
								}
							}
							visibleRowIndex++;
						}
					}
					ret = ExcelUtils.WriteExcelFile(workbook2, fileName);
					fileList.Add(ret);
				}
				bool flag7 = filesCount == 1;
				if (flag7)
				{
					result = ret;
				}
				else
				{
					string zipFileName = ExcelUtils.ZipXlsFiles(fileList);
					result = zipFileName;
				}
			}
			return result;
		}

		private static void SetCellValue(ICellStyle cellStyleDefault, ICellStyle cellStyleDate, ICellStyle cellStyleDateTime, ICellStyle cellStyleNumber, ICell cell, string drValue, BoundFieldMode editType)
		{
			switch (editType)
			{
			case BoundFieldMode.String:
			case BoundFieldMode.DropDown:
			case BoundFieldMode.CheckedDropDown:
				cell.CellStyle = cellStyleDefault;
				drValue = HttpUtility.HtmlDecode(drValue);
				cell.SetCellValue(drValue);
				break;
			case BoundFieldMode.CheckBox:
			{
				cell.CellStyle = cellStyleDefault;
				bool flag = drValue.IndexOf("CheckBoxUnChecked") >= 0 || drValue.IndexOf("CheckBoxChecked") >= 0;
				if (flag)
				{
					cell.SetCellValue((drValue.IndexOf("CheckBoxUnChecked", StringComparison.OrdinalIgnoreCase) > 0) ? "N" : "Y");
				}
				else
				{
					cell.SetCellValue(drValue);
				}
				break;
			}
			case BoundFieldMode.Date:
			{
				cell.CellStyle = cellStyleDate;
				DateTime dateTime;
				bool flag2 = DateTime.TryParse(drValue, out dateTime);
				if (flag2)
				{
					drValue = dateTime.ToString("yyyy-MM-dd");
				}
				cell.SetCellValue(drValue);
				break;
			}
			case BoundFieldMode.DateTime:
			{
				cell.CellStyle = cellStyleDateTime;
				DateTime dateTime;
				bool flag3 = DateTime.TryParse(drValue, out dateTime);
				if (flag3)
				{
					drValue = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
				}
				cell.SetCellValue(drValue);
				break;
			}
			case BoundFieldMode.Time:
			{
				cell.CellStyle = cellStyleDefault;
				DateTime dateTime;
				bool flag4 = DateTime.TryParse(drValue, out dateTime);
				if (flag4)
				{
					drValue = dateTime.ToString("HH:mm");
				}
				cell.SetCellValue(drValue);
				break;
			}
			case BoundFieldMode.Numeric:
				cell.CellStyle = cellStyleNumber;
				cell.SetCellValue(drValue);
				break;
			default:
				cell.CellStyle = cellStyleDefault;
				cell.SetCellValue("");
				break;
			}
		}

		private static int Pixel2ExcelWidth(double pixelWidth)
		{
			return (int)(pixelWidth * 2.0 * 256.0 / 12.0);
		}
	}
}
