using System;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Utils;

namespace TIM.T_KERNEL.SystemInit
{
	internal class PermissionOpInit
	{
		public static void Init()
		{
			Database database = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(database);
			hsql.Clear();
			hsql.Add("SELECT PERMISSIONOP_ID FROM PERMISSIONOP WHERE PERMISSIONOP_ID = :PERMISSIONOP_ID");
			hsql.ParamByName("PERMISSIONOP_ID").Value = "INSERT";
			bool flag = !DbHelper.ExistingRecord(hsql);
			if (flag)
			{
				hsql.Clear();
				hsql.Add("INSERT INTO PERMISSIONOP(PERMISSIONOP_XH,PERMISSIONOP_ID,PERMISSIONOP_NAME");
				hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME)");
				hsql.Add("VALUES");
				hsql.Add("(:PERMISSIONOP_XH,:PERMISSIONOP_ID,:PERMISSIONOP_NAME");
				hsql.Add(",:CREATERID,:CREATER,getdate(),:MODIFIERID,:MODIFIER,getdate())");
				hsql.ParamByName("PERMISSIONOP_XH").Value = "1";
				hsql.ParamByName("PERMISSIONOP_ID").Value = "INSERT";
				hsql.ParamByName("PERMISSIONOP_NAME").Value = "新建";
				hsql.ParamByName("CREATERID").Value = "ADMIN";
				hsql.ParamByName("CREATER").Value = "管理员";
				hsql.ParamByName("MODIFIERID").Value = "AMDIN";
				hsql.ParamByName("MODIFIER").Value = "管理员";
				database.ExecSQL(hsql);
			}
			hsql.Clear();
			hsql.Add("SELECT PERMISSIONOP_ID FROM PERMISSIONOP WHERE PERMISSIONOP_ID = :PERMISSIONOP_ID");
			hsql.ParamByName("PERMISSIONOP_ID").Value = "EDIT";
			bool flag2 = !DbHelper.ExistingRecord(hsql);
			if (flag2)
			{
				hsql.Clear();
				hsql.Add("INSERT INTO PERMISSIONOP(PERMISSIONOP_XH,PERMISSIONOP_ID,PERMISSIONOP_NAME");
				hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME)");
				hsql.Add("VALUES");
				hsql.Add("(:PERMISSIONOP_XH,:PERMISSIONOP_ID,:PERMISSIONOP_NAME");
				hsql.Add(",:CREATERID,:CREATER,getdate(),:MODIFIERID,:MODIFIER,getdate())");
				hsql.ParamByName("PERMISSIONOP_XH").Value = "2";
				hsql.ParamByName("PERMISSIONOP_ID").Value = "EDIT";
				hsql.ParamByName("PERMISSIONOP_NAME").Value = "编辑";
				hsql.ParamByName("CREATERID").Value = "ADMIN";
				hsql.ParamByName("CREATER").Value = "管理员";
				hsql.ParamByName("MODIFIERID").Value = "ADMIN";
				hsql.ParamByName("MODIFIER").Value = "管理员";
				database.ExecSQL(hsql);
			}
			hsql.Clear();
			hsql.Add("SELECT PERMISSIONOP_ID FROM PERMISSIONOP WHERE PERMISSIONOP_ID = :PERMISSIONOP_ID");
			hsql.ParamByName("PERMISSIONOP_ID").Value = "DELETE";
			bool flag3 = !DbHelper.ExistingRecord(hsql);
			if (flag3)
			{
				hsql.Clear();
				hsql.Add("INSERT INTO PERMISSIONOP(PERMISSIONOP_XH,PERMISSIONOP_ID,PERMISSIONOP_NAME");
				hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME)");
				hsql.Add("VALUES");
				hsql.Add("(:PERMISSIONOP_XH,:PERMISSIONOP_ID,:PERMISSIONOP_NAME");
				hsql.Add(",:CREATERID,:CREATER,getdate(),:MODIFIERID,:MODIFIER,getdate())");
				hsql.ParamByName("PERMISSIONOP_XH").Value = "3";
				hsql.ParamByName("PERMISSIONOP_ID").Value = "DELETE";
				hsql.ParamByName("PERMISSIONOP_NAME").Value = "删除";
				hsql.ParamByName("CREATERID").Value = "ADMIN";
				hsql.ParamByName("CREATER").Value = "管理员";
				hsql.ParamByName("MODIFIERID").Value = "ADMIN";
				hsql.ParamByName("MODIFIER").Value = "管理员";
				database.ExecSQL(hsql);
			}
			hsql.Clear();
			hsql.Add("SELECT PERMISSIONOP_ID FROM PERMISSIONOP WHERE PERMISSIONOP_ID = :PERMISSIONOP_ID");
			hsql.ParamByName("PERMISSIONOP_ID").Value = "VIEW";
			bool flag4 = !DbHelper.ExistingRecord(hsql);
			if (flag4)
			{
				hsql.Clear();
				hsql.Add("INSERT INTO PERMISSIONOP(PERMISSIONOP_XH,PERMISSIONOP_ID,PERMISSIONOP_NAME");
				hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME)");
				hsql.Add("VALUES");
				hsql.Add("(:PERMISSIONOP_XH,:PERMISSIONOP_ID,:PERMISSIONOP_NAME");
				hsql.Add(",:CREATERID,:CREATER,getdate(),:MODIFIERID,:MODIFIER,getdate())");
				hsql.ParamByName("PERMISSIONOP_XH").Value = "4";
				hsql.ParamByName("PERMISSIONOP_ID").Value = "VIEW";
				hsql.ParamByName("PERMISSIONOP_NAME").Value = "浏览";
				hsql.ParamByName("CREATERID").Value = "ADMIN";
				hsql.ParamByName("CREATER").Value = "管理员";
				hsql.ParamByName("MODIFIERID").Value = "ADMIN";
				hsql.ParamByName("MODIFIER").Value = "管理员";
				database.ExecSQL(hsql);
			}
			hsql.Clear();
			hsql.Add("SELECT PERMISSIONOP_ID FROM PERMISSIONOP WHERE PERMISSIONOP_ID = :PERMISSIONOP_ID");
			hsql.ParamByName("PERMISSIONOP_ID").Value = "PRINT";
			bool flag5 = !DbHelper.ExistingRecord(hsql);
			if (flag5)
			{
				hsql.Clear();
				hsql.Add("INSERT INTO PERMISSIONOP(PERMISSIONOP_XH,PERMISSIONOP_ID,PERMISSIONOP_NAME");
				hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME)");
				hsql.Add("VALUES");
				hsql.Add("(:PERMISSIONOP_XH,:PERMISSIONOP_ID,:PERMISSIONOP_NAME");
				hsql.Add(",:CREATERID,:CREATER,getdate(),:MODIFIERID,:MODIFIER,getdate())");
				hsql.ParamByName("PERMISSIONOP_XH").Value = "5";
				hsql.ParamByName("PERMISSIONOP_ID").Value = "PRINT";
				hsql.ParamByName("PERMISSIONOP_NAME").Value = "打印";
				hsql.ParamByName("CREATERID").Value = "ADMIN";
				hsql.ParamByName("CREATER").Value = "管理员";
				hsql.ParamByName("MODIFIERID").Value = "ADMIN";
				hsql.ParamByName("MODIFIER").Value = "管理员";
				database.ExecSQL(hsql);
			}
			hsql.Clear();
			hsql.Add("SELECT PERMISSIONOP_ID FROM PERMISSIONOP WHERE PERMISSIONOP_ID = :PERMISSIONOP_ID");
			hsql.ParamByName("PERMISSIONOP_ID").Value = "DESIGN";
			bool flag6 = !DbHelper.ExistingRecord(hsql);
			if (flag6)
			{
				hsql.Clear();
				hsql.Add("INSERT INTO PERMISSIONOP(PERMISSIONOP_XH,PERMISSIONOP_ID,PERMISSIONOP_NAME");
				hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME)");
				hsql.Add("VALUES");
				hsql.Add("(:PERMISSIONOP_XH,:PERMISSIONOP_ID,:PERMISSIONOP_NAME");
				hsql.Add(",:CREATERID,:CREATER,getdate(),:MODIFIERID,:MODIFIER,getdate())");
				hsql.ParamByName("PERMISSIONOP_XH").Value = "6";
				hsql.ParamByName("PERMISSIONOP_ID").Value = "DESIGN";
				hsql.ParamByName("PERMISSIONOP_NAME").Value = "报表设计";
				hsql.ParamByName("CREATERID").Value = "ADMIN";
				hsql.ParamByName("CREATER").Value = "管理员";
				hsql.ParamByName("MODIFIERID").Value = "ADMIN";
				hsql.ParamByName("MODIFIER").Value = "管理员";
				database.ExecSQL(hsql);
			}
		}
	}
}
