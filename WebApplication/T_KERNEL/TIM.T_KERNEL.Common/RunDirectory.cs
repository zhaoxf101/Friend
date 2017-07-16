using System;

namespace TIM.T_KERNEL.Common
{
	public class RunDirectory
	{
		internal const string LogPath = "Run\\Log\\";

		internal const string TempPath = "Run\\Temp\\";

		internal const string UploadPath = "Run\\Upload\\";

		internal const string DownloadPath = "Run\\Download\\";

		public static string LinkLogPath(string fileName)
		{
			return AppRuntime.AppRootPath + "Run\\Log\\" + fileName;
		}

		public static string LinkTempPath(string fileName)
		{
			return AppRuntime.AppRootPath + "Run\\Temp\\" + fileName;
		}

		public static string LinkUploadPath(string fileName)
		{
			return AppRuntime.AppRootPath + "Run\\Upload\\" + fileName;
		}

		public static string LinkDownloadPath(string fileName)
		{
			return AppRuntime.AppRootPath + "Run\\Download\\" + fileName;
		}
	}
}
