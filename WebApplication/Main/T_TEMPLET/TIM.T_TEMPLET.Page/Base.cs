using System;
namespace TIM.T_TEMPLET.Page
{
	public class Base : System.Web.UI.Page
	{
		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);
			this.Page.Theme = "Aqua";
		}
	}
}
