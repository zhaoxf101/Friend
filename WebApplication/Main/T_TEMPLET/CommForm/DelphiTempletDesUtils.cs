using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_WEBCTRL;
using TIM.T_ZIPLIB;

namespace TIM.T_TEMPLET.CommForm
{
	public class DelphiTempletDesUtils
	{
		public static void FillBBLXList(TimDropDownList ctrl, bool insertEmpty)
		{
			ctrl.Items.Clear();
			if (insertEmpty)
			{
				ctrl.Items.Add(new ListItem("", ""));
			}
			ctrl.Items.Add(new ListItem("日报", "D"));
			ctrl.Items.Add(new ListItem("周报", "W"));
			ctrl.Items.Add(new ListItem("月报", "M"));
			ctrl.Items.Add(new ListItem("年报", "Y"));
		}

		public static string GetDeBase64ZipEncry(string sEnStr)
		{
			byte[] bpath = Convert.FromBase64String(sEnStr);
			bpath = ZlibCompress.DecompressBytes(bpath);
			string sDeStr = Encoding.Default.GetString(bpath, 0, bpath.Length);
			DelphiDes des = new DelphiDes();
			return des.DecryStrHex(sDeStr, "uto@+~9%");
		}

		public static string GetEncryZipBase64(string sDeStr)
		{
			DelphiDes des = new DelphiDes();
			string sEnStr = des.EncryStrHex(sDeStr, "uto@+~9%");
			byte[] bEnStr = new byte[0];
			bEnStr = Encoding.Default.GetBytes(sEnStr);
			bEnStr = ZlibCompress.CompressBytes(bEnStr);
			return Convert.ToBase64String(bEnStr);
		}

		public static string GetBBLX(string sBBBH)
		{
			string R_BBLX = "";
			Database db = LogicContext.GetDatabase();
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add("   select BBSZ_BBLX from BBSZ ");
				hsql.Add(" where BBSZ_BH=:BBSZ_BH ");
				hsql.ParamByName("BBSZ_BH").Value = sBBBH;
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Rows.Count > 0;
				if (flag)
				{
					R_BBLX = ds.Tables[0].Rows[0]["BBSZ_BBLX"].ToString().Trim();
				}
			}
			catch
			{
			}
			return R_BBLX;
		}

		public static int GetBBQH(string sBBLX, DateTime dRQ)
		{
			int iBBQH = 0;
			bool flag = sBBLX.Equals("D");
			if (flag)
			{
				iBBQH = dRQ.DayOfYear;
			}
			bool flag2 = sBBLX.Equals("W");
			if (flag2)
			{
				iBBQH = DelphiTempletDesUtils.GetBBWeeks(dRQ);
			}
			bool flag3 = sBBLX.Equals("M");
			if (flag3)
			{
				iBBQH = dRQ.Month;
			}
			bool flag4 = sBBLX.Equals("Y");
			if (flag4)
			{
				iBBQH = 1;
			}
			return iBBQH;
		}

		public static bool RefBBStaEndDatetime(string sBBLX, DateTime dRQ, ref DateTime dBBStaDate, ref DateTime dBBEndDate)
		{
			bool flag = sBBLX.Equals("D");
			if (flag)
			{
				dBBStaDate = dRQ;
				dBBEndDate = dRQ;
			}
			bool flag2 = sBBLX.Equals("W");
			if (flag2)
			{
				dBBStaDate = DelphiTempletDesUtils.FirstDayOfWeek(dRQ);
				dBBEndDate = dBBStaDate.AddDays(6.0);
			}
			bool flag3 = sBBLX.Equals("M");
			if (flag3)
			{
				dBBStaDate = dRQ.AddDays((double)(1 - dRQ.Day));
				dBBEndDate = dRQ.AddMonths(1).AddDays((double)(-(double)dRQ.Day));
			}
			bool flag4 = sBBLX.Equals("Y");
			if (flag4)
			{
				dBBStaDate = new DateTime(dRQ.Year, 1, 1);
				dBBEndDate = new DateTime(dRQ.Year, 12, 31);
			}
			return true;
		}

		public static string GetBBDYYS(string sYSBH)
		{
			Database db = LogicContext.GetDatabase();
			string result;
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add(" select BBSZ_BBYS from BBSZ ");
				hsql.Add("WHERE BBSZ_BH = :BBSZ_BH");
				hsql.ParamByName("BBSZ_BH").Value = sYSBH;
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["BBSZ_BBYS"].ToString() != "";
				if (flag)
				{
					byte[] bybuf = new byte[0];
					bybuf = (byte[])ds.Tables[0].Rows[0]["BBSZ_BBYS"];
					string sBBYS = Encoding.Default.GetString(bybuf, 0, bybuf.Length);
					result = sBBYS;
				}
				else
				{
					result = "";
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static bool SaveBBSCYS(string sBBBH, int sBBND, int sBBQH, string sReport)
		{
			Database db = LogicContext.GetDatabase();
			bool result;
			try
			{
				HSQL hsql = new HSQL(db);
				byte[] bybuf = new byte[0];
				bybuf = Encoding.Default.GetBytes(sReport);
				hsql.Clear();
				hsql.Add(" update BBSC ");
				hsql.Add(" set BBSC_BBNR=:BBSC_BBNR ");
				hsql.Add("WHERE BBSC_BBBH = :BBSC_BBBH");
				hsql.Add("  and BBSC_BBND = :BBSC_BBND");
				hsql.Add("  and BBSC_BBQH = :BBSC_BBQH");
				hsql.ParamByName("BBSC_BBNR").Value = bybuf;
				hsql.ParamByName("BBSC_BBBH").Value = sBBBH;
				hsql.ParamByName("BBSC_BBND").Value = sBBND;
				hsql.ParamByName("BBSC_BBQH").Value = sBBQH;
				db.ExecSQL(hsql);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static string GetBBSCYS(string sBBBH, int iBBND, int iBBQH)
		{
			Database db = LogicContext.GetDatabase();
			string result;
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add(" select BBSC_BBNR from BBSC ");
				hsql.Add("WHERE BBSC_BBBH = :BBSC_BBBH");
				hsql.Add("  and BBSC_BBND = :BBSC_BBND");
				hsql.Add("  and BBSC_BBQH = :BBSC_BBQH");
				hsql.ParamByName("BBSC_BBBH").Value = sBBBH;
				hsql.ParamByName("BBSC_BBND").Value = iBBND;
				hsql.ParamByName("BBSC_BBQH").Value = iBBQH;
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["BBSC_BBNR"].ToString() != "";
				if (flag)
				{
					byte[] bybuf = new byte[0];
					bybuf = (byte[])ds.Tables[0].Rows[0]["BBSC_BBNR"];
					string sBBYS = Encoding.Default.GetString(bybuf, 0, bybuf.Length);
					result = sBBYS;
				}
				else
				{
					result = "";
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static string GetBBDYXML(string sBBBH)
		{
			string sBBDYXML = "";
			Database db = LogicContext.GetDatabase();
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add("   select BBSZ_BBLX from BBSZ ");
				hsql.Add(" where BBSZ_BH=:BBSZ_BH ");
				hsql.ParamByName("BBSZ_BH").Value = sBBBH;
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Rows.Count > 0;
				if (flag)
				{
					sBBDYXML = ds.Tables[0].Rows[0]["BBSZ_BBLX"].ToString().Trim();
				}
			}
			catch
			{
			}
			return sBBDYXML;
		}

		public static int GetBBMAXQH(string sBBBH, int iBBND)
		{
			Database db = LogicContext.GetDatabase();
			int result;
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add(" select isnull(max(BBSC_BBQH),0) as MAXBBQH from BBSC ");
				hsql.Add("WHERE BBSC_BBBH = :BBSC_BBBH");
				hsql.Add("  and BBSC_BBND = :BBSC_BBND");
				hsql.ParamByName("BBSC_BBBH").Value = sBBBH;
				hsql.ParamByName("BBSC_BBND").Value = iBBND;
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Rows.Count > 0;
				if (flag)
				{
					result = Convert.ToInt32(ds.Tables[0].Rows[0]["MAXBBQH"].ToString());
				}
				else
				{
					result = 0;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		public static void DecodeRC(string sRC, ref int iRow, ref int iCol)
		{
			int c = 1;
			while (c <= sRC.Length && !Regex.IsMatch(sRC[c - 1].ToString(), "^\\d{1}$"))
			{
				c++;
			}
			bool flag = c > 1 && c <= sRC.Length;
			if (flag)
			{
				iRow = Convert.ToInt32(sRC.Substring(c - 1, sRC.Length - c + 1));
				iCol = DelphiTempletDesUtils.GetCol(sRC.Substring(0, c - 1));
			}
		}

		public static int GetCol(string sCol)
		{
			int iResult = 0;
			bool flag = string.IsNullOrEmpty(sCol) || sCol.Length > 2;
			int result;
			if (flag)
			{
				result = iResult;
			}
			else
			{
				bool flag2 = sCol.Length == 2;
				if (flag2)
				{
					bool flag3 = !Regex.IsMatch(sCol, "^\\D{2}$");
					if (flag3)
					{
						result = iResult;
						return result;
					}
					string s = sCol[0].ToString();
					string s2 = sCol[1].ToString();
					int i = (int)(Encoding.ASCII.GetBytes(s)[0] - Encoding.ASCII.GetBytes("A")[0] + 1);
					int i2 = (int)(Encoding.ASCII.GetBytes(s2)[0] - Encoding.ASCII.GetBytes("A")[0] + 1);
					iResult = i * 26 + i2;
				}
				else
				{
					bool flag4 = sCol.Length == 1;
					if (flag4)
					{
						bool flag5 = !Regex.IsMatch(sCol, "^\\D{1}$");
						if (flag5)
						{
							result = iResult;
							return result;
						}
						iResult = (int)(Encoding.ASCII.GetBytes(sCol)[0] - Encoding.ASCII.GetBytes("A")[0] + 1);
					}
				}
				result = iResult;
			}
			return result;
		}

		public static bool HasBBLX_BBBH(string sBBLX, string sBBBH)
		{
			bool result = false;
			Database db = LogicContext.GetDatabase();
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add("  select 1 from BBSZ ");
				hsql.Add("  where BBSZ_BBLX=:BBSZ_BBLX ");
				hsql.Add("    and BBSZ_BH=:BBSZ_BH ");
				hsql.ParamByName("BBSZ_BBLX").Value = sBBLX;
				hsql.ParamByName("BBSZ_BH").Value = sBBBH;
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Rows.Count > 0;
				if (flag)
				{
					result = true;
				}
			}
			catch
			{
			}
			return result;
		}

		public static bool HasBBJG(string sBBBH, int iBBND, int iBBQH)
		{
			bool result = false;
			Database db = LogicContext.GetDatabase();
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add("  select 1 from BBSC ");
				hsql.Add("  where BBSC_BBBH=:BBSC_BBBH ");
				hsql.Add("    and BBSC_BBND=:BBSC_BBND ");
				hsql.Add("    and BBSC_BBQH=:BBSC_BBQH ");
				hsql.ParamByName("BBSC_BBBH").Value = sBBBH;
				hsql.ParamByName("BBSC_BBND").Value = iBBND;
				hsql.ParamByName("BBSC_BBQH").Value = iBBQH;
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Rows.Count > 0;
				if (flag)
				{
					result = true;
				}
			}
			catch
			{
			}
			return result;
		}

		public static bool InsertBBJG(string sBBBH, int iBBND, int iBBQH, DateTime dKSRQ, DateTime dJSRQ)
		{
			bool result = false;
			Database db = LogicContext.GetDatabase();
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add("INSERT INTO BBSC(");
				hsql.Add(" BBSC_BBBH, BBSC_BBND, BBSC_BBQH, BBSC_KSRQ,BBSC_JSRQ, BBSC_BZ  ");
				hsql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME)");
				hsql.Add("VALUES(");
				hsql.Add(" :BBSC_BBBH, :BBSC_BBND, :BBSC_BBQH, :BBSC_KSRQ,:BBSC_JSRQ, :BBSC_BZ  ");
				hsql.Add(",:CREATERID,:CREATER,:CREATEDTIME,:MODIFIERID,:MODIFIER,:MODIFIEDTIME)");
				hsql.ParamByName("BBSC_BBBH").Value = sBBBH;
				hsql.ParamByName("BBSC_BBND").Value = iBBND;
				hsql.ParamByName("BBSC_BBQH").Value = iBBQH;
				hsql.ParamByName("BBSC_KSRQ").Value = dKSRQ;
				hsql.ParamByName("BBSC_JSRQ").Value = dJSRQ;
				hsql.ParamByName("BBSC_BZ").Value = "报表服务生成";
				hsql.ParamByName("CREATERID").Value = "";
				hsql.ParamByName("CREATER").Value = "";
				hsql.ParamByName("CREATEDTIME").Value = DateTime.Now;
				hsql.ParamByName("MODIFIERID").Value = "";
				hsql.ParamByName("MODIFIER").Value = "";
				hsql.ParamByName("MODIFIEDTIME").Value = DateTime.Now;
				db.ExecSQL(hsql);
				result = true;
			}
			catch
			{
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

        public static int DayOfWeekInt(DateTime dRQ)
		{
			int week = 0;
			string dt = dRQ.DayOfWeek.ToString();
			string text = dt;
			uint num = ComputeStringHash(text);
			if (num <= 2582335447u)
			{
				if (num != 241744182u)
				{
					if (num != 978773849u)
					{
						if (num == 2582335447u)
						{
							if (text == "Thursday")
							{
								week = 4;
							}
						}
					}
					else if (text == "Monday")
					{
						week = 1;
					}
				}
				else if (text == "Saturday")
				{
					week = 6;
				}
			}
			else if (num <= 3505415673u)
			{
				if (num != 3154759506u)
				{
					if (num == 3505415673u)
					{
						if (text == "Sunday")
						{
							week = 7;
						}
					}
				}
				else if (text == "Friday")
				{
					week = 5;
				}
			}
			else if (num != 3894647671u)
			{
				if (num == 4263050160u)
				{
					if (text == "Tuesday")
					{
						week = 2;
					}
				}
			}
			else if (text == "Wednesday")
			{
				week = 3;
			}
			return week;
		}

		public static DateTime FirstDayOfWeek(DateTime dRQ)
		{
			int year = dRQ.Year;
			int iFirstWeekDay = DelphiTempletDesUtils.GetFirstDayIntWeek(year);
			int iWeekDay = DelphiTempletDesUtils.DayOfWeekInt(dRQ);
			bool flag = iWeekDay >= iFirstWeekDay;
			int iAddDay;
			if (flag)
			{
				iAddDay = iFirstWeekDay - iWeekDay;
			}
			else
			{
				iAddDay = iFirstWeekDay - iWeekDay - 7;
			}
			return dRQ.AddDays((double)iAddDay);
		}

		public static int GetFirstDayIntWeek(int iBBND)
		{
			int iWeek = 1;
			Database db = LogicContext.GetDatabase();
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add("  select BBZDY_ZKS from BBZDY ");
				hsql.Add("  where BBZDY_ND=:BBZDY_ND ");
				hsql.ParamByName("BBZDY_ND").Value = iBBND;
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Rows.Count > 0;
				if (flag)
				{
					iWeek = Convert.ToInt32(ds.Tables[0].Rows[0]["BBZDY_ZKS"].ToString());
				}
				else
				{
					iWeek = 1;
				}
			}
			catch
			{
			}
			return iWeek;
		}

		public static int GetBBWeeks(DateTime dRQ)
		{
			int iWeeks = dRQ.DayOfYear / 7 + 1;
			Database db = LogicContext.GetDatabase();
			try
			{
				HSQL hsql = new HSQL(db);
				hsql.Clear();
				hsql.Add("  select BBZDY_FIRRQ from BBZDY ");
				hsql.Add("  where BBZDY_FIRRQ<=:pRQ ");
				hsql.Add("    and BBZDY_LASRQ>=:pRQ ");
				hsql.ParamByName("pRQ").Value = dRQ;
				DataSet ds = db.OpenDataSet(hsql);
				bool flag = ds.Tables[0].Rows.Count > 0;
				if (flag)
				{
					iWeeks = (dRQ.DayOfYear - Convert.ToDateTime(ds.Tables[0].Rows[0]["BBZDY_FIRRQ"].ToString()).DayOfYear) / 7 + 1;
				}
			}
			catch
			{
			}
			return iWeeks;
		}
	}
}
