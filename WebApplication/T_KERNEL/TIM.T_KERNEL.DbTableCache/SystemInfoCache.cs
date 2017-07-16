using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class SystemInfoCache : DbTableCacheBase
	{
		public DataTable dtSystem;

		public DataView dvSystemBy_Id;

		public SystemInfoCache() : base("SYSTEM", "SYSTEM")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("select SYSTEM_ID,SYSTEM_NAME,SYSTEM_PSWLENGTH,SYSTEM_PSWDAYS,SYSTEM_PSWWARNDAYS,SYSTEM_PSWNEW,SYSTEM_PSWHISTORYCOUNT,SYSTEM_LIMITEDDATE ");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("from SYSTEM");
			sql.Add("where 1=1");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtSystem = dataSet.Tables[0];
				this.dvSystemBy_Id = new DataView(this.dtSystem, "", "SYSTEM_ID", DataViewRowState.CurrentRows);
			}
		}
	}
}
