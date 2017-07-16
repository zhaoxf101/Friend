using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Master
{
	public class TTreeChart : TMasterBase
	{
		protected ContentPlaceHolder CPHHead;

		protected UpdatePanel UpTree;

		protected TimTreeView LeftTree;

		protected UpdatePanel UpQuery;

		protected ContentPlaceHolder CPHQuery;

		protected UpdatePanel UpMenu;

		protected TimButtonMenu btnQuery;

		protected ContentPlaceHolder CPHButton;

		protected UpdatePanel UpContent;

		protected ContentPlaceHolder CPHContent;

		protected UpdatePanel UpTemplet;

		protected ContentPlaceHolder CPHTemplet;

		protected ContentPlaceHolder CPHSync;

		public UpdatePanel UpScriptPlace
		{
			get
			{
				return this._Maintenance.UpScriptPlace;
			}
		}

		public UpdatePanel UpTreePlace
		{
			get
			{
				return this.UpTree;
			}
		}

		public UpdatePanel UpMenuPlace
		{
			get
			{
				return this.UpMenu;
			}
		}

		public UpdatePanel UpQueryPlace
		{
			get
			{
				return this.UpQuery;
			}
		}

		public UpdatePanel UpContentPlace
		{
			get
			{
				return this.UpContent;
			}
		}

		public UpdatePanel UpTempletPlace
		{
			get
			{
				return this.UpTemplet;
			}
		}

		public TimTreeView LeftTreeV
		{
			get
			{
				return this.LeftTree;
			}
		}

		public TimButtonMenu BtnQuery
		{
			get
			{
				return this.btnQuery;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public override void SetMenu_HideAllStdBtn()
		{
			this.btnQuery.Visible = false;
		}

		public override void SetMenu_OnlyQuery()
		{
			this.btnQuery.Visible = true;
		}

		public override void SetMenu_OnlyViewEdit()
		{
			this.btnQuery.Visible = false;
		}
	}
}
