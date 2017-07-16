using System;
using System.Collections.Generic;

namespace TIM.T_KERNEL.Helper
{
	internal struct QRPolynomial
	{
		private int[] m_num;

		public QRPolynomial(DataCache num, int shift)
		{
			this = default(QRPolynomial);
			bool flag = num == null;
			if (flag)
			{
				throw new Error();
			}
			int index = 0;
			while (index < num.Count && num[index] == 0)
			{
				index++;
			}
			this.m_num = new int[num.Count - index + shift];
			for (int index2 = 0; index2 < num.Count - index; index2++)
			{
				this.m_num[index2] = num[index2 + index];
			}
		}

		public int Get(int index)
		{
			return this.m_num[index];
		}

		public int GetLength()
		{
			return this.m_num.Length;
		}

		public QRPolynomial Multiply(QRPolynomial e)
		{
			DataCache num = new DataCache(this.GetLength() + e.GetLength() - 1);
			for (int index = 0; index < this.GetLength(); index++)
			{
				for (int index2 = 0; index2 < e.GetLength(); index2++)
				{
					List<int> list;
					int index3;
					(list = num)[index3 = index + index2] = (list[index3] ^ QRMath.GExp(QRMath.GLog(this.Get(index)) + QRMath.GLog(e.Get(index2))));
				}
			}
			return new QRPolynomial(num, 0);
		}

		public QRPolynomial Mod(QRPolynomial e)
		{
			bool flag = Convert.ToInt64(this.GetLength()) - Convert.ToInt64(e.GetLength()) < 0L;
			QRPolynomial result;
			if (flag)
			{
				result = this;
			}
			else
			{
				int num = QRMath.GLog(this.Get(0)) - QRMath.GLog(e.Get(0));
				DataCache num2 = new DataCache(this.GetLength());
				for (int index = 0; index < this.GetLength(); index++)
				{
					num2[index] = this.Get(index);
				}
				for (int index2 = 0; index2 < e.GetLength(); index2++)
				{
					List<int> list;
					int index3;
					(list = num2)[index3 = index2] = (list[index3] ^ QRMath.GExp(QRMath.GLog(e.Get(index2)) + num));
				}
				result = new QRPolynomial(num2, 0).Mod(e);
			}
			return result;
		}
	}
}
