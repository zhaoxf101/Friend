using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace TIM.T_KERNEL.Data
{
	public class SqlDatabase : Database
	{
		public SqlDatabase(string dbId, string dbType, string connectString) : base(dbId, dbType, connectString, SqlClientFactory.Instance)
		{
		}

		protected override DbCommand PrepareCommand(HSQL sql)
		{
			DbCommand command = base.DbProviderFactory.CreateCommand();
			command.CommandTimeout = 180;
			command.Connection = this.m_connection;
			bool inTransaction = base.InTransaction;
			if (inTransaction)
			{
				command.Transaction = base.Transaction;
			}
			command.CommandType = CommandType.Text;
			command.CommandText = sql.ToString();
			for (int index = 0; index < sql.Params.Count; index++)
			{
				bool flag = sql.ParamByName(sql.Params[index].ToString()).ParamterType == TimDbType.Text || sql.ParamByName(sql.Params[index].ToString()).ParamterType == TimDbType.Long;
				if (flag)
				{
					SqlParameter sqlParameter = new SqlParameter("@" + sql.Params[index].ToString(), sql.ParamByName(sql.Params[index].ToString()).Value);
					sqlParameter.SqlDbType = SqlDbType.Text;
					bool flag2 = !string.IsNullOrEmpty(sql.ParamByName(sql.Params[index].ToString()).SourceColumn);
					if (flag2)
					{
						sqlParameter.SourceColumn = sql.ParamByName(sql.Params[index].ToString()).SourceColumn;
					}
					command.Parameters.Add(sqlParameter);
				}
				else
				{
					bool flag3 = sql.ParamByName(sql.Params[index].ToString()).ParamterType == TimDbType.LongRaw;
					if (flag3)
					{
						SqlParameter sqlParameter2 = new SqlParameter("@" + sql.Params[index].ToString(), sql.ParamByName(sql.Params[index].ToString()).Value);
						sqlParameter2.SqlDbType = SqlDbType.Image;
						bool flag4 = !string.IsNullOrEmpty(sql.ParamByName(sql.Params[index].ToString()).SourceColumn);
						if (flag4)
						{
							sqlParameter2.SourceColumn = sql.ParamByName(sql.Params[index].ToString()).SourceColumn;
						}
						command.Parameters.Add(sqlParameter2);
					}
					else
					{
						bool flag5 = sql.ParamByName(sql.Params[index].ToString()).ParamterType == TimDbType.NVarChar;
						if (flag5)
						{
							SqlParameter sqlParameter3 = new SqlParameter("@" + sql.Params[index].ToString(), sql.ParamByName(sql.Params[index].ToString()).Value);
							sqlParameter3.SqlDbType = SqlDbType.NVarChar;
							bool flag6 = sql.ParamByName(sql.Params[index].ToString()).Size != 0;
							if (flag6)
							{
								sqlParameter3.Size = sql.ParamByName(sql.Params[index].ToString()).Size;
							}
							bool flag7 = !string.IsNullOrEmpty(sql.ParamByName(sql.Params[index].ToString()).SourceColumn);
							if (flag7)
							{
								sqlParameter3.SourceColumn = sql.ParamByName(sql.Params[index].ToString()).SourceColumn;
							}
							command.Parameters.Add(sqlParameter3);
						}
						else
						{
							DbParameter parameter = base.CreateParameter("@" + sql.Params[index].ToString(), sql.ParamByName(sql.Params[index].ToString()).Value);
							bool flag8 = parameter.DbType == DbType.String;
							if (flag8)
							{
								parameter.DbType = DbType.AnsiString;
							}
							else
							{
								switch (sql.ParamByName(sql.Params[index].ToString()).ParamterType)
								{
								case TimDbType.VarChar:
									parameter.DbType = DbType.String;
									break;
								case TimDbType.DateTime:
									parameter.DbType = DbType.DateTime;
									break;
								case TimDbType.Float:
									parameter.DbType = DbType.Double;
									break;
								}
							}
							bool flag9 = sql.ParamByName(sql.Params[index].ToString()).Size != 0 && sql.ParamByName(sql.Params[index].ToString()).ParamterType != TimDbType.Float;
							if (flag9)
							{
								parameter.Size = sql.ParamByName(sql.Params[index].ToString()).Size;
							}
							bool flag10 = !string.IsNullOrEmpty(sql.ParamByName(sql.Params[index].ToString()).SourceColumn);
							if (flag10)
							{
								parameter.SourceColumn = sql.ParamByName(sql.Params[index].ToString()).SourceColumn;
							}
							command.Parameters.Add(parameter);
						}
					}
				}
			}
			return command;
		}
	}
}
