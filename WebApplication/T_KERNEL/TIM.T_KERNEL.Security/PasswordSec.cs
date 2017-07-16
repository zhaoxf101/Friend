using System;
using System.Text;

namespace TIM.T_KERNEL.Security
{
	public class PasswordSec
	{
		public static string Encode(string userId, string password)
		{
			decimal num = new decimal(0);
			int index = 0;
			byte[] bytes = Encoding.Default.GetBytes(userId + password);
			while (index < bytes.Length)
			{
				num = (num + bytes[index] * 0.320924197909306m) * 0.314159625358979m;
				index++;
			}
			string str = num.ToString("F18");
			return str.Substring(str.IndexOf('.') + 1, 10);
		}

		public static string SilverEncode(string userId, string password)
		{
			decimal num = new decimal(0);
			int index = 0;
			byte[] bytes = Encoding.Default.GetBytes(userId + password);
			while (index < bytes.Length)
			{
				num = (num + bytes[index] * 0.271828182845905m) * 0.314159625358979m;
				index++;
			}
			string str = num.ToString("F18");
			return str.Substring(str.IndexOf('.') + 1, 10);
		}
	}
}
