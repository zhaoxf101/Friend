using System;
using System.Web.UI.WebControls;
using TIM.T_TEMPLET.Master;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Page
{
	public class TCodeHelperBase : PageBase
	{
		private TCodeHelper m_curMaster = null;

		private TimGridView m_curGrid = null;

		private TimPagingBar m_curPagingBar = null;

		private string[] m_dataKeyNames;

		public TCodeHelper CurMaster
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
				this.CurGrid.DataKeyNames = value;
			}
		}

		private void BuildEvent()
		{
			this.CurMaster.OnOk += new TMasterBase.Ok(this.CurMaster_OnOk);
			this.CurMaster.OnClose += new TMasterBase.Close(this.CurMaster_OnClose);
			this.CurMaster.OnQuery += new TMasterBase.Query(this.CurMaster_OnQuery);
			this.CurPagingBar.PageChanging += new PageChangingEventHandler(this.CurPagingBar_PageChanging);
			this.CurPagingBar.PageChanged += new EventHandler(this.CurPagingBar_PageChanged);
			this.CurGrid.RowDataBound += new GridViewRowEventHandler(this.CurGrid_RowDataBound);
			this.CurGrid.RowDataBound += new GridViewRowEventHandler(this.CurGrid_RowDataBound_Templet);
		}

		private void CurMaster_OnClose(object sender, EventArgs e)
		{
			base.CloseDialog();
		}

		private void CurMaster_OnOk(object sender, EventArgs e)
		{
			base.RegisterScript("SetCodeValue", string.Format("AddDialogCallbackArgumentValue('{0}'); frameElement.dialog.close();", this.GetCallbackCtrlValue()));
		}

		protected virtual string GetCallbackCtrlValue()
		{
			return "";
		}

		protected virtual void CurGrid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
		}

		private void CurGrid_RowDataBound_Templet(object sender, GridViewRowEventArgs e)
		{
			bool flag = e.Row.RowType == DataControlRowType.DataRow;
			if (flag)
			{
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
			bool flag2 = !this.OnQuery();
			if (!flag2)
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

		private void SetCtrlStatus()
		{
			this.CurMaster.SetCtrlStatus();
		}

		protected sealed override void OnInit()
		{
			this.CurMaster.CurGrid = this.CurGrid;
			this.CurPagingBar = this.CurMaster.CurPagingBar;
			this.BuildEvent();
			this.CurGrid.OnClientDblClick = string.Concat(new string[]
			{
				"__doPostBack('",
				this.CurMaster.BtnOk.UniqueID,
				"','",
				this.CurMaster.BtnOk.UniqueID,
				"'); return false;"
			});
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
			this.CurMaster.BtnOk.OnClientClick = string.Concat(new string[]
			{
				"if ($('#",
				this.CurGrid.ClientID,
				"') == null) {alert('当前没有可选记录！'); return false;} if ($('#",
				this.CurGrid.ClientID,
				"').attr('selectedIndex') == -1) {alert('请选择处理记录！'); return false;}"
			});
			bool flag = !base.IsPostBack;
			if (flag)
			{
				string moduleClientVar = string.Empty;
				moduleClientVar = moduleClientVar + " var _EditingPage= '" + base.EditingPage + "';";
				moduleClientVar = moduleClientVar + " var _GvClientId= '" + this.CurGrid.ClientID + "';";
				base.RegisterScript("TempletClientVar", moduleClientVar);
			}
			base.SetMasterCtrlState();
			this.SetPageCtrlState();
		}

		internal override void SetTempletCtrlState()
		{
		}

		private void SetPageCtrlState()
		{
			this.SetCtrlState();
			this.SetPageUrlAppendParam();
			base.RegisterScript("TempletClientAppendVar", "var _PageUrlAppendParam = '" + base.PageUrlAppendParam.EncodedParameters + "';");
		}

		protected override void SetMenu_HideAllStdBtn()
		{
			this.CurMaster.SetMenu_HideAllStdBtn();
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
