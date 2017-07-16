using System;
using System.Web.UI.WebControls;
using TIM.T_CHART;
using TIM.T_TEMPLET.Master;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Page
{
	public class TChartListBase : PageBase
	{
		private string _chartJs = string.Empty;

		private TChartList m_curMaster = null;

		public TChartList CurMaster
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
			this.CurMaster.OnQuery += new TMasterBase.Query(this.CurMaster_OnQuery);
			this.CurMaster.OnPrint += new TMasterBase.Print(this.CurMaster_OnPrint);
			this.CurMaster.OnPrintMenuItem += new TMasterBase.PrintMenuItem(this.CurMaster_OnPrintMenuItem);
			this.CurMaster.OnPreview += new TMasterBase.Preview(this.CurMaster_OnPreview);
			this.CurMaster.OnPreviewMenuItem += new TMasterBase.PreviewMenuItem(this.CurMaster_OnPreviewMenuItem);
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
			this.PlaceUpdateChart();
			this.PlaceUpdateContent();
		}

		private void CurPagingBar_PageChanging(object src, PageChangingEventArgs e)
		{
		}

		private void CurMaster_OnPreviewMenuItem(object sender, string itemValue)
		{
		}

		private void CurMaster_OnPreview(object sender, EventArgs e)
		{
		}

		private void CurMaster_OnPrintMenuItem(object sender, string itemValue)
		{
		}

		private void CurMaster_OnPrint(object sender, EventArgs e)
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
				this.RegisterGenChartScript();
				this.PlaceUpdateChart();
				this.PlaceUpdateContent();
			}
		}

		private void RegisterGenChartScript()
		{
			TimCharts chart = this.ChartDataBind();
			bool flag = chart == null;
			if (!flag)
			{
				chart.InContainerName("chart_container").InFunction("DrawSelfChart");
				base.RegisterScript("DrawSelfChartFunc", chart.ChartScriptHtmlString().ToString(), false);
				base.RegisterScript("DrawSelfChart", "DrawSelfChart();");
			}
		}

		protected virtual TimCharts ChartDataBind()
		{
			return null;
		}

		protected void GridDataBind()
		{
			base.CurGrid.DataSource = base.CurEntity.RecordSet;
			base.CurGrid.DataBind();
		}

		private void SetCtrlStatus()
		{
			this.CurMaster.SetCtrlStatus();
		}

		protected sealed override void OnInit()
		{
			this.CurMaster.CurGrid = base.CurGrid;
			base.CurPagingBar = this.CurMaster.CurPagingBar;
			this.BuildEvent();
			base.CurGrid.OnClientDblClick = "return false;";
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
			this.CurMaster.BtnPrint.Visible = (this.CurMaster.BtnPreview.Visible = ((!this.CanPrint()) ? false : false));
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

		protected void PlaceUpdateChart()
		{
			bool flag = base.IsPostBack && !base.BeExportExcel;
			if (flag)
			{
				this.CurMaster.UpChartPlace.Update();
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
