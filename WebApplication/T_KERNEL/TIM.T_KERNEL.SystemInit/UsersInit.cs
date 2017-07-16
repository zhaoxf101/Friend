using System;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Utils;

namespace TIM.T_KERNEL.SystemInit
{
	internal class UsersInit
	{
		public static void Init()
		{
			Database database = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(database);
			hsql.Add("SELECT USERS_USERID FROM USERS WHERE USERS_USERID = :USERS_USERID");
			hsql.ParamByName("USERS_USERID").Value = "ADMIN";
			bool flag = DbHelper.ExistingRecord(hsql);
			if (!flag)
			{
				hsql.Clear();
				hsql.Add("INSERT INTO USERS(USERS_USERID,USERS_USERNAME,USERS_PASSWORD,USERS_ABBR,USERS_TYPE,USERS_DISABLED");
				hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME)");
				hsql.Add("VALUES");
				hsql.Add("(:USERS_USERID,:USERS_USERNAME,:USERS_PASSWORD,:USERS_ABBR,:USERS_TYPE,:USERS_DISABLED");
				hsql.Add(",:CREATERID,:CREATER,getdate(),:MODIFIERID,:MODIFIER,getdate())");
				hsql.ParamByName("USERS_USERID").Value = "ADMIN";
				hsql.ParamByName("USERS_USERNAME").Value = "管理员";
				hsql.ParamByName("USERS_PASSWORD").Value = "2396188767";
				hsql.ParamByName("USERS_ABBR").Value = "ADMIN";
				hsql.ParamByName("USERS_TYPE").Value = "S";
				hsql.ParamByName("USERS_DISABLED").Value = "N";
				hsql.ParamByName("CREATERID").Value = "ADMIN";
				hsql.ParamByName("CREATER").Value = "管理员";
				hsql.ParamByName("MODIFIERID").Value = "ADMIN";
				hsql.ParamByName("MODIFIER").Value = "管理员";
				database.ExecSQL(hsql);
			}
		}
	}
}
