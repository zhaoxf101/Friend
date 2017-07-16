using System;
using System.Collections.Generic;
using System.IO;

namespace TIM.T_KERNEL.Helper
{
	internal class CharDictionary
	{
		internal List<CharUnit> CharUnitTable;

		internal int Count;

		internal readonly short EndMark = 32767;

		internal int Length;

		internal short Offset;

		internal readonly byte[] Reserved = new byte[24];

		internal static CharDictionary Deserialize(BinaryReader binaryReader)
		{
			CharDictionary dictionary = new CharDictionary();
			binaryReader.ReadInt32();
			dictionary.Length = binaryReader.ReadInt32();
			dictionary.Count = binaryReader.ReadInt32();
			dictionary.Offset = binaryReader.ReadInt16();
			binaryReader.ReadBytes(24);
			dictionary.CharUnitTable = new List<CharUnit>();
			for (int i = 0; i < dictionary.Count; i++)
			{
				dictionary.CharUnitTable.Add(CharUnit.Deserialize(binaryReader));
			}
			binaryReader.ReadInt16();
			return dictionary;
		}

		internal CharUnit GetCharUnit(char ch)
		{
			CharUnitPredicate predicate = new CharUnitPredicate(ch);
			return this.CharUnitTable.Find(new Predicate<CharUnit>(predicate.Match));
		}

		internal CharUnit GetCharUnit(int index)
		{
			bool flag = index < 0 || index >= this.Count;
			CharUnit result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.CharUnitTable[index];
			}
			return result;
		}

		internal void Serialize(BinaryWriter binaryWriter)
		{
			binaryWriter.Write(this.Length);
			binaryWriter.Write(this.Count);
			binaryWriter.Write(this.Offset);
			binaryWriter.Write(this.Reserved);
			for (int i = 0; i < this.Count; i++)
			{
				this.CharUnitTable[i].Serialize(binaryWriter);
			}
			binaryWriter.Write(this.EndMark);
		}
	}
}
