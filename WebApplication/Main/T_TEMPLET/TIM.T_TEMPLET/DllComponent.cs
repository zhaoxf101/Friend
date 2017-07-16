using System;
using TIM.T_KERNEL.DbTableCache;

namespace TIM.T_TEMPLET
{
	public class DllComponent : TIM.T_KERNEL.DbTableCache.DllComponent
	{
		public DllComponent()
		{
			base.ComId = "T_TEMPLET";
			base.ComName = "模板";
			base.MdIdStart = 101020001;
			base.MdIdEnd = 101029999;
		}
	}
}
