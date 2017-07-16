using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class ParameterCache : DbTableCacheBase
	{
		public DataTable dtParameter;

		public DataView dvParameterBy_PmId;

		public ParameterCache() : base("PARAMETER", "PARAMETER")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT PARAMETER_PMID,PARAMETER_PMMC,PARAMETER_TYPE,PARAMETER_CONTROLTYPE,PARAMETER_DESC,PARAMETER_VALUES,PARAMETER_DEFAULT");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM PARAMETER");
			sql.Add("WHERE 1=1");
			sql.Add("ORDER BY PARAMETER_PMID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtParameter = dataSet.Tables[0];
				this.dvParameterBy_PmId = new DataView(this.dtParameter, "", "PARAMETER_PMID", DataViewRowState.CurrentRows);
			}
		}
	}
}
