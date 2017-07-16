using System;
using System.Web.Security;
using System.Web.UI;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Page;
using TIM.T_WEBCTRL;

namespace TIM.T_INDEX
{
	public class Logout : PageBase
	{
		protected UpdatePanel UpNested;

		protected TimButton btnLogin;

		public new NestedSite Master
		{
			get
			{
				return (NestedSite)base.Master;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void btnLogout_Click(object sender, EventArgs e)
		{
			base.Response.Cache.SetExpires(DateTime.Now.AddDays(-1.0));
			UserUtils.Logout();
			FormsAuthentication.SignOut();
			ScriptManager.RegisterStartupScript(this, base.GetType(), "Logout", "window.open('../Default.aspx','_parent');", true);
		}
	}
}
