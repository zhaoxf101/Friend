using System;
using System.IO;

namespace TIM.T_KERNEL.Helper
{
	internal class CharUnit
	{
		internal char Char;

		internal byte m_pinyinCount;

		internal short[] PinyinIndexList;

		internal byte m_strokeNumber;

		internal static CharUnit Deserialize(BinaryReader binaryReader)
		{
			CharUnit unit = new CharUnit();
			unit.Char = binaryReader.ReadChar();
			unit.m_strokeNumber = binaryReader.ReadByte();
			unit.m_pinyinCount = binaryReader.ReadByte();
			unit.PinyinIndexList = new short[(int)unit.m_pinyinCount];
			for (int i = 0; i < (int)unit.m_pinyinCount; i++)
			{
				unit.PinyinIndexList[i] = binaryReader.ReadInt16();
			}
			return unit;
		}

		internal void Serialize(BinaryWriter binaryWriter)
		{
			binaryWriter.Write(this.Char);
			binaryWriter.Write(this.m_strokeNumber);
			binaryWriter.Write(this.m_pinyinCount);
			for (int i = 0; i < (int)this.m_pinyinCount; i++)
			{
				binaryWriter.Write(this.PinyinIndexList[i]);
			}
		}
	}
}
