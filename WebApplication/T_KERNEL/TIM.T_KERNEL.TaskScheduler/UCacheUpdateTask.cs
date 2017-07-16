using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Caching;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.TaskScheduler
{
	internal class UCacheUpdateTask : ITask
	{
		public int MdId
		{
			get
			{
				return 101000003;
			}
		}

		public string ComId
		{
			get
			{
				return "T_KERNEL";
			}
		}

		public void Execute()
		{
			Dictionary<string, DateTime> dictionary = new Dictionary<string, DateTime>(StringComparer.InvariantCultureIgnoreCase);
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Clear();
			sql.Raw = true;
			bool flag = database.Driver == DbProviderType.MSSQL;
			if (flag)
			{
				sql.Add("select UCACHE_TABLENAME,UCACHE_UPDTIME,getdate() as DBTIME from UCACHE");
			}
			else
			{
				bool flag2 = database.Driver == DbProviderType.ORACLE;
				if (flag2)
				{
					sql.Add("select UCACHE_TABLENAME,UCACHE_UPDTIME,sysdate as DBTIME from UCACHE");
				}
			}
			DataSet dataSet = database.OpenDataSet(sql);
			for (int index = 0; index < dataSet.Tables[0].Rows.Count; index++)
			{
				DataRow dataRow = dataSet.Tables[0].Rows[index];
				bool flag3 = index == 0 && dataRow["DBTIME"] != DBNull.Value;
				if (flag3)
				{
					AppRuntime.ServerDateTime = (DateTime)dataRow["DBTIME"];
				}
				bool flag4 = dataRow["UCACHE_UPDTIME"] != DBNull.Value;
				if (flag4)
				{
					string key = dataRow["UCACHE_TABLENAME"].ToString().Trim();
					DateTime dateTime = (DateTime)dataRow["UCACHE_UPDTIME"];
					dictionary.Add(key, dateTime);
				}
			}
			UCacheTimeManager.TableUpdateTime = dictionary;
		}

		public void Init()
		{
		}
	}
}
