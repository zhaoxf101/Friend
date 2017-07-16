using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class ComponentUtils
	{
		internal static Component GetObject(DataRow row)
		{
			return new Component
			{
				ComId = row["COMPONENT_COMID"].ToString().Trim(),
				ComName = row["COMPONENT_COMNAME"].ToString().Trim(),
				MdIdStart = row["COMPONENT_MDIDSTART"].ToString().Trim().ToInt(),
				MdIdEnd = row["COMPONENT_MDIDEND"].ToString().Trim().ToInt(),
				Disabled = (row["COMPONENT_DISABLED"].ToString().Trim() == "Y")
			};
		}

		public static Component GetComponent(string componentId)
		{
			Component component = null;
			ComponentCache componentCache = (ComponentCache)new ComponentCache().GetData();
			int index = componentCache.dvComponentBy_ComId.Find(componentId);
			bool flag = index >= 0;
			if (flag)
			{
				Component component2 = new Component();
				component = ComponentUtils.GetObject(componentCache.dvComponentBy_ComId[index].Row);
			}
			return component;
		}

		public static List<Component> GetComponents()
		{
			List<Component> list = new List<Component>();
			foreach (DataRow row in ((ComponentCache)new ComponentCache().GetData()).dtComponent.Rows)
			{
				Component component = new Component();
				Component @object = ComponentUtils.GetObject(row);
				list.Add(@object);
			}
			return list;
		}
	}
}
