using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class DfsSetCache : DbTableCacheBase
	{
		public DataTable dtDfsSet;

		public DataView dvDfsSetBy_FsId;

		public DfsSetCache() : base("T_KERNEL_DFSSET", "DFSSET")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT DFSSET_FSID,DFSSET_FSNAME,DFSSET_SERVER,DFSSET_PATH");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM DFSSET");
			sql.Add("WHERE 1=1");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtDfsSet = dataSet.Tables[0];
				this.dvDfsSetBy_FsId = new DataView(this.dtDfsSet, "", "DFSSET_FSID", DataViewRowState.CurrentRows);
			}
		}
	}
}
