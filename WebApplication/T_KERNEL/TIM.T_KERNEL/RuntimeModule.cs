using System;
using System.Web;
using System.Web.Security;
using TIM.T_KERNEL.Common;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_KERNEL.Helper;
using TIM.T_KERNEL.Log;

namespace TIM.T_KERNEL
{
	public sealed class RuntimeModule : IHttpModule
	{
		public void Dispose()
		{
		}

		public void Init(HttpApplication application)
		{
			application.BeginRequest += new EventHandler(this.application_BeginRequest);
			application.AuthenticateRequest += new EventHandler(this.application_AuthenticateRequest);
			application.PostAcquireRequestState += new EventHandler(this.application_PostAcquireRequestState);
			application.PreSendRequestHeaders += new EventHandler(this.application_PreSendRequestHeaders);
			application.PreSendRequestContent += new EventHandler(this.application_PreSendRequestContent);
			application.EndRequest += new EventHandler(this.application_EndRequest);
		}

		private void application_BeginRequest(object sender, EventArgs e)
		{
			HttpContext context = ((HttpApplication)sender).Context;
			HttpRequest request = context.Request;
			string filePath = request.FilePath;
			string executionFilePath = request.AppRelativeCurrentExecutionFilePath;
			GlobalCulture.SetContextCulture();
			LogicContext current = LogicContext.Current;
			current.SetDatabase(AppConfig.DefaultDbId);
			current.Source = filePath;
			HttpCookie cookie = request.Cookies[CookieMemory.FormsCookieName];
			bool flag = cookie != null;
			if (flag)
			{
				CookieMemory cookie2 = new CookieMemory(cookie);
				LogicSession logicSession = AuthUtils.GetLogicSession(cookie2);
				bool flag2 = logicSession != null && logicSession.UserId == cookie2.UserId;
				if (!flag2)
				{
					AppEventLog.Debug("无法恢复");
					return;
				}
				current.CookieUpdateTime = cookie2.UpdateTime;
				logicSession.LastRequestTime = AppRuntime.ServerDateTime;
				current.SetLogicSession(logicSession);
				current.UserAuthCookies = cookie;
			}
			bool flag3 = filePath == null;
			if (!flag3)
			{
				string s = StringHelper.RightSubstring(filePath, 5);
				bool flag4 = (!StringHelper.EqualsIgnoreCase(s, ".aspx") && !StringHelper.EqualsIgnoreCase(s, ".asmx") && !StringHelper.EqualsIgnoreCase(s, ".ashx")) || context.Request.Url.ToString().IndexOf("ActiveModule.aspx") < 0;
				if (!flag4)
				{
					string routeUrl = ModuleUtils.GetRouteUrl(request.QueryString["AMID"].ToString().Trim().ToInt());
					current.AmId = request.QueryString["AMID"].ToString().Trim().ToInt();
					bool flag5 = routeUrl.IndexOf("?") < 0;
					if (flag5)
					{
						context.Response.Redirect(routeUrl + "?" + context.Request.QueryString, false);
					}
					else
					{
						context.Response.Redirect(routeUrl + "&" + context.Request.QueryString, false);
					}
				}
			}
		}

		private void application_AuthenticateRequest(object sender, EventArgs e)
		{
			HttpContext context = ((HttpApplication)sender).Context;
			HttpRequest request = context.Request;
			LogicContext current = LogicContext.Current;
			bool flag = current != null && string.IsNullOrWhiteSpace(current.UserId);
			if (flag)
			{
				context.User = null;
			}
			bool isAuthenticated = request.IsAuthenticated;
			if (!isAuthenticated)
			{
				string filePath = request.FilePath;
				bool flag2 = !StringHelper.EqualsLastStr(filePath, ".css") && !StringHelper.EqualsLastStr(filePath, ".js") && !StringHelper.EqualsLastStr(filePath, ".htc") && !StringHelper.EqualsLastStr(filePath, ".gif") && !StringHelper.EqualsLastStr(filePath, ".jpg") && !StringHelper.EqualsLastStr(filePath, ".png") && !StringHelper.EqualsLastStr(filePath, ".ico") && !StringHelper.EqualsLastStr(filePath, "bundles/MsAjaxJs") && !StringHelper.EqualsLastStr(filePath, "bundles/WebFormsJs") && !StringHelper.EqualsLastStr(filePath, "bundles/modernizr") && !StringHelper.EqualsLastStr(filePath, "Content/css") && !StringHelper.EqualsLastStr(filePath, "Content/ligerUI/skins/Timblue/css/css") && !StringHelper.EqualsLastStr(filePath, "Content/ligerUI/skins/Gray/css/css") && !StringHelper.EqualsLastStr(filePath, "Content/ligerUI/skins/Aqua/css/css");
				if (!flag2)
				{
					context.SkipAuthorization = true;
				}
			}
		}

		private void application_PostAcquireRequestState(object sender, EventArgs e)
		{
		}

		private void application_PreSendRequestHeaders(object sender, EventArgs e)
		{
			this.HandlerRequest(((HttpApplication)sender).Context);
		}

		private void application_PreSendRequestContent(object sender, EventArgs e)
		{
		}

		private void HandlerRequest(HttpContext context)
		{
			LogicContext current = LogicContext.Current;
			bool flag = current == null || current.HeaderIsSend;
			if (!flag)
			{
				current.HeaderIsSend = true;
				LogicSession userSession = current.UserSession;
				bool flag2 = userSession == null || string.IsNullOrEmpty(userSession.UserId) || userSession.Ignore;
				if (flag2)
				{
					FormsAuthentication.SignOut();
				}
				else
				{
					bool flag3 = userSession.RewriteAuthSession() && current.CookieUpdateTime < userSession.UpdateTime;
					if (flag3)
					{
						NameValueString nameValueString = new NameValueString();
						nameValueString.Add("U", userSession.UserId);
						nameValueString.Add("S", userSession.SessionId);
						nameValueString.Add("T", userSession.UpdateTime.To16String());
						HttpCookie authCookie = FormsAuthentication.GetAuthCookie(userSession.UserId, false);
						authCookie.Path = context.Request.ApplicationPath;
						FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(authCookie.Value);
						FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(authenticationTicket.Version, authenticationTicket.Name, authenticationTicket.IssueDate, authenticationTicket.Expiration, authenticationTicket.IsPersistent, nameValueString.LineText, authenticationTicket.CookiePath);
						authCookie.Value = FormsAuthentication.Encrypt(ticket);
						context.Response.Cookies.Set(authCookie);
					}
				}
				bool flag4 = userSession == null || string.IsNullOrEmpty(userSession.UserId) || !userSession.Ignore;
				if (flag4)
				{
				}
			}
		}

		private void application_EndRequest(object sender, EventArgs e)
		{
			this.HandlerRequest(((HttpApplication)sender).Context);
		}
	}
}
