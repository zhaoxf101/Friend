using System;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdVariable : RdNode
	{
		private string m_comment = string.Empty;

		private RdFieldType m_type = RdFieldType.gfString;

		private string m_formula = string.Empty;

		public string Comment
		{
			get
			{
				return this.m_comment;
			}
			set
			{
				bool flag = !this.m_comment.Equals(value);
				if (flag)
				{
					this.m_comment = value;
					this.DoOnCommentChanged();
				}
			}
		}

		public RdFieldType Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				bool flag = this.m_type != value;
				if (flag)
				{
					this.m_type = value;
					this.DoOnVarTypeChanged();
				}
			}
		}

		public string Formula
		{
			get
			{
				return this.m_formula;
			}
			set
			{
				bool flag = !this.m_formula.Equals(value);
				if (flag)
				{
					this.m_formula = value;
					this.DoOnFormulaChanged();
				}
			}
		}

		public virtual void DoOnCommentChanged()
		{
		}

		public virtual void DoOnVarTypeChanged()
		{
		}

		public virtual void DoOnFormulaChanged()
		{
		}

		public RdVariable(RdDocument document) : base(document)
		{
		}

		internal override void Load(XmlNode node)
		{
			base.Load(node);
			this.m_comment = Utils.GetXmlNodeAttribute(node, "Comment");
			this.m_type = ((Utils.GetXmlNodeAttribute(node, "VarType") == "1") ? RdFieldType.gfNumeric : RdFieldType.gfString);
			this.m_formula = Utils.GetXmlNodeAttribute(node, "Formula");
		}

		internal override string GetAttributes()
		{
			string result = base.GetAttributes();
			bool flag = !string.IsNullOrEmpty(this.m_comment);
			if (flag)
			{
				result += Utils.MakeAttribute("Comment", this.m_comment);
			}
			bool flag2 = this.m_type > RdFieldType.gfString;
			if (flag2)
			{
				string arg_57_0 = result;
				string arg_52_0 = "VarType";
				int type = (int)this.m_type;
				result = arg_57_0 + Utils.MakeAttribute(arg_52_0, type.ToString());
			}
			bool flag3 = !string.IsNullOrEmpty(this.m_formula);
			if (flag3)
			{
				result += Utils.MakeAttribute("SQL", this.m_formula);
			}
			return result;
		}

		internal override string GetXml()
		{
			string result = base.GetXml();
			return "<Variable" + this.GetAttributes() + "/>";
		}
	}
}
