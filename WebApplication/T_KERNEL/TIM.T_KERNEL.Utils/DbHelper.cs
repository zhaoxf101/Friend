using System;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.Utils
{
	public class DbHelper
	{
		public static bool ExistingRecord(HSQL hsql)
		{
			return LogicContext.GetDatabase().OpenDataSet(hsql).Tables[0].Rows.Count > 0;
		}

		public static bool ExistingRecord(string sql)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql2 = new HSQL(database);
			sql2.Add(sql);
			return database.OpenDataSet(sql2).Tables[0].Rows.Count > 0;
		}

		public static void ClearTmpTbl(string sTable, int iTmpID)
		{
			Database database = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(database);
			hsql.Clear();
			hsql.Add("delete from  " + sTable);
			hsql.Add(" where " + sTable.Trim() + "_ID=:pID ");
			hsql.Add("  or " + sTable.Trim() + "_VMTIME<=:pVMTIME ");
			hsql.ParamByName("pID").Value = iTmpID;
			hsql.ParamByName("pVMTIME").Value = DateTime.Now.AddDays(-3.0);
			database.ExecSQL(hsql);
		}
	}
}
