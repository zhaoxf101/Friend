using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class AuthCache : DbTableCacheBase
	{
		public DataTable dtAuth;

		public DataView dvAuthBy_SessionId;

		public AuthCache() : base("T_KERNEL_AUTH", "AUTH")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Raw = true;
			sql.Add("select AUTH_SESSIONID,AUTH_USERID,USERS_USERNAME,AUTH_LOGINTIME,AUTH_LOGINTYPE,AUTH_CLIENTIP,AUTH_CLIENTNAME,AUTH_DBID");
			sql.Add(",AUTH_LASTREFRESH,AUTH_LASTREQUEST,AUTH_UPDATETIME,AUTH_EXINFO");
			sql.Add("from AUTH,USERS");
			sql.Add("where AUTH_USERID = USERS_USERID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtAuth = dataSet.Tables[0];
				this.dvAuthBy_SessionId = new DataView(this.dtAuth, "", "AUTH_SESSIONID", DataViewRowState.CurrentRows);
			}
		}
	}
}
