using System;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.DbTableCache
{
	public class DbServer : DbEntity
	{
		public string DbId
		{
			get;
			set;
		}

		public string Desc
		{
			get;
			set;
		}

		public DbProviderType DbMS
		{
			get;
			set;
		}

		public string Conn
		{
			get;
			set;
		}
	}
}
