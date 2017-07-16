using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_KERNEL.Helper;
using TIM.T_KERNEL.Log;
using TIM.T_TEMPLET.CommForm;
using TIM.T_TEMPLET.DFS;
using TIM.T_TEMPLET.Enum;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Utils;
using TIM.T_WEBCTRL;
using TIM.T_WORKFLOW;

namespace TIM.T_TEMPLET.Page
{
	public class PageBase : Base
	{
		private LogicSession m_lgcSession = null;

		private string m_userId = string.Empty;

		private string m_userName = string.Empty;

		protected ScriptManager CurSM;

		private EntityManager m_curEntity = new EntityManager();

		private string m_editingPage = string.Empty;

		private WorkflowEngine m_workflow = null;

		private ReportStyleParse m_reportParse;

		private TimGridView m_curGrid = null;

		private TimPagingBar m_curPagingBar = null;

		private string[] m_dataKeyNames;

		private PageParameter m_pageUrlAppendParam = new PageParameter();

		private PageParameter m_pageUrlParam = new PageParameter();

		private string m_promptMessage = string.Empty;

		private int m_amId = 0;

		private int m_mdId = 0;

		private string m_mdName = string.Empty;

		private int m_width = 900;

		private int m_height = 600;

		private UserModulePermission m_pagePermission = new UserModulePermission();

		private ViewStateManager m_pageViewStateManager = null;

		private PageParameter m_pageParam = null;

		private bool m_beExportExcel = false;

		private Dictionary<string, string> m_codeHelperAppendParam = new Dictionary<string, string>();

		internal TMasterBase MasterBase
		{
			get
			{
				bool flag = this.Page != null && this.Page.Master != null && this.Page.Master is TMasterBase;
				TMasterBase result;
				if (flag)
				{
					result = (this.Page.Master as TMasterBase);
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public bool IsPartialPostBack
		{
			get
			{
				return base.Request.Headers.GetValues("X-UtoAjax") != null;
			}
		}

		public LogicSession LgcSession
		{
			get
			{
				return this.m_lgcSession;
			}
			set
			{
				this.m_lgcSession = value;
			}
		}

		public string UserId
		{
			get
			{
				bool flag = string.IsNullOrWhiteSpace(this.m_userId);
				if (flag)
				{
					bool flag2 = this.LgcSession == null;
					if (flag2)
					{
						AppEventLog.Debug("lgcsession is null");
					}
					else
					{
						AppEventLog.Debug("lgcsession=" + this.LgcSession.UserId);
					}
				}
				return this.m_userId;
			}
			set
			{
				this.m_userId = value;
			}
		}

		public string UserName
		{
			get
			{
				return this.m_userName;
			}
			set
			{
				this.m_userName = value;
			}
		}

		public EntityManager CurEntity
		{
			get
			{
				return this.m_curEntity;
			}
			set
			{
				this.m_curEntity = value;
			}
		}

		public string EditingPage
		{
			get
			{
				return this.m_editingPage;
			}
			set
			{
				this.m_editingPage = value;
			}
		}

		public WorkflowEngine Workflow
		{
			get
			{
				return this.m_workflow;
			}
			set
			{
				this.m_workflow = value;
			}
		}

		internal string WfIdField
		{
			get
			{
				return this.CurEntity.Entity.Table + "_WFID";
			}
		}

		internal string WfRunIdField
		{
			get
			{
				return this.CurEntity.Entity.Table + "_WFRUNID";
			}
		}

		public ReportStyleParse ReportParse
		{
			get
			{
				return this.m_reportParse;
			}
			set
			{
				this.m_reportParse = value;
			}
		}

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

		protected TimPagingBar CurPagingBar
		{
			get
			{
				return this.m_curPagingBar;
			}
			set
			{
				this.m_curPagingBar = value;
			}
		}

		public string[] DataKeyNames
		{
			get
			{
				return this.m_dataKeyNames;
			}
			set
			{
				this.m_dataKeyNames = value;
				List<string> keyList = this.m_dataKeyNames.ToList<string>();
				bool flag = !keyList.Contains("MODIFIEDTIME");
				if (flag)
				{
					keyList.Insert(0, "MODIFIEDTIME");
				}
				bool flag2 = !keyList.Contains("MODIFIER");
				if (flag2)
				{
					keyList.Insert(0, "MODIFIER");
				}
				bool flag3 = !keyList.Contains("MODIFIERID");
				if (flag3)
				{
					keyList.Insert(0, "MODIFIERID");
				}
				bool workflow = this.CurEntity.Entity.Workflow;
				if (workflow)
				{
					bool flag4 = !keyList.Contains(this.CurEntity.Entity.Table + "_WFID");
					if (flag4)
					{
						keyList.Insert(0, this.CurEntity.Entity.Table + "_WFID");
					}
					bool flag5 = !keyList.Contains(this.CurEntity.Entity.Table + "_WFRUNID");
					if (flag5)
					{
						keyList.Insert(0, this.CurEntity.Entity.Table + "_WFRUNID");
					}
				}
				this.m_dataKeyNames = keyList.ToArray();
				this.CurGrid.DataKeyNames = this.m_dataKeyNames;
			}
		}

		public PageParameter PageUrlAppendParam
		{
			get
			{
				return this.m_pageUrlAppendParam;
			}
			set
			{
				this.m_pageUrlAppendParam = value;
			}
		}

		public PageParameter PageUrlParam
		{
			get
			{
				return this.m_pageUrlParam;
			}
			set
			{
				this.m_pageUrlParam = value;
			}
		}

		private string PromptMessage
		{
			get
			{
				return this.m_promptMessage;
			}
		}

		public int AmId
		{
			get
			{
				return this.m_amId;
			}
			set
			{
				this.m_amId = value;
			}
		}

		public int MdId
		{
			get
			{
				return this.m_mdId;
			}
			set
			{
				this.m_mdId = value;
			}
		}

		public string MdName
		{
			get
			{
				return this.m_mdName;
			}
			set
			{
				this.m_mdName = value;
			}
		}

		public int Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		public int Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		internal string PageAmIdPlusClassName
		{
			get
			{
				return this.AmId.ToString() + this.Page.GetType().BaseType.Name.ToString().ToUpper();
			}
		}

		public UserModulePermission PagePermission
		{
			get
			{
				return this.m_pagePermission;
			}
			set
			{
				this.m_pagePermission = value;
			}
		}

		public ViewStateManager PageViewStateManager
		{
			get
			{
				return this.m_pageViewStateManager;
			}
			set
			{
				this.m_pageViewStateManager = value;
			}
		}

		public PageState PageOperState
		{
			get
			{
				return (PageState)this.PageViewStateManager["PageOperState"];
			}
			set
			{
				this.PageViewStateManager["PageOperState"] = value;
			}
		}

		public string ModifierId
		{
			get
			{
				object obj = this.PageViewStateManager["ModifierId"];
				bool flag = obj == null;
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = obj.ToString();
				}
				return result;
			}
			set
			{
				this.PageViewStateManager["ModifierId"] = value;
			}
		}

		public string WorkflowId
		{
			get
			{
				object obj = this.PageViewStateManager["WorkflowId"];
				bool flag = obj == null;
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = obj.ToString();
				}
				return result;
			}
			set
			{
				this.PageViewStateManager["WorkflowId"] = value;
			}
		}

		public int WorkflowRunId
		{
			get
			{
				object obj = this.PageViewStateManager["WorkflowRunId"];
				bool flag = obj == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = obj.ToString().ToInt();
				}
				return result;
			}
			set
			{
				this.PageViewStateManager["WorkflowRunId"] = value;
			}
		}

		public PageParameter PageParam
		{
			get
			{
				return this.m_pageParam;
			}
			set
			{
				this.m_pageParam = value;
			}
		}

		protected bool BeExportExcel
		{
			get
			{
				return this.m_beExportExcel;
			}
			set
			{
				this.m_beExportExcel = value;
			}
		}

		protected virtual void InitModuleInfo()
		{
		}

		protected virtual void InitEntity()
		{
		}

		protected virtual void InitTemplet()
		{
		}

		protected virtual void OnInitTree()
		{
		}

		protected virtual bool RebuildTree()
		{
			return false;
		}

		protected virtual void InitCtrlValue()
		{
		}

		protected virtual void SetPageUrlAppendParam()
		{
		}

		protected virtual void SetMenu_HideAllStdBtn()
		{
		}

		protected virtual void SetMenu_OnlyQuery()
		{
		}

		protected virtual void SetMenu_OnlyQueryAndReport()
		{
		}

		protected virtual void SetMenu_OnlyAttach()
		{
		}

		protected virtual void SetMenu_OnlyViewEdit()
		{
		}

		protected virtual void OnPreInit()
		{
		}

		protected sealed override void OnPreInit(EventArgs e)
		{
			this.OnPreInit();
			this.LgcSession = LogicContext.GetLogicSession();
			base.OnPreInit(e);
			bool flag = this.LgcSession == null || string.IsNullOrWhiteSpace(this.LgcSession.UserId);
			if (flag)
			{
				FormsAuthentication.SignOut();
			}
			this.UserId = LogicContext.Current.UserId;
			this.UserName = LogicContext.Current.UserName;
		}

		protected virtual void OnInit()
		{
		}

		internal bool InitLPWorkflowRI()
		{
			bool result = false;
			bool flag = this.CurGrid.SelectedIndex >= 0;
			if (flag)
			{
				this.CurEntity.WorkflowId = (this.WorkflowId = this.CurGrid.SelectedDataKey[this.WfIdField].ToString().Trim());
				this.CurEntity.WorkflowRunId = (this.WorkflowRunId = this.CurGrid.SelectedDataKey[this.WfRunIdField].ToString().Trim().ToInt());
				this.Workflow.Init(this.WorkflowId, this.WorkflowRunId);
				result = true;
			}
			return result;
		}

		internal bool InitEPWorkflowRI()
		{
			this.CurEntity.WorkflowId = this.WorkflowId;
			this.CurEntity.WorkflowRunId = this.WorkflowRunId;
			this.Workflow.Init(this.WorkflowId, this.WorkflowRunId);
			return true;
		}

		protected string GetUserName(string userId)
		{
			string result = userId;
			User user = UserUtils.GetUser(userId);
			bool flag = user != null;
			if (flag)
			{
				result = user.UserName;
			}
			return result;
		}

		protected string GetWfbName(string wfbId)
		{
			string result = string.Empty;
			return WorkflowEngine.GetWfbName(wfbId);
		}

		protected string GetWfName(string wfId)
		{
			string result = string.Empty;
			return WorkflowEngine.GetWfName(wfId);
		}

		protected string GetWfpName(string wfId, string wfpId)
		{
			string result = string.Empty;
			return WorkflowEngine.GetWfpName(wfId, wfpId);
		}

		protected string GetWfpExSetting(string settingName)
		{
			string result = string.Empty;
			return WorkflowEngine.GetWfpExSetting(this.Workflow.WfId, this.Workflow.WfpId, settingName);
		}

		protected string GetWfpExSetting(string wfId, string wfpId, string settingName)
		{
			string result = string.Empty;
			return WorkflowEngine.GetWfpExSetting(wfId, wfpId, settingName);
		}

		protected string GetWfUsersName(string userList)
		{
			string result = string.Empty;
			return WorkflowEngine.GetUsersName(userList);
		}

		protected string GetParameterValue(string pmId)
		{
			string result = string.Empty;
			LogicContext lgcContext = LogicContext.Current;
			bool flag = lgcContext != null;
			if (flag)
			{
				result = PmConfigUtils.GetParamValue(lgcContext.AmId, pmId);
			}
			else
			{
				result = PmConfigUtils.GetParamValue(0, pmId);
			}
			return result;
		}

		public string GetWfpActionDesc(string wfpAction)
		{
			string result = string.Empty;
			return WorkflowEngine.GetWfpActionDesc(wfpAction);
		}

		private void BuildWorkflowEngineEvent()
		{
			this.Workflow.OnParseParameter += new WorkflowEngine.ParseParameter(this.Workflow_OnParseParameterTemplet);
			this.Workflow.OnPreAction += new WorkflowEngine.PreAction(this.Workflow_OnPreActionTemplet);
			this.Workflow.OnActionComplete += new WorkflowEngine.ActionComplete(this.Workflow_OnActionCompleteTemplet);
		}

		public virtual bool Workflow_OnParseParameter(string pmName, out string pmValue)
		{
			pmValue = string.Empty;
			return false;
		}

		private bool Workflow_OnParseParameterTemplet(string pmName, out string pmValue)
		{
			bool result = this.CurEntity.Workflow_OnParseParameter(pmName, out pmValue);
			bool flag = !result;
			if (flag)
			{
				result = this.Workflow_OnParseParameter(pmName, out pmValue);
			}
			return result;
		}

		public virtual bool Workflow_OnPreAction(FlowActionParameter actionParameter)
		{
			return true;
		}

		private bool Workflow_OnPreActionTemplet(FlowActionParameter actionParameter)
		{
			bool result = this.CurEntity.Workflow_OnPreAction(actionParameter);
			bool flag = result;
			if (flag)
			{
				result = this.Workflow_OnPreAction(actionParameter);
			}
			return result;
		}

		public virtual bool Workflow_OnActionComplete(FlowActionParameter actionParameter)
		{
			return true;
		}

		private bool Workflow_OnActionCompleteTemplet(FlowActionParameter actionParameter)
		{
			bool result = this.CurEntity.Workflow_OnActionComplete(actionParameter);
			bool flag = result;
			if (flag)
			{
				result = this.Workflow_OnActionComplete(actionParameter);
			}
			bool flag2 = result;
			if (flag2)
			{
				Database db = LogicContext.GetDatabase();
				HSQL hsql = new HSQL(db);
				hsql.Add("UPDATE " + this.CurEntity.Entity.Table + " SET ");
				hsql.Add(string.Format(" {0} = '{1}'", this.CurEntity.Entity.Table + "_WFID", actionParameter.NextWfId));
				hsql.Add(string.Format(",{0} =  {1} ", this.CurEntity.Entity.Table + "_WFRUNID", actionParameter.WfRunId));
				hsql.Add(string.Format(",{0} = '{1}'", this.CurEntity.Entity.Table + "_WFPID", actionParameter.NextWfpId));
				hsql.Add(string.Format(",{0} = '{1}'", this.CurEntity.Entity.Table + "_WFTODO", actionParameter.Todo.ToWfUserString()));
				hsql.Add(string.Format(",{0} = '{1}'", this.CurEntity.Entity.Table + "_WFDONE", actionParameter.Done.ToWfUserString()));
				hsql.Add(string.Format(",{0} = '{1}'", this.CurEntity.Entity.Table + "_WFPACTDESC", actionParameter.Action.ToChsDesc()));
				hsql.Add("WHERE ");
				hsql.Add(string.Format("{0} = '{1}'", this.CurEntity.Entity.Table + "_WFID", actionParameter.WfId));
				hsql.Add("AND");
				hsql.Add(string.Format("{0} =  {1} ", this.CurEntity.Entity.Table + "_WFRUNID", actionParameter.WfRunId));
				int affectedRows = db.ExecSQL(hsql);
				bool flag3 = affectedRows <= 0;
				if (flag3)
				{
					result = false;
				}
			}
			return result;
		}

		private void SetNullFromWorkflowFieldRight(string wfId, string wfpId)
		{
			List<WFFR> lstWffr = WFFRUtils.GetWFFR(wfId, wfpId);
			foreach (WFFR wffr in lstWffr)
			{
				bool ctrlNull = wffr.CtrlNull;
				if (!ctrlNull)
				{
					foreach (KeyValuePair<string, FieldMapAttribute> item in this.CurEntity.Fields)
					{
						FieldMapAttribute fieldMap = item.Value;
						bool flag = wffr.ControlId.Equals(fieldMap.CtrlId);
						if (flag)
						{
							fieldMap.Null = false;
							break;
						}
					}
				}
			}
		}

		private bool BeNullFromWorkflowFieldRight(string wfId, string wfpId)
		{
			List<WFFR> lstWffr = WFFRUtils.GetWFFR(wfId, wfpId);
			bool result;
			foreach (WFFR wffr in lstWffr)
			{
				bool flag = !wffr.CtrlNull;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		internal void OnDirectSubmitIn(PageClassification pageClassification)
		{
			bool flag = pageClassification == PageClassification.Listing;
			if (flag)
			{
				bool flag2 = !this.InitLPWorkflowRI();
				if (flag2)
				{
					return;
				}
				bool flag3 = this.BeNullFromWorkflowFieldRight(this.Workflow.WfId, this.Workflow.WfpId);
				if (flag3)
				{
					this.PromptDialog("该业务流程当前步骤存在域权限不允许为空配置，请打开维护页面后再提交工作流！");
					return;
				}
			}
			else
			{
				bool flag4 = pageClassification == PageClassification.Editing;
				if (flag4)
				{
					bool flag5 = !this.InitEPWorkflowRI();
					if (flag5)
					{
						return;
					}
					this.SetNullFromWorkflowFieldRight(this.Workflow.WfId, this.Workflow.WfpId);
				}
			}
			bool flag6 = this.CanSubmitTemplet() && (pageClassification != PageClassification.Editing || this.VerifyNull());
			if (flag6)
			{
				FlowActionParameter actionParameter;
				bool flag7 = this.Workflow.IsDirectSubmit(out actionParameter);
				if (flag7)
				{
					Database db = LogicContext.GetDatabase();
					db.BeginTrans();
					try
					{
						bool flag8 = this.Workflow.Submit(actionParameter);
						if (flag8)
						{
							db.CommitTrans();
							bool flag9 = pageClassification == PageClassification.Listing;
							if (flag9)
							{
								string tips = string.Empty;
								bool flag10 = actionParameter.Action == WorkflowAction.S && actionParameter.ExecutedState == WfRunStateType.T;
								if (flag10)
								{
									tips = string.Format("<table><tr><td>流程提交成功！</td></tr><tr><td>下一步骤:{0}</td></tr></table>", this.GetWfpName(actionParameter.NextWfId, actionParameter.NextWfpId));
								}
								else
								{
									bool flag11 = actionParameter.Action == WorkflowAction.S;
									if (flag11)
									{
										tips = string.Format("<table><tr><td>流程提交成功！</td></tr><tr><td>下一步骤:{0}</td></tr><tr><td>待处理人:{1}</td></tr></table>", this.GetWfpName(actionParameter.NextWfId, actionParameter.NextWfpId), this.GetWfUsersName(actionParameter.Todo.ToWfUserString()));
									}
									else
									{
										bool flag12 = actionParameter.Action == WorkflowAction.I;
										if (flag12)
										{
											tips = string.Format("<table><tr><td>流程提交成功！</td></tr><tr><td>等待其他人会签！</td></tr><tr><td>待会签人:{0}</td></tr></table>", this.GetWfUsersName(actionParameter.Todo.ToWfUserString()));
										}
									}
								}
								this.RegisterScript("workflowInfo", "showWorkflowInfo('" + tips + "');");
							}
							else
							{
								bool flag13 = pageClassification == PageClassification.Editing;
								if (flag13)
								{
									this.CloseDialog();
								}
							}
						}
						else
						{
							db.RollbackTrans();
						}
					}
					catch (Exception ex)
					{
						db.RollbackTrans();
						this.PromptPreDialog(string.Format("事务提交操作失败！ 提示信息：\r {0}", ex.Message.Replace("\r\n", "\\r").Replace("\n", "\\r")));
					}
				}
				else
				{
					this.OpenWorkflowHandleDialog(WorkflowAction.S);
				}
			}
			else
			{
				this.PromptPreDialog("当前事务您没有流程提交权限！");
			}
		}

		internal void OnSubmitIn(PageClassification pageClassification)
		{
			bool flag = pageClassification == PageClassification.Listing;
			if (flag)
			{
				bool flag2 = !this.InitLPWorkflowRI();
				if (flag2)
				{
					return;
				}
				bool flag3 = this.BeNullFromWorkflowFieldRight(this.Workflow.WfId, this.Workflow.WfpId);
				if (flag3)
				{
					this.PromptDialog("该业务流程当前步骤存在域权限不允许为空配置，请打开维护页面后再提交工作流！");
					return;
				}
			}
			else
			{
				bool flag4 = pageClassification == PageClassification.Editing;
				if (flag4)
				{
					bool flag5 = !this.InitEPWorkflowRI();
					if (flag5)
					{
						return;
					}
					this.SetNullFromWorkflowFieldRight(this.Workflow.WfId, this.Workflow.WfpId);
				}
			}
			bool flag6 = this.CanSubmitTemplet() && (pageClassification != PageClassification.Editing || this.VerifyNull());
			if (flag6)
			{
				this.OpenWorkflowHandleDialog(WorkflowAction.S);
			}
			else
			{
				this.PromptPreDialog("当前事务您没有流程提交权限！");
			}
		}

		internal void OnWithdrawIn(PageClassification pageClassification)
		{
			bool flag = pageClassification == PageClassification.Listing;
			if (flag)
			{
				bool flag2 = !this.InitLPWorkflowRI();
				if (flag2)
				{
					return;
				}
			}
			else
			{
				bool flag3 = pageClassification == PageClassification.Editing;
				if (flag3)
				{
					bool flag4 = !this.InitEPWorkflowRI();
					if (flag4)
					{
						return;
					}
				}
			}
			bool flag5 = this.CanWithdrawTemplet();
			if (flag5)
			{
				FlowActionParameter actionParameter;
				bool flag6 = this.Workflow.IsDirectWithdraw(out actionParameter);
				if (flag6)
				{
					Database db = LogicContext.GetDatabase();
					db.BeginTrans();
					try
					{
						bool flag7 = this.Workflow.Withdraw(actionParameter);
						if (flag7)
						{
							db.CommitTrans();
							bool flag8 = pageClassification == PageClassification.Editing;
							if (flag8)
							{
								this.CloseDialog();
							}
						}
						else
						{
							db.RollbackTrans();
						}
					}
					catch (Exception ex)
					{
						db.RollbackTrans();
						this.PromptPreDialog(string.Format("事务撤回操作失败！ 提示信息：\r {0}", ex.Message.Replace("\r\n", "\\r").Replace("\n", "\\r")));
					}
				}
				else
				{
					this.OpenWorkflowHandleDialog(WorkflowAction.W);
				}
			}
			else
			{
				this.PromptPreDialog("当前事务您没有流程撤回权限！");
			}
		}

		internal void OnBackIn(PageClassification pageClassification)
		{
			bool flag = pageClassification == PageClassification.Listing;
			if (flag)
			{
				bool flag2 = !this.InitLPWorkflowRI();
				if (flag2)
				{
					return;
				}
			}
			else
			{
				bool flag3 = pageClassification == PageClassification.Editing;
				if (flag3)
				{
					bool flag4 = !this.InitEPWorkflowRI();
					if (flag4)
					{
						return;
					}
				}
			}
			this.OpenWorkflowHandleDialog(WorkflowAction.B);
		}

		internal void OnDeliverToIn(PageClassification pageClassification)
		{
			bool flag = pageClassification == PageClassification.Listing;
			if (flag)
			{
				bool flag2 = !this.InitLPWorkflowRI();
				if (flag2)
				{
					return;
				}
			}
			else
			{
				bool flag3 = pageClassification == PageClassification.Editing;
				if (flag3)
				{
					bool flag4 = !this.InitEPWorkflowRI();
					if (flag4)
					{
						return;
					}
				}
			}
			this.OpenWorkflowHandleDialog(WorkflowAction.D);
		}

		internal void OnVetoIn(PageClassification pageClassification)
		{
			bool flag = pageClassification == PageClassification.Listing;
			if (flag)
			{
				bool flag2 = !this.InitLPWorkflowRI();
				if (flag2)
				{
					return;
				}
			}
			else
			{
				bool flag3 = pageClassification == PageClassification.Editing;
				if (flag3)
				{
					bool flag4 = !this.InitEPWorkflowRI();
					if (flag4)
					{
						return;
					}
				}
			}
			FlowActionParameter actionParameter;
			bool flag5 = this.Workflow.IsDirectVeto(out actionParameter);
			if (flag5)
			{
				Database db = LogicContext.GetDatabase();
				db.BeginTrans();
				try
				{
					bool flag6 = this.Workflow.Veto(actionParameter);
					if (flag6)
					{
						db.CommitTrans();
						bool flag7 = pageClassification == PageClassification.Editing;
						if (flag7)
						{
							this.CloseDialog();
						}
					}
					else
					{
						db.RollbackTrans();
					}
				}
				catch (Exception ex)
				{
					db.RollbackTrans();
					this.PromptPreDialog(string.Format("事务否决操作失败！ 提示信息：\r {0}", ex.Message.Replace("\r\n", "\\r").Replace("\n", "\\r")));
				}
			}
			else
			{
				this.OpenWorkflowHandleDialog(WorkflowAction.V);
			}
		}

		internal void OnWorkflowTraceIn(PageClassification pageClassification)
		{
			bool flag = pageClassification == PageClassification.Listing;
			if (flag)
			{
				bool flag2 = !this.InitLPWorkflowRI();
				if (flag2)
				{
					return;
				}
			}
			else
			{
				bool flag3 = pageClassification == PageClassification.Editing;
				if (flag3)
				{
					bool flag4 = !this.InitEPWorkflowRI();
					if (flag4)
					{
						return;
					}
				}
			}
			this.OpenWorkflowHandleDialog(WorkflowAction.T);
		}

		internal void OnFlowBlockIn(PageClassification pageClassification, string workflowAction, string nextWfId, string nextWfpId, string todo, string opinion)
		{
			bool flag = pageClassification == PageClassification.Listing;
			if (flag)
			{
				bool flag2 = !this.InitLPWorkflowRI();
				if (flag2)
				{
					return;
				}
			}
			else
			{
				bool flag3 = pageClassification == PageClassification.Editing;
				if (flag3)
				{
					bool flag4 = !this.InitEPWorkflowRI();
					if (flag4)
					{
						return;
					}
				}
			}
			FlowActionParameter actionParameter = default(FlowActionParameter);
			actionParameter.Action = workflowAction.ToWorkflowAction();
			actionParameter.NextWfId = nextWfId;
			actionParameter.NextWfpId = nextWfpId;
			actionParameter.Todo = todo.ToWfUserList();
			actionParameter.Opinion = opinion;
			Database db = LogicContext.GetDatabase();
			db.BeginTrans();
			try
			{
				bool result = false;
				switch (actionParameter.Action)
				{
				case WorkflowAction.S:
					this.Workflow.BuildSubmitFlowActionParameter(ref actionParameter);
					result = this.Workflow.Submit(actionParameter);
					break;
				case WorkflowAction.B:
					this.Workflow.BuildBackFlowActionParameter(ref actionParameter);
					result = this.Workflow.Back(actionParameter);
					break;
				case WorkflowAction.W:
					this.Workflow.BuildWithdrawFlowActionParameter(ref actionParameter);
					result = this.Workflow.Withdraw(actionParameter);
					break;
				case WorkflowAction.D:
					this.Workflow.BuildDeliverToFlowActionParameter(ref actionParameter);
					result = this.Workflow.DeliverTo(actionParameter);
					break;
				case WorkflowAction.V:
					this.Workflow.BuildVetoFlowActionParameter(ref actionParameter);
					result = this.Workflow.Veto(actionParameter);
					break;
				}
				bool flag5 = result;
				if (flag5)
				{
					db.CommitTrans();
					bool flag6 = pageClassification == PageClassification.Editing;
					if (flag6)
					{
						this.CloseDialog();
					}
				}
				else
				{
					db.RollbackTrans();
				}
			}
			catch (Exception ex)
			{
				db.RollbackTrans();
				this.PromptPreDialog(string.Format("事务撤回操作失败！ 提示信息：\r {0}", ex.Message.Replace("\r\n", "\\r").Replace("\n", "\\r")));
			}
		}

		internal void OnWorkflowButtonDropDownIn(PageClassification pageClassification)
		{
			TMasterBase CurMaster = (TMasterBase)base.Master;
			bool flag = this.CurGrid.SelectedIndex < 0;
			if (flag)
			{
				CurMaster.SubmitPermission = false;
				CurMaster.BackPermission = false;
				CurMaster.WithdrawPermission = false;
				CurMaster.DeliverToPermission = false;
				CurMaster.VetoPermission = false;
				CurMaster.TracePermission = false;
			}
			else
			{
				bool flag2 = pageClassification == PageClassification.Listing;
				if (flag2)
				{
					bool flag3 = !this.InitLPWorkflowRI();
					if (flag3)
					{
						return;
					}
				}
				else
				{
					bool flag4 = pageClassification == PageClassification.Editing;
					if (flag4)
					{
						bool flag5 = !this.InitEPWorkflowRI();
						if (flag5)
						{
							return;
						}
					}
				}
				this.RecoverData();
				CurMaster.SubmitPermission = this.Workflow.GetActionPermission(WorkflowAction.S);
				CurMaster.BackPermission = this.Workflow.GetActionPermission(WorkflowAction.B);
				CurMaster.WithdrawPermission = this.Workflow.GetActionPermission(WorkflowAction.W);
				CurMaster.DeliverToPermission = this.Workflow.GetActionPermission(WorkflowAction.D);
				CurMaster.VetoPermission = this.Workflow.GetActionPermission(WorkflowAction.V);
				CurMaster.TracePermission = this.Workflow.GetActionPermission(WorkflowAction.T);
			}
			CurMaster.DropDownPermission = true;
			CurMaster.SetWorkflowButtonPermission();
		}

		protected sealed override void OnInit(EventArgs e)
		{
			this.CurSM = ScriptManager.GetCurrent(this.Page);
			this.InitModuleInfo();
			this.InitTemplet();
			this.Page.Title = this.MdName;
			bool workflow = this.CurEntity.Entity.Workflow;
			if (workflow)
			{
				this.Workflow = new WorkflowEngine();
				this.BuildWorkflowEngineEvent();
			}
			this.ReportParse = new ReportStyleParse();
			this.ReportParse.Title = this.MdName;
			this.ReportParse.OnReportUnknownFunction += new ReportStyleParse.TOnReportUnknownFunction(this.ReportParse_OnReportUnknownFunction);
			this.ReportParse.OnReportUnknownVariable += new ReportStyleParse.TOnReportUnknownVariable(this.ReportParse_OnReportUnknownVariable);
			this.OnInit();
			base.OnInit(e);
			bool flag = this.CurSM != null;
			if (flag)
			{
				this.CurSM.AsyncPostBackTimeout = 3600;
				this.CurSM.Scripts.Add(new ScriptReference("TIM.T_TEMPLET.Scripts.Templet.js", "T_TEMPLET"));
				this.CurSM.Scripts.Add(new ScriptReference("TIM.T_TEMPLET.Scripts.CodeHelper.js", "T_TEMPLET"));
			}
		}

		private void ReportParse_OnReportUnknownVariable(string Name, ref string ValueType, ref string Value)
		{
		}

		private void ReportParse_OnReportUnknownFunction(string Name, string Params, ref string ValueType, ref string Value)
		{
		}

		private void InitReprtStyle()
		{
			bool flag = this.MasterBase == null || this.MasterBase.TempletPrintButton == null || this.MasterBase.TempletPreviewButton == null || !this.MasterBase.TempletPrintButton.Visible || !this.MasterBase.TempletPrintButton.Enabled;
			if (!flag)
			{
				Database db = LogicContext.GetDatabase();
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add("SELECT REPORTSTYLE_STYLEID,REPORTSTYLE_STYLENAME");
				hsql.Add(",REPORTSTYLE_ORDER,REPORTSTYLE_DEFAULT,REPORTSTYLE_PUBLIC");
				hsql.Add(",REPORTSTYLE_EXECON,REPORTSTYLE_VERSION");
				hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
				hsql.Add("FROM REPORTSTYLE");
				hsql.Add("WHERE REPORTSTYLE_STYLEID = :REPORTSTYLE_STYLEID");
				hsql.Add("ORDER BY REPORTSTYLE_DEFAULT DESC,REPORTSTYLE_ORDER ASC");
				hsql.ParamByName("REPORTSTYLE_STYLEID").Value = this.PageAmIdPlusClassName;
				DataSet ds = db.OpenDataSet(hsql);
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					string styleId = ds.Tables[0].Rows[i]["REPORTSTYLE_STYLEID"].ToString().Trim();
					string styleOrder = ds.Tables[0].Rows[i]["REPORTSTYLE_ORDER"].ToString().Trim();
					string styleName = ds.Tables[0].Rows[i]["REPORTSTYLE_STYLENAME"].ToString().Trim();
					bool flag2 = ds.Tables[0].Rows[i]["REPORTSTYLE_DEFAULT"].ToString().Trim() == "Y";
					if (flag2)
					{
						this.MasterBase.TempletPrintButton.Items.Add(new TimMenuItem(styleName + " 【缺省】", styleOrder));
					}
					else
					{
						this.MasterBase.TempletPrintButton.Items.Add(new TimMenuItem(styleName, styleOrder));
					}
					bool flag3 = ds.Tables[0].Rows[i]["REPORTSTYLE_DEFAULT"].ToString().Trim() == "Y";
					if (flag3)
					{
						this.MasterBase.TempletPreviewButton.Items.Add(new TimMenuItem(styleName + " 【缺省】", styleOrder));
					}
					else
					{
						this.MasterBase.TempletPreviewButton.Items.Add(new TimMenuItem(styleName, styleOrder));
					}
				}
			}
		}

		private string GetReportStyle(string styleId, string styleOrder)
		{
			Database db = LogicContext.GetDatabase();
			string result;
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add("SELECT REPORTSTYLE_STYLE FROM REPORTSTYLE");
				hsql.Add("WHERE REPORTSTYLE_STYLEID = :REPORTSTYLE_STYLEID");
				hsql.Add("AND REPORTSTYLE_ORDER = :REPORTSTYLE_ORDER");
				hsql.ParamByName("REPORTSTYLE_STYLEID").Value = styleId;
				hsql.ParamByName("REPORTSTYLE_ORDER").Value = styleOrder;
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["REPORTSTYLE_STYLE"].ToString() != "";
				if (flag)
				{
					byte[] bybuf = new byte[0];
					bybuf = (byte[])ds.Tables[0].Rows[0]["REPORTSTYLE_STYLE"];
					result = Encoding.Default.GetString(bybuf, 0, bybuf.Length);
				}
				else
				{
					result = "";
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private void CurSM_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
		{
			bool flag = (this.CurEntity != null && !string.IsNullOrWhiteSpace(this.CurEntity.PromptMessage)) || !string.IsNullOrWhiteSpace(this.PromptMessage);
			if (flag)
			{
				this.CurSM.AsyncPostBackErrorMessage = ((this.CurEntity == null) ? "" : this.CurEntity.PromptMessage) + "\r" + this.PromptMessage;
			}
			else
			{
				this.CurSM.AsyncPostBackErrorMessage = e.Exception.Message + "\r详细信息：\r" + e.Exception.StackTrace;
			}
			HttpContext.Current.ClearError();
			HttpContext.Current.Server.ClearError();
		}

		protected virtual void OnInitComplete()
		{
		}

		protected sealed override void OnInitComplete(EventArgs e)
		{
			base.OnInitComplete(e);
			this.OnInitComplete();
		}

		protected virtual void OnPreLoad()
		{
		}

		protected sealed override void OnPreLoad(EventArgs e)
		{
			this.PageParam = new PageParameter();
			this.PageParam.ReadPageParameter(base.Request.QueryString, !base.IsPostBack, this.ViewState);
			bool flag = string.IsNullOrWhiteSpace(this.PageParam.GetString("EXAMID"));
			if (flag)
			{
				this.AmId = this.MdId;
			}
			else
			{
				this.AmId = this.PageParam.GetString("EXAMID").ToInt();
			}
			bool flag2 = this.CurSM != null;
			if (flag2)
			{
				this.CurSM.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(this.CurSM_AsyncPostBackError);
				bool isInAsyncPostBack = this.CurSM.IsInAsyncPostBack;
				if (isInAsyncPostBack)
				{
					bool flag3 = !string.IsNullOrWhiteSpace(base.Request.Form["__LASTFOCUS"]);
					if (flag3)
					{
						this.Set2Focus(base.Request.Form["__LASTFOCUS"].ToString());
					}
				}
			}
			this.PageViewStateManager = new ViewStateManager(this.ViewState);
			this.PagePermission = PermissionUtils.GetUserModulePermission(this.UserId, this.AmId);
			bool flag4 = !base.IsPostBack;
			if (flag4)
			{
				this.PageOperState = this.PageParam.GetString("SK").ToPageState();
				bool workflow = this.CurEntity.Entity.Workflow;
				if (workflow)
				{
					this.WorkflowId = this.PageParam.GetString("WFID");
					this.WorkflowRunId = this.PageParam.GetString("WFRUNID").ToInt();
				}
				this.InitCtrlValue();
			}
			this.OnPreLoad();
			bool flag5 = !base.IsPostBack;
			if (flag5)
			{
				this.InitReprtStyle();
			}
			this.SwitchPagePermission();
			base.OnPreLoad(e);
		}

		public void Set2Focus(Control ctl)
		{
			bool flag = ctl != null;
			if (flag)
			{
				this.Set2Focus(ctl.ClientID);
			}
		}

		public void Set2Focus(string clientID)
		{
			bool flag = this.CurSM != null;
			if (flag)
			{
				ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), Guid.NewGuid().ToString("N"), string.Concat(new string[]
				{
					"window.setTimeout('doFocus()', 1);function doFocus() {try {if (document.all.",
					clientID,
					")  document.all.",
					clientID,
					".focus();} catch(e){}}"
				}), true);
			}
		}

		private void SetCtrlClickLastFocus(Control pageCtrl)
		{
			bool flag = pageCtrl is TimTextBox;
			if (flag)
			{
				(pageCtrl as WebControl).Attributes.Add("onfocus", "scclf(this.id);");
			}
			else
			{
				bool flag2 = pageCtrl is TimNumericTextBox;
				if (flag2)
				{
					(pageCtrl as WebControl).Attributes.Add("onactivate", "scclf(this.id);");
				}
			}
			bool flag3 = pageCtrl.HasControls();
			if (flag3)
			{
				foreach (Control CurrentChildControl in pageCtrl.Controls)
				{
					this.SetCtrlClickLastFocus(CurrentChildControl);
				}
			}
		}

		protected virtual void OnPreLoadRecord()
		{
		}

		protected virtual void OnLoadRecord()
		{
		}

		protected virtual void OnLoadRecordComplete()
		{
		}

		protected virtual void OnLoad()
		{
		}

		protected sealed override void OnLoad(EventArgs e)
		{
			bool isPartialPostBack = this.IsPartialPostBack;
			if (!isPartialPostBack)
			{
				bool flag = (this.MdId < 102010001 || this.MdId > 102019999) && SystemInfoUtils.GetSystemInfo().LimitedDate < DateTime.Today;
				if (flag)
				{
					throw new Exception("系统未注册！");
				}
				base.OnLoad(e);
				this.OnLoad();
				this.SetCtrlClickLastFocus(this.Page);
				bool flag2 = this.MasterBase != null;
				if (flag2)
				{
					this.AppendCodeHelperParam();
					this.MasterBase._Maintenance.TempletCodeHelperParam.Value = this.GetCodeHelperAppendParam();
				}
				this.Page.Header.Attributes.Add("DialogWidth", this.Width.ToString());
				this.Page.Header.Attributes.Add("DialogHeight", this.Height.ToString());
				this.RegisterScript("DialogSize", string.Concat(new string[]
				{
					"var DialogWidth = ",
					this.Width.ToString(),
					"; var DialogHeight =",
					this.Height.ToString(),
					";"
				}));
			}
		}

		protected virtual void OnLoadComplete()
		{
		}

		protected override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);
		}

		protected virtual void OnPreRender()
		{
		}

		protected sealed override void OnPreRender(EventArgs e)
		{
			bool isInAsyncPostBack = this.CurSM.IsInAsyncPostBack;
			if (isInAsyncPostBack)
			{
				bool flag = this.CurEntity != null && !string.IsNullOrWhiteSpace(this.CurEntity.PromptMessage);
				if (flag)
				{
					this.CurSM.RegisterDataItem(this, this.CurEntity.PromptMessage);
				}
				else
				{
					bool flag2 = !string.IsNullOrWhiteSpace(this.PromptMessage);
					if (flag2)
					{
						this.CurSM.RegisterDataItem(this, this.PromptMessage);
					}
				}
			}
			else
			{
				bool flag3 = this.CurEntity != null && !string.IsNullOrWhiteSpace(this.CurEntity.PromptMessage);
				if (flag3)
				{
					this.PromptDialog(this.CurEntity.PromptMessage);
				}
				else
				{
					bool flag4 = !string.IsNullOrWhiteSpace(this.PromptMessage);
					if (flag4)
					{
						this.PromptDialog(this.PromptMessage);
					}
				}
			}
			this.OnPreRender();
			base.OnPreRender(e);
		}

		protected virtual void Render()
		{
		}

		protected sealed override void Render(HtmlTextWriter writer)
		{
			this.Render();
			base.Render(writer);
		}

		protected internal virtual void RecoverData()
		{
		}

		protected internal virtual void RecoverSlaveData()
		{
		}

		protected virtual void OnRecoverXGridComplete()
		{
		}

		private bool ModifyControl()
		{
			bool result = true;
			bool flag = this.CurEntity != null && this.CurEntity.Entity.ModifyControl && this.GetParameterValue("SYS0001") == "Y";
			if (flag)
			{
				bool flag2 = !string.IsNullOrWhiteSpace(this.ModifierId) && !this.ModifierId.Equals(this.UserId);
				if (flag2)
				{
					result = false;
				}
			}
			return result;
		}

		protected virtual bool CanView()
		{
			return true;
		}

		internal bool CanViewTemplet()
		{
			return this.PagePermission.View && this.CanView();
		}

		protected virtual bool CanInsert()
		{
			return true;
		}

		internal bool CanInsertTemplet()
		{
			return this.PagePermission.Insert && this.CanInsert() && this.CurEntity.CanInsert();
		}

		protected virtual bool CanEdit()
		{
			return true;
		}

		public bool CanEditTemplet()
		{
			bool workflow = this.CurEntity.Entity.Workflow;
			bool result;
			if (workflow)
			{
				this.Workflow.Init(this.WorkflowId, this.WorkflowRunId);
				result = (this.PagePermission.Edit && this.CurEntity.CanEdit() && this.CanEdit() && this.Workflow.GetActionPermission(WorkflowAction.E));
			}
			else
			{
				result = (this.PagePermission.Edit && this.ModifyControl() && this.CurEntity.CanEdit() && this.CanEdit());
			}
			return result;
		}

		protected virtual bool CanAttachEdit()
		{
			return true;
		}

		internal bool CanAttachEditTemplet()
		{
			return this.CanAttachEdit();
		}

		protected virtual bool CanDelete()
		{
			return true;
		}

		public bool CanDeleteTemplet()
		{
			bool workflow = this.CurEntity.Entity.Workflow;
			bool result;
			if (workflow)
			{
				this.Workflow.Init(this.WorkflowId, this.WorkflowRunId);
				result = (this.PagePermission.Delete && this.CurEntity.CanDelete() && this.CanDelete() && this.Workflow.GetActionPermission(WorkflowAction.R));
			}
			else
			{
				result = (this.PagePermission.Delete && this.ModifyControl() && this.CurEntity.CanDelete() && this.CanDelete());
			}
			return result;
		}

		protected virtual bool CanPrint()
		{
			return true;
		}

		internal bool CanPrintTemplet()
		{
			return this.PagePermission.Print && this.CanPrint();
		}

		protected virtual bool CanDesign()
		{
			return true;
		}

		internal bool CanDesignTemplet()
		{
			return this.PagePermission.Design && this.CanDesign();
		}

		internal virtual void SwitchPagePermission()
		{
		}

		protected virtual bool CanSlaveInsert()
		{
			return true;
		}

		protected virtual bool CanSlaveEdit()
		{
			return true;
		}

		protected virtual bool CanSlaveDelete()
		{
			return true;
		}

		internal bool CanTempletSlaveInsert()
		{
			return this.CanEditTemplet();
		}

		internal bool CanTempletSlaveEdit()
		{
			return this.CanEditTemplet();
		}

		internal bool CanTempletSlaveDelete()
		{
			return this.CanEditTemplet();
		}

		protected virtual bool CanSubmit()
		{
			return true;
		}

		internal bool CanSubmitTemplet()
		{
			bool flag = this.Workflow.IsEndWfpId(this.Workflow.WfId, this.Workflow.WfpId);
			bool result;
			if (flag)
			{
				this.PromptDialog("当前记录处于流程结束状态，不能进行提交操作！");
				result = false;
			}
			else
			{
				result = (this.CurEntity.CanSubmit() && this.CanSubmit() && this.Workflow.GetActionPermission(WorkflowAction.S));
			}
			return result;
		}

		protected virtual bool CanWithdraw()
		{
			return true;
		}

		internal bool CanWithdrawTemplet()
		{
			return this.CurEntity.CanWithdraw() && this.CanWithdraw() && this.Workflow.GetActionPermission(WorkflowAction.W);
		}

		protected internal virtual bool VerifyNull()
		{
			bool result = this.CurEntity.VerifyNull();
			foreach (KeyValuePair<string, FieldMapAttribute> item in this.CurEntity.Fields)
			{
				FieldMapAttribute fieldMap = item.Value;
				bool flag = !string.IsNullOrWhiteSpace(fieldMap.CtrlId) && !fieldMap.Null && ((fieldMap.Key && string.IsNullOrEmpty(fieldMap.NewValue.Trim())) || (!fieldMap.Key && string.IsNullOrEmpty(fieldMap.NewValue.TrimEnd(new char[0]))) || (fieldMap.DbType == TimDbType.Float && fieldMap.NewValue.TrimEnd(new char[0]).Equals("0")));
				if (flag)
				{
					bool flag2 = fieldMap.DbType == TimDbType.Float;
					if (flag2)
					{
						this.PromptDialog(string.Format(" 【{0}】不允许为空或零！\r", fieldMap.Desc));
					}
					else
					{
						this.PromptDialog(string.Format(" 【{0}】不允许为空！\r", fieldMap.Desc));
					}
					result = false;
					break;
				}
			}
			return result;
		}

		protected internal virtual bool VerifyLength()
		{
			bool result = true;
			foreach (KeyValuePair<string, FieldMapAttribute> item in this.CurEntity.Fields)
			{
				FieldMapAttribute fieldMap = item.Value;
				bool flag = !string.IsNullOrWhiteSpace(fieldMap.CtrlId) && (fieldMap.DbType == TimDbType.Char || fieldMap.DbType == TimDbType.VarChar || fieldMap.DbType == TimDbType.NVarChar) && Regex.Replace(fieldMap.NewValue, "[^\0-ÿ]", "**").Length > fieldMap.Len;
				if (flag)
				{
					this.PromptDialog(string.Format(" 【{0}】值超出设置限定长度({1})！ \r", fieldMap.Desc, fieldMap.Len));
					result = false;
					break;
				}
				bool flag2 = !string.IsNullOrWhiteSpace(fieldMap.CtrlId) && fieldMap.DbType == TimDbType.Text && AppConfig.DbMS == DbProviderType.ORACLE && Regex.Replace(fieldMap.NewValue, "[^\0-ÿ]", "**").Length > 4000;
				if (flag2)
				{
					this.PromptDialog(string.Format(" 【{0}】值超出设置限定长度(4000)！ \r", fieldMap.Desc));
					result = false;
					break;
				}
				bool flag3 = !string.IsNullOrWhiteSpace(fieldMap.CtrlId) && fieldMap.CtrlType == ControlType.CheckBoxList && Regex.Replace(fieldMap.NewValue, "[^\0-ÿ]", "**").Length > fieldMap.Len;
				if (flag3)
				{
					this.PromptDialog(string.Format(" 【{0}】值超出设置限定长度({1})！ \r", fieldMap.Desc, fieldMap.Len));
					result = false;
					break;
				}
			}
			return result;
		}

		protected virtual bool VerifyBusinessLogic()
		{
			return true;
		}

		internal virtual bool TempletVerifyBusinessLogic()
		{
			bool flag = this.CurEntity != null;
			bool result;
			if (flag)
			{
				result = (this.CurEntity.VerifyBusinessLogic() && this.VerifyBusinessLogic());
			}
			else
			{
				result = this.VerifyBusinessLogic();
			}
			return result;
		}

		protected virtual bool VerifyQuery()
		{
			return true;
		}

		protected virtual bool OnPreOk()
		{
			return true;
		}

		protected virtual bool OnOk()
		{
			return true;
		}

		protected virtual bool OnOkComplete()
		{
			return true;
		}

		protected virtual bool OnPreClose()
		{
			return true;
		}

		protected virtual bool OnClose()
		{
			return true;
		}

		protected virtual bool OnCloseComplete()
		{
			return true;
		}

		protected virtual bool OnPreInsert()
		{
			return true;
		}

		protected virtual bool OnInsert()
		{
			return true;
		}

		protected virtual bool OnInsertComplete()
		{
			return true;
		}

		protected virtual bool OnPreCopy()
		{
			return true;
		}

		protected virtual bool OnCopy()
		{
			return true;
		}

		protected virtual bool OnCopyComplete()
		{
			return true;
		}

		protected virtual bool OnPreView()
		{
			return true;
		}

		protected virtual bool OnView()
		{
			return true;
		}

		protected virtual bool OnViewComplete()
		{
			return true;
		}

		protected virtual bool OnPreEdit()
		{
			return true;
		}

		protected virtual bool OnEdit()
		{
			return true;
		}

		protected virtual bool OnEditComplete()
		{
			return true;
		}

		protected virtual bool OnPreSaveSwitch()
		{
			return true;
		}

		protected virtual bool OnPreSave()
		{
			return true;
		}

		protected virtual bool OnSave()
		{
			return true;
		}

		protected virtual bool OnSaveComplete()
		{
			return true;
		}

		protected virtual bool OnSaveCompleteSwitch()
		{
			return true;
		}

		protected virtual bool OnPreDelete()
		{
			return true;
		}

		protected virtual bool OnDelete()
		{
			return true;
		}

		protected virtual bool OnDeleteComplete()
		{
			return true;
		}

		protected virtual bool OnPreSlaveDelete()
		{
			return true;
		}

		protected virtual bool OnSlaveDelete()
		{
			return true;
		}

		protected virtual bool OnSlaveDeleteComplete()
		{
			return true;
		}

		protected virtual void SetDefaultPageSize()
		{
			bool flag = this.CurPagingBar != null;
			if (flag)
			{
				this.CurPagingBar.PageSize = 20;
			}
		}

		protected virtual bool OnPreQuery()
		{
			return true;
		}

		protected virtual bool OnQuery()
		{
			return true;
		}

		protected virtual bool OnQueryComplete()
		{
			return true;
		}

		protected virtual bool OnPreSlaveQuery()
		{
			return true;
		}

		protected virtual bool OnSlaveQuery()
		{
			return true;
		}

		protected virtual bool OnSlaveQueryComplete()
		{
			return true;
		}

		protected virtual void AppendCodeHelperParam()
		{
		}

		protected void AddCodeHelperParam(string key, string value)
		{
			this.m_codeHelperAppendParam.Add(key, value);
		}

		internal string GetCodeHelperAppendParam()
		{
			string result = string.Empty;
			foreach (KeyValuePair<string, string> item in this.m_codeHelperAppendParam)
			{
				result = string.Concat(new string[]
				{
					result,
					base.Server.UrlEncode(item.Key),
					"=",
					base.Server.UrlEncode(item.Value),
					"&"
				});
			}
			result.TrimEnd(new char[]
			{
				'&'
			});
			return result;
		}

		protected virtual void PlaceUpdateScript()
		{
		}

		public void RegisterScript(string key, string script)
		{
			this.RegisterScript(key, script, true);
		}

		internal void RegisterScript(string key, string script, bool addScriptTags)
		{
			ScriptManager.RegisterStartupScript(this, base.GetType(), key, script, addScriptTags);
			this.PlaceUpdateScript();
		}

		internal void GetCtrlValByPage(ContentPlaceHolder place)
		{
			foreach (KeyValuePair<string, FieldMapAttribute> item in this.CurEntity.Fields)
			{
				item.Value.OldValue = item.Value.NewValue;
				bool flag = item.Value.DbField == "CREATERID" || item.Value.DbField == "MODIFIERID";
				if (flag)
				{
					item.Value.NewValue = this.UserId;
					bool flag2 = !string.IsNullOrWhiteSpace(item.Value.CtrlId);
					if (flag2)
					{
						Control ctrl = place.FindControl(item.Value.CtrlId);
						bool flag3 = ctrl != null;
						if (flag3)
						{
							((TimTextBox)ctrl).Text = this.UserId;
						}
					}
				}
				else
				{
					bool flag4 = item.Value.DbField == "CREATER" || item.Value.DbField == "MODIFIER";
					if (flag4)
					{
						item.Value.NewValue = this.UserName;
						bool flag5 = !string.IsNullOrWhiteSpace(item.Value.CtrlId);
						if (flag5)
						{
							Control ctrl = place.FindControl(item.Value.CtrlId);
							bool flag6 = ctrl != null;
							if (flag6)
							{
								((TimTextBox)ctrl).Text = this.UserName;
							}
						}
					}
					else
					{
						bool flag7 = item.Value.DbField == "CREATEDTIME" || item.Value.DbField == "MODIFIEDTIME";
						if (flag7)
						{
							item.Value.NewValue = AppRuntime.ServerDateTime.ToString();
							bool flag8 = !string.IsNullOrWhiteSpace(item.Value.CtrlId);
							if (flag8)
							{
								Control ctrl = place.FindControl(item.Value.CtrlId);
								bool flag9 = ctrl != null;
								if (flag9)
								{
									((TimDateTime)ctrl).Text = item.Value.NewValue;
								}
							}
						}
						else
						{
							bool flag10 = string.IsNullOrWhiteSpace(item.Value.CtrlId);
							if (!flag10)
							{
								TempletUtils.GetCtrlValByPage(place, item.Value);
							}
						}
					}
				}
			}
		}

		internal void SetCtrlMaxLength(ContentPlaceHolder place)
		{
			foreach (KeyValuePair<string, FieldMapAttribute> item in this.CurEntity.Fields)
			{
				bool flag = string.IsNullOrEmpty(item.Value.CtrlId);
				if (!flag)
				{
					Control ctrl = place.FindControl(item.Value.CtrlId);
					bool flag2 = ctrl == null;
					if (!flag2)
					{
						ControlType ctrlType = item.Value.CtrlType;
						if (ctrlType == ControlType.TextBox)
						{
							((TimTextBox)ctrl).MaxLength = item.Value.Len;
						}
					}
				}
			}
		}

		protected virtual void SetCtrlState()
		{
		}

		protected virtual void SwitchControlValue()
		{
		}

		internal void SetPageCtrlState(ControlCollection controls, bool readOnly)
		{
			foreach (Control ctrl in controls)
			{
				bool flag = ctrl is TimPanel;
				if (flag)
				{
					((WebControl)ctrl).Enabled = !readOnly;
					this.SetPageCtrlState(ctrl.Controls, readOnly);
				}
				else
				{
					bool flag2 = ctrl is TimTextBox || ctrl is TimDate || ctrl is TimDateTime || ctrl is TimNumericTextBox || ctrl is TimCKEditor;
					if (flag2)
					{
						((TextBox)ctrl).ReadOnly = readOnly;
					}
					else
					{
						bool flag3 = ctrl is TimNumericUpDown;
						if (flag3)
						{
							((TimNumericUpDown)ctrl).ReadOnly = readOnly;
						}
						else
						{
							WebControl webCtrl = ctrl as WebControl;
							bool flag4 = webCtrl != null;
							if (flag4)
							{
								webCtrl.Enabled = !readOnly;
							}
						}
					}
				}
			}
		}

		internal void SetMasterCtrlState()
		{
			string entityPromptMessage = string.Empty;
			string templetPromptMessage = string.Empty;
			bool flag = this.CurEntity != null;
			if (flag)
			{
				entityPromptMessage = this.CurEntity.PromptMessage;
			}
			templetPromptMessage = this.PromptMessage;
			this.SetTempletCtrlState();
			bool flag2 = this.CurEntity != null;
			if (flag2)
			{
				this.CurEntity.PromptMessage = entityPromptMessage;
			}
			this.m_promptMessage = templetPromptMessage;
		}

		internal void SwitchPageOperState()
		{
			string entityPromptMessage = string.Empty;
			string templetPromptMessage = string.Empty;
			bool flag = this.CurEntity != null;
			if (flag)
			{
				entityPromptMessage = this.CurEntity.PromptMessage;
			}
			templetPromptMessage = this.PromptMessage;
			this.SwitchTempletPageOperState();
			bool flag2 = this.CurEntity != null;
			if (flag2)
			{
				this.CurEntity.PromptMessage = entityPromptMessage;
			}
			this.m_promptMessage = templetPromptMessage;
		}

		internal virtual void SetTempletCtrlState()
		{
		}

		internal virtual void SwitchTempletPageOperState()
		{
		}

		internal void SetWorkflowFieldRight(string wfId)
		{
			bool flag = string.IsNullOrWhiteSpace(wfId);
			if (!flag)
			{
				string wfpId = string.Empty;
				bool flag2 = string.IsNullOrWhiteSpace(this.Workflow.WfpId);
				if (flag2)
				{
					wfpId = this.Workflow.GetBeginWfpId(wfId);
				}
				else
				{
					wfpId = this.Workflow.WfpId;
				}
				List<WFFR> lstWffr = WFFRUtils.GetWFFR(wfId, wfpId);
				foreach (WFFR wffr in lstWffr)
				{
					Control ctrl = this.FindPointPlaceControl(wffr.ControlId);
					bool flag3 = ctrl == null;
					if (!flag3)
					{
						bool flag4 = !wffr.CtrlNull;
						if (flag4)
						{
							this.SetContrlSymbol(ctrl);
						}
						switch (wffr.Right)
						{
						case WffrRightType.R:
							this.SetControlReadOnly(ctrl, true);
							break;
						case WffrRightType.F:
							this.SetControlEnabled(ctrl, false);
							break;
						case WffrRightType.H:
							this.SetControlVisible(ctrl, false);
							break;
						case WffrRightType.S:
							this.SetControlVisible(ctrl, true);
							break;
						case WffrRightType.E:
						{
							bool flag5 = this.PageOperState == PageState.INSERT || this.PageOperState == PageState.COPY || this.PageOperState == PageState.EDIT;
							if (flag5)
							{
								this.SetControlReadOnly(ctrl, false);
							}
							break;
						}
						case WffrRightType.A:
						{
							bool flag6 = this.PageOperState == PageState.INSERT || this.PageOperState == PageState.COPY || this.PageOperState == PageState.EDIT;
							if (flag6)
							{
								this.SetControlEnabled(ctrl, true);
							}
							break;
						}
						}
					}
				}
			}
		}

		private void SetContrlSymbol(Control ctrl)
		{
		}

		private void SetControlVisible(Control ctrl, bool visible)
		{
			bool flag = ctrl is WebControl;
			if (flag)
			{
				((WebControl)ctrl).Style["display"] = (visible ? "block" : "none");
			}
			else
			{
				ctrl.Visible = visible;
			}
		}

		private void SetControlEnabled(Control ctrl, bool enabled)
		{
			bool flag = ctrl is WebControl;
			if (flag)
			{
				((WebControl)ctrl).Enabled = enabled;
			}
		}

		private void SetControlReadOnly(Control ctrl, bool readOnly)
		{
			bool flag = ctrl is TimTextBox || ctrl is TimDate || ctrl is TimDateTime || ctrl is TimNumericTextBox;
			if (flag)
			{
				((TextBox)ctrl).ReadOnly = readOnly;
			}
			else
			{
				bool flag2 = ctrl is WebControl;
				if (flag2)
				{
					((WebControl)ctrl).Enabled = !readOnly;
				}
			}
		}

		public Control FindPointPlaceControl(string id)
		{
			Control found = null;
			bool flag = found == null;
			if (flag)
			{
				found = this.Page.FindControl(id);
			}
			bool flag2 = found == null;
			if (flag2)
			{
				found = this.FindControlExtend(id, this.Page.Controls);
			}
			return found;
		}

		private Control FindControlExtend(string id, ControlCollection controls)
		{
			Control found = null;
			foreach (Control control in controls)
			{
				bool flag = control.ID == id;
				if (flag)
				{
					found = control;
					break;
				}
				bool flag2 = control.Controls.Count > 0;
				if (flag2)
				{
					found = this.FindControlExtend(id, control.Controls);
					bool flag3 = found != null;
					if (flag3)
					{
						break;
					}
				}
			}
			return found;
		}

		protected void PromptDialog(string msg)
		{
			bool flag = !base.IsPostBack || !this.CurSM.IsInAsyncPostBack;
			if (flag)
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), "Prompt", "alert('" + msg + "');", true);
			}
			else
			{
				this.m_promptMessage += msg;
			}
		}

		internal void PromptPreDialog(string msg)
		{
			bool flag = !base.IsPostBack || !this.CurSM.IsInAsyncPostBack;
			if (flag)
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), "Prompt", "alert('" + msg + "');", true);
			}
			else
			{
				bool flag2 = string.IsNullOrWhiteSpace(this.m_promptMessage);
				if (flag2)
				{
					this.m_promptMessage = msg;
				}
			}
		}

		protected void CloseDialog()
		{
			this.RegisterScript("CloseDialog", "window.setTimeout(function(){frameElement.dialog.close();},100);");
		}

		protected void OpenDialog(PageParameter openParams)
		{
			this.OpenDialogTemplet(openParams, "", "");
		}

		protected void OpenDialog(PageParameter openParams, string btnClientId)
		{
			this.OpenDialogTemplet(openParams, btnClientId, "");
		}

		internal void OpenWorkflowDialog(PageParameter openParams, string callbackFunc)
		{
			this.OpenDialogTemplet(openParams, "", callbackFunc);
		}

		private void OpenDialogTemplet(PageParameter openParams, string btnClientId, string callbackFunc)
		{
			bool flag = string.IsNullOrEmpty(openParams.UrlPath);
			if (!flag)
			{
				openParams.SaveParams();
				string urlPath = string.Concat(new string[]
				{
					openParams.UrlPath,
					"?",
					openParams.EncodedParameters,
					"&SK=",
					openParams.State.ToString()
				});
				StringBuilder sb = new StringBuilder();
				sb.Append("OpenPage(\"");
				bool flag2 = openParams.State > PageState.VIEW;
				if (flag2)
				{
					sb.Append(openParams.State.ToString());
				}
				sb.Append("\",\"");
				sb.Append(urlPath + "\"");
				bool flag3 = openParams.Width == 0;
				if (flag3)
				{
					sb.Append(",0");
				}
				else
				{
					sb.Append(("," + openParams.Width.ToString()) ?? "");
				}
				bool flag4 = openParams.Height == 0;
				if (flag4)
				{
					sb.Append(",0,\"");
				}
				else
				{
					sb.Append("," + openParams.Height.ToString() + ",\"");
				}
				bool flag5 = !string.IsNullOrEmpty(openParams.Title);
				if (flag5)
				{
					sb.Append(openParams.Title);
				}
				bool flag6 = string.IsNullOrWhiteSpace(callbackFunc);
				if (flag6)
				{
					sb.Append("\",callbackFunc");
				}
				else
				{
					sb.Append(("\"," + callbackFunc) ?? "");
				}
				bool allowClose = openParams.AllowClose;
				if (allowClose)
				{
					sb.Append(",true");
				}
				else
				{
					sb.Append(",false");
				}
				bool allowMax = openParams.AllowMax;
				if (allowMax)
				{
					sb.Append(",true);");
				}
				else
				{
					sb.Append(",false);");
				}
				bool flag7 = !string.IsNullOrEmpty(btnClientId);
				if (flag7)
				{
					sb.Append("function callbackFunc() { try { CallBtnClientClick(\"");
					sb.Append(btnClientId);
					sb.Append("\"); } catch(e) {alert(e)}};");
				}
				else
				{
					sb.Append("function callbackFunc(returnValue) {};");
				}
				this.RegisterScript("OpenDialog", sb.ToString());
			}
		}

		internal void OpenWorkflowHandleDialog(WorkflowAction workflowAction)
		{
			bool flag = workflowAction == WorkflowAction.T;
			if (flag)
			{
				PageParameter pageParams = new PageParameter();
				pageParams.UrlPath = this.Page.ResolveUrl("~") + "T_TEMPLET/CommForm/WfpTrace.aspx";
				pageParams.AddString("WFID", this.WorkflowId);
				pageParams.AddString("WFRUNID", this.WorkflowRunId.ToString());
				this.OpenDialog(pageParams);
			}
			else
			{
				PageParameter pageParams2 = new PageParameter();
				pageParams2.UrlPath = this.Page.ResolveUrl("~") + "T_TEMPLET/CommForm/WfHandle.aspx";
				pageParams2.Width = 560;
				pageParams2.Height = 530;
				pageParams2.State = PageState.EDIT;
				pageParams2.AddString("WFID", this.WorkflowId);
				pageParams2.AddString("WFRUNID", this.WorkflowRunId.ToString());
				pageParams2.AddString("WFPACTION", workflowAction.ToString());
				this.OpenDialog(pageParams2);
			}
		}

		internal void OpenAttachDialog(TMasterBase CurMaster)
		{
			DocFileListView fileForm = new DocFileListView();
			bool flag = this.PageOperState == PageState.INSERT || this.PageOperState == PageState.COPY || this.PageOperState == PageState.EDIT;
			if (flag)
			{
				fileForm.Enabled = true;
			}
			else
			{
				bool flag2 = this.CanAttachEditTemplet() && (this.CanEditTemplet() || this.CanDeleteTemplet());
				if (flag2)
				{
					fileForm.Enabled = true;
				}
			}
			fileForm.FileGroupId = CurMaster._Maintenance.TempletFileGroup.Value.ToDouble();
			fileForm.BusinessTable = this.CurEntity.Entity.Table;
			fileForm.FileGroupIdField = this.CurEntity.Entity.Table + "_FGID";
			fileForm.FilesField = this.CurEntity.Entity.Table + "_FILES";
			fileForm.FileGroupClientId = CurMaster._Maintenance.TempletFileGroup.ClientID;
			fileForm.FilesClientId = CurMaster._Maintenance.TempletFiles.ClientID;
			fileForm.MaxFileSize = 31457280L;
			fileForm.MaxFileGroupSize = 0L;
			fileForm.ExecOn = this.CurEntity.BuildExecOn();
			this.OpenDialog(fileForm.BuildPageParameter(CurMaster._Maintenance.TempletFileGroup));
		}

		private string GenNamedTempFilePath(string fileName)
		{
			string result = string.Empty;
			fileName = base.Server.UrlEncode(fileName);
			return this.Page.ResolveUrl("~/T_TEMPLET/Handler/NamedTempFileDownload.ashx?FILENAME=" + fileName.Trim());
		}

		private string GenNamedTempFile(TimGridView gvExcel, string fileName)
		{
			return ExcelUtils.Export(gvExcel, string.Format("{0}({1}).xls", this.MdName, this.UserName));
		}

		protected double GetFileGroupId()
		{
			bool flag = this.MasterBase != null;
			double result;
			if (flag)
			{
				result = this.MasterBase._Maintenance.TempletFileGroup.Value.ToDouble();
			}
			else
			{
				result = 0.0;
			}
			return result;
		}

		protected double GetFiles()
		{
			bool flag = this.MasterBase != null;
			double result;
			if (flag)
			{
				result = this.MasterBase._Maintenance.TempletFiles.Value.ToDouble();
			}
			else
			{
				result = 0.0;
			}
			return result;
		}

		protected void SetFileGroupId(double fileGroupId)
		{
			bool flag = this.MasterBase != null;
			if (flag)
			{
				this.MasterBase._Maintenance.TempletFileGroup.Value = fileGroupId.ToString();
				bool isPostBack = base.IsPostBack;
				if (isPostBack)
				{
					this.MasterBase._Maintenance.UpTempletPlace.Update();
				}
			}
		}

		protected void SetFiles(double files)
		{
			bool flag = this.MasterBase != null;
			if (flag)
			{
				this.MasterBase._Maintenance.TempletFiles.Value = files.ToString();
				bool isPostBack = base.IsPostBack;
				if (isPostBack)
				{
					this.MasterBase._Maintenance.UpTempletPlace.Update();
				}
			}
		}

		protected virtual void RecoverBtnAttachText()
		{
		}

		protected void ExportExcelFile(TimGridView gvExcel)
		{
			string fileName = string.Format("{0}({1}).xls", this.MdName, this.UserName);
			fileName = this.GenNamedTempFile(gvExcel, fileName);
			string filePath = this.GenNamedTempFilePath(fileName);
			this.RegisterScript("TExportExcle", "TempletDownloadExcel('" + filePath + "');");
		}
	}
}
