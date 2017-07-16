using System;
using System.Collections.Concurrent;

namespace TIM.T_KERNEL.Caching
{
	internal static class CacheFactory
	{
		private static ConcurrentDictionary<string, ICacheManager> _listCacheManager = new ConcurrentDictionary<string, ICacheManager>();

		public static ICacheManager GetDbTableCacheManager()
		{
			return CacheFactory.GetCacheManager("DbTableCacheManagerName");
		}

		private static ICacheManager GetCacheManager()
		{
			return CacheFactory.GetCacheManager(AppRuntime.AppId);
		}

		private static ICacheManager GetCacheManager(string cacheManagerName)
		{
			ICacheManager cacheManager = null;
			bool flag = !CacheFactory._listCacheManager.ContainsKey(cacheManagerName);
			if (flag)
			{
				bool flag2 = cacheManagerName == "DbTableCacheManagerName";
				if (flag2)
				{
					cacheManager = new DbTableCacheManager();
					CacheFactory._listCacheManager.TryAdd(cacheManagerName, cacheManager);
				}
			}
			else
			{
				CacheFactory._listCacheManager.TryGetValue(cacheManagerName, out cacheManager);
			}
			return cacheManager;
		}
	}
}
