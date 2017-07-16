using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Master
{
	public class TTreeGrid : TMasterBase
	{
		private TimGridView m_curGrid = null;

		protected ContentPlaceHolder CPHHead;

		protected UpdatePanel UpTree;

		protected TimTreeView LeftTree;

		protected UpdatePanel UpQuery;

		protected ContentPlaceHolder CPHQuery;

		protected UpdatePanel UpMenu;

		protected TimButtonMenu btnInsert;

		protected TimButtonMenu btnCopy;

		protected TimButtonMenu btnView;

		protected TimButtonMenu btnEdit;

		protected TimButtonMenu btnDelete;

		protected TimButtonMenu btnQuery;

		protected TimButtonMenu btnPrint;

		protected TimButtonMenu btnPreview;

		protected TimButtonMenu btnReportStyle;

		protected TimButtonMenu btnAttach;

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

		public TimButtonMenu BtnInsert
		{
			get
			{
				return this.btnInsert;
			}
		}

		public TimButtonMenu BtnCopy
		{
			get
			{
				return this.btnCopy;
			}
		}

		public TimButtonMenu BtnView
		{
			get
			{
				return this.btnView;
			}
		}

		public TimButtonMenu BtnEdit
		{
			get
			{
				return this.btnEdit;
			}
		}

		public TimButtonMenu BtnDelete
		{
			get
			{
				return this.btnDelete;
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

		public TimButtonMenu BtnReportStyle
		{
			get
			{
				return this.btnReportStyle;
			}
		}

		public TimButtonMenu BtnAttach
		{
			get
			{
				return this.btnAttach;
			}
		}

		protected override void OnInit()
		{
			this.TempletPrintButton = this.btnPrint;
			this.TempletPreviewButton = this.btnPreview;
			this.TempletReportStyleButton = this.btnReportStyle;
		}

		public override void SetMenu_HideAllStdBtn()
		{
			this.btnInsert.Visible = false;
			this.btnCopy.Visible = false;
			this.btnView.Visible = false;
			this.btnEdit.Visible = false;
			this.btnDelete.Visible = false;
			this.btnQuery.Visible = false;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
			this.btnReportStyle.Visible = false;
			this.btnAttach.Visible = false;
		}

		public override void SetMenu_OnlyQuery()
		{
			this.btnInsert.Visible = false;
			this.btnCopy.Visible = false;
			this.btnView.Visible = false;
			this.btnEdit.Visible = false;
			this.btnDelete.Visible = false;
			this.btnQuery.Visible = true;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
			this.btnReportStyle.Visible = false;
			this.btnAttach.Visible = false;
		}

		public override void SetMenu_OnlyQueryAndReport()
		{
			this.btnInsert.Visible = false;
			this.btnCopy.Visible = false;
			this.btnView.Visible = false;
			this.btnEdit.Visible = false;
			this.btnDelete.Visible = false;
			this.btnQuery.Visible = true;
			this.btnPrint.Visible = true;
			this.btnPreview.Visible = true;
			this.btnReportStyle.Visible = true;
			this.btnAttach.Visible = false;
		}

		public override void SetMenu_OnlyViewEdit()
		{
			this.btnInsert.Visible = false;
			this.btnCopy.Visible = false;
			this.btnDelete.Visible = false;
			this.btnQuery.Visible = false;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
			this.btnReportStyle.Visible = false;
			this.btnAttach.Visible = false;
		}

		public override void SetMenu_OnlyAttach()
		{
			this.btnInsert.Visible = false;
			this.btnCopy.Visible = false;
			this.btnView.Visible = false;
			this.btnEdit.Visible = false;
			this.btnDelete.Visible = false;
			this.btnQuery.Visible = false;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
			this.btnReportStyle.Visible = false;
			this.btnAttach.Visible = true;
		}
	}
}
