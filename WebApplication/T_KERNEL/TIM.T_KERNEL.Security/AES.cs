using System;
using System.Security.Cryptography;
using System.Text;

namespace TIM.T_KERNEL.Security
{
	public class AES
	{
		private static readonly string AesKey = "HoliWhl.HoliWhl.HoliWhl.HoliWhl.";

		public static string Encrypt(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(AES.AesKey);
			byte[] bytes2 = Encoding.UTF8.GetBytes(str);
			byte[] inArray = new RijndaelManaged
			{
				Key = bytes,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			}.CreateEncryptor().TransformFinalBlock(bytes2, 0, bytes2.Length);
			return Convert.ToBase64String(inArray, 0, inArray.Length);
		}

		public static string Decrypt(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(AES.AesKey);
			byte[] inputBuffer = Convert.FromBase64String(str);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.Key = bytes;
			rijndaelManaged.Mode = CipherMode.ECB;
			rijndaelManaged.Padding = PaddingMode.PKCS7;
			return Encoding.UTF8.GetString(rijndaelManaged.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
		}
	}
}
