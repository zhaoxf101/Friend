using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class CodeHelperCache : DbTableCacheBase
	{
		public DataTable dtCodeHelper;

		public DataView dvCodeHelperBy_CodeId;

		public CodeHelperCache() : base("CODEHELPER", "CODEHELPER")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT CODEHELPER_CODEID,CODEHELPER_CODENAME,CODEHELPER_MDID");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM CODEHELPER");
			sql.Add("WHERE 1=1");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtCodeHelper = dataSet.Tables[0];
				this.dvCodeHelperBy_CodeId = new DataView(this.dtCodeHelper, "", "CODEHELPER_CODEID", DataViewRowState.CurrentRows);
			}
		}
	}
}
