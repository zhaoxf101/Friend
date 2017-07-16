using System;
using System.Globalization;
using System.Threading;

namespace TIM.T_KERNEL
{
	internal sealed class GlobalCulture
	{
		private static CultureInfo ZhCnCultureInfo;

		public static CultureInfo DefaultCultureInfo
		{
			get
			{
				return GlobalCulture.ZhCnCultureInfo;
			}
		}

		static GlobalCulture()
		{
			bool flag = GlobalCulture.ZhCnCultureInfo != null;
			if (!flag)
			{
				GlobalCulture.ZhCnCultureInfo = new CultureInfo("zh-CN")
				{
					DateTimeFormat = 
					{
						LongDatePattern = "yyyy-MM-dd",
						ShortDatePattern = "yyyy-MM-dd",
						DateSeparator = "-",
						LongTimePattern = "HH:mm:ss",
						ShortTimePattern = "HH:mm:ss"
					}
				};
			}
		}

		public static void SetContextCulture()
		{
			Thread.CurrentThread.CurrentCulture = GlobalCulture.DefaultCultureInfo;
			Thread.CurrentThread.CurrentUICulture = GlobalCulture.DefaultCultureInfo;
		}
	}
}
