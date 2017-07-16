using System;
using TIM.T_KERNEL.Caching;

namespace TIM.T_KERNEL.Utils
{
	public class CacheEvent
	{
		public static void TableIsUpdated(string tableName)
		{
			DbTableCacheEvent.DbTableIsUpdated(tableName);
		}

		internal static void UpdateUCache()
		{
			CacheEvent.TableIsUpdated("AUTH");
			CacheEvent.TableIsUpdated("CODEHELPER");
			CacheEvent.TableIsUpdated("COMPONENT");
			CacheEvent.TableIsUpdated("DBSERVER");
			CacheEvent.TableIsUpdated("FUNCMODEL");
			CacheEvent.TableIsUpdated("MODULE");
			CacheEvent.TableIsUpdated("PARAMETER");
			CacheEvent.TableIsUpdated("PERMISSION");
			CacheEvent.TableIsUpdated("PMCONFIG");
			CacheEvent.TableIsUpdated("PMREFER");
			CacheEvent.TableIsUpdated("ROLE");
			CacheEvent.TableIsUpdated("ROLEUSER");
			CacheEvent.TableIsUpdated("SYSTEM");
			CacheEvent.TableIsUpdated("USERS");
		}
	}
}
