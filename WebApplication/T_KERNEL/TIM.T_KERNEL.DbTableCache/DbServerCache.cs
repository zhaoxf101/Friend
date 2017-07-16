using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class DbServerCache : DbTableCacheBase
	{
		public DataTable dtDbServer;

		public DataView dvDbServerBy_DbId;

		public DbServerCache() : base("DBSERVER", "DBSERVER")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("select DBSERVER_DBID,DBSERVER_DESC,DBSERVER_DBMS,DBSERVER_CONN");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("from DBSERVER");
			sql.Add("where 1=1");
			sql.Add("ORDER BY DBSERVER_DBID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtDbServer = dataSet.Tables[0];
				this.dvDbServerBy_DbId = new DataView(this.dtDbServer, "", "DBSERVER_DBID", DataViewRowState.CurrentRows);
			}
		}
	}
}
