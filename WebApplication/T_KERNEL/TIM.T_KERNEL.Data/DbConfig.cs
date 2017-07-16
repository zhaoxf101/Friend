using System;
using TIM.T_KERNEL.Common;

namespace TIM.T_KERNEL.Data
{
	internal sealed class DbConfig
	{
		private string m_dbId = string.Empty;

		private string m_dbName = string.Empty;

		private DbProviderType m_providerType = DbProviderType.NULL;

		private string m_dbParam = string.Empty;

		private string _type = string.Empty;

		private string _serverName = string.Empty;

		private string _databaseName = string.Empty;

		private string _userName = string.Empty;

		private string _password = string.Empty;

		private string _connectionString = string.Empty;

		public string DbId
		{
			get
			{
				return this.m_dbId;
			}
			private set
			{
				this.m_dbId = value;
			}
		}

		public string DbName
		{
			get
			{
				return this.m_dbName;
			}
			private set
			{
				this.m_dbName = value;
			}
		}

		public DbProviderType ProviderType
		{
			get
			{
				return this.m_providerType;
			}
			private set
			{
				this.m_providerType = value;
			}
		}

		public string DbParam
		{
			get
			{
				return this.m_dbParam;
			}
			private set
			{
				this.m_dbParam = value;
			}
		}

		public string ConnectionString
		{
			get
			{
				this.AnalyzingDb();
				return this._connectionString;
			}
		}

		public DbConfig(string dbId, string dbName, DbProviderType dbProviderType, string dbParam)
		{
			bool flag = string.IsNullOrEmpty(dbId);
			if (flag)
			{
				throw new Exception("数据库编码不允许为空！");
			}
			bool flag2 = dbProviderType == DbProviderType.NULL;
			if (flag2)
			{
				throw new Exception("数据库存储引擎未指定或不支持！");
			}
			bool flag3 = string.IsNullOrEmpty(dbParam);
			if (flag3)
			{
				throw new Exception("数据库连接参数未配置");
			}
			this.DbId = dbId;
			this.DbName = dbName;
			this.ProviderType = dbProviderType;
			this.DbParam = dbParam;
		}

		private void AnalyzingDb()
		{
			NameValueString nameValueString = new NameValueString();
			nameValueString.LineText = this.DbParam;
			this._serverName = nameValueString["SERVER NAME"];
			this._databaseName = nameValueString["DATABASE NAME"];
			this._userName = nameValueString["USER NAME"];
			this._password = nameValueString["PASSWORD"];
			bool flag = this.ProviderType == DbProviderType.MSSQL;
			if (flag)
			{
				this._connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", new object[]
				{
					this._serverName,
					this._databaseName,
					this._userName,
					this._password
				});
			}
			else
			{
				bool flag2 = this.ProviderType != DbProviderType.ORACLE;
				if (!flag2)
				{
					this._connectionString = string.Format("Data Source={0};User Id={1};Password={2};", this._serverName, this._userName, this._password);
				}
			}
		}
	}
}
