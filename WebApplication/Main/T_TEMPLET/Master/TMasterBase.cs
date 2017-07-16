using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.UI;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Master
{
	public class TMasterBase : MasterPage
	{
		public delegate void Ok(object sender, EventArgs e);

		public delegate void Close(object sender, EventArgs e);

		public delegate void Insert(object sender, EventArgs e);

		public delegate void Copy(object sender, EventArgs e);

		public delegate void View(object sender, EventArgs e);

		public delegate void Edit(object sender, EventArgs e);

		public delegate void Save(object sender, EventArgs e);

		public delegate void Cancel(object sender, EventArgs e);

		public delegate void Delete(object sender, EventArgs e);

		public delegate void Query(object sender, EventArgs e);

		public delegate void Print(object sender, EventArgs e);

		public delegate void PrintMenuItem(object sender, string itemValue);

		public delegate void Preview(object sender, EventArgs e);

		public delegate void PreviewMenuItem(object sender, string itemValue);

		public delegate void ReportStyle(object sender, EventArgs e);

		public delegate void Attach(object sender, EventArgs e);

		public delegate void AttachMenuItem(object sender, string itemValue);

		public delegate void FlowBlock(string workflowAction, string nextWfId, string nextWfpId, string todo, string opinion);

		public delegate void DirectSubmit(object sender, EventArgs e);

		public delegate void Submit(object sender, EventArgs e);

		public delegate void Back(object sender, EventArgs e);

		public delegate void Withdraw(object sender, EventArgs e);

		public delegate void DeliverTo(object sender, EventArgs e);

		public delegate void Veto(object sender, EventArgs e);

		public delegate void WorkflowTrace(object sender, EventArgs e);

		public delegate void WorkflowButtonDropDown(object sender, EventArgs e);

		public delegate void SlaveInsert(object sender, EventArgs e);

		public delegate void SlaveCopy(object sender, EventArgs e);

		public delegate void SlaveView(object sender, EventArgs e);

		public delegate void SlaveEdit(object sender, EventArgs e);

		public delegate void SlaveDelete(object sender, EventArgs e);

		public delegate void SlaveUpdate(object sender, EventArgs e);

		public delegate void TreeNodeChanged(object sender, TimTreeViewNodeEventArgs e);

		public delegate void TreeNodePopulate(object sender, TimTreeViewNodeEventArgs e);

		public delegate void TreeNodeExpand(object sender, TimTreeViewNodeEventArgs e);

		public delegate void TreeNodeDragDrop(object sender, TimTreeViewDragEventArgs e);

		public delegate void TreeNodeCheckChanged(object sender, TimTreeViewNodeEventArgs e);

		public delegate void TreeNodeDblClick(object sender, TimTreeViewNodeEventArgs e);

		internal Maintenance _Maintenance = null;

		internal TimButtonMenu TempletWorkflowButton;

		internal TimButtonMenu TempletPreviewButton;

		internal TimButtonMenu TempletPrintButton;

		internal TimButtonMenu TempletReportStyleButton;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Ok OnOk;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Close OnClose;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Insert OnInsert;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Copy OnCopy;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.View OnView;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Edit OnEdit;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Save OnSave;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Cancel OnCancel;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Delete OnDelete;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Query OnQuery;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Print OnPrint;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.PrintMenuItem OnPrintMenuItem;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Preview OnPreview;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.PreviewMenuItem OnPreviewMenuItem;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.ReportStyle OnReportStyle;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Attach OnAttach;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.AttachMenuItem OnAttachMenuItem;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.FlowBlock OnFlowBlock;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.DirectSubmit OnDirectSubmit;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Submit OnSubmit;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Back OnBack;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Withdraw OnWithdraw;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.DeliverTo OnDeliverTo;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.Veto OnVeto;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.WorkflowTrace OnWorkflowTrace;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.WorkflowButtonDropDown OnWorkflowButtonDropDown;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.SlaveInsert OnSlaveInsert;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.SlaveCopy OnSlaveCopy;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.SlaveView OnSlaveView;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.SlaveInsert OnSlaveEdit;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.SlaveDelete OnSlaveDelete;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.SlaveUpdate OnSlaveUpdate;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.TreeNodeChanged OnTreeNodeChanged;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.TreeNodePopulate OnTreeNodePopulate;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.TreeNodeExpand OnTreeNodeExpand;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.TreeNodeDragDrop OnTreeNodeDragDrop;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.TreeNodeCheckChanged OnTreeNodeCheckChanged;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event TMasterBase.TreeNodeDblClick OnTreeNodeDblClick;

		internal bool DropDownPermission
		{
			get;
			set;
		}

		internal bool SubmitPermission
		{
			get;
			set;
		}

		internal bool BackPermission
		{
			get;
			set;
		}

		internal bool WithdrawPermission
		{
			get;
			set;
		}

		internal bool DeliverToPermission
		{
			get;
			set;
		}

		internal bool VetoPermission
		{
			get;
			set;
		}

		internal bool TracePermission
		{
			get;
			set;
		}

		protected virtual void OnInit()
		{
		}

		protected override void OnInit(EventArgs e)
		{
			this.OnInit();
			base.OnInit(e);
			bool flag = base.Master is Maintenance;
			if (flag)
			{
				this._Maintenance = (base.Master as Maintenance);
				this._Maintenance.BtnFlowBlock.Click += new EventHandler(this.BtnFlowBlock_Click);
			}
		}

		protected void btnOk_Click(object sender, EventArgs e)
		{
			bool flag = this.OnOk != null;
			if (flag)
			{
				this.OnOk(sender, e);
			}
		}

		protected void btnClose_Click(object sender, EventArgs e)
		{
			bool flag = this.OnClose != null;
			if (flag)
			{
				this.OnClose(sender, e);
			}
		}

		protected void btnInsert_Click(object sender, EventArgs e)
		{
			bool flag = this.OnInsert != null;
			if (flag)
			{
				this.OnInsert(sender, e);
			}
		}

		protected void btnCopy_Click(object sender, EventArgs e)
		{
			bool flag = this.OnCopy != null;
			if (flag)
			{
				this.OnCopy(sender, e);
			}
		}

		protected void btnView_Click(object sender, EventArgs e)
		{
			bool flag = this.OnView != null;
			if (flag)
			{
				this.OnView(sender, e);
			}
		}

		protected void btnEdit_Click(object sender, EventArgs e)
		{
			bool flag = this.OnEdit != null;
			if (flag)
			{
				this.OnEdit(sender, e);
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			bool flag = this.OnSave != null;
			if (flag)
			{
				this.OnSave(sender, e);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			bool flag = this.OnCancel != null;
			if (flag)
			{
				this.OnCancel(sender, e);
			}
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			bool flag = this.OnDelete != null;
			if (flag)
			{
				this.OnDelete(sender, e);
			}
		}

		protected void btnQuery_Click(object sender, EventArgs e)
		{
			bool flag = this.OnQuery != null;
			if (flag)
			{
				this.OnQuery(sender, e);
			}
		}

		protected void btnPrint_Click(object sender, EventArgs e)
		{
			bool flag = this.OnPrint != null;
			if (flag)
			{
				this.OnPrint(sender, e);
			}
		}

		protected void btnPrint_MenuItemClick(object sender, string itemValue)
		{
			bool flag = this.OnPrintMenuItem != null;
			if (flag)
			{
				this.OnPrintMenuItem(sender, itemValue);
			}
		}

		protected void btnPreview_Click(object sender, EventArgs e)
		{
			bool flag = this.OnPreview != null;
			if (flag)
			{
				this.OnPreview(sender, e);
			}
		}

		protected void btnPreview_MenuItemClick(object sender, string itemValue)
		{
			bool flag = this.OnPreviewMenuItem != null;
			if (flag)
			{
				this.OnPreviewMenuItem(sender, itemValue);
			}
		}

		protected void btnReportStyle_Click(object sender, EventArgs e)
		{
			bool flag = this.OnReportStyle != null;
			if (flag)
			{
				this.OnReportStyle(sender, e);
			}
		}

		protected void btnAttach_Click(object sender, EventArgs e)
		{
			bool flag = this.OnAttach != null;
			if (flag)
			{
				this.OnAttach(sender, e);
			}
		}

		protected void btnAttach_MenuItemClick(object sender, string itemValue)
		{
			bool flag = this.OnAttachMenuItem != null;
			if (flag)
			{
				this.OnAttachMenuItem(sender, itemValue);
			}
		}

		internal void SetWorkflowButtonPermission()
		{
			this.TempletWorkflowButton.Enabled = (this.DropDownPermission || this.SubmitPermission || this.BackPermission || this.WithdrawPermission || this.DeliverToPermission || this.VetoPermission || this.TracePermission);
			this.TempletWorkflowButton.Items[0].Enabled = this.SubmitPermission;
			this.TempletWorkflowButton.Items[1].Enabled = this.BackPermission;
			this.TempletWorkflowButton.Items[2].Enabled = this.WithdrawPermission;
			this.TempletWorkflowButton.Items[3].Enabled = this.DeliverToPermission;
			this.TempletWorkflowButton.Items[4].Enabled = this.VetoPermission;
			this.TempletWorkflowButton.Items[5].Enabled = this.TracePermission;
		}

		internal void BtnFlowBlock_Click(object sender, EventArgs e)
		{
			this.OnFlowBlock(this._Maintenance.TempletAction.Value, this._Maintenance.TempletNextWfId.Value, this._Maintenance.TempletNextWfpId.Value, this._Maintenance.TempletTodo.Value, this._Maintenance.TempletOpinion.Value);
		}

		protected void btnWorkflow_Click(object sender, EventArgs e)
		{
			bool flag = this.OnDirectSubmit != null;
			if (flag)
			{
				this.OnDirectSubmit(sender, e);
			}
		}

		protected void btnWorkflow_MenuItemClick(object sender, string itemValue)
		{
			EventArgs eventArgs = null;
			if (!(itemValue == "0"))
			{
				if (!(itemValue == "1"))
				{
					if (!(itemValue == "2"))
					{
						if (!(itemValue == "3"))
						{
							if (!(itemValue == "4"))
							{
								if (itemValue == "5")
								{
									bool flag = this.OnWorkflowTrace != null;
									if (flag)
									{
										this.OnWorkflowTrace(sender, eventArgs);
									}
								}
							}
							else
							{
								bool flag2 = this.OnVeto != null;
								if (flag2)
								{
									this.OnVeto(sender, eventArgs);
								}
							}
						}
						else
						{
							bool flag3 = this.OnDeliverTo != null;
							if (flag3)
							{
								this.OnDeliverTo(sender, eventArgs);
							}
						}
					}
					else
					{
						bool flag4 = this.OnWithdraw != null;
						if (flag4)
						{
							this.OnWithdraw(sender, eventArgs);
						}
					}
				}
				else
				{
					bool flag5 = this.OnBack != null;
					if (flag5)
					{
						this.OnBack(sender, eventArgs);
					}
				}
			}
			else
			{
				bool flag6 = this.OnSubmit != null;
				if (flag6)
				{
					this.OnSubmit(sender, eventArgs);
				}
			}
		}

		protected void btnWorkflow_DropDown(object sender, EventArgs e)
		{
			bool flag = this.OnWorkflowButtonDropDown != null;
			if (flag)
			{
				this.OnWorkflowButtonDropDown(sender, e);
			}
		}

		protected void btnSlaveInsert_Click(object sender, EventArgs e)
		{
			bool flag = this.OnSlaveInsert != null;
			if (flag)
			{
				this.OnSlaveInsert(sender, e);
			}
		}

		protected void btnSlaveCopy_Click(object sender, EventArgs e)
		{
			bool flag = this.OnSlaveCopy != null;
			if (flag)
			{
				this.OnSlaveCopy(sender, e);
			}
		}

		protected void btnSlaveView_Click(object sender, EventArgs e)
		{
			bool flag = this.OnSlaveView != null;
			if (flag)
			{
				this.OnSlaveView(sender, e);
			}
		}

		protected void btnSlaveEdit_Click(object sender, EventArgs e)
		{
			bool flag = this.OnSlaveEdit != null;
			if (flag)
			{
				this.OnSlaveEdit(sender, e);
			}
		}

		protected void btnSlaveDelete_Click(object sender, EventArgs e)
		{
			bool flag = this.OnSlaveDelete != null;
			if (flag)
			{
				this.OnSlaveDelete(sender, e);
			}
		}

		protected void btnSlaveUpdate_Click(object sender, EventArgs e)
		{
			bool flag = this.OnSlaveUpdate != null;
			if (flag)
			{
				this.OnSlaveUpdate(sender, e);
			}
		}

		protected void LeftTree_SelectedNodeChanged(object sender, TimTreeViewNodeEventArgs e)
		{
			this.OnTreeNodeChanged(sender, e);
		}

		protected void LeftTree_TreeNodePopulate(object sender, TimTreeViewNodeEventArgs e)
		{
			this.OnTreeNodePopulate(sender, e);
		}

		protected void LeftTree_TreeNodeExpand(object sender, TimTreeViewNodeEventArgs e)
		{
			this.OnTreeNodeExpand(sender, e);
		}

		protected void LeftTree_DragDrop(object sender, TimTreeViewDragEventArgs e)
		{
			this.OnTreeNodeDragDrop(sender, e);
		}

		protected void LeftTree_CheckChanged(object sender, TimTreeViewNodeEventArgs e)
		{
			this.OnTreeNodeCheckChanged(sender, e);
		}

		protected void LeftTree_DblClick(object sender, TimTreeViewNodeEventArgs e)
		{
			this.OnTreeNodeDblClick(sender, e);
		}

		protected void GridPagingBar_PageChanging(object src, PageChangingEventArgs e)
		{
		}

		protected void GridPagingBar_PageChanged(object sender, EventArgs e)
		{
		}

		internal void SetCtrlStatus()
		{
		}

		public virtual void SetMenu_HideAllStdBtn()
		{
		}

		public virtual void SetMenu_OnlyQuery()
		{
		}

		public virtual void SetMenu_OnlyQueryAndReport()
		{
		}

		public virtual void SetMenu_OnlyViewEdit()
		{
		}

		public virtual void SetMenu_OnlyAttach()
		{
		}
	}
}
