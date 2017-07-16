using System;
using System.Collections.Generic;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_KERNEL.Utils;

namespace TIM.T_KERNEL.SystemInit
{
	internal class ModuleInit
	{
		public static void Init(TIM.T_KERNEL.DbTableCache.DllComponent dllComponent)
		{
			List<DllModule> modules = dllComponent.Modules;
			Database database = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(database);
			hsql.Clear();
			hsql.Add("delete from MODULE WHERE MODULE_COMID = :MODULE_COMID");
			hsql.ParamByName("MODULE_COMID").Value = dllComponent.ComId;
			database.ExecSQL(hsql);
			foreach (DllModule dllModule in modules)
			{
				bool flag = dllModule.Type == ModuleType.W || dllModule.Type == ModuleType.N;
				if (flag)
				{
					hsql.Clear();
					hsql.Add("SELECT MEPAGE_MDID FROM MEPAGE WHERE MEPAGE_MDID = :MEPAGE_MDID");
					hsql.ParamByName("MEPAGE_MDID").Value = dllModule.MdId;
					bool flag2 = DbHelper.ExistingRecord(hsql);
					if (flag2)
					{
						hsql.Clear();
						hsql.Add("UPDATE MEPAGE SET ");
						hsql.Add("  MEPAGE_WFBID = :MEPAGE_WFBID,");
						hsql.Add("  MEPAGE_COMID = :MEPAGE_COMID,");
						hsql.Add("  MEPAGE_URL = :MEPAGE_URL,");
						hsql.Add("  MEPAGE_TYPE = :MEPAGE_TYPE");
						hsql.Add("WHERE MEPAGE_MDID = :MEPAGE_MDID");
						hsql.ParamByName("MEPAGE_MDID").Value = dllModule.MdId;
						hsql.ParamByName("MEPAGE_WFBID").Value = dllModule.WfbId;
						hsql.ParamByName("MEPAGE_COMID").Value = dllComponent.ComId;
						hsql.ParamByName("MEPAGE_URL").Value = dllModule.Url;
						hsql.ParamByName("MEPAGE_TYPE").Value = dllModule.Type.ToString();
						database.ExecSQL(hsql);
					}
					else
					{
						hsql.Clear();
						hsql.Add("INSERT INTO MEPAGE(MEPAGE_MDID,MEPAGE_WFBID,MEPAGE_COMID,MEPAGE_URL,MEPAGE_TYPE)");
						hsql.Add("VALUES");
						hsql.Add("(:MEPAGE_MDID,:MEPAGE_WFBID,:MEPAGE_COMID,:MEPAGE_URL,:MEPAGE_TYPE)");
						hsql.ParamByName("MEPAGE_MDID").Value = dllModule.MdId;
						hsql.ParamByName("MEPAGE_WFBID").Value = dllModule.WfbId;
						hsql.ParamByName("MEPAGE_COMID").Value = dllComponent.ComId;
						hsql.ParamByName("MEPAGE_URL").Value = dllModule.Url;
						hsql.ParamByName("MEPAGE_TYPE").Value = dllModule.Type.ToString();
						database.ExecSQL(hsql);
					}
				}
				else
				{
					hsql.Clear();
					hsql.Add("SELECT MODULE_MDID FROM MODULE WHERE MODULE_MDID = :MODULE_MDID");
					hsql.ParamByName("MODULE_MDID").Value = dllModule.MdId;
					bool flag3 = DbHelper.ExistingRecord(hsql);
					if (flag3)
					{
						hsql.Clear();
						hsql.Add("UPDATE MODULE SET ");
						hsql.Add("  MODULE_COMID = :MODULE_COMID,");
						hsql.Add("  MODULE_MDNAME = :MODULE_MDNAME,");
						hsql.Add("  MODULE_TYPE = :MODULE_TYPE,");
						hsql.Add("  MODULE_URL = :MODULE_URL,");
						hsql.Add("  MODULE_ATTRIBUTE = :MODULE_ATTRIBUTE");
						hsql.Add("WHERE MODULE_MDID = :MODULE_MDID");
						hsql.ParamByName("MODULE_MDID").Value = dllModule.MdId;
						hsql.ParamByName("MODULE_COMID").Value = dllComponent.ComId;
						hsql.ParamByName("MODULE_MDNAME").Value = dllModule.MdName;
						hsql.ParamByName("MODULE_TYPE").Value = dllModule.Type.ToString();
						hsql.ParamByName("MODULE_URL").Value = "";
						hsql.ParamByName("MODULE_ATTRIBUTE").Value = dllModule.Attribute;
						database.ExecSQL(hsql);
					}
					else
					{
						hsql.Clear();
						hsql.Add("INSERT INTO MODULE(MODULE_MDID,MODULE_COMID,MODULE_MDNAME,MODULE_TYPE,MODULE_STDMDID,MODULE_URL,MODULE_ATTRIBUTE)");
						hsql.Add("VALUES");
						hsql.Add("(:MODULE_MDID,:MODULE_COMID,:MODULE_MDNAME,:MODULE_TYPE,:MODULE_STDMDID,:MODULE_URL,:MODULE_ATTRIBUTE)");
						hsql.ParamByName("MODULE_MDID").Value = dllModule.MdId;
						hsql.ParamByName("MODULE_COMID").Value = dllComponent.ComId;
						hsql.ParamByName("MODULE_MDNAME").Value = dllModule.MdName;
						hsql.ParamByName("MODULE_TYPE").Value = dllModule.Type.ToString();
						hsql.ParamByName("MODULE_STDMDID").Value = 0;
						hsql.ParamByName("MODULE_URL").Value = "";
						hsql.ParamByName("MODULE_ATTRIBUTE").Value = dllModule.Attribute;
						database.ExecSQL(hsql);
					}
				}
			}
		}
	}
}
