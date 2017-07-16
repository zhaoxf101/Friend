using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace TIM.T_WEBCTRL
{
	internal sealed class UploadLog
	{
		private static string GetLogLocation
		{
			get
			{
				string text = ConfigurationManager.AppSettings["logLocation"];
				bool flag = !string.IsNullOrEmpty(text) && !Path.IsPathRooted(text);
				if (flag)
				{
					text = HttpContext.Current.Server.MapPath(text);
				}
				return text;
			}
		}

		private UploadLog()
		{
		}

		public static void Log(string message)
		{
		}

		public static void Log(UploadedFile file, string uploadId)
		{
		}

		public static void Log(string message, string uploadId)
		{
		}

		public static void Log(HttpWorkerRequest worker, string uploadId)
		{
		}
	}
}
