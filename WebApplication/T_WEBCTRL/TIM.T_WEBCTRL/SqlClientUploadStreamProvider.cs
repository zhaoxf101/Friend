using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace TIM.T_WEBCTRL
{
	public sealed class SqlClientUploadStreamProvider : IUploadStreamProvider
	{
		public enum CriteriaMethod
		{
			Identity,
			Custom
		}

		private string _tablename;

		private SqlClientUploadStreamProvider.CriteriaMethod _criteriaMethod;

		private ICriteriaGenerator _criteriaGenerator;

		private string _filenamefield;

		private string _connectionString;

		private string _dataField;

		private string _keyfieldname;

		public const string WhereCriteriaKey = "whereCriteria";

		public SqlClientUploadStreamProvider(NameValueConfigurationSection configuration)
		{
			string text = configuration["criteriaMethod"];
			bool flag = text != null && text.Length != 0;
			if (flag)
			{
				this._criteriaMethod = (SqlClientUploadStreamProvider.CriteriaMethod)Enum.Parse(typeof(SqlClientUploadStreamProvider.CriteriaMethod), text, true);
			}
			else
			{
				this._criteriaMethod = SqlClientUploadStreamProvider.CriteriaMethod.Identity;
			}
			bool flag2 = this._criteriaMethod == SqlClientUploadStreamProvider.CriteriaMethod.Custom;
			if (flag2)
			{
				this._criteriaGenerator = (ConfigurationHashThread.CreateInstance(configuration["criteriaGenerator"], new object[]
				{
					configuration
				}) as ICriteriaGenerator);
				bool flag3 = this._criteriaGenerator == null;
				if (flag3)
				{
					throw new ApplicationException("无法对CriteriaGenerator进行实例化.");
				}
			}
			this._connectionString = configuration["connectionString"];
			this._tablename = configuration["table"];
			this._keyfieldname = configuration["keyField"];
			this._dataField = configuration["dataField"];
			this._filenamefield = configuration["fileNameField"];
		}

		public Stream GetInputStream(UploadedFile file)
		{
			return new SqlClientOutputStream(this._connectionString, this._tablename, this._dataField, file.LocationInfo["whereCriteria"]);
		}

		public Stream GetOutputStream(UploadedFile file)
		{
			bool flag = this._criteriaMethod == SqlClientUploadStreamProvider.CriteriaMethod.Identity;
			string text;
			if (flag)
			{
				using (SqlConnection connection = new SqlConnection(this._connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						StringBuilder builder = new StringBuilder();
						builder.Append("INSERT INTO ");
						builder.Append(this._tablename);
						builder.Append(" (");
						builder.Append(this._dataField);
						bool flag2 = this._filenamefield != null;
						if (flag2)
						{
							builder.Append(",");
							builder.Append(this._filenamefield);
						}
						builder.Append(") VALUES (NULL");
						bool flag3 = this._filenamefield != null;
						if (flag3)
						{
							builder.Append(",@fileName");
							SqlParameter parameter = command.CreateParameter();
							parameter.ParameterName = "@fileName";
							parameter.DbType = DbType.String;
							parameter.Value = file.ClientName;
							command.Parameters.Add(parameter);
						}
						builder.Append(");");
						builder.Append("SELECT CAST(SCOPE_IDENTITY() AS int);");
						command.CommandText = builder.ToString();
						connection.Open();
						text = this._keyfieldname + "=" + ((int)command.ExecuteScalar()).ToString();
					}
					goto IL_179;
				}
			}
			text = this._criteriaGenerator.GenerateCriteria(file);
			IL_179:
			Stream stream;
			try
			{
				file.LocationInfo["whereCriteria"] = text;
				stream = new SqlClientInputStream(this._connectionString, this._tablename, this._dataField, text);
			}
			catch
			{
				this.RemoveOutput(file);
				file.LocationInfo.Clear();
				throw;
			}
			return stream;
		}

		public void RemoveOutput(UploadedFile file)
		{
			bool flag = file.LocationInfo.Count > 0;
			if (flag)
			{
				using (SqlConnection connection = new SqlConnection(this._connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = "DELETE FROM " + this._tablename + " WHERE " + file.LocationInfo["whereCriteria"];
						connection.Open();
						command.ExecuteNonQuery();
					}
				}
			}
		}
	}
}
