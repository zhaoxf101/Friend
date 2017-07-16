using System;

namespace TIM.T_KERNEL.Helper
{
	public struct Options
	{
		public int Width
		{
			get;
			set;
		}

		public int Height
		{
			get;
			set;
		}

		public QRErrorCorrectLevel CorrectLevel
		{
			get;
			set;
		}

		public int TypeNumber
		{
			get;
			set;
		}

		public string Text
		{
			get;
			set;
		}

		public Options(string text)
		{
			this = default(Options);
			this.Width = 256;
			this.Height = 256;
			this.TypeNumber = -1;
			this.CorrectLevel = QRErrorCorrectLevel.H;
			this.Text = text;
		}
	}
}
