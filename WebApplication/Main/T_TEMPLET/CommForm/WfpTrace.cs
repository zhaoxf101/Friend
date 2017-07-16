using System;
using System.Web.UI.WebControls;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Page;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.CommForm
{
	public class WfpTrace : TListingBase
	{
		private WfRunLog _MasterEntity = new WfRunLog();

		protected TimLabel lblQueryId;

		protected TimTextBox txtQueryId;

		protected TimLabel lblQueryName;

		protected TimTextBox txtQueryName;

		protected TimGridView gvMaster;

		public new TListing Master
		{
			get
			{
				return (TListing)base.Master;
			}
		}

		protected override void InitModuleInfo()
		{
			base.MdId = 101021001;
			base.MdName = "流程跟踪";
			base.Width = 900;
			base.Height = 600;
		}

		protected override void InitTemplet()
		{
			base.CurMaster = this.Master;
			base.CurEntity = this._MasterEntity;
			base.CurGrid = this.gvMaster;
			base.DataKeyNames = new string[]
			{
				"NO1",
				"NO2",
				"WFID",
				"WFRUNID"
			};
		}

		protected override void SetCtrlState()
		{
			this.SetMenu_HideAllStdBtn();
		}

		protected override void OnInitComplete()
		{
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected override bool OnPreQuery()
		{
			this._MasterEntity.QueryWfId = base.PageParam.GetString("WFID");
			this._MasterEntity.QueryWfRunId = base.PageParam.GetString("WFRUNID");
			return true;
		}

		protected override void OnLoadRecord()
		{
			bool flag = !base.IsPostBack;
			if (flag)
			{
				base.CurMaster_OnQuery(null, null);
			}
		}

		protected override void OnPreLoad()
		{
			base.CurGrid.OnClientDblClick = "return false;";
		}

		protected override void OnLoadComplete()
		{
		}

		protected override void CurGrid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			bool flag = e.Row.RowType == DataControlRowType.DataRow;
			if (flag)
			{
				e.Row.Cells[1].Text = base.GetWfpName(base.CurGrid.DataKeys[e.Row.RowIndex]["WFID"].ToString().Trim(), e.Row.Cells[1].Text.Trim());
				e.Row.Cells[2].Text = base.GetWfUsersName(e.Row.Cells[2].Text);
				e.Row.Cells[3].Text = base.GetWfUsersName(e.Row.Cells[3].Text);
				e.Row.Cells[4].Text = base.GetWfpActionDesc(e.Row.Cells[4].Text);
			}
		}
	}
}
