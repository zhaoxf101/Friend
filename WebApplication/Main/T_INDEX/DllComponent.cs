using System;
using TIM.T_KERNEL.DbTableCache;

namespace TIM.T_INDEX
{
	public class DllComponent : TIM.T_KERNEL.DbTableCache.DllComponent
	{
		public DllComponent()
		{
			base.ComId = "T_INDEX";
			base.ComName = "系统框架";
			base.MdIdStart = 101030001;
			base.MdIdEnd = 101039999;
		}
	}
}
