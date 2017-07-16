using System;
using System.Web.UI;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Page;
using TIM.T_WEBCTRL;

namespace TIM.T_INDEX
{
	public class PassMe : PageBase
	{
		protected UpdatePanel UpNested;

		protected TimLabel lblPsw;

		protected TimTextBox txtPsw;

		protected TimLabel lblNewPsw;

		protected TimTextBox txtNewPsw;

		protected TimLabel lblConfirmNewPsw;

		protected TimTextBox txtConfirmNewPsw;

		protected TimButton btnPass;

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

		protected void btnPass_Click(object sender, EventArgs e)
		{
			bool flag = this.txtPsw.Text == this.txtNewPsw.Text;
			if (flag)
			{
				base.PromptDialog(string.Format("与当前密码一致，无需修改！", base.UserId));
			}
			else
			{
				bool flag2 = this.txtNewPsw.Text != this.txtConfirmNewPsw.Text;
				if (flag2)
				{
					base.PromptDialog(string.Format("两次输入的密码不匹配！", base.UserId));
				}
				else
				{
					bool flag3 = !UserUtils.UpdatePsw(base.UserId, this.txtPsw.Text, this.txtNewPsw.Text, this.txtConfirmNewPsw.Text);
					if (flag3)
					{
						base.PromptDialog(string.Format("用户({0})密码修改失败！", base.UserId));
					}
					else
					{
						base.PromptDialog(string.Format("用户({0})密码修改成功，下次登录时生效！", base.UserId));
					}
				}
			}
		}
	}
}
