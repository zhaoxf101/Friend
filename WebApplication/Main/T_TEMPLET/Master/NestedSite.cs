using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_TEMPLET.Master
{
	public class NestedSite : MasterPage
	{
		protected ContentPlaceHolder NestedHead;

		protected ContentPlaceHolder NestedBody;

		protected ContentPlaceHolder NestedTemplet;

		protected ContentPlaceHolder NestedSync;

		public UpdatePanel UpScriptPlace
		{
			get
			{
				return ((Site)base.Master).UpScriptPlace;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}
	}
}
