using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class PmConfigCache : DbTableCacheBase
	{
		public DataTable dtPmConfig;

		public DataView dvPmConfigBy_RoleId_UserId_MdId_PmId;

		public DataView dvPmConfigBy_UserId_MdId_PmId;

		public DataView dvPmConfigBy_RoleId_MdId_PmId;

		public DataView dvPmConfigBy_UserId_PmId;

		public DataView dvPmConfigBy_RoleId_PmId;

		public DataView dvPmConfigBy_MdId_PmId;

		public DataView dvPmConfigBy_PmId;

		public PmConfigCache() : base("PMCONFIG", "PMCONFIG")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT PMCONFIG_PMID,PMCONFIG_MDID,PMCONFIG_USERID,PMCONFIG_ROLEID,PMCONFIG_TYPE,PMCONFIG_VALUE");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM PMCONFIG");
			sql.Add("WHERE 1=1");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtPmConfig = dataSet.Tables[0];
				this.dvPmConfigBy_RoleId_UserId_MdId_PmId = new DataView(this.dtPmConfig, "", "PMCONFIG_ROLEID,PMCONFIG_USERID,PMCONFIG_MDID,PMCONFIG_PMID", DataViewRowState.CurrentRows);
				this.dvPmConfigBy_UserId_MdId_PmId = new DataView(this.dtPmConfig, "", "PMCONFIG_USERID,PMCONFIG_MDID,PMCONFIG_PMID", DataViewRowState.CurrentRows);
				this.dvPmConfigBy_RoleId_MdId_PmId = new DataView(this.dtPmConfig, "", "PMCONFIG_ROLEID,PMCONFIG_MDID,PMCONFIG_PMID", DataViewRowState.CurrentRows);
				this.dvPmConfigBy_UserId_PmId = new DataView(this.dtPmConfig, "", "PMCONFIG_USERID,PMCONFIG_PMID", DataViewRowState.CurrentRows);
				this.dvPmConfigBy_RoleId_PmId = new DataView(this.dtPmConfig, "", "PMCONFIG_ROLEID,PMCONFIG_PMID", DataViewRowState.CurrentRows);
				this.dvPmConfigBy_MdId_PmId = new DataView(this.dtPmConfig, "", "PMCONFIG_MDID,PMCONFIG_PMID", DataViewRowState.CurrentRows);
				this.dvPmConfigBy_PmId = new DataView(this.dtPmConfig, "", "PMCONFIG_PMID", DataViewRowState.CurrentRows);
			}
		}
	}
}
