using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Remoting.Messaging;
using System.Web;
using TIM.T_KERNEL.Common;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.DbTableCache;

namespace TIM.T_KERNEL
{
	[Serializable]
	public sealed class LogicContext : ILogicalThreadAffinative
	{
		private string m_logicSessionId = string.Empty;

		private string m_dbId = string.Empty;

		private string m_comId = string.Empty;

		private int m_mdId = 0;

		private int m_amId = 0;

		private string m_userId = string.Empty;

		private string m_userName = string.Empty;

		private bool m_headerIsSend = false;

		private LogicSession m_userSession = null;

		private Hashtable _ContextVar = null;

		internal const string ContextKey = "TIMI.LogicContext.CallContext";

		private HttpCookie m_userAuthCookies;

		private ListDictionary m_databases;

		private int m_TmpID;

		public static LogicContext Current
		{
			get
			{
				bool flag = HttpContext.Current != null;
				LogicContext result;
				if (flag)
				{
					bool flag2 = HttpContext.Current.Items["TIMI.LogicContext.CallContext"] == null;
					if (flag2)
					{
						HttpContext.Current.Items["TIMI.LogicContext.CallContext"] = new LogicContext();
					}
					result = (HttpContext.Current.Items["TIMI.LogicContext.CallContext"] as LogicContext);
				}
				else
				{
					bool flag3 = CallContext.GetData("TIMI.LogicContext.CallContext") == null;
					if (flag3)
					{
						CallContext.SetData("TIMI.LogicContext.CallContext", new LogicContext());
					}
					result = (CallContext.GetData("TIMI.LogicContext.CallContext") as LogicContext);
				}
				return result;
			}
		}

		public string LogicSessionId
		{
			get
			{
				return this.m_logicSessionId;
			}
			set
			{
				this.m_logicSessionId = value;
			}
		}

		public string DbId
		{
			get
			{
				return this.m_dbId;
			}
			internal set
			{
				this.m_dbId = value;
			}
		}

		public string ComId
		{
			get
			{
				return this.m_comId;
			}
			internal set
			{
				this.m_comId = value;
			}
		}

		public int MdId
		{
			get
			{
				return this.m_mdId;
			}
			internal set
			{
				this.m_mdId = value;
			}
		}

		public int AmId
		{
			get
			{
				return this.m_amId;
			}
			internal set
			{
				this.m_amId = value;
			}
		}

		public string UserId
		{
			get
			{
				return this.m_userId;
			}
			set
			{
				this.m_userId = value;
			}
		}

		public string UserName
		{
			get
			{
				return this.m_userName;
			}
			internal set
			{
				this.m_userName = value;
			}
		}

		public int TmpID
		{
			get
			{
				return this.m_TmpID;
			}
			internal set
			{
				this.m_TmpID = value;
			}
		}

		internal bool HeaderIsSend
		{
			get
			{
				return this.m_headerIsSend;
			}
			set
			{
				this.m_headerIsSend = value;
			}
		}

		public string Source
		{
			get;
			set;
		}

		internal DateTime CookieUpdateTime
		{
			get;
			set;
		}

		internal HttpCookie UserAuthCookies
		{
			get
			{
				return this.m_userAuthCookies;
			}
			set
			{
				this.m_userAuthCookies = value;
			}
		}

		internal LogicSession UserSession
		{
			get
			{
				return this.m_userSession;
			}
			set
			{
				this.m_userSession = value;
			}
		}

		internal void SetLogicSession(LogicSession logicSession)
		{
			bool flag = logicSession == null;
			if (!flag)
			{
				this.UserSession = logicSession;
				this.LogicSessionId = logicSession.SessionId;
				this.UserId = logicSession.UserId;
				this.UserName = logicSession.UserName;
				this.DbId = logicSession.DbId;
				this.TmpID = logicSession.TmpID;
			}
		}

		public void SetRunModule(string comId, int mdId, int amId)
		{
			this.ComId = comId;
			this.MdId = mdId;
			this.AmId = amId;
		}

		public static Database GetDatabase()
		{
			return LogicContext.GetDatabase(AppConfig.DefaultDbId);
		}

		public static Database GetDatabase(string dbId)
		{
			LogicContext current = LogicContext.Current;
			bool flag = current == null;
			if (flag)
			{
				throw new Exception("内部错误：当前线程未设置上下文环境！");
			}
			bool flag2 = current.m_databases == null;
			if (flag2)
			{
				current.m_databases = new ListDictionary();
			}
			bool flag3 = current.m_databases.Contains(dbId);
			Database database;
			if (flag3)
			{
				database = (Database)current.m_databases[dbId];
			}
			else
			{
				DbConfig dbConfig = AppDbInstance.GetDbInstance(dbId);
				bool flag4 = dbConfig == null;
				if (flag4)
				{
					DbServer dbServer = DbServerUtils.GetDbServer(dbId);
					bool flag5 = dbServer != null;
					if (flag5)
					{
						dbConfig = new DbConfig(dbServer.DbId, dbServer.Desc, dbServer.DbMS, new NameValueString
						{
							NaviteText = dbServer.Conn
						}.LineText);
					}
				}
                //database = ((dbConfig.ProviderType != DbProviderType.MSSQL) ?
                //                ((dbConfig.ProviderType != DbProviderType.ORACLE) ? 
                //                new SqlDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString) : 
                //                new OdacDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString)) :
                // new SqlDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString));

                if (dbConfig.ProviderType != DbProviderType.MSSQL)
                {
                    if (dbConfig.ProviderType != DbProviderType.ORACLE)
                    {
                        database = new SqlDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString);
                    }
                    else
                    {
                        database = new OdacDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString);
                    }
                }
                else
                {
                    database = new SqlDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString);
                }


				current.m_databases.Add(dbConfig.DbId, database);
			}
			return database;
		}

		public static int GetTmpVID()
		{
			LogicContext current = LogicContext.Current;
			bool flag = current == null;
			if (flag)
			{
				throw new Exception("内部错误：当前线程未设置上下文环境！");
			}
			return current.TmpID;
		}

		internal static Database GetDatabase(DbConfig dbConfig)
		{
            //return (dbConfig.ProviderType != DbProviderType.MSSQL) ? ((dbConfig.ProviderType != DbProviderType.ORACLE) ? new SqlDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString) : new OdacDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString)) : new SqlDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString);
            Database database = null;

            if (dbConfig.ProviderType != DbProviderType.MSSQL)
            {
                if (dbConfig.ProviderType != DbProviderType.ORACLE)
                {
                    database = new SqlDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString);
                }
                else
                {
                    database = new OdacDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString);
                }
            }
            else
            {
                database = new SqlDatabase(dbConfig.DbId, dbConfig.ProviderType.ToString(), dbConfig.ConnectionString);
            }

            return database;
        }

        internal void SetDatabase(string dbId)
		{
			this.DbId = dbId;
		}

		public static LogicSession GetLogicSession()
		{
			LogicSession logicSession = null;
			LogicContext current = LogicContext.Current;
			bool flag = current != null;
			if (flag)
			{
				logicSession = current.UserSession;
			}
			return logicSession;
		}

		public static LogicSession GetServiceLogicSession()
		{
			LogicSession logicSession = new LogicSession(AppConfig.Default_Service_UserId, LogicSessionType.S);
			logicSession.DbId = AppConfig.DefaultDbId;
			LogicContext current = LogicContext.Current;
			current.SetLogicSession(logicSession);
			current.UserId = AppConfig.Default_Service_UserId;
			current.SetDatabase(AppConfig.DefaultDbId);
			return logicSession;
		}

		private void InitContextVar()
		{
			bool flag = this._ContextVar != null;
			if (!flag)
			{
				this._ContextVar = new Hashtable();
			}
		}

		public void SetContextVar(string key, string value)
		{
			this.InitContextVar();
			bool flag = this._ContextVar.ContainsKey(key);
			if (flag)
			{
				this._ContextVar[key] = value;
			}
			else
			{
				this._ContextVar.Add(key, value);
			}
		}

		public string GetContextVar(string key)
		{
			this.InitContextVar();
			bool flag = this._ContextVar.ContainsKey(key);
			string result;
			if (flag)
			{
				result = this._ContextVar[key].ToString();
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
