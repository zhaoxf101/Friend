using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class ComponentCache : DbTableCacheBase
	{
		public DataTable dtComponent;

		public DataView dvComponentBy_ComId;

		public ComponentCache() : base("COMPONENT", "COMPONENT")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT COMPONENT_COMID,COMPONENT_COMNAME,COMPONENT_MDIDSTART,COMPONENT_MDIDEND,COMPONENT_DISABLED");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM COMPONENT");
			sql.Add("WHERE 1=1");
			sql.Add("ORDER BY COMPONENT_COMID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtComponent = dataSet.Tables[0];
				this.dvComponentBy_ComId = new DataView(this.dtComponent, "", "COMPONENT_COMID", DataViewRowState.CurrentRows);
			}
		}
	}
}
