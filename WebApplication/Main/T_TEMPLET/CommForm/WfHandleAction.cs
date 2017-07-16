using System;
using TIM.T_KERNEL.Data;
using TIM.T_TEMPLET.Page;

namespace TIM.T_TEMPLET.CommForm
{
	[Entity(Table = "", Workflow = false, BeDoc = false, ModifyControl = true)]
	public class WfHandleAction : EntityManager
	{
		public override HSQL BuildRecordSql()
		{
			return base.BuildRecordSql();
		}
	}
}
