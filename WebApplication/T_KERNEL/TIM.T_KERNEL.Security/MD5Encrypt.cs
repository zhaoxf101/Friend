using System;
using System.Security.Cryptography;
using System.Text;

namespace TIM.T_KERNEL.Security
{
	public class MD5Encrypt
	{
		public static string MD5Encoding(string rawPass)
		{
			byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(rawPass));
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = hash;
			for (int i = 0; i < array.Length; i++)
			{
				byte num = array[i];
				stringBuilder.Append(num.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		public static string MD5Encoding(string rawPass, object salt)
		{
			bool flag = salt == null;
			string result;
			if (flag)
			{
				result = rawPass;
			}
			else
			{
				result = MD5Encrypt.MD5Encoding(rawPass + "{" + salt.ToString() + "}");
			}
			return result;
		}
	}
}
