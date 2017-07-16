using System;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Utils;

namespace TIM.T_KERNEL.SystemInit
{
	internal class SystemInit
	{
		private const string SYSTEMID = "0000000001";

		private const string SYSTEMNAME = "提米信息管理系统";

		public static void Init()
		{
			Database database = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(database);
			hsql.Add("SELECT SYSTEM_ID FROM SYSTEM WHERE SYSTEM_ID = :SYSTEM_ID");
			hsql.ParamByName("SYSTEM_ID").Value = "0000000001";
			bool flag = DbHelper.ExistingRecord(hsql);
			if (!flag)
			{
				hsql.Clear();
				hsql.Add("INSERT INTO SYSTEM(SYSTEM_ID,SYSTEM_NAME,SYSTEM_PSWLENGTH,SYSTEM_PSWDAYS,SYSTEM_PSWWARNDAYS,SYSTEM_PSWNEW,SYSTEM_PSWHISTORYCOUNT,SYSTEM_LIMITEDDATE ");
				hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME)");
				hsql.Add("VALUES");
				hsql.Add("(:SYSTEM_ID,:SYSTEM_NAME,:SYSTEM_PSWLENGTH,:SYSTEM_PSWDAYS,:SYSTEM_PSWWARNDAYS,:SYSTEM_PSWNEW,:SYSTEM_PSWHISTORYCOUNT,SYSTEM_LIMITEDDATE ");
				hsql.Add(",:CREATERID,:CREATER,getdate(),:MODIFIERID,:MODIFIER,getdate())");
				hsql.ParamByName("SYSTEM_ID").Value = "0000000001";
				hsql.ParamByName("SYSTEM_NAME").Value = "提米信息管理系统";
				hsql.ParamByName("SYSTEM_PSWLENGTH").Value = "5";
				hsql.ParamByName("SYSTEM_PSWDAYS").Value = "0";
				hsql.ParamByName("SYSTEM_PSWWARNDAYS").Value = "0";
				hsql.ParamByName("SYSTEM_PSWNEW").Value = "Y";
				hsql.ParamByName("SYSTEM_PSWHISTORYCOUNT").Value = "0";
				hsql.ParamByName("CREATERID").Value = "ADMIN";
				hsql.ParamByName("CREATER").Value = "管理员";
				hsql.ParamByName("MODIFIERID").Value = "ADMIN";
				hsql.ParamByName("MODIFIER").Value = "管理员";
				hsql.ParamByName("SYSTEM_LIMITEDDATE").Value = DateTime.Today.AddYears(1);
				database.ExecSQL(hsql);
			}
		}
	}
}
