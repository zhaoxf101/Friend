using System;
using System.Collections.Generic;
using System.Xml;

namespace TIM.T_TEMPLET.Reporting
{
	public class RdDataSets : RdSetNode
	{
		private Dictionary<string, RdDataSet> m_list = null;

		public Dictionary<string, RdDataSet> List
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

		internal int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		internal RdDataSets(RdDocument document) : base(document)
		{
			this.m_list = new Dictionary<string, RdDataSet>();
		}

		internal void Clear()
		{
			this.m_list.Clear();
		}

		internal void Load(XmlNode node)
		{
			bool hasChildNodes = node.HasChildNodes;
			if (hasChildNodes)
			{
				for (XmlNode dataSetNode = node.FirstChild; dataSetNode != null; dataSetNode = dataSetNode.NextSibling)
				{
					bool flag = dataSetNode.Name == "DataSet";
					if (flag)
					{
						RdDataSet gtrDataSet = new RdDataSet(base.Document);
						gtrDataSet.Load(dataSetNode);
						this.m_list.Add(gtrDataSet.Name, gtrDataSet);
						base.Document.Changed();
					}
				}
			}
		}

		internal string GetXml()
		{
			string result = string.Empty;
			foreach (KeyValuePair<string, RdDataSet> value in this.m_list)
			{
				bool flag = value.Value.MasterDataSet == null;
				if (flag)
				{
					result += value.Value.GetXml();
				}
			}
			return result = "<DataSets>" + result + "</DataSets>";
		}
	}
}
