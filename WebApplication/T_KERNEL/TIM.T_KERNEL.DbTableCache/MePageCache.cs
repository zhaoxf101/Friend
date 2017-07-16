using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class MePageCache : DbTableCacheBase
	{
		public DataTable dtMePage;

		public DataView dvMePageBy_MdId;

		public DataView dvMePageBy_MdId_WfbId;

		public MePageCache() : base("T_KERNEL_MEPAGE", "MEPAGE")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT MEPAGE_MDID AS MDID,MEPAGE_COMID AS COMID,WFD_WFBID AS WFBID,WFD_WFID AS WFID,MEPAGE_URL AS PAGEURL,MEPAGE_TYPE");
			sql.Add("FROM MEPAGE");
			sql.Add("LEFT JOIN MODULE ON MODULE_MDID = MEPAGE_MDID");
			sql.Add("LEFT JOIN WFD ON WFD_WFBID = MEPAGE_WFBID");
			sql.Add("WHERE MEPAGE_COMID = MODULE_COMID");
			sql.Add("ORDER BY MDID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtMePage = dataSet.Tables[0];
				MePageCache.BuildMePageUrl(this.dtMePage);
				this.dvMePageBy_MdId = new DataView(this.dtMePage, "", "MDID", DataViewRowState.CurrentRows);
				this.dvMePageBy_MdId_WfbId = new DataView(this.dtMePage, "", "MDID,WFID", DataViewRowState.CurrentRows);
			}
		}

		private static void BuildMePageUrl(DataTable dtMePage)
		{
			for (int index = 0; index < dtMePage.Rows.Count; index++)
			{
				dtMePage.Rows[index]["PAGEURL"] = "~/WIDGET/" + dtMePage.Rows[index]["COMID"].ToString().Trim() + "/" + dtMePage.Rows[index]["PAGEURL"].ToString().Trim();
			}
		}
	}
}
