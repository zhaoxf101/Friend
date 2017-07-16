using System;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Utils;
using TIM.T_TEMPLET.Page;

namespace TIM.T_TEMPLET.CommForm
{
	[Entity(Table = "REPORTSTYLE", Workflow = false, BeDoc = false, ModifyControl = false)]
	public class ReportStyle : EntityManager
	{
		public string QueryStyleId = string.Empty;

		public string QueryStyleName = string.Empty;

		public string QueryStyleOrder = string.Empty;

		[FieldMap(DbField = "REPORTSTYLE_STYLEID", Desc = "样式编码", Key = true, DbType = TimDbType.Char, Null = false, Len = 50, CtrlId = "txtStyleId", CtrlType = ControlType.TextBox, DefValue = "")]
		public object REPORTSTYLE_STYLEID
		{
			get
			{
				return base.GetField("REPORTSTYLE_STYLEID");
			}
			set
			{
				base.SetField("REPORTSTYLE_STYLEID", value);
			}
		}

		[FieldMap(DbField = "REPORTSTYLE_STYLENAME", Desc = "样式名称", Key = false, DbType = TimDbType.VarChar, Null = false, Len = 40, CtrlId = "txtStyleName", CtrlType = ControlType.TextBox, DefValue = "")]
		public object REPORTSTYLE_STYLENAME
		{
			get
			{
				return base.GetField("REPORTSTYLE_STYLENAME");
			}
			set
			{
				base.SetField("REPORTSTYLE_STYLENAME", value);
			}
		}

		[FieldMap(DbField = "REPORTSTYLE_ORDER", Desc = "序号", Key = true, DbType = TimDbType.Float, Null = false, Len = 0, CtrlId = "ntOrder", CtrlType = ControlType.NumericTextBox, DefValue = "")]
		public object REPORTSTYLE_ORDER
		{
			get
			{
				return base.GetField("REPORTSTYLE_ORDER");
			}
			set
			{
				base.SetField("REPORTSTYLE_ORDER", value);
			}
		}

		[FieldMap(DbField = "REPORTSTYLE_DEFAULT", Desc = "缺省", Key = false, DbType = TimDbType.Char, Null = true, Len = 1, CtrlId = "chkDefault", CtrlType = ControlType.CheckBox, DefValue = "")]
		public object REPORTSTYLE_DEFAULT
		{
			get
			{
				return base.GetField("REPORTSTYLE_DEFAULT");
			}
			set
			{
				base.SetField("REPORTSTYLE_DEFAULT", value);
			}
		}

		[FieldMap(DbField = "REPORTSTYLE_PUBLIC", Desc = "公用", Key = false, DbType = TimDbType.Char, Null = true, Len = 1, CtrlId = "chkPublic", CtrlType = ControlType.CheckBox, DefValue = "")]
		public object REPORTSTYLE_PUBLIC
		{
			get
			{
				return base.GetField("REPORTSTYLE_PUBLIC");
			}
			set
			{
				base.SetField("REPORTSTYLE_PUBLIC", value);
			}
		}

		[FieldMap(DbField = "REPORTSTYLE_EXECON", Desc = "执行条件", Key = false, DbType = TimDbType.VarChar, Null = true, Len = 250, CtrlId = "txtExecOn", CtrlType = ControlType.TextBox, DefValue = "")]
		public object REPORTSTYLE_EXECON
		{
			get
			{
				return base.GetField("REPORTSTYLE_EXECON");
			}
			set
			{
				base.SetField("REPORTSTYLE_EXECON", value);
			}
		}

		[FieldMap(DbField = "REPORTSTYLE_VERSION", Desc = "版本", Key = false, DbType = TimDbType.Float, Null = true, Len = 0, CtrlId = "ntVersion", CtrlType = ControlType.NumericTextBox, DefValue = "")]
		public object REPORTSTYLE_VERSION
		{
			get
			{
				return base.GetField("REPORTSTYLE_VERSION");
			}
			set
			{
				base.SetField("REPORTSTYLE_VERSION", value);
			}
		}

		public override HSQL BuildRecordSetSql()
		{
			HSQL hsql = base.BuildRecordSetSql();
			hsql.Clear();
			hsql.Add("SELECT REPORTSTYLE_STYLEID,REPORTSTYLE_STYLENAME");
			hsql.Add(",REPORTSTYLE_ORDER,REPORTSTYLE_DEFAULT,REPORTSTYLE_PUBLIC");
			hsql.Add(",REPORTSTYLE_EXECON,REPORTSTYLE_VERSION");
			hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			hsql.Add("FROM REPORTSTYLE");
			hsql.Add("WHERE 1 = 1");
			bool flag = !string.IsNullOrWhiteSpace(this.QueryStyleId);
			if (flag)
			{
				hsql.Add("AND REPORTSTYLE_STYLEID like '%'||:REPORTSTYLE_STYLEID||'%'");
				hsql.ParamByName("REPORTSTYLE_STYLEID").Value = this.QueryStyleId;
			}
			bool flag2 = !string.IsNullOrWhiteSpace(this.QueryStyleName);
			if (flag2)
			{
				hsql.Add("AND REPORTSTYLE_STYLENAME like '%'||:REPORTSTYLE_STYLENAME||'%' ");
				hsql.ParamByName("REPORTSTYLE_STYLENAME").Value = this.QueryStyleName;
			}
			hsql.Add("ORDER BY REPORTSTYLE_DEFAULT DESC,REPORTSTYLE_ORDER ASC");
			return hsql;
		}

		public override HSQL BuildRecordSql()
		{
			HSQL hsql = base.BuildRecordSql();
			hsql.Clear();
			hsql.Add("SELECT REPORTSTYLE_STYLEID,REPORTSTYLE_STYLENAME");
			hsql.Add(",REPORTSTYLE_ORDER,REPORTSTYLE_DEFAULT,REPORTSTYLE_PUBLIC");
			hsql.Add(",REPORTSTYLE_EXECON,REPORTSTYLE_VERSION");
			hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME");
			hsql.Add("FROM REPORTSTYLE");
			hsql.Add("WHERE REPORTSTYLE_STYLEID = :REPORTSTYLE_STYLEID");
			hsql.Add("AND REPORTSTYLE_ORDER = :REPORTSTYLE_ORDER");
			bool flag = !string.IsNullOrWhiteSpace(this.QueryStyleId);
			if (flag)
			{
				hsql.ParamByName("REPORTSTYLE_STYLEID").Value = this.QueryStyleId;
			}
			bool flag2 = !string.IsNullOrWhiteSpace(this.QueryStyleOrder);
			if (flag2)
			{
				hsql.ParamByName("REPORTSTYLE_ORDER").Value = this.QueryStyleOrder;
			}
			return hsql;
		}

		public override HSQL BuildInsertSql()
		{
			HSQL hsql = base.BuildInsertSql();
			hsql.Clear();
			hsql.Add("INSERT INTO REPORTSTYLE(REPORTSTYLE_STYLEID,REPORTSTYLE_STYLENAME");
			hsql.Add(",REPORTSTYLE_ORDER,REPORTSTYLE_DEFAULT,REPORTSTYLE_PUBLIC");
			hsql.Add(",REPORTSTYLE_EXECON,REPORTSTYLE_VERSION");
			hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME)");
			hsql.Add("VALUES");
			hsql.Add("(:REPORTSTYLE_STYLEID,:REPORTSTYLE_STYLENAME");
			hsql.Add(",:REPORTSTYLE_ORDER,:REPORTSTYLE_DEFAULT,:REPORTSTYLE_PUBLIC");
			hsql.Add(",:REPORTSTYLE_EXECON,:REPORTSTYLE_VERSION");
			hsql.Add(",:CREATERID,:CREATER,:CREATEDTIME,:MODIFIERID,:MODIFIER,:MODIFIEDTIME)");
			return hsql;
		}

		public override HSQL BuildUpdateSql()
		{
			HSQL hsql = base.BuildUpdateSql();
			hsql.Clear();
			hsql.Add("UPDATE REPORTSTYLE SET");
			hsql.Add(" REPORTSTYLE_STYLENAME = :REPORTSTYLE_STYLENAME");
			hsql.Add(",REPORTSTYLE_DEFAULT = :REPORTSTYLE_DEFAULT");
			hsql.Add(",REPORTSTYLE_PUBLIC = :REPORTSTYLE_PUBLIC");
			hsql.Add(",REPORTSTYLE_EXECON = :REPORTSTYLE_EXECON");
			hsql.Add(",REPORTSTYLE_VERSION = :REPORTSTYLE_VERSION");
			hsql.Add(",MODIFIERID = :MODIFIERID");
			hsql.Add(",MODIFIER = :MODIFIER");
			hsql.Add(",MODIFIEDTIME = :MODIFIEDTIME");
			hsql.Add("WHERE REPORTSTYLE_STYLEID = :REPORTSTYLE_STYLEID");
			hsql.Add("AND REPORTSTYLE_ORDER = :REPORTSTYLE_ORDER");
			return hsql;
		}

		public override HSQL BuildDeleteSql()
		{
			HSQL hsql = base.BuildDeleteSql();
			hsql.Clear();
			hsql.Add("DELETE FROM REPORTSTYLE");
			hsql.Add("WHERE REPORTSTYLE_STYLEID = :REPORTSTYLE_STYLEID");
			hsql.Add("AND REPORTSTYLE_ORDER = :REPORTSTYLE_ORDER");
			return hsql;
		}

		public override bool UpdateComplete(HSQL hsql)
		{
			CacheEvent.TableIsUpdated("REPORTSTYLE");
			return base.UpdateComplete(hsql);
		}

		public override bool DeleteComplete(HSQL hsql)
		{
			CacheEvent.TableIsUpdated("REPORTSTYLE");
			return base.DeleteComplete(hsql);
		}

		public override bool InsertComplete(HSQL hsql)
		{
			CacheEvent.TableIsUpdated("REPORTSTYLE");
			return base.InsertComplete(hsql);
		}
	}
}
