using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Master
{
	public class TMasterSlave : TMasterBase
	{
		private TimGridView m_curGrid = null;

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

		protected TimButtonMenu btnAttach;

		protected ContentPlaceHolder CPHButton;

		protected TimButtonMenu btnWorkflow;

		protected UpdatePanel UpContent;

		protected ContentPlaceHolder CPHContent;

		protected UpdatePanel UpSlaveMenu;

		protected TimButtonMenu btnSlaveInsert;

		protected TimButtonMenu btnSlaveCopy;

		protected TimButtonMenu btnSlaveView;

		protected TimButtonMenu btnSlaveEdit;

		protected TimButtonMenu btnSlaveDelete;

		protected TimButton btnSlaveUpdate;

		protected TimPagingBar GridPagingBar;

		protected UpdatePanel UpSlave;

		protected ContentPlaceHolder CPHSlave;

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

		public UpdatePanel UpContentPlace
		{
			get
			{
				return this.UpContent;
			}
		}

		public ContentPlaceHolder CphContent
		{
			get
			{
				return this.CPHContent;
			}
		}

		public UpdatePanel UpSlaveMenuPlace
		{
			get
			{
				return this.UpSlaveMenu;
			}
		}

		public UpdatePanel UpSlavePlace
		{
			get
			{
				return this.UpSlave;
			}
		}

		public ContentPlaceHolder CphSlave
		{
			get
			{
				return this.CPHSlave;
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

		public TimButtonMenu BtnSlaveInsert
		{
			get
			{
				return this.btnSlaveInsert;
			}
		}

		public TimButtonMenu BtnSlaveCopy
		{
			get
			{
				return this.btnSlaveCopy;
			}
		}

		public TimButtonMenu BtnSlaveView
		{
			get
			{
				return this.btnSlaveView;
			}
		}

		public TimButtonMenu BtnSlaveEdit
		{
			get
			{
				return this.btnSlaveEdit;
			}
		}

		public TimButtonMenu BtnSlaveDelete
		{
			get
			{
				return this.btnSlaveDelete;
			}
		}

		public TimButton BtnSlaveUpdate
		{
			get
			{
				return this.btnSlaveUpdate;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.TempletWorkflowButton = this.btnWorkflow;
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
			this.btnAttach.Visible = false;
		}

		public override void SetMenu_OnlyViewEdit()
		{
			this.btnInsert.Visible = false;
			this.btnCopy.Visible = false;
			this.btnDelete.Visible = false;
			this.btnPrint.Visible = false;
			this.btnPreview.Visible = false;
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
			this.btnAttach.Visible = true;
		}
	}
}
