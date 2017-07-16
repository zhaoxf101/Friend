using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class RoleUserCache : DbTableCacheBase
	{
		public DataTable dtRoleUser;

		public DataView dvRoleUserBy_RoleId;

		public DataView dvRoleUserBy_UserId;

		public RoleUserCache() : base("ROLEUSER", "ROLEUSER")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("select ROLEUSER_ROLEID,ROLEUSER_USERID");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("from ROLEUSER");
			sql.Add("where 1=1");
			sql.Add("ORDER BY ROLEUSER_ROLEID,ROLEUSER_USERID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtRoleUser = dataSet.Tables[0];
				this.dvRoleUserBy_RoleId = new DataView(this.dtRoleUser, "", "ROLEUSER_ROLEID", DataViewRowState.CurrentRows);
				this.dvRoleUserBy_UserId = new DataView(this.dtRoleUser, "", "ROLEUSER_USERID", DataViewRowState.CurrentRows);
			}
		}
	}
}
