using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Web;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	internal sealed class AuthUtils
	{
		private static Dictionary<string, LogicSession> GlobalLogicSession = new Dictionary<string, LogicSession>(StringComparer.InvariantCultureIgnoreCase);

		private static object _lockObj = new object();

		private static Auth GetObject(DataRow row)
		{
			return new Auth
			{
				SessionId = row["AUTH_SESSIONID"].ToString().Trim(),
				UserId = row["AUTH_USERID"].ToString().Trim(),
				UserName = row["USERS_USERNAME"].ToString().Trim(),
				LoginTime = row["AUTH_LOGINTIME"].ToDateTime(),
				LoginType = row["AUTH_LOGINTYPE"].ToString().Trim().ToLogicSessionType(),
				ClientIp = row["AUTH_CLIENTIP"].ToString().Trim(),
				ClientName = row["AUTH_CLIENTNAME"].ToString().Trim(),
				DbId = row["AUTH_DBID"].ToString().Trim(),
				LastRefresh = row["AUTH_LASTREFRESH"].ToDateTime(),
				LastRequest = row["AUTH_LASTREQUEST"].ToDateTime(),
				UpdateTime = row["AUTH_UPDATETIME"].ToDateTime(),
				ExInfo = row["AUTH_EXINFO"].ToString().Trim()
			};
		}

		public static LogicSession GetLogicSession(CookieMemory cookie)
		{
			string sessionId = cookie.SessionId;
			DateTime updateTime = cookie.UpdateTime;
			LogicSession logicSession = null;
			LogicSession lgcSession = null;
			object lockObj = AuthUtils._lockObj;
			LogicSession result;
			lock (lockObj)
			{
				bool local_5_;
				do
				{
					bool flag2 = !AuthUtils.GlobalLogicSession.TryGetValue(sessionId, out lgcSession);
					if (flag2)
					{
						lgcSession = new LogicSession(sessionId);
						AuthUtils.GlobalLogicSession[sessionId] = lgcSession;
					}
					local_5_ = Monitor.TryEnter(lgcSession);
					bool flag3 = !local_5_;
					if (flag3)
					{
						Thread.Sleep(0);
					}
				}
				while (!local_5_);
				try
				{
					DateTime local_6 = AppRuntime.ServerDateTime;
					bool flag4 = lgcSession.LoginType == LogicSessionType.N && !AuthUtils.SelectAuthToSession(sessionId, lgcSession);
					if (flag4)
					{
						lgcSession.Ignore = true;
						result = null;
						return result;
					}
					bool flag5 = lgcSession.LastRequestTime < AppRuntime.ServerDateTime.AddMinutes(-480.0) && !AuthUtils.UpdateAuthLastRequest(sessionId, local_6);
					if (flag5)
					{
						lgcSession.Ignore = true;
						result = null;
						return result;
					}
					bool flag6 = updateTime > lgcSession.UpdateTime && !AuthUtils.SelectAuthToSession(sessionId, lgcSession);
					if (flag6)
					{
						lgcSession.Ignore = true;
						result = null;
						return result;
					}
					lgcSession.LastRequestTime = local_6;
					logicSession = lgcSession;
				}
				finally
				{
					Monitor.Exit(lgcSession);
				}
			}
			result = logicSession;
			return result;
		}

		public static Auth GetAuth(string sessionId)
		{
			Auth auth = null;
			AuthCache authCache = (AuthCache)new AuthCache().GetData();
			int index = authCache.dvAuthBy_SessionId.Find(sessionId);
			bool flag = index >= 0;
			if (flag)
			{
				Auth auth2 = new Auth();
				auth = AuthUtils.GetObject(authCache.dvAuthBy_SessionId[index].Row);
			}
			return auth;
		}

		internal static LogicSession SignIn(string userId, string clientIp)
		{
			HttpContext current = HttpContext.Current;
			LogicSession logicSession = new LogicSession(userId, LogicSessionType.C);
			logicSession.DbId = AppConfig.DefaultDbId;
			logicSession.ClientIp = clientIp;
			object lockObj = AuthUtils._lockObj;
			lock (lockObj)
			{
				bool flag2 = AuthUtils.GlobalLogicSession.ContainsKey(logicSession.SessionId);
				if (flag2)
				{
					logicSession = null;
				}
				else
				{
					AuthUtils.GlobalLogicSession.Add(logicSession.SessionId, logicSession);
					AuthUtils.InsertAuth(new Auth
					{
						SessionId = logicSession.SessionId,
						UserId = logicSession.UserId,
						LoginTime = logicSession.LastRequestTime,
						LoginType = logicSession.LoginType,
						ClientIp = logicSession.ClientIp,
						ClientName = logicSession.ClientIp,
						DbId = logicSession.DbId,
						LastRefresh = logicSession.LastRefreshTime,
						LastRequest = logicSession.LastRequestTime,
						UpdateTime = logicSession.UpdateTime,
						ExInfo = logicSession.ExInfo
					});
					current.Response.SetCookie(new HttpCookie("USERID", logicSession.UserId)
					{
						HttpOnly = false,
						Path = AppRuntime.AppVirtualPath
					});
				}
			}
			return logicSession;
		}

		internal static void SignOut()
		{
			LogicContext current = LogicContext.Current;
			bool flag = current == null;
			if (!flag)
			{
				LogicSession userSession = current.UserSession;
				bool flag2 = userSession != null;
				if (flag2)
				{
					object lockObj = AuthUtils._lockObj;
					lock (lockObj)
					{
						AuthUtils.GlobalLogicSession.Remove(userSession.SessionId);
						userSession.Ignore = true;
						AuthUtils.DeleteAuth(userSession.SessionId);
					}
				}
			}
		}

		internal static bool SelectAuthToSession(string sessionId, LogicSession lgcSession)
		{
			Auth auth = AuthUtils.SelectAuth(sessionId);
			bool flag = auth == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				lgcSession.UserId = auth.UserId;
				lgcSession.UserName = auth.UserName;
				lgcSession.LoginTime = auth.LoginTime;
				lgcSession.LoginType = auth.LoginType;
				lgcSession.ClientIp = auth.ClientIp;
				lgcSession.ClientName = auth.ClientName;
				lgcSession.DbId = auth.DbId;
				lgcSession.LastRefreshTime = auth.LastRefresh;
				lgcSession.LastRequestTime = auth.LastRequest;
				lgcSession.UpdateTime = auth.UpdateTime;
				lgcSession.ExInfo = auth.ExInfo;
				lgcSession.AuthSession = auth;
				result = true;
			}
			return result;
		}

		internal static Auth SelectAuth(string sessionId)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("select AUTH_SESSIONID,AUTH_USERID,USERS_USERNAME,AUTH_LOGINTIME,AUTH_LOGINTYPE,AUTH_CLIENTIP,AUTH_CLIENTNAME,AUTH_DBID");
			sql.Add(",AUTH_LASTREFRESH,AUTH_LASTREQUEST,AUTH_UPDATETIME,AUTH_EXINFO");
			sql.Add("from AUTH,USERS");
			sql.Add("where AUTH_SESSIONID=:AUTH_SESSIONID AND AUTH_USERID = USERS_USERID");
			sql.ParamByName("AUTH_SESSIONID").Value = sessionId;
			DataTable dataTable = database.OpenDataSet(sql).Tables[0];
			bool flag = dataTable.Rows.Count > 0;
			Auth result;
			if (flag)
			{
				result = AuthUtils.GetObject(dataTable.Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		internal static bool InsertAuth(Auth auth)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("insert into AUTH");
			sql.Add("       (AUTH_SESSIONID,AUTH_USERID,AUTH_LOGINTIME,AUTH_LOGINTYPE,AUTH_CLIENTIP,AUTH_CLIENTNAME,AUTH_DBID,");
			sql.Add("        AUTH_LASTREFRESH,AUTH_LASTREQUEST,AUTH_UPDATETIME,AUTH_EXINFO)");
			sql.Add("values(:AUTH_SESSIONID,:AUTH_USERID,:AUTH_LOGINTIME,:AUTH_LOGINTYPE,:AUTH_CLIENTIP,:AUTH_CLIENTNAME,:AUTH_DBID,");
			sql.Add("       :AUTH_LASTREFRESH,:AUTH_LASTREQUEST,:AUTH_UPDATETIME,:AUTH_EXINFO)");
			sql.ParamByName("AUTH_SESSIONID").Value = auth.SessionId;
			sql.ParamByName("AUTH_USERID").Value = auth.UserId;
			sql.ParamByName("AUTH_LOGINTIME").Value = auth.LoginTime;
			sql.ParamByName("AUTH_LOGINTYPE").Value = auth.LoginType.ToString();
			sql.ParamByName("AUTH_CLIENTIP").Value = auth.ClientIp;
			sql.ParamByName("AUTH_CLIENTNAME").Value = auth.ClientName;
			sql.ParamByName("AUTH_DBID").Value = auth.DbId;
			sql.ParamByName("AUTH_LASTREFRESH").Value = auth.LastRefresh;
			sql.ParamByName("AUTH_LASTREQUEST").Value = auth.LastRequest;
			sql.ParamByName("AUTH_UPDATETIME").Value = auth.UpdateTime;
			sql.ParamByName("AUTH_EXINFO").Value = auth.ExInfo;
			bool flag = database.ExecSQL(sql) != 1;
			if (flag)
			{
				throw new Exception("关键字重复!");
			}
			return true;
		}

		internal static bool UpdateAuth(Auth auth)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("update AUTH set ");
			sql.Add("   AUTH_USERID = :AUTH_USERID,");
			sql.Add("   AUTH_LOGINTIME = :AUTH_LOGINTIME,");
			sql.Add("   AUTH_LOGINTYPE = :AUTH_LOGINTYPE,");
			sql.Add("   AUTH_CLIENTIP = :AUTH_CLIENTIP,");
			sql.Add("   AUTH_CLIENTNAME = :AUTH_CLIENTNAME,");
			sql.Add("   AUTH_DBID = :AUTH_DBID,");
			sql.Add("   AUTH_LASTREFRESH = :AUTH_LASTREFRESH,");
			sql.Add("   AUTH_LASTREQUEST = :AUTH_LASTREQUEST,");
			sql.Add("   AUTH_UPDATETIME = :AUTH_UPDATETIME,");
			sql.Add("   AUTH_EXINFO = :AUTH_EXINFO");
			sql.Add("where AUTH_SESSIONID=:AUTH_SESSIONID");
			sql.ParamByName("AUTH_SESSIONID").Value = auth.SessionId;
			sql.ParamByName("AUTH_USERID").Value = auth.UserId;
			sql.ParamByName("AUTH_LOGINTIME").Value = auth.LoginTime;
			sql.ParamByName("AUTH_LOGINTYPE").Value = auth.LoginType;
			sql.ParamByName("AUTH_CLIENTIP").Value = auth.ClientIp;
			sql.ParamByName("AUTH_CLIENTNAME").Value = auth.ClientName;
			sql.ParamByName("AUTH_DBID").Value = auth.DbId;
			sql.ParamByName("AUTH_LASTREFRESH").Value = auth.LastRefresh;
			sql.ParamByName("AUTH_LASTREQUEST").Value = auth.LastRequest;
			sql.ParamByName("AUTH_UPDATETIME").Value = auth.UpdateTime;
			sql.ParamByName("AUTH_EXINFO").Value = auth.ExInfo;
			bool flag = database.ExecSQL(sql) == 1;
			if (flag)
			{
			}
			return true;
		}

		internal static bool DeleteAuth(string sessionId)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Add("delete from AUTH ");
			sql.Add("where AUTH_SESSIONID=:AUTH_SESSIONID");
			sql.ParamByName("AUTH_SESSIONID").Value = sessionId;
			return database.ExecSQL(sql) == 1;
		}

		internal static bool UpdateAuthLastRefresh(string sessionId)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Raw = true;
			sql.Clear();
			sql.Add("update AUTH set");
			sql.Add(" AUTH_LASTREFRESH = :AUTH_LASTREFRESH");
			sql.Add(" where AUTH_SESSIONID=:AUTH_SESSIONID");
			sql.ParamByName("AUTH_SESSIONID").Value = sessionId;
			sql.ParamByName("AUTH_LASTREFRESH").Value = AppRuntime.ServerDateTime;
			return database.ExecSQL(sql) == 1;
		}

		internal static bool UpdateAuthLastRequest(string sessionId, DateTime lastRequest)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Raw = true;
			sql.Clear();
			sql.Add("update AUTH set");
			sql.Add(" AUTH_LASTREQUEST = :AUTH_LASTREQUEST");
			sql.Add(" where AUTH_SESSIONID=:AUTH_SESSIONID");
			sql.ParamByName("AUTH_SESSIONID").Value = sessionId;
			sql.ParamByName("AUTH_LASTREQUEST").Value = lastRequest;
			return database.ExecSQL(sql) == 1;
		}

		private static bool InsertUltSessionId()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Raw = true;
			sql.Clear();
			sql.Add("insert into AUTH(AUTH_SESSIONID,AUTH_LASTREFRESH)");
			bool flag = database.Driver == DbProviderType.MSSQL;
			if (flag)
			{
				sql.Add("values(:AUTH_SESSIONID, getdate())");
			}
			else
			{
				bool flag2 = database.Driver == DbProviderType.ORACLE;
				if (flag2)
				{
					sql.Add("values(:AUTH_SESSIONID, sysdate)");
				}
			}
			sql.ParamByName("AUTH_SESSIONID").Value = "876978727978717673657871";
			int num = 0;
			try
			{
				num = database.ExecSQL(sql);
			}
			catch
			{
			}
			return num == 1;
		}

		private static bool UpdateUltSessionIdLastRefreshTime()
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Clear();
			sql.Raw = true;
			sql.Add("update AUTH set");
			bool flag = database.Driver == DbProviderType.MSSQL;
			if (flag)
			{
				sql.Add(" AUTH_LASTREFRESH = getdate()");
			}
			else
			{
				bool flag2 = database.Driver == DbProviderType.ORACLE;
				if (flag2)
				{
					sql.Add(" AUTH_LASTREFRESH = sysdate");
				}
			}
			sql.Add(" where AUTH_SESSIONID=:AUTH_SESSIONID");
			sql.ParamByName("AUTH_SESSIONID").Value = "876978727978717673657871";
			return database.ExecSQL(sql) == 1;
		}

		private static bool GetUltIdLastRefreshTimeAndDbTime(out DateTime lastRefreshTime, out DateTime dbTime)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Clear();
			sql.Raw = true;
			bool flag2 = database.Driver == DbProviderType.MSSQL;
			if (flag2)
			{
				sql.Add("select AUTH_LASTREFRESH,getdate() as DBTIME");
			}
			else
			{
				bool flag3 = database.Driver == DbProviderType.ORACLE;
				if (flag3)
				{
					sql.Add("select AUTH_LASTREFRESH,sysdate as DBTIME");
				}
			}
			sql.Add("from AUTH");
			sql.Add("where AUTH_SESSIONID = :AUTH_SESSIONID");
			sql.ParamByName("AUTH_SESSIONID").Value = "876978727978717673657871";
			DataTable dataTable = database.OpenDataSet(sql).Tables[0];
			bool flag4 = dataTable.Rows.Count == 1;
			bool flag;
			if (flag4)
			{
				DataRow dataRow = dataTable.Rows[0];
				flag = true;
				lastRefreshTime = dataRow["AUTH_LASTREFRESH"].ToString().ToDateTime();
				dbTime = dataRow["DBTIME"].ToString().ToDateTime();
			}
			else
			{
				flag = false;
				lastRefreshTime = AppRuntime.UltDateTime;
				dbTime = AppRuntime.UltDateTime;
			}
			return flag;
		}

		private static bool IsCheckAuthSession()
		{
			DateTime lastRefreshTime;
			DateTime dbTime;
			bool flag = !AuthUtils.GetUltIdLastRefreshTimeAndDbTime(out lastRefreshTime, out dbTime);
			if (flag)
			{
				AuthUtils.InsertUltSessionId();
			}
			return lastRefreshTime.AddMinutes(55.0) < dbTime && AuthUtils.UpdateUltSessionIdLastRefreshTime();
		}

		internal static void LogicSessionUpdateFromAuthSessionTask()
		{
			bool flag = !AuthUtils.IsCheckAuthSession();
			if (!flag)
			{
				DateTime dateTime = AppRuntime.ServerDateTime.AddMinutes(-480.0);
				Database database = LogicContext.GetDatabase();
				HSQL sql = new HSQL(database);
				sql.Clear();
				sql.Raw = true;
				sql.Add("select AUTH_SESSIONID,AUTH_USERID,AUTH_LASTREFRESH,AUTH_LASTREQUEST");
				sql.Add("from AUTH");
				sql.Add("where AUTH_SESSIONID <> :AUTH_SESSIONID");
				sql.ParamByName("AUTH_SESSIONID").Value = "876978727978717673657871";
				DataSet dataSet = database.OpenDataSet(sql);
				for (int index = 0; index < dataSet.Tables[0].Rows.Count; index++)
				{
					string sessionId = dataSet.Tables[0].Rows[index]["AUTH_SESSIONID"].ToString().Trim();
					dataSet.Tables[0].Rows[index]["AUTH_USERID"].ToString().Trim();
					bool flag2 = dataSet.Tables[0].Rows[index]["AUTH_LASTREQUEST"].ToString().ToDateTime() < dateTime;
					if (flag2)
					{
						AuthUtils.DeleteAuth(sessionId);
					}
					else
					{
						AuthUtils.UpdateAuthLastRefresh(sessionId);
					}
					Thread.Sleep(100);
				}
			}
		}

		internal static void LogicSessionUpdateFromMemorySessionTask()
		{
			object lockObj = AuthUtils._lockObj;
			Dictionary<string, LogicSession> dictionary;
			lock (lockObj)
			{
				try
				{
					dictionary = new Dictionary<string, LogicSession>(AuthUtils.GlobalLogicSession);
				}
				catch
				{
					return;
				}
			}
			DateTime dateTime = AppRuntime.ServerDateTime.AddMinutes(-480.0);
			DateTime dateTime2 = AppRuntime.ServerDateTime.AddMinutes(-5.0);
			foreach (KeyValuePair<string, LogicSession> keyValuePair in dictionary)
			{
				LogicSession logicSession = keyValuePair.Value;
				bool ignore = logicSession.Ignore;
				if (ignore)
				{
					LogicSession obj = logicSession;
					lock (obj)
					{
						bool flag3 = logicSession.LastRequestTime < dateTime2;
						if (flag3)
						{
							AuthUtils.RemoveSession(logicSession.SessionId);
						}
					}
				}
				else
				{
					LogicSession obj2 = logicSession;
					lock (obj2)
					{
						bool flag5 = logicSession.LastRequestTime < dateTime;
						if (flag5)
						{
							AuthUtils.RemoveSession(logicSession.SessionId);
						}
					}
				}
			}
		}

		internal static void RemoveSession(string sessionId)
		{
			LogicSession logicSession = null;
			try
			{
				while (!Monitor.TryEnter(logicSession))
				{
					Thread.Sleep(0);
				}
				bool flag = !AuthUtils.GlobalLogicSession.TryGetValue(sessionId, out logicSession);
				if (!flag)
				{
					object lockObj = AuthUtils._lockObj;
					lock (lockObj)
					{
						AuthUtils.GlobalLogicSession.Remove(sessionId);
					}
				}
			}
			finally
			{
				Monitor.Exit(logicSession);
			}
		}
	}
}
