using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Configuration;

namespace TIM.T_WEBCTRL
{
	public class UtoUploadConfiguration
	{
		private long _maxRequestLength = -1L;

		private NameValueConfigurationSection _UploadParser;

		private NameValueConfigurationSection _UploadStreamProvider;

		private StatusManagerConfigurationSection _StatusManager;

		private static readonly string UtoUploadConfig = "UtoUploadConfig";

		public static bool HandleRequests
		{
			get
			{
				string str = UtoUploadConfiguration.UploadParser["handleRequests"];
				bool flag;
				bool flag2 = !bool.TryParse(str, out flag);
				if (flag2)
				{
					flag = true;
				}
				return flag;
			}
		}

		public static long MaxRequestLength
		{
			get
			{
				return UtoUploadConfiguration.getConfiguration().calcMaxRequestLength;
			}
		}

		public static StatusManagerConfigurationSection StatusManager
		{
			get
			{
				return UtoUploadConfiguration.getConfiguration()._StatusManager;
			}
		}

		public static NameValueConfigurationSection UploadParser
		{
			get
			{
				return UtoUploadConfiguration.getConfiguration()._UploadParser;
			}
		}

		public static NameValueConfigurationSection UploadStreamProvider
		{
			get
			{
				return UtoUploadConfiguration.getConfiguration()._UploadStreamProvider;
			}
		}

		private long calcMaxRequestLength
		{
			get
			{
				bool flag = this._maxRequestLength == -1L;
				if (flag)
				{
					this.GetMaxRequestLength(HttpContext.Current);
				}
				return this._maxRequestLength;
			}
		}

		private UtoUploadConfiguration()
		{
		}

		private void GetMaxRequestLength(HttpContext context)
		{
			string s = UtoUploadConfiguration.UploadParser["maxUploadRequestLength"];
			long num;
			bool flag = !long.TryParse(s, out num);
			if (flag)
			{
				num = -1L;
			}
			bool flag2 = num > 0L;
			if (flag2)
			{
				this._maxRequestLength = num;
			}
			else
			{
				long num2 = this.getMaxAllowedContentLength(context);
				long num3 = this.getHttpRuntimeMaxRequestLength(context);
				bool flag3 = num2 > 0L;
				if (flag3)
				{
					this._maxRequestLength = ((num2 < num3) ? num2 : num3);
				}
				else
				{
					this._maxRequestLength = num3;
				}
				bool flag4 = this._maxRequestLength <= 0L;
				if (flag4)
				{
					this._maxRequestLength = -1L;
				}
			}
		}

		private static UtoUploadConfiguration getConfiguration()
		{
			HttpContext current = HttpContext.Current;
			UtoUploadConfiguration configuration = null;
			bool flag = current != null;
			if (flag)
			{
				configuration = (current.Items[UtoUploadConfiguration.UtoUploadConfig] as UtoUploadConfiguration);
			}
			bool flag2 = configuration == null;
			if (flag2)
			{
				configuration = new UtoUploadConfiguration();
				bool flag3 = current == null;
				if (flag3)
				{
					configuration._UploadParser = (ConfigurationManager.GetSection("UtoUpload/uploadParser") as NameValueConfigurationSection);
					configuration._UploadStreamProvider = (ConfigurationManager.GetSection("UtoUpload/uploadStreamProvider") as NameValueConfigurationSection);
					configuration._StatusManager = (ConfigurationManager.GetSection("UtoUpload/statusManager") as StatusManagerConfigurationSection);
				}
				else
				{
					configuration._UploadParser = (current.GetSection("UtoUpload/uploadParser") as NameValueConfigurationSection);
					configuration._UploadStreamProvider = (current.GetSection("UtoUpload/uploadStreamProvider") as NameValueConfigurationSection);
					configuration._StatusManager = (current.GetSection("UtoUpload/statusManager") as StatusManagerConfigurationSection);
				}
				bool flag4 = configuration._UploadParser == null;
				if (flag4)
				{
					configuration._UploadParser = new NameValueConfigurationSection(null, null);
				}
				bool flag5 = configuration._UploadStreamProvider == null;
				if (flag5)
				{
					configuration._UploadStreamProvider = new NameValueConfigurationSection(null, null);
				}
				bool flag6 = configuration._StatusManager == null;
				if (flag6)
				{
					configuration._StatusManager = new StatusManagerConfigurationSection(null, null);
				}
				bool flag7 = current != null;
				if (flag7)
				{
					current.Items[UtoUploadConfiguration.UtoUploadConfig] = configuration;
				}
			}
			return configuration;
		}

		private long getHttpRuntimeMaxRequestLength(HttpContext context)
		{
			long num = -1L;
			object section = null;
			try
			{
				section = context.GetSection("system.web/httpRuntime");
			}
			catch
			{
			}
			bool flag = section != null;
			if (flag)
			{
				num = (long)(((HttpRuntimeSection)section).MaxRequestLength * 1024);
			}
			return num;
		}

		private long getMaxAllowedContentLength(HttpContext context)
		{
			long num = 0L;
			try
			{
				Assembly assembly = Assembly.Load("Microsoft.Web.Administration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
				bool flag = assembly != null;
				if (flag)
				{
					object target = assembly.GetType("Microsoft.Web.Administration.WebConfigurationManager").InvokeMember("GetSection", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[]
					{
						"system.webServer/security/requestFiltering"
					});
					object obj3 = target.GetType().InvokeMember("GetChildElement", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, target, new object[]
					{
						"requestLimits"
					});
					num = (long)obj3.GetType().InvokeMember("GetAttributeValue", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, obj3, new object[]
					{
						"maxAllowedContentLength"
					});
				}
			}
			catch
			{
			}
			return num;
		}
	}
}
