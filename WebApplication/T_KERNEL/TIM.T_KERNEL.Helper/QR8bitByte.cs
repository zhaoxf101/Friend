using System;
using System.Text.RegularExpressions;

namespace TIM.T_KERNEL.Helper
{
	public struct QR8bitByte
	{
		public QRMode Mode
		{
			get;
			private set;
		}

		private string m_data
		{
			get;
			set;
		}

		public int Length
		{
			get
			{
				return this.m_data.Length;
			}
		}

		public QR8bitByte(string data)
		{
			this = default(QR8bitByte);
			this.Mode = QRMode.MODE_8BIT_BYTE;
			MatchEvaluator evaluator = new MatchEvaluator(QR8bitByte.ReplaceUTF8_1);
			MatchEvaluator evaluator2 = new MatchEvaluator(QR8bitByte.ReplaceUTF8_2);
			data = Regex.Replace(data, "[\u0080-߿]", evaluator);
			data = Regex.Replace(data, "[ࠀ-￿]", evaluator2);
			this.m_data = data;
		}

		public static string ReplaceUTF8_1(Match m)
		{
			int num = (int)m.Value.ToCharArray()[0];
			return new string(new char[]
			{
				(char)(192 | num >> 6),
				(char)(128 | (num & 63))
			});
		}

		public static string ReplaceUTF8_2(Match m)
		{
			int num = (int)m.Value.ToCharArray()[0];
			return new string(new char[]
			{
				(char)(224 | num >> 12),
				(char)(128 | (num >> 6 & 63)),
				(char)(128 | (num & 63))
			});
		}

		public void Write(QRBitBuffer buffer)
		{
			for (int index = 0; index < this.m_data.Length; index++)
			{
				buffer.Put((int)this.m_data[index], 8);
			}
		}
	}
}
