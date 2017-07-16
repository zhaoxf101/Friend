using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace TIM.T_WEBCTRL
{
	public sealed class SqlClientInputStream : Stream
	{
		private SqlConnection _SqlConnection;

		private long _position;

		private SqlCommand _SqlCommand;

		private SqlParameter _SqlParameterData;

		private SqlParameter _SqlParameter;

		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		public override long Length
		{
			get
			{
				return this._position;
			}
		}

		public override long Position
		{
			get
			{
				return this._position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public SqlClientInputStream(string connectionString, string table, string dataField, string whereCriteria)
		{
			this._SqlConnection = new SqlConnection(connectionString);
			this._SqlCommand = this._SqlConnection.CreateCommand();
			this._SqlCommand.CommandText = string.Concat(new string[]
			{
				"UPDATE ",
				table,
				" SET ",
				dataField,
				"=NULL WHERE ",
				whereCriteria,
				";SELECT TEXTPTR(",
				dataField,
				") FROM ",
				table,
				" WHERE ",
				whereCriteria
			});
			byte[] buffer;
			try
			{
				this._SqlConnection.Open();
				buffer = (byte[])this._SqlCommand.ExecuteScalar();
			}
			finally
			{
				this._SqlConnection.Close();
			}
			this._SqlCommand.CommandText = string.Concat(new string[]
			{
				"UPDATETEXT ",
				table,
				".",
				dataField,
				" @ptr @offset NULL @data;"
			});
			SqlParameter parameter = this._SqlCommand.CreateParameter();
			parameter.DbType = DbType.Binary;
			parameter.ParameterName = "@ptr";
			parameter.Size = 16;
			parameter.Value = buffer;
			this._SqlCommand.Parameters.Add(parameter);
			this._SqlParameter = this._SqlCommand.CreateParameter();
			this._SqlParameter.DbType = DbType.Int32;
			this._SqlParameter.ParameterName = "@offset";
			this._SqlParameter.Size = 4;
			this._SqlCommand.Parameters.Add(this._SqlParameter);
			this._SqlParameterData = this._SqlCommand.CreateParameter();
			this._SqlParameterData.SqlDbType = SqlDbType.Image;
			this._SqlParameterData.ParameterName = "@data";
			this._SqlParameterData.Size = 8040;
			this._SqlCommand.Parameters.Add(this._SqlParameterData);
		}

		public SqlClientInputStream(string connectionString, string table, string dataField, string idField, int idValue) : this(connectionString, table, dataField, idField + "=" + idValue.ToString())
		{
		}

		public override void Close()
		{
			base.Close();
			this._SqlConnection.Dispose();
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int start, int count)
		{
			int num = 8040;
			int num2 = 0;
			this._SqlParameterData.Value = buffer;
			try
			{
				this._SqlConnection.Open();
				while (num2 < count)
				{
					bool flag = num > count - num2;
					if (flag)
					{
						num = count - num2;
					}
					this._SqlParameterData.Offset = start + num2;
					this._SqlParameterData.Size = num;
					this._SqlParameter.Value = this._position;
					this._SqlCommand.ExecuteNonQuery();
					this._position += (long)num;
					num2 += num;
				}
			}
			finally
			{
				this._SqlConnection.Close();
			}
		}
	}
}
