using System;
using System.Diagnostics;

namespace TIM.T_KERNEL.Log
{
	public class AppEventLog
	{
		public static void Debug(string msg)
		{
			try
			{
				bool flag = !EventLog.SourceExists("Application");
				if (flag)
				{
					EventLog.CreateEventSource("Application", "Application");
				}
				new EventLog
				{
					Source = "Application"
				}.WriteEntry(msg);
			}
			catch
			{
			}
		}

		public static void Warning(string msg)
		{
			try
			{
				bool flag = !EventLog.SourceExists("Application");
				if (flag)
				{
					EventLog.CreateEventSource("Application", "Application");
				}
				new EventLog
				{
					Source = "Application"
				}.WriteEntry(msg);
			}
			catch
			{
			}
		}

		public static void Error(string msg)
		{
			try
			{
				bool flag = !EventLog.SourceExists("Application");
				if (flag)
				{
					EventLog.CreateEventSource("Application", "Application");
				}
				new EventLog
				{
					Source = "Application"
				}.WriteEntry(msg);
			}
			catch
			{
			}
		}
	}
}
