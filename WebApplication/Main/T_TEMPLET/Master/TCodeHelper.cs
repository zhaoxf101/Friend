using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Master
{
	public class TCodeHelper : TMasterBase
	{
		private TimGridView m_curGrid = null;

		protected ContentPlaceHolder CPHHead;

		protected UpdatePanel UpQuery;

		protected ContentPlaceHolder CPHQuery;

		protected UpdatePanel UpMenu;

		protected TimButtonMenu btnOk;

		protected TimButtonMenu btnClose;

		protected TimButtonMenu btnQuery;

		protected ContentPlaceHolder CPHButton;

		protected UpdatePanel UpContent;

		protected TimPagingBar GridPagingBar;

		protected ContentPlaceHolder CPHContent;

		protected UpdatePanel UpTemplet;

		protected ContentPlaceHolder CPHTemplet;

		protected ContentPlaceHolder CPHSync;

		public TimGridView CurGrid
		{
			get
			{
				return this.m_curGrid;
			}
			set
			{
				this.m_curGrid = value;
			}
		}

		public TimPagingBar CurPagingBar
		{
			get
			{
				return this.GridPagingBar;
			}
		}

		public UpdatePanel UpScriptPlace
		{
			get
			{
				return this._Maintenance.UpScriptPlace;
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

		public TimButtonMenu BtnOk
		{
			get
			{
				return this.btnOk;
			}
		}

		public TimButtonMenu BtnClosse
		{
			get
			{
				return this.btnClose;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public override void SetMenu_HideAllStdBtn()
		{
			this.btnOk.Visible = false;
			this.btnClose.Visible = false;
		}
	}
}
