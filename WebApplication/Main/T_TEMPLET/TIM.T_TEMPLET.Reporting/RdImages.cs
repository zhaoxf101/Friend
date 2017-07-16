using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdImages
	{
		private RdDocument m_document = null;

		private Dictionary<string, RdImage> m_list = null;

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

		internal Dictionary<string, RdImage> List
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

		public RdImages(RdDocument document)
		{
			this.m_document = document;
			this.m_list = new Dictionary<string, RdImage>();
		}

		public void Clear()
		{
			this.m_list.Clear();
		}

		public void Load(XmlNode node)
		{
			bool hasChildNodes = node.HasChildNodes;
			if (hasChildNodes)
			{
				for (XmlNode imageNode = node.FirstChild; imageNode != null; imageNode = imageNode.NextSibling)
				{
					bool flag = imageNode.Name == "Image";
					if (flag)
					{
						RdImage gtrImage = new RdImage(this.m_document);
						gtrImage.Load(imageNode);
						this.m_list.Add(gtrImage.Name, gtrImage);
						this.Document.Changed();
					}
				}
			}
		}

		public string GetXml()
		{
			string result = string.Empty;
			foreach (KeyValuePair<string, RdImage> value in this.m_list)
			{
				result += value.Value.GetXml();
			}
			return result = "<Images>" + result + "</Images>";
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
			result.Left = computeCell.Column;
			result.Top = computeCell.Row;
			result.Right = computeCell.Column + computeCell.Width - 1;
			result.Bottom = computeCell.Row + computeCell.Height - 1;
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
				computeCell.SetHAlignment(RdHAlignment.haRight);
				computeCell.SetVAlignment(RdVAlignment.vaTop);
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
