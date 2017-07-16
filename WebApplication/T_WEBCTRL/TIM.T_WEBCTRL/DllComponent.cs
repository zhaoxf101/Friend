using System;
using TIM.T_KERNEL.DbTableCache;

namespace TIM.T_WEBCTRL
{
	public class DllComponent : TIM.T_KERNEL.DbTableCache.DllComponent
	{
		public DllComponent()
		{
			base.ComId = "T_WEBCTRL";
			base.ComName = "控件";
			base.MdIdStart = 101010001;
			base.MdIdEnd = 101019999;
		}
	}
}
