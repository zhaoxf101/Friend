using System;
using TIM.T_CHART;
using TIM.T_TEMPLET.Master;

namespace TIM.T_TEMPLET.Page
{
	public class TChartBase : PageBase
	{
		private string _chartJs = string.Empty;

		private TChart m_curMaster = null;

		public TChart CurMaster
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
						base.CurEntity.QueryRecordSet();
						bool flag4 = !this.OnQueryComplete();
						result2 = (!flag4 || result);
					}
				}
			}
			return result2;
		}

		protected void CurMaster_OnQuery(object sender, EventArgs e)
		{
			bool flag = !this.OnQuery();
			if (!flag)
			{
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
				base.RegisterScript("DrawSelfChart", "ChartAdjust();");
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
			this.BuildEvent();
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
				base.RegisterScript("ChartAdjust", "ChartAdjust();");
			}
		}

		protected void PlaceUpdateContent()
		{
			bool flag = base.IsPostBack && !base.BeExportExcel;
			if (flag)
			{
				this.CurMaster.UpContentPlace.Update();
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
