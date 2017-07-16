using System;
using System.Collections.Generic;

namespace TIM.T_KERNEL.Helper
{
	public class QRCode
	{
		private List<QR8bitByte> m_dataList = new List<QR8bitByte>();

		private const int PAD0 = 236;

		private const int PAD1 = 17;

		private int m_typeNumber;

		private DataCache m_dataCache;

		private int m_moduleCount;

		private bool?[][] m_modules;

		private QRErrorCorrectLevel m_errorCorrectLevel;

		public QRCode(Options options) : this(options.TypeNumber, options.CorrectLevel)
		{
			this.AddData(options.Text);
		}

		public QRCode(int typeNumber, QRErrorCorrectLevel level)
		{
			this.m_typeNumber = typeNumber;
			this.m_errorCorrectLevel = level;
			this.m_dataCache = null;
		}

		public void AddData(string data)
		{
			this.m_dataCache = null;
			this.m_dataList.Add(new QR8bitByte(data));
		}

		public void Make()
		{
			bool flag = this.m_typeNumber < 1;
			if (flag)
			{
				int num;
				for (num = 1; num < 40; num++)
				{
					List<QRRSBlock> rsBlocks = QRRSBlock.GetRSBlocks(num, this.m_errorCorrectLevel);
					QRBitBuffer buffer = new QRBitBuffer();
					int num2 = 0;
					for (int index = 0; index < rsBlocks.Count; index++)
					{
						num2 += rsBlocks[index].DataCount;
					}
					for (int index2 = 0; index2 < this.m_dataList.Count; index2++)
					{
						QR8bitByte qr8bitByte = this.m_dataList[index2];
						buffer.Put((int)qr8bitByte.Mode, 4);
						buffer.Put(qr8bitByte.Length, QRUtil.GetLengthInBits(qr8bitByte.Mode, num));
						qr8bitByte.Write(buffer);
					}
					bool flag2 = buffer.GetLengthInBits() <= num2 * 8;
					if (flag2)
					{
						break;
					}
				}
				this.m_typeNumber = num;
			}
			this.MakeImpl(false, this.GetBestMaskPattern());
		}

		private QRMaskPattern GetBestMaskPattern()
		{
			double num = 0.0;
			QRMaskPattern qrMaskPattern = QRMaskPattern.PATTERN000;
			for (int index = 0; index < 8; index++)
			{
				this.MakeImpl(true, (QRMaskPattern)index);
				double lostPoint = QRUtil.GetLostPoint(this);
				bool flag = index == 0 || num > lostPoint;
				if (flag)
				{
					num = lostPoint;
					qrMaskPattern = (QRMaskPattern)index;
				}
			}
			return qrMaskPattern;
		}

		private void MakeImpl(bool test, QRMaskPattern maskPattern)
		{
			this.m_moduleCount = this.m_typeNumber * 4 + 17;
			this.m_modules = new bool?[this.m_moduleCount][];
			for (int index = 0; index < this.m_moduleCount; index++)
			{
				this.m_modules[index] = new bool?[this.m_moduleCount];
				for (int index2 = 0; index2 < this.m_moduleCount; index2++)
				{
					this.m_modules[index][index2] = null;
				}
			}
			this.SetupPositionProbePattern(0, 0);
			this.SetupPositionProbePattern(this.m_moduleCount - 7, 0);
			this.SetupPositionProbePattern(0, this.m_moduleCount - 7);
			this.SetupPositionAdjustPattern();
			this.SetupTimingPattern();
			this.setupTypeInfo(test, maskPattern);
			bool flag = this.m_typeNumber >= 7;
			if (flag)
			{
				this.setupTypeNumber(test);
			}
			bool flag2 = this.m_dataCache == null;
			if (flag2)
			{
				this.m_dataCache = this.CreateData(this.m_typeNumber, this.m_errorCorrectLevel, this.m_dataList);
			}
			this.MapData(this.m_dataCache, maskPattern);
		}

		public bool IsDark(int row, int col)
		{
			return this.m_modules[row][col].Value;
		}

		private void SetupTimingPattern()
		{
			for (int index = 8; index < this.m_moduleCount - 8; index++)
			{
				bool flag = !this.m_modules[index][6].HasValue;
				if (flag)
				{
					this.m_modules[index][6] = new bool?(index % 2 == 0);
				}
			}
			for (int index2 = 8; index2 < this.m_moduleCount - 8; index2++)
			{
				bool flag2 = !this.m_modules[6][index2].HasValue;
				if (flag2)
				{
					this.m_modules[6][index2] = new bool?(index2 % 2 == 0);
				}
			}
		}

		private void setupTypeNumber(bool test)
		{
			int bchTypeNumber = QRUtil.GetBCHTypeNumber(this.m_typeNumber);
			for (int index = 0; index < 18; index++)
			{
				bool flag = !test && (bchTypeNumber >> index & 1) == 1;
				this.m_modules[(int)Math.Floor(index / 3m)][index % 3 + this.m_moduleCount - 8 - 3] = new bool?(flag);
			}
			for (int index2 = 0; index2 < 18; index2++)
			{
				bool flag2 = !test && (bchTypeNumber >> index2 & 1) == 1;
				this.m_modules[index2 % 3 + this.m_moduleCount - 8 - 3][(int)Math.Floor(index2 / 3m)] = new bool?(flag2);
			}
		}

		private void SetupPositionAdjustPattern()
		{
			int[] patternPosition = QRUtil.GetPatternPosition(this.m_typeNumber);
			for (int index = 0; index < patternPosition.Length; index++)
			{
				for (int index2 = 0; index2 < patternPosition.Length; index2++)
				{
					int index3 = patternPosition[index];
					int index4 = patternPosition[index2];
					bool flag = !this.m_modules[index3][index4].HasValue;
					if (flag)
					{
						for (int index5 = -2; index5 <= 2; index5++)
						{
							for (int index6 = -2; index6 <= 2; index6++)
							{
								int num = (index5 == -2 || index5 == 2 || index6 == -2 || index6 == 2) ? 0 : ((index5 != 0) ? 1 : ((index6 != 0) ? 1 : 0));
								this.m_modules[index3 + index5][index4 + index6] = ((num != 0) ? new bool?(false) : new bool?(true));
							}
						}
					}
				}
			}
		}

		private void setupTypeInfo(bool test, QRMaskPattern maskPattern)
		{
			int bchTypeInfo = QRUtil.GetBCHTypeInfo((int)((int)this.m_errorCorrectLevel << 3 | (int)maskPattern));
			for (int index = 0; index < 15; index++)
			{
				bool flag = !test && (bchTypeInfo >> index & 1) == 1;
				bool flag3 = index < 6;
				if (flag3)
				{
					this.m_modules[index][8] = new bool?(flag);
				}
				else
				{
					bool flag4 = index < 8;
					if (flag4)
					{
						this.m_modules[index + 1][8] = new bool?(flag);
					}
					else
					{
						this.m_modules[this.m_moduleCount - 15 + index][8] = new bool?(flag);
					}
				}
			}
			for (int index2 = 0; index2 < 15; index2++)
			{
				bool flag2 = !test && (bchTypeInfo >> index2 & 1) == 1;
				bool flag5 = index2 < 8;
				if (flag5)
				{
					this.m_modules[8][this.m_moduleCount - index2 - 1] = new bool?(flag2);
				}
				else
				{
					bool flag6 = index2 < 9;
					if (flag6)
					{
						this.m_modules[8][15 - index2 - 1 + 1] = new bool?(flag2);
					}
					else
					{
						this.m_modules[8][15 - index2 - 1] = new bool?(flag2);
					}
				}
			}
			this.m_modules[this.m_moduleCount - 8][8] = new bool?(!test);
		}

		private void MapData(DataCache data, QRMaskPattern maskPattern)
		{
			int num = -1;
			int i = this.m_moduleCount - 1;
			int num2 = 7;
			int index = 0;
			for (int num3 = this.m_moduleCount - 1; num3 > 0; num3 -= 2)
			{
				bool flag2 = num3 == 6;
				if (flag2)
				{
					num3--;
				}
				do
				{
					for (int index2 = 0; index2 < 2; index2++)
					{
						bool flag3 = !this.m_modules[i][num3 - index2].HasValue;
						if (flag3)
						{
							bool flag = false;
							bool flag4 = index < data.Count;
							if (flag4)
							{
								flag = ((Convert.ToUInt32(data[index]) >> num2 & 1u) == 1u);
							}
							bool mask = QRUtil.GetMask(maskPattern, i, num3 - index2);
							if (mask)
							{
								flag = !flag;
							}
							this.m_modules[i][num3 - index2] = new bool?(flag);
							num2--;
							bool flag5 = num2 == -1;
							if (flag5)
							{
								index++;
								num2 = 7;
							}
						}
					}
					i += num;
				}
				while (i >= 0 && this.m_moduleCount > i);
				i -= num;
				num = -num;
			}
		}

		private DataCache CreateData(int typeNumber, QRErrorCorrectLevel errorCorrectLevel, List<QR8bitByte> dataList)
		{
			List<QRRSBlock> rsBlocks = QRRSBlock.GetRSBlocks(typeNumber, errorCorrectLevel);
			QRBitBuffer buffer = new QRBitBuffer();
			for (int index = 0; index < dataList.Count; index++)
			{
				QR8bitByte qr8bitByte = dataList[index];
				buffer.Put((int)qr8bitByte.Mode, 4);
				buffer.Put(qr8bitByte.Length, QRUtil.GetLengthInBits(qr8bitByte.Mode, typeNumber));
				qr8bitByte.Write(buffer);
			}
			int num = 0;
			for (int index2 = 0; index2 < rsBlocks.Count; index2++)
			{
				num += rsBlocks[index2].DataCount;
			}
			bool flag = buffer.GetLengthInBits() > num * 8;
			if (flag)
			{
				throw new Error(string.Concat(new object[]
				{
					"code length overflow. (",
					buffer.GetLengthInBits(),
					">",
					num * 8,
					")"
				}));
			}
			bool flag2 = buffer.GetLengthInBits() + 4 <= num * 8;
			if (flag2)
			{
				buffer.Put(0, 4);
			}
			while (buffer.GetLengthInBits() % 8 != 0)
			{
				buffer.PutBit(false);
			}
			while (true)
			{
				bool flag3 = buffer.GetLengthInBits() < num * 8;
				if (!flag3)
				{
					break;
				}
				buffer.Put(236, 8);
				bool flag4 = buffer.GetLengthInBits() < num * 8;
				if (!flag4)
				{
					break;
				}
				buffer.Put(17, 8);
			}
			return this.CreateBytes(buffer, rsBlocks);
		}

		private DataCache CreateBytes(QRBitBuffer buffer, List<QRRSBlock> rsBlocks)
		{
			int num = 0;
			int val1_ = 0;
			int val1_2 = 0;
			DataCache[] dataCacheArray = new DataCache[rsBlocks.Count];
			DataCache[] dataCacheArray2 = new DataCache[rsBlocks.Count];
			for (int index = 0; index < rsBlocks.Count; index++)
			{
				int dataCount = rsBlocks[index].DataCount;
				int num2 = rsBlocks[index].TotalCount - dataCount;
				val1_ = Math.Max(val1_, dataCount);
				val1_2 = Math.Max(val1_2, num2);
				dataCacheArray[index] = new DataCache(dataCount);
				for (int index2 = 0; index2 < dataCacheArray[index].Count; index2++)
				{
					dataCacheArray[index][index2] = (255 & buffer.m_buffer[index2 + num]);
				}
				num += dataCount;
				QRPolynomial correctPolynomial = QRUtil.GetErrorCorrectPolynomial(num2);
				QRPolynomial qrPolynomial = new QRPolynomial(dataCacheArray[index], correctPolynomial.GetLength() - 1).Mod(correctPolynomial);
				dataCacheArray2[index] = new DataCache(correctPolynomial.GetLength() - 1);
				for (int index3 = 0; index3 < dataCacheArray2[index].Count; index3++)
				{
					int index4 = index3 + qrPolynomial.GetLength() - dataCacheArray2[index].Count;
					dataCacheArray2[index][index3] = ((index4 >= 0) ? qrPolynomial.Get(index4) : 0);
				}
			}
			int capacity = 0;
			for (int index5 = 0; index5 < rsBlocks.Count; index5++)
			{
				capacity += rsBlocks[index5].TotalCount;
			}
			DataCache dataCache = new DataCache(capacity);
			int num3 = 0;
			for (int index6 = 0; index6 < val1_; index6++)
			{
				for (int index7 = 0; index7 < rsBlocks.Count; index7++)
				{
					bool flag = index6 < dataCacheArray[index7].Count;
					if (flag)
					{
						dataCache[num3++] = dataCacheArray[index7][index6];
					}
				}
			}
			for (int index8 = 0; index8 < val1_2; index8++)
			{
				for (int index9 = 0; index9 < rsBlocks.Count; index9++)
				{
					bool flag2 = index8 < dataCacheArray2[index9].Count;
					if (flag2)
					{
						dataCache[num3++] = dataCacheArray2[index9][index8];
					}
				}
			}
			return dataCache;
		}

		private void SetupPositionProbePattern(int row, int col)
		{
			for (int index = -1; index <= 7; index++)
			{
				bool flag = row + index > -1 && this.m_moduleCount > row + index;
				if (flag)
				{
					for (int index2 = -1; index2 <= 7; index2++)
					{
						bool flag2 = col + index2 > -1 && this.m_moduleCount > col + index2;
						if (flag2)
						{
							int num = ((0 <= index && index <= 6 && (index2 == 0 || index2 == 6)) || (0 <= index2 && index2 <= 6 && (index == 0 || index == 6))) ? 0 : ((2 > index || index > 4 || 2 > index2) ? 1 : ((index2 > 4) ? 1 : 0));
							this.m_modules[row + index][col + index2] = ((num != 0) ? new bool?(false) : new bool?(true));
						}
					}
				}
			}
		}

		public int GetModuleCount()
		{
			return this.m_moduleCount;
		}

		internal int getBestMaskPattern()
		{
			double num = 0.0;
			int num2 = 0;
			for (int index = 0; index < 8; index++)
			{
				this.MakeImpl(true, (QRMaskPattern)index);
				double lostPoint = QRUtil.GetLostPoint(this);
				bool flag = index == 0 || num > lostPoint;
				if (flag)
				{
					num = lostPoint;
					num2 = index;
				}
			}
			return num2;
		}
	}
}
