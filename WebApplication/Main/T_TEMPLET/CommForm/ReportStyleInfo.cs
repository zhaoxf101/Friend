using System;
using System.Text;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Page;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.CommForm
{
	public class ReportStyleInfo : TEditingBase
	{
		private ReportStyle MasterEntity = new ReportStyle();

		protected TimLabel lblStyleId;

		protected TimTextBox txtStyleId;

		protected TimLabel lblStyleName;

		protected TimTextBox txtStyleName;

		protected TimLabel lblOrder;

		protected TimNumericTextBox ntOrder;

		protected TimLabel TimLabel1;

		protected TimCheckBox chkDefault;

		protected TimLabel TimLabel2;

		protected TimCheckBox chkPublic;

		protected TimLabel lblExecOn;

		protected TimTextBox txtExecOn;

		protected TimLabel lblModifier;

		protected TimTextBox txtModifier;

		protected TimLabel lblModifierTime;

		protected TimDateTime dtModifierTime;

		public new TEditing Master
		{
			get
			{
				return (TEditing)base.Master;
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
			base.CurEntity = this.MasterEntity;
		}

		protected override void InitCtrlValue()
		{
		}

		protected override void InitInsert()
		{
			this.txtStyleId.Text = base.PageParam.GetString("STYLEID");
			this.chkPublic.Checked = true;
		}

		protected override void InitCopy()
		{
			this.txtStyleId.Text = base.PageParam.GetString("STYLEID");
			this.chkPublic.Checked = true;
		}

		protected override void SetCtrlState()
		{
			this.txtStyleId.Enabled = false;
			this.chkDefault.Enabled = false;
			this.chkPublic.Enabled = false;
		}

		internal override void SwitchPagePermission()
		{
			base.PagePermission.Insert = (base.PagePermission.View = (base.PagePermission.Edit = (base.PagePermission.Delete = (base.UserId == "ADMIN"))));
		}

		protected override void OnPreLoadRecord()
		{
			bool flag = !base.IsPostBack;
			if (flag)
			{
				this.MasterEntity.QueryStyleId = base.PageParam.GetString("STYLEID");
				this.MasterEntity.QueryStyleOrder = base.PageParam.GetString("STYLEORDER");
			}
			else
			{
				this.MasterEntity.QueryStyleId = this.txtStyleId.Text.Trim();
				this.MasterEntity.QueryStyleOrder = this.ntOrder.Value.ToString();
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !base.IsPostBack;
			if (flag)
			{
			}
		}

		protected override bool OnSaveComplete()
		{
			this.SaveReportStyle(this.txtStyleId.Text.Trim(), this.ntOrder.Text, base.PageParam.GetExtRawString("REPORTSTYLE"));
			return true;
		}

		private bool SaveReportStyle(string styleId, string styleOrder, string reportStyle)
		{
			Database db = LogicContext.GetDatabase();
			bool result;
			try
			{
				byte[] bybuf = new byte[0];
				bybuf = Encoding.Default.GetBytes(reportStyle);
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add("UPDATE REPORTSTYLE SET");
				hsql.Add(" REPORTSTYLE_STYLE = :REPORTSTYLE_STYLE ");
				hsql.Add("WHERE REPORTSTYLE_STYLEID = :REPORTSTYLE_STYLEID");
				hsql.Add("AND REPORTSTYLE_ORDER = :REPORTSTYLE_ORDER");
				hsql.ParamByName("REPORTSTYLE_STYLE").Value = bybuf;
				hsql.ParamByName("REPORTSTYLE_STYLEID").Value = styleId;
				hsql.ParamByName("REPORTSTYLE_ORDER").Value = styleOrder;
				db.ExecSQL(hsql);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
