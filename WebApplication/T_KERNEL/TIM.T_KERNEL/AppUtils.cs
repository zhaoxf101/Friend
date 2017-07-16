using System;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL
{
	internal class AppUtils
	{
		internal static DateTime GetDbServerDateTime()
		{
			DateTime dateTime = DateTime.Now;
			try
			{
				Database database = LogicContext.GetDatabase(new DbConfig("DBCFG", AppConfig.DefaultDbDesc, AppConfig.DbMS, AppConfig.DbServer));
				HSQL sql = new HSQL(database);
				sql.Clear();
				sql.Raw = true;
				bool flag = database.Driver == DbProviderType.MSSQL;
				if (flag)
				{
					sql.Add("select getdate()");
				}
				else
				{
					bool flag2 = database.Driver == DbProviderType.ORACLE;
					if (flag2)
					{
						sql.Add("select sysdate from dual");
					}
				}
				object obj = database.ExecScalar(sql);
				bool flag3 = obj != null;
				if (flag3)
				{
					dateTime = (DateTime)obj;
				}
			}
			catch
			{
				dateTime = DateTime.Now;
			}
			return dateTime;
		}
	}
}
