using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using TIM.T_KERNEL;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_KERNEL.Helper;
using TIM.T_KERNEL.Menu;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Page;

namespace TIM.T_INDEX
{
	public class Index : SystemTitlePage
	{
		public string _UserName = string.Empty;

		public string _ConpanyName = string.Empty;

		public string _Menu = string.Empty;

		public new NestedSite Master
		{
			get
			{
				return (NestedSite)base.Master;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !base.IsPostBack;
			if (flag)
			{
				string value = "0";
				bool flag2 = base.Request.Params["FMDID"] != null;
				if (flag2)
				{
					value = base.Request.Params["FMDID"].ToString();
				}
				int mdid = 0;
				bool flag3 = string.IsNullOrWhiteSpace(value);
				if (flag3)
				{
					mdid = value.ToInt();
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Clear();
				List<TimMenu> children = FuncModelUtils.FilterUserMenu(FuncModelUtils.GetFuncModelMenu(mdid), LogicContext.Current.UserId).Children;
				foreach (TimMenu current in children)
				{
					bool flag4 = current.Type != ModuleType.C;
					if (!flag4)
					{
						stringBuilder.Append("<li>");
						stringBuilder.Append("<a href='#'><span>");
						stringBuilder.Append(" <img src='images/menu_icon1.png'/></span>" + current.Name + "</a>");
						List<TimMenu> children2 = FuncModelUtils.FilterUserMenu(FuncModelUtils.GetFuncModelMenu(current.Id), LogicContext.Current.UserId).Children;
						stringBuilder.Append("<ul>");
						foreach (TimMenu current2 in children2)
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"<li><a onclick=\"f_addTab(",
								current2.Id,
								", '",
								current2.Name,
								"', '",
								current2.Url,
								"');\">",
								current2.Name,
								"</a></li>"
							}));
						}
						stringBuilder.Append("</ul>");
						stringBuilder.Append("</li>");
					}
				}
				this._Menu = stringBuilder.ToString();
			}
			LogicContext current3 = LogicContext.Current;
			bool flag5 = current3 != null;
			if (flag5)
			{
				this._UserName = current3.UserName;
				SystemInfo systemInfo = SystemInfoUtils.GetSystemInfo();
				this._ConpanyName = systemInfo.Name;
			}
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
