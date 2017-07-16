using System;
using System.Collections.Generic;

namespace TIM.T_KERNEL.Helper
{
	public class QRBitBuffer
	{
		internal List<int> m_buffer = new List<int>();

		private int m_length = 0;

		public bool Get(int index)
		{
			return (Convert.ToUInt32(this.m_buffer[Convert.ToInt32(Math.Floor(index / 8.0))]) >> 7 - index % 8 & 1u) == 1u;
		}

		public void Put(int num, int length)
		{
			for (int index = 0; index < length; index++)
			{
				this.PutBit((Convert.ToUInt32(num) >> length - index - 1 & 1u) == 1u);
			}
		}

		public int GetLengthInBits()
		{
			return this.m_length;
		}

		public void PutBit(bool bit)
		{
			int num = (int)Math.Floor(this.m_length / 8.0);
			bool flag = this.m_buffer.Count <= num;
			if (flag)
			{
				this.m_buffer.Add(0);
			}
			if (bit)
			{
				List<int> list;
				int index;
				(list = this.m_buffer)[index = num] = (list[index] | (int)(Convert.ToUInt32(128) >> this.m_length % 8));
			}
			this.m_length++;
		}
	}
}
