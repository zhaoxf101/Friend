using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class UserCache : DbTableCacheBase
	{
		public DataTable dtUser;

		public DataView dvUserBy_UserId;

		public DataView dvUserBy_UserName;

		public DataView dvUserBy_Mac;

		public UserCache() : base("USERS", "USERS")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("select USERS_USERID,USERS_USERNAME,USERS_PASSWORD,USERS_ABBR,USERS_TYPE,USERS_TEL,USERS_MOBILE,USERS_EMAIL,USERS_DISABLED,USERS_PASSWORDSETTIME");
			sql.Add(",USERS_MAC");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("from USERS");
			sql.Add("where 1=1");
			sql.Add("ORDER BY USERS_USERID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtUser = dataSet.Tables[0];
				this.dvUserBy_UserId = new DataView(this.dtUser, "", "USERS_USERID", DataViewRowState.CurrentRows);
				this.dvUserBy_UserName = new DataView(this.dtUser, "", "USERS_USERNAME", DataViewRowState.CurrentRows);
				this.dvUserBy_Mac = new DataView(this.dtUser, "", "USERS_MAC", DataViewRowState.CurrentRows);
			}
		}
	}
}
