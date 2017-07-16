using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class FuncModelCache : DbTableCacheBase
	{
		public DataTable dtFuncModel;

		public DataView dvFuncModelBy_ChildId;

		public DataView dvFuncModelBy_FatherId;

		public FuncModelCache() : base("FUNCMODEL", "FUNCMODEL")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT FUNCMODEL_CHILDID,FUNCMODEL_ORDER,FUNCMODEL_FATHERID,FUNCMODEL_NAME,FUNCMODEL_TYPE");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM FUNCMODEL");
			sql.Add("WHERE 1 = 1");
			sql.Add("ORDER BY FUNCMODEL_FATHERID,FUNCMODEL_ORDER ASC");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtFuncModel = dataSet.Tables[0];
				this.dvFuncModelBy_FatherId = new DataView(this.dtFuncModel, "", "FUNCMODEL_FATHERID", DataViewRowState.CurrentRows);
				this.dvFuncModelBy_ChildId = new DataView(this.dtFuncModel, "", "FUNCMODEL_CHILDID", DataViewRowState.CurrentRows);
			}
		}
	}
}
