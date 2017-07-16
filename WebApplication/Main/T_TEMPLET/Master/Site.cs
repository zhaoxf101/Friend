using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TIM.T_TEMPLET.Master
{
	public class Site : MasterPage
	{
		protected ContentPlaceHolder SiteHead;

		protected HtmlForm SiteForm;

		protected ScriptManager SiteSM;

		protected UpdatePanel UpScript;

		protected ContentPlaceHolder SiteBody;

		protected ContentPlaceHolder SiteTemplet;

		protected ContentPlaceHolder SiteSync;

		protected Button virtualBtn;

		public UpdatePanel UpScriptPlace
		{
			get
			{
				return this.UpScript;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}
	}
}
