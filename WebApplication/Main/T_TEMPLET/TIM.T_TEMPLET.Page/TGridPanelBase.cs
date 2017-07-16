using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Common;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_KERNEL.Helper;
using TIM.T_TEMPLET.Master;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Page
{
	public class TGridPanelBase : PageBase
	{
		private TGridPanel m_curMaster = null;

		private List<TimBoundField> m_columns = new List<TimBoundField>();

		private List<string> m_columnSwitchToName = new List<string>();

		public TGridPanel CurMaster
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

		internal List<TimBoundField> Columns
		{
			get
			{
				return this.m_columns;
			}
			set
			{
				this.m_columns = value;
			}
		}

		public List<string> ColumnSwitchToName
		{
			get
			{
				return this.m_columnSwitchToName;
			}
			set
			{
				this.m_columnSwitchToName = value;
			}
		}

		protected void AddColumn(TimBoundField boundField)
		{
			this.Columns.Add(boundField);
		}

		protected void AddColumnSwitchToName(string field)
		{
			this.ColumnSwitchToName.Add(field);
		}

		protected virtual HSQL BuildTopHSQL()
		{
			Database db = LogicContext.GetDatabase();
			return new HSQL(db);
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

		protected override void OnLoadRecord()
		{
			Database db = LogicContext.GetDatabase();
			DataSet ds = db.OpenDataSet(this.BuildTopHSQL());
			foreach (string item in this.ColumnSwitchToName)
			{
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					ds.Tables[0].Rows[i][item] = base.GetWfUsersName(ds.Tables[0].Rows[i][item].ToString());
				}
			}
			for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
			{
				int openAmid = ds.Tables[0].Rows[j]["OPENAMID"].ToString().ToInt();
				MePage mePage = MePageUtils.GetMePage(openAmid);
				PageParameter pageParameter = new PageParameter();
				bool flag = mePage != null;
				if (flag)
				{
					pageParameter.UrlPath = this.Page.ResolveUrl(mePage.PageUrl);
					bool flag2 = mePage.Type == ModuleType.W;
					if (flag2)
					{
						pageParameter.AddString("WFID", ds.Tables[0].Rows[j]["WFID"].ToString().Trim());
						pageParameter.AddString("WFRUNID", ds.Tables[0].Rows[j]["WFRUNID"].ToString().Trim());
					}
					else
					{
						bool flag3 = mePage.Type == ModuleType.N;
						if (flag3)
						{
							string keyWhere = ds.Tables[0].Rows[j]["KEYWHERE"].ToString().Trim();
							pageParameter.AddNameValueString(new NameValueString
							{
								NaviteText = keyWhere
							});
						}
					}
					ds.Tables[0].Rows[j]["PAGEURL"] = pageParameter.CombineUrl();
				}
			}
			IsoDateTimeConverter iso = new IsoDateTimeConverter();
			iso.DateTimeFormat = "yyyy-MM-dd";
			string jsonData = JsonConvert.SerializeObject(ds.Tables[0], new JsonConverter[]
			{
				new DataTableConverter(),
				iso
			});
			StringBuilder sb = new StringBuilder();
			sb.Append("var $maingrid;");
			sb.Append("$(function () {");
			sb.Append("$maingrid = $('#templetGridPanel').ligerGrid({");
			sb.Append("columns: [");
			foreach (TimBoundField item2 in this.Columns)
			{
				sb.Append(string.Concat(new string[]
				{
					"{ display: '",
					item2.HeaderText,
					"', name: '",
					item2.DataField,
					"', align: 'left', width: ",
					item2.ItemStyle.Width.Value.ToString(),
					" },"
				}));
			}
			sb.Append("{ display: '', name: 'PAGEURL', align: 'left', width: 1, hide: true }");
			sb.Append("],");
			sb.Append("data:{ Rows:" + jsonData + "},");
			sb.Append("height:'99%',");
			sb.Append("rowHeight:22,");
			sb.Append("headerRowHeight:23,");
			sb.Append("enabledSort: false,");
			sb.Append("allowAdjustColWidth:false,");
			sb.Append("frozenCheckbox:true,");
			sb.Append("usePager: false,");
			sb.Append("onDblClickRow: function (data, rowindex, rowobj) {");
			sb.Append("OpenPage('VIEW',data.PAGEURL);");
			sb.Append("}");
			sb.Append("});");
			sb.Append("});");
			base.RegisterScript("disGridPanel", sb.ToString());
			base.RegisterScript("PortalPanelRefresh", "function panelRefresh(){window.location.href = window.location.href;}; window.setTimeout('panelRefresh()',600000);");
		}
	}
}
