using System;
using System.Collections.Generic;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdVariables
	{
		private RdDocument m_document = null;

		private Dictionary<string, RdVariable> m_list = null;

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

		internal Dictionary<string, RdVariable> List
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

		public RdVariables(RdDocument document)
		{
			this.m_document = document;
			this.m_list = new Dictionary<string, RdVariable>();
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
				for (XmlNode variableNode = node.FirstChild; variableNode != null; variableNode = variableNode.NextSibling)
				{
					bool flag = variableNode.Name == "Variable";
					if (flag)
					{
						RdVariable gtrVariable = new RdVariable(this.m_document);
						gtrVariable.Load(variableNode);
						this.m_list.Add(gtrVariable.Name, gtrVariable);
						this.Document.Changed();
					}
				}
			}
		}

		public string GetXml()
		{
			string result = string.Empty;
			foreach (KeyValuePair<string, RdVariable> value in this.m_list)
			{
				result += value.Value.GetXml();
			}
			return result = "<Variables>" + result + "</Variables>";
		}

		internal RdVariable FindVariable(string name)
		{
			RdVariable ret = null;
			foreach (KeyValuePair<string, RdVariable> item in this.m_list)
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
