using System;
using System.Linq;
using System.Text;

namespace TIM.T_KERNEL.Common
{
	internal class UtoBase64
	{
		private static readonly char[] intToBase64 = new char[]
		{
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'-',
			'_'
		};

		private static readonly int[] base64ToInt = new int[]
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			62,
			-1,
			-1,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25,
			-1,
			-1,
			-1,
			-1,
			63,
			-1,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51
		};

		private const int maxLength = 1024;

		public static string ToBase64(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			int length = bytes.Length;
			int num = length / 3;
			int num2 = length - 3 * num;
			StringBuilder stringBuilder = new StringBuilder(4 * ((length + 2) / 3));
			int num3 = 0;
			for (int index = 0; index < num; index++)
			{
				byte[] numArray = bytes;
				int index2 = num3;
				int num4 = 1;
				int num5 = index2 + num4;
				int num6 = (int)(numArray[index2] & 255);
				byte[] numArray2 = bytes;
				int index3 = num5;
				int num7 = 1;
				int num8 = index3 + num7;
				int num9 = (int)(numArray2[index3] & 255);
				byte[] numArray3 = bytes;
				int index4 = num8;
				int num10 = 1;
				num3 = index4 + num10;
				int num11 = (int)(numArray3[index4] & 255);
				stringBuilder.Append(UtoBase64.intToBase64[num6 >> 2]);
				stringBuilder.Append(UtoBase64.intToBase64[(num6 << 4 & 63) | num9 >> 4]);
				stringBuilder.Append(UtoBase64.intToBase64[(num9 << 2 & 63) | num11 >> 6]);
				stringBuilder.Append(UtoBase64.intToBase64[num11 & 63]);
			}
			bool flag = num2 != 0;
			if (flag)
			{
				byte[] numArray4 = bytes;
				int index5 = num3;
				int num12 = 1;
				int num13 = index5 + num12;
				int num14 = (int)(numArray4[index5] & 255);
				stringBuilder.Append(UtoBase64.intToBase64[num14 >> 2]);
				bool flag2 = num2 == 1;
				if (flag2)
				{
					stringBuilder.Append(UtoBase64.intToBase64[num14 << 4 & 63]);
				}
				else
				{
					byte[] numArray5 = bytes;
					int index6 = num13;
					int num15 = 1;
					int num16 = index6 + num15;
					int num17 = (int)(numArray5[index6] & 255);
					stringBuilder.Append(UtoBase64.intToBase64[(num14 << 4 & 63) | num17 >> 4]);
					stringBuilder.Append(UtoBase64.intToBase64[num17 << 2 & 63]);
				}
			}
			return stringBuilder.ToString();
		}

		public static string FromBase64(string s)
		{
			int totalWidth = s.Length % 4;
			bool flag = totalWidth != 0;
			if (flag)
			{
				totalWidth = 4 - totalWidth;
			}
			s += string.Empty.PadRight(totalWidth, '=');
			int length = s.Length;
			int num = length / 4;
			bool flag2 = 4 * num != length;
			if (flag2)
			{
				throw new Exception("字串长度必须是4的倍数");
			}
			int num2 = 0;
			int num3 = num;
			bool flag3 = length != 0;
			if (flag3)
			{
				bool flag4 = s.ElementAt(length - 1) == '=';
				if (flag4)
				{
					num2++;
					num3--;
				}
				bool flag5 = s.ElementAt(length - 2) == '=';
				if (flag5)
				{
					num2++;
				}
			}
			byte[] bytes = new byte[3 * num - num2];
			try
			{
				int num4 = 0;
				int num5 = 0;
				for (int index = 0; index < num3; index++)
				{
					string str = s;
					int index2 = num4;
					int num6 = 1;
					int num7 = index2 + num6;
					int num8 = UtoBase64.base64toInt(str.ElementAt(index2), UtoBase64.base64ToInt);
					string str2 = s;
					int index3 = num7;
					int num9 = 1;
					int num10 = index3 + num9;
					int num11 = UtoBase64.base64toInt(str2.ElementAt(index3), UtoBase64.base64ToInt);
					string str3 = s;
					int index4 = num10;
					int num12 = 1;
					int num13 = index4 + num12;
					int num14 = UtoBase64.base64toInt(str3.ElementAt(index4), UtoBase64.base64ToInt);
					string str4 = s;
					int index5 = num13;
					int num15 = 1;
					num4 = index5 + num15;
					int num16 = UtoBase64.base64toInt(str4.ElementAt(index5), UtoBase64.base64ToInt);
					byte[] numArray = bytes;
					int index6 = num5;
					int num17 = 1;
					int num18 = index6 + num17;
					int num19 = (int)((byte)(num8 << 2 | num11 >> 4));
					numArray[index6] = (byte)num19;
					byte[] numArray2 = bytes;
					int index7 = num18;
					int num20 = 1;
					int num21 = index7 + num20;
					int num22 = (int)((byte)(num11 << 4 | num14 >> 2));
					numArray2[index7] = (byte)num22;
					byte[] numArray3 = bytes;
					int index8 = num21;
					int num23 = 1;
					num5 = index8 + num23;
					int num24 = (int)((byte)(num14 << 6 | num16));
					numArray3[index8] = (byte)num24;
				}
				bool flag6 = num2 != 0;
				if (flag6)
				{
					string str5 = s;
					int index9 = num4;
					int num25 = 1;
					int num26 = index9 + num25;
					int num27 = UtoBase64.base64toInt(str5.ElementAt(index9), UtoBase64.base64ToInt);
					string str6 = s;
					int index10 = num26;
					int num28 = 1;
					int num29 = index10 + num28;
					int num30 = UtoBase64.base64toInt(str6.ElementAt(index10), UtoBase64.base64ToInt);
					byte[] numArray4 = bytes;
					int index11 = num5;
					int num31 = 1;
					int num32 = index11 + num31;
					int num33 = (int)((byte)(num27 << 2 | num30 >> 4));
					numArray4[index11] = (byte)num33;
					bool flag7 = num2 == 1;
					if (flag7)
					{
						string str7 = s;
						int index12 = num29;
						int num34 = 1;
						int num35 = index12 + num34;
						int num36 = UtoBase64.base64toInt(str7.ElementAt(index12), UtoBase64.base64ToInt);
						byte[] numArray5 = bytes;
						int index13 = num32;
						int num37 = 1;
						int num38 = index13 + num37;
						int num39 = (int)((byte)(num30 << 4 | num36 >> 2));
						numArray5[index13] = (byte)num39;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return Encoding.UTF8.GetString(bytes);
		}

		private static int base64toInt(char c, int[] alphaToInt)
		{
			int num = alphaToInt[(int)c];
			bool flag = num < 0;
			if (flag)
			{
				throw new Exception("非法索引值");
			}
			return num;
		}
	}
}
