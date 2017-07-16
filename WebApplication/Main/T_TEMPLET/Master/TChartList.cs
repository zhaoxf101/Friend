using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Master
{
	public class TChartList : TMasterBase
	{
		private TimGridView m_curGrid = null;

		protected ContentPlaceHolder CPHHead;

		protected UpdatePanel UpQuery;

		protected ContentPlaceHolder CPHQuery;

		protected UpdatePanel UpMenu;

		protected TimButtonMenu btnQuery;

		protected TimButtonMenu btnPrint;

		protected TimButtonMenu btnPreview;

		protected ContentPlaceHolder CPHButton;

		protected UpdatePanel UpChart;

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

		public UpdatePanel UpChartPlace
		{
			get
			{
				return this.UpChart;
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

		public TimButtonMenu BtnQuery
		{
			get
			{
				return this.btnQuery;
			}
		}

		public TimButtonMenu BtnPrint
		{
			get
			{
				return this.btnPrint;
			}
		}

		public TimButtonMenu BtnPreview
		{
			get
			{
				return this.btnPreview;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public override void SetMenu_HideAllStdBtn()
		{
			this.btnQuery.Visible = false;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
		}

		public override void SetMenu_OnlyQuery()
		{
			this.btnQuery.Visible = true;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
		}

		public override void SetMenu_OnlyViewEdit()
		{
			this.btnQuery.Visible = false;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
		}

		public override void SetMenu_OnlyAttach()
		{
			this.btnQuery.Visible = false;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
		}
	}
}
