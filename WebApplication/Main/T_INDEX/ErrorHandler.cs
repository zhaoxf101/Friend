using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TIM.T_INDEX
{
	public class ErrorHandler : Page
	{
		protected HtmlForm form1;

		protected Label lblTitle;

		protected Label lblMessage;

		protected void Page_Load(object sender, EventArgs e)
		{
			Exception ex = base.Server.GetLastError();
			bool flag = ex != null;
			if (flag)
			{
				bool flag2 = ex.InnerException != null;
				if (flag2)
				{
					ex = ex.InnerException;
				}
				string text = "<div id='divMsgDetail' style='display:none;height:160px;overflow-y:auto;'><b>堆栈信息:</b>" + ex.StackTrace.ToString() + "</div>";
				this.lblMessage.Text = text;
				string text2 = "<div id='divTitle'><b>错误信息：</b>" + ex.Message.ToString() + "</div>";
				this.lblTitle.Text = text2;
			}
			else
			{
				string text3 = "<div id='divMsgDetail' style='display:none;height:160px;'><b>错误信息:</b>无<br><b>堆栈信息:</b>无</div>";
				this.lblMessage.Text = text3;
			}
			base.Server.ClearError();
		}
	}
}
