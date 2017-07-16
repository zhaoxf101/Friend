using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.TimException;
using TIM.T_WORKFLOW;

namespace TIM.T_TEMPLET.Page
{
	public class EntityManager
	{
		protected Database _DbConnection;

		private DataTable m_recordSet;

		private DataTable m_oldRecordSet;

		public int ActivedIndex = -1;

		internal int PageSize = -1;

		internal int PageIndex = -1;

		internal int RecordCount = 0;

		private bool m_allowPagingQuery = true;

		private string m_promptMessage = string.Empty;

		private EntityAttribute m_entity = new EntityAttribute();

		private Dictionary<string, FieldMapAttribute> m_fields = new Dictionary<string, FieldMapAttribute>();

		private Dictionary<string, RelatedEntityAttribute> m_relatedObject = new Dictionary<string, RelatedEntityAttribute>();

		private HSQL m_recordSetSql = null;

		private HSQL m_recordSql = null;

		private HSQL m_insertSql = null;

		private HSQL m_updateSql = null;

		private HSQL m_deleteSql = null;

		protected string UserId
		{
			get
			{
				string userId = string.Empty;
				LogicContext lgc = LogicContext.Current;
				bool flag = lgc != null;
				if (flag)
				{
					userId = lgc.UserId;
				}
				return userId;
			}
		}

		public DataTable RecordSet
		{
			get
			{
				return this.m_recordSet;
			}
			set
			{
				this.m_recordSet = value;
			}
		}

		public DataTable OldRecordSet
		{
			get
			{
				return this.m_oldRecordSet;
			}
			set
			{
				this.m_oldRecordSet = value;
			}
		}

		internal string WorkflowId
		{
			get;
			set;
		}

		internal int WorkflowRunId
		{
			get;
			set;
		}

		internal double FileGroupId
		{
			get;
			set;
		}

		internal double GroupFiles
		{
			get;
			set;
		}

		public bool AllowPagingQuery
		{
			get
			{
				return this.m_allowPagingQuery;
			}
			set
			{
				this.m_allowPagingQuery = value;
			}
		}

		internal string PromptMessage
		{
			get
			{
				return this.m_promptMessage;
			}
			set
			{
				this.m_promptMessage = value;
			}
		}

		public EntityAttribute Entity
		{
			get
			{
				return this.m_entity;
			}
		}

		public Dictionary<string, FieldMapAttribute> Fields
		{
			get
			{
				return this.m_fields;
			}
		}

		public Dictionary<string, RelatedEntityAttribute> RelatedObject
		{
			get
			{
				return this.m_relatedObject;
			}
			set
			{
				this.m_relatedObject = value;
			}
		}

		public HSQL RecordSetSql
		{
			get
			{
				return this.m_recordSetSql;
			}
			set
			{
				this.m_recordSetSql = value;
			}
		}

		public HSQL RecordSql
		{
			get
			{
				return this.m_recordSql;
			}
			set
			{
				this.m_recordSql = value;
			}
		}

		public HSQL InsertSql
		{
			get
			{
				return this.m_insertSql;
			}
			set
			{
				this.m_insertSql = value;
			}
		}

		public HSQL UpdateSql
		{
			get
			{
				return this.m_updateSql;
			}
			set
			{
				this.m_updateSql = value;
			}
		}

		public HSQL DeleteSql
		{
			get
			{
				return this.m_deleteSql;
			}
			set
			{
				this.m_deleteSql = value;
			}
		}

		[FieldMap(DbField = "CREATERID", Desc = "创建人编码", Key = false, DbType = TimDbType.Char, Null = true, Len = 10, CtrlId = "txtCreaterId", CtrlType = ControlType.TextBox, DefValue = "")]
		public object CREATERID
		{
			get
			{
				return this.GetField("CREATERID");
			}
			set
			{
				this.SetField("CREATERID", value);
			}
		}

		[FieldMap(DbField = "CREATER", Desc = "创建人", Key = false, DbType = TimDbType.Char, Null = true, Len = 30, CtrlId = "txtCreater", CtrlType = ControlType.TextBox, DefValue = "")]
		public object CREATER
		{
			get
			{
				return this.GetField("CREATER");
			}
			set
			{
				this.SetField("CREATER", value);
			}
		}

		[FieldMap(DbField = "CREATEDTIME", Desc = "创建时间", Key = false, DbType = TimDbType.DateTime, Null = true, Len = 0, CtrlId = "dtCreaterTime", CtrlType = ControlType.DateTime, DefValue = "")]
		public object CREATEDTIME
		{
			get
			{
				return this.GetField("CREATEDTIME");
			}
			set
			{
				this.SetField("CREATEDTIME", value);
			}
		}

		public object MODIFIERID
		{
			get
			{
				return this.GetField("MODIFIERID");
			}
			set
			{
				this.SetField("MODIFIERID", value);
			}
		}

		[FieldMap(DbField = "MODIFIER", Desc = "修改人", Key = false, DbType = TimDbType.Char, Null = true, Len = 30, CtrlId = "txtModifier", CtrlType = ControlType.TextBox, DefValue = "")]
		public object MODIFIER
		{
			get
			{
				return this.GetField("MODIFIER");
			}
			set
			{
				this.SetField("MODIFIER", value);
			}
		}

		[FieldMap(DbField = "MODIFIEDTIME", Desc = "修改时间", Key = false, DbType = TimDbType.DateTime, Null = true, Len = 0, CtrlId = "dtModifierTime", CtrlType = ControlType.DateTime, DefValue = "")]
		public object MODIFIEDTIME
		{
			get
			{
				return this.GetField("MODIFIEDTIME");
			}
			set
			{
				this.SetField("MODIFIEDTIME", value);
			}
		}

		public EntityManager()
		{
			this.BuildEntity();
			this.BuildTable();
		}

		public void GetDbConnection()
		{
			this._DbConnection = LogicContext.GetDatabase();
		}

		private void BuildEntity()
		{
			object[] attrClass = base.GetType().GetCustomAttributes(typeof(EntityAttribute), true);
			bool flag = attrClass != null && attrClass.Length != 0;
			if (flag)
			{
				this.m_entity = (EntityAttribute)attrClass[0];
			}
			PropertyInfo[] properties = base.GetType().GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo item = properties[i];
				object[] attrs = item.GetCustomAttributes(typeof(FieldMapAttribute), true);
				bool flag2 = attrs != null && attrs.Length != 0;
				if (flag2)
				{
					this.m_fields.Add(((FieldMapAttribute)attrs[0]).DbField, (FieldMapAttribute)attrs[0]);
				}
				object[] attrsRelated = item.GetCustomAttributes(typeof(RelatedEntityAttribute), true);
				bool flag3 = attrsRelated != null && attrsRelated.Length != 0;
				if (flag3)
				{
					this.RelatedObject.Add(((RelatedEntityAttribute)attrsRelated[0]).Name, (RelatedEntityAttribute)attrsRelated[0]);
				}
			}
			FieldMapAttribute MODIFIERID = new FieldMapAttribute();
			MODIFIERID.DbField = "MODIFIERID";
			MODIFIERID.Desc = "修改人编码";
			MODIFIERID.Key = false;
			MODIFIERID.DbType = TimDbType.Char;
			MODIFIERID.Null = true;
			MODIFIERID.Len = 10;
			MODIFIERID.CtrlId = "txtModifierId";
			MODIFIERID.CtrlType = ControlType.TextBox;
			MODIFIERID.DefValue = "";
			this.m_fields.Add(MODIFIERID.DbField, MODIFIERID);
		}

		private void BuildTable()
		{
			this.m_recordSet = new DataTable(this.Entity.Table);
			this.m_oldRecordSet = new DataTable(this.Entity.Table);
			DataColumnCollection columns = this.m_recordSet.Columns;
			DataColumnCollection oldColumns = this.m_oldRecordSet.Columns;
			foreach (KeyValuePair<string, FieldMapAttribute> de in this.m_fields)
			{
				bool flag = de.Value.DbType == TimDbType.DateTime;
				if (flag)
				{
					columns.Add(de.Value.DbField, typeof(DateTime));
					oldColumns.Add(de.Value.DbField, typeof(DateTime));
				}
				else
				{
					bool flag2 = de.Value.DbType == TimDbType.Float;
					if (flag2)
					{
						columns.Add(de.Value.DbField, typeof(double));
						oldColumns.Add(de.Value.DbField, typeof(double));
					}
					else
					{
						columns.Add(de.Value.DbField);
						oldColumns.Add(de.Value.DbField);
					}
				}
			}
		}

		public virtual HSQL BuildRecordSetSql()
		{
			this.GetDbConnection();
			return new HSQL(this._DbConnection);
		}

		public virtual HSQL BuildRecordSql()
		{
			this.GetDbConnection();
			return new HSQL(this._DbConnection);
		}

		public virtual string BuildExecOn()
		{
			return string.Empty;
		}

		public virtual HSQL BuildInsertSql()
		{
			this.GetDbConnection();
			return new HSQL(this._DbConnection);
		}

		public virtual HSQL BuildUpdateSql()
		{
			this.GetDbConnection();
			return new HSQL(this._DbConnection);
		}

		public virtual HSQL BuildDeleteSql()
		{
			this.GetDbConnection();
			return new HSQL(this._DbConnection);
		}

		public virtual HSQL BuildTreeSql()
		{
			this.GetDbConnection();
			return new HSQL(this._DbConnection);
		}

		public virtual bool VerifyNull()
		{
			return true;
		}

		public virtual bool VerifyLength()
		{
			return true;
		}

		public virtual bool VerifyBusinessLogic()
		{
			return true;
		}

		protected virtual bool VerifyQuery()
		{
			return true;
		}

		public virtual bool PreInsert(HSQL hsql)
		{
			return true;
		}

		public virtual bool CanInsert()
		{
			return true;
		}

		public bool Insert()
		{
			bool result = true;
			try
			{
				bool flag = result;
				if (flag)
				{
					result = this.PreInsert(this.InsertSql);
				}
				this.SetSqlParams(this.InsertSql);
				bool flag2 = result;
				if (flag2)
				{
					this._DbConnection.ExecSQL(this.InsertSql);
				}
				bool flag3 = result;
				if (flag3)
				{
					result = this.InsertComplete(this.InsertSql);
				}
			}
			catch (SqlException sqlError)
			{
				result = false;
				bool flag4 = sqlError.Number == 2627;
				if (flag4)
				{
					this.DisplayPrompt("关键字重复，已存在编号相同的记录，保存失败！");
				}
				else
				{
					this.DisplayPrompt("保存失败！" + sqlError.Message.Replace("\r\n", "\\r").Replace("\n", "\\r"));
				}
			}
			catch (OracleException odpError)
			{
				result = false;
				bool flag5 = odpError.Number == 1;
				if (flag5)
				{
					this.DisplayPrompt("关键字重复，已存在编号相同的记录，保存失败！");
				}
				else
				{
					this.DisplayPrompt("保存失败！" + odpError.Message.Replace("\r\n", "\\r").Replace("\n", "\\r"));
				}
			}
			catch (Exception error)
			{
				result = false;
				this.DisplayPrompt("保存失败！" + error.Message.Replace("\r\n", "\\r").Replace("\n", "\\r"));
			}
			return result;
		}

		public virtual bool InsertComplete(HSQL hsql)
		{
			return true;
		}

		public virtual bool CanEdit()
		{
			return true;
		}

		public virtual bool PreUpdate(HSQL hsql)
		{
			return true;
		}

		public bool Update()
		{
			bool result = true;
			try
			{
				bool flag = result;
				if (flag)
				{
					result = this.PreUpdate(this.UpdateSql);
				}
				this.SetSqlParams(this.UpdateSql);
				bool flag2 = result;
				if (flag2)
				{
					this._DbConnection.ExecSQL(this.UpdateSql);
				}
				bool flag3 = result;
				if (flag3)
				{
					result = this.UpdateComplete(this.UpdateSql);
				}
			}
			catch (Exception error)
			{
				result = false;
				this.DisplayPrompt("保存失败！" + error.Message.Replace("\r\n", "\\r").Replace("\n", "\\r"));
			}
			return result;
		}

		public virtual bool UpdateComplete(HSQL hsql)
		{
			return true;
		}

		public virtual bool CanDelete()
		{
			return true;
		}

		public virtual bool PreDelete(HSQL hsql)
		{
			return true;
		}

		private bool OnDeleteBill()
		{
			bool result;
			foreach (KeyValuePair<string, RelatedEntityAttribute> item in this.RelatedObject)
			{
				bool flag = !item.Value.DeleteSlave;
				if (!flag)
				{
					EntityManager emObj = (EntityManager)base.GetType().GetProperty(item.Value.Name).GetValue(this, null);
					emObj.AllowPagingQuery = false;
					emObj.RecordSetSql = emObj.BuildRecordSetSql();
					string[] keyList = item.Value.SlaveMainKey.Split(new char[]
					{
						','
					});
					for (int keyIndex = 0; keyIndex < keyList.Length; keyIndex++)
					{
						bool flag2 = emObj.RecordSetSql.ParamByName(keyList[keyIndex]) != null;
						if (flag2)
						{
							emObj.RecordSetSql.AddParam(keyList[keyIndex], this.Fields[item.Value.MasterMainKey.Split(new char[]
							{
								','
							})[keyIndex]].DbType, this.Fields[item.Value.MasterMainKey.Split(new char[]
							{
								','
							})[keyIndex]].Len, this.GetField(item.Value.MasterMainKey.Split(new char[]
							{
								','
							})[keyIndex]));
						}
					}
					emObj.QueryRecordSet();
					for (int i = 0; i < emObj.RecordSet.Rows.Count; i++)
					{
						emObj.DeleteSql = emObj.BuildDeleteSql();
						emObj.ActivedIndex = i;
						bool flag3 = !emObj.Delete();
						if (flag3)
						{
							this.DisplayPrompt(emObj.PromptMessage);
							result = false;
							return result;
						}
					}
				}
			}
			result = true;
			return result;
		}

		public bool Delete()
		{
			bool result = true;
			try
			{
				bool flag = result;
				if (flag)
				{
					result = this.PreDelete(this.DeleteSql);
				}
				bool flag2 = result;
				if (flag2)
				{
					result = this.OnDeleteBill();
				}
				bool flag3 = result;
				if (flag3)
				{
					this.SetSqlParams(this.DeleteSql);
				}
				bool flag4 = result;
				if (flag4)
				{
					this._DbConnection.ExecSQL(this.DeleteSql);
				}
				bool flag5 = result;
				if (flag5)
				{
					result = this.DeleteComplete(this.DeleteSql);
				}
			}
			catch (Exception ex)
			{
				result = false;
				this.DisplayPrompt(ex.Message);
			}
			return result;
		}

		public virtual bool DeleteComplete(HSQL hsql)
		{
			return true;
		}

		public virtual bool PreQueryRecord(HSQL hsql)
		{
			return true;
		}

		public virtual void QueryRecord()
		{
			bool flag = !this.PreQueryRecord(this.RecordSql);
			if (!flag)
			{
				DbDataAdapter adapter = this._DbConnection.OpenAdapter(this.RecordSql);
				adapter.TableMappings.Add("Table", this.Entity.Table);
				this.m_recordSet.Clear();
				this.BuildTable();
				int startRecord = 0;
				int maxRecords = 1073741823;
				this.RecordCount = 0;
				this.RecordCount = this._DbConnection.OpenDataSet(this.RecordSql, this.m_recordSet, this.m_oldRecordSet, startRecord, maxRecords);
				this.ActivedIndex = 0;
				this.QueryRecordComplete(this.RecordSql);
			}
		}

		public virtual bool QueryRecordComplete(HSQL hsql)
		{
			return true;
		}

		public virtual bool PreQueryRecordSet(HSQL hsql)
		{
			return true;
		}

		public void QueryRecordSet()
		{
			bool flag = !this.PreQueryRecordSet(this.RecordSetSql);
			if (!flag)
			{
				DbDataAdapter adapter = this._DbConnection.OpenAdapter(this.RecordSetSql);
				adapter.TableMappings.Add("Table", this.Entity.Table);
				this.m_recordSet.Clear();
				this.BuildTable();
				int startRecord = 0;
				int maxRecords = 1073741823;
				bool flag2 = this.AllowPagingQuery && this.PageSize > 0;
				if (flag2)
				{
					startRecord = this.PageSize * ((this.PageIndex - 1 >= 0) ? (this.PageIndex - 1) : 0);
					maxRecords = this.PageSize * (this.PageIndex + 1) - startRecord;
				}
				this.RecordCount = 0;
				this.RecordCount = this._DbConnection.OpenDataSet(this.RecordSetSql, this.m_recordSet, this.m_oldRecordSet, startRecord, maxRecords);
				this.ActivedIndex = 0;
				this.QueryRecordSetComplete(this.RecordSetSql);
			}
		}

		public virtual bool QueryRecordSetComplete(HSQL hsql)
		{
			return true;
		}

		private void SetSqlParams(HSQL hsql)
		{
			foreach (string paramName in hsql.ParamList)
			{
				bool flag = this.RecordSet.Columns.Contains(paramName);
				if (flag)
				{
					bool beDoc = this.Entity.BeDoc;
					if (beDoc)
					{
						bool flag2 = paramName == this.Entity.Table + "_FGID";
						if (flag2)
						{
							hsql.ParamByName(paramName).Value = this.FileGroupId;
							continue;
						}
						bool flag3 = paramName == this.Entity.Table + "_FILES";
						if (flag3)
						{
							hsql.ParamByName(paramName).Value = this.GroupFiles;
							continue;
						}
					}
					bool flag4 = this.Fields.ContainsKey(paramName) && this.Fields[paramName].DbType == TimDbType.DateTime && string.IsNullOrEmpty(this.RecordSet.Rows[this.ActivedIndex][paramName].ToString().Trim());
					if (flag4)
					{
						hsql.ParamByName(paramName).Value = null;
					}
					else
					{
						hsql.ParamByName(paramName).Value = this.RecordSet.Rows[this.ActivedIndex][paramName].ToString();
					}
				}
				else
				{
					bool flag5 = paramName == this.Entity.Table + "_WFID";
					if (flag5)
					{
						hsql.ParamByName(paramName).Value = this.WorkflowId;
					}
					else
					{
						bool flag6 = paramName == this.Entity.Table + "_WFRUNID";
						if (flag6)
						{
							hsql.ParamByName(paramName).Value = this.WorkflowRunId;
						}
					}
				}
			}
		}

		protected string GetWfpExSetting(string wfId, string wfpId, string settingName)
		{
			string result = string.Empty;
			return WorkflowEngine.GetWfpExSetting(wfId, wfpId, settingName);
		}

		public virtual bool CanSubmit()
		{
			return true;
		}

		public virtual bool CanWithdraw()
		{
			return true;
		}

		public virtual bool Workflow_OnParseParameter(string pmName, out string pmValue)
		{
			pmValue = string.Empty;
			return false;
		}

		public virtual bool Workflow_OnPreAction(FlowActionParameter actionParameter)
		{
			return true;
		}

		public virtual bool Workflow_OnActionComplete(FlowActionParameter actionParameter)
		{
			return true;
		}

		public object GetField(string field)
		{
			return this.GetField(field, false);
		}

		public object GetField(string field, bool original)
		{
			bool flag = this.ActivedIndex < 0;
			if (flag)
			{
				throw new RowIndexException();
			}
			bool flag2 = !this.RecordSet.Columns.Contains(field);
			if (flag2)
			{
				throw new ColumnIndexException(field);
			}
			object result;
			if (original)
			{
				result = this.OldRecordSet.Rows[this.ActivedIndex][field];
			}
			else
			{
				result = this.RecordSet.Rows[this.ActivedIndex][field];
			}
			return result;
		}

		public void SetField(string field, object value)
		{
			this.RecordSet.Rows[this.ActivedIndex][field] = value;
		}

		protected void DisplayPrompt(string msg)
		{
			this.m_promptMessage += msg;
		}
	}
}
