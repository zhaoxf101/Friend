using System;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	internal class PointCache : DbTableCacheBase
	{
		public DataTable dtPoint;

		public DataView dvPointBy_PointId;

		public PointCache() : base("SSCDB", "SSCDB")
		{
		}

		protected override void ReadTableData()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("SELECT SSCDB_TAGNAME,SSCDB_GID,SSCDB_TAGBQMC,SSCDB_TYPE,SSCDB_JLDW,SSCDB_TABLEINDEX,SSCDB_FIELDINDEX");
			sql.Add(",SSCDB_DSOUID,SSCDB_DSOUMC,SSCDB_DECI,SSCDB_MAX,SSCDB_MIN,SSCDB_EMAX,SSCDB_EMIN,SSCDB_DCSTAGNAME");
			sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			sql.Add("FROM SSCDB");
			sql.Add("WHERE 1=1");
			sql.Add("ORDER BY SSCDB_GID");
			DataSet dataSet = database.OpenDataSet(sql);
			bool flag = dataSet.Tables.Count <= 0;
			if (!flag)
			{
				this.dtPoint = dataSet.Tables[0];
				this.dvPointBy_PointId = new DataView(this.dtPoint, "", "SSCDB_TAGNAME", DataViewRowState.CurrentRows);
			}
		}
	}
}
