using System;
using System.Collections.Generic;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL
{
	internal sealed class AppDbInstance
	{
		private static Dictionary<string, DbConfig> DbInstanceList = new Dictionary<string, DbConfig>(StringComparer.OrdinalIgnoreCase);

		public static DbConfig GetDbInstance(string dbId)
		{
			DbConfig dbConfig = null;
			bool flag = AppDbInstance.DbInstanceList == null;
			DbConfig result;
			if (flag)
			{
				result = null;
			}
			else
			{
				lock (AppDbInstance.DbInstanceList)
				{
					bool flag2 = AppDbInstance.DbInstanceList.ContainsKey(dbId);
					if (flag2)
					{
						dbConfig = AppDbInstance.DbInstanceList[dbId];
					}
				}
				result = dbConfig;
			}
			return result;
		}

		public static void AddDbInstance(string dbId, DbConfig dbConfig)
		{
			bool flag = AppDbInstance.DbInstanceList == null;
			if (flag)
			{
				AppDbInstance.DbInstanceList = new Dictionary<string, DbConfig>();
			}
			lock (AppDbInstance.DbInstanceList)
			{
				bool flag2 = AppDbInstance.DbInstanceList.ContainsKey(dbId);
				if (flag2)
				{
					AppDbInstance.DbInstanceList[dbId] = dbConfig;
				}
				else
				{
					AppDbInstance.DbInstanceList.Add(dbId, dbConfig);
				}
			}
		}
	}
}
