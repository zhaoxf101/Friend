using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace TIM.T_KERNEL.Caching
{
	internal class DbTableCacheManager : ICacheManager
	{
		private static MemoryCache _Cache = null;

		private Dictionary<string, TableDependency> _CacheTableDependency;

		private object _Lock;

		private int _CacheCount
		{
			get
			{
				return this._CacheTableDependency.Count;
			}
		}

		public DbTableCacheManager()
		{
			DbTableCacheManager._Cache = new MemoryCache("__TIM_DBTABLE_CACHE__", null);
			this._CacheTableDependency = new Dictionary<string, TableDependency>();
			this._Lock = new object();
		}

		public bool Add(string key, object value, TableDependency tableDependency)
		{
			object @lock = this._Lock;
			lock (@lock)
			{
				this._CacheTableDependency.Add(key, tableDependency);
			}
			return DbTableCacheManager._Cache.Add(key, value, ObjectCache.InfiniteAbsoluteExpiration, null);
		}

		public object Get(string key)
		{
			TableDependency tableDependency = null;
			object @lock = this._Lock;
			lock (@lock)
			{
				this._CacheTableDependency.TryGetValue(key, out tableDependency);
			}
			bool flag2 = tableDependency != null && tableDependency.HasExpired();
			if (flag2)
			{
				this.Remove(key);
			}
			object obj = DbTableCacheManager._Cache.Get(key, null);
			bool flag3 = obj == null;
			if (flag3)
			{
				object lock2 = this._Lock;
				lock (lock2)
				{
					this._CacheTableDependency.Remove(key);
				}
			}
			return obj;
		}

		public void Remove(string key)
		{
			object @lock = this._Lock;
			lock (@lock)
			{
				this._CacheTableDependency.Remove(key);
			}
			DbTableCacheManager._Cache.Remove(key, null);
		}
	}
}
