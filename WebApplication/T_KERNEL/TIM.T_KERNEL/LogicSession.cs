using System;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.DataTemplet;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_KERNEL.Security;

namespace TIM.T_KERNEL
{
	public class LogicSession
	{
		private string m_exInfo = string.Empty;

		private Auth m_authSession = null;

		private object _lockObj;

		private int m_TmpID;

		public string UserId
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		internal string SessionId
		{
			get;
			set;
		}

		public string DbId
		{
			get;
			internal set;
		}

		internal DateTime LoginTime
		{
			get;
			set;
		}

		internal string ClientIp
		{
			get;
			set;
		}

		internal string ClientName
		{
			get;
			set;
		}

		internal DateTime LastRequestTime
		{
			get;
			set;
		}

		internal DateTime LastRefreshTime
		{
			get;
			set;
		}

		internal DateTime UpdateTime
		{
			get;
			set;
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

		internal string ExInfo
		{
			get
			{
				return this.m_exInfo;
			}
			set
			{
				this.m_exInfo = value;
			}
		}

		internal DateTime RewriteAuthTime
		{
			get;
			set;
		}

		internal bool IsTimeOut
		{
			get
			{
				return this.LastRequestTime.AddHours(10.0) > DateTime.Now;
			}
		}

		internal bool Ignore
		{
			get;
			set;
		}

		internal LogicSessionType LoginType
		{
			get;
			set;
		}

		internal Auth AuthSession
		{
			get
			{
				return this.m_authSession;
			}
			set
			{
				this.m_authSession = value;
			}
		}

		internal LogicSession(string sessionId)
		{
			this._lockObj = new object();
			this.SessionId = sessionId;
			this.DbId = AppConfig.DefaultDbId;
			this.LoginType = LogicSessionType.N;
			this.Ignore = false;
			this.TmpID = new Random().Next();
		}

		internal LogicSession(string userId, LogicSessionType sessionType)
		{
			this._lockObj = new object();
			this.UserId = userId;
			this.LoginTime = AppRuntime.ServerDateTime;
			this.LoginType = sessionType;
			this.SessionId = Sys.Gen24BitGuid();
			this.LastRequestTime = this.LoginTime;
			this.LastRefreshTime = this.LoginTime;
			this.UpdateTime = this.LoginTime;
			this.RewriteAuthTime = this.LoginTime;
			this.Ignore = false;
			this.TmpID = new Random().Next();
		}

		internal bool RewriteAuthSession()
		{
			DateTime dateTime = AppRuntime.ServerDateTime;
			bool flag = dateTime <= this.UpdateTime;
			if (flag)
			{
				dateTime = this.UpdateTime.AddSeconds(1.0);
			}
			bool flag2 = !(this.RewriteAuthTime < dateTime);
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				Database database = LogicContext.GetDatabase();
				HSQL sql = new HSQL(database);
				sql.Raw = true;
				sql.Clear();
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
				sql.Add("where AUTH_SESSIONID = :AUTH_SESSIONID");
				sql.ParamByName("AUTH_USERID").Value = this.UserId;
				sql.ParamByName("AUTH_LOGINTIME").Value = this.LoginTime;
				sql.ParamByName("AUTH_LOGINTYPE").Value = this.LoginType.ToString();
				sql.ParamByName("AUTH_CLIENTIP").Value = this.ClientIp;
				sql.ParamByName("AUTH_CLIENTNAME").Value = this.ClientName;
				sql.ParamByName("AUTH_DBID").Value = this.DbId;
				sql.ParamByName("AUTH_LASTREFRESH").Value = this.LastRefreshTime;
				sql.ParamByName("AUTH_LASTREQUEST").Value = this.LastRequestTime;
				sql.ParamByName("AUTH_UPDATETIME").Value = this.UpdateTime;
				sql.ParamByName("AUTH_EXINFO").Value = this.ExInfo;
				sql.ParamByName("AUTH_SESSIONID").Value = this.SessionId;
				database.ExecSQL(sql);
				this.UpdateTime = dateTime;
				this.RewriteAuthTime = AppRuntime.ServerDateTime.AddMinutes(5.0);
				result = true;
			}
			return result;
		}

		public string GS(int mdId, string param)
		{
			return DataModuleUtils.GS(mdId, param);
		}

		public double GN(int mdId, string param)
		{
			return DataModuleUtils.GN(mdId, param);
		}

		public string GS(string mdName, string param)
		{
			return DataModuleUtils.GS(mdName, param);
		}

		public double GN(string mdName, string param)
		{
			return DataModuleUtils.GN(mdName, param);
		}
	}
}
