using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_TEMPLET.Master;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Page
{
	public class TTreeGridBase : PageBase
	{
		private TTreeGrid m_curMaster = null;

		private int m_treeWidth = 0;

		private Hashtable m_treeImgList = new Hashtable();

		private bool m_enabledSelectedTreeNodeChanged = true;

		private bool m_enabledTreeNodeCheckChanged = false;

		private bool m_enabledTreeNodeDblClick = false;

		private bool m_enabledTreeNodeExpanded = false;

		private TimGridView m_curGrid = null;

		private TimPagingBar m_curPagingBar = null;

		private string[] m_dataKeyNames;

		public TTreeGrid CurMaster
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

		public int TreeWidth
		{
			get
			{
				return this.m_treeWidth;
			}
			set
			{
				this.m_treeWidth = value;
			}
		}

		public Hashtable TreeImgList
		{
			get
			{
				return this.m_treeImgList;
			}
			set
			{
				this.m_treeImgList = value;
			}
		}

		public bool EnabledSelectedTreeNodeChanged
		{
			get
			{
				return this.m_enabledSelectedTreeNodeChanged;
			}
			set
			{
				this.m_enabledSelectedTreeNodeChanged = value;
			}
		}

		public bool EnabledTreeNodeCheckChanged
		{
			get
			{
				return this.m_enabledTreeNodeCheckChanged;
			}
			set
			{
				this.m_enabledTreeNodeCheckChanged = value;
			}
		}

		public bool EnabledTreeNodeDblClick
		{
			get
			{
				return this.m_enabledTreeNodeDblClick;
			}
			set
			{
				this.m_enabledTreeNodeDblClick = value;
			}
		}

		public bool EnabledTreeNodeExpanded
		{
			get
			{
				return this.m_enabledTreeNodeExpanded;
			}
			set
			{
				this.m_enabledTreeNodeExpanded = value;
			}
		}

		public new TimGridView CurGrid
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

		public new TimPagingBar CurPagingBar
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

		public new string[] DataKeyNames
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
				this.m_dataKeyNames = keyList.ToArray();
				this.CurGrid.DataKeyNames = this.m_dataKeyNames;
			}
		}

		private void BuildEvent()
		{
			this.CurMaster.OnTreeNodeChanged += new TMasterBase.TreeNodeChanged(this.CurMaster_OnTreeNodeChanged);
			this.CurMaster.OnTreeNodePopulate += new TMasterBase.TreeNodePopulate(this.CurMaster_OnTreeNodePopulate);
			this.CurMaster.OnTreeNodeExpand += new TMasterBase.TreeNodeExpand(this.CurMaster_OnTreeNodeExpand);
			this.CurMaster.OnTreeNodeDragDrop += new TMasterBase.TreeNodeDragDrop(this.CurMaster_OnTreeNodeDragDrop);
			this.CurMaster.OnTreeNodeCheckChanged += new TMasterBase.TreeNodeCheckChanged(this.CurMaster_OnTreeNodeCheckChanged);
			this.CurMaster.OnTreeNodeCheckChanged += new TMasterBase.TreeNodeCheckChanged(this.CurMaster_OnTreeNodeCheckChanged);
			this.CurMaster.OnTreeNodeDblClick += new TMasterBase.TreeNodeDblClick(this.CurMaster_OnTreeNodeDblClick);
			this.CurMaster.OnDelete += new TMasterBase.Delete(this.CurMaster_OnDelete);
			this.CurMaster.OnQuery += new TMasterBase.Query(this.CurMaster_OnQuery);
			this.CurMaster.OnPrint += new TMasterBase.Print(this.CurMaster_OnPrint);
			this.CurMaster.OnPrintMenuItem += new TMasterBase.PrintMenuItem(this.CurMaster_OnPrintMenuItem);
			this.CurMaster.OnPreview += new TMasterBase.Preview(this.CurMaster_OnPreview);
			this.CurMaster.OnPreviewMenuItem += new TMasterBase.PreviewMenuItem(this.CurMaster_OnPreviewMenuItem);
			this.CurMaster.OnReportStyle += new TMasterBase.ReportStyle(this.CurMaster_OnReportStyle);
			this.CurMaster.OnAttach += new TMasterBase.Attach(this.CurMaster_OnAttach);
			this.CurMaster.OnAttachMenuItem += new TMasterBase.AttachMenuItem(this.CurMaster_OnAttachMenuItem);
			this.CurPagingBar.PageChanging += new PageChangingEventHandler(this.CurPagingBar_PageChanging);
			this.CurPagingBar.PageChanged += new EventHandler(this.CurPagingBar_PageChanged);
			this.CurGrid.RowDataBound += new GridViewRowEventHandler(this.CurGrid_RowDataBound);
			this.CurGrid.RowDataBound += new GridViewRowEventHandler(this.CurGrid_RowDataBound_Templet);
		}

		public TimTreeViewNode TreeAddNode(TimTreeViewNode fatherNode, string text, string value, string imgKey, bool selectAction, bool populateOnDemand, bool needExpandChildrenNode)
		{
			bool flag = this.TreeImgList.Count <= 0 || string.IsNullOrWhiteSpace(imgKey) || !this.TreeImgList.ContainsKey(imgKey);
			TimTreeViewNode addNode;
			if (flag)
			{
				addNode = new TimTreeViewNode(text, value);
			}
			else
			{
				addNode = new TimTreeViewNode(text, value, this.TreeImgList[imgKey].ToString());
			}
			addNode.ToolTip = text;
			addNode.PopulateOnDemand = populateOnDemand;
			bool flag2 = this.CurMaster.LeftTreeV.Nodes.Count <= 0;
			if (flag2)
			{
				this.CurMaster.LeftTreeV.SelectedNode = addNode;
			}
			bool flag3 = fatherNode == null;
			if (flag3)
			{
				this.CurMaster.LeftTreeV.Nodes.Add(addNode);
			}
			else
			{
				fatherNode.ChildNodes.Add(addNode);
			}
			bool flag4 = !selectAction;
			if (flag4)
			{
				addNode.SelectAction = TreeNodeSelectAction.None;
			}
			if (needExpandChildrenNode)
			{
			}
			return addNode;
		}

		protected void OnTreeNodePreChanged(object sender, TimTreeViewNodeEventArgs e)
		{
		}

		private void CurMaster_OnTreeNodeChanged(object sender, TimTreeViewNodeEventArgs e)
		{
			this.CurPagingBar.CurrentPageIndex = 1;
			this.CurGrid.SelectedIndex = -1;
			this.OnQuery();
			this.GridDataBind();
			this.PlaceUpdateTree();
			this.PlaceUpdateMenu();
			this.PlaceUpdateContent();
		}

		public virtual void CurMaster_OnTreeNodePopulate(object sender, TimTreeViewNodeEventArgs e)
		{
		}

		public virtual void CurMaster_OnTreeNodeExpand(object sender, TimTreeViewNodeEventArgs e)
		{
		}

		public virtual void CurMaster_OnTreeNodeDragDrop(object sender, TimTreeViewDragEventArgs e)
		{
		}

		public virtual void CurMaster_OnTreeNodeCheckChanged(object sender, TimTreeViewNodeEventArgs e)
		{
		}

		public virtual void CurMaster_OnTreeNodeDblClick(object sender, TimTreeViewNodeEventArgs e)
		{
		}

		protected virtual void CurGrid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
		}

		private void CurGrid_RowDataBound_Templet(object sender, GridViewRowEventArgs e)
		{
			bool flag = e.Row.RowType == DataControlRowType.DataRow;
			if (flag)
			{
				bool flag2 = string.IsNullOrWhiteSpace(base.PageUrlParam.UrlPath);
				if (flag2)
				{
					e.Row.Attributes.Add("onclick", e.Row.Attributes["onclick"] + "SetPageUrlParam(this,'" + base.PageUrlParam.EncodedParameters + "');");
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
						"');"
					}));
				}
				base.PageUrlParam = new PageParameter();
			}
		}

		private void CurPagingBar_PageChanged(object sender, EventArgs e)
		{
			this.CurGrid.SelectedIndex = -1;
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
			base.ReportParse.PreparePrintOrPreview(base.PageAmIdPlusClassName, itemValue, "Preview");
			base.RegisterScript("PreviewMenuItem", "window.setTimeout('SetReport()', 1);function SetReport() {" + base.ReportParse.run + "}");
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
						base.CurEntity.PageSize = this.CurPagingBar.PageSize;
						base.CurEntity.PageIndex = this.CurPagingBar.CurrentPageIndex - 1;
						base.CurEntity.QueryRecordSet();
						bool allowPaging = this.CurGrid.AllowPaging;
						if (allowPaging)
						{
							this.CurPagingBar.RecordCount = base.CurEntity.RecordCount;
							this.CurGrid.PageSize = this.CurPagingBar.PageSize;
							this.CurGrid.PageIndex = this.CurPagingBar.CurrentPageIndex - 1;
						}
						else
						{
							this.CurPagingBar.RecordCount = base.CurEntity.RecordCount;
							this.CurPagingBar.PageSize = base.CurEntity.RecordCount;
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
				this.CurPagingBar.CurrentPageIndex = 1;
			}
			this.CurGrid.SelectedIndex = -1;
			bool flag2 = this.RebuildTree();
			if (flag2)
			{
				this.PlaceUpdateTree();
			}
			bool flag3 = !this.OnQuery();
			if (!flag3)
			{
				this.GridDataBind();
				this.PlaceUpdateContent();
			}
		}

		protected void GridDataBind()
		{
			this.CurGrid.DataSource = base.CurEntity.RecordSet;
			this.CurGrid.DataBind();
		}

		protected internal override void RecoverData()
		{
			bool flag = base.CurEntity != null && base.CurEntity.Entity.ModifyControl;
			if (flag)
			{
				base.ModifierId = this.CurGrid.DataKeys[this.CurGrid.SelectedIndex]["MODIFIERID"].ToString().Trim();
			}
			base.CurEntity.RecordSet.Columns.Clear();
			string[] dataKeyNames = this.DataKeyNames;
			for (int i = 0; i < dataKeyNames.Length; i++)
			{
				string keyName = dataKeyNames[i];
				base.CurEntity.RecordSet.Columns.Add(keyName);
			}
			DataRow row = base.CurEntity.RecordSet.NewRow();
			string[] dataKeyNames2 = this.DataKeyNames;
			for (int j = 0; j < dataKeyNames2.Length; j++)
			{
				string keyName2 = dataKeyNames2[j];
				bool flag2 = base.CurEntity.Fields.ContainsKey(keyName2) && base.CurEntity.Fields[keyName2].Key;
				if (flag2)
				{
					row[keyName2] = this.CurGrid.DataKeys[this.CurGrid.SelectedIndex][keyName2].ToString();
				}
				else
				{
					row[keyName2] = this.CurGrid.DataKeys[this.CurGrid.SelectedIndex][keyName2].ToString().Trim();
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
			bool flag6 = result;
			if (flag6)
			{
				result = this.OnDeleteComplete();
			}
			return result;
		}

		private void CurMaster_OnDelete(object sender, EventArgs e)
		{
			bool result = false;
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
				this.CurGrid.SelectedIndex = -1;
				this.PlaceUpdateContent();
			}
		}

		private void SetCtrlStatus()
		{
			this.CurMaster.SetCtrlStatus();
		}

		protected sealed override void OnInit()
		{
			this.CurMaster.CurGrid = this.CurGrid;
			this.CurPagingBar = this.CurMaster.CurPagingBar;
			this.BuildEvent();
			this.CurGrid.OnClientDblClick = "OpenPage('VIEW');return false;";
			this.TreeImgList.Add("TreeFolder", "../../Images/Tim/folder.gif");
			this.TreeImgList.Add("TreeFolderOpen", "../../Images/Tim/folderopen.gif");
			this.TreeImgList.Add("TreeLeaf", "../../Images/Tim/treeleaf.gif");
			this.TreeImgList.Add("MailUnreadBox", "../../Images/Tim/mail/MailUnreadBox.gif");
			this.TreeImgList.Add("MailInBox", "../../Images/Tim/mail/MailInBox.gif");
			this.TreeImgList.Add("MailSendBox", "../../Images/Tim/mail/MailSendBox.gif");
			this.TreeImgList.Add("MailDraftBox", "../../Images/Tim/mail/MailDraftBox.gif");
			this.TreeImgList.Add("MailTempletBox", "../../Images/Tim/mail/MailTempletBox.gif");
			this.TreeImgList.Add("MailTrashBox", "../../Images/Tim/mail/MailTrashBox.gif");
			this.TreeImgList.Add("MailUserBox", "../../Images/Tim/mail/MailUserBox.gif");
			this.CurMaster.LeftTreeV.InitEvents(this.EnabledSelectedTreeNodeChanged, this.EnabledTreeNodeExpanded, this.EnabledTreeNodeCheckChanged, this.EnabledTreeNodeDblClick);
		}

		protected override void OnLoad()
		{
			bool flag = !base.IsPostBack;
			if (flag)
			{
				this.OnPreLoadRecord();
				this.OnInitTree();
				this.OnLoadRecord();
				this.OnLoadRecordComplete();
			}
		}

		protected sealed override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);
			this.OnLoadComplete();
			this.CurMaster.BtnCopy.OnClientClick = (this.CurMaster.BtnView.OnClientClick = (this.CurMaster.BtnEdit.OnClientClick = (this.CurMaster.BtnDelete.OnClientClick = string.Concat(new string[]
			{
				"if ($('#",
				this.CurGrid.ClientID,
				"') == null) {alert('当前没有可选记录！'); return false;} if ($('#",
				this.CurGrid.ClientID,
				"').attr('selectedIndex') == -1) {alert('请选择处理记录！'); return false;}"
			}))));
			TimButtonMenu expr_A7 = this.CurMaster.BtnInsert;
			expr_A7.OnClientClick += " OpenPage('INSERT');return false;";
			TimButtonMenu expr_C8 = this.CurMaster.BtnCopy;
			expr_C8.OnClientClick += " OpenPage('COPY');return false;";
			TimButtonMenu expr_E9 = this.CurMaster.BtnView;
			expr_E9.OnClientClick += " OpenPage('VIEW');return false;";
			TimButtonMenu expr_10A = this.CurMaster.BtnEdit;
			expr_10A.OnClientClick += " OpenPage('EDIT');return false;";
			TimButtonMenu expr_12B = this.CurMaster.BtnDelete;
			expr_12B.OnClientClick += "if (confirm('您确定要删除所选记录？') == false) return false;";
			base.SetMasterCtrlState();
			this.SetPageCtrlState();
			bool flag = !base.IsPostBack;
			if (flag)
			{
				string moduleClientVar = string.Empty;
				moduleClientVar = moduleClientVar + " var _EditingPage= '" + base.EditingPage + "';";
				moduleClientVar = moduleClientVar + " var _GvClientId= '" + this.CurGrid.ClientID + "';";
				base.RegisterScript("TempletClientVar", moduleClientVar);
			}
		}

		internal override void SetTempletCtrlState()
		{
			this.CurMaster.BtnInsert.Visible = (this.CurMaster.BtnCopy.Visible = base.CanInsertTemplet());
			this.CurMaster.BtnPrint.Visible = (this.CurMaster.BtnPreview.Visible = (this.CurMaster.BtnPrint.Enabled = (this.CurMaster.BtnPreview.Enabled = base.CanPrintTemplet())));
			this.CurMaster.BtnReportStyle.Visible = (this.CurMaster.BtnReportStyle.Enabled = base.CanDesignTemplet());
		}

		private void SetPageCtrlState()
		{
			this.SetCtrlState();
			this.SetPageUrlAppendParam();
			base.RegisterScript("TempletClientAppendVar", "var _PageUrlAppendParam = '" + base.PageUrlAppendParam.CombineUrl(false) + "';");
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

		protected void PlaceUpdateTree()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpTreePlace.Update();
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
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
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
