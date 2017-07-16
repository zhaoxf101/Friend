using System;
using TIM.T_KERNEL.DbTableCache;

namespace TIM.T_TEMPLET.Page
{
	public class SystemTitlePage : System.Web.UI.Page
    {
		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);
			base.Title = SystemInfoUtils.GetSystemInfo().Name;
		}
	}
}
