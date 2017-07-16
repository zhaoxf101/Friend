using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class UserGroupCache : DbTableCacheBase
	{
		public DataTable dtUserGroup;

		public DataView dvUserGroupBy_UgId;

		public DataView dvUserGroupBy_UgName;

		public UserGroupCache() : base("T_KERNEL_USERGROUP", "USERGROUP")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT USERGROUP_UGID,USERGROUP_UGNAME,USERGROUP_DISABLED");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM USERGROUP");
			sql.Add("WHERE 1=1");
			sql.Add("ORDER BY USERGROUP_UGID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtUserGroup = dataSet.Tables[0];
				this.dvUserGroupBy_UgId = new DataView(this.dtUserGroup, "", "USERGROUP_UGID", DataViewRowState.CurrentRows);
				this.dvUserGroupBy_UgName = new DataView(this.dtUserGroup, "", "USERGROUP_UGNAME", DataViewRowState.CurrentRows);
			}
		}
	}
}
