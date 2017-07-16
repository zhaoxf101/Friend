using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using TIM.T_KERNEL.Log;
using TIM.T_KERNEL.SystemInit;
using TIM.T_KERNEL.TaskScheduler;
using TIM.T_KERNEL.Utils;

namespace TIM.T_KERNEL
{
	public static class AppRuntime
	{
		public static string AppId = "FTIMI.WebApp";

		public static string AppVersion = "1.0.0.0";

		public static string AppName = "JavaScript";

		public static string AppType = string.Empty;

		public static string AppSiteName = string.Empty;

		public static string AppRootPath = string.Empty;

		public static string AppVirtualPath = string.Empty;

		public static AppBitType AppBit = AppBitType.None;

		public static string MachineName = string.Empty;

		public static DateTime UltDateTime = new DateTime(1899, 12, 31);

		public static string ModifierIdKey = "MODIFIERID";

		private static bool m_stopping = false;

		private static long _offsetTicks = 0L;

		public static DateTime StartTime = DateTime.Now;

		public const string AppDllRegex = "T_*.dll";

		public const string ActiveRoutePath = "ActiveModule.aspx?AMID=";

		public const string UltDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

		internal const int CacheUpdatePeriodMinutes = 1;

		internal const int AuthSessionUpdatePeriodMinutes = 60;

		internal const int AuthSessionBeforeUpdateMinutes = 5;

		internal const int MemorySessionUpdatePeriodMinutes = 5;

		internal const string AuthUltSessionId = "876978727978717673657871";

		internal const int SessionExpirePeriodMinutes = 480;

		internal const string DbTableCacheManagerProvider = "DbTableCacheManagerName";

		internal const string DbTableCacheName = "__TIM_DBTABLE_CACHE__";

		public const string WfId_SuffixKey = "_WFID";

		public const string WfRunId_SuffixKey = "_WFRUNID";

		public const string WfpId_SuffixKey = "_WFPID";

		public const string WfTodo_SuffixKey = "_WFTODO";

		public const string WfDone_SuffixKey = "_WFDONE";

		public const string WfpActDesc_SuffixKey = "_WFPACTDESC";

		public const string DFS_Group_SuffixKey = "_FGID";

		public const string DFS_Files_SuffixKey = "_FILES";

		public const string DFS_GenGroupIdKey = "DFS_FGID";

		public const string DFS_GenFileIdKey = "DFS_FILEID";

		public const long MaxFileSize = 31457280L;

		public const string SubTotalDesc = "小计";

		public const string SubTotalRowColor = "#7CB5EC";

		public const string TotalDesc = "合计";

		public const string TotalRowColor = "#9DC8F1";

		public const string GrandTotalDesc = "累计";

		public const string GrandTotalRowColor = "#82CB7A";

		private static DateTime _DbQueryDateTime;

		internal static bool IsStopping
		{
			get
			{
				return AppRuntime.m_stopping;
			}
		}

		public static DateTime ServerDateTime
		{
			get
			{
				return DateTime.Now.AddTicks(Interlocked.Read(ref AppRuntime._offsetTicks));
			}
			internal set
			{
				long num = value.Ticks - DateTime.Now.Ticks;
				bool flag = Math.Abs(num - AppRuntime._offsetTicks) <= 20000000L;
				if (!flag)
				{
					Interlocked.Exchange(ref AppRuntime._offsetTicks, num);
				}
			}
		}

		internal static void Start()
		{
			AppRuntime.AppType = ((Environment.CurrentDirectory == AppDomain.CurrentDomain.BaseDirectory) ? "WIN" : "WEB");
			AppRuntime.AppSiteName = HostingEnvironment.SiteName;
			AppRuntime.AppRootPath = HostingEnvironment.ApplicationPhysicalPath;
			string applicationVirtualPath = HostingEnvironment.ApplicationVirtualPath;
			AppRuntime.AppVirtualPath = ((applicationVirtualPath != "/") ? (applicationVirtualPath + "/") : applicationVirtualPath);
			AppRuntime.AppBit = ((IntPtr.Size != 4) ? ((IntPtr.Size != 8) ? AppBitType.None : AppBitType.Bit64) : AppBitType.Bit32);
			AppRuntime.MachineName = Environment.MachineName;
			Environment.SetEnvironmentVariable("NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK");
			Environment.SetEnvironmentVariable("NLS_DATE_FORMAT", "YYYY-MM-DD HH24:MI:SS");
			Environment.SetEnvironmentVariable("NLS_TIMESTAMP_FORMAT", "YYYY-MM-DD HH24:MI:SS");
			string str = AppConfig.TNS_ADMIN;
			bool flag = !string.IsNullOrEmpty(str);
			if (flag)
			{
				Environment.SetEnvironmentVariable("TNS_ADMIN", str);
			}
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AppRuntime.CurrentDomain_AssemblyResolve);
			AppRuntime._DbQueryDateTime = AppUtils.GetDbServerDateTime();
			AppRuntime.ServerDateTime = AppRuntime._DbQueryDateTime;
			try
			{
				ComponentInit.Init();
				UsersInit.Init();
                TIM.T_KERNEL.SystemInit.SystemInit.Init();
				PermissionOpInit.Init();
				CacheEvent.UpdateUCache();
			}
			catch (Exception ex)
			{
				AppEventLog.Error("AppRuntime 异常" + ex.Message);
			}
			TimerManager.Initialize();
			TimerManager.Instance.AddTask(TimerCategory.SessionTask, "AUTHSESSION", typeof(AuthSessionUpdateTask), string.Empty, string.Empty);
			TimerManager.Instance.AddTask(TimerCategory.SessionTask, "MEMORYSESSION", typeof(UCacheUpdateTask), string.Empty, string.Empty);
			TimerManager.Instance.AddTask(TimerCategory.TableCacheTask, "TABLECACHE", typeof(UCacheUpdateTask), string.Empty, string.Empty);
			TimerManager.Instance.AddTask(TimerCategory.TimeModuleTask, "TIMEMODULE", typeof(JobScheduleTask), AppConfig.DefaultDbId, "ADMIN");
		}

		internal static void ReStart()
		{
			try
			{
				HttpRuntime.UnloadAppDomain();
			}
			catch
			{
				File.SetLastWriteTimeUtc(HostingEnvironment.ApplicationPhysicalPath + "\\Web.config", DateTime.UtcNow);
			}
		}

		internal static void Stopping()
		{
			AppRuntime.m_stopping = true;
			TimerManager.Instance.Stop();
		}

		internal static void Stop()
		{
			Thread.Sleep(3000);
		}

		internal static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			string str = string.Empty;
			return Assembly.LoadFrom(((AppRuntime.AppBit != AppBitType.Bit32) ? (AppRuntime.AppRootPath + "ODP.NET.x64") : (AppRuntime.AppRootPath + "ODP.NET.x86")) + "\\Oracle.DataAccess.dll");
		}
	}
}
