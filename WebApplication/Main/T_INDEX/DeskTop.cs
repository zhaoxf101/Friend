using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_TEMPLET.Page;

namespace TIM.T_INDEX
{
	public class DeskTop : SystemTitlePage
	{
		public string objIcons = "";

		protected HtmlForm form1;

		protected void Page_Load(object sender, EventArgs e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FuncModel> modelByFatherId = FuncModelUtils.GetModelByFatherId(0);
			for (int i = 0; i < modelByFatherId.Count; i++)
			{
				FuncModel funcModel = modelByFatherId[i];
				stringBuilder.Append("{");
				stringBuilder.Append("icon: '../images/DeskTop/system.gif',");
				stringBuilder.Append("title: '" + funcModel.Name + "',");
				stringBuilder.Append("url: 'WorkSpace.aspx?FMDID=" + funcModel.Id + "',");
				stringBuilder.Append("height: '500',");
				stringBuilder.Append("width: '600',");
				stringBuilder.Append("wsstate: 'WSMAX',");
				stringBuilder.Append("showDialogId: ''}\n\r,");
			}
			stringBuilder.Append("{");
			stringBuilder.Append("icon: '../images/DeskTop/password.gif',");
			stringBuilder.Append("title: '修改密码',");
			stringBuilder.Append("url: 'PassMe.aspx',");
			stringBuilder.Append("height: '200',");
			stringBuilder.Append("width: '400',");
			stringBuilder.Append("wsstate: 'WSNORMAL',");
			stringBuilder.Append("showDialogId: ''}\n\r,");
			stringBuilder.Append("{");
			stringBuilder.Append("icon: '../images/DeskTop/close.gif',");
			stringBuilder.Append("title: '关闭',");
			stringBuilder.Append("url: 'Logout.aspx',");
			stringBuilder.Append("height: '200',");
			stringBuilder.Append("width: '400',");
			stringBuilder.Append("wsstate: 'WSNORMAL',");
			stringBuilder.Append("showDialogId: ''}\n\r");
			this.objIcons = stringBuilder.ToString();
		}
	}
}
