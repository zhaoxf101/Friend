using System;
using System.Collections.Generic;

namespace TIM.T_KERNEL.Helper
{
	internal class DataCache : List<int>
	{
		public DataCache(int capacity)
		{
			for (int index = 0; index < capacity; index++)
			{
				base.Add(0);
			}
		}

		public DataCache()
		{
		}
	}
}
