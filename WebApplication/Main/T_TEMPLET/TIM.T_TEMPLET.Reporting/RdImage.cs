using System;
using System.Drawing;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdImage : RdNode
	{
		private int m_left = 0;

		private int m_top = 0;

		private int m_right = 0;

		private int m_bottom = 0;

		private int m_width = 0;

		private int m_height = 0;

		private string m_grdphicStr = string.Empty;

		private Graphics m_graphic = null;

		private bool m_preview = true;

		private bool m_print = true;

		private RdHAlignment m_hAlignment = RdHAlignment.haCenter;

		private RdVAlignment m_vAlignment = RdVAlignment.vaCenter;

		private bool m_transparent = false;

		private RdImageControl m_imageControl = RdImageControl.icAlign;

		private RdImageType m_type = RdImageType.itBitmap;

		private int m_xDPI = 72;

		private int m_yDPI = 72;

		private Bitmap m_imageBitmap = null;

		private int m_bitmapXDPI = 0;

		private int m_bitmapYDPI = 0;

		public int Left
		{
			get
			{
				return this.m_left;
			}
			set
			{
				bool flag = this.m_left != value;
				if (flag)
				{
					this.m_left = value;
					this.DoOnLeftChanged();
				}
			}
		}

		public int Top
		{
			get
			{
				return this.m_top;
			}
			set
			{
				bool flag = this.m_top != value;
				if (flag)
				{
					this.m_top = value;
					this.DoOnTopChanged();
				}
			}
		}

		public int Right
		{
			get
			{
				return this.m_right;
			}
			set
			{
				bool flag = this.m_right != value;
				if (flag)
				{
					this.m_right = value;
					this.DoOnRightChanged();
				}
			}
		}

		public int Bottom
		{
			get
			{
				return this.m_bottom;
			}
			set
			{
				bool flag = this.m_bottom != value;
				if (flag)
				{
					this.m_bottom = value;
					this.DoOnBottomChanged();
				}
			}
		}

		public int Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				bool flag = this.m_width != value;
				if (flag)
				{
					this.m_width = value;
					this.DoOnWidthChanged();
				}
			}
		}

		public int Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				bool flag = this.m_height != value;
				if (flag)
				{
					this.m_height = value;
					this.DoOnHeightChanged();
				}
			}
		}

		public string GrdphicStr
		{
			get
			{
				return this.m_grdphicStr;
			}
			set
			{
				this.m_grdphicStr = value;
			}
		}

		public Graphics Graphic
		{
			get
			{
				return this.m_graphic;
			}
			set
			{
				this.m_graphic = value;
			}
		}

		public bool Preview
		{
			get
			{
				return this.m_preview;
			}
			set
			{
				bool flag = this.m_preview != value;
				if (flag)
				{
					this.m_preview = value;
					this.DoOnPreviewChanged();
				}
			}
		}

		public bool Print
		{
			get
			{
				return this.m_print;
			}
			set
			{
				bool flag = this.m_preview != value;
				if (flag)
				{
					this.m_print = value;
					this.DoOnPrintChanged();
				}
			}
		}

		public RdHAlignment HAlignment
		{
			get
			{
				return this.m_hAlignment;
			}
			set
			{
				bool flag = this.m_hAlignment != value;
				if (flag)
				{
					this.m_hAlignment = value;
					this.DoOnHAlignmentChanged();
				}
			}
		}

		public RdVAlignment VAlignment
		{
			get
			{
				return this.m_vAlignment;
			}
			set
			{
				bool flag = this.m_vAlignment != value;
				if (flag)
				{
					this.m_vAlignment = value;
					this.DoOnVAlignmentChanged();
				}
			}
		}

		public bool Transparent
		{
			get
			{
				return this.m_transparent;
			}
			set
			{
				bool flag = this.m_transparent != value;
				if (flag)
				{
					this.m_transparent = value;
					this.DoOnTransparentChanged();
				}
			}
		}

		public RdImageControl ImageControl
		{
			get
			{
				return this.m_imageControl;
			}
			set
			{
				bool flag = this.m_imageControl != value;
				if (flag)
				{
					this.m_imageControl = value;
					this.DoOnImageControlChanged();
				}
			}
		}

		public RdImageType Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		public int XDPI
		{
			get
			{
				return this.m_xDPI;
			}
			set
			{
				bool flag = this.m_xDPI != value;
				if (flag)
				{
					this.m_xDPI = value;
					this.DoOnXDPIChanged();
				}
			}
		}

		public int YDPI
		{
			get
			{
				return this.m_yDPI;
			}
			set
			{
				bool flag = this.m_yDPI != value;
				if (flag)
				{
					this.m_yDPI = value;
					this.DoOnYDPIChanged();
				}
			}
		}

		public Bitmap ImageBitmap
		{
			get
			{
				return this.m_imageBitmap;
			}
			set
			{
				this.m_imageBitmap = value;
			}
		}

		public int BitmapXDPI
		{
			get
			{
				return this.m_bitmapXDPI;
			}
			set
			{
				this.m_bitmapXDPI = value;
			}
		}

		public int BitmapYDPI
		{
			get
			{
				return this.m_bitmapYDPI;
			}
			set
			{
				this.m_bitmapYDPI = value;
			}
		}

		private void DoOnLeftChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTopChanged()
		{
			base.Document.Changed();
		}

		private void DoOnRightChanged()
		{
			base.Document.Changed();
		}

		private void DoOnBottomChanged()
		{
			base.Document.Changed();
		}

		private void DoOnWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnHeightChanged()
		{
			base.Document.Changed();
		}

		private void DoOnGraphicChanged()
		{
			base.Document.Changed();
		}

		private void DoOnPreviewChanged()
		{
			base.Document.Changed();
		}

		private void DoOnPrintChanged()
		{
			base.Document.Changed();
		}

		private void DoOnHAlignmentChanged()
		{
			base.Document.Changed();
		}

		private void DoOnVAlignmentChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTransparentChanged()
		{
			base.Document.Changed();
		}

		private void DoOnImageControlChanged()
		{
			base.Document.Changed();
		}

		private void DoOnImageTypeChanged()
		{
			base.Document.Changed();
		}

		private void DoOnXDPIChanged()
		{
			base.Document.Changed();
		}

		private void DoOnYDPIChanged()
		{
			base.Document.Changed();
		}

		public RdImage(RdDocument document) : base(document)
		{
		}

		internal override void Load(XmlNode node)
		{
			base.Load(node);
			this.m_left = Utils.GetAttrInt(node, "Left", 0);
			this.m_top = Utils.GetAttrInt(node, "Top", 0);
			this.m_right = Utils.GetAttrInt(node, "Right", 0);
			this.m_bottom = Utils.GetAttrInt(node, "Bottom", 0);
			this.m_type = Utils.Str2ImageType(Utils.GetXmlNodeAttribute(node, "ImageType"), RdImageType.itBitmap);
			this.m_grdphicStr = Utils.GetAttrString(node, "Graphic", string.Empty);
			this.m_print = Utils.GetAttrBool(node, "Print", true);
			this.m_preview = Utils.GetAttrBool(node, "Preview", true);
			this.m_hAlignment = Utils.Str2HAlignment(Utils.GetXmlNodeAttribute(node, "HAlignment"), RdHAlignment.haCenter);
			this.m_vAlignment = Utils.Str2VAlignment(Utils.GetXmlNodeAttribute(node, "VAlignment"), RdVAlignment.vaCenter);
			this.m_transparent = Utils.GetAttrBool(node, "Transparent", false);
			this.m_imageControl = Utils.Str2ImageControl(Utils.GetXmlNodeAttribute(node, "ImageControl"), RdImageControl.icAlign);
			this.m_xDPI = Utils.GetAttrInt(node, "XDPI", 72);
			this.m_yDPI = Utils.GetAttrInt(node, "YDPI", 72);
			this.m_width = Utils.GetAttrInt(node, "Width", 0);
			this.m_height = Utils.GetAttrInt(node, "Height", 0);
		}

		internal override string GetAttributes()
		{
			string result = base.GetAttributes();
			result = string.Concat(new string[]
			{
				result,
				Utils.MakeAttribute("Left", this.m_left.ToString()),
				Utils.MakeAttribute("Top", this.m_top.ToString()),
				Utils.MakeAttribute("Right", this.m_right.ToString()),
				Utils.MakeAttribute("Bottom", this.m_bottom.ToString())
			});
			bool flag = !string.IsNullOrEmpty(this.m_grdphicStr);
			if (flag)
			{
				result += Utils.MakeAttribute("Graphic", this.m_grdphicStr);
			}
			bool flag2 = !this.m_print;
			if (flag2)
			{
				result += Utils.MakeAttribute("Print", Utils.Bool2Str(this.m_print));
			}
			bool flag3 = !this.m_preview;
			if (flag3)
			{
				result += Utils.MakeAttribute("Preview", Utils.Bool2Str(this.m_preview));
			}
			bool flag4 = this.m_hAlignment != RdHAlignment.haCenter;
			if (flag4)
			{
				result += Utils.MakeAttribute("HAlignment", Utils.HAlignment2Str(this.m_hAlignment));
			}
			bool flag5 = this.m_vAlignment != RdVAlignment.vaCenter;
			if (flag5)
			{
				result += Utils.MakeAttribute("VAlignment", Utils.VAlignment2Str(this.m_vAlignment));
			}
			bool transparent = this.m_transparent;
			if (transparent)
			{
				result += Utils.MakeAttribute("Transparent", Utils.Bool2Str(this.m_transparent));
			}
			bool flag6 = this.m_imageControl > RdImageControl.icAlign;
			if (flag6)
			{
				result += Utils.MakeAttribute("ImageControl", Utils.ImageControl2Str(this.m_imageControl));
			}
			bool flag7 = this.m_type > RdImageType.itBitmap;
			if (flag7)
			{
				result += Utils.MakeAttribute("ImageType", Utils.ImageType2Str(this.m_type));
			}
			bool flag8 = this.m_xDPI != 72;
			if (flag8)
			{
				result += Utils.MakeAttribute("XDPI", this.m_xDPI.ToString());
			}
			bool flag9 = this.m_yDPI != 72;
			if (flag9)
			{
				result += Utils.MakeAttribute("YDPI", this.m_yDPI.ToString());
			}
			bool flag10 = this.m_width != 0;
			if (flag10)
			{
				result += Utils.MakeAttribute("Width", this.m_width.ToString());
			}
			bool flag11 = this.m_height != 0;
			if (flag11)
			{
				result += Utils.MakeAttribute("Height", this.m_height.ToString());
			}
			return result;
		}

		internal override string GetXml()
		{
			string result = base.GetXml();
			return result + "<Image" + this.GetAttributes() + "/>";
		}
	}
}
