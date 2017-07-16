using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Data.Common;

namespace TIM.T_KERNEL.Data
{
	public class OdacDatabase : Database
	{
		public OdacDatabase(string dbId, string dbType, string connectString) : base(dbId, dbType, connectString, OracleClientFactory.Instance)
		{
		}

		protected override DbCommand PrepareCommand(HSQL sql)
		{
			DbCommand command = base.DbProviderFactory.CreateCommand();
			((OracleCommand)command).BindByName = true;
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
					oracleParameter.OracleDbType = OracleDbType.Long;
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
						oracleParameter2.OracleDbType = OracleDbType.LongRaw;
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
							oracleParameter3.OracleDbType = OracleDbType.Varchar2;
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
								oracleParameter4.OracleDbType = OracleDbType.NVarchar2;
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
									case TimDbType.Null:
									{
										bool flag14 = sql.ParamByName(sql.Params[index].ToString()).Value is DateTime;
										if (flag14)
										{
											parameter.DbType = DbType.Date;
										}
										break;
									}
									case TimDbType.VarChar:
										parameter.DbType = DbType.String;
										break;
									case TimDbType.DateTime:
										parameter.DbType = DbType.Date;
										break;
									case TimDbType.Float:
										parameter.DbType = DbType.Double;
										break;
									}
								}
								bool flag15 = sql.ParamByName(sql.Params[index].ToString()).Size != 0;
								if (flag15)
								{
									parameter.Size = sql.ParamByName(sql.Params[index].ToString()).Size;
								}
								bool flag16 = !string.IsNullOrEmpty(sql.ParamByName(sql.Params[index].ToString()).SourceColumn);
								if (flag16)
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
