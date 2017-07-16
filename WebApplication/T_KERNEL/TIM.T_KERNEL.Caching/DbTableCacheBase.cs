using System;

namespace TIM.T_KERNEL.Caching
{
	public class DbTableCacheBase
	{
		private static object _lock = new object();

		private string _Key = string.Empty;

		private string _DependencyTables = string.Empty;

		public DbTableCacheBase(string key, string dependencyTables)
		{
			this._Key = key;
			this._DependencyTables = dependencyTables;
		}

		protected virtual void ReadTableData()
		{
		}

		public object GetData()
		{
			object obj = null;
			ICacheManager tableCacheManager = CacheFactory.GetDbTableCacheManager();
			object @lock = DbTableCacheBase._lock;
			lock (@lock)
			{
				obj = tableCacheManager.Get(this._Key);
				bool flag2 = obj == null;
				if (flag2)
				{
					this.ReadTableData();
					obj = this;
					TableDependency local_2 = new TableDependency(this._DependencyTables);
					tableCacheManager.Add(this._Key, this, local_2);
				}
			}
			return obj;
		}
	}
}
