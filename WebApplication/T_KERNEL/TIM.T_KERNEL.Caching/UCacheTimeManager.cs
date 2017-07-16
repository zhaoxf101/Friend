using System;
using System.Collections.Generic;
using System.Threading;

namespace TIM.T_KERNEL.Caching
{
	internal class UCacheTimeManager
	{
		public static Dictionary<string, DateTime> TableUpdateTime = new Dictionary<string, DateTime>(StringComparer.InvariantCultureIgnoreCase);

		public static Dictionary<string, DateTime> ObjectUpdateTime = new Dictionary<string, DateTime>(StringComparer.InvariantCultureIgnoreCase);

		public static DateTime GetCacheTime(string tableName)
		{
			DateTime dateTime = DateTime.MaxValue;
			bool flag = UCacheTimeManager.TableUpdateTime.TryGetValue(tableName, out dateTime);
			DateTime result;
			if (flag)
			{
				result = dateTime;
			}
			else
			{
				DateTime serverDateTime = AppRuntime.ServerDateTime;
				object obj;
				Monitor.Enter((Dictionary<string, DateTime>)(obj = UCacheTimeManager.TableUpdateTime));
				try
				{
					bool flag2 = UCacheTimeManager.ObjectUpdateTime.TryGetValue(tableName, out dateTime);
					if (flag2)
					{
						bool flag3 = DateTime.Compare(dateTime.AddMinutes(1.0), serverDateTime) < 0;
						if (flag3)
						{
							UCacheTimeManager.ObjectUpdateTime[tableName] = serverDateTime;
							dateTime = serverDateTime;
						}
					}
					else
					{
						UCacheTimeManager.ObjectUpdateTime.Add(tableName, serverDateTime);
					}
				}
				finally
				{
					Monitor.Exit(obj);
				}
				result = dateTime;
			}
			return result;
		}
	}
}
