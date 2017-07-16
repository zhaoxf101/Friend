using System;
using System.Resources;

namespace TIM.T_WEBCTRL
{
	internal sealed class SR
	{
		private ResourceManager _rm;

		private static SR _loader = null;

		private static object _lock = new object();

		private ResourceManager Resources
		{
			get
			{
				return this._rm;
			}
		}

		private SR()
		{
			this._rm = new ResourceManager("TIM.T_WEBCTRL.TimPagingBar.TimPagingBar", base.GetType().Assembly);
		}

		private static SR GetLoader()
		{
			bool flag = SR._loader == null;
			if (flag)
			{
				object @lock = SR._lock;
				lock (@lock)
				{
					bool flag3 = SR._loader == null;
					if (flag3)
					{
						SR._loader = new SR();
					}
				}
			}
			return SR._loader;
		}

		public static string GetString(string name)
		{
			SR loader = SR.GetLoader();
			string localized = null;
			bool flag = loader != null;
			if (flag)
			{
				localized = loader.Resources.GetString(name);
			}
			return localized ?? string.Empty;
		}
	}
}
