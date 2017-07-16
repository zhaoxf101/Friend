using System;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.Caching
{
	internal class DbTableCacheEvent
	{
		public static void DbTableIsUpdated(string tableName)
		{
			bool flag = string.IsNullOrEmpty(tableName);
			if (!flag)
			{
				tableName = tableName.Trim().ToUpper();
				bool flag2 = DbTableCacheEvent.UpdateUCache(tableName) != 0;
				if (!flag2)
				{
					DbTableCacheEvent.InsertUCache(tableName);
				}
			}
		}

		public static int UpdateUCache(string tableName)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Clear();
			sql.Add("update UCACHE set");
			bool flag = database.Driver == DbProviderType.MSSQL;
			if (flag)
			{
				sql.Add("UCACHE_UPDTIME = getdate()");
			}
			else
			{
				bool flag2 = database.Driver == DbProviderType.ORACLE;
				if (flag2)
				{
					sql.Add("UCACHE_UPDTIME = sysdate");
				}
			}
			sql.Add("where UCACHE_TABLENAME = :UCACHE_TABLENAME");
			sql.ParamByName("UCACHE_TABLENAME").Value = tableName;
			return database.ExecSQL(sql);
		}

		public static int InsertUCache(string tableName)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Clear();
			sql.Add("insert into UCACHE(UCACHE_TABLENAME, UCACHE_UPDTIME)");
			bool flag = database.Driver == DbProviderType.MSSQL;
			if (flag)
			{
				sql.Add("values(:UCACHE_TABLENAME,getdate())");
			}
			else
			{
				bool flag2 = database.Driver == DbProviderType.ORACLE;
				if (flag2)
				{
					sql.Add("values(:UCACHE_TABLENAME,sysdate)");
				}
			}
			sql.ParamByName("UCACHE_TABLENAME").Value = tableName;
			return database.ExecSQL(sql);
		}
	}
}
