using System;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RmImage
	{
		private RmImages m_images = null;

		private string m_fileName = string.Empty;

		private RdImageType m_graphicType = RdImageType.itBitmap;

		private int m_xDPI = 0;

		private int m_yDPI = 0;

		private RdCell m_cell = null;

		private bool m_tempFile = false;

		private RdImageAlignment m_imageAlignment = RdImageAlignment.ihCenter;

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

		public string FileName
		{
			get
			{
				return this.m_fileName;
			}
			set
			{
				this.m_fileName = value;
			}
		}

		public RdImageType GraphicType
		{
			get
			{
				return this.m_graphicType;
			}
			set
			{
				this.m_graphicType = value;
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
				this.m_xDPI = value;
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
				this.m_yDPI = value;
			}
		}

		public RdCell Cell
		{
			get
			{
				return this.m_cell;
			}
			set
			{
				this.m_cell = value;
			}
		}

		public bool TempFile
		{
			get
			{
				return this.m_tempFile;
			}
			set
			{
				this.m_tempFile = value;
			}
		}

		public RdImageAlignment ImageAlignment
		{
			get
			{
				return this.m_imageAlignment;
			}
			set
			{
				this.m_imageAlignment = value;
			}
		}
	}
}
