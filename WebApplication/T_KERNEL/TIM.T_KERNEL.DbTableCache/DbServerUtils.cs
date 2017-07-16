using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Common;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class DbServerUtils
	{
		internal static DbServer GetObject(DataRow row)
		{
			DbServer dbServer = new DbServer();
			dbServer.DbId = row["DBSERVER_DBID"].ToString().Trim();
			dbServer.Desc = row["DBSERVER_DESC"].ToString().Trim();
			dbServer.DbMS = row["DBSERVER_DBMS"].ToString().Trim().ToDbProviderType();
			string base64 = row["DBSERVER_CONN"].ToString().Trim();
			try
			{
				base64 = DbServerUtils.DecodeDbConn(base64);
			}
			catch
			{
			}
			dbServer.Conn = base64;
			return dbServer;
		}

		public static DbServer GetDbServer(string dbId)
		{
			DbServer dbServer = null;
			DbServerCache dbServerCache = (DbServerCache)new DbServerCache().GetData();
			int index = dbServerCache.dvDbServerBy_DbId.Find(dbId);
			bool flag = index >= 0;
			if (flag)
			{
				DbServer dbServer2 = new DbServer();
				dbServer = DbServerUtils.GetObject(dbServerCache.dvDbServerBy_DbId[index].Row);
			}
			return dbServer;
		}

		public static List<DbServer> GetDbServers()
		{
			List<DbServer> list = new List<DbServer>();
			foreach (DataRow row in ((DbServerCache)new DbServerCache().GetData()).dtDbServer.Rows)
			{
				DbServer dbServer = new DbServer();
				DbServer @object = DbServerUtils.GetObject(row);
				list.Add(@object);
			}
			return list;
		}

		public static string EncodeDbConn(string conn)
		{
			string str = string.Empty;
			return UtoBase64.ToBase64(conn);
		}

		public static string DecodeDbConn(string base64)
		{
			string str = string.Empty;
			return UtoBase64.FromBase64(base64);
		}
	}
}
