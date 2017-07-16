using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Script.Serialization;

namespace TIM.T_WEBCTRL
{
	public class MenuItemsConverter : JavaScriptConverter
	{
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(TimMenuItemCollection)
				};
			}
		}

		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			TimMenuItemCollection listType = obj as TimMenuItemCollection;
			bool flag = listType != null;
			IDictionary<string, object> result2;
			if (flag)
			{
				int NodesCount = listType.Count;
				int level = 0;
				ArrayList itemsList = new ArrayList();
				Dictionary<string, object> result = new Dictionary<string, object>();
				bool flag2 = NodesCount > 0;
				if (flag2)
				{
					TimMenuItemCollection topnodes = listType.TopNodes();
					bool flag3 = topnodes.Count > 0;
					if (flag3)
					{
						for (int i = 0; i < topnodes.Count; i++)
						{
							TimMenuItem oTreeNode = topnodes[i];
							itemsList.Add(this.ChildNodesJson(topnodes[i], listType, level));
						}
					}
					result["Item0"] = itemsList;
					result2 = result;
					return result2;
				}
			}
			result2 = new Dictionary<string, object>();
			return result2;
		}

		private IDictionary<string, object> ChildNodesJson(TimMenuItem TreeNode, TimMenuItemCollection tnc, int le)
		{
			ArrayList childItemsList = new ArrayList();
			TimMenuItemCollection childNodes = TreeNode.GetChilds(tnc);
			Dictionary<string, object> result = new Dictionary<string, object>();
			int childnodescount = childNodes.Count;
			bool flag = childNodes != null && le < 48;
			if (flag)
			{
				for (int i = 0; i < childNodes.Count; i++)
				{
					childItemsList.Add(this.ChildNodesJson(childNodes[i], tnc, le + 1));
				}
			}
			Dictionary<string, object> listDict = new Dictionary<string, object>();
			listDict.Add("Text", TreeNode.Text);
			listDict.Add("Value", TreeNode.Value);
			bool flag2 = !TreeNode.Enabled;
			if (flag2)
			{
				listDict.Add("Enabled", TreeNode.Enabled);
			}
			listDict.Add("FatherValue", TreeNode.FatherValue);
			string sChartColor = ColorTranslator.ToHtml(TreeNode.ChartColor);
			bool flag3 = sChartColor != "#F6F6F6";
			if (flag3)
			{
				listDict.Add("ChartColor", sChartColor);
			}
			bool flag4 = childItemsList.Count > 0;
			if (flag4)
			{
				listDict.Add("Item", childItemsList);
			}
			return listDict;
		}

		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			return null;
		}
	}
}
