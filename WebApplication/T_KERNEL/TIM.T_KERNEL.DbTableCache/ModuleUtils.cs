using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class ModuleUtils
	{
		internal static Module GetObject(DataRow row)
		{
			Module module = new Module();
			module.MdId = row["MODULE_MDID"].ToString().Trim().ToInt();
			module.ComId = row["MODULE_COMID"].ToString().Trim();
			module.MdName = row["MODULE_MDNAME"].ToString().Trim();
			ModuleType result;
			Enum.TryParse<ModuleType>(row["MODULE_TYPE"].ToString().Trim(), out result);
			module.Type = result;
			module.StdMdId = row["MODULE_STDMDID"].ToString().Trim().ToInt();
			module.Url = row["MODULE_URL"].ToString().Trim();
			module.Attribute = row["MODULE_ATTRIBUTE"].ToString().Trim();
			return module;
		}

		public static Module GetModule(int moduleId)
		{
			Module module = null;
			ModuleCache moduleCache = (ModuleCache)new ModuleCache().GetData();
			int index = moduleCache.dvModuleBy_MdId.Find(moduleId);
			bool flag = index >= 0;
			if (flag)
			{
				Module module2 = new Module();
				module = ModuleUtils.GetObject(moduleCache.dvModuleBy_MdId[index].Row);
			}
			return module;
		}

		public static List<Module> GetModules(string componentId)
		{
			List<Module> list = new List<Module>();
			DataRowView[] array = ((ModuleCache)new ModuleCache().GetData()).dvModuleBy_ComId.FindRows(componentId);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				Module module = new Module();
				Module @object = ModuleUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}

		public static List<Module> GetPermissionModules(string componentId)
		{
			List<Module> list = new List<Module>();
			DataRowView[] array = ((ModuleCache)new ModuleCache().GetData()).dvModuleBy_ComId.FindRows(componentId);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				Module module = new Module();
				Module @object = ModuleUtils.GetObject(dataRowView.Row);
				bool flag = ModuleUtils.IsRequirePermission(@object);
				if (flag)
				{
					list.Add(@object);
				}
			}
			return list;
		}

		public static bool IsRequirePermission(Module module)
		{
			bool flag = true;
			bool flag2 = module == null;
			bool result;
			if (flag2)
			{
				result = true;
			}
			else
			{
				bool flag3 = module.Type == ModuleType.C || module.Type == ModuleType.D || module.Type == ModuleType.S || module.Type == ModuleType.T || module.Type == ModuleType.I || module.Type == ModuleType.W || module.Type == ModuleType.H || module.Type == ModuleType.P;
				if (flag3)
				{
					flag = false;
				}
				result = flag;
			}
			return result;
		}

		public static List<Module> GetModules(ModuleType moduleType)
		{
			List<Module> list = new List<Module>();
			DataRowView[] array = ((ModuleCache)new ModuleCache().GetData()).dvModuleBy_Type.FindRows(moduleType.ToString());
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				Module module = new Module();
				Module @object = ModuleUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}

		public static string GetRouteUrl(int amId)
		{
			string str = string.Empty;
			Module module = ModuleUtils.GetModule(amId);
			bool flag = module != null;
			if (flag)
			{
				str = module.Url;
			}
			return str;
		}

		internal static bool IsStdModule(int amId)
		{
			Module module = ModuleUtils.GetModule(amId);
			bool flag = module != null;
			return !flag || module.ComId.IndexOf("UDC_") < 0;
		}

		public static bool IsExtModule(int amId)
		{
			Module module = ModuleUtils.GetModule(amId);
			return module != null && (module.Type == ModuleType.E || module.Type == ModuleType.J);
		}

		public static int GetMdId(int amId)
		{
			Module module = ModuleUtils.GetModule(amId);
			bool flag = module != null;
			int result;
			if (flag)
			{
				result = module.StdMdId;
			}
			else
			{
				result = 0;
			}
			return result;
		}
	}
}
