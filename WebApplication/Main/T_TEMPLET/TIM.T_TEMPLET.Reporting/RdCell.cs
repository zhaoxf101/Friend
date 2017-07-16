using System;
using System.Collections.Generic;
using System.Xml;
using TIM.T_KERNEL.Helper;

namespace TIM.T_TEMPLET.Reporting
{
	public class RdCell : RdContentNode
	{
		private object m_builder = null;

		private int m_row = 0;

		private int m_column = 0;

		private int m_width = 1;

		private int m_height = 1;

		private string m_value = "";

		private string m_result = "";

		private string m_formula = "";

		private RdCell m_hiddenBy = null;

		private List<string> m_affectedBy = new List<string>();

		private RmVars m_pointer = null;

		private string m_generatedBy = string.Empty;

		internal object Builder
		{
			get
			{
				return this.m_builder;
			}
			set
			{
				this.m_builder = value;
			}
		}

		internal int Row
		{
			get
			{
				return this.m_row;
			}
			set
			{
				this.m_row = value;
			}
		}

		internal int Column
		{
			get
			{
				return this.m_column;
			}
			set
			{
				this.m_column = value;
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
					bool flag2 = this.m_width != 1;
					if (flag2)
					{
						this.DislinkHiddenCell();
					}
					this.m_width = value;
					bool flag3 = this.m_column + this.m_width - 1 > base.Document.Columns.ColumnCount;
					if (flag3)
					{
						base.Document.Columns.ColumnCount = this.m_column + this.m_width - 1;
					}
					bool flag4 = this.m_width != 1;
					if (flag4)
					{
						this.LinkHiddenCell();
					}
					this.DoOnWidthChanged();
				}
			}
		}

		internal int Height
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
					bool flag2 = this.m_height != 1;
					if (flag2)
					{
						this.DislinkHiddenCell();
					}
					this.m_height = value;
					bool flag3 = this.m_row + this.m_height - 1 > base.Document.Rows.RowCount;
					if (flag3)
					{
						base.Document.Rows.RowCount = this.m_row + this.m_height - 1;
					}
					bool flag4 = this.m_height != 1;
					if (flag4)
					{
						this.LinkHiddenCell();
					}
					this.DoOnHeightChanged();
				}
			}
		}

		internal string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		internal string Result
		{
			get
			{
				return this.m_result;
			}
			set
			{
				this.m_result = value;
			}
		}

		internal string Formula
		{
			get
			{
				bool flag = this.AutoDataType != RdDataType.dtFormula;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					bool flag2 = this.m_value.IndexOf('=') == 0;
					if (flag2)
					{
						result = this.m_value.Substring(1).Trim();
					}
					else
					{
						result = this.m_value.Trim();
					}
				}
				return result;
			}
			set
			{
				bool flag = this.m_formula != value || base.Style.DataType != RdDataType.dtFormula;
				if (flag)
				{
					this.m_formula = value;
					base.Style.DataType = RdDataType.dtFormula;
				}
			}
		}

		internal RdCell HiddenBy
		{
			get
			{
				return this.m_hiddenBy;
			}
			set
			{
				this.m_hiddenBy = value;
			}
		}

		internal List<string> AffectedBy
		{
			get
			{
				return this.m_affectedBy;
			}
			set
			{
				this.m_affectedBy = value;
			}
		}

		internal RmVars Pointer
		{
			get
			{
				return this.m_pointer;
			}
			set
			{
				this.m_pointer = value;
			}
		}

		internal string GeneratedBy
		{
			get
			{
				return this.m_generatedBy;
			}
			set
			{
				this.m_generatedBy = value;
			}
		}

		public RdDataType AutoDataType
		{
			get
			{
				bool flag = base.Style.DataType == RdDataType.dtAuto;
				RdDataType result;
				if (flag)
				{
					bool flag2 = string.IsNullOrEmpty(this.Value);
					if (flag2)
					{
						result = RdDataType.dtString;
					}
					else
					{
						bool flag3 = this.Value.Substring(0, 1) == "=";
						if (flag3)
						{
							result = RdDataType.dtFormula;
						}
						else
						{
							bool flag4 = Utils.IsNumber(this.Value);
							if (flag4)
							{
								result = RdDataType.dtNumber;
							}
							else
							{
								result = RdDataType.dtString;
							}
						}
					}
				}
				else
				{
					result = base.Style.DataType;
				}
				return result;
			}
		}

		protected void DoOnWidthChanged()
		{
		}

		protected void DoOnHeightChanged()
		{
		}

		protected void DoOnValueChanged()
		{
		}

		protected void DoOnDisplayFormatChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnFontChanged()
		{
		}

		protected void DoOnFontNameChanged()
		{
		}

		protected void DoOnFontSizeChanged()
		{
		}

		protected void DoOnFontBoldChanged()
		{
		}

		protected void DoOnFontItalicChanged()
		{
		}

		protected void DoOnFontUnderlineChanged()
		{
		}

		protected void DoOnFontStrikeoutChanged()
		{
		}

		protected void DoOnTopMarginChanged()
		{
		}

		protected void DoOnBottomMarginChanged()
		{
		}

		protected void DOOnTextControlChanged()
		{
		}

		protected void DoOnLineSpaceChanged()
		{
		}

		internal RdCell(RdDocument document, int row, int column) : base(document)
		{
			this.m_row = row;
			this.m_column = column;
			this.m_value = "";
			this.m_width = 1;
			this.m_height = 1;
			this.m_hiddenBy = null;
			this.m_affectedBy = new List<string>();
		}

		internal string GetName()
		{
			return Utils.EncodeCellId(this.m_row, this.m_column);
		}

		public string GetAsString()
		{
			return this.Value;
		}

		public void SetAsString(string value)
		{
			bool flag = this.Value != value;
			if (flag)
			{
				this.Value = value;
				base.Style.DataType = RdDataType.dtAuto;
				this.DoOnValueChanged();
			}
		}

		public double GetAsNumber()
		{
			return Utils.Str2Double(this.Value, 0.0);
		}

		public void SetAsNumber(double number)
		{
			bool flag = (double)Utils.Str2Float(this.Value, 0f) != number || base.Style.DataType != RdDataType.dtNumber;
			if (flag)
			{
				this.Value = number.ToString();
				base.Style.DataType = RdDataType.dtNumber;
				this.DoOnValueChanged();
			}
		}

		public string GetAsText()
		{
			bool flag = this.AutoDataType == RdDataType.dtFormula && !base.Document.ShowFormula;
			string result;
			if (flag)
			{
				result = this.Result;
			}
			else
			{
				result = this.Value;
			}
			return result;
		}

		public void SetAsText(string text)
		{
			bool flag = !this.Value.Equals(text) || base.Style.DataType != RdDataType.dtString;
			if (flag)
			{
				this.Value = text;
				base.Style.DataType = RdDataType.dtString;
				this.DoOnValueChanged();
			}
		}

		internal string GetAsFormula()
		{
			return this.Value;
		}

		internal void SetAsFormula(string formula)
		{
			bool flag = !this.Value.Equals(formula) || base.Style.DataType != RdDataType.dtFormula;
			if (flag)
			{
				this.Value = formula;
			}
			base.Style.DataType = RdDataType.dtFormula;
			this.DoOnValueChanged();
		}

		internal string GetFormula()
		{
			string ret = string.Empty;
			bool flag = this.AutoDataType != RdDataType.dtFormula;
			string result;
			if (flag)
			{
				result = ret;
			}
			else
			{
				bool flag2 = this.Value.Substring(0, 1) == "=";
				if (flag2)
				{
					ret = this.Value.Substring(1);
				}
				else
				{
					ret = this.Value;
				}
				ret = ret.Trim();
				result = ret;
			}
			return result;
		}

		internal void RowHeightChanged()
		{
			base.Document.Rows[this.Row].CalcHeight = -1;
		}

		internal void DislinkHiddenCell()
		{
			for (int i = this.Row; i <= this.Row + this.Height - 1; i++)
			{
				for (int j = this.Column; j <= this.Column + this.Width - 1; j++)
				{
					base.Document.GetCell(i, j).HiddenBy = null;
				}
			}
		}

		internal void LinkHiddenCell()
		{
			for (int i = this.Row; i <= this.Row + this.Height - 1; i++)
			{
				for (int j = this.Column; j <= this.Column + this.Width - 1; j++)
				{
					base.Document.GetCell(i, j).HiddenBy = this;
				}
			}
			this.HiddenBy = null;
		}

		internal void RefreshAffectedByList()
		{
			int Y2;
			int Y;
			int X3;
			int X2 = X3 = (Y = (Y2 = 0));
			this.AffectedBy.Clear();
			bool flag = this.AutoDataType == RdDataType.dtFormula;
			if (flag)
			{
				base.Document.RegisterFormulaCell(this);
				Expressions exp = new Expressions();
				exp.Expression = this.GetFormula();
				for (int i = 0; i < exp.ExpVariables.Count; i++)
				{
					string tmpVar = exp.ExpVariables[i];
					bool flag2 = tmpVar.IndexOf(":") >= 0;
					if (flag2)
					{
						bool flag3 = Utils.DecodeRangeId(tmpVar, ref X3, ref Y, ref X2, ref Y2);
						if (flag3)
						{
							this.AffectedBy.Add(tmpVar);
						}
					}
					else
					{
						bool flag4 = Utils.DecodeCellId(tmpVar, ref X3, ref Y);
						if (flag4)
						{
							this.AffectedBy.Add(tmpVar);
						}
					}
				}
			}
			else
			{
				base.Document.UnregisterFormulaCell(this);
			}
		}

		internal override void Load(XmlNode node)
		{
			base.Load(node);
			this.Width = Utils.GetAttrInt(node, "Width", 1);
			this.Height = Utils.GetAttrInt(node, "Height", 1);
			this.Value = Utils.GetXmlNodeAttribute(node, "Value");
			this.Result = Utils.GetXmlNodeAttribute(node, "Result");
			base.Style.DataType = Utils.Str2DataType(Utils.GetXmlNodeAttribute(node, "Type"), RdDataType.dtAuto);
			this.GeneratedBy = Utils.GetXmlNodeAttribute(node, "GB");
			this.RefreshAffectedByList();
		}

		internal override string GetAttributes()
		{
			string result = base.GetAttributes();
			bool flag = this.Width != 1;
			if (flag)
			{
				result += Utils.MakeAttribute("Width", this.Width.ToString());
			}
			bool flag2 = this.Height != 1;
			if (flag2)
			{
				result += Utils.MakeAttribute("Height", this.Height.ToString());
			}
			bool flag3 = !string.IsNullOrEmpty(this.Value);
			if (flag3)
			{
				result += Utils.MakeAttribute("Value", this.Value.FormatXmlValue());
			}
			bool flag4 = !string.IsNullOrEmpty(this.Result);
			if (flag4)
			{
				result += Utils.MakeAttribute("Result", this.Result);
			}
			bool flag5 = !string.IsNullOrEmpty(this.GeneratedBy);
			if (flag5)
			{
				result += Utils.MakeAttribute("GB", this.GeneratedBy);
			}
			return result;
		}

		internal override string GetXml()
		{
			bool flag = this.HiddenBy != null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				string ret = base.GetXml();
				ret = this.GetAttributes();
				bool flag2 = !string.IsNullOrEmpty(ret);
				if (flag2)
				{
					ret = string.Concat(new string[]
					{
						"<Cell",
						Utils.MakeAttribute("Row", this.Row.ToString()),
						Utils.MakeAttribute("Column", this.Column.ToString()),
						ret,
						"/>"
					});
				}
				result = ret;
			}
			return result;
		}

		internal void CopyNode(ref RdCell node)
		{
			node.Name = base.Name;
			node.Tag = base.Tag;
			RdMergeCellsStyle tmpStyle = new RdMergeCellsStyle();
			Utils.CopyStyleRec(ref tmpStyle, base.Style);
			node.Style = tmpStyle;
			node.Width = this.Width;
			node.Height = this.Height;
			node.Value = this.Value;
			node.Result = this.Result;
			node.GeneratedBy = this.GeneratedBy;
		}
	}
}
