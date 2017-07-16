using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RmImages : RmSetNode
	{
		private Dictionary<string, RmImage> m_items = null;

		internal Dictionary<string, RmImage> Items
		{
			get
			{
				return this.m_items;
			}
			set
			{
				this.m_items = value;
			}
		}

		public RmImages(RmReportingMaker builder) : base(builder)
		{
			this.m_items = new Dictionary<string, RmImage>();
		}

		public void Clear()
		{
			this.Items.Clear();
		}

		internal RdImage NewImage(RdDocument doc, RdCell computeCell, ArrayList paramList)
		{
			RdImage result = new RdImage(doc);
			RdCell curCell = (RdCell)computeCell.Data;
			result.Left = computeCell.Column;
			result.Top = computeCell.Row;
			result.Right = computeCell.Column + curCell.Width - 1;
			result.Bottom = computeCell.Row + curCell.Height - 1;
			string imgPath = string.Empty;
			string imgAlignment = string.Empty;
			bool flag = paramList.Count == 1;
			if (flag)
			{
				imgPath = paramList[0].ToString();
			}
			else
			{
				bool flag2 = paramList.Count == 2;
				if (flag2)
				{
					imgPath = paramList[0].ToString();
					imgAlignment = paramList[1].ToString();
				}
			}
			bool flag3 = string.IsNullOrEmpty(imgPath);
			RdImage result2;
			if (flag3)
			{
				result2 = null;
			}
			else
			{
				Image img = Image.FromFile(imgPath);
				result.Name = imgPath.Substring(imgPath.LastIndexOf('\\') + 1);
				result.Width = img.Width;
				result.Height = img.Height;
				result.Preview = true;
				result.Print = true;
				byte[] graphic = File.ReadAllBytes(imgPath);
				result.GrdphicStr = Convert.ToBase64String(graphic);
				string a = imgAlignment;
				if (!(a == "L"))
				{
					if (!(a == "R"))
					{
						if (!(a == "C"))
						{
							result.HAlignment = RdHAlignment.haCenter;
							result.ImageControl = RdImageControl.icStretch;
						}
						else
						{
							result.HAlignment = RdHAlignment.haCenter;
							result.ImageControl = RdImageControl.icAlign;
						}
					}
					else
					{
						result.HAlignment = RdHAlignment.haRight;
						result.ImageControl = RdImageControl.icAlign;
					}
				}
				else
				{
					result.HAlignment = RdHAlignment.haLeft;
					result.ImageControl = RdImageControl.icAlign;
				}
				result.VAlignment = RdVAlignment.vaCenter;
				result.Transparent = false;
				bool flag4 = img.RawFormat.Guid == ImageFormat.Bmp.Guid;
				if (flag4)
				{
					result.Type = RdImageType.itBitmap;
				}
				else
				{
					bool flag5 = img.RawFormat.Guid == ImageFormat.Emf.Guid;
					if (flag5)
					{
						result.Type = RdImageType.itEMF;
					}
					else
					{
						result.Type = RdImageType.itJPEG;
					}
				}
				curCell.SetHAlignment(RdHAlignment.haRight);
				curCell.SetVAlignment(RdVAlignment.vaTop);
				result2 = result;
			}
			return result2;
		}

		internal RdImage NewImage(RdDocument doc, RdCell computeCell, ArrayList paramList, byte[] graphicValue)
		{
			RdImage result = new RdImage(doc);
			RdCell curCell = (RdCell)computeCell.Data;
			result.Left = computeCell.Column;
			result.Top = computeCell.Row;
			result.Right = computeCell.Column + curCell.Width - 1;
			result.Bottom = computeCell.Row + curCell.Height - 1;
			string imgPath = string.Empty;
			string imgAlignment = string.Empty;
			bool flag = paramList.Count == 1;
			if (flag)
			{
				imgPath = paramList[0].ToString();
			}
			else
			{
				bool flag2 = paramList.Count == 2;
				if (flag2)
				{
					imgPath = paramList[0].ToString();
					imgAlignment = paramList[1].ToString();
				}
			}
			bool flag3 = string.IsNullOrEmpty(imgPath);
			RdImage result2;
			if (flag3)
			{
				result2 = null;
			}
			else
			{
				result.Name = imgPath.Substring(imgPath.LastIndexOf('\\') + 1);
				result.Width = 532;
				result.Height = 333;
				result.Preview = true;
				result.Print = true;
				result.GrdphicStr = Convert.ToBase64String(graphicValue);
				string a = imgAlignment;
				if (!(a == "L"))
				{
					if (!(a == "R"))
					{
						if (!(a == "C"))
						{
							result.HAlignment = RdHAlignment.haCenter;
							result.ImageControl = RdImageControl.icStretch;
						}
						else
						{
							result.HAlignment = RdHAlignment.haCenter;
							result.ImageControl = RdImageControl.icAlign;
						}
					}
					else
					{
						result.HAlignment = RdHAlignment.haRight;
						result.ImageControl = RdImageControl.icAlign;
					}
				}
				else
				{
					result.HAlignment = RdHAlignment.haLeft;
					result.ImageControl = RdImageControl.icAlign;
				}
				result.VAlignment = RdVAlignment.vaCenter;
				result.Transparent = false;
				result.Type = RdImageType.itEMF;
				curCell.SetHAlignment(RdHAlignment.haRight);
				curCell.SetVAlignment(RdVAlignment.vaTop);
				result2 = result;
			}
			return result2;
		}

		internal RdImage GenChartImage(RdDocument doc, RdCell computeCell, ArrayList paramList)
		{
			return null;
		}
	}
}
