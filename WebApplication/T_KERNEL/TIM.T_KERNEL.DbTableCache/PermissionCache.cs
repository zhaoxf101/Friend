using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class PermissionCache : DbTableCacheBase
	{
		public DataTable dtPermission;

		public DataView dvPermissionBy_Type_Id_MdId;

		public DataView dvPermissionBy_MdId;

		public PermissionCache() : base("PERMISSION", "PERMISSION")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("select PERMISSION_TYPE,PERMISSION_ID,PERMISSION_AMID");
			sql.Add(",PERMISSION_INSERT,PERMISSION_EDIT,PERMISSION_DELETE,PERMISSION_PRINT,PERMISSION_VIEW,PERMISSION_DESIGN");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("from PERMISSION");
			sql.Add("where 1=1");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtPermission = dataSet.Tables[0];
				this.dvPermissionBy_MdId = new DataView(this.dtPermission, "", "PERMISSION_AMID", DataViewRowState.CurrentRows);
				this.dvPermissionBy_Type_Id_MdId = new DataView(this.dtPermission, "", "PERMISSION_TYPE,PERMISSION_ID,PERMISSION_AMID", DataViewRowState.CurrentRows);
			}
		}
	}
}
