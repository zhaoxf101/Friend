using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class PmReferCache : DbTableCacheBase
	{
		public DataTable dtPmRefer;

		public DataView dvPmReferBy_PmId_MdId;

		public PmReferCache() : base("PMREFER", "PMREFER")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT PMREFER_PMID,PMREFER_MDID,PMREFER_COMID");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM PMREFER");
			sql.Add("WHERE 1=1");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtPmRefer = dataSet.Tables[0];
				this.dvPmReferBy_PmId_MdId = new DataView(this.dtPmRefer, "", "PMREFER_PMID,PMREFER_MDID", DataViewRowState.CurrentRows);
			}
		}
	}
}
