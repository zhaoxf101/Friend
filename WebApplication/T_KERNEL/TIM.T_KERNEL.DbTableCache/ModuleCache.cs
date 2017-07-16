using System;
using System.Collections;
using System.Data;
using System.IO;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class ModuleCache : DbTableCacheBase
	{
		public Hashtable htModuleUrl;

		public DataTable dtModule;

		public DataView dvModuleBy_MdId;

		public DataView dvModuleBy_ComId;

		public DataView dvModuleBy_Type;

		public ModuleCache() : base("MODULE", "MODULE")
		{
		}

		protected override void ReadTableData()
		{
			this.htModuleUrl = ModuleCache.GetModuleUrl();
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT MODULE_MDID,MODULE_COMID,MODULE_MDNAME,MODULE_TYPE,MODULE_STDMDID,MODULE_URL,MODULE_ATTRIBUTE");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM MODULE");
			sql.Add("WHERE 1=1");
			sql.Add("ORDER BY MODULE_COMID,MODULE_MDID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtModule = dataSet.Tables[0];
				ModuleCache.BuildModuleUrl(this.dtModule, this.htModuleUrl);
				this.dvModuleBy_MdId = new DataView(this.dtModule, "", "MODULE_MDID", DataViewRowState.CurrentRows);
				this.dvModuleBy_ComId = new DataView(this.dtModule, "", "MODULE_COMID", DataViewRowState.CurrentRows);
				this.dvModuleBy_Type = new DataView(this.dtModule, "", "MODULE_TYPE", DataViewRowState.CurrentRows);
			}
		}

		private static Hashtable GetModuleUrl()
		{
			Hashtable hashtable = new Hashtable();
			string str = string.Empty;
			foreach (Component component in ComponentUtils.GetComponents())
			{
				string str2 = AppRuntime.AppRootPath + "\\bin\\" + component.ComId + ".dll";
				bool flag = File.Exists(str2);
				if (flag)
				{
					DllComponent dllComponent = new DllComponent(str2);
					bool flag2 = dllComponent.Instance != null;
					if (flag2)
					{
						foreach (Module module in dllComponent.Instance.Modules)
						{
							bool flag3 = !hashtable.ContainsKey(module.MdId) && !string.IsNullOrEmpty(module.Url);
							if (flag3)
							{
								hashtable.Add(module.MdId, "~/WIDGET/" + component.ComId + "/" + module.Url);
							}
						}
					}
				}
			}
			return hashtable;
		}

		private static void BuildModuleUrl(DataTable dtModule, Hashtable htModuleUrl)
		{
			for (int index = 0; index < dtModule.Rows.Count; index++)
			{
				int num = dtModule.Rows[index]["MODULE_MDID"].ToString().ToInt();
				bool flag = htModuleUrl.ContainsKey(num);
				if (flag)
				{
					dtModule.Rows[index]["MODULE_URL"] = htModuleUrl[num].ToString();
				}
			}
		}
	}
}
