using System;
using System.Data;
using System.Data.Common;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.Data
{
	public abstract class Database : IDisposable
	{
		private string m_dbId;

		private string m_dbDriver;

		private string m_connectionString;

		private DbProviderFactory m_dbProviderFactory;

		public DbConnection m_connection;

		private DbTransaction m_transaction;

		private bool m_inTransaction;

		public HSQL LastSQL;

		public bool Connected
		{
			get
			{
				return this.IsConnected();
			}
		}

		public bool InTransaction
		{
			get
			{
				return this.m_inTransaction;
			}
		}

		public string DriverName
		{
			get
			{
				return this.m_dbDriver;
			}
		}

		public DbProviderType Driver
		{
			get
			{
				return this.m_dbDriver.ToDbProviderType();
			}
		}

		public string DatabaseName
		{
			get
			{
				return this.m_dbId;
			}
		}

		public DbProviderFactory DbProviderFactory
		{
			get
			{
				return this.m_dbProviderFactory;
			}
		}

		public DbTransaction Transaction
		{
			get
			{
				return this.m_transaction;
			}
		}

		public Database(string dbId, string dbType, string connectString, DbProviderFactory dbProviderFactory)
		{
			this.m_dbId = dbId;
			this.m_dbDriver = dbType;
			this.m_connectionString = connectString;
			this.m_dbProviderFactory = dbProviderFactory;
			this.m_connection = this.m_dbProviderFactory.CreateConnection();
			this.m_connection.ConnectionString = this.m_connectionString;
			this.m_inTransaction = false;
		}

		public void Dispose()
		{
			bool inTransaction = this.InTransaction;
			if (inTransaction)
			{
				try
				{
					this.RollbackTrans();
				}
				catch
				{
					throw new Exception("事务已结束。错误语句:" + this.LastSQL.Text);
				}
				this.CloseConnection();
				throw new Exception("事务未结束。");
			}
			bool flag = !this.Connected;
			if (!flag)
			{
				this.CloseConnection();
			}
		}

		private bool IsConnected()
		{
			return (this.m_connection.State & ConnectionState.Open) == ConnectionState.Open;
		}

		private void RunUpdateDateFormat()
		{
			try
			{
				DbCommand command = this.m_dbProviderFactory.CreateCommand();
				command.CommandTimeout = 180;
				command.Connection = this.m_connection;
				command.CommandType = CommandType.Text;
				command.CommandText = "alter session set NLS_DATE_FORMAT ='YYYY-MM-DD HH24:MI:SS'";
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void OpenConnection()
		{
			bool flag = this.IsConnected();
			if (!flag)
			{
				try
				{
					this.m_connection.Open();
					bool flag2 = this.m_dbDriver.Trim().Equals("ORACLE");
					if (flag2)
					{
						this.RunUpdateDateFormat();
					}
				}
				catch (DbException ex)
				{
					throw new Exception("数据库连接失败，请检查数据库配置和网络连接。", ex);
				}
			}
		}

		public void CloseConnection()
		{
			bool flag = !this.IsConnected();
			if (!flag)
			{
				bool inTransaction = this.m_inTransaction;
				if (inTransaction)
				{
					this.RollbackTrans();
					this.m_connection.Close();
					throw new Exception("关闭数据库连接前事务未结束！");
				}
				this.m_connection.Close();
			}
		}

		public void BeginTrans()
		{
			bool inTransaction = this.m_inTransaction;
			if (inTransaction)
			{
				throw new Exception("数据库连接已经在事务中，事务不应该嵌套！");
			}
			this.OpenConnection();
			this.m_transaction = this.m_connection.BeginTransaction();
			this.m_inTransaction = true;
		}

		public void CommitTrans()
		{
			bool flag = !this.m_inTransaction;
			if (flag)
			{
				throw new Exception("数据库连接不在事务中，无效提交操作！");
			}
			this.m_transaction.Commit();
			this.m_inTransaction = false;
			this.m_transaction = null;
			this.m_connection.Close();
		}

		public void RollbackTrans()
		{
			bool flag = !this.m_inTransaction;
			if (flag)
			{
				throw new Exception("数据库连接不在事务中，无效回滚操作。");
			}
			try
			{
				this.m_transaction.Rollback();
			}
			catch
			{
			}
			this.m_inTransaction = false;
			this.m_transaction = null;
			this.m_connection.Close();
		}

		public virtual DbDataAdapter OpenAdapter(HSQL sql)
		{
			this.LastSQL = sql;
			DbCommand dbCommand = this.PrepareCommand(sql);
			bool flag = !this.Connected;
			if (flag)
			{
				this.OpenConnection();
			}
			DbDataAdapter dataAdapter = this.m_dbProviderFactory.CreateDataAdapter();
			dataAdapter.SelectCommand = dbCommand;
			bool flag2 = !this.m_inTransaction;
			if (flag2)
			{
				this.CloseConnection();
			}
			return dataAdapter;
		}

		public int ExecSQL(HSQL sql)
		{
			this.LastSQL = sql;
			int num;
			try
			{
				DbCommand dbCommand = this.PrepareCommand(sql);
				bool flag = !this.Connected;
				if (flag)
				{
					this.OpenConnection();
				}
				num = dbCommand.ExecuteNonQuery();
				bool flag2 = !this.InTransaction;
				if (flag2)
				{
					this.CloseConnection();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return num;
		}

		public DataSet OpenDataSet(HSQL sql)
		{
			DataSet dataSet = new DataSet();
			this.LastSQL = sql;
			try
			{
				DbCommand dbCommand = this.PrepareCommand(sql);
				bool flag = !this.Connected;
				if (flag)
				{
					this.OpenConnection();
				}
				DbDataAdapter dataAdapter = this.m_dbProviderFactory.CreateDataAdapter();
				dataAdapter.SelectCommand = dbCommand;
				dataAdapter.Fill(dataSet);
				bool flag2 = !this.m_inTransaction;
				if (flag2)
				{
					this.CloseConnection();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return dataSet;
		}

		public int OpenDataSet(HSQL sql, DataTable dt, int startRecord, int maxRecords)
		{
			int num = 0;
			DbCommand cmd;
			using (DbDataReader dbDataReader = this.OpenReader(sql, out cmd))
			{
				for (int ordinal = 0; ordinal < dbDataReader.FieldCount; ordinal++)
				{
					bool flag = !dt.Columns.Contains(dbDataReader.GetName(ordinal));
					if (flag)
					{
						dt.Columns.Add(dbDataReader.GetName(ordinal));
					}
				}
				while (dbDataReader.Read())
				{
					num++;
					bool flag2 = !(num < startRecord + 1 | num >= startRecord + maxRecords + 1);
					if (flag2)
					{
						DataRow row = dt.NewRow();
						for (int ordinal2 = 0; ordinal2 < dbDataReader.FieldCount; ordinal2++)
						{
							bool flag3 = dt.Columns.Contains(dbDataReader.GetName(ordinal2));
							if (flag3)
							{
								row[dbDataReader.GetName(ordinal2)] = dbDataReader[ordinal2];
							}
						}
						dt.Rows.Add(row);
					}
				}
				bool flag4 = cmd != null;
				if (flag4)
				{
					cmd.Cancel();
				}
			}
			return num;
		}

		public int OpenDataSet(HSQL sql, DataTable dt, DataTable seDt, int startRecord, int maxRecords)
		{
			int num = 0;
			DbCommand cmd;
			using (DbDataReader dbDataReader = this.OpenReader(sql, out cmd))
			{
				for (int ordinal = 0; ordinal < dbDataReader.FieldCount; ordinal++)
				{
					bool flag = !dt.Columns.Contains(dbDataReader.GetName(ordinal));
					if (flag)
					{
						dt.Columns.Add(dbDataReader.GetName(ordinal));
					}
					bool flag2 = !seDt.Columns.Contains(dbDataReader.GetName(ordinal));
					if (flag2)
					{
						seDt.Columns.Add(dbDataReader.GetName(ordinal));
					}
				}
				while (dbDataReader.Read())
				{
					num++;
					bool flag3 = !(num < startRecord + 1 | num >= startRecord + maxRecords + 1);
					if (flag3)
					{
						DataRow row = dt.NewRow();
						DataRow row2 = seDt.NewRow();
						for (int ordinal2 = 0; ordinal2 < dbDataReader.FieldCount; ordinal2++)
						{
							bool flag4 = dt.Columns.Contains(dbDataReader.GetName(ordinal2));
							if (flag4)
							{
								row[dbDataReader.GetName(ordinal2)] = dbDataReader[ordinal2];
								row2[dbDataReader.GetName(ordinal2)] = dbDataReader[ordinal2];
							}
						}
						dt.Rows.Add(row);
						seDt.Rows.Add(row2);
					}
				}
				bool flag5 = cmd != null;
				if (flag5)
				{
					cmd.Cancel();
				}
			}
			return num;
		}

		public DbDataReader OpenReader(HSQL sql)
		{
			this.LastSQL = sql;
			DbDataReader dbDataReader;
			try
			{
				DbCommand dbCommand = this.PrepareCommand(sql);
				bool flag = !this.Connected;
				if (flag)
				{
					this.OpenConnection();
				}
				dbDataReader = ((!this.m_inTransaction) ? dbCommand.ExecuteReader(CommandBehavior.CloseConnection) : dbCommand.ExecuteReader());
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return dbDataReader;
		}

		public DbDataReader OpenReader(HSQL sql, out DbCommand cmd)
		{
			this.LastSQL = sql;
			DbDataReader dbDataReader;
			try
			{
				cmd = this.PrepareCommand(sql);
				bool flag = !this.Connected;
				if (flag)
				{
					this.OpenConnection();
				}
				dbDataReader = ((!this.m_inTransaction) ? cmd.ExecuteReader(CommandBehavior.CloseConnection) : cmd.ExecuteReader());
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return dbDataReader;
		}

		public object ExecScalar(HSQL sql)
		{
			this.LastSQL = sql;
			object obj;
			try
			{
				DbCommand dbCommand = this.PrepareCommand(sql);
				bool flag = !this.Connected;
				if (flag)
				{
					this.OpenConnection();
				}
				obj = dbCommand.ExecuteScalar();
				bool flag2 = !this.InTransaction;
				if (flag2)
				{
					this.CloseConnection();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return obj;
		}

		protected virtual DbCommand PrepareCommand(HSQL sql)
		{
			DbCommand command = this.m_dbProviderFactory.CreateCommand();
			command.CommandTimeout = 180;
			command.Connection = this.m_connection;
			bool inTransaction = this.m_inTransaction;
			if (inTransaction)
			{
				command.Transaction = this.m_transaction;
			}
			command.CommandType = CommandType.Text;
			command.CommandText = sql.ToString();
			for (int index = 0; index < sql.Params.Count; index++)
			{
				DbParameter parameter = this.CreateParameter("@" + sql.Params[index].ToString(), sql.ParamByName(sql.Params[index].ToString()).Value);
				command.Parameters.Add(parameter);
			}
			return command;
		}

		public virtual string BuildParameterName(string name)
		{
			return name;
		}

		protected DbParameter CreateParameter(string name)
		{
			DbParameter parameter = this.m_dbProviderFactory.CreateParameter();
			parameter.ParameterName = this.BuildParameterName(name);
			return parameter;
		}

		protected DbParameter CreateParameter(string name, object value)
		{
			DbParameter parameter = this.CreateParameter(name);
			parameter.Value = ((value == null) ? DBNull.Value : value);
			return parameter;
		}
	}
}
