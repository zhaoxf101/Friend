using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Master
{
	public class TBillXGrid : TMasterBase
	{
		protected ContentPlaceHolder CPHHead;

		protected UpdatePanel UpMenu;

		protected TimButtonMenu btnInsert;

		protected TimButtonMenu btnCopy;

		protected TimButtonMenu btnEdit;

		protected TimButtonMenu btnSave;

		protected TimButtonMenu btnCancel;

		protected TimButtonMenu btnDelete;

		protected TimButtonMenu btnPrint;

		protected TimButtonMenu btnPreview;

		protected TimButtonMenu btnReportStyle;

		protected TimButtonMenu btnAttach;

		protected ContentPlaceHolder CPHButton;

		protected TimButtonMenu btnWorkflow;

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

		public UpdatePanel UpMenuPlace
		{
			get
			{
				return this.UpMenu;
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

		public UpdatePanel UpTempletMaintenancePlace
		{
			get
			{
				return this._Maintenance.UpTempletPlace;
			}
		}

		public ContentPlaceHolder CphContent
		{
			get
			{
				return this.CPHContent;
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

		public TimButtonMenu BtnEdit
		{
			get
			{
				return this.btnEdit;
			}
		}

		public TimButtonMenu BtnSave
		{
			get
			{
				return this.btnSave;
			}
		}

		public TimButtonMenu BtnCancel
		{
			get
			{
				return this.btnCancel;
			}
		}

		public TimButtonMenu BtnDelete
		{
			get
			{
				return this.btnDelete;
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

		public TimButtonMenu BtnWorkflow
		{
			get
			{
				return this.btnWorkflow;
			}
		}

		protected override void OnInit()
		{
			this.TempletWorkflowButton = this.btnWorkflow;
			this.TempletPrintButton = this.btnPrint;
			this.TempletPreviewButton = this.btnPreview;
			this.TempletReportStyleButton = this.btnReportStyle;
		}

		public override void SetMenu_HideAllStdBtn()
		{
			this.btnInsert.Visible = false;
			this.btnCopy.Visible = false;
			this.btnEdit.Visible = false;
			this.btnSave.Visible = false;
			this.btnCancel.Visible = false;
			this.btnDelete.Visible = false;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
			this.btnReportStyle.Visible = false;
			this.btnAttach.Visible = false;
		}

		public override void SetMenu_OnlyViewEdit()
		{
			this.btnInsert.Visible = false;
			this.btnCopy.Visible = false;
			this.btnDelete.Visible = false;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
			this.btnReportStyle.Visible = false;
			this.btnAttach.Visible = false;
		}

		public override void SetMenu_OnlyAttach()
		{
			this.btnInsert.Visible = false;
			this.btnCopy.Visible = false;
			this.btnEdit.Visible = false;
			this.btnSave.Visible = false;
			this.btnCancel.Visible = false;
			this.btnDelete.Visible = false;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
			this.btnReportStyle.Visible = false;
			this.btnAttach.Visible = true;
		}
	}
}
