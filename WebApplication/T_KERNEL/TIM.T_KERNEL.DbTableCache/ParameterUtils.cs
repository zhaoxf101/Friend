using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class ParameterUtils
	{
		internal static Parameter GetObject(DataRow row)
		{
			Parameter parameter = new Parameter();
			parameter.PmId = row["PARAMETER_PMID"].ToString().Trim();
			parameter.PmMc = row["PARAMETER_PMMC"].ToString().Trim();
			string a = row["PARAMETER_TYPE"].ToString().Trim();
			if (!(a == "S"))
			{
				if (!(a == "M"))
				{
					parameter.Type = ParameterType.Null;
				}
				else
				{
					parameter.Type = ParameterType.M;
				}
			}
			else
			{
				parameter.Type = ParameterType.S;
			}
			string a2 = row["PARAMETER_CONTROLTYPE"].ToString().Trim();
			if (!(a2 == "C"))
			{
				if (!(a2 == "N"))
				{
					if (!(a2 == "S"))
					{
						if (!(a2 == "M"))
						{
							if (!(a2 == "Y"))
							{
								parameter.ControlType = ParameterControlType.Null;
							}
							else
							{
								parameter.ControlType = ParameterControlType.Y;
								parameter.DefaultValue = ParameterUtils.GetColorValue(ParameterUtils.GetDefaultValue(row["PARAMETER_DEFAULT"].ToString().Trim()));
							}
						}
						else
						{
							parameter.ControlType = ParameterControlType.M;
							parameter.DefaultValue = ParameterUtils.GetDefaultValue(row["PARAMETER_DEFAULT"].ToString().Trim());
						}
					}
					else
					{
						parameter.ControlType = ParameterControlType.S;
						parameter.DefaultValue = ParameterUtils.GetDefaultValue(row["PARAMETER_DEFAULT"].ToString().Trim());
					}
				}
				else
				{
					parameter.ControlType = ParameterControlType.N;
					parameter.DefaultValue = row["PARAMETER_DEFAULT"].ToString().Trim();
				}
			}
			else
			{
				parameter.ControlType = ParameterControlType.C;
				parameter.DefaultValue = row["PARAMETER_DEFAULT"].ToString().Trim();
			}
			parameter.Desc = row["PARAMETER_DESC"].ToString().Trim();
			parameter.Values = row["PARAMETER_VALUES"].ToString().Trim();
			parameter.DefaultValue = row["PARAMETER_DEFAULT"].ToString().Trim();
			parameter.CreaterId = row["CREATERID"].ToString().Trim();
			parameter.Creater = row["CREATER"].ToString().Trim();
			parameter.CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime();
			parameter.ModifierId = row["MODIFIERID"].ToString().Trim();
			parameter.Modifier = row["MODIFIER"].ToString().Trim();
			parameter.ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime();
			return parameter;
		}

		internal static string GetDefaultValue(string value)
		{
			string str = "";
			char[] chArray = new char[]
			{
				','
			};
			string[] array = value.Split(chArray);
			for (int i = 0; i < array.Length; i++)
			{
				string str2 = array[i];
				str = ((str2.IndexOf(":") <= -1) ? ((!string.IsNullOrEmpty(str)) ? (str + "," + str2) : str2) : ((!string.IsNullOrEmpty(str)) ? (str + "," + str2.Substring(str2.IndexOf(":") + 1)) : str2.Substring(str2.IndexOf(":") + 1)));
			}
			return str;
		}

		internal static string GetColorValue(string value)
		{
			string str = string.Empty;
			bool flag = value.Length == 7 && value.Substring(0, 1) == "$";
			if (flag)
			{
				string str2 = value.Substring(1, 2);
				string str3 = value.Substring(3, 2);
				str = "#" + value.Substring(5, 2) + str3 + str2;
			}
			return str;
		}

		public static Parameter GetParameter(string pmId)
		{
			Parameter parameter = null;
			ParameterCache parameterCache = (ParameterCache)new ParameterCache().GetData();
			int index = parameterCache.dvParameterBy_PmId.Find(pmId);
			bool flag = index >= 0;
			if (flag)
			{
				Parameter parameter2 = new Parameter();
				parameter = ParameterUtils.GetObject(parameterCache.dvParameterBy_PmId[index].Row);
			}
			return parameter;
		}

		public static List<Parameter> GetParameters()
		{
			List<Parameter> list = new List<Parameter>();
			foreach (DataRow row in ((ParameterCache)new ParameterCache().GetData()).dtParameter.Rows)
			{
				Parameter parameter = new Parameter();
				Parameter @object = ParameterUtils.GetObject(row);
				list.Add(@object);
			}
			return list;
		}
	}
}
