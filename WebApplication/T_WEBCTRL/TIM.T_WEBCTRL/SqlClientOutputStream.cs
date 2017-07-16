using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace TIM.T_WEBCTRL
{
	public sealed class SqlClientOutputStream : Stream
	{
		private SqlConnection _SqlConnection;

		private long _position;

		private SqlCommand _SqlCommand;

		private SqlParameter _SqlParameterSize;

		private long _executeScalar;

		private SqlParameter _SqlParameter;

		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public override long Length
		{
			get
			{
				return this._executeScalar;
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
				bool flag = this._position < 0L || this._position > this._executeScalar;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._position = value;
			}
		}

		public SqlClientOutputStream(string connectionString, string table, string dataField, string whereCriteria)
		{
			this._SqlConnection = new SqlConnection(connectionString);
			this._SqlCommand = this._SqlConnection.CreateCommand();
			this._SqlCommand.CommandText = string.Concat(new string[]
			{
				"SELECT DATALENGTH(",
				dataField,
				") FROM ",
				table,
				" WHERE ",
				whereCriteria
			});
			try
			{
				this._SqlConnection.Open();
				this._executeScalar = (long)((int)this._SqlCommand.ExecuteScalar());
			}
			finally
			{
				this._SqlConnection.Close();
			}
			this._SqlCommand.CommandText = string.Concat(new string[]
			{
				"SELECT TEXTPTR(",
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
				"READTEXT ",
				table,
				".",
				dataField,
				" @ptr @offset @size;"
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
			this._SqlParameterSize = this._SqlCommand.CreateParameter();
			this._SqlParameterSize.DbType = DbType.Int32;
			this._SqlParameterSize.ParameterName = "@size";
			this._SqlParameterSize.Size = 4;
			this._SqlCommand.Parameters.Add(this._SqlParameterSize);
		}

		public SqlClientOutputStream(string connectionString, string table, string dataField, string idField, int idValue) : this(connectionString, table, dataField, idField + "=" + idValue.ToString())
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
			bool flag = this._position > this._executeScalar;
			if (flag)
			{
				throw new InvalidOperationException("Tried to read past end of stream.");
			}
			bool flag2 = this._position == this._executeScalar;
			int result;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				bool flag3 = this._position + (long)count > this._executeScalar;
				int num;
				if (flag3)
				{
					num = (int)(this._executeScalar - this._position);
				}
				else
				{
					num = count;
				}
				this._SqlParameterSize.Value = num;
				this._SqlParameter.Value = this._position;
				byte[] src = null;
				try
				{
					this._SqlConnection.Open();
					src = (byte[])this._SqlCommand.ExecuteScalar();
				}
				finally
				{
					this._SqlConnection.Close();
				}
				Buffer.BlockCopy(src, 0, buffer, offset, num);
				this._position += (long)num;
				result = num;
			}
			return result;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
			{
				bool flag = offset < 0L || offset > this._executeScalar;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._position = offset;
				break;
			}
			case SeekOrigin.Current:
			{
				bool flag2 = this._position + offset < 0L || this._position + offset > this._executeScalar;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._position += offset;
				break;
			}
			case SeekOrigin.End:
			{
				bool flag3 = offset > 0L || this._executeScalar + offset < 0L;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._position = this._executeScalar + offset;
				break;
			}
			}
			return this._position;
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}
	}
}
