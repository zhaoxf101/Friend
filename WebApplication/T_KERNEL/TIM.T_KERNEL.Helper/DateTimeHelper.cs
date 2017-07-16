using System;

namespace TIM.T_KERNEL.Helper
{
	public static class DateTimeHelper
	{
		public static string To24Hour(this DateTime value)
		{
			return value.ToString("yyyy-MM-dd HH:mm:ss");
		}

		public static string To12Hour(this DateTime value)
		{
			return value.ToString("yyyy-MM-dd hh:mm:ss");
		}

		public static DateTime PrevDay(this DateTime value)
		{
			return value.AddDays(-1.0);
		}

		public static DateTime NextDay(this DateTime value)
		{
			return value.AddDays(1.0);
		}

		public static DateTime FirstDay(this DateTime value)
		{
			return value.AddDays((double)(1 - value.Day));
		}

		public static DateTime LastDay(this DateTime value)
		{
			return value.AddDays((double)(1 - value.Day)).AddMonths(1).AddDays(-1.0);
		}

		public static DateTime EndOfLastMonth(this DateTime value)
		{
			return value.AddDays((double)(1 - value.Day)).AddDays(-1.0);
		}

		public static DateTime FirstOfNextMonth(this DateTime value)
		{
			return value.AddDays((double)(1 - value.Day)).AddMonths(1).AddDays(-1.0).AddDays(1.0);
		}

		public static DateTime FirstDayOfYear(this DateTime value)
		{
			return string.Format("{0}-{1}-{2}", value.Year, "01", "01").ToDate();
		}

		public static DateTime LastDayOfYear(this DateTime value)
		{
			return string.Format("{0}-{1}-{2}", value.Year, "12", "31").ToDate();
		}

		public static int WeekOfYear(this DateTime value)
		{
			int num = Convert.ToInt32(Convert.ToDateTime(value.Year.ToString() + "-1-1 ").DayOfWeek);
			int num2 = value.DayOfYear - (7 - num);
			bool flag = num2 <= 0;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				int num3 = num2 / 7;
				bool flag2 = num2 % 7 != 0;
				if (flag2)
				{
					num3++;
				}
				result = num3 + 1;
			}
			return result;
		}

		public static int YearWeekCount(this DateTime value)
		{
			DateTime dateTime = DateTime.Parse(value.Year.ToString() + "-01-01");
			bool flag = Convert.ToInt32(dateTime.DayOfWeek) == 1;
			int result;
			if (flag)
			{
				result = dateTime.AddYears(1).AddDays(-1.0).DayOfYear / 7 + 1;
			}
			else
			{
				result = dateTime.AddYears(1).AddDays(-1.0).DayOfYear / 7 + 2;
			}
			return result;
		}

		public static void GetWeek(this DateTime value, int nNumWeek, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
		{
			DateTime dateTime = new DateTime(value.Year, 1, 1) + new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
			dtWeekStart = dateTime.AddDays((double)(-(double)dateTime.DayOfWeek));
			dtWeekeEnd = dateTime.AddDays((double)(DayOfWeek.Saturday - dateTime.DayOfWeek));
		}

		public static int DaysInMonth(this DateTime value)
		{
			return DateTime.DaysInMonth(value.Year, value.Month);
		}

		public static int DaysInYear(this DateTime value)
		{
			int num = 0;
			for (int month = 1; month <= 12; month++)
			{
				num += DateTime.DaysInMonth(value.Year, month);
			}
			return num;
		}

		public static string To16String(this DateTime value)
		{
			return Convert.ToString(value.Ticks, 16);
		}

		public static DateTime ToDateTime(this object value)
		{
			DateTime dateTime = AppRuntime.UltDateTime;
			bool flag = value == null || value == DBNull.Value;
			if (flag)
			{
				dateTime = AppRuntime.UltDateTime;
			}
			bool flag2 = value is DateTime;
			if (flag2)
			{
				dateTime = (DateTime)value;
			}
			bool flag3 = value is string;
			if (flag3)
			{
				dateTime = value.ToString().ToDateTime();
			}
			return dateTime;
		}

		public static string ToHourMinute(this DateTime value)
		{
			string str = value.Hour.ToString().PadLeft(2, '0');
			string str2 = ":";
			string str3 = value.Minute.ToString().PadLeft(2, '0');
			return str + str2 + str3;
		}

		public static string GetYearMonth(this DateTime value)
		{
			return string.Format("{0}-{1}", value.Year, value.Month);
		}

		public static string GetYear(this DateTime value)
		{
			return string.Format("{0}", value.Year);
		}
	}
}
