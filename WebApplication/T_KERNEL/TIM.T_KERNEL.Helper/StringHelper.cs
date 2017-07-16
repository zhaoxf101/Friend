using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.DbTableCache;

namespace TIM.T_KERNEL.Helper
{
	public static class StringHelper
	{
        internal static uint ComputeStringHash(string s)
        {
            uint num = 0;
            if (s != null)
            {
                num = 0x811c9dc5;
                for (int i = 0; i < s.Length; i++)
                {
                    num = (s[i] ^ num) * 0x1000193;
                }
            }
            return num;
        }

        public static bool ToBool(this string value)
		{
			bool flag = false;
			bool flag3 = string.IsNullOrWhiteSpace(value);
			bool result;
			if (flag3)
			{
				result = flag;
			}
			else
			{
				value = value.Trim();
				string text = value;
				uint num = ComputeStringHash(text);
				if (num <= 1303515621u)
				{
					if (num <= 348981803u)
					{
						if (num != 184981848u)
						{
							if (num != 348981803u)
							{
								goto IL_1A7;
							}
							if (!(text == "-1"))
							{
								goto IL_1A7;
							}
							goto IL_1A3;
						}
						else
						{
							if (!(text == "false"))
							{
								goto IL_1A7;
							}
							goto IL_1A3;
						}
					}
					else if (num != 873244444u)
					{
						if (num != 890022063u)
						{
							if (num != 1303515621u)
							{
								goto IL_1A7;
							}
							if (!(text == "true"))
							{
								goto IL_1A7;
							}
						}
						else
						{
							if (!(text == "0"))
							{
								goto IL_1A7;
							}
							goto IL_1A3;
						}
					}
					else if (!(text == "1"))
					{
						goto IL_1A7;
					}
				}
				else if (num <= 3453902341u)
				{
					if (num != 2541049336u)
					{
						if (num != 3406561745u)
						{
							if (num != 3453902341u)
							{
								goto IL_1A7;
							}
							if (!(text == "True"))
							{
								goto IL_1A7;
							}
						}
						else
						{
							if (!(text == "N"))
							{
								goto IL_1A7;
							}
							goto IL_1A3;
						}
					}
					else
					{
						if (!(text == "False"))
						{
							goto IL_1A7;
						}
						goto IL_1A3;
					}
				}
				else if (num != 3691781268u)
				{
					if (num != 3943445553u)
					{
						if (num != 4228665076u)
						{
							goto IL_1A7;
						}
						if (!(text == "y"))
						{
							goto IL_1A7;
						}
					}
					else
					{
						if (!(text == "n"))
						{
							goto IL_1A7;
						}
						goto IL_1A3;
					}
				}
				else if (!(text == "Y"))
				{
					goto IL_1A7;
				}
				bool flag2 = true;
				goto IL_1AB;
				IL_1A3:
				flag2 = false;
				goto IL_1AB;
				IL_1A7:
				flag2 = false;
				IL_1AB:
				result = flag2;
			}
			return result;
		}

		public static int ToInt(this string value)
		{
			int result = 0;
			bool flag = string.IsNullOrWhiteSpace(value) || !int.TryParse(value, out result);
			int result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				result2 = result;
			}
			return result2;
		}

		public static float ToFloat(this string value)
		{
			float result = 0f;
			bool flag = string.IsNullOrWhiteSpace(value) || !float.TryParse(value, out result);
			float result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				result2 = result;
			}
			return result2;
		}

		public static double ToDouble(this string value)
		{
			double result = 0.0;
			bool flag = string.IsNullOrWhiteSpace(value) || !double.TryParse(value, out result);
			double result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				result2 = result;
			}
			return result2;
		}

		public static long ToLong(this string value)
		{
			long result = 0L;
			bool flag = string.IsNullOrWhiteSpace(value) || !long.TryParse(value, out result);
			long result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				result2 = result;
			}
			return result2;
		}

		public static decimal ToDecimal(this string value)
		{
			decimal result = new decimal(0);
			bool flag = string.IsNullOrWhiteSpace(value) || !decimal.TryParse(value, out result);
			decimal result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				result2 = result;
			}
			return result2;
		}

		public static DateTime ToDateTime(this string value)
		{
			DateTime result = AppRuntime.UltDateTime;
			bool flag = string.IsNullOrWhiteSpace(value) || DateTime.TryParse(value, out result);
			DateTime result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				result = AppRuntime.UltDateTime;
				result2 = result;
			}
			return result2;
		}

		public static DateTime ToMinDateTime(this string value)
		{
			DateTime result = AppRuntime.UltDateTime;
			bool flag = string.IsNullOrWhiteSpace(value);
			DateTime result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				bool flag2 = !DateTime.TryParse(value, out result);
				if (flag2)
				{
					result = AppRuntime.UltDateTime;
				}
				result2 = result.Date;
			}
			return result2;
		}

		public static DateTime ToMaxDateTime(this string value)
		{
			DateTime result = AppRuntime.UltDateTime;
			bool flag = string.IsNullOrWhiteSpace(value);
			DateTime result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				bool flag2 = !DateTime.TryParse(value, out result);
				if (flag2)
				{
					result = AppRuntime.UltDateTime;
				}
				result2 = result.Date.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
			}
			return result2;
		}

		public static DateTime ToDateTimeFrom16(this string value)
		{
			return new DateTime(Convert.ToInt64(value, 16));
		}

		public static DateTime ToDate(this string value)
		{
			DateTime result = AppRuntime.UltDateTime;
			bool flag = string.IsNullOrWhiteSpace(value);
			DateTime result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				bool flag2 = !DateTime.TryParse(value, out result);
				if (flag2)
				{
					result = AppRuntime.UltDateTime;
				}
				result2 = result.Date;
			}
			return result2;
		}

		public static List<string> ToWfUserList(this string value)
		{
			List<string> list = new List<string>();
			value = value.TrimStart(new char[]
			{
				','
			}).TrimEnd(new char[]
			{
				','
			});
			bool flag = !string.IsNullOrWhiteSpace(value);
			if (flag)
			{
				list = value.Split(new char[]
				{
					','
				}).ToList<string>();
			}
			return list;
		}

		public static string ToWfUserString(this List<string> value)
		{
			string str = "";
			bool flag = value == null || value.Count == 0;
			string result;
			if (flag)
			{
				result = str;
			}
			else
			{
				string str2 = ",";
				foreach (string str3 in value)
				{
					str2 = str2 + str3 + ",";
				}
				result = str2;
			}
			return result;
		}

		public static DbProviderType ToDbProviderType(this string value)
		{
			DbProviderType dbProviderType = DbProviderType.MSSQL;
			string a = value.ToUpper();
			if (!(a == "MSSQL"))
			{
				if (!(a == "ORACLE"))
				{
					if (a == "POSTGRESQL")
					{
						dbProviderType = DbProviderType.PostgreSQL;
					}
				}
				else
				{
					dbProviderType = DbProviderType.ORACLE;
				}
			}
			else
			{
				dbProviderType = DbProviderType.MSSQL;
			}
			return dbProviderType;
		}

		public static LogicSessionType ToLogicSessionType(this string value)
		{
			LogicSessionType logicSessionType = LogicSessionType.N;
			string a = value.ToUpper();
			if (!(a == "C"))
			{
				if (!(a == "S"))
				{
					if (a == "N")
					{
						logicSessionType = LogicSessionType.N;
					}
				}
				else
				{
					logicSessionType = LogicSessionType.S;
				}
			}
			else
			{
				logicSessionType = LogicSessionType.C;
			}
			return logicSessionType;
		}

		public static string ToFullModuleTypeName(this string value)
		{
			string str = string.Empty;
			string text = value.ToUpper();
			uint num = ComputeStringHash(text);
			if (num <= 3423339364u)
			{
				if (num <= 3238785555u)
				{
					if (num != 3222007936u)
					{
						if (num == 3238785555u)
						{
							if (text == "D")
							{
								str = "D|取数类模块";
							}
						}
					}
					else if (text == "E")
					{
						str = "E|扩展类外部模块";
					}
				}
				else if (num != 3289118412u)
				{
					if (num != 3322673650u)
					{
						if (num == 3423339364u)
						{
							if (text == "I")
							{
								str = "I|信息类模块";
							}
						}
					}
					else if (text == "C")
					{
						str = "C|文件夹";
					}
				}
				else if (text == "A")
				{
					str = "A|应用类模块";
				}
			}
			else if (num <= 3507227459u)
			{
				if (num != 3440116983u)
				{
					if (num != 3473672221u)
					{
						if (num == 3507227459u)
						{
							if (text == "T")
							{
								str = "T|定时计划类模块";
							}
						}
					}
					else if (text == "J")
					{
						str = "J|扩展类继承模块";
					}
				}
				else if (text == "H")
				{
					str = "H|代码帮助类模块";
				}
			}
			else if (num != 3524005078u)
			{
				if (num != 3574337935u)
				{
					if (num == 3591115554u)
					{
						if (text == "S")
						{
							str = "S|服务类模块";
						}
					}
				}
				else if (text == "P")
				{
					str = "P|区域类模块";
				}
			}
			else if (text == "W")
			{
				str = "W|工作流类模块";
			}
			return str;
		}

		public static string ToFileExtName(this string value)
		{
			string str = string.Empty;
			return new FileInfo(value).Extension;
		}

		public static string ToFileName(this string value)
		{
			string str = string.Empty;
			FileInfo fileInfo = new FileInfo(value);
			return fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
		}

		public static string FormatFileSize(this string value)
		{
			string str = string.Empty;
			long num = value.ToLong();
			bool flag = num < 0L;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("filesize");
			}
			return (num < 1073741824L) ? ((num < 1048576L) ? ((num < 1024L) ? string.Format("{0:0.00} bytes", num) : string.Format("{0:0.00} KB", (double)num / 1024.0)) : string.Format("{0:0.00} MB", (double)num / 1048576.0)) : string.Format("{0:0.00} GB", (double)num / 1073741824.0);
		}

		public static string RightSubstring(string inputStr, int length)
		{
			StringBuilder stringBuilder = new StringBuilder(length);
			int num = 0;
			for (int index = inputStr.Length - 1; index >= 0; index--)
			{
				stringBuilder.Insert(0, inputStr[index]);
				num++;
				bool flag = num >= length;
				if (flag)
				{
					break;
				}
			}
			return stringBuilder.ToString();
		}

		public static bool EqualsIgnoreCase(string s1, string s2)
		{
			bool flag = string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2) || s2.Length != s1.Length;
				result = (!flag2 && string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0);
			}
			return result;
		}

		public static bool EqualsLastStr(string filePath, string ext)
		{
			bool flag = string.IsNullOrEmpty(filePath) && string.IsNullOrEmpty(ext);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(ext) || string.IsNullOrEmpty(ext);
				if (flag2)
				{
					result = false;
				}
				else
				{
					int indexA = filePath.Length - ext.Length;
					bool flag3 = indexA < 0;
					result = (!flag3 && string.Compare(filePath, indexA, ext, 0, ext.Length, StringComparison.OrdinalIgnoreCase) == 0);
				}
			}
			return result;
		}

		public static string FormatHtmlText(string text, bool useHtmlTag)
		{
			StringBuilder stringBuilder = new StringBuilder(text);
			stringBuilder.Replace("  ", " &nbsp;");
			bool flag = !useHtmlTag;
			if (flag)
			{
				stringBuilder.Replace("<", "&lt;");
				stringBuilder.Replace(">", "&gt;");
				stringBuilder.Replace("\"", "&quot;");
			}
			stringBuilder.Replace("\r\n", "<br/>");
			return stringBuilder.ToString();
		}

		public static string FormatXmlValue(this string value)
		{
			string str = string.Empty;
			return value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;");
		}

		public static Dictionary<string, User> ToSmsUsers(this string value)
		{
			Dictionary<string, User> dictionary = new Dictionary<string, User>();
			string str = string.Empty;
			string str2 = string.Empty;
			string[] strArray = value.TrimEnd(new char[]
			{
				';'
			}).Split(new char[]
			{
				';'
			});
			for (int index = 0; index < strArray.Length; index++)
			{
				bool flag = !string.IsNullOrWhiteSpace(strArray[index]);
				if (flag)
				{
					string[] strArray2 = strArray[index].Split(new char[]
					{
						'<'
					});
					bool flag2 = strArray2.Length < 2;
					if (flag2)
					{
						throw new Exception("格式错误，请重新填写！\n" + strArray[index]);
					}
					string str3 = strArray2[1].TrimEnd(new char[]
					{
						'>'
					});
					string str4 = strArray2[0];
					bool flag3 = str4.Trim().StartsWith(".");
					if (flag3)
					{
						foreach (UgUser ugUser in UgUserUtils.GetUsers(str3))
						{
							User user = UserUtils.GetUser(ugUser.UserId);
							bool flag4 = user != null && !string.IsNullOrWhiteSpace(user.Mobile) && !dictionary.ContainsKey(user.Mobile);
							if (flag4)
							{
								dictionary.Add(user.Mobile, user);
							}
						}
					}
					else
					{
						bool flag5 = str4.Trim().StartsWith(",");
						if (flag5)
						{
							foreach (DeptUser deptUser in DeptUserUtils.GetUsers(str3))
							{
								User user2 = UserUtils.GetUser(deptUser.UserId);
								bool flag6 = user2 != null && !string.IsNullOrWhiteSpace(user2.Mobile) && !dictionary.ContainsKey(user2.Mobile);
								if (flag6)
								{
									dictionary.Add(user2.Mobile, user2);
								}
							}
						}
						else
						{
							User user3 = UserUtils.GetUser(str3);
							bool flag7 = user3 != null && !string.IsNullOrWhiteSpace(user3.Mobile) && !dictionary.ContainsKey(user3.Mobile);
							if (flag7)
							{
								dictionary.Add(user3.Mobile, user3);
							}
						}
					}
				}
			}
			return dictionary;
		}
	}
}
