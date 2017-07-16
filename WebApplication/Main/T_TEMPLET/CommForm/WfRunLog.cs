using System;
using TIM.T_KERNEL.Data;
using TIM.T_TEMPLET.Page;

namespace TIM.T_TEMPLET.CommForm
{
	[Entity(Table = "WFRUNLOG", Workflow = false, BeDoc = false, ModifyControl = true)]
	public class WfRunLog : EntityManager
	{
		public string QueryWfId = string.Empty;

		public string QueryWfRunId = string.Empty;

		public override HSQL BuildRecordSetSql()
		{
			HSQL hsql = base.BuildRecordSetSql();
			hsql.Clear();
			hsql.Add("SELECT 1.0 AS NO1,WFRUNLOG_LOGNO AS NO2");
			hsql.Add(",WFRUNLOG_WFPID AS WFPID,WFRUNLOG_WFPACTION AS WFPACTION");
			hsql.Add(",WFRUNLOG_RBEGIN AS RBEGIN,WFRUNLOG_REND AS REND,WFRUNLOG_AEND AS AEND");
			hsql.Add(",WFRUNLOG_AUSERID AS AUSER,WFRUNLOG_OPINION AS OPINION,WFRUNLOG_TODO AS TODO,WFRUNLOG_AGENT AS AGENT");
			hsql.Add(",WFRUNLOG_WFID AS WFID,WFRUNLOG_RUNID AS WFRUNID");
			hsql.Add("FROM WFRUNLOG");
			hsql.Add("WHERE WFRUNLOG_WFID = :WFID");
			hsql.Add("AND WFRUNLOG_RUNID = :WFRUNID");
			hsql.Add("UNION");
			hsql.Add("SELECT 2.0 AS NO1,0 AS NO2");
			hsql.Add(",WFRUN_WFPID AS WFPID");
			hsql.Add(",case WFRUN_STATE when 'F' then '流程结束' when 'I' then '流程终止' else '[待处理]' end as WFPACTION");
			hsql.Add(",WFRUN_RBEGIN AS RBEGIN,WFRUN_REND AS REND,null AS AEND");
			hsql.Add(",'' AS AUSER,'' AS OPINION,WFRUN_TODO AS TODO,WFRUN_AGENT AS AGENT");
			hsql.Add(",WFRUN_WFID AS WFID,WFRUN_RUNID AS WFRUNID");
			hsql.Add("FROM WFRUN");
			hsql.Add("WHERE WFRUN_WFID = :WFID");
			hsql.Add("AND WFRUN_RUNID = :WFRUNID");
			hsql.Add("ORDER BY NO1,NO2 ASC");
			hsql.AddParam("WFID", TimDbType.Char, 10, this.QueryWfId);
			hsql.AddParam("WFRUNID", TimDbType.Float, 0, this.QueryWfRunId);
			return hsql;
		}
	}
}
