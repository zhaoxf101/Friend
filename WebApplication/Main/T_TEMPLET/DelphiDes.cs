using System;
using System.Text;

internal class DelphiDes
{
	private static readonly byte[] BitIP = new byte[]
	{
		57,
		49,
		41,
		33,
		25,
		17,
		9,
		1,
		59,
		51,
		43,
		35,
		27,
		19,
		11,
		3,
		61,
		53,
		45,
		37,
		29,
		21,
		13,
		5,
		63,
		55,
		47,
		39,
		31,
		23,
		15,
		7,
		56,
		48,
		40,
		32,
		24,
		16,
		8,
		0,
		58,
		50,
		42,
		34,
		26,
		18,
		10,
		2,
		60,
		52,
		44,
		36,
		28,
		20,
		12,
		4,
		62,
		54,
		46,
		38,
		30,
		22,
		14,
		6
	};

	private static readonly byte[] BitCP = new byte[]
	{
		39,
		7,
		47,
		15,
		55,
		23,
		63,
		31,
		38,
		6,
		46,
		14,
		54,
		22,
		62,
		30,
		37,
		5,
		45,
		13,
		53,
		21,
		61,
		29,
		36,
		4,
		44,
		12,
		52,
		20,
		60,
		28,
		35,
		3,
		43,
		11,
		51,
		19,
		59,
		27,
		34,
		2,
		42,
		10,
		50,
		18,
		58,
		26,
		33,
		1,
		41,
		9,
		49,
		17,
		57,
		25,
		32,
		0,
		40,
		8,
		48,
		16,
		56,
		24
	};

	private static readonly int[] BitExp = new int[]
	{
		31,
		0,
		1,
		2,
		3,
		4,
		3,
		4,
		5,
		6,
		7,
		8,
		7,
		8,
		9,
		10,
		11,
		12,
		11,
		12,
		13,
		14,
		15,
		16,
		15,
		16,
		17,
		18,
		19,
		20,
		19,
		20,
		21,
		22,
		23,
		24,
		23,
		24,
		25,
		26,
		27,
		28,
		27,
		28,
		29,
		30,
		31,
		0
	};

	private static readonly byte[] BitPM = new byte[]
	{
		15,
		6,
		19,
		20,
		28,
		11,
		27,
		16,
		0,
		14,
		22,
		25,
		4,
		17,
		30,
		9,
		1,
		7,
		23,
		13,
		31,
		26,
		2,
		8,
		18,
		12,
		29,
		5,
		21,
		10,
		3,
		24
	};

	private static readonly byte[,] sBox = new byte[,]
	{
		{
			14,
			4,
			13,
			1,
			2,
			15,
			11,
			8,
			3,
			10,
			6,
			12,
			5,
			9,
			0,
			7,
			0,
			15,
			7,
			4,
			14,
			2,
			13,
			1,
			10,
			6,
			12,
			11,
			9,
			5,
			3,
			8,
			4,
			1,
			14,
			8,
			13,
			6,
			2,
			11,
			15,
			12,
			9,
			7,
			3,
			10,
			5,
			0,
			15,
			12,
			8,
			2,
			4,
			9,
			1,
			7,
			5,
			11,
			3,
			14,
			10,
			0,
			6,
			13
		},
		{
			15,
			1,
			8,
			14,
			6,
			11,
			3,
			4,
			9,
			7,
			2,
			13,
			12,
			0,
			5,
			10,
			3,
			13,
			4,
			7,
			15,
			2,
			8,
			14,
			12,
			0,
			1,
			10,
			6,
			9,
			11,
			5,
			0,
			14,
			7,
			11,
			10,
			4,
			13,
			1,
			5,
			8,
			12,
			6,
			9,
			3,
			2,
			15,
			13,
			8,
			10,
			1,
			3,
			15,
			4,
			2,
			11,
			6,
			7,
			12,
			0,
			5,
			14,
			9
		},
		{
			10,
			0,
			9,
			14,
			6,
			3,
			15,
			5,
			1,
			13,
			12,
			7,
			11,
			4,
			2,
			8,
			13,
			7,
			0,
			9,
			3,
			4,
			6,
			10,
			2,
			8,
			5,
			14,
			12,
			11,
			15,
			1,
			13,
			6,
			4,
			9,
			8,
			15,
			3,
			0,
			11,
			1,
			2,
			12,
			5,
			10,
			14,
			7,
			1,
			10,
			13,
			0,
			6,
			9,
			8,
			7,
			4,
			15,
			14,
			3,
			11,
			5,
			2,
			12
		},
		{
			7,
			13,
			14,
			3,
			0,
			6,
			9,
			10,
			1,
			2,
			8,
			5,
			11,
			12,
			4,
			15,
			13,
			8,
			11,
			5,
			6,
			15,
			0,
			3,
			4,
			7,
			2,
			12,
			1,
			10,
			14,
			9,
			10,
			6,
			9,
			0,
			12,
			11,
			7,
			13,
			15,
			1,
			3,
			14,
			5,
			2,
			8,
			4,
			3,
			15,
			0,
			6,
			10,
			1,
			13,
			8,
			9,
			4,
			5,
			11,
			12,
			7,
			2,
			14
		},
		{
			2,
			12,
			4,
			1,
			7,
			10,
			11,
			6,
			8,
			5,
			3,
			15,
			13,
			0,
			14,
			9,
			14,
			11,
			2,
			12,
			4,
			7,
			13,
			1,
			5,
			0,
			15,
			10,
			3,
			9,
			8,
			6,
			4,
			2,
			1,
			11,
			10,
			13,
			7,
			8,
			15,
			9,
			12,
			5,
			6,
			3,
			0,
			14,
			11,
			8,
			12,
			7,
			1,
			14,
			2,
			13,
			6,
			15,
			0,
			9,
			10,
			4,
			5,
			3
		},
		{
			12,
			1,
			10,
			15,
			9,
			2,
			6,
			8,
			0,
			13,
			3,
			4,
			14,
			7,
			5,
			11,
			10,
			15,
			4,
			2,
			7,
			12,
			9,
			5,
			6,
			1,
			13,
			14,
			0,
			11,
			3,
			8,
			9,
			14,
			15,
			5,
			2,
			8,
			12,
			3,
			7,
			0,
			4,
			10,
			1,
			13,
			11,
			6,
			4,
			3,
			2,
			12,
			9,
			5,
			15,
			10,
			11,
			14,
			1,
			7,
			6,
			0,
			8,
			13
		},
		{
			4,
			11,
			2,
			14,
			15,
			0,
			8,
			13,
			3,
			12,
			9,
			7,
			5,
			10,
			6,
			1,
			13,
			0,
			11,
			7,
			4,
			9,
			1,
			10,
			14,
			3,
			5,
			12,
			2,
			15,
			8,
			6,
			1,
			4,
			11,
			13,
			12,
			3,
			7,
			14,
			10,
			15,
			6,
			8,
			0,
			5,
			9,
			2,
			6,
			11,
			13,
			8,
			1,
			4,
			10,
			7,
			9,
			5,
			0,
			15,
			14,
			2,
			3,
			12
		},
		{
			13,
			2,
			8,
			4,
			6,
			15,
			11,
			1,
			10,
			9,
			3,
			14,
			5,
			0,
			12,
			7,
			1,
			15,
			13,
			8,
			10,
			3,
			7,
			4,
			12,
			5,
			6,
			11,
			0,
			14,
			9,
			2,
			7,
			11,
			4,
			1,
			9,
			12,
			14,
			2,
			0,
			6,
			10,
			13,
			15,
			3,
			5,
			8,
			2,
			1,
			14,
			7,
			4,
			10,
			8,
			13,
			15,
			12,
			9,
			0,
			3,
			5,
			6,
			11
		}
	};

	private static readonly byte[] BitPMC1 = new byte[]
	{
		56,
		48,
		40,
		32,
		24,
		16,
		8,
		0,
		57,
		49,
		41,
		33,
		25,
		17,
		9,
		1,
		58,
		50,
		42,
		34,
		26,
		18,
		10,
		2,
		59,
		51,
		43,
		35,
		62,
		54,
		46,
		38,
		30,
		22,
		14,
		6,
		61,
		53,
		45,
		37,
		29,
		21,
		13,
		5,
		60,
		52,
		44,
		36,
		28,
		20,
		12,
		4,
		27,
		19,
		11,
		3
	};

	private static readonly byte[] BitPMC2 = new byte[]
	{
		13,
		16,
		10,
		23,
		0,
		4,
		2,
		27,
		14,
		5,
		20,
		9,
		22,
		18,
		11,
		3,
		25,
		7,
		15,
		6,
		26,
		19,
		12,
		1,
		40,
		51,
		30,
		36,
		46,
		54,
		29,
		39,
		50,
		44,
		32,
		47,
		43,
		48,
		38,
		55,
		33,
		52,
		45,
		41,
		49,
		35,
		28,
		31
	};

	private byte[][] SubKey;

	private static readonly byte[] bitDisplace = new byte[]
	{
		1,
		1,
		2,
		2,
		2,
		2,
		2,
		2,
		1,
		2,
		2,
		2,
		2,
		2,
		2,
		1
	};

	public DelphiDes()
	{
		this.SubKey = new byte[16][];
		for (int i = 0; i < this.SubKey.Length; i++)
		{
			this.SubKey[i] = new byte[6];
		}
	}

	private void initPermutation(byte[] inData)
	{
		byte[] newData = new byte[8];
		for (int i = 0; i < 64; i++)
		{
			bool flag = ((int)inData[DelphiDes.BitIP[i] >> 3] & 1 << (int)(7 - (DelphiDes.BitIP[i] & 7))) != 0;
			if (flag)
			{
				byte[] expr_3A_cp_0 = newData;
				int expr_3A_cp_1 = i >> 3;
				expr_3A_cp_0[expr_3A_cp_1] |= (byte)(1 << 7 - (i & 7));
			}
		}
		Array.Copy(newData, inData, 8);
	}

	private void conversePermutation(byte[] inData)
	{
		byte[] newData = new byte[8];
		for (int i = 0; i < 64; i++)
		{
			bool flag = ((int)inData[DelphiDes.BitCP[i] >> 3] & 1 << (int)(7 - (DelphiDes.BitCP[i] & 7))) != 0;
			if (flag)
			{
				byte[] expr_3A_cp_0 = newData;
				int expr_3A_cp_1 = i >> 3;
				expr_3A_cp_0[expr_3A_cp_1] |= (byte)(1 << 7 - (i & 7));
			}
		}
		Array.Copy(newData, inData, 8);
	}

	private void expand(byte[] inData, byte[] outData)
	{
		Array.Clear(outData, 0, 6);
		for (int i = 0; i < 48; i++)
		{
			bool flag = ((int)inData[DelphiDes.BitExp[i] >> 3] & 1 << 7 - (DelphiDes.BitExp[i] & 7)) != 0;
			if (flag)
			{
				int expr_3C_cp_1 = i >> 3;
				outData[expr_3C_cp_1] |= (byte)(1 << 7 - (i & 7));
			}
		}
	}

	private void permutation(byte[] inData)
	{
		byte[] newData = new byte[4];
		for (int i = 0; i < 32; i++)
		{
			bool flag = ((int)inData[DelphiDes.BitPM[i] >> 3] & 1 << (int)(7 - (DelphiDes.BitPM[i] & 7))) != 0;
			if (flag)
			{
				byte[] expr_3A_cp_0 = newData;
				int expr_3A_cp_1 = i >> 3;
				expr_3A_cp_0[expr_3A_cp_1] |= (byte)(1 << 7 - (i & 7));
			}
		}
		Array.Copy(newData, inData, 4);
	}

	private byte si(byte s, byte inByte)
	{
		int c = (int)(inByte & 32) | (inByte & 30) >> 1 | (int)(inByte & 1) << 4;
		return (byte)(DelphiDes.sBox[(int)s, c] & 15);
	}

	private void permutationChoose1(byte[] inData, byte[] outData)
	{
		Array.Clear(outData, 0, 7);
		for (int i = 0; i < 56; i++)
		{
			bool flag = ((int)inData[DelphiDes.BitPMC1[i] >> 3] & 1 << (int)(7 - (DelphiDes.BitPMC1[i] & 7))) != 0;
			if (flag)
			{
				int expr_3C_cp_1 = i >> 3;
				outData[expr_3C_cp_1] |= (byte)(1 << 7 - (i & 7));
			}
		}
	}

	private void permutationChoose2(byte[] inData, byte[] outData)
	{
		Array.Clear(outData, 0, 6);
		for (int i = 0; i < 48; i++)
		{
			bool flag = ((int)inData[DelphiDes.BitPMC2[i] >> 3] & 1 << (int)(7 - (DelphiDes.BitPMC2[i] & 7))) != 0;
			if (flag)
			{
				int expr_3C_cp_1 = i >> 3;
				outData[expr_3C_cp_1] |= (byte)(1 << 7 - (i & 7));
			}
		}
	}

	private void cycleMove(byte[] inData, byte bitMove)
	{
		for (int i = 0; i < (int)bitMove; i++)
		{
			inData[0] = (byte)((int)inData[0] << 1 | inData[1] >> 7);
			inData[1] = (byte)((int)inData[1] << 1 | inData[2] >> 7);
			inData[2] = (byte)((int)inData[2] << 1 | inData[3] >> 7);
			inData[3] = (byte)((int)inData[3] << 1 | (inData[0] & 16) >> 4);
			inData[0] = (byte)(inData[0] & 15);
		}
	}

	private void makeKey(byte[] inKey, byte[][] outKey)
	{
		byte[] outData56 = new byte[7];
		byte[] key28l = new byte[4];
		byte[] key28r = new byte[4];
		byte[] key56o = new byte[7];
		this.permutationChoose1(inKey, outData56);
		key28l[0] = (byte)(outData56[0] >> 4);
		key28l[1] = (byte)((int)outData56[0] << 4 | outData56[1] >> 4);
		key28l[2] = (byte)((int)outData56[1] << 4 | outData56[2] >> 4);
		key28l[3] = (byte)((int)outData56[2] << 4 | outData56[3] >> 4);
		key28r[0] = (byte)(outData56[3] & 15);
		key28r[1] = outData56[4];
		key28r[2] = outData56[5];
		key28r[3] = outData56[6];
		for (int i = 0; i < 16; i++)
		{
			this.cycleMove(key28l, DelphiDes.bitDisplace[i]);
			this.cycleMove(key28r, DelphiDes.bitDisplace[i]);
			key56o[0] = (byte)((int)key28l[0] << 4 | key28l[1] >> 4);
			key56o[1] = (byte)((int)key28l[1] << 4 | key28l[2] >> 4);
			key56o[2] = (byte)((int)key28l[2] << 4 | key28l[3] >> 4);
			key56o[3] = (byte)((int)key28l[3] << 4 | (int)key28r[0]);
			key56o[4] = key28r[1];
			key56o[5] = key28r[2];
			key56o[6] = key28r[3];
			this.permutationChoose2(key56o, outKey[i]);
		}
	}

	private void encry(byte[] inData, byte[] subKey, byte[] outData)
	{
		byte[] outBuf = new byte[6];
		byte[] buf = new byte[8];
		this.expand(inData, outBuf);
		for (int i = 0; i < 6; i++)
		{
			outBuf[i] ^= subKey[i];
		}
		buf[0] = (byte)(outBuf[0] >> 2);
		buf[1] = (byte)((int)(outBuf[0] & 3) << 4 | outBuf[1] >> 4);
		buf[2] = (byte)((int)(outBuf[1] & 15) << 2 | outBuf[2] >> 6);
		buf[3] = (byte)(outBuf[2] & 63);
		buf[4] = (byte)(outBuf[3] >> 2);
		buf[5] = (byte)((int)(outBuf[3] & 3) << 4 | outBuf[4] >> 4);
		buf[6] = (byte)((int)(outBuf[4] & 15) << 2 | outBuf[5] >> 6);
		buf[7] = (byte)(outBuf[5] & 63);
		for (int j = 0; j < 8; j++)
		{
			buf[j] = this.si((byte)j, buf[j]);
		}
		for (int k = 0; k < 4; k++)
		{
			outBuf[k] = (byte)((int)buf[k * 2] << 4 | (int)buf[k * 2 + 1]);
		}
		this.permutation(outBuf);
		for (int l = 0; l < 4; l++)
		{
			outData[l] = outBuf[l];
		}
	}

	private void desData(TDesMode desMode, byte[] inData, byte[] outData)
	{
		byte[] temp = new byte[4];
		byte[] buf = new byte[4];
		for (int i = 0; i < 8; i++)
		{
			outData[i] = inData[i];
		}
		this.initPermutation(outData);
		bool flag = desMode == TDesMode.dmEncry;
		if (flag)
		{
			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					temp[j] = outData[j];
				}
				for (int j = 0; j < 4; j++)
				{
					outData[j] = outData[j + 4];
				}
				this.encry(outData, this.SubKey[i], buf);
				for (int j = 0; j < 4; j++)
				{
					outData[j + 4] = (byte)(temp[j] ^ buf[j]);
				}
			}
			for (int j = 0; j < 4; j++)
			{
				temp[j] = outData[j + 4];
			}
			for (int j = 0; j < 4; j++)
			{
				outData[j + 4] = outData[j];
			}
			for (int j = 0; j < 4; j++)
			{
				outData[j] = temp[j];
			}
		}
		else
		{
			bool flag2 = desMode == TDesMode.dmDecry;
			if (flag2)
			{
				for (int i = 15; i >= 0; i--)
				{
					for (int j = 0; j < 4; j++)
					{
						temp[j] = outData[j];
					}
					for (int j = 0; j < 4; j++)
					{
						outData[j] = outData[j + 4];
					}
					this.encry(outData, this.SubKey[i], buf);
					for (int j = 0; j < 4; j++)
					{
						outData[j + 4] = (byte)(temp[j] ^ buf[j]);
					}
				}
				for (int j = 0; j < 4; j++)
				{
					temp[j] = outData[j + 4];
				}
				for (int j = 0; j < 4; j++)
				{
					outData[j + 4] = outData[j];
				}
				for (int j = 0; j < 4; j++)
				{
					outData[j] = temp[j];
				}
			}
		}
		this.conversePermutation(outData);
	}

	public string EncryStr(string Str, string Key)
	{
		byte[] StrByte = new byte[8];
		byte[] OutByte = new byte[8];
		byte[] KeyByte = new byte[8];
		byte[] sByte = Encoding.Default.GetBytes(Str);
		int sLen = sByte.Length;
		bool flag = sLen > 0 && sByte[sLen - 1] == 0;
		if (flag)
		{
			throw new ArgumentException("Error: the last char is NULL char.", "Str");
		}
		bool flag2 = Key.Length < 8;
		if (flag2)
		{
			while (Key.Length < 8)
			{
				Key += "\0";
			}
		}
		while (sLen % 8 != 0)
		{
			Str += "\0";
			sByte = Encoding.Default.GetBytes(Str);
			sLen = sByte.Length;
		}
		for (int i = 0; i < 8; i++)
		{
			KeyByte[i] = (byte)Key[i];
		}
		this.makeKey(KeyByte, this.SubKey);
		StringBuilder StrResult = new StringBuilder();
		for (int j = 0; j < sLen / 8; j++)
		{
			for (int k = 0; k < 8; k++)
			{
				StrByte[k] = sByte[j * 8 + k];
			}
			this.desData(TDesMode.dmEncry, StrByte, OutByte);
			for (int l = 0; l < 8; l++)
			{
				StrResult.Append((char)OutByte[l]);
			}
		}
		return StrResult.ToString();
	}

	public string DecryStr(string Str, string Key)
	{
		byte[] StrByte = new byte[8];
		byte[] OutByte = new byte[8];
		byte[] KeyByte = new byte[8];
		byte[] StrRst = new byte[Str.Length];
		bool flag = Key.Length < 8;
		if (flag)
		{
			while (Key.Length < 8)
			{
				Key += "\0";
			}
		}
		for (int i = 0; i < 8; i++)
		{
			KeyByte[i] = (byte)Key[i];
		}
		this.makeKey(KeyByte, this.SubKey);
		StringBuilder StrResult = new StringBuilder();
		int ii = 0;
		for (int j = 0; j < Str.Length / 8; j++)
		{
			for (int k = 0; k < 8; k++)
			{
				StrByte[k] = (byte)Str[j * 8 + k];
			}
			this.desData(TDesMode.dmDecry, StrByte, OutByte);
			for (int l = 0; l < 8; l++)
			{
				StrRst[ii] = OutByte[l];
				ii++;
			}
		}
		string StrR = Encoding.Default.GetString(StrRst);
		int ioo = StrR.Length;
		return StrR.TrimEnd(new char[1]);
	}

	public string EncryStrHex(string Str, string Key)
	{
		string TempResult = this.EncryStr(Str, Key);
		StringBuilder StrResult = new StringBuilder();
		string text = TempResult;
		for (int i = 0; i < text.Length; i++)
		{
			char c = text[i];
			StrResult.AppendFormat("{0:X2}", (int)c);
		}
		return StrResult.ToString();
	}

	public string DecryStrHex(string StrHex, string Key)
	{
		bool flag = StrHex.Length % 2 != 0;
		if (flag)
		{
			throw new ArgumentException("Error: string length must be even.", "StrHex");
		}
		StringBuilder Str = new StringBuilder();
		for (int i = 0; i < StrHex.Length; i += 2)
		{
			Str.Append((char)(Uri.FromHex(StrHex[i]) * 16 + Uri.FromHex(StrHex[i + 1])));
		}
		return this.DecryStr(Str.ToString(), Key);
	}
}
