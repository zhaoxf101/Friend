using System;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Helper;
using TIM.T_TEMPLET.Page;

namespace TIM.T_TEMPLET.CommForm
{
	[Entity(Table = "DFSFILE", Workflow = false, BeDoc = false, ModifyControl = false)]
	public class DocFile : EntityManager
	{
		public string QueryFileGroupId = string.Empty;

		public override HSQL BuildRecordSetSql()
		{
			HSQL hsql = base.BuildRecordSetSql();
			hsql.Clear();
			hsql.Add("SELECT DFSGROUP_GROUPID,DFSGROUP_FILEID");
			hsql.Add(",DFSFILE_FSID,DFSFILE_FILEID,DFSFILE_FILENAME,DFSFILE_EXTNAME,DFSFILE_FILESIZE");
			hsql.Add("FROM DFSGROUP LEFT JOIN DFSFILE ON DFSFILE_FILEID = DFSGROUP_FILEID");
			hsql.Add("WHERE DFSGROUP_GROUPID = :DFSGROUP_GROUPID");
			hsql.Add("ORDER BY DFSGROUP_FILEID");
			hsql.AddParam("DFSGROUP_GROUPID", TimDbType.Float, 0, this.QueryFileGroupId.ToInt());
			return hsql;
		}
	}
}
