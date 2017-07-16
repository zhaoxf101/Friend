using System;

namespace TIM.T_KERNEL.Helper
{
	internal static class QRMath
	{
		private static readonly int[] EXP_TABLE;

		private static readonly int[] LOG_TABLE;

		static QRMath()
		{
			QRMath.EXP_TABLE = new int[256];
			QRMath.LOG_TABLE = new int[256];
			for (int index = 0; index < 8; index++)
			{
				QRMath.EXP_TABLE[index] = 1 << index;
			}
			for (int index2 = 8; index2 < 256; index2++)
			{
				QRMath.EXP_TABLE[index2] = (QRMath.EXP_TABLE[index2 - 4] ^ QRMath.EXP_TABLE[index2 - 5] ^ QRMath.EXP_TABLE[index2 - 6] ^ QRMath.EXP_TABLE[index2 - 8]);
			}
			for (int index3 = 0; index3 < 255; index3++)
			{
				QRMath.LOG_TABLE[QRMath.EXP_TABLE[index3]] = index3;
			}
		}

		internal static int GLog(int n)
		{
			bool flag = n < 1;
			if (flag)
			{
				throw new Error("glog(" + n + ")");
			}
			return QRMath.LOG_TABLE[n];
		}

		internal static int GExp(int n)
		{
			while (n < 0)
			{
				n += 255;
			}
			while (n >= 256)
			{
				n -= 255;
			}
			return QRMath.EXP_TABLE[n];
		}
	}
}
