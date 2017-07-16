using System;
using System.Drawing;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	public class RdContentNode : RdNode
	{
		private RdMergeCellsStyle m_style;

		public RdMergeCellsStyle Style
		{
			get
			{
				return this.m_style;
			}
			set
			{
				this.m_style = value;
			}
		}

		private void DoOnDataTypeChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDisplayFormatChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontNameChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontSizeChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontBoldChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontItalicChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontUnderlineChanged()
		{
			base.Document.Changed();
		}

		private void DoOnFontStrikeoutChanged()
		{
			base.Document.Changed();
		}

		private void DoOnCellBordersChanged()
		{
			base.Document.Changed();
		}

		private void DoOnLeftBorderStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnLeftBorderColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnLeftBorderWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTopBorderStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTopBorderColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTopBorderWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnRightBorderStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnRightBorderColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnRightBorderWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnBottomBorderStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnBottomBorderColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnBottomBorderWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLT2RBStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLT2RBColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLT2RBWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLB2RTStyleChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLB2RTColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnDiagLB2RTWidthChanged()
		{
			base.Document.Changed();
		}

		private void DoOnColorChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTransparentChanged()
		{
			base.Document.Changed();
		}

		private void DoOnPatternChanged()
		{
			base.Document.Changed();
		}

		private void DoOnPatternColorChanged()
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

		private void DoOnLeftMarginChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTopMarginChanged()
		{
			base.Document.Changed();
		}

		private void DoOnRightMarginChanged()
		{
			base.Document.Changed();
		}

		private void DoOnBottomMarginChanged()
		{
			base.Document.Changed();
		}

		private void DoOnThreePartTextChanged()
		{
			base.Document.Changed();
		}

		private void DoOnTextControlChanged()
		{
			base.Document.Changed();
		}

		private void DoOnLineSpaceChanged()
		{
			base.Document.Changed();
		}

		private void DoOnLockedChanged()
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

		private void DoOnSmallFontWordWrapChanged()
		{
			base.Document.Changed();
		}

		internal RdContentNode(RdDocument document) : base(document)
		{
			RdMergeCellsStyle tmpStyle = new RdMergeCellsStyle();
			Utils.CopyStyleRec(ref tmpStyle, document.DefaultStyle);
			this.m_style = tmpStyle;
		}

		internal override void Load(XmlNode node)
		{
			base.Load(node);
			this.m_style = new RdMergeCellsStyle();
			Utils.StyleRecLoad(node, ref this.m_style, base.Document.DefaultStyle);
		}

		internal void SetDataType(RdDataType value)
		{
			this.m_style.DataType = value;
			this.DoOnDataTypeChanged();
		}

		internal void SetDisplayFormat(string value)
		{
			this.m_style.DisplayFormat = value;
			this.DoOnDisplayFormatChanged();
		}

		internal void SetFontName(string value)
		{
			this.m_style.FontName = value;
			this.DoOnFontNameChanged();
		}

		internal void SetFontSize(int value)
		{
			this.m_style.FontSize = value;
			this.DoOnFontSizeChanged();
		}

		internal void SetFontColor(Color value)
		{
			this.m_style.FontColor = value;
			this.DoOnFontColorChanged();
		}

		internal void SetFontBold(bool value)
		{
			this.m_style.FontBold = value;
			this.DoOnFontBoldChanged();
		}

		internal void SetFontItalic(bool value)
		{
			this.m_style.FontItalic = value;
			this.DoOnFontItalicChanged();
		}

		internal void SetFontUnderline(bool value)
		{
			this.m_style.FontUnderline = value;
			this.DoOnFontUnderlineChanged();
		}

		internal void SetFontStrikeout(bool value)
		{
			this.m_style.FontStrikeout = value;
			this.DoOnFontStrikeoutChanged();
		}

		internal void SetLeftBorderStyle(RdLineStyle value)
		{
			this.m_style.LeftBorderStyle = value;
			this.DoOnLeftBorderStyleChanged();
		}

		internal void SetLeftBorderColor(Color value)
		{
			this.m_style.LeftBorderColor = value;
			this.DoOnLeftBorderColorChanged();
		}

		internal void SetLeftBorderWidth(int value)
		{
			this.m_style.LeftBorderWidth = value;
			this.DoOnLeftBorderWidthChanged();
		}

		internal void SetTopBorderStyle(RdLineStyle value)
		{
			this.m_style.TopBorderStyle = value;
			this.DoOnTopBorderStyleChanged();
		}

		internal void SetTopBorderColor(Color value)
		{
			this.m_style.TopBorderColor = value;
			this.DoOnTopBorderColorChanged();
		}

		public void SetTopBorderWidth(int value)
		{
			this.m_style.TopBorderWidth = value;
			this.DoOnTopBorderWidthChanged();
		}

		internal void SetRightBorderStyle(RdLineStyle value)
		{
			this.m_style.RightBorderStyle = value;
			this.DoOnRightBorderStyleChanged();
		}

		internal void SetRightBorderColor(Color value)
		{
			this.m_style.RightBorderColor = value;
			this.DoOnRightBorderColorChanged();
		}

		internal void SetRightBorderWidth(int value)
		{
			this.m_style.RightBorderWidth = value;
			this.DoOnRightBorderWidthChanged();
		}

		internal void SetBottomBorderStyle(RdLineStyle value)
		{
			this.m_style.BottomBorderStyle = value;
			this.DoOnBottomBorderStyleChanged();
		}

		internal void SetBottomBorderColor(Color value)
		{
			this.m_style.BottomBorderColor = value;
			this.DoOnBottomBorderColorChanged();
		}

		internal void SetBottomBorderWidth(int value)
		{
			this.m_style.BottomBorderWidth = value;
			this.DoOnBottomBorderWidthChanged();
		}

		internal void SetDiagLT2RBStyle(RdLineStyle value)
		{
			this.m_style.DiagLT2RBStyle = value;
			this.DoOnDiagLT2RBStyleChanged();
		}

		internal void SetDiagLT2RBColor(Color value)
		{
			this.m_style.DiagLT2RBColor = value;
			this.DoOnDiagLT2RBColorChanged();
		}

		internal void SetDiagLT2RBWidth(int value)
		{
			this.m_style.DiagLT2RBWidth = value;
			this.DoOnDiagLT2RBWidthChanged();
		}

		internal void SetDiagLB2RTStyle(RdLineStyle value)
		{
			this.m_style.DiagLB2RTStyle = value;
			this.DoOnDiagLB2RTStyleChanged();
		}

		internal void SetDiagLB2RTColor(Color value)
		{
			this.m_style.DiagLB2RTColor = value;
			this.DoOnDiagLB2RTColorChanged();
		}

		internal void SetDiagLB2RTWidth(int value)
		{
			this.m_style.DiagLB2RTWidth = value;
			this.DoOnDiagLB2RTWidthChanged();
		}

		internal void SetColor(Color value)
		{
			this.m_style.Color = value;
			this.DoOnColorChanged();
		}

		internal void SetTransparent(bool value)
		{
			this.m_style.Transparent = value;
			this.DoOnTransparentChanged();
		}

		internal void SetPattern(int value)
		{
			this.m_style.Pattern = value;
			this.DoOnPatternChanged();
		}

		internal void SetPatternColor(Color value)
		{
			this.m_style.PatternColor = value;
			this.DoOnPatternColorChanged();
		}

		internal void SetHAlignment(RdHAlignment value)
		{
			this.m_style.HAlignment = value;
			this.DoOnHAlignmentChanged();
		}

		internal void SetVAlignment(RdVAlignment value)
		{
			this.m_style.VAlignment = value;
			this.DoOnVAlignmentChanged();
		}

		internal void SetLeftMargin(int value)
		{
			this.m_style.LeftMargin = value;
			this.DoOnLeftMarginChanged();
		}

		internal void SetTopMargin(int value)
		{
			this.m_style.TopMargin = value;
			this.DoOnTopMarginChanged();
		}

		internal void SetRightMargin(int value)
		{
			this.m_style.RightMargin = value;
			this.DoOnRightMarginChanged();
		}

		internal void SetBottomMargin(int value)
		{
			this.m_style.BottomMargin = value;
			this.DoOnBottomMarginChanged();
		}

		internal void SetThreePartText(bool value)
		{
			this.m_style.ThreePartText = value;
			this.DoOnThreePartTextChanged();
		}

		internal void SetTextControl(RdTextControl value)
		{
			this.m_style.TextControl = value;
			this.DoOnTextControlChanged();
		}

		internal void SetLineSpace(int value)
		{
			this.m_style.LineSpace = value;
			this.DoOnLineSpaceChanged();
		}

		internal void SetLocked(bool value)
		{
			this.m_style.Locked = value;
			this.DoOnLockedChanged();
		}

		internal void SetPreview(bool value)
		{
			this.m_style.Preview = value;
			this.DoOnPreviewChanged();
		}

		internal void SetPrint(bool value)
		{
			this.m_style.Print = value;
			this.DoOnPrintChanged();
		}

		internal void SetSmallFontWordWrap(bool value)
		{
			this.m_style.SmallFontWordWrap = value;
			this.DoOnSmallFontWordWrapChanged();
		}
	}
}
