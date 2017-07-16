using System;
using System.Collections.Generic;

namespace TIM.T_KERNEL.Caching
{
	internal class TableDependency
	{
		private Dictionary<string, DateTime> m_tableLastTime = null;

		private string _TableName = string.Empty;

		private DateTime _CacheTime = DateTime.MaxValue;

		public TableDependency(string dependencyTables)
		{
			bool flag = string.IsNullOrEmpty(dependencyTables);
			if (flag)
			{
				throw new ArgumentException("缓存依赖的数据表名不能为空。", "dependencyTables");
			}
			string[] strArray = dependencyTables.Trim().Trim(new char[]
			{
				','
			}).Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			bool flag2 = strArray.Length == 1;
			if (flag2)
			{
				this._TableName = strArray[0];
				this._CacheTime = UCacheTimeManager.GetCacheTime(this._TableName);
			}
			else
			{
				bool flag3 = strArray.Length <= 1;
				if (!flag3)
				{
					this.m_tableLastTime = new Dictionary<string, DateTime>(strArray.Length, StringComparer.InvariantCultureIgnoreCase);
					for (int index = 0; index < strArray.Length; index++)
					{
						this.m_tableLastTime.Add(strArray[index], UCacheTimeManager.GetCacheTime(this._TableName));
					}
				}
			}
		}

		public bool HasExpired()
		{
			bool flag = false;
			DateTime dateTime = DateTime.MaxValue;
			bool flag2 = this.m_tableLastTime == null;
			bool result;
			if (flag2)
			{
				DateTime cacheTime = UCacheTimeManager.GetCacheTime(this._TableName);
				result = (!(cacheTime == DateTime.MaxValue) && cacheTime != this._CacheTime);
			}
			else
			{
				foreach (KeyValuePair<string, DateTime> keyValuePair in this.m_tableLastTime)
				{
					DateTime cacheTime2 = UCacheTimeManager.GetCacheTime(keyValuePair.Key);
					bool flag3 = cacheTime2 == DateTime.MaxValue;
					if (flag3)
					{
						flag = false;
						result = flag;
						return result;
					}
					bool flag4 = cacheTime2 != keyValuePair.Value;
					if (flag4)
					{
						flag = true;
						result = flag;
						return result;
					}
				}
				result = flag;
			}
			return result;
		}
	}
}
