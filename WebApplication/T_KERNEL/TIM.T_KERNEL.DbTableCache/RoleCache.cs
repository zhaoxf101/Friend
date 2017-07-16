using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class RoleCache : DbTableCacheBase
	{
		public DataTable dtRole;

		public DataView dvRoleBy_RoleId;

		public DataView dvRoleBy_RoleName;

		public RoleCache() : base("ROLE", "ROLE")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("select ROLE_ROLEID,ROLE_ROLENAME,ROLE_DESC");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("from ROLE");
			sql.Add("where 1=1");
			sql.Add("ORDER BY ROLE_ROLEID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtRole = dataSet.Tables[0];
				this.dvRoleBy_RoleId = new DataView(this.dtRole, "", "ROLE_ROLEID", DataViewRowState.CurrentRows);
				this.dvRoleBy_RoleName = new DataView(this.dtRole, "", "ROLE_ROLENAME", DataViewRowState.CurrentRows);
			}
		}
	}
}
