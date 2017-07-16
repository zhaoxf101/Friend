using System;
using System.Drawing;

namespace TIM.T_TEMPLET.Reporting
{
	public class RdMergeCellsStyle
	{
		private RdDataType m_dataType;

		private string m_displayFormat;

		private string m_fontName;

		private int m_fontSize;

		private Color m_fontColor;

		private bool m_fontBold;

		private bool m_fontItalic;

		private bool m_fontUnderline;

		private bool m_fontStrikeout;

		private RdLineStyle m_leftBorderStyle;

		private Color m_leftBorderColor;

		private int m_leftBorderWidth;

		private RdLineStyle m_topBorderStyle;

		private Color m_topBorderColor;

		private int m_topBorderWidth;

		private RdLineStyle m_rightBorderStyle;

		private Color m_rightBorderColor;

		private int m_rightBorderWidth;

		private RdLineStyle m_bottomBorderStyle;

		private Color m_bottomBorderColor;

		private int m_bottomBorderWidth;

		private RdLineStyle m_diagLT2RBStyle;

		private Color m_diagLT2RBColor;

		private int m_diagLT2RBWidth;

		private RdLineStyle m_diagLB2RTStyle;

		private Color m_diagLB2RTColor;

		private int m_diagLB2RTWidth;

		private Color m_color = Color.White;

		private bool m_transparent;

		private int m_pattern;

		private Color m_patternColor;

		private RdHAlignment m_hAlignment;

		private RdVAlignment m_vAlignment;

		private int m_leftMargin;

		private int m_topMargin;

		private int m_rightMargin;

		private int m_bottomMargin;

		private bool m_threePartText;

		private RdTextControl m_textControl;

		private int m_lineSpace;

		private bool m_preview;

		private bool m_print;

		private bool m_locked;

		private bool m_smallFontWordWrap;

		internal RdDataType DataType
		{
			get
			{
				return this.m_dataType;
			}
			set
			{
				this.m_dataType = value;
			}
		}

		internal string DisplayFormat
		{
			get
			{
				return this.m_displayFormat;
			}
			set
			{
				this.m_displayFormat = value;
			}
		}

		internal string FontName
		{
			get
			{
				return this.m_fontName;
			}
			set
			{
				this.m_fontName = value;
			}
		}

		internal int FontSize
		{
			get
			{
				return this.m_fontSize;
			}
			set
			{
				this.m_fontSize = value;
			}
		}

		internal Color FontColor
		{
			get
			{
				return this.m_fontColor;
			}
			set
			{
				this.m_fontColor = value;
			}
		}

		internal bool FontBold
		{
			get
			{
				return this.m_fontBold;
			}
			set
			{
				this.m_fontBold = value;
			}
		}

		internal bool FontItalic
		{
			get
			{
				return this.m_fontItalic;
			}
			set
			{
				this.m_fontItalic = value;
			}
		}

		internal bool FontUnderline
		{
			get
			{
				return this.m_fontUnderline;
			}
			set
			{
				this.m_fontUnderline = value;
			}
		}

		internal bool FontStrikeout
		{
			get
			{
				return this.m_fontStrikeout;
			}
			set
			{
				this.m_fontStrikeout = value;
			}
		}

		public RdLineStyle LeftBorderStyle
		{
			get
			{
				return this.m_leftBorderStyle;
			}
			set
			{
				this.m_leftBorderStyle = value;
			}
		}

		internal Color LeftBorderColor
		{
			get
			{
				return this.m_leftBorderColor;
			}
			set
			{
				this.m_leftBorderColor = value;
			}
		}

		internal int LeftBorderWidth
		{
			get
			{
				return this.m_leftBorderWidth;
			}
			set
			{
				this.m_leftBorderWidth = value;
			}
		}

		public RdLineStyle TopBorderStyle
		{
			get
			{
				return this.m_topBorderStyle;
			}
			set
			{
				this.m_topBorderStyle = value;
			}
		}

		internal Color TopBorderColor
		{
			get
			{
				return this.m_topBorderColor;
			}
			set
			{
				this.m_topBorderColor = value;
			}
		}

		internal int TopBorderWidth
		{
			get
			{
				return this.m_topBorderWidth;
			}
			set
			{
				this.m_topBorderWidth = value;
			}
		}

		public RdLineStyle RightBorderStyle
		{
			get
			{
				return this.m_rightBorderStyle;
			}
			set
			{
				this.m_rightBorderStyle = value;
			}
		}

		internal Color RightBorderColor
		{
			get
			{
				return this.m_rightBorderColor;
			}
			set
			{
				this.m_rightBorderColor = value;
			}
		}

		internal int RightBorderWidth
		{
			get
			{
				return this.m_rightBorderWidth;
			}
			set
			{
				this.m_rightBorderWidth = value;
			}
		}

		public RdLineStyle BottomBorderStyle
		{
			get
			{
				return this.m_bottomBorderStyle;
			}
			set
			{
				this.m_bottomBorderStyle = value;
			}
		}

		internal Color BottomBorderColor
		{
			get
			{
				return this.m_bottomBorderColor;
			}
			set
			{
				this.m_bottomBorderColor = value;
			}
		}

		internal int BottomBorderWidth
		{
			get
			{
				return this.m_bottomBorderWidth;
			}
			set
			{
				this.m_bottomBorderWidth = value;
			}
		}

		internal RdLineStyle DiagLT2RBStyle
		{
			get
			{
				return this.m_diagLT2RBStyle;
			}
			set
			{
				this.m_diagLT2RBStyle = value;
			}
		}

		internal Color DiagLT2RBColor
		{
			get
			{
				return this.m_diagLT2RBColor;
			}
			set
			{
				this.m_diagLT2RBColor = value;
			}
		}

		internal int DiagLT2RBWidth
		{
			get
			{
				return this.m_diagLT2RBWidth;
			}
			set
			{
				this.m_diagLT2RBWidth = value;
			}
		}

		internal RdLineStyle DiagLB2RTStyle
		{
			get
			{
				return this.m_diagLB2RTStyle;
			}
			set
			{
				this.m_diagLB2RTStyle = value;
			}
		}

		internal Color DiagLB2RTColor
		{
			get
			{
				return this.m_diagLB2RTColor;
			}
			set
			{
				this.m_diagLB2RTColor = value;
			}
		}

		internal int DiagLB2RTWidth
		{
			get
			{
				return this.m_diagLB2RTWidth;
			}
			set
			{
				this.m_diagLB2RTWidth = value;
			}
		}

		internal Color Color
		{
			get
			{
				return this.m_color;
			}
			set
			{
				this.m_color = value;
			}
		}

		internal bool Transparent
		{
			get
			{
				return this.m_transparent;
			}
			set
			{
				this.m_transparent = value;
			}
		}

		internal int Pattern
		{
			get
			{
				return this.m_pattern;
			}
			set
			{
				this.m_pattern = value;
			}
		}

		internal Color PatternColor
		{
			get
			{
				return this.m_patternColor;
			}
			set
			{
				this.m_patternColor = value;
			}
		}

		internal RdHAlignment HAlignment
		{
			get
			{
				return this.m_hAlignment;
			}
			set
			{
				this.m_hAlignment = value;
			}
		}

		internal RdVAlignment VAlignment
		{
			get
			{
				return this.m_vAlignment;
			}
			set
			{
				this.m_vAlignment = value;
			}
		}

		internal int LeftMargin
		{
			get
			{
				return this.m_leftMargin;
			}
			set
			{
				this.m_leftMargin = value;
			}
		}

		internal int TopMargin
		{
			get
			{
				return this.m_topMargin;
			}
			set
			{
				this.m_topMargin = value;
			}
		}

		internal int RightMargin
		{
			get
			{
				return this.m_rightMargin;
			}
			set
			{
				this.m_rightMargin = value;
			}
		}

		internal int BottomMargin
		{
			get
			{
				return this.m_bottomMargin;
			}
			set
			{
				this.m_bottomMargin = value;
			}
		}

		internal bool ThreePartText
		{
			get
			{
				return this.m_threePartText;
			}
			set
			{
				this.m_threePartText = value;
			}
		}

		internal RdTextControl TextControl
		{
			get
			{
				return this.m_textControl;
			}
			set
			{
				this.m_textControl = value;
			}
		}

		internal int LineSpace
		{
			get
			{
				return this.m_lineSpace;
			}
			set
			{
				this.m_lineSpace = value;
			}
		}

		internal bool Preview
		{
			get
			{
				return this.m_preview;
			}
			set
			{
				this.m_preview = value;
			}
		}

		internal bool Print
		{
			get
			{
				return this.m_print;
			}
			set
			{
				this.m_print = value;
			}
		}

		internal bool Locked
		{
			get
			{
				return this.m_locked;
			}
			set
			{
				this.m_locked = value;
			}
		}

		internal bool SmallFontWordWrap
		{
			get
			{
				return this.m_smallFontWordWrap;
			}
			set
			{
				this.m_smallFontWordWrap = value;
			}
		}
	}
}
