using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Page;
using TIM.T_WEBCTRL;
using TIM.T_WORKFLOW;

namespace TIM.T_TEMPLET.CommForm
{
	public class WfHandle : TEditingBase
	{
		private string _WfId = string.Empty;

		private string _WfName = string.Empty;

		private int _WfRunId = 0;

		private WorkflowAction _WfpAction = WorkflowAction.NULL;

		protected TimLabel lblWfId;

		protected TimDropDownList ddlWfId;

		protected TimLabel lblTips;

		protected TimLabel lblWfpId;

		protected TimDropDownList ddlWfpId;

		protected TimLabel lblNextWfpId;

		protected TimDropDownList ddlNextWfpId;

		protected TimLabel lblUsers;

		protected TimCheckBoxList cblUsers;

		protected TimLabel lblTodo;

		protected TimTextBox txtTodo;

		protected TimLabel lblOpinion;

		protected TimTextBox txtOpinion;

		protected TimButton btnOk;

		protected TimButton btnCancel;

		protected TimHiddenField hidWfpAction;

		protected TimHiddenField hidNextWfId;

		protected TimHiddenField hidNextWfpId;

		protected TimHiddenField hidRequiredTodo;

		protected TimHiddenField hidRequiredTodoUsers;

		protected TimHiddenField hidRequiredOpinion;

		protected TimHiddenField hidTodo;

		public new TEditing Master
		{
			get
			{
				return (TEditing)base.Master;
			}
		}

		protected override void InitModuleInfo()
		{
			base.MdId = 101021002;
			base.MdName = "事务处理";
			base.Width = 560;
			base.Height = 560;
		}

		protected override void InitTemplet()
		{
			base.CurMaster = this.Master;
		}

		protected override void InitCtrlValue()
		{
			this._WfId = base.PageParam.GetString("WFID");
			this._WfRunId = base.PageParam.GetInt("WFRUNID");
			this._WfpAction = base.PageParam.GetString("WFPACTION").Trim().ToWorkflowAction();
			base.Workflow = new WorkflowEngine();
			base.Workflow.Init(this._WfId, this._WfRunId);
		}

		protected override void OnPreLoad()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this._WfId = base.PageParam.GetString("WFID");
				this._WfRunId = base.PageParam.GetInt("WFRUNID");
				this._WfpAction = base.PageParam.GetString("WFPACTION").Trim().ToWorkflowAction();
				base.Workflow = new WorkflowEngine();
				base.Workflow.Init(this._WfId, this._WfRunId);
			}
		}

		protected override void SetCtrlState()
		{
			this.SetMenu_HideAllStdBtn();
			this.ddlWfId.Enabled = false;
			this.ddlWfpId.Enabled = false;
			this.txtTodo.Enabled = true;
			this.txtTodo.ReadOnly = true;
			this.txtOpinion.Enabled = true;
			this.txtOpinion.ReadOnly = false;
			this.btnOk.Enabled = true;
			this.btnCancel.Enabled = true;
			switch (this._WfpAction)
			{
			case WorkflowAction.S:
				this.ddlNextWfpId.Enabled = true;
				this.cblUsers.Enabled = true;
				break;
			case WorkflowAction.B:
				this.ddlNextWfpId.Enabled = false;
				this.cblUsers.Enabled = true;
				break;
			case WorkflowAction.W:
				this.ddlNextWfpId.Enabled = false;
				this.cblUsers.Enabled = false;
				break;
			case WorkflowAction.D:
				this.ddlNextWfpId.Enabled = false;
				this.cblUsers.Enabled = true;
				break;
			case WorkflowAction.V:
				this.ddlNextWfpId.Enabled = false;
				this.cblUsers.Enabled = false;
				break;
			}
			this.txtTodo.Enabled = true;
			this.txtTodo.ReadOnly = true;
		}

		protected override void OnPreLoadRecord()
		{
		}

		protected override void OnLoadRecord()
		{
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected override void OnLoadRecordComplete()
		{
			this._WfName = base.GetWfName(this._WfId);
			base.Title = string.Format("【{0}】{1}", this._WfName, base.MdName);
			this.ddlWfId.Items.Add(new ListItem(this._WfId + "|" + this._WfName, this._WfId));
			this.ddlWfpId.Items.Add(new ListItem(base.Workflow.WfpId + "|" + base.GetWfpName(this._WfId, base.Workflow.WfpId), base.Workflow.WfpId));
			this.hidRequiredTodo.Value = "N";
			this.hidRequiredTodoUsers.Value = "1";
			this.hidWfpAction.Value = this._WfpAction.ToString();
			this.hidNextWfId.Value = this._WfId;
			switch (this._WfpAction)
			{
			case WorkflowAction.S:
			{
				this.lblNextWfpId.Text = "提交事务：";
				bool flag = base.Workflow.WfpSet.Position_05 || (base.Workflow.WfpSet.Position_07 && DateTime.Now > base.Workflow.WfRunREnd);
				if (flag)
				{
					this.hidRequiredOpinion.Value = "Y";
				}
				this.SubmitHandle();
				break;
			}
			case WorkflowAction.B:
			{
				this.lblNextWfpId.Text = "退回事务：";
				bool position_ = base.Workflow.WfpSet.Position_01;
				if (position_)
				{
					this.hidRequiredOpinion.Value = "Y";
				}
				this.BackHandle();
				break;
			}
			case WorkflowAction.W:
			{
				this.lblNextWfpId.Text = "撤回事务：";
				bool position_2 = base.Workflow.WfpSet.Position_03;
				if (position_2)
				{
					this.hidRequiredOpinion.Value = "Y";
				}
				this.WithdrawHandle();
				break;
			}
			case WorkflowAction.D:
			{
				this.lblNextWfpId.Text = "转交事务：";
				bool position_3 = base.Workflow.WfpSet.Position_13;
				if (position_3)
				{
					this.hidRequiredOpinion.Value = "Y";
				}
				this.DeliverToHandle();
				break;
			}
			case WorkflowAction.V:
			{
				bool position_4 = base.Workflow.WfpSet.Position_09;
				if (position_4)
				{
					this.hidRequiredOpinion.Value = "Y";
				}
				this.lblNextWfpId.Text = "否决事务：";
				this.VetoHandle();
				break;
			}
			}
			this.hidNextWfpId.Value = this.ddlNextWfpId.SelectedValue;
		}

		private void SubmitHandle()
		{
			List<WFL> lstNextWfl = base.Workflow.GetNextWfpId(base.Workflow.WfId, base.Workflow.WfpId);
			this.ddlNextWfpId.Items.Clear();
			foreach (WFL wfl in lstNextWfl)
			{
				WFP nextWfp = WFPUtils.GetWFP(wfl.NextWfId, wfl.NextWfpId);
				bool flag = nextWfp != null;
				if (flag)
				{
					this.ddlNextWfpId.Items.Add(new ListItem(nextWfp.WfpId + "|" + nextWfp.WfpName, nextWfp.WfpId));
				}
			}
			bool flag2 = this.ddlNextWfpId.SelectedValue != "TTTT";
			if (flag2)
			{
				this.ParseWfpUsers();
			}
		}

		private void ParseWfpUsers()
		{
			List<string> lstMustUsers = new List<string>();
			List<string> lstDefaultUsers = new List<string>();
			List<string> lstHandUsers = new List<string>();
			this.cblUsers.Items.Clear();
			this.txtTodo.Text = "";
			bool flag = base.Workflow.RequiredParseUsers(base.Workflow.WfId, this.ddlNextWfpId.SelectedValue);
			if (flag)
			{
				this.hidRequiredTodoUsers.Value = base.Workflow.ParseWfpUsers(base.Workflow.WfId, this.ddlNextWfpId.SelectedValue, lstMustUsers, lstDefaultUsers, lstHandUsers).ToString();
				this.hidRequiredTodo.Value = "Y";
				foreach (string userId in lstMustUsers)
				{
					User user = UserUtils.GetUser(userId);
					bool flag2 = user != null;
					if (flag2)
					{
						this.cblUsers.Items.Add(this.BuildUserListItem(userId, user.UserName, false, true));
					}
				}
				foreach (string userId2 in lstDefaultUsers)
				{
					User user = UserUtils.GetUser(userId2);
					bool flag3 = user != null && this.cblUsers.Items.FindByValue(string.Format("{0}|{1}", userId2, user.UserName)) == null;
					if (flag3)
					{
						this.cblUsers.Items.Add(this.BuildUserListItem(userId2, user.UserName, true, true));
					}
				}
				foreach (string userId3 in lstHandUsers)
				{
					User user = UserUtils.GetUser(userId3);
					bool flag4 = user != null && this.cblUsers.Items.FindByValue(string.Format("{0}|{1}", userId3, user.UserName)) == null;
					if (flag4)
					{
						this.cblUsers.Items.Add(this.BuildUserListItem(userId3, user.UserName, true, false));
					}
				}
				bool flag5 = lstMustUsers.Count == 0 && lstDefaultUsers.Count == 0 && lstHandUsers.Count == 0 && !base.Workflow.BeWfuConfig(base.Workflow.WfId, this.ddlNextWfpId.SelectedValue);
				if (flag5)
				{
					List<User> lstAllUser = UserUtils.GetAllUser();
					foreach (User item in lstAllUser)
					{
						this.cblUsers.Items.Add(this.BuildUserListItem(item.UserId, item.UserName, true, false));
					}
				}
				foreach (ListItem item2 in this.cblUsers.Items)
				{
					bool selected = item2.Selected;
					if (selected)
					{
						TimTextBox expr_323 = this.txtTodo;
						expr_323.Text = expr_323.Text + item2.Value + ",";
					}
				}
				base.PlaceUpdateContent();
				base.PlaceUpdateTemplet();
			}
			else
			{
				this.hidRequiredTodo.Value = "N";
				base.PlaceUpdateContent();
				base.PlaceUpdateTemplet();
			}
		}

		private void BackHandle()
		{
			List<string> lstPreTodo;
			string preWfpId = base.Workflow.GetBackPreWfpAndTodo(this._WfId, this._WfRunId, out lstPreTodo);
			this.ddlNextWfpId.Items.Clear();
			this.ddlNextWfpId.Items.Add(new ListItem(preWfpId + "|" + base.GetWfpName(this._WfId, preWfpId), preWfpId));
			this.hidRequiredTodo.Value = "Y";
			foreach (string userId in lstPreTodo)
			{
				User user = UserUtils.GetUser(userId);
				bool flag = user != null && this.cblUsers.Items.FindByValue(string.Format("{0}|{1}", userId, user.UserName)) == null;
				if (flag)
				{
					this.cblUsers.Items.Add(this.BuildUserListItem(user.UserId, user.UserName, true, false));
				}
			}
		}

		private void WithdrawHandle()
		{
			this.ddlNextWfpId.Items.Clear();
			this.ddlNextWfpId.Items.Add(new ListItem(base.Workflow.WithdrawWfpId + "|" + base.GetWfpName(this._WfId, base.Workflow.WithdrawWfpId), base.Workflow.WithdrawWfpId));
		}

		private void DeliverToHandle()
		{
			this.ddlNextWfpId.Items.Clear();
			this.ddlNextWfpId.Items.Add(new ListItem(base.Workflow.WfpId + "|" + base.GetWfpName(this._WfId, base.Workflow.WfpId), base.Workflow.WfpId));
			this.cblUsers.Items.Clear();
			List<User> lstUsers = UserUtils.GetAllUser();
			foreach (User user in lstUsers)
			{
				this.cblUsers.Items.Add(this.BuildUserListItem(user.UserId, user.UserName, true, false));
			}
		}

		private void VetoHandle()
		{
			this.ddlNextWfpId.Items.Add(new ListItem("VVVV|否决", "VVVV"));
		}

		private ListItem BuildUserListItem(string userId, string userName, bool enabled, bool selected)
		{
			ListItem lstItem = new ListItem(string.Format("|{0,-10}|{1,-15}", userId, userName), string.Format("{0}|{1}", userId, userName));
			lstItem.Attributes.Add("onclick", "CColor(this);");
			lstItem.Attributes.Add("alt", userId);
			lstItem.Selected = selected;
			lstItem.Enabled = enabled;
			return lstItem;
		}

		protected void ddlNextWfpId_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.hidNextWfpId.Value = this.ddlNextWfpId.SelectedValue;
			this.ParseWfpUsers();
			base.PlaceUpdateContent();
			base.PlaceUpdateTemplet();
		}

		protected void btnOk_Click(object sender, EventArgs e)
		{
			string runscript = string.Concat(new string[]
			{
				"function SetWfArgument(){$('#SiteTemplet_Templet_Action', frameElement.dialog.options.opener.document).val('",
				this._WfpAction.ToString(),
				"');$('#SiteTemplet_Templet_NextWfId', frameElement.dialog.options.opener.document).val('",
				this.ddlWfId.SelectedValue,
				"');$('#SiteTemplet_Templet_NextWfpId', frameElement.dialog.options.opener.document).val('",
				this.ddlNextWfpId.SelectedValue,
				"');$('#SiteTemplet_Templet_Todo', frameElement.dialog.options.opener.document).val('",
				this.hidTodo.Value,
				"');$('#SiteTemplet_Templet_Opinion', frameElement.dialog.options.opener.document).val('",
				this.txtOpinion.Text.Trim().Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"),
				"');$('#SiteTemplet_btnFlowBlock', frameElement.dialog.options.opener.document).click();frameElement.dialog.close();}"
			});
			base.RegisterScript("SetArgument", runscript + "SetWfArgument();");
		}
	}
}
