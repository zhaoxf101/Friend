using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class UgUserCache : DbTableCacheBase
	{
		public DataTable dtUgUser;

		public DataView dvUgUserBy_UgId;

		public DataView dvUgUserBy_UserId;

		public UgUserCache() : base("T_KERNEL_UGUSER", "UGUSER")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT UGUSER_UGID,UGUSER_USERID");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM UGUSER");
			sql.Add("WHERE 1=1");
			sql.Add("ORDER BY UGUSER_UGID,UGUSER_USERID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtUgUser = dataSet.Tables[0];
				this.dvUgUserBy_UgId = new DataView(this.dtUgUser, "", "UGUSER_UGID", DataViewRowState.CurrentRows);
				this.dvUgUserBy_UserId = new DataView(this.dtUgUser, "", "UGUSER_USERID", DataViewRowState.CurrentRows);
			}
		}
	}
}
