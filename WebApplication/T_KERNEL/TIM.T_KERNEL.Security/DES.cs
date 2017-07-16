using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TIM.T_KERNEL.Security
{
	public class DES
	{
		private static readonly string DesKey = "12345678";

		public static string Encrypt(string str)
		{
			DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
			byte[] bytes = Encoding.Default.GetBytes(str);
			cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(DES.DesKey);
			cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(DES.DesKey);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = memoryStream.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				byte num = array[i];
				stringBuilder.AppendFormat("{0:X2}", num);
			}
			return stringBuilder.ToString();
		}

		public static string Decrypt(string str)
		{
			DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
			byte[] buffer = new byte[str.Length / 2];
			for (int index = 0; index < str.Length / 2; index++)
			{
				buffer[index] = (byte)Convert.ToInt32(str.Substring(index * 2, 2), 16);
			}
			cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(DES.DesKey);
			cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(DES.DesKey);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(buffer, 0, buffer.Length);
			cryptoStream.FlushFinalBlock();
			StringBuilder stringBuilder = new StringBuilder();
			return Encoding.Default.GetString(memoryStream.ToArray());
		}
	}
}
