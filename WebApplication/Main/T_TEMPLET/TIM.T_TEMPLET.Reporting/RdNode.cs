using System;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	public class RdNode
	{
		private RdDocument m_document = null;

		private string m_name = string.Empty;

		private string m_tag = string.Empty;

		private object m_data = null;

		internal RdDocument Document
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

		public string Name
		{
			get
			{
				bool flag = this.m_name == "";
				if (flag)
				{
					this.DoOnGetName();
				}
				return this.m_name;
			}
			set
			{
				this.m_name = value;
				this.DoOnNameChanged();
			}
		}

		internal string Tag
		{
			get
			{
				return this.m_tag;
			}
			set
			{
				this.m_tag = value;
				this.DoOnTagChanged();
			}
		}

		internal object Data
		{
			get
			{
				return this.m_data;
			}
			set
			{
				this.m_data = value;
			}
		}

		protected virtual void DoOnGetName()
		{
			this.m_name = "";
		}

		protected void DoOnNameChanged()
		{
		}

		protected void DoOnTagChanged()
		{
		}

		internal RdNode(RdDocument document)
		{
			bool flag = document == null;
			if (flag)
			{
				throw new Exception("报表样式无法解析！");
			}
			this.m_document = document;
			this.m_name = "";
			this.m_tag = "";
		}

		internal virtual void Load(XmlNode node)
		{
			this.m_name = Utils.GetXmlNodeAttribute(node, "Name");
			this.m_tag = Utils.GetXmlNodeAttribute(node, "Tag");
		}

		internal virtual string GetXml()
		{
			return "";
		}

		internal virtual string GetAttributes()
		{
			string result = "";
			bool flag = !string.IsNullOrEmpty(this.m_name);
			if (flag)
			{
				result = result + " Name=\"" + this.m_name + "\"";
			}
			bool flag2 = !string.IsNullOrEmpty(this.m_tag);
			if (flag2)
			{
				result = result + " Tag=\"" + this.m_tag + "\"";
			}
			return result;
		}

		internal void CopyNode(RdNode node)
		{
			node.Name = this.m_name;
			node.Tag = this.m_tag;
		}
	}
}
