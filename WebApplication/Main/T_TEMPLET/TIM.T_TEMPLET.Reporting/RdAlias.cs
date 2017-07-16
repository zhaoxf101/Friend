using System;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdAlias : RdNode
	{
		private int m_left = 0;

		private int m_right = 0;

		private int m_top = 0;

		private int m_bottom = 0;

		public int Left
		{
			get
			{
				return this.m_left;
			}
			set
			{
				bool flag = this.m_left != value && value > 0 && (value <= this.m_right || this.m_right == 0);
				if (flag)
				{
					this.m_left = value;
					this.DoOnLeftChanged();
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
				bool flag = this.m_right != value && value > 0 && (this.m_left <= value || this.m_left == 0);
				if (flag)
				{
					this.m_right = value;
					this.DoOnRightChanged();
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
				bool flag = this.m_top != value && value > 0 && (value <= this.m_bottom || this.m_bottom == 0);
				if (flag)
				{
					this.m_top = value;
					this.DoOnTopChanged();
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
				bool flag = this.m_bottom != value && value > 0 && (this.m_top <= value || this.m_top == 0);
				if (flag)
				{
					this.m_bottom = value;
					this.DoOnBottomChanged();
				}
			}
		}

		protected void DoOnLeftChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnTopChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnRightChanged()
		{
			base.Document.Changed();
		}

		protected void DoOnBottomChanged()
		{
			base.Document.Changed();
		}

		public RdAlias(RdDocument document) : base(document)
		{
		}

		internal override void Load(XmlNode node)
		{
			base.Load(node);
			this.m_left = Utils.Str2Int(Utils.GetXmlNodeAttribute(node, "Left"), 0);
			this.m_right = Utils.Str2Int(Utils.GetXmlNodeAttribute(node, "Right"), 0);
			this.m_top = Utils.Str2Int(Utils.GetXmlNodeAttribute(node, "Top"), 0);
			this.m_bottom = Utils.Str2Int(Utils.GetXmlNodeAttribute(node, "Bottom"), 0);
		}

		internal override string GetAttributes()
		{
			string result = base.GetAttributes();
			return string.Concat(new string[]
			{
				result,
				Utils.MakeAttribute("Left", this.m_left.ToString()),
				Utils.MakeAttribute("Top", this.m_top.ToString()),
				Utils.MakeAttribute("Right", this.m_right.ToString()),
				Utils.MakeAttribute("Bottom", this.m_bottom.ToString())
			});
		}

		internal override string GetXml()
		{
			string result = base.GetXml();
			bool flag = this.m_left == 0 || this.m_top == 0 || this.m_right == 0 || this.m_bottom == 0;
			string result2;
			if (flag)
			{
				result2 = string.Empty;
			}
			else
			{
				result = result + "<Alias" + this.GetAttributes() + ">";
				result2 = result;
			}
			return result2;
		}
	}
}
