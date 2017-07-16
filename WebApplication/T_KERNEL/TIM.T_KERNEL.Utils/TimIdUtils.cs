using System;
using System.Data;
using TIM.T_KERNEL.Data;

namespace TIM.T_KERNEL.Utils
{
	public class TimIdUtils
	{
		public static int GenUtoId(string name)
		{
			int num = -1;
			Database database = LogicContext.GetDatabase();
			int result;
			try
			{
				HSQL sql = new HSQL(database);
				sql.Clear();
				sql.Add("SELECT UTOID_VALUE FROM UTOID WHERE UTOID_NAME = :UTOID_NAME");
				sql.ParamByName("UTOID_NAME").Value = name;
				DataSet dataSet = database.OpenDataSet(sql);
				bool flag = dataSet.Tables[0].Rows.Count == 0;
				if (flag)
				{
					try
					{
						sql.Clear();
						sql.Add("INSERT INTO UTOID(UTOID_NAME, UTOID_VALUE) values(:UTOID_NAME,0)");
						sql.ParamByName("UTOID_NAME").Value = name;
						database.ExecSQL(sql);
						num = 0;
					}
					catch
					{
					}
				}
				else
				{
					num = Convert.ToInt32(dataSet.Tables[0].Rows[0]["UTOID_VALUE"].ToString().Trim());
				}
				sql.Clear();
				sql.Add("UPDATE UTOID SET UTOID_VALUE = UTOID_VALUE + 1  WHERE UTOID_NAME = :UTOID_NAME and UTOID_VALUE = :UTOID_VALUE");
				sql.ParamByName("UTOID_NAME").Value = name;
				sql.ParamByName("UTOID_VALUE").Value = num;
				while (database.ExecSQL(sql) < 1)
				{
					num++;
					sql.Clear();
					sql.Add("UPDATE UTOID SET UTOID_VALUE = UTOID_VALUE + 1  WHERE UTOID_NAME = :UTOID_NAME and UTOID_VALUE = :UTOID_VALUE");
					sql.ParamByName("UTOID_NAME").Value = name;
					sql.ParamByName("UTOID_VALUE").Value = num;
				}
				result = num + 1;
			}
			catch (Exception ex_161)
			{
				throw new Exception("获取最大号失败！");
			}
			return result;
		}

		public static string GenUtoId(string name, string prefix, int length)
		{
			string str = string.Empty;
			return prefix + TimIdUtils.GenUtoId(name).ToString().PadLeft(length - prefix.Length, '0');
		}

		public static string GenUtoIdPrefixAndYM(string prefix, string year, string month)
		{
			string str = string.Empty;
			string name = prefix + year.PadLeft(4, '0') + month.PadLeft(2, '0');
			return name + TimIdUtils.GenUtoId(name).ToString().PadLeft(4, '0');
		}

		public static string GenUtoIdPrefixYMDAndNameY(string name)
		{
			string str = string.Empty;
			DateTime now = DateTime.Now;
			string str2 = now.Year.ToString().Substring(2, 2);
			string str3 = str2.PadLeft(2, '0');
			string str4 = now.Month.ToString().PadLeft(2, '0');
			string str5 = now.Day.ToString().PadLeft(2, '0');
			string str6 = TimIdUtils.GenUtoId(name.ToUpper() + str2).ToString().PadLeft(8, '0');
			return str3 + str4 + str5 + str6;
		}
	}
}
