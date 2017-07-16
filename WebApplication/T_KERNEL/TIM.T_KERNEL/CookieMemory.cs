using System;
using System.Web;
using System.Web.Security;
using TIM.T_KERNEL.Common;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL
{
	internal class CookieMemory
	{
		private NameValueString ns = null;

		private const string SessionIdKey = "S";

		private const string UserIdKey = "U";

		private const string UpdateTimeKey = "T";

		public static string FormsCookieName
		{
			get
			{
				return FormsAuthentication.FormsCookieName;
			}
		}

		public string SessionId
		{
			get;
			set;
		}

		public string UserId
		{
			get;
			set;
		}

		public DateTime UpdateTime
		{
			get;
			set;
		}

		public CookieMemory(string cookieUserData)
		{
			this.RecoverCookie(cookieUserData);
		}

		public CookieMemory(HttpCookie cookie)
		{
			string cookieUserData = string.Empty;
			bool flag = cookie != null;
			if (flag)
			{
				cookieUserData = FormsAuthentication.Decrypt(cookie.Value).UserData;
			}
			this.RecoverCookie(cookieUserData);
		}

		private void RecoverCookie(string cookieUserData)
		{
			this.ns = new NameValueString();
			this.ns.LineText = cookieUserData;
			this.SessionId = this.ns["S"];
			this.UserId = this.ns["U"];
			this.UpdateTime = this.ns["T"].ToDateTimeFrom16();
		}

		public override string ToString()
		{
			return new NameValueString
			{
				{
					"S",
					this.SessionId
				},
				{
					"U",
					this.UserId
				},
				{
					"T",
					this.UpdateTime.To16String()
				}
			}.LineText;
		}
	}
}
