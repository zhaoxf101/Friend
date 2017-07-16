using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using TIM.T_KERNEL.Helper;

namespace TIM.T_TEMPLET.Reporting
{
	internal class Utils
	{
		private const string CImageTypeBitmap = "B";

		private const string CImageTypeJPEG = "J";

		private const string CImageTypeEMF = "E";

		private const string CImageControlAlign = "A";

		private const string CImageControlStretch = "S";

		private const string CImageControlTile = "T";

		private const string CPaperOrientationPortrait = "P";

		private const string CPaperOrientationLandscape = "L";

		private const string CAlphaCharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		private const string CDigitCharSet = "0123456789";

		private const string CHexDigitCharSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public const string CRowTypeReportHeader = "RH";

		public const string CRowTypeReportFooter = "RF";

		public const string CRowTypePageHeader = "PH";

		public const string CRowTypePageFooter = "PF";

		public const string CRowTypeColumnHeader = "CH";

		public const string CRowTypeColumnFooter = "CF";

		public const string CRowTypeDetailData = "DD";

		public const string CRowTypeGroupHeader = "GH";

		public const string CRowTypeGroupFooter = "GF";

		public const string CColumnTypeRowHeader = "RH";

		public const string CColumnTypeRowFooter = "RF";

		public const string CColumnTypeDetailData = "DD";

		public const string CDataTypeAuto = "A";

		public const string CDataTypeString = "C";

		public const string CDataTypeNumber = "N";

		public const string CDataTypeFormula = "F";

		public const string CLineStyleNone = "0";

		public const string CLineStyleThinSolid = "1";

		public const string CLineStyleThinDash = "2";

		public const string CLineStyleThinDot = "3";

		public const string CLineStyleThinDashDot = "4";

		public const string CLineStyleThinDashDotDot = "5";

		public const string CLineStyleThickSolid = "6";

		public const string CLineStyleThickDash = "7";

		public const string CLineStyleThickDot = "8";

		public const string CLineStyleThickDashDot = "9";

		public const string CLineStyleThickDashDotDot = "A";

		public const string CHAlignmentAuto = "A";

		public const string CHAlignmentLeft = "L";

		public const string CHAlignmentCenter = "C";

		public const string CHAlignmentRight = "R";

		public const string CHAlignmentEdge = "E";

		public const string CVAlignmentTop = "T";

		public const string CVAlignmentCenter = "C";

		public const string CVAlignmentBottom = "B";

		public const string CTextControlCut = "C";

		public const string CTextControlWordWrap = "W";

		public const string CTextControlSmallFont = "S";

		internal static RdPaper FindPaperByName(string paperName)
		{
			RdPaper result = null;
			bool flag = string.IsNullOrEmpty(paperName);
			RdPaper result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				foreach (KeyValuePair<int, RdPaper> item in TgtPapers.m_items)
				{
					bool flag2 = item.Value.Name == paperName;
					if (flag2)
					{
						result = item.Value;
						break;
					}
				}
				result2 = result;
			}
			return result2;
		}

		internal static RdPaper FindPaper(int paper)
		{
			RdPaper result = null;
			foreach (KeyValuePair<int, RdPaper> item in TgtPapers.m_items)
			{
				bool flag = item.Key == paper;
				if (flag)
				{
					result = item.Value;
					break;
				}
			}
			return result;
		}

		internal static bool ValidPaperName(string paperName)
		{
			bool result = false;
			bool flag = string.IsNullOrEmpty(paperName);
			bool result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				foreach (KeyValuePair<int, RdPaper> item in TgtPapers.m_items)
				{
					bool flag2 = item.Value.Name == paperName;
					if (flag2)
					{
						result = true;
						break;
					}
				}
				result2 = result;
			}
			return result2;
		}

		internal static int Str2Paper(string paperName)
		{
			int result = -2147483648;
			RdPaper paper = Utils.FindPaperByName(paperName);
			bool flag = paper != null;
			if (flag)
			{
				result = paper.Paper;
			}
			return result;
		}

		internal static string Paper2Str(int paper)
		{
			RdPaper result = Utils.FindPaper(paper);
			bool flag = result != null;
			string result2;
			if (flag)
			{
				result2 = result.Name;
			}
			else
			{
				result2 = "";
			}
			return result2;
		}

		internal static int FindPaperWidth(int paper)
		{
			RdPaper tmpPaper = Utils.FindPaper(paper);
			bool flag = tmpPaper == null;
			int result;
			if (flag)
			{
				result = 100;
			}
			else
			{
				result = tmpPaper.Width;
			}
			return result;
		}

		internal static int FindPaperHeight(int paper)
		{
			RdPaper tmpPaper = Utils.FindPaper(paper);
			bool flag = tmpPaper == null;
			int result;
			if (flag)
			{
				result = 100;
			}
			else
			{
				result = tmpPaper.Height;
			}
			return result;
		}

		internal static bool IsNumber(string text)
		{
			Regex regex = new Regex("^[-+]?[0-9]*\\.?[0-9]+$");
			return regex.IsMatch(text);
		}

		internal static int Str2Int(string str, int failValue)
		{
			int result = 2147483647;
			bool flag = !int.TryParse(str, out result);
			int result2;
			if (flag)
			{
				result2 = failValue;
			}
			else
			{
				result2 = result;
			}
			return result2;
		}

		internal static float Str2Float(string str, float failValue)
		{
			float ret = 3.40282347E+38f;
			bool flag = !float.TryParse(str, out ret);
			float result;
			if (flag)
			{
				result = failValue;
			}
			else
			{
				result = ret;
			}
			return result;
		}

		internal static double Str2Double(string str, double failValue)
		{
			double ret = 1.7976931348623157E+308;
			bool flag = !double.TryParse(str, out ret);
			double result;
			if (flag)
			{
				result = failValue;
			}
			else
			{
				result = ret;
			}
			return result;
		}

		internal static DateTime Str2DateTime(string str, DateTime failValue)
		{
			DateTime ret;
			bool flag = !DateTime.TryParse(str, out ret);
			DateTime result;
			if (flag)
			{
				result = failValue;
			}
			else
			{
				result = ret;
			}
			return result;
		}

		internal static string Bool2Str(bool b)
		{
			string result;
			if (b)
			{
				result = "Y";
			}
			else
			{
				result = "N";
			}
			return result;
		}

		internal static bool Str2Bool(string str)
		{
			return str.Equals("Y");
		}

		internal static string GetXmlNodeAttribute(XmlNode node, string attributeName)
		{
			XmlAttribute xmlAttribute = node.Attributes[attributeName];
			bool flag = xmlAttribute != null;
			string result;
			if (flag)
			{
				result = xmlAttribute.Value.Trim();
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		internal static string GetAttrString(XmlNode node, string attributeName, string defaultValue)
		{
			XmlAttribute xmlAttribute = node.Attributes[attributeName];
			bool flag = xmlAttribute != null;
			string result;
			if (flag)
			{
				result = xmlAttribute.Value.Trim();
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		internal static int GetAttrInt(XmlNode node, string attrName, int defaultValue)
		{
			string value = Utils.GetXmlNodeAttribute(node, attrName);
			bool flag = string.IsNullOrEmpty(value);
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = Utils.Str2Int(value, defaultValue);
			}
			return result;
		}

		internal static float GetAttrFloat(XmlNode node, string attrName, float defaultValue)
		{
			string value = Utils.GetXmlNodeAttribute(node, attrName);
			bool flag = string.IsNullOrEmpty(value);
			float result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = Utils.Str2Float(value, defaultValue);
			}
			return result;
		}

		internal static DateTime GetAttrDateTime(XmlNode node, string attrName, DateTime defaultValue)
		{
			string value = Utils.GetXmlNodeAttribute(node, attrName);
			return Utils.Str2DateTime(value, defaultValue);
		}

		internal static bool GetAttrBool(XmlNode node, string attrName, bool defaultValue)
		{
			string value = Utils.GetXmlNodeAttribute(node, attrName);
			bool flag = string.IsNullOrEmpty(value);
			bool result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = Utils.Str2Bool(value);
			}
			return result;
		}

		internal static RdImageType Str2ImageType(string value, RdImageType defaultValue)
		{
			RdImageType result;
			if (!(value == "B"))
			{
				if (!(value == "J"))
				{
					if (!(value == "E"))
					{
						result = defaultValue;
					}
					else
					{
						result = RdImageType.itEMF;
					}
				}
				else
				{
					result = RdImageType.itJPEG;
				}
			}
			else
			{
				result = RdImageType.itBitmap;
			}
			return result;
		}

		internal static string ImageType2Str(RdImageType imageType)
		{
			string result = string.Empty;
			switch (imageType)
			{
			case RdImageType.itBitmap:
				result = "B";
				break;
			case RdImageType.itJPEG:
				result = "J";
				break;
			case RdImageType.itEMF:
				result = "E";
				break;
			}
			return result;
		}

		internal static RdImageControl Str2ImageControl(string value, RdImageControl defaultValue)
		{
			RdImageControl result;
			if (!(value == "A"))
			{
				if (!(value == "S"))
				{
					if (!(value == "T"))
					{
						result = defaultValue;
					}
					else
					{
						result = RdImageControl.icTile;
					}
				}
				else
				{
					result = RdImageControl.icStretch;
				}
			}
			else
			{
				result = RdImageControl.icAlign;
			}
			return result;
		}

		internal static string ImageControl2Str(RdImageControl imageControl)
		{
			string result = string.Empty;
			switch (imageControl)
			{
			case RdImageControl.icAlign:
				result = "A";
				break;
			case RdImageControl.icStretch:
				result = "S";
				break;
			case RdImageControl.icTile:
				result = "T";
				break;
			}
			return result;
		}

		internal static Image GetAttrGraphic(XmlNode node)
		{
			string value = Utils.GetXmlNodeAttribute(node, "Graphic");
			byte[] arr = Convert.FromBase64String(value);
			MemoryStream ms = new MemoryStream(arr);
			return Image.FromStream(ms);
		}

		internal static string MakeAttribute(string name, string value)
		{
			return string.Concat(new string[]
			{
				" ",
				name,
				"=\"",
				value,
				"\""
			});
		}

		internal static RdPaperOrientation Str2PaperOrientation(string value, RdPaperOrientation defaultValue)
		{
			RdPaperOrientation ret;
			if (!(value == "P"))
			{
				if (!(value == "L"))
				{
					ret = defaultValue;
				}
				else
				{
					ret = RdPaperOrientation.roLandscape;
				}
			}
			else
			{
				ret = RdPaperOrientation.roPortrait;
			}
			return ret;
		}

		internal static string PaperOrientation2Str(RdPaperOrientation value)
		{
			string ret = string.Empty;
			if (value != RdPaperOrientation.roPortrait)
			{
				if (value == RdPaperOrientation.roLandscape)
				{
					ret = "L";
				}
			}
			else
			{
				ret = "P";
			}
			return ret;
		}

		internal static bool IsAlphaCharSet(string str)
		{
			return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(str) >= 0;
		}

		internal static bool IsDigitCharSet(string str)
		{
			return "0123456789".IndexOf(str) >= 0;
		}

		internal static bool IsHexDigitCharSet(string str)
		{
			return "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(str) >= 0;
		}

		internal static string EncodeCellId(int row, int column)
		{
			return Utils.EncodeColumnId(column) + row.ToString();
		}

		internal static bool DecodeCellId(string id, ref int row, ref int column)
		{
			bool flag = string.IsNullOrEmpty(id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string tempId = id.Trim().ToUpper();
				bool flag2 = string.IsNullOrEmpty(tempId) || tempId.Length == 0;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = Regex.IsMatch(tempId, "\\$[A-Z]+\\$[0-9]+") && Regex.Match(tempId, "\\$[A-Z]+\\$[0-9]+").Value == tempId;
					if (flag3)
					{
						bool flag4 = !Utils.DecodeColumnId(Regex.Match(tempId, "\\$[A-Z]+").Value.Replace("$", ""), ref column);
						if (flag4)
						{
							result = false;
						}
						else
						{
							row = Regex.Match(tempId, "\\$[0-9]+").Value.Replace("$", "").ToInt();
							result = true;
						}
					}
					else
					{
						bool flag5 = Regex.IsMatch(tempId, "R[0-9]+C[0-9]+") && Regex.Match(tempId, "R[0-9]+C[0-9]+").Value == tempId;
						if (flag5)
						{
							row = Regex.Match(tempId, "R[0-9]+").Value.Replace("R", "").ToInt();
							column = Regex.Match(tempId, "C[0-9]+").Value.Replace("R", "").ToInt();
							result = true;
						}
						else
						{
							bool flag6 = Regex.IsMatch(tempId, "[A-Z]+[0-9]+") && Regex.Match(tempId, "[A-Z]+[0-9]+").Value == tempId;
							if (flag6)
							{
								bool flag7 = !Utils.DecodeColumnId(Regex.Match(tempId, "[A-Z]+").Value, ref column);
								if (flag7)
								{
									result = false;
								}
								else
								{
									row = Regex.Match(tempId, "[0-9]+").Value.ToInt();
									result = true;
								}
							}
							else
							{
								result = false;
							}
						}
					}
				}
			}
			return result;
		}

		internal static string EncodeColumnId(int column)
		{
			string result = string.Empty;
			for (int curNumber = column; curNumber > 0; curNumber = (curNumber - 1) / 26)
			{
				result = ((char)((curNumber - 1) % 26 + 65)).ToString() + result;
			}
			return result;
		}

		internal static bool DecodeColumnId(string id, ref int column)
		{
			id = id.ToUpper();
			column = 0;
			for (int i = 0; i < id.Length; i++)
			{
				ASCIIEncoding asciiEncoding = new ASCIIEncoding();
				int intAsciiCode = (int)asciiEncoding.GetBytes(id.Substring(i, 1))[0];
				column = column * 26 + intAsciiCode - 64;
			}
			return true;
		}

		internal static string EncodeRangeId(int left, int top, int right, int bottom)
		{
			string ret = string.Empty;
			bool flag = left < 1 || right < 1 || top < 1 || bottom < 1;
			string result;
			if (flag)
			{
				result = ret;
			}
			else
			{
				bool flag2 = left <= right;
				int L;
				int R;
				if (flag2)
				{
					L = left;
					R = right;
				}
				else
				{
					L = right;
					R = left;
				}
				bool flag3 = top <= bottom;
				int T;
				int B;
				if (flag3)
				{
					T = top;
					B = bottom;
				}
				else
				{
					T = bottom;
					B = top;
				}
				bool flag4 = L != R || T != B;
				if (flag4)
				{
					ret = Utils.EncodeCellId(T, L) + ":" + Utils.EncodeCellId(B, R);
				}
				else
				{
					ret = Utils.EncodeCellId(T, L);
				}
				result = ret;
			}
			return result;
		}

		internal static bool DecodeRangeId(string id, ref int left, ref int top, ref int right, ref int bottom)
		{
			int C2;
			int R2;
			int R;
			int C = R = (R2 = (C2 = -1));
			bool flag = string.IsNullOrEmpty(id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string tempId = id.Trim().ToUpper();
				bool flag2 = string.IsNullOrEmpty(tempId) || tempId.Length == 0;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = Regex.IsMatch(tempId, "[A-Z]+[0-9]+:[A-Z]+[0-9]+") && Regex.Match(tempId, "[A-Z]+[0-9]+:[A-Z]+[0-9]+").Value == tempId;
					if (flag3)
					{
						string leftTop = tempId.Split(new char[]
						{
							':'
						})[0];
						string rightBotton = tempId.Split(new char[]
						{
							':'
						})[1];
						bool flag4 = !Utils.DecodeCellId(leftTop, ref R, ref C) || !Utils.DecodeCellId(rightBotton, ref R2, ref C2);
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = R < 1 || C < 1 || R2 < 1 || C2 < 1;
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = C <= C2;
								if (flag6)
								{
									left = C;
									right = C2;
								}
								else
								{
									left = C2;
									right = C;
								}
								bool flag7 = R <= R2;
								if (flag7)
								{
									top = R;
									bottom = R2;
								}
								else
								{
									top = R2;
									bottom = R;
								}
								result = true;
							}
						}
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		public static RdRowType Str2RowType(string value, RdRowType defaultValue)
		{
			uint num = ComputeStringHash(value);
			RdRowType ret;
			if (num <= 1490086304u)
			{
				if (num <= 721420139u)
				{
					if (num != 687864901u)
					{
						if (num == 721420139u)
						{
							if (value == "PF")
							{
								ret = RdRowType.rtPageFooter;
								return ret;
							}
						}
					}
					else if (value == "PH")
					{
						ret = RdRowType.rtPageHeader;
						return ret;
					}
				}
				else if (num != 1020165877u)
				{
					if (num == 1490086304u)
					{
						if (value == "GF")
						{
							ret = RdRowType.rtGroupFooter;
							return ret;
						}
					}
				}
				else if (value == "DD")
				{
					ret = RdRowType.rtDetailData;
					return ret;
				}
			}
			else if (num <= 2027558492u)
			{
				if (num != 1724972970u)
				{
					if (num == 2027558492u)
					{
						if (value == "CF")
						{
							ret = RdRowType.rtColumnFooter;
							return ret;
						}
					}
				}
				else if (value == "GH")
				{
					ret = RdRowType.rtGroupHeader;
					return ret;
				}
			}
			else if (num != 2063923849u)
			{
				if (num != 2128224206u)
				{
					if (num == 2231700039u)
					{
						if (value == "RH")
						{
							ret = RdRowType.rtReportHeader;
							return ret;
						}
					}
				}
				else if (value == "CH")
				{
					ret = RdRowType.rtColumnHeader;
					return ret;
				}
			}
			else if (value == "RF")
			{
				ret = RdRowType.rtReportFooter;
				return ret;
			}
			ret = defaultValue;
			return ret;
		}

		public static string RowType2Str(RdRowType value)
		{
			string ret = string.Empty;
			switch (value)
			{
			case RdRowType.rtReportHeader:
				ret = "RH";
				break;
			case RdRowType.rtPageHeader:
				ret = "PH";
				break;
			case RdRowType.rtColumnHeader:
				ret = "CH";
				break;
			case RdRowType.rtGroupHeader:
				ret = "GH";
				break;
			case RdRowType.rtDetailData:
				ret = "DD";
				break;
			case RdRowType.rtGroupFooter:
				ret = "GF";
				break;
			case RdRowType.rtColumnFooter:
				ret = "CF";
				break;
			case RdRowType.rtPageFooter:
				ret = "PF";
				break;
			case RdRowType.rtReportFooter:
				ret = "RF";
				break;
			default:
				ret = "DD";
				break;
			}
			return ret;
		}

		public static RdColumnType Str2ColumnType(string value, RdColumnType defValue)
		{
			RdColumnType ret;
			if (!(value == "RH"))
			{
				if (!(value == "RF"))
				{
					if (!(value == "DD"))
					{
						ret = defValue;
					}
					else
					{
						ret = RdColumnType.ctDetailData;
					}
				}
				else
				{
					ret = RdColumnType.ctRowFooter;
				}
			}
			else
			{
				ret = RdColumnType.ctRowHeader;
			}
			return ret;
		}

		public static string ColumnType2Str(RdColumnType value)
		{
			string ret = string.Empty;
			switch (value)
			{
			case RdColumnType.ctRowHeader:
				ret = "RH";
				break;
			case RdColumnType.ctDetailData:
				ret = "DD";
				break;
			case RdColumnType.ctRowFooter:
				ret = "RF";
				break;
			default:
				ret = "DD";
				break;
			}
			return ret;
		}

		public static void StyleRecLoad(XmlNode node, ref RdMergeCellsStyle style, RdMergeCellsStyle defaultStyle)
		{
			Utils.CopyStyleRec(ref style, defaultStyle);
			style.DataType = Utils.Str2DataType(Utils.GetXmlNodeAttribute(node, "Type"), style.DataType);
			style.DisplayFormat = Utils.GetXmlNodeAttribute(node, "DisplayFormat");
			style.FontName = Utils.GetXmlNodeAttribute(node, "FontName");
			style.FontSize = Utils.GetAttrInt(node, "FontSize", style.FontSize);
			style.FontColor = Utils.Str2Color(Utils.GetXmlNodeAttribute(node, "FontColor"), style.FontColor);
			style.FontBold = Utils.GetAttrBool(node, "FontBold", style.FontBold);
			style.FontItalic = Utils.GetAttrBool(node, "FontItalic", style.FontItalic);
			style.FontUnderline = Utils.GetAttrBool(node, "FontUnderline", style.FontUnderline);
			style.FontStrikeout = Utils.GetAttrBool(node, "FontStrikeout", style.FontStrikeout);
			style.LeftBorderStyle = Utils.Str2LineStyle(Utils.GetXmlNodeAttribute(node, "LeftBorderStyle"), style.LeftBorderStyle);
			style.LeftBorderColor = Utils.Str2Color(Utils.GetXmlNodeAttribute(node, "LeftBorderColor"), style.LeftBorderColor);
			style.LeftBorderWidth = Utils.GetAttrInt(node, "LeftBorderWidth", style.LeftBorderWidth);
			style.TopBorderStyle = Utils.Str2LineStyle(Utils.GetXmlNodeAttribute(node, "TopBorderStyle"), style.TopBorderStyle);
			style.TopBorderColor = Utils.Str2Color(Utils.GetXmlNodeAttribute(node, "TopBorderColor"), style.TopBorderColor);
			style.TopBorderWidth = Utils.GetAttrInt(node, "TopBorderWidth", style.TopBorderWidth);
			style.RightBorderStyle = Utils.Str2LineStyle(Utils.GetXmlNodeAttribute(node, "RightBorderStyle"), style.RightBorderStyle);
			style.RightBorderColor = Utils.Str2Color(Utils.GetXmlNodeAttribute(node, "RightBorderColor"), style.RightBorderColor);
			style.RightBorderWidth = Utils.GetAttrInt(node, "RightBorderWidth", style.RightBorderWidth);
			style.BottomBorderStyle = Utils.Str2LineStyle(Utils.GetXmlNodeAttribute(node, "BottomBorderStyle"), style.BottomBorderStyle);
			style.BottomBorderColor = Utils.Str2Color(Utils.GetXmlNodeAttribute(node, "BottomBorderColor"), style.BottomBorderColor);
			style.BottomBorderWidth = Utils.GetAttrInt(node, "BottomBorderWidth", style.BottomBorderWidth);
			style.DiagLT2RBStyle = Utils.Str2LineStyle(Utils.GetXmlNodeAttribute(node, "DiagLT2RBStyle"), style.DiagLT2RBStyle);
			style.DiagLT2RBColor = Utils.Str2Color(Utils.GetXmlNodeAttribute(node, "DiagLT2RBColor"), style.DiagLT2RBColor);
			style.DiagLT2RBWidth = Utils.GetAttrInt(node, "DiagLT2RBWidth", style.DiagLT2RBWidth);
			style.DiagLB2RTStyle = Utils.Str2LineStyle(Utils.GetXmlNodeAttribute(node, "DiagLB2RTStyle"), style.DiagLB2RTStyle);
			style.DiagLB2RTColor = Utils.Str2Color(Utils.GetXmlNodeAttribute(node, "DiagLB2RTColor"), style.DiagLB2RTColor);
			style.DiagLB2RTWidth = Utils.GetAttrInt(node, "DiagLB2RTWidth", style.DiagLB2RTWidth);
			style.Color = Utils.Str2Color(Utils.GetXmlNodeAttribute(node, "Color"), Color.White);
			style.Transparent = Utils.GetAttrBool(node, "Transparent", style.Transparent);
			style.Pattern = Utils.GetAttrInt(node, "Pattern", style.Pattern);
			style.PatternColor = Utils.Str2Color(Utils.GetXmlNodeAttribute(node, "PatternColor"), style.PatternColor);
			style.HAlignment = Utils.Str2HAlignment(Utils.GetXmlNodeAttribute(node, "HAlignment"), style.HAlignment);
			style.VAlignment = Utils.Str2VAlignment(Utils.GetXmlNodeAttribute(node, "VAlignment"), style.VAlignment);
			style.LeftMargin = Utils.GetAttrInt(node, "LeftMargin", style.LeftMargin);
			style.TopMargin = Utils.GetAttrInt(node, "TopMargin", style.TopMargin);
			style.RightMargin = Utils.GetAttrInt(node, "RightMargin", style.RightMargin);
			style.BottomMargin = Utils.GetAttrInt(node, "BottomMargin", style.BottomMargin);
			style.ThreePartText = Utils.GetAttrBool(node, "ThreePartText", style.ThreePartText);
			style.TextControl = Utils.Str2TextControl(Utils.GetXmlNodeAttribute(node, "TextControl"), style.TextControl);
			style.LineSpace = Utils.GetAttrInt(node, "LineSpace", style.LineSpace);
			style.Locked = Utils.GetAttrBool(node, "Locked", style.Locked);
			style.Preview = Utils.GetAttrBool(node, "Preview", style.Preview);
			style.Print = Utils.GetAttrBool(node, "Print", style.Print);
			style.SmallFontWordWrap = Utils.GetAttrBool(node, "SmallFontWordWrap", style.SmallFontWordWrap);
		}

		public static void CopyStyleRec(ref RdMergeCellsStyle toStyle, RdMergeCellsStyle fromStyle)
		{
			toStyle.DataType = fromStyle.DataType;
			toStyle.DisplayFormat = fromStyle.DisplayFormat;
			toStyle.FontName = fromStyle.FontName;
			toStyle.FontSize = fromStyle.FontSize;
			toStyle.FontColor = fromStyle.FontColor;
			toStyle.FontBold = fromStyle.FontBold;
			toStyle.FontItalic = fromStyle.FontItalic;
			toStyle.FontUnderline = fromStyle.FontUnderline;
			toStyle.FontStrikeout = fromStyle.FontStrikeout;
			toStyle.LeftBorderStyle = fromStyle.LeftBorderStyle;
			toStyle.LeftBorderColor = fromStyle.LeftBorderColor;
			toStyle.LeftBorderWidth = fromStyle.LeftBorderWidth;
			toStyle.TopBorderStyle = fromStyle.TopBorderStyle;
			toStyle.TopBorderColor = fromStyle.TopBorderColor;
			toStyle.TopBorderWidth = fromStyle.TopBorderWidth;
			toStyle.RightBorderStyle = fromStyle.RightBorderStyle;
			toStyle.RightBorderColor = fromStyle.RightBorderColor;
			toStyle.RightBorderWidth = fromStyle.RightBorderWidth;
			toStyle.BottomBorderStyle = fromStyle.BottomBorderStyle;
			toStyle.BottomBorderColor = fromStyle.BottomBorderColor;
			toStyle.BottomBorderWidth = fromStyle.BottomBorderWidth;
			toStyle.DiagLT2RBStyle = fromStyle.DiagLT2RBStyle;
			toStyle.DiagLT2RBColor = fromStyle.DiagLT2RBColor;
			toStyle.DiagLT2RBWidth = fromStyle.DiagLT2RBWidth;
			toStyle.DiagLB2RTStyle = fromStyle.DiagLB2RTStyle;
			toStyle.DiagLB2RTColor = fromStyle.DiagLB2RTColor;
			toStyle.DiagLB2RTWidth = fromStyle.DiagLB2RTWidth;
			toStyle.Color = fromStyle.Color;
			toStyle.Transparent = fromStyle.Transparent;
			toStyle.Pattern = fromStyle.Pattern;
			toStyle.PatternColor = fromStyle.PatternColor;
			toStyle.HAlignment = fromStyle.HAlignment;
			toStyle.VAlignment = fromStyle.VAlignment;
			toStyle.LeftMargin = fromStyle.LeftMargin;
			toStyle.TopMargin = fromStyle.TopMargin;
			toStyle.RightMargin = fromStyle.RightMargin;
			toStyle.BottomMargin = fromStyle.BottomMargin;
			toStyle.ThreePartText = fromStyle.ThreePartText;
			toStyle.TextControl = fromStyle.TextControl;
			toStyle.LineSpace = fromStyle.LineSpace;
			toStyle.Locked = fromStyle.Locked;
			toStyle.Preview = fromStyle.Preview;
			toStyle.Print = fromStyle.Print;
			toStyle.SmallFontWordWrap = fromStyle.SmallFontWordWrap;
		}

		public static bool StyleRecEqual(RdMergeCellsStyle style1, RdMergeCellsStyle style2)
		{
			return style1.DataType == style2.DataType && style1.DisplayFormat == style2.DisplayFormat && style1.FontName == style2.FontName && style1.FontSize == style2.FontSize && style1.FontColor == style2.FontColor && style1.FontBold == style2.FontBold && style1.FontItalic == style2.FontItalic && style1.FontUnderline == style2.FontUnderline && style1.FontStrikeout == style2.FontStrikeout && style1.LeftBorderStyle == style2.LeftBorderStyle && style1.LeftBorderColor == style2.LeftBorderColor && style1.LeftBorderWidth == style2.LeftBorderWidth && style1.TopBorderStyle == style2.TopBorderStyle && style1.TopBorderColor == style2.TopBorderColor && style1.TopBorderWidth == style2.TopBorderWidth && style1.RightBorderStyle == style2.RightBorderStyle && style1.RightBorderColor == style2.RightBorderColor && style1.RightBorderWidth == style2.RightBorderWidth && style1.BottomBorderStyle == style2.BottomBorderStyle && style1.BottomBorderColor == style2.BottomBorderColor && style1.BottomBorderWidth == style2.BottomBorderWidth && style1.DiagLT2RBStyle == style2.DiagLT2RBStyle && style1.DiagLT2RBColor == style2.DiagLT2RBColor && style1.DiagLT2RBWidth == style2.DiagLT2RBWidth && style1.DiagLB2RTStyle == style2.DiagLB2RTStyle && style1.DiagLB2RTColor == style2.DiagLB2RTColor && style1.DiagLB2RTWidth == style2.DiagLB2RTWidth && style1.Color == style2.Color && style1.Transparent == style2.Transparent && style1.Pattern == style2.Pattern && style1.PatternColor == style2.PatternColor && style1.HAlignment == style2.HAlignment && style1.VAlignment == style2.VAlignment && style1.LeftMargin == style2.LeftMargin && style1.TopMargin == style2.TopMargin && style1.RightMargin == style2.RightMargin && style1.BottomMargin == style2.BottomMargin && style1.ThreePartText == style2.ThreePartText && style1.TextControl == style2.TextControl && style1.LineSpace == style2.LineSpace && style1.Locked == style2.Locked && style1.Preview == style2.Preview && style1.Print == style2.Print && style1.SmallFontWordWrap == style2.SmallFontWordWrap;
		}

		public static string StyleRecGetAttributes(RdMergeCellsStyle style, RdMergeCellsStyle defaultStyle)
		{
			bool IncludeAll = Utils.StyleRecEqual(style, defaultStyle);
			string ret = string.Empty;
			bool flag = IncludeAll || style.DataType != defaultStyle.DataType;
			if (flag)
			{
				ret += Utils.MakeAttribute("Type", Utils.DataType2Str(style.DataType));
			}
			bool flag2 = IncludeAll || style.DisplayFormat != defaultStyle.DisplayFormat;
			if (flag2)
			{
				ret += Utils.MakeAttribute("DisplayFormat", style.DisplayFormat);
			}
			bool flag3 = IncludeAll || style.FontName != defaultStyle.FontName;
			if (flag3)
			{
				ret += Utils.MakeAttribute("FontName", style.FontName);
			}
			bool flag4 = IncludeAll || style.FontSize != defaultStyle.FontSize;
			if (flag4)
			{
				ret += Utils.MakeAttribute("FontSize", style.FontSize.ToString());
			}
			bool flag5 = IncludeAll || style.FontColor != defaultStyle.FontColor;
			if (flag5)
			{
				ret += Utils.MakeAttribute("FontColor", Utils.Color2Str(style.FontColor));
			}
			bool flag6 = IncludeAll || style.FontBold != defaultStyle.FontBold;
			if (flag6)
			{
				ret += Utils.MakeAttribute("FontBold", Utils.Bool2Str(style.FontBold));
			}
			bool flag7 = IncludeAll || style.FontItalic != defaultStyle.FontItalic;
			if (flag7)
			{
				ret += Utils.MakeAttribute("FontItalic", Utils.Bool2Str(style.FontItalic));
			}
			bool flag8 = IncludeAll || style.FontUnderline != defaultStyle.FontUnderline;
			if (flag8)
			{
				ret += Utils.MakeAttribute("FontUnderline", Utils.Bool2Str(style.FontUnderline));
			}
			bool flag9 = IncludeAll || style.FontStrikeout != defaultStyle.FontStrikeout;
			if (flag9)
			{
				ret += Utils.MakeAttribute("FontStrikeout", Utils.Bool2Str(style.FontStrikeout));
			}
			bool flag10 = IncludeAll || style.LeftBorderStyle != defaultStyle.LeftBorderStyle;
			if (flag10)
			{
				ret += Utils.MakeAttribute("LeftBorderStyle", Utils.LineStyle2Str(style.LeftBorderStyle));
			}
			bool flag11 = IncludeAll || style.LeftBorderColor != defaultStyle.LeftBorderColor;
			if (flag11)
			{
				ret += Utils.MakeAttribute("LeftBorderColor", Utils.Color2Str(style.LeftBorderColor));
			}
			bool flag12 = IncludeAll || style.LeftBorderWidth != defaultStyle.LeftBorderWidth;
			if (flag12)
			{
				ret += Utils.MakeAttribute("LeftBorderWidth", style.LeftBorderWidth.ToString());
			}
			bool flag13 = IncludeAll || style.TopBorderStyle != defaultStyle.TopBorderStyle;
			if (flag13)
			{
				ret += Utils.MakeAttribute("TopBorderStyle", Utils.LineStyle2Str(style.TopBorderStyle));
			}
			bool flag14 = IncludeAll || style.TopBorderColor != defaultStyle.TopBorderColor;
			if (flag14)
			{
				ret += Utils.MakeAttribute("TopBorderColor", Utils.Color2Str(style.TopBorderColor));
			}
			bool flag15 = IncludeAll || style.TopBorderWidth != defaultStyle.TopBorderWidth;
			if (flag15)
			{
				ret += Utils.MakeAttribute("TopBorderWidth", style.TopBorderWidth.ToString());
			}
			bool flag16 = IncludeAll || style.RightBorderStyle != defaultStyle.RightBorderStyle;
			if (flag16)
			{
				ret += Utils.MakeAttribute("RightBorderStyle", Utils.LineStyle2Str(style.RightBorderStyle));
			}
			bool flag17 = IncludeAll || style.RightBorderColor != defaultStyle.RightBorderColor;
			if (flag17)
			{
				ret += Utils.MakeAttribute("RightBorderColor", Utils.Color2Str(style.RightBorderColor));
			}
			bool flag18 = IncludeAll || style.RightBorderWidth != defaultStyle.RightBorderWidth;
			if (flag18)
			{
				ret += Utils.MakeAttribute("RightBorderWidth", style.RightBorderWidth.ToString());
			}
			bool flag19 = IncludeAll || style.BottomBorderStyle != defaultStyle.BottomBorderStyle;
			if (flag19)
			{
				ret += Utils.MakeAttribute("BottomBorderStyle", Utils.LineStyle2Str(style.BottomBorderStyle));
			}
			bool flag20 = IncludeAll || style.BottomBorderColor != defaultStyle.BottomBorderColor;
			if (flag20)
			{
				ret += Utils.MakeAttribute("BottomBorderColor", Utils.Color2Str(style.BottomBorderColor));
			}
			bool flag21 = IncludeAll || style.BottomBorderWidth != defaultStyle.BottomBorderWidth;
			if (flag21)
			{
				ret += Utils.MakeAttribute("BottomBorderWidth", style.BottomBorderWidth.ToString());
			}
			bool flag22 = IncludeAll || style.DiagLT2RBStyle != defaultStyle.DiagLT2RBStyle;
			if (flag22)
			{
				ret += Utils.MakeAttribute("DiagLT2RBStyle", Utils.LineStyle2Str(style.DiagLT2RBStyle));
			}
			bool flag23 = IncludeAll || style.DiagLT2RBColor != defaultStyle.DiagLT2RBColor;
			if (flag23)
			{
				ret += Utils.MakeAttribute("DiagLT2RBColor", Utils.Color2Str(style.DiagLT2RBColor));
			}
			bool flag24 = IncludeAll || style.DiagLT2RBWidth != defaultStyle.DiagLT2RBWidth;
			if (flag24)
			{
				ret += Utils.MakeAttribute("DiagLT2RBWidth", style.DiagLT2RBWidth.ToString());
			}
			bool flag25 = IncludeAll || style.DiagLB2RTStyle != defaultStyle.DiagLB2RTStyle;
			if (flag25)
			{
				ret += Utils.MakeAttribute("DiagLB2RTStyle", Utils.LineStyle2Str(style.DiagLB2RTStyle));
			}
			bool flag26 = IncludeAll || style.DiagLB2RTColor != defaultStyle.DiagLB2RTColor;
			if (flag26)
			{
				ret += Utils.MakeAttribute("DiagLB2RTColor", Utils.Color2Str(style.DiagLB2RTColor));
			}
			bool flag27 = IncludeAll || style.DiagLB2RTWidth != defaultStyle.DiagLB2RTWidth;
			if (flag27)
			{
				ret += Utils.MakeAttribute("DiagLB2RTWidth", style.DiagLB2RTWidth.ToString());
			}
			bool flag28 = IncludeAll || Utils.Color2Str(style.Color) != Utils.Color2Str(defaultStyle.Color);
			if (flag28)
			{
				ret += Utils.MakeAttribute("Color", Utils.Color2Str(style.Color));
			}
			bool flag29 = IncludeAll || style.Transparent != defaultStyle.Transparent;
			if (flag29)
			{
				ret += Utils.MakeAttribute("Transparent", Utils.Bool2Str(style.Transparent));
			}
			bool flag30 = IncludeAll || style.Pattern != defaultStyle.Pattern;
			if (flag30)
			{
				ret += Utils.MakeAttribute("Pattern", style.Pattern.ToString());
			}
			bool flag31 = IncludeAll || style.PatternColor != defaultStyle.PatternColor;
			if (flag31)
			{
				ret += Utils.MakeAttribute("PatternColor", Utils.Color2Str(style.PatternColor));
			}
			bool flag32 = IncludeAll || style.HAlignment != defaultStyle.HAlignment;
			if (flag32)
			{
				ret += Utils.MakeAttribute("HAlignment", Utils.HAlignment2Str(style.HAlignment));
			}
			bool flag33 = IncludeAll || style.VAlignment != defaultStyle.VAlignment;
			if (flag33)
			{
				ret += Utils.MakeAttribute("VAlignment", Utils.VAlignment2Str(style.VAlignment));
			}
			bool flag34 = IncludeAll || style.LeftMargin != defaultStyle.LeftMargin;
			if (flag34)
			{
				ret += Utils.MakeAttribute("LeftMargin", style.LeftMargin.ToString());
			}
			bool flag35 = IncludeAll || style.TopMargin != defaultStyle.TopMargin;
			if (flag35)
			{
				ret += Utils.MakeAttribute("TopMargin", style.TopMargin.ToString());
			}
			bool flag36 = IncludeAll || style.RightMargin != defaultStyle.RightMargin;
			if (flag36)
			{
				ret += Utils.MakeAttribute("RightMargin", style.RightMargin.ToString());
			}
			bool flag37 = IncludeAll || style.BottomMargin != defaultStyle.BottomMargin;
			if (flag37)
			{
				ret += Utils.MakeAttribute("BottomMargin", style.BottomMargin.ToString());
			}
			bool flag38 = IncludeAll || style.ThreePartText != defaultStyle.ThreePartText;
			if (flag38)
			{
				ret += Utils.MakeAttribute("ThreePartText", Utils.Bool2Str(style.ThreePartText));
			}
			bool flag39 = IncludeAll || style.TextControl != defaultStyle.TextControl;
			if (flag39)
			{
				ret += Utils.MakeAttribute("TextControl", Utils.TextControl2Str(style.TextControl));
			}
			bool flag40 = IncludeAll || style.LineSpace != defaultStyle.LineSpace;
			if (flag40)
			{
				ret += Utils.MakeAttribute("LineSpace", style.LineSpace.ToString());
			}
			bool flag41 = IncludeAll || style.Locked != defaultStyle.Locked;
			if (flag41)
			{
				ret += Utils.MakeAttribute("Locked", Utils.Bool2Str(style.Locked));
			}
			bool flag42 = IncludeAll || style.Preview != defaultStyle.Preview;
			if (flag42)
			{
				ret += Utils.MakeAttribute("Preview", Utils.Bool2Str(style.Preview));
			}
			bool flag43 = IncludeAll || style.Print != defaultStyle.Print;
			if (flag43)
			{
				ret += Utils.MakeAttribute("Print", Utils.Bool2Str(style.Print));
			}
			bool flag44 = IncludeAll || style.SmallFontWordWrap != defaultStyle.SmallFontWordWrap;
			if (flag44)
			{
				ret += Utils.MakeAttribute("SmallFontWordWrap", Utils.Bool2Str(style.SmallFontWordWrap));
			}
			return ret;
		}

		public static void StyleRecInit(ref RdMergeCellsStyle styleRec)
		{
			styleRec.DataType = RdDataType.dtAuto;
			styleRec.FontName = "宋体";
			styleRec.FontSize = 9;
			styleRec.FontColor = Color.Black;
			styleRec.FontBold = false;
			styleRec.FontItalic = false;
			styleRec.FontUnderline = false;
			styleRec.FontStrikeout = false;
			styleRec.DisplayFormat = "";
			styleRec.LeftBorderStyle = RdLineStyle.lsThinSolid;
			styleRec.LeftBorderColor = Color.Black;
			styleRec.LeftBorderWidth = 0;
			styleRec.TopBorderStyle = RdLineStyle.lsThinSolid;
			styleRec.TopBorderColor = Color.Black;
			styleRec.TopBorderWidth = 0;
			styleRec.RightBorderStyle = RdLineStyle.lsThinSolid;
			styleRec.RightBorderColor = Color.Black;
			styleRec.RightBorderWidth = 0;
			styleRec.BottomBorderStyle = RdLineStyle.lsThinSolid;
			styleRec.BottomBorderColor = Color.Black;
			styleRec.BottomBorderWidth = 0;
			styleRec.DiagLT2RBStyle = RdLineStyle.lsNone;
			styleRec.DiagLT2RBColor = Color.Black;
			styleRec.DiagLT2RBWidth = 0;
			styleRec.DiagLB2RTStyle = RdLineStyle.lsNone;
			styleRec.DiagLB2RTColor = Color.Black;
			styleRec.DiagLB2RTWidth = 0;
			styleRec.Color = Color.White;
			styleRec.Transparent = false;
			styleRec.Pattern = 1;
			styleRec.PatternColor = Color.Black;
			styleRec.HAlignment = RdHAlignment.haAuto;
			styleRec.VAlignment = RdVAlignment.vaCenter;
			styleRec.LeftMargin = 39;
			styleRec.TopMargin = 39;
			styleRec.RightMargin = 39;
			styleRec.BottomMargin = 39;
			styleRec.ThreePartText = false;
			styleRec.TextControl = RdTextControl.tcCut;
			styleRec.LineSpace = 1;
			styleRec.Locked = false;
			styleRec.Preview = true;
			styleRec.Print = true;
			styleRec.SmallFontWordWrap = false;
		}

		public static RdDataType Str2DataType(string value, RdDataType defaultValue)
		{
			RdDataType result;
			if (!(value == "A"))
			{
				if (!(value == "C"))
				{
					if (!(value == "N"))
					{
						if (!(value == "F"))
						{
							result = defaultValue;
						}
						else
						{
							result = RdDataType.dtFormula;
						}
					}
					else
					{
						result = RdDataType.dtNumber;
					}
				}
				else
				{
					result = RdDataType.dtString;
				}
			}
			else
			{
				result = RdDataType.dtAuto;
			}
			return result;
		}

		public static string DataType2Str(RdDataType value)
		{
			string result = string.Empty;
			switch (value)
			{
			case RdDataType.dtAuto:
				result = "A";
				break;
			case RdDataType.dtString:
				result = "C";
				break;
			case RdDataType.dtNumber:
				result = "N";
				break;
			case RdDataType.dtFormula:
				result = "F";
				break;
			default:
				result = "A";
				break;
			}
			return result;
		}

		public static Color Str2Color(string value, Color defaultValue)
		{
			Color result;
			try
			{
				value = value.TrimStart(new char[]
				{
					'#'
				});
				value = Regex.Replace(value.ToLower(), "[g-zG-Z]", "");
				int length = value.Length;
				char[] rgb;
				int red;
				int green;
				int blue;
				if (length == 3)
				{
					rgb = value.ToCharArray();
					red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
					green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
					blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
					result = Color.FromArgb(red, green, blue);
					return result;
				}
				if (length != 6)
				{
					result = defaultValue;
					return result;
				}
				rgb = value.ToCharArray();
				red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
				green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
				blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
				result = Color.FromArgb(red, green, blue);
				return result;
			}
			catch
			{
			}
			result = defaultValue;
			return result;
		}

		public static string Color2Str(Color value)
		{
			string result = string.Empty;
			try
			{
				result = string.Format("#{0:X2}{1:X2}{2:X2}", value.R, value.G, value.B);
			}
			catch
			{
			}
			return result;
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

        public static RdLineStyle Str2LineStyle(string value, RdLineStyle defaultValue)
		{
			uint num = ComputeStringHash(value);
			RdLineStyle result;
			if (num <= 873244444u)
			{
				if (num <= 822911587u)
				{
					if (num != 806133968u)
					{
						if (num == 822911587u)
						{
							if (value == "4")
							{
								result = RdLineStyle.lsThinDashDot;
								return result;
							}
						}
					}
					else if (value == "5")
					{
						result = RdLineStyle.lsThickDashDotDot;
						return result;
					}
				}
				else if (num != 839689206u)
				{
					if (num != 856466825u)
					{
						if (num == 873244444u)
						{
							if (value == "1")
							{
								result = RdLineStyle.lsThinSolid;
								return result;
							}
						}
					}
					else if (value == "6")
					{
						result = RdLineStyle.lsThickSolid;
						return result;
					}
				}
				else if (value == "7")
				{
					result = RdLineStyle.lsThickDash;
					return result;
				}
			}
			else if (num <= 923577301u)
			{
				if (num != 890022063u)
				{
					if (num != 906799682u)
					{
						if (num == 923577301u)
						{
							if (value == "2")
							{
								result = RdLineStyle.lsThinDash;
								return result;
							}
						}
					}
					else if (value == "3")
					{
						result = RdLineStyle.lsThinDot;
						return result;
					}
				}
				else if (value == "0")
				{
					result = RdLineStyle.lsNone;
					return result;
				}
			}
			else if (num != 1007465396u)
			{
				if (num != 1024243015u)
				{
					if (num == 3289118412u)
					{
						if (value == "A")
						{
							result = RdLineStyle.lsThickDashDotDot;
							return result;
						}
					}
				}
				else if (value == "8")
				{
					result = RdLineStyle.lsThickDot;
					return result;
				}
			}
			else if (value == "9")
			{
				result = RdLineStyle.lsThickDashDot;
				return result;
			}
			result = defaultValue;
			return result;
		}

		public static string LineStyle2Str(RdLineStyle value)
		{
			string result = string.Empty;
			switch (value)
			{
			case RdLineStyle.lsNone:
				result = "0";
				break;
			case RdLineStyle.lsThinSolid:
				result = "1";
				break;
			case RdLineStyle.lsThinDash:
				result = "2";
				break;
			case RdLineStyle.lsThinDot:
				result = "3";
				break;
			case RdLineStyle.lsThinDashDot:
				result = "4";
				break;
			case RdLineStyle.lsThinDashDotDot:
				result = "5";
				break;
			case RdLineStyle.lsThickSolid:
				result = "6";
				break;
			case RdLineStyle.lsThickDash:
				result = "7";
				break;
			case RdLineStyle.lsThickDot:
				result = "8";
				break;
			case RdLineStyle.lsThickDashDot:
				result = "9";
				break;
			case RdLineStyle.lsThickDashDotDot:
				result = "A";
				break;
			}
			return result;
		}

		public static RdHAlignment Str2HAlignment(string value, RdHAlignment defaultValue)
		{
			RdHAlignment result;
			if (!(value == "A"))
			{
				if (!(value == "L"))
				{
					if (!(value == "C"))
					{
						if (!(value == "R"))
						{
							if (!(value == "E"))
							{
								result = defaultValue;
							}
							else
							{
								result = RdHAlignment.haEdge;
							}
						}
						else
						{
							result = RdHAlignment.haRight;
						}
					}
					else
					{
						result = RdHAlignment.haCenter;
					}
				}
				else
				{
					result = RdHAlignment.haLeft;
				}
			}
			else
			{
				result = RdHAlignment.haAuto;
			}
			return result;
		}

		public static string HAlignment2Str(RdHAlignment hAlignment)
		{
			string result = string.Empty;
			switch (hAlignment)
			{
			case RdHAlignment.haAuto:
				result = "A";
				break;
			case RdHAlignment.haLeft:
				result = "L";
				break;
			case RdHAlignment.haCenter:
				result = "C";
				break;
			case RdHAlignment.haRight:
				result = "R";
				break;
			case RdHAlignment.haEdge:
				result = "E";
				break;
			}
			return result;
		}

		public static RdVAlignment Str2VAlignment(string value, RdVAlignment defaultValue)
		{
			RdVAlignment result;
			if (!(value == "T"))
			{
				if (!(value == "C"))
				{
					if (!(value == "B"))
					{
						result = defaultValue;
					}
					else
					{
						result = RdVAlignment.vaBottom;
					}
				}
				else
				{
					result = RdVAlignment.vaCenter;
				}
			}
			else
			{
				result = RdVAlignment.vaTop;
			}
			return result;
		}

		public static string VAlignment2Str(RdVAlignment vAlignment)
		{
			string result = string.Empty;
			switch (vAlignment)
			{
			case RdVAlignment.vaTop:
				result = "T";
				break;
			case RdVAlignment.vaCenter:
				result = "C";
				break;
			case RdVAlignment.vaBottom:
				result = "B";
				break;
			}
			return result;
		}

		public static RdTextControl Str2TextControl(string value, RdTextControl defaultValue)
		{
			RdTextControl result;
			if (!(value == "C"))
			{
				if (!(value == "W"))
				{
					if (!(value == "S"))
					{
						result = defaultValue;
					}
					else
					{
						result = RdTextControl.tcSmallFont;
					}
				}
				else
				{
					result = RdTextControl.tcWordWrap;
				}
			}
			else
			{
				result = RdTextControl.tcCut;
			}
			return result;
		}

		public static string TextControl2Str(RdTextControl value)
		{
			string result = string.Empty;
			switch (value)
			{
			case RdTextControl.tcCut:
				result = "C";
				break;
			case RdTextControl.tcWordWrap:
				result = "W";
				break;
			case RdTextControl.tcSmallFont:
				result = "S";
				break;
			}
			return result;
		}
	}
}
