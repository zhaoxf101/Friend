using System;

namespace TIM.T_KERNEL.Helper
{
	public static class NumericHelper
	{
		public static int ToInt(this double value)
		{
			return (int)value;
		}

		public static double RoundX(this double value, int decimalPlaces)
		{
			double result;
			try
			{
				result = Math.Round(value, decimalPlaces, MidpointRounding.AwayFromZero);
			}
			catch
			{
				throw new Exception("计算四舍五入异常！");
			}
			return result;
		}

		public static double NaNToZero(this double value)
		{
			bool flag = double.IsNaN(value);
			double result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				result = value;
			}
			return result;
		}

		public static decimal RoundUp(this decimal value, sbyte digits)
		{
			bool flag = digits == 0;
			decimal result;
			if (flag)
			{
				result = ((value >= 0m) ? decimal.Ceiling(value) : decimal.Floor(value));
			}
			else
			{
				decimal num = Convert.ToDecimal(Math.Pow(10.0, (double)digits));
				result = ((value >= 0m) ? decimal.Ceiling(value * num) : decimal.Floor(value * num)) / num;
			}
			return result;
		}

		public static decimal RoundDown(this decimal value, sbyte digits)
		{
			bool flag = digits == 0;
			decimal result;
			if (flag)
			{
				result = ((value >= 0m) ? decimal.Floor(value) : decimal.Ceiling(value));
			}
			else
			{
				decimal num = Convert.ToDecimal(Math.Pow(10.0, (double)digits));
				result = ((value >= 0m) ? decimal.Floor(value * num) : decimal.Ceiling(value * num)) / num;
			}
			return result;
		}

		public static double RoundUp(this double value, sbyte digits)
		{
			return decimal.ToDouble(Convert.ToDecimal(value).RoundUp(digits));
		}

		public static double RoundDown(this double value, sbyte digits)
		{
			return decimal.ToDouble(Convert.ToDecimal(value).RoundDown(digits));
		}

		public static string FormatFileSize(this long filesize)
		{
			string str = string.Empty;
			bool flag = filesize < 0L;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("filesize");
			}
			return (filesize < 1073741824L) ? ((filesize < 1048576L) ? ((filesize < 1024L) ? string.Format("{0:0.00} bytes", filesize) : string.Format("{0:0.00} KB", (double)filesize / 1024.0)) : string.Format("{0:0.00} MB", (double)filesize / 1048576.0)) : string.Format("{0:0.00} GB", (double)filesize / 1073741824.0);
		}
	}
}
