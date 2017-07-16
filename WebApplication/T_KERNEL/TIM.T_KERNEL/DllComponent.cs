using System;
using TIM.T_KERNEL.DbTableCache;

namespace TIM.T_KERNEL
{
	public class DllComponent : TIM.T_KERNEL.DbTableCache.DllComponent
	{
		public DllComponent()
		{
			base.ComId = "T_KERNEL";
			base.ComName = "运行平台";
			base.MdIdStart = 101000001;
			base.MdIdEnd = 101009999;
		}
	}
}
