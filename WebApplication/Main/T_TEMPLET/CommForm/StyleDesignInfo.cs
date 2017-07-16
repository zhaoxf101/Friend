using System;
using System.Data;
using System.Text;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Page;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.CommForm
{
	public class StyleDesignInfo : TEditingBase
	{
		private StyleDesign MasterEntity = new StyleDesign();

		protected TimHiddenField hidStyleId;

		protected TimHiddenField hidStyleOrder;

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
			base.Width = 0;
			base.Height = 0;
		}

		protected override void InitTemplet()
		{
			base.CurMaster = this.Master;
			base.CurEntity = this.MasterEntity;
		}

		protected override void InitCtrlValue()
		{
		}

		protected override void SetCtrlState()
		{
			this.SetMenu_HideAllStdBtn();
		}

		protected override void OnLoadRecord()
		{
		}

		protected override void OnPreLoadRecord()
		{
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = base.Request.Form["SAVEREPORT"] != null;
			if (flag)
			{
				string styleId = base.Request.Form["STYLEID"];
				string styleOrder = base.Request.Form["STYLEORDER"];
				string style = base.Request.Form["REPORTFORM"];
				this.SaveReportStyle(styleId, styleOrder, style);
			}
			bool flag2 = !base.IsPostBack;
			if (flag2)
			{
				bool flag3 = string.IsNullOrEmpty(base.Request.Form["SAVEREPORT"]);
				if (flag3)
				{
					this.hidStyleId.Value = base.PageParam.GetString("STYLEID");
					this.hidStyleOrder.Value = base.PageParam.GetString("STYLEORDER");
					string designType = string.Format(";document.all.UtoYSDY.UtoType = '{0}'", "DesignBill");
					string defineDataset = string.Format(";document.all.UtoYSDY.EnableEditUtoDataSet = '{0}'", "true");
					string datasetXml = string.Format(";document.all.UtoYSDY.CompressUtoDataSet = '{0}'", DelphiTempletDesUtils.GetEncryZipBase64(base.PageParam.GetExtRawString("REPORTSTYLE")));
					string reportStyle = this.GetReportStyle(this.hidStyleId.Value, this.hidStyleOrder.Value);
					bool flag4 = string.IsNullOrWhiteSpace(reportStyle);
					string LinkString;
					if (flag4)
					{
						LinkString = string.Format(";document.all.UtoYSDY.CompressUtoXml = '{0}';", DelphiTempletDesUtils.GetEncryZipBase64(base.PageParam.GetExtRawString("REPORTSTYLE")));
					}
					else
					{
						LinkString = string.Format(";document.all.UtoYSDY.CompressUtoReport = '{0}';", reportStyle);
					}
					string regScript = designType + defineDataset + datasetXml + LinkString;
					base.RegisterScript("DesignReportStyle", regScript);
				}
			}
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
	}
}
