using System;
using System.Collections.Generic;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace TIM.T_KERNEL.Security
{
	public class Sys
	{
		private static string RNGCryptoServiceProviderMethod(int length)
		{
			char[] chArray = "abcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();
			byte[] data = new byte[1];
			RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
			cryptoServiceProvider.GetNonZeroBytes(data);
			byte[] data2 = new byte[length];
			cryptoServiceProvider.GetNonZeroBytes(data2);
			StringBuilder stringBuilder = new StringBuilder(length);
			byte[] array = data2;
			for (int i = 0; i < array.Length; i++)
			{
				byte num = array[i];
				stringBuilder.Append(chArray[(int)num % (chArray.Length - 1)]);
			}
			return stringBuilder.ToString();
		}

		public static string Gen24BitGuid()
		{
			return Sys.RNGCryptoServiceProviderMethod(24);
		}

		public static string CreateMachineKey(int len)
		{
			byte[] data = new byte[len];
			new RNGCryptoServiceProvider().GetBytes(data);
			StringBuilder stringBuilder = new StringBuilder();
			for (int index = 0; index < data.Length; index++)
			{
				stringBuilder.Append(string.Format("{0:X2}", data[index]));
			}
			return stringBuilder.ToString();
		}

		public static string GenerateMachineID(string strProductName)
		{
			return Sys.GenerateKey(strProductName, 5) + "-" + Sys.GenerateKey(Sys.GetHardDiskVol(), 10);
		}

		public static string GetHardDiskVol()
		{
			ManagementObject mo = new ManagementObject();
			int retVal = 0;
			int a = 0;
			int b = 0;
			string str = null;
			int i = ServerHardwareInfo.GetVolumeInformation("c:\\", str, 256, ref retVal, a, b, null, 256);
			return retVal.ToString();
		}

		private static string GenerateKey(string keyString, int nlen)
		{
			string serialNumber = keyString.Trim();
			List<int> sn = new List<int>(21);
			long num = 0L;
			long num2 = 0L;
			long num3 = 0L;
			long num4 = 0L;
			char[] serial = serialNumber.ToCharArray();
			int len = serialNumber.Length;
			bool flag = len != 0;
			if (flag)
			{
				for (int i = 1; i <= len; i++)
				{
					int istr = (int)serial[i - 1];
					double db = Math.Sqrt((double)istr);
					num = (num + (long)(istr * i * i) * ((long)i * (long)(db + 1.0))) % 100000L;
					db = Math.Pow((double)istr, 2.0);
					num2 = (num2 * (long)i + (long)db * (long)i) % 100000L;
					db = Math.Sqrt((double)num);
					num3 = (num2 + (long)db) % 100000L;
					db = Math.Sqrt((double)num3);
					num4 = (num * (long)i + num2 * (long)i * (long)i + (long)db) % 100000L;
				}
			}
			for (int i = 0; i < 5; i++)
			{
				sn.Add((int)(num + 31L + (long)(i * i * i)) % 128);
			}
			for (int i = 5; i < 10; i++)
			{
				sn.Add((int)(num2 + 31L + (long)(i * i * i)) % 128);
			}
			for (int i = 10; i < 15; i++)
			{
				sn.Add((int)(num3 + 31L + (long)(i * i * i)) % 128);
			}
			for (int i = 15; i < 20; i++)
			{
				sn.Add((int)(num4 + 31L + (long)(i * i * i)) % 128);
			}
			sn.Add(0);
			for (int i = 0; i < 20; i++)
			{
				while ((sn[i] < 49 || sn[i] > 57) && (sn[i] < 65 || sn[i] > 90))
				{
					sn[i] = (sn[i] + 31 + 7 * i) % 128;
				}
			}
			string p;
			if (nlen != 5)
			{
				if (nlen != 10)
				{
					p = string.Format("{0}{1}{2}{3}{4}-{5}{6}{7}{8}{9}-{10}{11}{12}{13}{14}-{15}{16}{17}{18}{19}", new object[]
					{
						(char)sn[0],
						(char)sn[1],
						(char)sn[2],
						(char)sn[3],
						(char)sn[4],
						(char)sn[5],
						(char)sn[6],
						(char)sn[7],
						(char)sn[8],
						(char)sn[9],
						(char)sn[10],
						(char)sn[11],
						(char)sn[12],
						(char)sn[13],
						(char)sn[14],
						(char)sn[15],
						(char)sn[16],
						(char)sn[17],
						(char)sn[18],
						(char)sn[19]
					});
				}
				else
				{
					p = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", new object[]
					{
						(char)sn[0],
						(char)sn[1],
						(char)sn[2],
						(char)sn[3],
						(char)sn[4],
						(char)sn[15],
						(char)sn[16],
						(char)sn[17],
						(char)sn[18],
						(char)sn[19]
					});
				}
			}
			else
			{
				p = string.Format("{0}{1}{2}{3}{4}", new object[]
				{
					(char)sn[0],
					(char)sn[1],
					(char)sn[2],
					(char)sn[3],
					(char)sn[4]
				});
			}
			return p.Replace("1", "Z").Replace("I", "3").Replace("0", "A").Replace("O", "9");
		}
	}
}
