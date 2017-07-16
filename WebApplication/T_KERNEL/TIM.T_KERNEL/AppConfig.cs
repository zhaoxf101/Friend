using System;
using System.Configuration;
using TIM.T_KERNEL.Common;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL
{
	public sealed class AppConfig
	{
		public static string DefaultDbId;

		public static string DefaultDbDesc;

		public static DbProviderType DbMS;

		public static string DbServer;

		public static string TNS_ADMIN;

		public static string Default_Service_UserId;

		public const int SessinTimeOutHours = 10;

		public const int SessionUpdateMinutes = 5;

		static AppConfig()
		{
			AppConfig.DefaultDbId = "DEFAULT";
			AppConfig.DefaultDbDesc = "系统数据库";
			AppConfig.DbMS = DbProviderType.MSSQL;
			AppConfig.DbServer = string.Empty;
			AppConfig.TNS_ADMIN = string.Empty;
			AppConfig.Default_Service_UserId = "SERVICE";
			AppConfig.DefaultDbId = ConfigurationManager.AppSettings["DEFAULTDBID"];
			AppConfig.DefaultDbDesc = ConfigurationManager.AppSettings["DEFAULTDBDESC"];
			AppConfig.DbMS = ConfigurationManager.AppSettings["DBMS"].ToDbProviderType();
			AppConfig.DbServer = new NameValueString
			{
				NaviteText = ConfigurationManager.AppSettings["DBSERVER"]
			}.LineText;
			AppConfig.TNS_ADMIN = ConfigurationManager.AppSettings["TNS_ADMIN"];
			AppDbInstance.AddDbInstance(AppConfig.DefaultDbId, new DbConfig(AppConfig.DefaultDbId, AppConfig.DefaultDbDesc, AppConfig.DbMS, AppConfig.DbServer));
		}
	}
}
