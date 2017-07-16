using System;
using System.Data;
using System.Data.SqlClient;

namespace TIM.T_WEBCTRL
{
	public sealed class SqlClientStatusManager : IStatusManager
	{
		private string _tablename;

		private string _statusfield;

		private string _connectionString;

		private string _lastUpdatedField;

		private string _keyfieldname;

		public SqlClientStatusManager(NameValueConfigurationSection configuration)
		{
			this._connectionString = configuration["connectionString"];
			this._tablename = configuration["table"];
			this._keyfieldname = configuration["keyField"];
			this._statusfield = configuration["statusField"];
			this._lastUpdatedField = configuration["lastUpdatedField"];
			UploadLog.Log("Using SqlClientStatusManager. Settings:");
			UploadLog.Log("connectionString: " + this._connectionString);
			UploadLog.Log("table: " + this._tablename);
			UploadLog.Log("keyField: " + this._keyfieldname);
			UploadLog.Log("statusField: " + this._statusfield);
			UploadLog.Log("lastUpdatedField: " + this._lastUpdatedField);
			UploadLog.Log("updateInterval: " + UtoUploadConfiguration.StatusManager.UpdateInterval.ToString());
		}

		public UploadStatus GetUploadStatus(string uploadId)
		{
			UploadStatus result;
			using (IDbConnection connection = new SqlConnection(this._connectionString))
			{
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = string.Concat(new string[]
					{
						"SELECT ",
						this._statusfield,
						" FROM ",
						this._tablename,
						" WHERE ",
						this._keyfieldname,
						"=@uploadId"
					});
					IDbDataParameter parameter = command.CreateParameter();
					parameter.DbType = DbType.String;
					parameter.ParameterName = "@uploadId";
					parameter.Value = uploadId;
					command.Parameters.Add(parameter);
					connection.Open();
					string text = command.ExecuteScalar() as string;
					UploadLog.Log("SqlStatus: returned uploadId=" + uploadId);
					bool flag = !string.IsNullOrEmpty(text);
					if (flag)
					{
						result = UploadStatus.GetUploadStatus(text);
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public void RemoveStaleStatus(int staleMinutes)
		{
			using (IDbConnection connection = new SqlConnection(this._connectionString))
			{
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = string.Concat(new string[]
					{
						"DELETE FROM ",
						this._tablename,
						" WHERE DATEDIFF(n, ",
						this._lastUpdatedField,
						", GETDATE()) > ",
						staleMinutes.ToString()
					});
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public void RemoveStatus(string uploadId)
		{
			using (IDbConnection connection = new SqlConnection(this._connectionString))
			{
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = string.Concat(new string[]
					{
						"DELETE FROM ",
						this._tablename,
						" WHERE ",
						this._keyfieldname,
						"=@uploadId"
					});
					IDbDataParameter parameter = command.CreateParameter();
					parameter.DbType = DbType.String;
					parameter.ParameterName = "@uploadId";
					parameter.Value = uploadId;
					command.Parameters.Add(parameter);
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
			UploadLog.Log("SqlStatus: removed uploadId=" + uploadId);
		}

		public void StatusChanged(UploadStatus status)
		{
			using (IDbConnection connection = new SqlConnection(this._connectionString))
			{
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = string.Concat(new string[]
					{
						"UPDATE ",
						this._tablename,
						" SET ",
						this._statusfield,
						"=@UploadStatus, ",
						this._lastUpdatedField,
						"=GETDATE() WHERE ",
						this._keyfieldname,
						"=@uploadId"
					});
					IDbDataParameter parameter = command.CreateParameter();
					parameter.DbType = DbType.String;
					parameter.ParameterName = "@uploadId";
					parameter.Value = status.UploadId;
					command.Parameters.Add(parameter);
					IDbDataParameter parameter2 = command.CreateParameter();
					parameter2.DbType = DbType.String;
					parameter2.ParameterName = "@UploadStatus";
					parameter2.Value = status.GetSerializeUploadedObjects();
					command.Parameters.Add(parameter2);
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
			UploadLog.Log("SqlStatus: updated uploadId=" + status.UploadId);
		}

		public void UploadStarted(UploadStatus status)
		{
			using (IDbConnection connection = new SqlConnection(this._connectionString))
			{
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = string.Concat(new string[]
					{
						"INSERT INTO ",
						this._tablename,
						"(",
						this._statusfield,
						",",
						this._keyfieldname,
						") VALUES (@UploadStatus,@uploadId)"
					});
					IDbDataParameter parameter = command.CreateParameter();
					parameter.DbType = DbType.String;
					parameter.ParameterName = "@uploadId";
					parameter.Value = status.UploadId;
					command.Parameters.Add(parameter);
					IDbDataParameter parameter2 = command.CreateParameter();
					parameter2.DbType = DbType.String;
					parameter2.ParameterName = "@UploadStatus";
					parameter2.Value = status.GetSerializeUploadedObjects();
					command.Parameters.Add(parameter2);
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
			UploadLog.Log("SqlStatus: inserted uploadId=" + status.UploadId);
		}
	}
}
