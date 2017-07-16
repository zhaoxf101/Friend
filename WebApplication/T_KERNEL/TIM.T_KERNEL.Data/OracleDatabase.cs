using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;

namespace TIM.T_KERNEL.Data
{
	public class OracleDatabase : Database
	{
		public OracleDatabase(string dbId, string dbType, string connectString, DbProviderFactory dbProviderFactory) : base(dbId, dbType, connectString, OracleClientFactory.Instance)
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
				bool flag = sql.ParamByName(sql.Params[index].ToString()).ParamterType == TimDbType.Long;
				if (flag)
				{
					OracleParameter oracleParameter = new OracleParameter(sql.Params[index].ToString(), sql.ParamByName(sql.Params[index].ToString()).Value);
					oracleParameter.OracleType = OracleType.LongVarChar;
					bool flag2 = sql.ParamByName(sql.Params[index].ToString()).Size != 0;
					if (flag2)
					{
						oracleParameter.Size = sql.ParamByName(sql.Params[index].ToString()).Size;
					}
					bool flag3 = !string.IsNullOrEmpty(sql.ParamByName(sql.Params[index].ToString()).SourceColumn);
					if (flag3)
					{
						oracleParameter.SourceColumn = sql.ParamByName(sql.Params[index].ToString()).SourceColumn;
					}
					command.Parameters.Add(oracleParameter);
				}
				else
				{
					bool flag4 = sql.ParamByName(sql.Params[index].ToString()).ParamterType == TimDbType.LongRaw;
					if (flag4)
					{
						OracleParameter oracleParameter2 = new OracleParameter(sql.Params[index].ToString(), sql.ParamByName(sql.Params[index].ToString()).Value);
						oracleParameter2.OracleType = OracleType.LongRaw;
						bool flag5 = sql.ParamByName(sql.Params[index].ToString()).Size != 0;
						if (flag5)
						{
							oracleParameter2.Size = sql.ParamByName(sql.Params[index].ToString()).Size;
						}
						bool flag6 = !string.IsNullOrEmpty(sql.ParamByName(sql.Params[index].ToString()).SourceColumn);
						if (flag6)
						{
							oracleParameter2.SourceColumn = sql.ParamByName(sql.Params[index].ToString()).SourceColumn;
						}
						command.Parameters.Add(oracleParameter2);
					}
					else
					{
						bool flag7 = sql.ParamByName(sql.Params[index].ToString()).ParamterType == TimDbType.Text;
						if (flag7)
						{
							OracleParameter oracleParameter3 = new OracleParameter(sql.Params[index].ToString(), sql.ParamByName(sql.Params[index].ToString()).Value);
							oracleParameter3.OracleType = OracleType.VarChar;
							bool flag8 = sql.ParamByName(sql.Params[index].ToString()).Size != 0;
							if (flag8)
							{
								oracleParameter3.Size = sql.ParamByName(sql.Params[index].ToString()).Size;
							}
							else
							{
								oracleParameter3.Size = 4000;
							}
							bool flag9 = !string.IsNullOrEmpty(sql.ParamByName(sql.Params[index].ToString()).SourceColumn);
							if (flag9)
							{
								oracleParameter3.SourceColumn = sql.ParamByName(sql.Params[index].ToString()).SourceColumn;
							}
							command.Parameters.Add(oracleParameter3);
						}
						else
						{
							bool flag10 = sql.ParamByName(sql.Params[index].ToString()).ParamterType == TimDbType.NVarChar;
							if (flag10)
							{
								OracleParameter oracleParameter4 = new OracleParameter(sql.Params[index].ToString(), sql.ParamByName(sql.Params[index].ToString()).Value);
								oracleParameter4.OracleType = OracleType.NVarChar;
								bool flag11 = sql.ParamByName(sql.Params[index].ToString()).Size != 0;
								if (flag11)
								{
									oracleParameter4.Size = sql.ParamByName(sql.Params[index].ToString()).Size;
								}
								bool flag12 = !string.IsNullOrEmpty(sql.ParamByName(sql.Params[index].ToString()).SourceColumn);
								if (flag12)
								{
									oracleParameter4.SourceColumn = sql.ParamByName(sql.Params[index].ToString()).SourceColumn;
								}
								command.Parameters.Add(oracleParameter4);
							}
							else
							{
								DbParameter parameter = base.CreateParameter(sql.Params[index].ToString(), sql.ParamByName(sql.Params[index].ToString()).Value);
								bool flag13 = parameter.DbType == DbType.String;
								if (flag13)
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
								bool flag14 = sql.ParamByName(sql.Params[index].ToString()).Size != 0 && sql.ParamByName(sql.Params[index].ToString()).ParamterType != TimDbType.Float;
								if (flag14)
								{
									parameter.Size = sql.ParamByName(sql.Params[index].ToString()).Size;
								}
								bool flag15 = !string.IsNullOrEmpty(sql.ParamByName(sql.Params[index].ToString()).SourceColumn);
								if (flag15)
								{
									parameter.SourceColumn = sql.ParamByName(sql.Params[index].ToString()).SourceColumn;
								}
								command.Parameters.Add(parameter);
							}
						}
					}
				}
			}
			return command;
		}
	}
}
