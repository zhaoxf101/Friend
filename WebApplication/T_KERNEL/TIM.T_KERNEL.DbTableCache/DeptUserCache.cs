using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class DeptUserCache : DbTableCacheBase
	{
		public DataTable dtDeptUser;

		public DataView dvDeptUserBy_DeptId;

		public DataView dvDeptUserBy_UserId;

		public DeptUserCache() : base("T_KERNEL_DEPTUSER", "DEPTUSER")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT DEPTUSER_DEPTID,DEPTUSER_USERID");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM DEPTUSER");
			sql.Add("WHERE 1=1");
			sql.Add("ORDER BY DEPTUSER_DEPTID,DEPTUSER_USERID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtDeptUser = dataSet.Tables[0];
				this.dvDeptUserBy_DeptId = new DataView(this.dtDeptUser, "", "DEPTUSER_DEPTID", DataViewRowState.CurrentRows);
				this.dvDeptUserBy_UserId = new DataView(this.dtDeptUser, "", "DEPTUSER_USERID", DataViewRowState.CurrentRows);
			}
		}
	}
}
