using System;
using System.Web;
using System.Web.Optimization;
using TIM.T_KERNEL.Log;
using TIM.T_KERNEL.Optimization;

namespace TIM.T_KERNEL
{
	public class AppGlobal : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.CurrentDomain_UnhandledException);
			AppRuntime.Start();
			AppEventLog.Warning("TIMMIS STARTED");
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			AppEventLog.Error("CurrentDomain UnhandledException: " + e.ExceptionObject.ToString());
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			bool flag = !base.Request.AppRelativeCurrentExecutionFilePath.Equals("~/");
			if (!flag)
			{
				HttpContext.Current.RewritePath("~/default.aspx");
			}
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			HttpContext context = ((HttpApplication)sender).Context;
			bool flag = context == null;
			if (!flag)
			{
				Exception exception = context.Server.GetLastError();
				bool flag2 = exception == null;
				if (!flag2)
				{
					base.Server.Transfer("~/T_INDEX/ErrorHandler.aspx");
					bool flag3 = exception.InnerException != null;
					if (flag3)
					{
						exception = exception.InnerException;
					}
					AppEventLog.Error(exception.Message + exception.StackTrace);
					context.ClearError();
				}
			}
		}

		protected void Application_End(object sender, EventArgs e)
		{
			AppRuntime.Stopping();
			string msg = "TIMMIS Stopped";
			AppRuntime.Stop();
			AppEventLog.Error(msg);
		}

		protected void Session_Start(object sender, EventArgs e)
		{
		}

		protected void Session_End(object sender, EventArgs e)
		{
		}
	}
}
