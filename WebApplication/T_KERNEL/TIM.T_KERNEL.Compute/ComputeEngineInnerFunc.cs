using System;
using System.Collections;
using System.Globalization;

namespace TIM.T_KERNEL.Compute
{
	public class ComputeEngineInnerFunc
	{
		private string[] CEnglishMonth = new string[]
		{
			"JANUARY",
			"FEBRUARY",
			"MARCH",
			"APRIL",
			"MAY",
			"JUNE",
			"JULY",
			"AUGUST",
			"SEPTEMBER",
			"OCTOBER",
			"NOVEMBER",
			"DECEMBER"
		};

		public object ABS(ArrayList param)
		{
			this.CheckParamCount(param, 1);
			return Math.Abs(double.Parse(param[0].ToString()));
		}

		public object AVERAGE(ArrayList param)
		{
			double x = 0.0;
			for (int i = 0; i < param.Count; i++)
			{
				x += double.Parse(param[i].ToString());
			}
			return (param.Count == 0) ? 0.0 : (x / (double)param.Count);
		}

		public object AVERAGEEX(ArrayList param)
		{
			double x = 0.0;
			int iCount = 0;
			for (int i = 0; i < param.Count; i++)
			{
				bool flag = double.Parse(param[i].ToString()) != 0.0;
				if (flag)
				{
					x += double.Parse(param[i].ToString());
					iCount++;
				}
			}
			return (iCount == 0) ? 0.0 : (x / (double)iCount);
		}

		private string ChineseNum(double value, int decimalPlaces, bool chnUpperOrLower)
		{
			string result = "";
			try
			{
				string CDigit;
				if (chnUpperOrLower)
				{
					CDigit = "零壹贰叁肆伍陆柒捌玖";
				}
				else
				{
					CDigit = "０一二三四五六七八九";
				}
				value = this.Roundx(value, decimalPlaces);
				bool flag = value < 0.0;
				bool FS;
				if (flag)
				{
					FS = true;
					value = -value;
				}
				else
				{
					FS = false;
				}
				long IntNum = (long)((int)value);
				bool flag2 = this.frac(value) == 0.0;
				if (flag2)
				{
					decimalPlaces = 0;
				}
				bool flag3 = decimalPlaces == 0;
				if (flag3)
				{
					bool flag4 = IntNum >= 100000000L;
					if (flag4)
					{
						result = this.ChineseNum((double)((int)(IntNum / 100000000L)), 0, chnUpperOrLower) + "亿";
						bool flag5 = IntNum % 100000000L != 0L;
						if (flag5)
						{
							result = result + "零" + this.ChineseNum((double)(IntNum % 100000000L), 0, chnUpperOrLower);
						}
					}
					else
					{
						bool flag6 = IntNum >= 10000L;
						if (flag6)
						{
							result = this.ChineseNum((double)(IntNum / 10000L), 0, chnUpperOrLower) + "万";
							bool flag7 = IntNum % 10000L != 0L;
							if (flag7)
							{
								bool flag8 = IntNum % 10000L < 1000L;
								if (flag8)
								{
									result += "零";
								}
								result += this.ChineseNum((double)(IntNum % 10000L), 0, chnUpperOrLower);
							}
						}
						else
						{
							bool flag9 = IntNum >= 1000L;
							if (flag9)
							{
								result = CDigit.Substring((int)(IntNum / 1000L), 1);
								if (chnUpperOrLower)
								{
									result += "仟";
								}
								else
								{
									result += " 千";
								}
								bool flag10 = IntNum % 1000L != 0L;
								if (flag10)
								{
									bool flag11 = IntNum % 1000L < 100L;
									if (flag11)
									{
										result += "零";
									}
									result += this.ChineseNum((double)(IntNum % 1000L), 0, chnUpperOrLower);
								}
							}
							else
							{
								bool flag12 = IntNum >= 100L;
								if (flag12)
								{
									result = CDigit.Substring((int)(IntNum / 100L), 1);
									if (chnUpperOrLower)
									{
										result += "佰";
									}
									else
									{
										result += "百";
									}
									bool flag13 = IntNum % 100L != 0L;
									if (flag13)
									{
										bool flag14 = IntNum % 100L < 10L;
										if (flag14)
										{
											result += "零";
										}
										result += this.ChineseNum((double)(IntNum % 100L), 0, chnUpperOrLower);
									}
								}
								else
								{
									bool flag15 = IntNum >= 20L;
									if (flag15)
									{
										result = CDigit.Substring((int)(IntNum / 10L), 1);
										if (chnUpperOrLower)
										{
											result += "拾";
										}
										else
										{
											result += "十";
										}
										bool flag16 = IntNum % 10L != 0L;
										if (flag16)
										{
											result += CDigit.Substring((int)(IntNum % 10L), 1);
										}
									}
									else
									{
										bool flag17 = IntNum >= 10L;
										if (flag17)
										{
											if (chnUpperOrLower)
											{
												result = "壹拾";
											}
											else
											{
												result = "十";
											}
											bool flag18 = IntNum % 10L != 0L;
											if (flag18)
											{
												result += CDigit.Substring((int)(IntNum % 10L), 1);
											}
										}
										else
										{
											result = CDigit.Substring((int)IntNum, 1);
										}
									}
								}
							}
						}
					}
				}
				else
				{
					string CDec = ((int)Math.Round((1.0 + this.frac(value)) * 100.0)).ToString();
					CDec = CDec.Substring(1, CDec.Length - 1);
					int i = CDec.Length;
					for (int j = 1; j <= i; j++)
					{
						int k = Convert.ToInt32(CDec.Substring(j - 1, 1));
						CDec = CDec.Remove(j - 1, 1);
						CDec = CDec.Insert(j - 1, CDigit.Substring(k, 1));
					}
					result = this.ChineseNum((double)IntNum, 0, chnUpperOrLower) + "点" + CDec;
				}
				bool flag19 = FS;
				if (flag19)
				{
					result = "负" + result;
				}
			}
			catch
			{
			}
			return result;
		}

		private double frac(double param)
		{
			return param - (double)((int)param);
		}

		private string CUpperNum(double Num)
		{
			string UpperString = this.ChineseNum(Num, 2, true);
			bool flag = this.frac(this.Roundx(Num, 2)) != 0.0;
			if (flag)
			{
				string TempString = UpperString.Substring(0, UpperString.Length - 2);
				TempString = TempString + UpperString.Substring(UpperString.Length - 2, 1) + "角";
				bool flag2 = UpperString.Substring(UpperString.Length - 1, 1) != "零";
				if (flag2)
				{
					TempString = TempString + UpperString.Substring(UpperString.Length - 1, 1) + "分";
				}
				UpperString = TempString;
				bool flag3 = UpperString.Substring(0, 2) == "零点";
				if (flag3)
				{
					UpperString = UpperString.Substring(2, UpperString.Length - 2);
				}
				else
				{
					int Dot = UpperString.IndexOf("点");
					UpperString = UpperString.Substring(0, Dot) + "圆" + UpperString.Substring(Dot + 1);
				}
			}
			else
			{
				UpperString += "圆整";
			}
			return UpperString;
		}

		public object CHNNUM(ArrayList param)
		{
			this.CheckParamCount(param, 3);
			double x = double.Parse(param[0].ToString());
			int y = (int)double.Parse(param[1].ToString());
			bool z = (int)double.Parse(param[2].ToString()) == 0;
			return this.ChineseNum(x, y, z);
		}

		public object CHNCUR(ArrayList param)
		{
			this.CheckParamCount(param, 1);
			return this.CUpperNum(double.Parse(param[0].ToString()));
		}

		public object CHNDATE(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			DateTime dat = Convert.ToDateTime(param[0].ToString().Trim());
			return string.Concat(new string[]
			{
				this.GetStr(dat.Year),
				"年",
				this.ChineseNum((double)dat.Month, 0, false),
				"月",
				this.ChineseNum((double)dat.Day, 0, false),
				"日"
			});
		}

		private string GetStr(int i)
		{
			string[] array = new string[]
			{
				"零",
				"一",
				"二",
				"三",
				"四",
				"五",
				"六",
				"七",
				"八",
				"九"
			};
			string result = "";
			string s = i.ToString();
			for (i = 0; i < s.Length; i++)
			{
				result += array[int.Parse(s[i].ToString())];
			}
			return result;
		}

		public object COPY(ArrayList param)
		{
			bool flag = param.Count != 3 || !param[0].GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
			if (flag)
			{
				throw new Exception();
			}
			string x = param[0].ToString().Trim();
			int y = (int)double.Parse(param[1].ToString());
			int z = (int)double.Parse(param[2].ToString());
			return x.Substring(y - 1, z);
		}

		private string EnglishNumber(int num)
		{
			string[] small = new string[]
			{
				"ONE",
				"TWO",
				"THREE",
				"FOUR",
				"FIVE",
				"SIX",
				"SEVEN",
				"EIGHT",
				"NINE",
				"TEN",
				"ELEVEN",
				"TWELVE",
				"THIRTEEN",
				"FOURTEEN",
				"FIFTEEN",
				"SIXTEEN",
				"SEVENTEEN",
				"EIGHTEEN",
				"NINETEEN"
			};
			string[] tens = new string[]
			{
				"TWENTY",
				"THIRTY",
				"FORTY",
				"FIFTY",
				"SIXTY",
				"SEVENTY",
				"EIGHTY",
				"NINETY"
			};
			string[] Big = new string[]
			{
				" ",
				" THOUSAND ",
				" MILLION ",
				" BILLION "
			};
			string result = "";
			int IntNum = num;
			int UnitNo = 1;
			while (IntNum != 0)
			{
				int ThousandNum = IntNum % 1000;
				IntNum /= 1000;
				int HundredNum = ThousandNum / 100;
				int TenNum = ThousandNum % 100;
				bool flag = HundredNum == 0;
				string upString;
				if (flag)
				{
					upString = "";
				}
				else
				{
					upString = small[HundredNum - 1] + " HUNDRED";
				}
				bool flag2 = TenNum != 0;
				if (flag2)
				{
					bool flag3 = HundredNum != 0;
					if (flag3)
					{
						upString += " AND ";
					}
					bool flag4 = TenNum < 20;
					if (flag4)
					{
						upString += small[TenNum - 1];
					}
					else
					{
						upString += tens[TenNum / 10 - 2];
						bool flag5 = TenNum % 10 != 0;
						if (flag5)
						{
							upString = upString + "-" + small[TenNum % 10 - 1];
						}
					}
				}
				bool flag6 = upString != "";
				if (flag6)
				{
					bool flag7 = IntNum != 0 && HundredNum == 0 && upString.Substring(0, 2) != "A";
					if (flag7)
					{
						result = " AND " + upString + Big[UnitNo - 1] + result;
					}
					else
					{
						result = upString + Big[UnitNo - 1] + result;
					}
				}
				UnitNo++;
			}
			return result;
		}

		public object ENGNUM(ArrayList param)
		{
			this.CheckParamCount(param, 1);
			return this.EnglishNumber((int)((double)param[0]));
		}

		private string EnglishCur(double JE)
		{
			string sInteger = this.EnglishNumber((int)JE).Trim();
			string sDecimal = this.EnglishNumber((int)Math.Round((JE - (double)((int)JE)) * 100.0)).Trim();
			bool flag = sDecimal != "";
			if (flag)
			{
				sDecimal += " CENTS";
			}
			bool flag2 = sInteger != "" && sDecimal != "";
			string result;
			if (flag2)
			{
				result = sInteger + " AND " + sDecimal;
			}
			else
			{
				result = sInteger + sDecimal;
			}
			return result;
		}

		public object ENGCUR(ArrayList param)
		{
			this.CheckParamCount(param, 1);
			return this.EnglishCur((double)param[0]);
		}

		public object ENGMON(ArrayList param)
		{
			this.CheckParamCount(param, 2);
			double x = double.Parse(param[0].ToString());
			double y = double.Parse(param[1].ToString());
			return (y == 0.0) ? this.CEnglishMonth[(int)x - 1] : this.CEnglishMonth[(int)x - 1].Substring(0, 3);
		}

		public object ENGDATE(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			DateTime dat = Convert.ToDateTime(param[0].ToString().Trim());
			string mon = this.CEnglishMonth[dat.Month - 1];
			string day = dat.Day.ToString("D2");
			string year = dat.Year.ToString("D2");
			return mon + day + year;
		}

		public object FORMAT(ArrayList param)
		{
			bool flag = param.Count == 0;
			if (flag)
			{
				throw new Exception();
			}
			bool flag2 = param.Count == 1;
			object result;
			if (flag2)
			{
				bool flag3 = param[0].GetType().ToString().Equals("SYSTEM.STRING");
				if (!flag3)
				{
					throw new Exception();
				}
				result = param[0].ToString();
			}
			else
			{
				string x = param[0].ToString();
				int iCount = param.Count - 1;
				object[] Args = new object[iCount];
				for (int i = 0; i < iCount; i++)
				{
					string sType = param[i + 1].GetType().ToString();
					bool flag4 = sType.ToUpper().Equals("SYSTEM.STRING");
					if (flag4)
					{
						Args[i] = param[i + 1].ToString();
					}
					else
					{
						bool flag5 = sType.ToUpper().Equals("SYSTEM.DOUBLE");
						if (!flag5)
						{
							throw new Exception();
						}
						double sparm = double.Parse(param[i + 1].ToString());
						bool flag6 = this.frac(sparm) == 0.0;
						if (flag6)
						{
							Args[i] = (int)sparm;
						}
						else
						{
							Args[i] = sparm;
						}
					}
				}
				result = string.Format(x, Args);
			}
			return result;
		}

		public object FRAC(ArrayList param)
		{
			this.CheckParamCount(param, 1);
			double x = double.Parse(param[0].ToString());
			int i = (int)x;
			return x - (double)i;
		}

		public object IIF(ArrayList param)
		{
			this.CheckParamCount(param, 3);
			double x = double.Parse(param[0].ToString());
			bool flag = x > 0.0;
			object result;
			if (flag)
			{
				result = param[1];
			}
			else
			{
				result = param[2];
			}
			return result;
		}

		public object INT(ArrayList param)
		{
			this.CheckParamCount(param, 1);
			double x = double.Parse(param[0].ToString());
			int i = (int)x;
			return i;
		}

		public object LENGTH(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return param[0].ToString().Length;
		}

		public object TRIM(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return param[0].ToString().Trim();
		}

		public object UPPERCASE(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return param[0].ToString().ToUpper();
		}

		public object LOWERCASE(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return param[0].ToString().ToLower();
		}

		public object MAX(ArrayList param)
		{
			bool flag = param.Count == 0;
			if (flag)
			{
				throw new ComputeEngineParamException("参数列表为空，不能进行计算！");
			}
			double x = double.Parse(param[0].ToString());
			for (int i = 1; i < param.Count; i++)
			{
				double x2 = double.Parse(param[i].ToString());
				bool flag2 = x < x2;
				if (flag2)
				{
					x = x2;
				}
			}
			return x;
		}

		public object MIN(ArrayList param)
		{
			bool flag = param.Count == 0;
			if (flag)
			{
				throw new ComputeEngineParamException("参数列表为空，不能进行计算！");
			}
			double x = double.Parse(param[0].ToString());
			for (int i = 1; i < param.Count; i++)
			{
				double x2 = double.Parse(param[i].ToString());
				bool flag2 = x > x2;
				if (flag2)
				{
					x = x2;
				}
			}
			return x;
		}

		public object MOD(ArrayList param)
		{
			this.CheckParamCount(param, 2);
			double x = double.Parse(param[0].ToString());
			double x2 = double.Parse(param[1].ToString());
			bool flag = x2 == 0.0;
			if (flag)
			{
				throw new Exception();
			}
			return x % x2;
		}

		public object PI(ArrayList param)
		{
			this.CheckParamCount(param, 0);
			return 3.1415926535897931;
		}

		public object POS(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 2);
			string x = param[0].ToString().Trim();
			string y = param[1].ToString().Trim();
			return x.IndexOf(y) + 1;
		}

		public object POWER(ArrayList param)
		{
			this.CheckParamCount(param, 2);
			double x = double.Parse(param[0].ToString());
			double y = double.Parse(param[1].ToString());
			bool flag = x <= 0.0 || y <= 0.0;
			if (flag)
			{
				throw new Exception();
			}
			return Math.Pow(x, y);
		}

		public object ROUNDX(ArrayList param)
		{
			this.CheckParamCount(param, 2);
			decimal x = decimal.Parse(param[0].ToString());
			int y = (int)Math.Truncate(double.Parse(param[1].ToString()));
			return Math.Round(x, y);
		}

		public object ROUND(ArrayList param)
		{
			this.CheckParamCount(param, 2);
			double x = double.Parse(param[0].ToString());
			int y = (int)Math.Truncate(double.Parse(param[1].ToString()));
			return this.Roundx(x, y);
		}

		private double Roundx(double num, int digit)
		{
			bool flag = digit > 10;
			if (flag)
			{
				digit = 0;
			}
			else
			{
				bool flag2 = digit < -10;
				if (flag2)
				{
					digit = -10;
				}
			}
			double z = Math.Pow(10.0, (double)digit);
			bool flag3 = num > 0.0;
			double result;
			if (flag3)
			{
				result = (double)((long)(num * z + 0.5)) / z;
			}
			else
			{
				result = (double)((long)(num * z - 0.5)) / z;
			}
			return result;
		}

		public object SQRT(ArrayList param)
		{
			this.CheckParamCount(param, 1);
			double x = double.Parse(param[0].ToString());
			bool flag = x < 0.0;
			if (flag)
			{
				throw new ComputeEngineParamException("负数不可求平方根！");
			}
			return Math.Sqrt(x);
		}

		public object SQR(ArrayList param)
		{
			this.CheckParamCount(param, 1);
			double x = double.Parse(param[0].ToString());
			return x * x;
		}

		public object STR(ArrayList param)
		{
			bool flag = param.Count != 2 || !param[1].GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
			if (flag)
			{
				throw new Exception();
			}
			double x = double.Parse(param[0].ToString());
			string y = param[1].ToString().Trim();
			bool flag2 = this.frac(x) == 0.0;
			object result;
			if (flag2)
			{
				result = ((int)x).ToString(y);
			}
			else
			{
				result = x.ToString(y);
			}
			return result;
		}

		public object SUM(ArrayList param)
		{
			double x = 0.0;
			for (int i = 0; i < param.Count; i++)
			{
				x += double.Parse(param[i].ToString());
			}
			return x;
		}

		public object VAL(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return Convert.ToDouble(param[0].ToString().Trim());
		}

		public object GETDATE(ArrayList param)
		{
			this.CheckParamCount(param, 0);
			return DateTime.Now.ToString("yyyy-MM-dd");
		}

		public object GETTIME(ArrayList param)
		{
			this.CheckParamCount(param, 0);
			return DateTime.Now.ToString().Remove(0, 10);
		}

		public object GETNOW(ArrayList param)
		{
			bool flag = param.Count == 0;
			object result;
			if (flag)
			{
				result = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			}
			else
			{
				bool flag2 = param.Count != 1;
				if (flag2)
				{
					throw new ComputeEngineParamException("只能指定一个参数！");
				}
				result = DateTime.Now.ToString(param[0].ToString());
			}
			return result;
		}

		public object YEAROF(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return Convert.ToDateTime(param[0].ToString().Trim()).Year;
		}

		public object MONTHOF(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return Convert.ToDateTime(param[0].ToString().Trim()).Month;
		}

		public object DAYOF(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return Convert.ToDateTime(param[0].ToString().Trim()).Day;
		}

		public object HOUROF(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return Convert.ToDateTime(param[0].ToString().Trim()).Hour;
		}

		public object MINUTEOF(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return Convert.ToDateTime(param[0].ToString().Trim()).Minute;
		}

		public object SECONDOF(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return Convert.ToDateTime(param[0].ToString().Trim()).Second;
		}

		public object DAYSINMONTH(ArrayList param)
		{
			this.CheckParamCount(param, 2);
			int year = (int)double.Parse(param[0].ToString());
			int month = (int)double.Parse(param[1].ToString());
			return DateTime.DaysInMonth(year, month);
		}

		public object DAYSINYEAR(ArrayList param)
		{
			this.CheckParamCount(param, 1);
			int year = (int)double.Parse(param[0].ToString());
			int days = 0;
			for (int i = 1; i <= 12; i++)
			{
				days += DateTime.DaysInMonth(year, i);
			}
			return days;
		}

		public object DAYOFWEEK(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			return Convert.ToDateTime(param[0].ToString().Trim()).DayOfWeek.ToString();
		}

		public object WEEKOF(ArrayList param)
		{
			this.CheckParamCountAndObjectIsString(param, 1);
			DateTime dat = Convert.ToDateTime(param[0].ToString().Trim());
			GregorianCalendar gc = new GregorianCalendar();
			int WeekOfYear = gc.GetWeekOfYear(dat, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
			return WeekOfYear;
		}

		private void CheckParamCount(ArrayList param, int paramCount)
		{
			bool flag = param.Count != paramCount;
			if (flag)
			{
				throw new ComputeEngineParamException(string.Format("参数数量错误：期望[{0}]个参数，实际[{1}]个参数！", paramCount, param.Count));
			}
		}

		private void CheckParamCountAndObjectIsString(ArrayList param, int paramCount)
		{
			bool flag = param.Count != paramCount;
			if (flag)
			{
				throw new ComputeEngineParamException(string.Format("参数数量错误：期望[{0}]个参数，实际[{1}]个参数！", paramCount, param.Count));
			}
			while (paramCount != 0)
			{
				bool flag2 = !param[paramCount - 1].GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
				if (flag2)
				{
					throw new ComputeEngineParamException(string.Format("参数类型错误，在参数位置[{0}]处为非字符串类型！", paramCount));
				}
				paramCount--;
			}
		}
	}
}
