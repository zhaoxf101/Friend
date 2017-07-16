using System;
using System.Collections.Generic;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdAliasList
	{
		private RdDocument m_document = null;

		private Dictionary<string, RdAlias> m_list = null;

		public RdDocument Document
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

		internal Dictionary<string, RdAlias> List
		{
			get
			{
				return this.m_list;
			}
			set
			{
				this.m_list = value;
			}
		}

		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		public RdAliasList(RdDocument document)
		{
			this.m_document = document;
			this.m_list = new Dictionary<string, RdAlias>();
		}

		public void Clear()
		{
			this.m_list.Clear();
		}

		public void Load(XmlNode node)
		{
			bool hasChildNodes = node.HasChildNodes;
			if (hasChildNodes)
			{
				for (XmlNode aliasNode = node.FirstChild; aliasNode != null; aliasNode = aliasNode.NextSibling)
				{
					bool flag = aliasNode.Name == "Alias";
					if (flag)
					{
						RdAlias gtrAlias = new RdAlias(this.m_document);
						gtrAlias.Load(aliasNode);
						this.m_list.Add(gtrAlias.Name, gtrAlias);
						this.Document.Changed();
					}
				}
			}
		}

		public string GetXml()
		{
			string result = string.Empty;
			foreach (KeyValuePair<string, RdAlias> value in this.m_list)
			{
				result += value.Value.GetXml();
			}
			return result = "<AliasList>" + result + "</AliasList>";
		}

		public RdAlias FindAlias(string name)
		{
			RdAlias ret = null;
			foreach (KeyValuePair<string, RdAlias> item in this.m_list)
			{
				bool flag = item.Key == name;
				if (flag)
				{
					ret = item.Value;
					break;
				}
			}
			return ret;
		}
	}
}
