using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class DeptCache : DbTableCacheBase
	{
		public DataTable dtDept;

		public DataView dvDeptBy_DeptId;

		public DataView dvDeptBy_DeptName;

		public DeptCache() : base("T_KERNEL_DEPT", "DEPT")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT DEPT_DEPTID,DEPT_DEPTNAME,DEPT_FZRID,DEPT_DISABLED");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM DEPT");
			sql.Add("WHERE 1=1");
			sql.Add("ORDER BY DEPT_DEPTID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtDept = dataSet.Tables[0];
				this.dvDeptBy_DeptId = new DataView(this.dtDept, "", "DEPT_DEPTID", DataViewRowState.CurrentRows);
				this.dvDeptBy_DeptName = new DataView(this.dtDept, "", "DEPT_DEPTNAME", DataViewRowState.CurrentRows);
			}
		}
	}
}
