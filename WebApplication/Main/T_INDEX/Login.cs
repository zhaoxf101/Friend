using System;
using System.Web.Security;
using System.Web.UI;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Page;
using TIM.T_WEBCTRL;

namespace TIM.T_INDEX
{
	public class Login : SystemTitlePage
	{
		protected UpdatePanel UpNested;

		protected TimTextBox UserName;

		protected TimTextBox Password;

		protected TimButton btnLogin;

		protected TimLiteral litMessage;

		public new NestedSite Master
		{
			get
			{
				return (NestedSite)base.Master;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Page.Form.DefaultButton = "";
		}

		protected void btnLogin_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = UserUtils.Login(this.UserName.Text.Trim().ToUpper(), this.Password.Text, base.Request.UserHostAddress.ToString());
				if (flag)
				{
					base.Response.Redirect(FormsAuthentication.GetRedirectUrl(this.UserName.Text.Trim().ToUpper(), false), false);
				}
			}
			catch (Exception ex)
			{
				this.litMessage.Text = ex.Message;
			}
		}
	}
}
