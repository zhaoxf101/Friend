using System;
using System.IO;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.DataTemplet;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_KERNEL.Log;
using TIM.T_KERNEL.Utils;

namespace TIM.T_KERNEL.SystemInit
{
	internal class ComponentInit
	{
		public static void Init()
		{
			FileInfo[] files = new DirectoryInfo(AppRuntime.AppRootPath + "\\bin").GetFiles("T_*.dll");
			int i = 0;
			while (i < files.Length)
			{
				FileInfo fileInfo = files[i];
				TIM.T_KERNEL.DbTableCache.DllComponent dllComponent = null;
				try
				{
					dllComponent = new TIM.T_KERNEL.DbTableCache.DllComponent(fileInfo.FullName).Instance;
					bool flag = dllComponent != null && !string.IsNullOrWhiteSpace(dllComponent.ComId) && !string.IsNullOrWhiteSpace(dllComponent.ComName);
					if (!flag)
					{
						goto IL_289;
					}
					Database database = LogicContext.GetDatabase();
					HSQL hsql = new HSQL(database);
					hsql.Add("SELECT COMPONENT_COMID FROM COMPONENT WHERE COMPONENT_COMID = :COMPONENT_COMID");
					hsql.ParamByName("COMPONENT_COMID").Value = dllComponent.ComId;
					bool flag2 = DbHelper.ExistingRecord(hsql);
					if (flag2)
					{
						hsql.Clear();
						hsql.Add("UPDATE COMPONENT SET");
						hsql.Add("  COMPONENT_COMNAME = :COMPONENT_COMNAME,");
						hsql.Add("  COMPONENT_MDIDSTART = :COMPONENT_MDIDSTART,");
						hsql.Add("  COMPONENT_MDIDEND = :COMPONENT_MDIDEND,");
						hsql.Add("  COMPONENT_DISABLED = :COMPONENT_DISABLED");
						hsql.Add("WHERE COMPONENT_COMID = :COMPONENT_COMID");
						hsql.ParamByName("COMPONENT_COMID").Value = dllComponent.ComId;
						hsql.ParamByName("COMPONENT_COMNAME").Value = dllComponent.ComName;
						hsql.ParamByName("COMPONENT_MDIDSTART").Value = dllComponent.MdIdStart;
						hsql.ParamByName("COMPONENT_MDIDEND").Value = dllComponent.MdIdEnd;
						hsql.ParamByName("COMPONENT_DISABLED").Value = "N";
						database.ExecSQL(hsql);
					}
					else
					{
						hsql.Clear();
						hsql.Add("INSERT INTO COMPONENT(COMPONENT_COMID,COMPONENT_COMNAME,COMPONENT_MDIDSTART,COMPONENT_MDIDEND,COMPONENT_DISABLED)");
						hsql.Add("VALUES");
						hsql.Add("(:COMPONENT_COMID,:COMPONENT_COMNAME,:COMPONENT_MDIDSTART,:COMPONENT_MDIDEND,:COMPONENT_DISABLED)");
						hsql.ParamByName("COMPONENT_COMID").Value = dllComponent.ComId;
						hsql.ParamByName("COMPONENT_COMNAME").Value = dllComponent.ComName;
						hsql.ParamByName("COMPONENT_MDIDSTART").Value = dllComponent.MdIdStart;
						hsql.ParamByName("COMPONENT_MDIDEND").Value = dllComponent.MdIdEnd;
						hsql.ParamByName("COMPONENT_DISABLED").Value = "N";
						database.ExecSQL(hsql);
					}
				}
				catch (Exception ex)
				{
					AppEventLog.Error("ComponentInit 异常" + ex.Message);
				}
				goto IL_27A;
				IL_289:
				i++;
				continue;
				IL_27A:
				ModuleInit.Init(dllComponent);
				DataModuleUtils.Init(dllComponent);
				goto IL_289;
			}
		}
	}
}
