using System;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdPaper
	{
		private int m_paper = 9;

		private string m_name = string.Empty;

		private int m_width = 0;

		private int m_height = 0;

		public int Paper
		{
			get
			{
				return this.m_paper;
			}
			set
			{
				this.m_paper = value;
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

		public int Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
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
				this.m_height = value;
			}
		}

		public RdPaper(int paper, string paperName, int paperWidth, int paperHeight)
		{
			this.m_paper = paper;
			this.m_name = paperName;
			this.m_width = paperWidth;
			this.m_height = paperHeight;
		}
	}
}
