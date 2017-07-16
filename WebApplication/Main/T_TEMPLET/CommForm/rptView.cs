using System;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Page;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.CommForm
{
	public class rptView : TEditingBase
	{
		private ReportStyle _MasterEntity = new ReportStyle();

		public string sHtmlFilePath = "";

		protected TimButtonMenu btnDefault;

		public new TEditing Master
		{
			get
			{
				return (TEditing)base.Master;
			}
		}

		protected override void InitModuleInfo()
		{
			base.MdId = 101021004;
			base.MdName = "预览";
			base.Width = 900;
			base.Height = 600;
		}

		protected override void InitTemplet()
		{
			base.CurMaster = this.Master;
			base.CurEntity = this._MasterEntity;
			this.SetMenu_HideAllStdBtn();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			string sFileName = base.PageParam.GetString("RPT_HTMLPATH");
			this.sHtmlFilePath = string.Concat(new string[]
			{
				this.Page.ResolveUrl("~"),
				"RptFiles/",
				DateTime.Today.ToShortDateString(),
				"/",
				sFileName.Trim()
			});
		}
	}
}
