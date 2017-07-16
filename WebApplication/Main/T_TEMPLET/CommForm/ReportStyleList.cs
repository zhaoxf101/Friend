using System;
using System.Web.UI.WebControls;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Page;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.CommForm
{
	public class ReportStyleList : TListingBase
	{
		private ReportStyle _MasterEntity = new ReportStyle();

		protected TimButtonMenu btnDefault;

		protected TimButtonMenu btnPublic;

		protected TimButtonMenu btnStyle;

		protected TimLabel lblQueryStyleName;

		protected TimTextBox txtQueryStyleName;

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
			base.MdId = 101021004;
			base.MdName = "样式定义";
			base.Width = 900;
			base.Height = 600;
		}

		protected override void InitTemplet()
		{
			base.CurMaster = this.Master;
			base.CurEntity = this._MasterEntity;
			base.CurGrid = this.gvMaster;
			base.EditingPage = "ReportStyleInfo.aspx";
			base.DataKeyNames = new string[]
			{
				"REPORTSTYLE_STYLEID",
				"REPORTSTYLE_STYLENAME",
				"REPORTSTYLE_ORDER",
				"REPORTSTYLE_DEFAULT",
				"REPORTSTYLE_PUBLIC",
				"REPORTSTYLE_VERSION"
			};
		}

		protected override void InitCtrlValue()
		{
		}

		protected override void SetCtrlState()
		{
		}

		internal override void SwitchPagePermission()
		{
			this.btnDefault.Visible = (this.btnPublic.Visible = (this.btnStyle.Visible = (base.PagePermission.Insert = (base.PagePermission.View = (base.PagePermission.Edit = (base.PagePermission.Delete = (base.UserId == "ADMIN")))))));
		}

		protected override void SetPageUrlAppendParam()
		{
			base.PageUrlAppendParam.AddString("STYLEID", base.PageParam.GetString("STYLEID"));
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected override bool OnPreQuery()
		{
			this._MasterEntity.QueryStyleId = base.PageParam.GetString("STYLEID");
			this._MasterEntity.QueryStyleName = this.txtQueryStyleName.Text;
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

		protected override void OnInitComplete()
		{
			this.btnDefault.OnClientClick = (this.btnPublic.OnClientClick = (this.btnStyle.OnClientClick = string.Concat(new string[]
			{
				"if ($('#",
				base.CurGrid.ClientID,
				"') == null) {alert('当前没有可选记录！'); return false;} if ($('#",
				base.CurGrid.ClientID,
				"').attr('selectedIndex') == -1) {alert('请选择处理记录！'); return false;}"
			})));
		}

		protected override void CurGrid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			bool flag = e.Row.RowType == DataControlRowType.DataRow;
			if (flag)
			{
				base.PageUrlParam.AddString("STYLEID", this.gvMaster.DataKeys[e.Row.RowIndex]["REPORTSTYLE_STYLEID"].ToString().Trim());
				base.PageUrlParam.AddString("STYLEORDER", this.gvMaster.DataKeys[e.Row.RowIndex]["REPORTSTYLE_ORDER"].ToString().Trim());
				base.PageUrlParam.AddExtString("REPORTSTYLE", base.PageParam.GetExtRawString("REPORTSTYLE"));
				base.PageUrlParam.SaveParams();
			}
		}

		protected void btnDefault_Click(object sender, EventArgs e)
		{
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Clear();
			hsql.Add("UPDATE REPORTSTYLE SET REPORTSTYLE_DEFAULT = 'N' WHERE REPORTSTYLE_STYLEID = :REPORTSTYLE_STYLEID");
			hsql.ParamByName("REPORTSTYLE_STYLEID").Value = this.gvMaster.DataKeys[this.gvMaster.SelectedIndex]["REPORTSTYLE_STYLEID"].ToString().Trim();
			db.ExecSQL(hsql);
			hsql.Clear();
			hsql.Add("UPDATE REPORTSTYLE SET REPORTSTYLE_DEFAULT = 'Y' WHERE REPORTSTYLE_STYLEID = :REPORTSTYLE_STYLEID AND REPORTSTYLE_ORDER = :REPORTSTYLE_ORDER");
			hsql.ParamByName("REPORTSTYLE_STYLEID").Value = this.gvMaster.DataKeys[this.gvMaster.SelectedIndex]["REPORTSTYLE_STYLEID"].ToString().Trim();
			hsql.ParamByName("REPORTSTYLE_ORDER").Value = this.gvMaster.DataKeys[this.gvMaster.SelectedIndex]["REPORTSTYLE_ORDER"].ToString().Trim();
			db.ExecSQL(hsql);
			base.CurMaster_OnQuery(null, null);
		}

		protected void btnPublic_Click(object sender, EventArgs e)
		{
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Clear();
			bool flag = this.gvMaster.DataKeys[this.gvMaster.SelectedIndex]["REPORTSTYLE_PUBLIC"].ToString().Trim() == "Y";
			if (flag)
			{
				hsql.Add("UPDATE REPORTSTYLE SET REPORTSTYLE_PUBLIC = 'N' WHERE REPORTSTYLE_STYLEID = :REPORTSTYLE_STYLEID AND REPORTSTYLE_ORDER = :REPORTSTYLE_ORDER");
			}
			else
			{
				hsql.Add("UPDATE REPORTSTYLE SET REPORTSTYLE_PUBLIC = 'Y' WHERE REPORTSTYLE_STYLEID = :REPORTSTYLE_STYLEID AND REPORTSTYLE_ORDER = :REPORTSTYLE_ORDER");
			}
			hsql.ParamByName("REPORTSTYLE_STYLEID").Value = this.gvMaster.DataKeys[this.gvMaster.SelectedIndex]["REPORTSTYLE_STYLEID"].ToString().Trim();
			hsql.ParamByName("REPORTSTYLE_ORDER").Value = this.gvMaster.DataKeys[this.gvMaster.SelectedIndex]["REPORTSTYLE_ORDER"].ToString().Trim();
			db.ExecSQL(hsql);
			base.CurMaster_OnQuery(null, null);
		}

		protected void btnStyle_Click(object sender, EventArgs e)
		{
			PageParameter pageParams = new PageParameter();
			pageParams.Width = 0;
			pageParams.Height = 0;
			pageParams.UrlPath = this.Page.ResolveUrl("~") + "T_TEMPLET/CommForm/StyleDesignInfo.aspx";
			pageParams.AddString("STYLEID", this.gvMaster.DataKeys[this.gvMaster.SelectedIndex]["REPORTSTYLE_STYLEID"].ToString().Trim());
			pageParams.AddString("STYLEORDER", this.gvMaster.DataKeys[this.gvMaster.SelectedIndex]["REPORTSTYLE_ORDER"].ToString().Trim());
			pageParams.AddExtString("REPORTSTYLE", base.PageParam.GetExtRawString("REPORTSTYLE"));
			base.OpenDialog(pageParams);
		}
	}
}
