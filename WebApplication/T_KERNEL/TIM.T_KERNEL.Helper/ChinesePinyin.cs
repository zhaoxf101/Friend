using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace TIM.T_KERNEL.Helper
{
	public class ChinesePinyin
	{
		private string[] m_pinyinList = new string[8];

		private const short MaxPolyphoneNum = 8;

		private static CharDictionary charDictionary;

		private static PinyinDictionary pinyinDictionary;

		private static Dictionary<char, string> pinyinCustom;

		private char m_chineseCharacter;

		private bool m_isPolyphone;

		private short m_pinyinCount;

		private short m_strokeNumber;

		private string m_unknownFill;

		private string m_chineseString;

		private string m_unknownChars;

		private string m_strPinYin;

		private string m_strPolyphone1;

		private string m_strShouZiMu;

		private string m_strPolyphone2;

		public char ChineseCharacter
		{
			get
			{
				return this.m_chineseCharacter;
			}
		}

		public bool IsPolyphone
		{
			get
			{
				return this.m_isPolyphone;
			}
		}

		public short PinyinCount
		{
			get
			{
				return this.m_pinyinCount;
			}
		}

		public ReadOnlyCollection<string> Pinyins
		{
			get
			{
				return new ReadOnlyCollection<string>(this.m_pinyinList);
			}
		}

		public short StrokeNumber
		{
			get
			{
				return this.m_strokeNumber;
			}
		}

		public string ChineseString
		{
			get
			{
				return this.m_chineseString;
			}
			set
			{
				this.m_chineseString = value;
				this.DoConvertToPinyin();
			}
		}

		public string UnknownFill
		{
			get
			{
				return this.m_unknownFill;
			}
		}

		public string PinYinString
		{
			get
			{
				return this.m_strPinYin;
			}
		}

		public string ShouZiMuString
		{
			get
			{
				return this.m_strShouZiMu;
			}
		}

		public string Polyphone1
		{
			get
			{
				return this.m_strPolyphone1;
			}
		}

		public string Polyphone2
		{
			get
			{
				return this.m_strPolyphone2;
			}
		}

		public string UnknownChars
		{
			get
			{
				return this.m_unknownChars;
			}
		}

		static ChinesePinyin()
		{
			ChinesePinyin.LoadResource();
			ChinesePinyin.LoadCustomResource();
		}

		public ChinesePinyin()
		{
			this.m_unknownFill = "";
		}

		public ChinesePinyin(string chrUnknownFill)
		{
			this.m_unknownFill = chrUnknownFill;
		}

		private static void LoadCustomResource()
		{
		}

		private static void LoadResource()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("TIM.T_KERNEL.Helper.PinyinDict.resources"))
			{
				using (ResourceReader resourceReader = new ResourceReader(manifestResourceStream))
				{
					string resourceType;
					byte[] resourceData;
					resourceReader.GetResourceData("PinyinDictionary", out resourceType, out resourceData);
					using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(resourceData)))
					{
						ChinesePinyin.pinyinDictionary = PinyinDictionary.Deserialize(binaryReader);
					}
				}
			}
			using (Stream manifestResourceStream2 = executingAssembly.GetManifestResourceStream("TIM.T_KERNEL.Helper.CharDict.resources"))
			{
				using (ResourceReader resourceReader2 = new ResourceReader(manifestResourceStream2))
				{
					string resourceType;
					byte[] resourceData;
					resourceReader2.GetResourceData("CharDictionary", out resourceType, out resourceData);
					using (BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(resourceData)))
					{
						ChinesePinyin.charDictionary = CharDictionary.Deserialize(binaryReader2);
					}
				}
			}
			using (Stream manifestResourceStream3 = executingAssembly.GetManifestResourceStream("TIM.T_KERNEL.Helper.ChinesePY.txt"))
			{
				using (TextReader textReader = new StreamReader(manifestResourceStream3, Encoding.UTF8))
				{
					ChinesePinyin.pinyinCustom = new Dictionary<char, string>();
					while (true)
					{
						string str = textReader.ReadLine();
						bool flag = str != null;
						if (!flag)
						{
							break;
						}
						string[] strArray = str.Trim().Split(new char[]
						{
							' '
						});
						bool flag2 = strArray.Length > 1 && strArray[0].Length == 1 && !ChinesePinyin.pinyinCustom.ContainsKey(strArray[0][0]);
						if (flag2)
						{
							string str2 = strArray[1].ToUpper();
							bool flag3 = str2[str2.Length - 1] >= '1' && str2[str2.Length - 1] <= '5';
							if (flag3)
							{
								str2 = str2.Substring(0, str2.Length - 1);
							}
							ChinesePinyin.pinyinCustom.Add(strArray[0][0], str2);
						}
					}
				}
			}
		}

		internal static void ResourceToTxt(string strTxtFile)
		{
			using (FileStream fileStream = new FileStream(strTxtFile, FileMode.Create, FileAccess.Write))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
				{
					streamWriter.Write(ChinesePinyin.charDictionary.Count);
					streamWriter.WriteLine("");
					for (int index = 0; index < ChinesePinyin.charDictionary.Count; index++)
					{
						CharUnit charUnit = ChinesePinyin.charDictionary.CharUnitTable[index];
						streamWriter.Write(charUnit.Char);
						streamWriter.Write(" ");
						streamWriter.Write((int)charUnit.m_strokeNumber);
						streamWriter.Write(" ");
						streamWriter.Write((int)charUnit.m_pinyinCount);
						for (int index2 = 0; index2 < (int)charUnit.m_pinyinCount; index2++)
						{
							PinyinUnit pinYinUnitByIndex = ChinesePinyin.pinyinDictionary.GetPinYinUnitByIndex((int)charUnit.PinyinIndexList[index2]);
							streamWriter.Write(" ");
							streamWriter.Write(pinYinUnitByIndex.Pinyin);
						}
						streamWriter.WriteLine("");
					}
				}
			}
		}

		internal static void TxtToResource()
		{
		}

		private static bool ExistSameElement<T>(T[] array1, T[] array2) where T : IComparable
		{
			int index = 0;
			int index2 = 0;
			bool result;
			while (index < array1.Length && index2 < array2.Length)
			{
				bool flag = array1[index].CompareTo(array2[index2]) < 0;
				if (flag)
				{
					index++;
				}
				else
				{
					bool flag2 = array1[index].CompareTo(array2[index2]) <= 0;
					if (flag2)
					{
						result = true;
						return result;
					}
					index2++;
				}
			}
			result = false;
			return result;
		}

		private static bool IsChineseLetter(char ch)
		{
			int num = Convert.ToInt32("4e00", 16);
			int num2 = Convert.ToInt32("9fff", 16);
			return (int)ch >= num && (int)ch <= num2;
		}

		private void ClearResult()
		{
			this.m_chineseCharacter = '\0';
			this.m_pinyinCount = 0;
			this.m_isPolyphone = false;
			this.m_strokeNumber = 0;
			this.m_unknownChars = "";
			this.m_strPinYin = "";
			this.m_strPolyphone1 = "";
			this.m_strShouZiMu = "";
			this.m_strPolyphone2 = "";
		}

		private void DoConvertToPinyin()
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<char> list = new List<char>();
			List<string> list2 = new List<string>();
			this.ClearResult();
			for (int index = 0; index < this.m_chineseString.Length; index++)
			{
				char index2 = this.m_chineseString[index];
				string str = "";
				char ch = '\0';
				bool flag = false;
				bool flag2 = false;
				list2.Clear();
				bool flag3 = ChinesePinyin.IsChineseLetter(index2);
				if (flag3)
				{
					bool flag4 = index == 0;
					if (flag4)
					{
						this.m_chineseCharacter = index2;
					}
					CharUnit charUnit = ChinesePinyin.charDictionary.GetCharUnit(index2);
					bool flag5 = charUnit != null;
					if (flag5)
					{
						int num = (int)charUnit.m_pinyinCount;
						PinyinUnit pinYinUnitByIndex = ChinesePinyin.pinyinDictionary.GetPinYinUnitByIndex((int)charUnit.PinyinIndexList[0]);
						bool flag6 = pinYinUnitByIndex != null;
						if (flag6)
						{
							str = pinYinUnitByIndex.Pinyin.Substring(0, pinYinUnitByIndex.Pinyin.Length - 1).ToLower();
							ch = pinYinUnitByIndex.Pinyin[0];
							list2.Add(str);
							bool flag7 = index == 0;
							if (flag7)
							{
								this.m_strokeNumber = (short)charUnit.m_strokeNumber;
								this.m_pinyinCount = (short)charUnit.m_pinyinCount;
								this.m_isPolyphone = (this.m_pinyinCount > 1);
								this.m_pinyinList[0] = pinYinUnitByIndex.Pinyin.ToLower();
							}
						}
						for (int index3 = 1; index3 < num; index3++)
						{
							PinyinUnit pinYinUnitByIndex2 = ChinesePinyin.pinyinDictionary.GetPinYinUnitByIndex((int)charUnit.PinyinIndexList[index3]);
							bool flag8 = pinYinUnitByIndex2 != null;
							if (flag8)
							{
								string str2 = pinYinUnitByIndex2.Pinyin.ToLower();
								bool flag9 = index == 0;
								if (flag9)
								{
									this.m_pinyinList[index3] = str2;
								}
								string str3 = str2.Substring(0, str2.Length - 1);
								bool flag10 = !list2.Contains(str3);
								if (flag10)
								{
									list2.Add(str3);
									flag = true;
									bool flag11 = ch != pinYinUnitByIndex2.Pinyin[0];
									if (flag11)
									{
										flag2 = true;
									}
								}
							}
						}
					}
				}
				else
				{
					str = index2.ToString();
					ch = index2;
				}
				bool flag12 = ChinesePinyin.pinyinCustom.ContainsKey(index2);
				if (flag12)
				{
					string str4 = ChinesePinyin.pinyinCustom[index2];
					str = str4.ToLower();
					bool flag13 = str4.Length > 0;
					if (flag13)
					{
						ch = str4[0];
					}
				}
				bool flag14 = str != "";
				if (flag14)
				{
					stringBuilder.Append(str);
					this.m_strShouZiMu += ch.ToString();
					bool flag15 = flag && !list.Contains(index2);
					if (flag15)
					{
						list.Add(index2);
						string str5 = "";
						for (int index4 = 0; index4 < list2.Count; index4++)
						{
							bool flag16 = str5 != "";
							if (flag16)
							{
								str5 += " ";
							}
							str5 += list2[index4];
						}
						this.m_strPolyphone1 = string.Concat(new string[]
						{
							this.m_strPolyphone1,
							index2.ToString(),
							"(",
							str5,
							")"
						});
						bool flag17 = flag2;
						if (flag17)
						{
							this.m_strPolyphone2 = string.Concat(new string[]
							{
								this.m_strPolyphone2,
								index2.ToString(),
								"(",
								str5,
								")"
							});
						}
					}
				}
				else
				{
					this.m_unknownChars += index2.ToString();
					bool flag18 = this.m_unknownFill != "";
					if (flag18)
					{
						stringBuilder.Append(this.m_unknownFill);
						this.m_strShouZiMu += this.m_unknownFill;
					}
					else
					{
						stringBuilder.Append(index2);
						this.m_strShouZiMu += index2.ToString();
					}
				}
			}
			this.m_strPinYin = stringBuilder.ToString();
		}

		public static string GetPinYin(string strChinese)
		{
			return ChinesePinyin.GetPinYin(strChinese, "");
		}

		public static string GetPinYin(string strChinese, string strUnknownFill)
		{
			return new ChinesePinyin(strUnknownFill)
			{
				ChineseString = strChinese
			}.PinYinString;
		}

		public static string GetShouZiMu(string strChinese)
		{
			return ChinesePinyin.GetShouZiMu(strChinese, "");
		}

		public static string GetShouZiMu(string strChinese, string strUnknownFill)
		{
			return new ChinesePinyin(strUnknownFill)
			{
				ChineseString = strChinese
			}.ShouZiMuString;
		}
	}
}
