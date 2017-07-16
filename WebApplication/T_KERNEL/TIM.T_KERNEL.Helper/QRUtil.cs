using System;

namespace TIM.T_KERNEL.Helper
{
	internal static class QRUtil
	{
		internal const int G15 = 1335;

		internal const int G15_MASK = 21522;

		internal const int G18 = 7973;

		internal static readonly int[][] PATTERN_POSITION_TABLE = new int[][]
		{
			new int[0],
			new int[]
			{
				6,
				18
			},
			new int[]
			{
				6,
				22
			},
			new int[]
			{
				6,
				26
			},
			new int[]
			{
				6,
				30
			},
			new int[]
			{
				6,
				34
			},
			new int[]
			{
				6,
				22,
				38
			},
			new int[]
			{
				6,
				24,
				42
			},
			new int[]
			{
				6,
				26,
				46
			},
			new int[]
			{
				6,
				28,
				50
			},
			new int[]
			{
				6,
				30,
				54
			},
			new int[]
			{
				6,
				32,
				58
			},
			new int[]
			{
				6,
				34,
				62
			},
			new int[]
			{
				6,
				26,
				46,
				66
			},
			new int[]
			{
				6,
				26,
				48,
				70
			},
			new int[]
			{
				6,
				26,
				50,
				74
			},
			new int[]
			{
				6,
				30,
				54,
				78
			},
			new int[]
			{
				6,
				30,
				56,
				82
			},
			new int[]
			{
				6,
				30,
				58,
				86
			},
			new int[]
			{
				6,
				34,
				62,
				90
			},
			new int[]
			{
				6,
				28,
				50,
				72,
				94
			},
			new int[]
			{
				6,
				26,
				50,
				74,
				98
			},
			new int[]
			{
				6,
				30,
				54,
				78,
				102
			},
			new int[]
			{
				6,
				28,
				54,
				80,
				106
			},
			new int[]
			{
				6,
				32,
				58,
				84,
				110
			},
			new int[]
			{
				6,
				30,
				58,
				86,
				114
			},
			new int[]
			{
				6,
				34,
				62,
				90,
				118
			},
			new int[]
			{
				6,
				26,
				50,
				74,
				98,
				122
			},
			new int[]
			{
				6,
				30,
				54,
				78,
				102,
				126
			},
			new int[]
			{
				6,
				26,
				52,
				78,
				104,
				130
			},
			new int[]
			{
				6,
				30,
				56,
				82,
				108,
				134
			},
			new int[]
			{
				6,
				34,
				60,
				86,
				112,
				138
			},
			new int[]
			{
				6,
				30,
				58,
				86,
				114,
				142
			},
			new int[]
			{
				6,
				34,
				62,
				90,
				118,
				146
			},
			new int[]
			{
				6,
				30,
				54,
				78,
				102,
				126,
				150
			},
			new int[]
			{
				6,
				24,
				50,
				76,
				102,
				128,
				154
			},
			new int[]
			{
				6,
				28,
				54,
				80,
				106,
				132,
				158
			},
			new int[]
			{
				6,
				32,
				58,
				84,
				110,
				136,
				162
			},
			new int[]
			{
				6,
				26,
				54,
				82,
				110,
				138,
				166
			},
			new int[]
			{
				6,
				30,
				58,
				86,
				114,
				142,
				170
			}
		};

		internal static int GetBCHDigit(int dataInt)
		{
			int num = 0;
			for (uint i = Convert.ToUInt32(dataInt); i > 0u; i >>= 1)
			{
				num++;
			}
			return num;
		}

		internal static int GetBCHTypeInfo(int data)
		{
			int dataInt = data << 10;
			int num2;
			while ((num2 = QRUtil.GetBCHDigit(dataInt) - QRUtil.GetBCHDigit(1335)) >= 0)
			{
				dataInt ^= Convert.ToInt32(1335) << num2;
			}
			return (data << 10 | dataInt) ^ 21522;
		}

		internal static int GetBCHTypeNumber(int data)
		{
			int dataInt = data << 12;
			while (QRUtil.GetBCHDigit(dataInt) - QRUtil.GetBCHDigit(7973) >= 0)
			{
				dataInt ^= 7973 << QRUtil.GetBCHDigit(dataInt) - QRUtil.GetBCHDigit(7973);
			}
			return data << 12 | dataInt;
		}

		internal static QRPolynomial GetErrorCorrectPolynomial(int errorCorrectLength)
		{
			QRPolynomial polynomial = new QRPolynomial(new DataCache
			{
				1
			}, 0);
			for (int i = 0; i < errorCorrectLength; i++)
			{
				polynomial = polynomial.Multiply(new QRPolynomial(new DataCache
				{
					1,
					QRMath.GExp(i)
				}, 0));
			}
			return polynomial;
		}

		internal static int GetLengthInBits(QRMode mode, int type)
		{
			bool flag = 1 > type || type >= 10;
			if (!flag)
			{
				switch (mode)
				{
				case QRMode.MODE_NUMBER:
				{
					int result = 10;
					return result;
				}
				case QRMode.MODE_ALPHA_NUM:
				{
					int result = 9;
					return result;
				}
				case (QRMode)3:
					break;
				case QRMode.MODE_8BIT_BYTE:
				{
					int result = 8;
					return result;
				}
				default:
					if (mode == QRMode.MODE_KANJI)
					{
						int result = 8;
						return result;
					}
					break;
				}
				throw new Error("mode:" + mode);
			}
			bool flag2 = type >= 27;
			if (!flag2)
			{
				switch (mode)
				{
				case QRMode.MODE_NUMBER:
				{
					int result = 12;
					return result;
				}
				case QRMode.MODE_ALPHA_NUM:
				{
					int result = 11;
					return result;
				}
				case (QRMode)3:
					break;
				case QRMode.MODE_8BIT_BYTE:
				{
					int result = 16;
					return result;
				}
				default:
					if (mode == QRMode.MODE_KANJI)
					{
						int result = 10;
						return result;
					}
					break;
				}
				throw new Error("mode:" + mode);
			}
			bool flag3 = type >= 41;
			if (flag3)
			{
				throw new Error("type:" + type);
			}
			switch (mode)
			{
			case QRMode.MODE_NUMBER:
			{
				int result = 14;
				return result;
			}
			case QRMode.MODE_ALPHA_NUM:
			{
				int result = 13;
				return result;
			}
			case (QRMode)3:
				break;
			case QRMode.MODE_8BIT_BYTE:
			{
				int result = 16;
				return result;
			}
			default:
				if (mode == QRMode.MODE_KANJI)
				{
					int result = 12;
					return result;
				}
				break;
			}
			throw new Error("mode:" + mode);
		}

		internal static double GetLostPoint(QRCode qrCode)
		{
			int moduleCount = qrCode.GetModuleCount();
			double num2 = 0.0;
			for (int num3 = 0; num3 < moduleCount; num3++)
			{
				for (int num4 = 0; num4 < moduleCount; num4++)
				{
					int num5 = 0;
					bool flag = qrCode.IsDark(num3, num4);
					for (int i = -1; i <= 1; i++)
					{
						bool flag2 = num3 + i >= 0 && moduleCount > num3 + i;
						if (flag2)
						{
							for (int j = -1; j <= 1; j++)
							{
								bool flag3 = num4 + j >= 0 && moduleCount > num4 + j && (i != 0 || j != 0) && flag == qrCode.IsDark(num3 + i, num4 + j);
								if (flag3)
								{
									num5++;
								}
							}
						}
					}
					bool flag4 = num5 > 5;
					if (flag4)
					{
						num2 += (double)(3 + num5 - 5);
					}
				}
			}
			for (int num3 = 0; num3 < moduleCount - 1; num3++)
			{
				for (int num4 = 0; num4 < moduleCount - 1; num4++)
				{
					int num6 = 0;
					bool flag5 = qrCode.IsDark(num3, num4);
					if (flag5)
					{
						num6++;
					}
					bool flag6 = qrCode.IsDark(num3 + 1, num4);
					if (flag6)
					{
						num6++;
					}
					bool flag7 = qrCode.IsDark(num3, num4 + 1);
					if (flag7)
					{
						num6++;
					}
					bool flag8 = qrCode.IsDark(num3 + 1, num4 + 1);
					if (flag8)
					{
						num6++;
					}
					bool flag9 = num6 == 0 || num6 == 4;
					if (flag9)
					{
						num2 += 3.0;
					}
				}
			}
			for (int num3 = 0; num3 < moduleCount; num3++)
			{
				for (int num4 = 0; num4 < moduleCount - 6; num4++)
				{
					bool flag10 = qrCode.IsDark(num3, num4) && !qrCode.IsDark(num3, num4 + 1) && qrCode.IsDark(num3, num4 + 2) && qrCode.IsDark(num3, num4 + 3) && qrCode.IsDark(num3, num4 + 4) && !qrCode.IsDark(num3, num4 + 5) && qrCode.IsDark(num3, num4 + 6);
					if (flag10)
					{
						num2 += 40.0;
					}
				}
			}
			for (int num4 = 0; num4 < moduleCount; num4++)
			{
				for (int num3 = 0; num3 < moduleCount - 6; num3++)
				{
					bool flag11 = qrCode.IsDark(num3, num4) && !qrCode.IsDark(num3 + 1, num4) && qrCode.IsDark(num3 + 2, num4) && qrCode.IsDark(num3 + 3, num4) && qrCode.IsDark(num3 + 4, num4) && !qrCode.IsDark(num3 + 5, num4) && qrCode.IsDark(num3 + 6, num4);
					if (flag11)
					{
						num2 += 40.0;
					}
				}
			}
			int num7 = 0;
			for (int num4 = 0; num4 < moduleCount; num4++)
			{
				for (int num3 = 0; num3 < moduleCount; num3++)
				{
					bool flag12 = qrCode.IsDark(num3, num4);
					if (flag12)
					{
						num7++;
					}
				}
			}
			double num8 = Math.Abs(100.0 * (double)num7 / (double)moduleCount / (double)moduleCount - 50.0) / 5.0;
			return num2 + num8 * 10.0;
		}

		internal static bool GetMask(QRMaskPattern maskPattern, int i, int j)
		{
			bool result;
			switch (maskPattern)
			{
			case QRMaskPattern.PATTERN000:
				result = ((i + j) % 2 == 0);
				break;
			case QRMaskPattern.PATTERN001:
				result = (i % 2 == 0);
				break;
			case QRMaskPattern.PATTERN010:
				result = (j % 3 == 0);
				break;
			case QRMaskPattern.PATTERN011:
				result = ((i + j) % 3 == 0);
				break;
			case QRMaskPattern.PATTERN100:
				result = ((Math.Floor(i / 2m) + Math.Floor(j / 3m)) % 2m == decimal.Zero);
				break;
			case QRMaskPattern.PATTERN101:
				result = (i * j % 2 + i * j % 3 == 0);
				break;
			case QRMaskPattern.PATTERN110:
				result = ((i * j % 2 + i * j % 3) % 2 == 0);
				break;
			case QRMaskPattern.PATTERN111:
				result = ((i * j % 3 + (i + j) % 2) % 2 == 0);
				break;
			default:
				throw new Error("bad maskPattern:" + maskPattern);
			}
			return result;
		}

		internal static int[] GetPatternPosition(int typeNumber)
		{
			return QRUtil.PATTERN_POSITION_TABLE[typeNumber - 1];
		}
	}
}
