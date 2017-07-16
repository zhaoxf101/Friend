using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace TIM.T_TEMPLET.Reporting
{
	internal class ExpressionFunc
	{
		private MethodInfo[] _funcMethodInfo = null;

		private Dictionary<string, FunctionTriple> m_globalFunction = new Dictionary<string, FunctionTriple>();

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

		public ExpressionFunc()
		{
			this.RegisterGlobalFunction();
		}

		public FunctionTriple Find(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			FunctionTriple result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string _nameUpper = name.Trim().ToUpper();
				bool flag2 = this.m_globalFunction.ContainsKey(_nameUpper);
				if (flag2)
				{
					result = new FunctionTriple(_nameUpper, this, null);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public bool Add(string name, FunctionTriple function)
		{
			bool flag = string.IsNullOrEmpty(name);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string _nameUpper = name.Trim().ToUpper();
				bool flag2 = !this.m_globalFunction.ContainsKey(_nameUpper);
				if (flag2)
				{
					this.m_globalFunction.Add(_nameUpper, function);
				}
				result = true;
			}
			return result;
		}

		public bool Remove(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string _nameUpper = name.Trim().ToUpper();
				bool flag2 = this.m_globalFunction.ContainsKey(_nameUpper);
				if (flag2)
				{
					this.m_globalFunction.Remove(_nameUpper);
				}
				result = true;
			}
			return result;
		}

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


        public object Run(string name, ArrayList funcParams)
		{
			bool flag = string.IsNullOrEmpty(name);
			object result2;
			if (flag)
			{
				result2 = null;
			}
			else
			{
				string _nameUpper = name.Trim().ToUpper();
				object result = null;
				string text = _nameUpper;
				uint num = ComputeStringHash(text);
				if (num <= 2086133413u)
				{
					if (num <= 950998320u)
					{
						if (num <= 387926337u)
						{
							if (num <= 156963947u)
							{
								if (num != 16023644u)
								{
									if (num != 86370480u)
									{
										if (num == 156963947u)
										{
											if (text == "UPPERCASE")
											{
												result = this.UPPERCASE(funcParams);
											}
										}
									}
									else if (text == "STR")
									{
										result = this.STR(funcParams);
									}
								}
								else if (text == "DAYOFWEEK")
								{
									result = this.DAYOFWEEK(funcParams);
								}
							}
							else if (num != 239465655u)
							{
								if (num != 385905500u)
								{
									if (num == 387926337u)
									{
										if (text == "SQR")
										{
											result = this.SQR(funcParams);
										}
									}
								}
								else if (text == "VAL")
								{
									result = this.VAL(funcParams);
								}
							}
							else if (text == "MIN")
							{
								result = this.MIN(funcParams);
							}
						}
						else if (num <= 539513288u)
						{
							if (num != 401617785u)
							{
								if (num != 475632249u)
								{
									if (num == 539513288u)
									{
										if (text == "SUM")
										{
											result = this.SUM(funcParams);
										}
									}
								}
								else if (text == "MAX")
								{
									result = this.MAX(funcParams);
								}
							}
							else if (text == "ROUNDX")
							{
								result = this.ROUNDX(funcParams);
							}
						}
						else if (num != 582295364u)
						{
							if (num != 589974479u)
							{
								if (num == 950998320u)
								{
									if (text == "SECONDOF")
									{
										result = this.SECONDOF(funcParams);
									}
								}
							}
							else if (text == "MAXINDEX")
							{
								result = this.MAXINDEX(funcParams);
							}
						}
						else if (text == "COPY")
						{
							result = this.COPY(funcParams);
						}
					}
					else if (num <= 1631094621u)
					{
						if (num <= 1268358898u)
						{
							if (num != 992177940u)
							{
								if (num != 1166812647u)
								{
									if (num == 1268358898u)
									{
										if (text == "HOUROF")
										{
											result = this.HOUROF(funcParams);
										}
									}
								}
								else if (text == "HTMLTEXT")
								{
									result = this.HTMLTEXT(funcParams);
								}
							}
							else if (text == "CHNDATE")
							{
								result = this.CHNDATE(funcParams);
							}
						}
						else if (num != 1286481789u)
						{
							if (num != 1574857250u)
							{
								if (num == 1631094621u)
								{
									if (text == "MININDEX")
									{
										result = this.MININDEX(funcParams);
									}
								}
							}
							else if (text == "CHNCUR")
							{
								result = this.CHNCUR(funcParams);
							}
						}
						else if (text == "YEAROF")
						{
							result = this.YEAROF(funcParams);
						}
					}
					else if (num <= 1837786619u)
					{
						if (num != 1808983475u)
						{
							if (num != 1820608667u)
							{
								if (num == 1837786619u)
								{
									if (text == "ENGCUR")
									{
										result = this.ENGCUR(funcParams);
									}
								}
							}
							else if (text == "ABS")
							{
								result = this.ABS(funcParams);
							}
						}
						else if (text == "GETDATE")
						{
							result = this.GETDATE(funcParams);
						}
					}
					else if (num != 2064408853u)
					{
						if (num != 2067794959u)
						{
							if (num == 2086133413u)
							{
								if (text == "ENGNUM")
								{
									result = this.ENGNUM(funcParams);
								}
							}
						}
						else if (text == "SQRT")
						{
							result = this.SQRT(funcParams);
						}
					}
					else if (text == "LENGTH")
					{
						result = this.LENGTH(funcParams);
					}
				}
				else if (num <= 3206795317u)
				{
					if (num <= 2693668162u)
					{
						if (num <= 2538062546u)
						{
							if (num != 2241596457u)
							{
								if (num != 2312855983u)
								{
									if (num == 2538062546u)
									{
										if (text == "FORMAT")
										{
											result = this.FORMAT(funcParams);
										}
									}
								}
								else if (text == "INCWEEK")
								{
									result = this.INCWEEK(funcParams);
								}
							}
							else if (text == "FRAC")
							{
								result = this.FRAC(funcParams);
							}
						}
						else if (num != 2553527398u)
						{
							if (num != 2565374185u)
							{
								if (num == 2693668162u)
								{
									if (text == "INCYEAR")
									{
										result = this.INCYEAR(funcParams);
									}
								}
							}
							else if (text == "POS")
							{
								result = this.POS(funcParams);
							}
						}
						else if (text == "GETTIME")
						{
							result = this.GETTIME(funcParams);
						}
					}
					else if (num <= 2859438216u)
					{
						if (num != 2702515139u)
						{
							if (num != 2703812144u)
							{
								if (num == 2859438216u)
								{
									if (text == "AVERAGE")
									{
										result = this.AVERAGE(funcParams);
									}
								}
							}
							else if (text == "DAYOF")
							{
								result = this.DAYOF(funcParams);
							}
						}
						else if (text == "INCMONTH")
						{
							result = this.INCMONTH(funcParams);
						}
					}
					else if (num != 3032802928u)
					{
						if (num != 3067563547u)
						{
							if (num == 3206795317u)
							{
								if (text == "DAYSINMONTH")
								{
									result = this.DAYSINMONTH(funcParams);
								}
							}
						}
						else if (text == "ROUND")
						{
							result = this.ROUND(funcParams);
						}
					}
					else if (text == "WEEKOF")
					{
						result = this.WEEKOF(funcParams);
					}
				}
				else if (num <= 3785232124u)
				{
					if (num <= 3636696446u)
					{
						if (num != 3338760679u)
						{
							if (num != 3365182252u)
							{
								if (num == 3636696446u)
								{
									if (text == "INT")
									{
										result = this.INT(funcParams);
									}
								}
							}
							else if (text == "DAYSINYEAR")
							{
								result = this.DAYSINYEAR(funcParams);
							}
						}
						else if (text == "ENGMON")
						{
							result = this.ENGMON(funcParams);
						}
					}
					else if (num != 3637137731u)
					{
						if (num != 3663621693u)
						{
							if (num == 3785232124u)
							{
								if (text == "MINUTEOF")
								{
									result = this.MINUTEOF(funcParams);
								}
							}
						}
						else if (text == "AVERAGEEX")
						{
							result = this.AVERAGEEX(funcParams);
						}
					}
					else if (text == "IIF")
					{
						result = this.IIF(funcParams);
					}
				}
				else if (num <= 3881771281u)
				{
					if (num != 3825202918u)
					{
						if (num != 3825596808u)
						{
							if (num == 3881771281u)
							{
								if (text == "GETNOW")
								{
									result = this.GETNOW(funcParams);
								}
							}
						}
						else if (text == "MONTHOF")
						{
							result = this.MONTHOF(funcParams);
						}
					}
					else if (text == "LOWERCASE")
					{
						result = this.LOWERCASE(funcParams);
					}
				}
				else if (num != 3903510451u)
				{
					if (num != 4154820355u)
					{
						if (num == 4280037248u)
						{
							if (text == "CHNNUM")
							{
								result = this.CHNNUM(funcParams);
							}
						}
					}
					else if (text == "INCDAY")
					{
						result = this.INCDAY(funcParams);
					}
				}
				else if (text == "ENGDATE")
				{
					result = this.ENGDATE(funcParams);
				}
				result2 = result;
			}
			return result2;
		}

		private void RegisterGlobalFunction()
		{
			this.m_globalFunction.Add("MAX", null);
			this.m_globalFunction.Add("MAXINDEX", null);
			this.m_globalFunction.Add("MIN", null);
			this.m_globalFunction.Add("MININDEX", null);
			this.m_globalFunction.Add("SUM", null);
			this.m_globalFunction.Add("AVERAGE", null);
			this.m_globalFunction.Add("AVERAGEEX", null);
			this.m_globalFunction.Add("IIF", null);
			this.m_globalFunction.Add("ABS", null);
			this.m_globalFunction.Add("ROUND", null);
			this.m_globalFunction.Add("ROUNDX", null);
			this.m_globalFunction.Add("INT", null);
			this.m_globalFunction.Add("FRAC", null);
			this.m_globalFunction.Add("LENGTH", null);
			this.m_globalFunction.Add("COPY", null);
			this.m_globalFunction.Add("STR", null);
			this.m_globalFunction.Add("VAL", null);
			this.m_globalFunction.Add("SQR", null);
			this.m_globalFunction.Add("SQRT", null);
			this.m_globalFunction.Add("CHNNUM", null);
			this.m_globalFunction.Add("CHNCUR", null);
			this.m_globalFunction.Add("FORMAT", null);
			this.m_globalFunction.Add("CHNDATE", null);
			this.m_globalFunction.Add("GETDATE", null);
			this.m_globalFunction.Add("GETTIME", null);
			this.m_globalFunction.Add("GETNOW", null);
			this.m_globalFunction.Add("YEAROF", null);
			this.m_globalFunction.Add("MONTHOF", null);
			this.m_globalFunction.Add("DAYOF", null);
			this.m_globalFunction.Add("HOUROF", null);
			this.m_globalFunction.Add("MINUTEOF", null);
			this.m_globalFunction.Add("SECONDOF", null);
			this.m_globalFunction.Add("UPPERCASE", null);
			this.m_globalFunction.Add("LOWERCASE", null);
			this.m_globalFunction.Add("POS", null);
			this.m_globalFunction.Add("DAYSINMONTH", null);
			this.m_globalFunction.Add("DAYSINYEAR", null);
			this.m_globalFunction.Add("DAYOFWEEK", null);
			this.m_globalFunction.Add("WEEKOF", null);
			this.m_globalFunction.Add("ENGNUM", null);
			this.m_globalFunction.Add("ENGCUR", null);
			this.m_globalFunction.Add("ENGMON", null);
			this.m_globalFunction.Add("ENGDATE", null);
			this.m_globalFunction.Add("INCYEAR", null);
			this.m_globalFunction.Add("INCMONTH", null);
			this.m_globalFunction.Add("INCWEEK", null);
			this.m_globalFunction.Add("INCDAY", null);
			this.m_globalFunction.Add("HTMLTEXT", null);
			this._funcMethodInfo = typeof(ExpressionFunc).GetMethods();
			for (int i = 0; i < this._funcMethodInfo.Length; i++)
			{
				string _nameUpper = this._funcMethodInfo[i].Name.ToUpper();
				bool flag = this.m_globalFunction.ContainsKey(_nameUpper);
				if (flag)
				{
					this.m_globalFunction[_nameUpper] = new FunctionTriple(_nameUpper, this, this._funcMethodInfo[i]);
				}
			}
		}

		public object ABS(ArrayList param)
		{
			this.ThrowDoubleError(param, 1);
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

		private string ChineseNum(double Num, int Dec, bool NumKind)
		{
			string result = "";
			try
			{
				string CDigit;
				if (NumKind)
				{
					CDigit = "零壹贰叁肆伍陆柒捌玖";
				}
				else
				{
					CDigit = "０一二三四五六七八九";
				}
				Num = this.Roundx(Num, Dec);
				bool flag = Num < 0.0;
				bool FS;
				if (flag)
				{
					FS = true;
					Num = -Num;
				}
				else
				{
					FS = false;
				}
				long IntNum = (long)((int)Num);
				bool flag2 = this.frac(Num) == 0.0;
				if (flag2)
				{
					Dec = 0;
				}
				bool flag3 = Dec == 0;
				if (flag3)
				{
					bool flag4 = IntNum >= 100000000L;
					if (flag4)
					{
						result = this.ChineseNum((double)((int)(IntNum / 100000000L)), 0, NumKind) + "亿";
						bool flag5 = IntNum % 100000000L != 0L;
						if (flag5)
						{
							result = result + "零" + this.ChineseNum((double)(IntNum % 100000000L), 0, NumKind);
						}
					}
					else
					{
						bool flag6 = IntNum >= 10000L;
						if (flag6)
						{
							result = this.ChineseNum((double)(IntNum / 10000L), 0, NumKind) + "万";
							bool flag7 = IntNum % 10000L != 0L;
							if (flag7)
							{
								bool flag8 = IntNum % 10000L < 1000L;
								if (flag8)
								{
									result += "零";
								}
								result += this.ChineseNum((double)(IntNum % 10000L), 0, NumKind);
							}
						}
						else
						{
							bool flag9 = IntNum >= 1000L;
							if (flag9)
							{
								result = CDigit.Substring((int)(IntNum / 1000L), 1);
								if (NumKind)
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
									result += this.ChineseNum((double)(IntNum % 1000L), 0, NumKind);
								}
							}
							else
							{
								bool flag12 = IntNum >= 100L;
								if (flag12)
								{
									result = CDigit.Substring((int)(IntNum / 100L), 1);
									if (NumKind)
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
										result += this.ChineseNum((double)(IntNum % 100L), 0, NumKind);
									}
								}
								else
								{
									bool flag15 = IntNum >= 20L;
									if (flag15)
									{
										result = CDigit.Substring((int)(IntNum / 10L), 1);
										if (NumKind)
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
											if (NumKind)
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
					string CDec = ((int)Math.Round((1.0 + this.frac(Num)) * 100.0)).ToString();
					CDec = CDec.Substring(1, CDec.Length - 1);
					int i = CDec.Length;
					for (int j = 1; j <= i; j++)
					{
						int k = Convert.ToInt32(CDec.Substring(j - 1, 1));
						CDec = CDec.Remove(j - 1, 1);
						CDec = CDec.Insert(j - 1, CDigit.Substring(k, 1));
					}
					result = this.ChineseNum((double)IntNum, 0, NumKind) + "点" + CDec;
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
			this.ThrowDoubleError(param, 3);
			double x = double.Parse(param[0].ToString());
			int y = (int)double.Parse(param[1].ToString());
			bool z = (int)double.Parse(param[2].ToString()) == 0;
			return this.ChineseNum(x, y, z);
		}

		public object CHNCUR(ArrayList param)
		{
			this.ThrowDoubleError(param, 1);
			return this.CUpperNum(double.Parse(param[0].ToString()));
		}

		public object CHNDATE(ArrayList param)
		{
			this.ThrowStringError(param, 1);
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
			object result;
			try
			{
				result = x.Substring(y - 1, z);
			}
			catch
			{
				result = "";
			}
			return result;
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
			this.ThrowDoubleError(param, 1);
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
			this.ThrowDoubleError(param, 1);
			return this.EnglishCur((double)param[0]);
		}

		public object ENGMON(ArrayList param)
		{
			this.ThrowDoubleError(param, 2);
			double x = double.Parse(param[0].ToString());
			double y = double.Parse(param[1].ToString());
			return (y == 0.0) ? this.CEnglishMonth[(int)x - 1] : this.CEnglishMonth[(int)x - 1].Substring(0, 3);
		}

		public object ENGDATE(ArrayList param)
		{
			this.ThrowStringError(param, 1);
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
			this.ThrowDoubleError(param, 1);
			double x = double.Parse(param[0].ToString());
			int i = (int)x;
			return x - (double)i;
		}

		public object IIF(ArrayList param)
		{
			this.ThrowDoubleError(param, 3);
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
			this.ThrowDoubleError(param, 1);
			double x = double.Parse(param[0].ToString());
			int i = (int)x;
			return i;
		}

		public object LENGTH(ArrayList param)
		{
			this.ThrowStringError(param, 1);
			return param[0].ToString().Length;
		}

		public object LOWERCASE(ArrayList param)
		{
			this.ThrowStringError(param, 1);
			return param[0].ToString().ToLower();
		}

		public object MAX(ArrayList param)
		{
			bool flag = param.Count == 0;
			if (flag)
			{
				throw new Exception();
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

		public object MAXINDEX(ArrayList param)
		{
			bool flag = param.Count < 2;
			if (flag)
			{
				throw new Exception();
			}
			ArrayList lstSorted = new ArrayList();
			double indexNumber = double.Parse(param[0].ToString());
			lstSorted.Add(indexNumber);
			for (int i = 1; i < param.Count; i++)
			{
				double tmp = double.Parse(param[i].ToString());
				lstSorted.Add(tmp);
			}
			lstSorted.Sort();
			lstSorted.Reverse();
			double result = (double)(lstSorted.IndexOf(indexNumber) + 1);
			return result;
		}

		public object MININDEX(ArrayList param)
		{
			bool flag = param.Count < 2;
			if (flag)
			{
				throw new Exception();
			}
			ArrayList lstSorted = new ArrayList();
			double indexNumber = double.Parse(param[0].ToString());
			lstSorted.Add(indexNumber);
			for (int i = 1; i < param.Count; i++)
			{
				double tmp = double.Parse(param[i].ToString());
				lstSorted.Add(tmp);
			}
			lstSorted.Sort();
			double result = (double)(lstSorted.IndexOf(indexNumber) + 1);
			return result;
		}

		public object MIN(ArrayList param)
		{
			bool flag = param.Count == 0;
			if (flag)
			{
				throw new Exception();
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
			this.ThrowDoubleError(param, 2);
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
			this.ThrowDoubleError(param, 0);
			return 3.1415926535897931;
		}

		public object POS(ArrayList param)
		{
			this.ThrowStringError(param, 2);
			string x = param[0].ToString().Trim();
			string y = param[1].ToString().Trim();
			return x.IndexOf(y) + 1;
		}

		public object POWER(ArrayList param)
		{
			this.ThrowDoubleError(param, 2);
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
			this.ThrowDoubleError(param, 2);
			decimal x = decimal.Parse(param[0].ToString());
			int y = (int)Math.Truncate(double.Parse(param[1].ToString()));
			return Math.Round(x, y);
		}

		public object ROUND(ArrayList param)
		{
			this.ThrowDoubleError(param, 2);
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
			this.ThrowDoubleError(param, 1);
			double x = double.Parse(param[0].ToString());
			bool flag = x < 0.0;
			if (flag)
			{
				throw new Exception("负数不可求平方根！");
			}
			return Math.Sqrt(x);
		}

		public object SQR(ArrayList param)
		{
			this.ThrowDoubleError(param, 1);
			double x = double.Parse(param[0].ToString());
			return x * x;
		}

		public object STR(ArrayList param)
		{
			bool flag = param.Count == 1;
			object result;
			if (flag)
			{
				result = param[0].ToString();
			}
			else
			{
				bool flag2 = param.Count != 2 || !param[1].GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
				if (flag2)
				{
					throw new Exception();
				}
				double x = double.Parse(param[0].ToString());
				string y = param[1].ToString().Trim();
				bool flag3 = this.frac(x) == 0.0;
				if (flag3)
				{
					result = ((int)x).ToString(y);
				}
				else
				{
					result = x.ToString(y);
				}
			}
			return result;
		}

		public object SUM(ArrayList param)
		{
			bool paramIsString = false;
			for (int i = 0; i < param.Count; i++)
			{
				bool flag = param[i] is string;
				if (flag)
				{
					paramIsString = true;
					break;
				}
			}
			bool flag2 = !paramIsString;
			object result;
			if (flag2)
			{
				double x = 0.0;
				for (int j = 0; j < param.Count; j++)
				{
					x += double.Parse(param[j].ToString());
				}
				result = x;
			}
			else
			{
				string resultS = "";
				for (int k = 0; k < param.Count; k++)
				{
					resultS += param[k].ToString();
				}
				result = resultS;
			}
			return result;
		}

		public object TRIM(ArrayList param)
		{
			this.ThrowStringError(param, 1);
			return param[0].ToString().Trim();
		}

		public object UPPERCASE(ArrayList param)
		{
			this.ThrowStringError(param, 1);
			return param[0].ToString().ToUpper();
		}

		public object VAL(ArrayList param)
		{
			this.ThrowStringError(param, 1);
			return Convert.ToDouble(param[0].ToString().Trim());
		}

		public object TESTEVAL(ArrayList param)
		{
			this.ThrowStringError(param, 1);
			return param[0].ToString().ToUpper();
		}

		public object GETDATE(ArrayList param)
		{
			this.ThrowDoubleError(param, 0);
			return DateTime.Now.ToString("yyyy-MM-dd");
		}

		public object GETTIME(ArrayList param)
		{
			this.ThrowDoubleError(param, 0);
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
					throw new Exception();
				}
				result = DateTime.Now.ToString(param[0].ToString());
			}
			return result;
		}

		public object YEAROF(ArrayList param)
		{
			DateTime dat;
			object result;
			try
			{
				dat = Convert.ToDateTime(param[0].ToString().Trim());
			}
			catch
			{
				result = 0;
				return result;
			}
			result = dat.Year;
			return result;
		}

		public object MONTHOF(ArrayList param)
		{
			DateTime dat;
			object result;
			try
			{
				dat = Convert.ToDateTime(param[0].ToString().Trim());
			}
			catch
			{
				result = 0;
				return result;
			}
			result = dat.Month;
			return result;
		}

		public object DAYOF(ArrayList param)
		{
			DateTime dat;
			object result;
			try
			{
				dat = Convert.ToDateTime(param[0].ToString().Trim());
			}
			catch
			{
				result = 0;
				return result;
			}
			result = dat.Day;
			return result;
		}

		public object HOUROF(ArrayList param)
		{
			DateTime dat;
			object result;
			try
			{
				dat = Convert.ToDateTime(param[0].ToString().Trim());
			}
			catch
			{
				result = 0;
				return result;
			}
			result = dat.Hour;
			return result;
		}

		public object MINUTEOF(ArrayList param)
		{
			DateTime dat;
			object result;
			try
			{
				dat = Convert.ToDateTime(param[0].ToString().Trim());
			}
			catch
			{
				result = 0;
				return result;
			}
			result = dat.Minute;
			return result;
		}

		public object SECONDOF(ArrayList param)
		{
			this.ThrowStringError(param, 1);
			return Convert.ToDateTime(param[0].ToString().Trim()).Second;
		}

		public object DAYSINMONTH(ArrayList param)
		{
			this.ThrowDoubleError(param, 2);
			int year = (int)double.Parse(param[0].ToString());
			int month = (int)double.Parse(param[1].ToString());
			return DateTime.DaysInMonth(year, month);
		}

		public object DAYSINYEAR(ArrayList param)
		{
			this.ThrowDoubleError(param, 1);
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
			this.ThrowStringError(param, 1);
			return Convert.ToDateTime(param[0].ToString().Trim()).DayOfWeek.ToString();
		}

		public object WEEKOF(ArrayList param)
		{
			this.ThrowStringError(param, 1);
			DateTime dat = Convert.ToDateTime(param[0].ToString().Trim());
			GregorianCalendar gc = new GregorianCalendar();
			int WeekOfYear = gc.GetWeekOfYear(dat, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
			return WeekOfYear;
		}

		public object INCYEAR(ArrayList param)
		{
			string date = param[0].ToString();
			string inc = param[1].ToString();
			return Utils.Str2DateTime(date, DateTime.MinValue).AddYears(Utils.Str2Int(inc, 0)).ToString();
		}

		public object INCMONTH(ArrayList param)
		{
			string date = param[0].ToString();
			string inc = param[1].ToString();
			return Utils.Str2DateTime(date, DateTime.MinValue).AddMonths(Utils.Str2Int(inc, 0)).ToString();
		}

		public object INCWEEK(ArrayList param)
		{
			string date = param[0].ToString();
			string inc = param[1].ToString();
			return Utils.Str2DateTime(date, DateTime.MinValue).AddDays((double)(Utils.Str2Int(inc, 0) * 7)).ToString();
		}

		public object INCDAY(ArrayList param)
		{
			string date = param[0].ToString();
			string inc = param[1].ToString();
			return Utils.Str2DateTime(date, DateTime.MinValue).AddDays((double)Utils.Str2Int(inc, 0)).ToString();
		}

		public object HTMLTEXT(ArrayList param)
		{
			string result = string.Empty;
			bool flag = param.Count > 0;
			if (flag)
			{
				HtmlToText html2Text = new HtmlToText();
				result = html2Text.Convert(param[0].ToString());
			}
			return result;
		}

		private void ThrowDoubleError(ArrayList param, int paramCount)
		{
			bool flag = param.Count != paramCount;
			if (flag)
			{
				throw new Exception();
			}
		}

		private void ThrowStringError(ArrayList param, int paramCount)
		{
			bool flag = param.Count != paramCount;
			if (flag)
			{
				throw new Exception();
			}
			while (paramCount != 0)
			{
				bool flag2 = !param[paramCount - 1].GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
				if (flag2)
				{
					throw new Exception();
				}
				paramCount--;
			}
		}
	}
}
