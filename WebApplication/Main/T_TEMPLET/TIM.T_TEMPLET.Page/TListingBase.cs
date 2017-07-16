using System;
using System.Data;
using System.Web.UI.WebControls;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Helper;
using TIM.T_TEMPLET.Enum;
using TIM.T_TEMPLET.Master;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Page
{
	public class TListingBase : PageBase
	{
		private TListing m_curMaster = null;

		public TListing CurMaster
		{
			get
			{
				return this.m_curMaster;
			}
			set
			{
				this.m_curMaster = value;
			}
		}

		private void BuildEvent()
		{
			this.CurMaster.OnDelete += new TMasterBase.Delete(this.CurMaster_OnDelete);
			this.CurMaster.OnQuery += new TMasterBase.Query(this.CurMaster_OnQuery);
			this.CurMaster.OnPrint += new TMasterBase.Print(this.CurMaster_OnPrint);
			this.CurMaster.OnPrintMenuItem += new TMasterBase.PrintMenuItem(this.CurMaster_OnPrintMenuItem);
			this.CurMaster.OnPreview += new TMasterBase.Preview(this.CurMaster_OnPreview);
			this.CurMaster.OnPreviewMenuItem += new TMasterBase.PreviewMenuItem(this.CurMaster_OnPreviewMenuItem);
			this.CurMaster.OnReportStyle += new TMasterBase.ReportStyle(this.CurMaster_OnReportStyle);
			this.CurMaster.OnAttach += new TMasterBase.Attach(this.CurMaster_OnAttach);
			this.CurMaster.OnAttachMenuItem += new TMasterBase.AttachMenuItem(this.CurMaster_OnAttachMenuItem);
			base.CurPagingBar.PageChanging += new PageChangingEventHandler(this.CurPagingBar_PageChanging);
			base.CurPagingBar.PageChanged += new EventHandler(this.CurPagingBar_PageChanged);
			base.CurGrid.RowDataBound += new GridViewRowEventHandler(this.CurGrid_RowDataBound);
			base.CurGrid.RowDataBound += new GridViewRowEventHandler(this.CurGrid_RowDataBound_Templet);
		}

		protected virtual void CurGrid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
		}

		private void CurGrid_RowDataBound_Templet(object sender, GridViewRowEventArgs e)
		{
			bool flag = e.Row.RowType == DataControlRowType.DataRow;
			if (flag)
			{
				string sAllMaxPara = "false";
				bool allowMax = base.PageUrlParam.AllowMax;
				if (allowMax)
				{
					sAllMaxPara = "true";
				}
				bool flag2 = string.IsNullOrWhiteSpace(base.PageUrlParam.UrlPath);
				if (flag2)
				{
					e.Row.Attributes.Add("onclick", string.Concat(new string[]
					{
						e.Row.Attributes["onclick"],
						"SetPageUrlParam(this,'",
						base.PageUrlParam.EncodedParameters,
						"',",
						sAllMaxPara,
						");"
					}));
				}
				else
				{
					e.Row.Attributes.Add("onclick", string.Concat(new string[]
					{
						e.Row.Attributes["onclick"],
						"SetPageUrlParam(this,'",
						base.PageUrlParam.UrlPath,
						"?",
						base.PageUrlParam.EncodedParameters,
						"',",
						sAllMaxPara,
						");"
					}));
				}
				base.PageUrlParam = new PageParameter();
			}
		}

		private void CurPagingBar_PageChanged(object sender, EventArgs e)
		{
			base.CurGrid.SelectedIndex = -1;
			this.OnQuery();
			this.GridDataBind();
			this.PlaceUpdateContent();
		}

		private void CurPagingBar_PageChanging(object src, PageChangingEventArgs e)
		{
		}

		private void CurMaster_OnAttachMenuItem(object sender, string itemValue)
		{
		}

		private void CurMaster_OnAttach(object sender, EventArgs e)
		{
		}

		private void CurMaster_OnPrint(object sender, EventArgs e)
		{
		}

		private void CurMaster_OnPrintMenuItem(object sender, string itemValue)
		{
			base.ReportParse.ReportDataSetAdd(base.CurEntity.Entity.Table, base.CurEntity);
			this.OnPreQuery();
			base.CurEntity.RecordSetSql = base.CurEntity.BuildRecordSetSql();
			base.ReportParse.ReportRecordSetAdd(base.CurEntity.Entity.Table, base.CurEntity, base.CurEntity.RecordSetSql);
			base.ReportParse.PreparePrintOrPreview(base.PageAmIdPlusClassName, itemValue, "Print");
			base.RegisterScript("PreviewMenuItem", "window.setTimeout('SetReport()', 1);function SetReport() {" + base.ReportParse.run + "}");
		}

		private void CurMaster_OnPreview(object sender, EventArgs e)
		{
		}

		private void CurMaster_OnPreviewMenuItem(object sender, string itemValue)
		{
			base.ReportParse.ReportDataSetAdd(base.CurEntity.Entity.Table, base.CurEntity);
			this.OnPreQuery();
			base.CurEntity.RecordSetSql = base.CurEntity.BuildRecordSetSql();
			base.ReportParse.ReportRecordSetAdd(base.CurEntity.Entity.Table, base.CurEntity, base.CurEntity.RecordSetSql);
			string sHtml = base.ReportParse.PreparePrintOrPreviewHtml(base.PageAmIdPlusClassName, itemValue, "Preview");
			PageParameter pageParams = new PageParameter();
			pageParams.AllowMax = true;
			pageParams.UrlPath = this.Page.ResolveUrl("~") + "T_TEMPLET/CommForm/rptView.aspx";
			pageParams.AddString("RPT_HTMLPATH", sHtml);
			base.OpenDialog(pageParams);
		}

		private void CurMaster_OnReportStyle(object sender, EventArgs e)
		{
			PageParameter pageParams = new PageParameter();
			pageParams.UrlPath = this.Page.ResolveUrl("~") + "T_TEMPLET/CommForm/ReportStyleList.aspx";
			pageParams.AddString("STYLEID", base.PageAmIdPlusClassName);
			base.ReportParse.ReportDataSetAdd(base.CurEntity.Entity.Table, base.CurEntity);
			base.ReportParse.PrepareSetMode();
			pageParams.AddExtString("REPORTSTYLE", base.ReportParse.ReportXMLDataSets);
			base.OpenDialog(pageParams);
		}

		protected sealed override bool OnQuery()
		{
			bool result = false;
			bool flag = !this.VerifyQuery();
			bool result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				bool flag2 = !this.OnPreQuery();
				if (flag2)
				{
					result2 = result;
				}
				else
				{
					base.CurEntity.RecordSetSql = base.CurEntity.BuildRecordSetSql();
					bool flag3 = !base.OnQuery();
					if (flag3)
					{
						result2 = result;
					}
					else
					{
						base.CurEntity.PageSize = base.CurPagingBar.PageSize;
						base.CurEntity.PageIndex = base.CurPagingBar.CurrentPageIndex - 1;
						base.CurEntity.QueryRecordSet();
						bool allowPaging = base.CurGrid.AllowPaging;
						if (allowPaging)
						{
							base.CurPagingBar.RecordCount = base.CurEntity.RecordCount;
							base.CurGrid.PageSize = base.CurPagingBar.PageSize;
							base.CurGrid.PageIndex = base.CurPagingBar.CurrentPageIndex - 1;
						}
						else
						{
							base.CurPagingBar.RecordCount = base.CurEntity.RecordCount;
							base.CurPagingBar.PageSize = base.CurEntity.RecordCount;
						}
						bool flag4 = !this.OnQueryComplete();
						result2 = (!flag4 || result);
					}
				}
			}
			return result2;
		}

		protected void CurMaster_OnQuery(object sender, EventArgs e)
		{
			bool flag = sender != null;
			if (flag)
			{
				base.CurPagingBar.CurrentPageIndex = 1;
			}
			base.CurGrid.SelectedIndex = -1;
			bool flag2 = !this.OnQuery();
			if (!flag2)
			{
				bool beExportExcel = base.BeExportExcel;
				if (beExportExcel)
				{
					this.SetDefaultPageSize();
				}
				this.GridDataBind();
				this.PlaceUpdateContent();
			}
		}

		protected void GridDataBind()
		{
			base.CurGrid.DataSource = base.CurEntity.RecordSet;
			base.CurGrid.DataBind();
		}

		protected internal override void RecoverData()
		{
			bool flag = base.CurEntity != null && base.CurEntity.Entity.ModifyControl;
			if (flag)
			{
				base.ModifierId = base.CurGrid.DataKeys[base.CurGrid.SelectedIndex]["MODIFIERID"].ToString().Trim();
			}
			base.CurEntity.RecordSet.Columns.Clear();
			string[] dataKeyNames = base.DataKeyNames;
			for (int i = 0; i < dataKeyNames.Length; i++)
			{
				string keyName = dataKeyNames[i];
				base.CurEntity.RecordSet.Columns.Add(keyName);
			}
			DataRow row = base.CurEntity.RecordSet.NewRow();
			string[] dataKeyNames2 = base.DataKeyNames;
			for (int j = 0; j < dataKeyNames2.Length; j++)
			{
				string keyName2 = dataKeyNames2[j];
				bool flag2 = base.CurEntity.Fields.ContainsKey(keyName2) && base.CurEntity.Fields[keyName2].Key;
				if (flag2)
				{
					row[keyName2] = base.CurGrid.DataKeys[base.CurGrid.SelectedIndex][keyName2].ToString();
				}
				else
				{
					row[keyName2] = base.CurGrid.DataKeys[base.CurGrid.SelectedIndex][keyName2].ToString().Trim();
				}
			}
			base.CurEntity.RecordSet.Rows.Add(row);
			base.CurEntity.ActivedIndex = 0;
		}

		protected sealed override bool OnDelete()
		{
			bool result = true;
			this.RecoverData();
			base.CurEntity.DeleteSql = base.CurEntity.BuildDeleteSql();
			bool flag = result;
			if (flag)
			{
				result = this.OnPreDelete();
			}
			bool flag2 = result;
			if (flag2)
			{
				result = base.CanDeleteTemplet();
			}
			bool flag3 = !result;
			if (flag3)
			{
				base.PromptDialog("当前用户没有此记录的删除权限！");
			}
			bool flag4 = result;
			if (flag4)
			{
				result = base.OnDelete();
			}
			base.CurEntity.MODIFIERID = base.UserId;
			base.CurEntity.MODIFIER = base.UserName;
			base.CurEntity.MODIFIEDTIME = DateTime.Now.ToString();
			bool flag5 = result;
			if (flag5)
			{
				result = base.CurEntity.Delete();
			}
			bool workflow = base.CurEntity.Entity.Workflow;
			if (workflow)
			{
				bool flag6 = result;
				if (flag6)
				{
					result = base.Workflow.DeleteWorkflowRI(base.WorkflowId, base.WorkflowRunId);
				}
			}
			bool flag7 = result;
			if (flag7)
			{
				result = this.OnDeleteComplete();
			}
			return result;
		}

		private void CurMaster_OnDelete(object sender, EventArgs e)
		{
			bool result = false;
			bool workflow = base.CurEntity.Entity.Workflow;
			if (workflow)
			{
				base.CurEntity.WorkflowId = (base.WorkflowId = base.CurGrid.SelectedDataKey[base.WfIdField].ToString().Trim());
				base.CurEntity.WorkflowRunId = (base.WorkflowRunId = base.CurGrid.SelectedDataKey[base.WfRunIdField].ToString().Trim().ToInt());
			}
			Database db = LogicContext.GetDatabase();
			db.BeginTrans();
			try
			{
				result = this.OnDelete();
				bool flag = result;
				if (flag)
				{
					db.CommitTrans();
				}
				else
				{
					db.RollbackTrans();
				}
			}
			catch
			{
				db.RollbackTrans();
			}
			bool flag2 = !result;
			if (!flag2)
			{
				this.OnQuery();
				this.GridDataBind();
				base.CurGrid.SelectedIndex = -1;
				this.PlaceUpdateContent();
			}
		}

		private void SetCtrlStatus()
		{
			this.CurMaster.SetCtrlStatus();
		}

		private void BuildWorkflowButtonEvent()
		{
			this.CurMaster.OnDirectSubmit += new TMasterBase.DirectSubmit(this.CurMaster_OnDirectSubmit);
			this.CurMaster.OnSubmit += new TMasterBase.Submit(this.CurMaster_OnSubmit);
			this.CurMaster.OnBack += new TMasterBase.Back(this.CurMaster_OnBack);
			this.CurMaster.OnWithdraw += new TMasterBase.Withdraw(this.CurMaster_OnWithdraw);
			this.CurMaster.OnDeliverTo += new TMasterBase.DeliverTo(this.CurMaster_OnDeliverTo);
			this.CurMaster.OnVeto += new TMasterBase.Veto(this.CurMaster_OnVeto);
			this.CurMaster.OnWorkflowTrace += new TMasterBase.WorkflowTrace(this.CurMaster_OnWorkflowTrace);
			this.CurMaster.OnFlowBlock += new TMasterBase.FlowBlock(this.CurMaster_OnFlowBlock);
			this.CurMaster.OnWorkflowButtonDropDown += new TMasterBase.WorkflowButtonDropDown(this.CurMaster_OnWorkflowButtonDropDown);
		}

		private void CurMaster_OnDirectSubmit(object sender, EventArgs e)
		{
			this.RecoverData();
			base.OnDirectSubmitIn(PageClassification.Listing);
		}

		private void CurMaster_OnSubmit(object sender, EventArgs e)
		{
			this.RecoverData();
			base.OnSubmitIn(PageClassification.Listing);
		}

		private void CurMaster_OnBack(object sender, EventArgs e)
		{
			base.OnBackIn(PageClassification.Listing);
		}

		private void CurMaster_OnWithdraw(object sender, EventArgs e)
		{
			base.OnWithdrawIn(PageClassification.Listing);
		}

		private void CurMaster_OnDeliverTo(object sender, EventArgs e)
		{
			base.OnDeliverToIn(PageClassification.Listing);
		}

		private void CurMaster_OnVeto(object sender, EventArgs e)
		{
			base.OnVetoIn(PageClassification.Listing);
		}

		private void CurMaster_OnWorkflowTrace(object sender, EventArgs e)
		{
			base.OnWorkflowTraceIn(PageClassification.Listing);
		}

		private void CurMaster_OnFlowBlock(string workflowAction, string nextWfId, string nextWfpId, string todo, string opinion)
		{
			this.RecoverData();
			base.OnFlowBlockIn(PageClassification.Listing, workflowAction, nextWfId, nextWfpId, todo, opinion);
		}

		private void CurMaster_OnWorkflowButtonDropDown(object sender, EventArgs e)
		{
			base.OnWorkflowButtonDropDownIn(PageClassification.Listing);
			this.PlaceUpdateMenu();
		}

		protected sealed override void OnInit()
		{
			this.CurMaster.CurGrid = base.CurGrid;
			base.CurPagingBar = this.CurMaster.CurPagingBar;
			this.BuildEvent();
			bool workflow = base.CurEntity.Entity.Workflow;
			if (workflow)
			{
				this.BuildWorkflowButtonEvent();
			}
			base.CurGrid.OnClientDblClick = "OpenPage('VIEW');return false;";
		}

		protected override void OnLoad()
		{
			bool flag = !base.IsPostBack;
			if (flag)
			{
				this.OnPreLoadRecord();
				this.OnLoadRecord();
				this.OnLoadRecordComplete();
			}
		}

		protected sealed override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);
			this.OnLoadComplete();
			this.CurMaster.BtnCopy.OnClientClick = (this.CurMaster.BtnView.OnClientClick = (this.CurMaster.BtnEdit.OnClientClick = (this.CurMaster.BtnDelete.OnClientClick = (this.CurMaster.BtnWorkflow.OnClientClick = string.Concat(new string[]
			{
				"if ($('#",
				base.CurGrid.ClientID,
				"') == null) {alert('当前没有可选记录！'); return false;} if ($('#",
				base.CurGrid.ClientID,
				"').attr('selectedIndex') == -1) {alert('请选择处理记录！'); return false;}"
			})))));
			TimButtonMenu expr_BB = this.CurMaster.BtnInsert;
			expr_BB.OnClientClick += " OpenPage('INSERT');return false;";
			TimButtonMenu expr_DC = this.CurMaster.BtnCopy;
			expr_DC.OnClientClick += " OpenPage('COPY');return false;";
			TimButtonMenu expr_FD = this.CurMaster.BtnView;
			expr_FD.OnClientClick += " OpenPage('VIEW');return false;";
			TimButtonMenu expr_11E = this.CurMaster.BtnEdit;
			expr_11E.OnClientClick += " OpenPage('EDIT');return false;";
			TimButtonMenu expr_13F = this.CurMaster.BtnDelete;
			expr_13F.OnClientClick += "if (confirm('您确定要删除所选记录？') == false) return false;";
			bool flag = !base.IsPostBack;
			if (flag)
			{
				string moduleClientVar = string.Empty;
				moduleClientVar = moduleClientVar + " var _EditingPage= '" + base.EditingPage + "';";
				moduleClientVar = moduleClientVar + " var _GvClientId= '" + base.CurGrid.ClientID + "';";
				base.RegisterScript("TempletClientVar", moduleClientVar);
			}
			base.SetMasterCtrlState();
			this.SetPageCtrlState();
		}

		internal override void SetTempletCtrlState()
		{
			this.CurMaster.BtnInsert.Visible = (this.CurMaster.BtnCopy.Visible = base.CanInsertTemplet());
			this.CurMaster.BtnPrint.Visible = (this.CurMaster.BtnPreview.Visible = (this.CurMaster.BtnPrint.Enabled = (this.CurMaster.BtnPreview.Enabled = base.CanPrintTemplet())));
			this.CurMaster.BtnReportStyle.Visible = (this.CurMaster.BtnReportStyle.Enabled = base.CanDesignTemplet());
			this.CurMaster.BtnWorkflow.Visible = base.CurEntity.Entity.Workflow;
		}

		private void SetPageCtrlState()
		{
			this.SetCtrlState();
			this.SetPageUrlAppendParam();
			base.RegisterScript("TempletClientAppendVar", "var _PageUrlAppendParam = '" + base.PageUrlAppendParam.EncodedParameters + "';");
		}

		protected override void SetMenu_OnlyQuery()
		{
			this.CurMaster.SetMenu_OnlyQuery();
		}

		protected override void SetMenu_OnlyQueryAndReport()
		{
			this.CurMaster.SetMenu_OnlyQueryAndReport();
		}

		protected override void SetMenu_HideAllStdBtn()
		{
			this.CurMaster.SetMenu_HideAllStdBtn();
		}

		protected override void SetMenu_OnlyViewEdit()
		{
			this.CurMaster.SetMenu_OnlyViewEdit();
		}

		protected override void PlaceUpdateScript()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpScriptPlace.Update();
			}
		}

		protected void PlaceUpdateMenu()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpMenuPlace.Update();
			}
		}

		protected void PlaceUpdateQuery()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpQueryPlace.Update();
			}
		}

		protected void PlaceUpdateContent()
		{
			bool flag = base.IsPostBack && !base.BeExportExcel;
			if (flag)
			{
				this.CurMaster.UpContentPlace.Update();
				base.RegisterScript("GridAdjust", "GridAdjust();");
			}
		}

		protected void PlaceUpdateTemplet()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpTempletPlace.Update();
			}
		}
	}
}
