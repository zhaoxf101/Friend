using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_WEBCTRL;
using TIM.T_WORKFLOW;

namespace TIM.T_TEMPLET
{
	public class TimDbCtrlUtils
	{
		public static void FillDropDownList(TimDropDownList ctrl, string sql, bool insertEmpty)
		{
			TimDbCtrlUtils.FillDropDownList(ctrl, sql, insertEmpty, false);
		}

		public static void FillDropDownList(TimDropDownList ctrl, string sql, bool insertEmpty, bool firstSelected)
		{
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Clear();
			hsql.Add(sql);
			try
			{
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Columns.Count < 2;
				if (flag)
				{
					throw new Exception("");
				}
				ctrl.Items.Clear();
				if (insertEmpty)
				{
					ctrl.Items.Add(new ListItem(" ", " "));
				}
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					string text = ds.Tables[0].Rows[i][0].ToString().Trim();
					string value = ds.Tables[0].Rows[i][1].ToString().Trim();
					ctrl.Items.Add(new ListItem(text, value));
				}
				if (insertEmpty)
				{
					bool flag2 = firstSelected && ctrl.Items.Count > 1;
					if (flag2)
					{
						ctrl.Items[1].Selected = true;
					}
				}
				else
				{
					bool flag3 = firstSelected && ctrl.Items.Count > 0;
					if (flag3)
					{
						ctrl.Items[0].Selected = true;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void FillYear(TimDropDownList ctrl)
		{
			ctrl.Items.Clear();
			for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year + 10; i++)
			{
				ctrl.Items.Add(new ListItem(i.ToString(), i.ToString()));
			}
			ctrl.SelectedValue = DateTime.Now.Year.ToString();
		}

		public static void FillMonth(TimDropDownList ctrl)
		{
			ctrl.Items.Clear();
			for (int i = 1; i <= 12; i++)
			{
				ctrl.Items.Add(new ListItem(i.ToString(), i.ToString()));
			}
			ctrl.SelectedValue = DateTime.Now.Month.ToString();
		}

		public static void FillYNStatus(TimDropDownList ctrl)
		{
			ctrl.Items.Clear();
			ctrl.Items.Add(new ListItem("", ""));
			ctrl.Items.Add(new ListItem("否", "N"));
			ctrl.Items.Add(new ListItem("是", "Y"));
			ctrl.SelectedValue = "";
		}

		public static void FillBM(TimDropDownList ctrl, bool insertEmpty)
		{
			TimDbCtrlUtils.FillDropDownList(ctrl, "SELECT RTRIM(BM_BMID) ||'|'|| BM_BMMC,RTRIM(BM_BMID) FROM BM WHERE 1=1", insertEmpty);
		}

		public static void FillWLLB(TimDropDownList ctrl, bool insertEmpty)
		{
			TimDbCtrlUtils.FillDropDownList(ctrl, "SELECT RTRIM(WLLB_WLLBID) ||'|'|| WLLB_WLLBMC,WLLB_WLLBID FROM WLLB WHERE 1=1", insertEmpty);
		}

		public static void FillCK(TimDropDownList ctrl, bool insertEmpty)
		{
			TimDbCtrlUtils.FillDropDownList(ctrl, "SELECT RTRIM(CK_CKID)||'|'||CK_CKMC,CK_CKID FROM CK WHERE 1=1", insertEmpty);
		}

		public static void FillRKStatus(TimDropDownList ctrl)
		{
			ctrl.Items.Clear();
			ctrl.Items.Add(new ListItem("未入库", "WRK")
			{
				Selected = true
			});
			ctrl.Items.Add(new ListItem("已入库", "YRK"));
			ctrl.Items.Add(new ListItem("全部", "ALL"));
		}

		public static void FillCKStatus(TimDropDownList ctrl)
		{
			ctrl.Items.Clear();
			ctrl.Items.Add(new ListItem("未出库", "WCK")
			{
				Selected = true
			});
			ctrl.Items.Add(new ListItem("已出库", "YCK"));
			ctrl.Items.Add(new ListItem("全部", "ALL"));
		}

		public static void FillWfpQOption(TimDropDownList ctrl)
		{
			ctrl.Items.Clear();
			ctrl.Items.Add(new ListItem("T|待我处理", "T")
			{
				Selected = true
			});
			ctrl.Items.Add(new ListItem("D|我已处理", "D"));
		}

		public static void FillWfpAllQOption(TimDropDownList ctrl)
		{
			ctrl.Items.Clear();
			ctrl.Items.Add(new ListItem("T|待我处理", "T")
			{
				Selected = true
			});
			ctrl.Items.Add(new ListItem("D|我已处理", "D"));
			ctrl.Items.Add(new ListItem("A|所有事务", "A"));
		}

		public static void FillWfpAllQOptionWithNull(TimDropDownList ctrl)
		{
			ctrl.Items.Clear();
			ctrl.Items.Add(new ListItem(" ", " ")
			{
				Selected = true
			});
			ctrl.Items.Add(new ListItem("T|待我处理", "T"));
			ctrl.Items.Add(new ListItem("D|我已处理", "D"));
			ctrl.Items.Add(new ListItem("A|所有事务", "A"));
		}

		public static void FillWfpWithNull(TimDropDownList ctrl, string wfbId)
		{
			WorkflowEngine workflow = new WorkflowEngine();
			string workflowId = workflow.GetWorkflowId(wfbId);
			List<WFP> lstWfp = WFPUtils.GetWFP(workflowId);
			ctrl.Items.Clear();
			ctrl.Items.Add(new ListItem(" ", " ")
			{
				Selected = true
			});
			foreach (WFP item in lstWfp)
			{
				ctrl.Items.Add(new ListItem(item.WfpId.Trim() + "|" + item.WfpName.Trim(), item.WfpId.Trim()));
			}
		}
	}
}
