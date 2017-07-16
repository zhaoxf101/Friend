using System;
using System.Collections.Generic;
using System.Text;

namespace TIM.T_KERNEL.GSM
{
	internal class PDUEncoding
	{
		private string serviceCenterAddress = "00";

		private string protocolDataUnitType = "11";

		private string messageReference = "00";

		private string originatorAddress = "00";

		private string destinationAddress = "00";

		private string protocolIdentifer = "00";

		private string dataCodingScheme = "08";

		private string serviceCenterTimeStamp = "";

		private string validityPeriod = "C4";

		private string userDataLenghth = "00";

		private string userData = "";

		public string ServiceCenterAddress
		{
			get
			{
				return this.ParityChange(this.serviceCenterAddress.Substring(4, 2 * Convert.ToInt32(this.serviceCenterAddress.Substring(0, 2), 16) - 2)).TrimEnd(new char[]
				{
					'F',
					'f'
				});
			}
			set
			{
				bool flag = value == null || value.Length == 0;
				if (flag)
				{
					this.serviceCenterAddress = "00";
				}
				else
				{
					value = value.TrimStart(new char[]
					{
						'+'
					});
					bool flag2 = value.Substring(0, 2) != "86";
					if (flag2)
					{
						value = "86" + value;
					}
					value = "91" + this.ParityChange(value);
					this.serviceCenterAddress = (value.Length / 2).ToString("X2") + value;
				}
			}
		}

		public string ProtocolDataUnitType
		{
			get
			{
				return this.protocolDataUnitType;
			}
			set
			{
				this.protocolDataUnitType = value;
			}
		}

		public string MessageReference
		{
			get
			{
				return "00";
			}
			set
			{
				this.messageReference = value;
			}
		}

		public string OriginatorAddress
		{
			get
			{
				int length = Convert.ToInt32(this.originatorAddress.Substring(0, 2), 16);
				string str = string.Empty;
				bool flag = length % 2 == 1;
				if (flag)
				{
					length++;
				}
				return this.ParityChange(this.originatorAddress.Substring(4, length)).TrimEnd(new char[]
				{
					'F',
					'f'
				});
			}
		}

		public string DestinationAddress
		{
			set
			{
				bool flag = value == null || value.Length == 0;
				if (flag)
				{
					throw new ArgumentNullException("目的号码不允许为空");
				}
				value = value.TrimStart(new char[]
				{
					'+'
				});
				bool flag2 = value.Substring(0, 2) == "86";
				if (flag2)
				{
					value = value.TrimStart(new char[]
					{
						'8',
						'6'
					});
				}
				int length = value.Length;
				value = this.ParityChange(value);
				this.destinationAddress = length.ToString("X2") + "A1" + value;
			}
		}

		public string ProtocolIdentifer
		{
			get
			{
				return this.protocolIdentifer;
			}
			set
			{
				this.protocolIdentifer = value;
			}
		}

		public string DataCodingScheme
		{
			get
			{
				return this.dataCodingScheme;
			}
			set
			{
				this.dataCodingScheme = value;
			}
		}

		public string ServiceCenterTimeStamp
		{
			get
			{
				return "20" + this.ParityChange(this.serviceCenterTimeStamp).Substring(0, 12);
			}
		}

		public string ValidityPeriod
		{
			get
			{
				return "C4";
			}
			set
			{
				this.validityPeriod = value;
			}
		}

		public string UserDataLenghth
		{
			get
			{
				return (this.userData.Length / 2).ToString("X2");
			}
			set
			{
				this.userDataLenghth = value;
			}
		}

		public string UserData
		{
			get
			{
				string str = string.Empty;
				bool flag = this.dataCodingScheme.Substring(1, 1) == "8";
				if (flag)
				{
					int num = Convert.ToInt32(this.userDataLenghth, 16) * 2;
					for (int startIndex = 0; startIndex < num; startIndex += 4)
					{
						int num2 = (int)Convert.ToInt16(this.userData.Substring(startIndex, 4), 16);
						str += ((char)num2).ToString();
					}
				}
				else
				{
					str = this.PDU7bitContentDecoder(this.userData);
				}
				return str;
			}
			set
			{
				bool flag = this.dataCodingScheme.Substring(1, 1) == "8";
				if (flag)
				{
					this.userData = string.Empty;
					byte[] bytes = Encoding.BigEndianUnicode.GetBytes(value);
					for (int startIndex = 0; startIndex < bytes.Length; startIndex++)
					{
						this.userData += BitConverter.ToString(bytes, startIndex, 1);
					}
					this.userDataLenghth = (this.userData.Length / 2).ToString("X2");
				}
				else
				{
					this.userData = string.Empty;
					this.userDataLenghth = value.Length.ToString("X2");
					byte[] bytes2 = Encoding.ASCII.GetBytes(value);
					string str = string.Empty;
					for (int length = value.Length; length > 0; length--)
					{
						string str2 = Convert.ToString(bytes2[length - 1], 2);
						while (str2.Length < 7)
						{
							str2 = "0" + str2;
						}
						str += str2;
					}
					for (int length2 = str.Length; length2 > 0; length2 -= 8)
					{
						bool flag2 = length2 > 8;
						if (flag2)
						{
							string str3 = this.userData;
							string str4 = Convert.ToInt32(str.Substring(length2 - 8, 8), 2).ToString("X2");
							string str5 = str3 + str4;
							this.userData = str5;
						}
						else
						{
							string str6 = this.userData;
							string str7 = Convert.ToInt32(str.Substring(0, length2), 2).ToString("X2");
							string str8 = str6 + str7;
							this.userData = str8;
						}
					}
				}
			}
		}

		private string ParityChange(string str)
		{
			string str2 = string.Empty;
			bool flag = str.Length % 2 != 0;
			if (flag)
			{
				str += "F";
			}
			for (int index = 0; index < str.Length; index += 2)
			{
				str2 = str2 + str[index + 1].ToString() + str[index].ToString();
			}
			return str2;
		}

		private bool IsASCII(string str)
		{
			return str.Length == Encoding.UTF8.GetBytes(str).Length;
		}

		private byte[] Hex2Bin(string hex)
		{
			byte[] numArray = new byte[hex.Length / 2];
			for (int index = 0; index < hex.Length; index += 2)
			{
				byte[] numArray2 = numArray;
				int index2 = index / 2;
				string str = hex[index].ToString();
				string str2 = hex[index + 1].ToString();
				int num = (int)Convert.ToByte(str + str2, 16);
				numArray2[index2] = (byte)num;
			}
			return numArray;
		}

		private string Bin2BinStringof8Bit(byte[] bytes)
		{
			string str = string.Empty;
			for (int i = 0; i < bytes.Length; i++)
			{
				byte num = bytes[i];
				string str2 = Convert.ToString(num, 2);
				while (str2.Length < 8)
				{
					str2 = "0" + str2;
				}
				str += str2;
			}
			return str;
		}

		private string BinStringof8Bit2AsciiwithReverse(string bin)
		{
			byte[] bytes = new byte[bin.Length / 7];
			string str2 = bin.Remove(0, bin.Length % 7);
			for (int startIndex = 0; startIndex < str2.Length; startIndex += 7)
			{
				bytes[startIndex / 7] = Convert.ToByte(str2.Substring(startIndex, 7), 2);
			}
			Array.Reverse(bytes);
			return Encoding.ASCII.GetString(bytes);
		}

		private string PDU7bitContentDecoder(string userData)
		{
			string str = string.Empty;
			byte[] bytes = this.Hex2Bin(userData);
			Array.Reverse(bytes);
			return this.BinStringof8Bit2AsciiwithReverse(this.Bin2BinStringof8Bit(bytes));
		}

		private List<CodedMessage> PDUUSC2Encoder(string phone, string Text)
		{
			this.dataCodingScheme = "08";
			this.DestinationAddress = phone;
			List<CodedMessage> list = new List<CodedMessage>();
			bool flag = Text.Length > 70;
			if (flag)
			{
				this.ProtocolDataUnitType = "51";
				int num = Text.Length / 67;
				bool flag2 = Text.Length % 67 != 0;
				if (flag2)
				{
					num++;
				}
				for (int index = 0; index < num; index++)
				{
					bool flag3 = index < num - 1;
					if (flag3)
					{
						this.UserData = Text.Substring(index * 67, 67);
						list.Add(new CodedMessage(string.Concat(new string[]
						{
							this.serviceCenterAddress,
							this.protocolDataUnitType,
							this.messageReference,
							this.destinationAddress,
							this.protocolIdentifer,
							this.dataCodingScheme,
							this.validityPeriod,
							(this.userData.Length / 2 + 6).ToString("X2"),
							"05000339",
							num.ToString("X2"),
							(index + 1).ToString("X2"),
							this.userData
						})));
					}
					else
					{
						this.UserData = Text.Substring(index * 67);
						bool flag4 = this.userData != null || this.userData.Length != 0;
						if (flag4)
						{
							list.Add(new CodedMessage(string.Concat(new string[]
							{
								this.serviceCenterAddress,
								this.protocolDataUnitType,
								this.messageReference,
								this.destinationAddress,
								this.protocolIdentifer,
								this.dataCodingScheme,
								this.validityPeriod,
								(this.userData.Length / 2 + 6).ToString("X2"),
								"05000339",
								num.ToString("X2"),
								(index + 1).ToString("X2"),
								this.userData
							})));
						}
					}
				}
			}
			else
			{
				this.ProtocolDataUnitType = "11";
				this.UserData = Text;
				list.Add(new CodedMessage(string.Concat(new string[]
				{
					this.serviceCenterAddress,
					this.protocolDataUnitType,
					this.messageReference,
					this.destinationAddress,
					this.protocolIdentifer,
					this.dataCodingScheme,
					this.validityPeriod,
					this.userDataLenghth,
					this.userData
				})));
			}
			return list;
		}

		private List<CodedMessage> PDU7BitEncoder(string phone, string Text)
		{
			this.dataCodingScheme = "00";
			this.DestinationAddress = phone;
			List<CodedMessage> list = new List<CodedMessage>();
			bool flag = Text.Length > 160;
			List<CodedMessage> result;
			if (flag)
			{
				this.ProtocolDataUnitType = "51";
				int num = Text.Length / 153;
				bool flag2 = Text.Length % 153 != 0;
				if (flag2)
				{
					num++;
				}
				for (int index = 0; index < num; index++)
				{
					bool flag3 = index < num - 1;
					if (flag3)
					{
						this.UserData = Text.Substring(index * 153 + 1, 152);
						List<CodedMessage> list2 = list;
						string[] expr_A1 = new string[13];
						expr_A1[0] = this.serviceCenterAddress;
						expr_A1[1] = this.protocolDataUnitType;
						expr_A1[2] = this.messageReference;
						expr_A1[3] = this.destinationAddress;
						expr_A1[4] = this.protocolIdentifer;
						expr_A1[5] = this.dataCodingScheme;
						expr_A1[6] = this.validityPeriod;
						string[] strArray = expr_A1;
						string[] strArray2 = strArray;
						int index2 = 7;
						string str = 160.ToString("X2");
						strArray2[index2] = str;
						strArray[8] = "05000339";
						strArray[9] = num.ToString("X2");
						string[] strArray3 = strArray;
						int index3 = 10;
						string str2 = (index + 1).ToString("X2");
						strArray3[index3] = str2;
						string[] strArray4 = strArray;
						int index4 = 11;
						string str3 = ((int)new ASCIIEncoding().GetBytes(Text.Substring(index * 153, 1))[0] << 1).ToString("X2");
						strArray4[index4] = str3;
						strArray[12] = this.userData;
						CodedMessage codedMessage = new CodedMessage(string.Concat(strArray));
						list2.Add(codedMessage);
					}
					else
					{
						this.UserData = Text.Substring(index * 153 + 1);
						int length = Text.Substring(index * 153).Length;
						bool flag4 = this.userData != null || this.userData.Length != 0;
						if (flag4)
						{
							List<CodedMessage> list3 = list;
							string[] expr_1FE = new string[13];
							expr_1FE[0] = this.serviceCenterAddress;
							expr_1FE[1] = this.protocolDataUnitType;
							expr_1FE[2] = this.messageReference;
							expr_1FE[3] = this.destinationAddress;
							expr_1FE[4] = this.protocolIdentifer;
							expr_1FE[5] = this.dataCodingScheme;
							expr_1FE[6] = this.validityPeriod;
							string[] strArray5 = expr_1FE;
							string[] strArray6 = strArray5;
							int index5 = 7;
							string str4 = (length + 7).ToString("X2");
							strArray6[index5] = str4;
							strArray5[8] = "05000339";
							strArray5[9] = num.ToString("X2");
							string[] strArray7 = strArray5;
							int index6 = 10;
							string str5 = (index + 1).ToString("X2");
							strArray7[index6] = str5;
							string[] strArray8 = strArray5;
							int index7 = 11;
							string str6 = ((int)new ASCIIEncoding().GetBytes(Text.Substring(index * 153, 1))[0] << 1).ToString("X2");
							strArray8[index7] = str6;
							strArray5[12] = this.userData;
							CodedMessage codedMessage2 = new CodedMessage(string.Concat(strArray5));
							list3.Add(codedMessage2);
						}
					}
				}
				result = list;
			}
			else
			{
				this.UserData = Text;
				list.Add(new CodedMessage(string.Concat(new string[]
				{
					this.serviceCenterAddress,
					this.protocolDataUnitType,
					this.messageReference,
					this.destinationAddress,
					this.protocolIdentifer,
					this.dataCodingScheme,
					this.validityPeriod,
					this.userDataLenghth,
					this.userData
				})));
				result = list;
			}
			return result;
		}

		public List<CodedMessage> PDUEncoder(string phone, string text)
		{
			bool flag = this.IsASCII(text);
			List<CodedMessage> result;
			if (flag)
			{
				result = this.PDU7BitEncoder(phone, text);
			}
			else
			{
				result = this.PDUUSC2Encoder(phone, text);
			}
			return result;
		}

		public DecodedMessage PDUDecoder(string strPDU)
		{
			int num = Convert.ToInt32(strPDU.Substring(0, 2), 16) * 2 + 2;
			this.serviceCenterAddress = strPDU.Substring(0, num);
			this.protocolDataUnitType = strPDU.Substring(num, 2);
			int num2 = Convert.ToInt32(strPDU.Substring(num + 2, 2), 16);
			bool flag = num2 % 2 == 1;
			if (flag)
			{
				num2++;
			}
			int length = num2 + 4;
			this.originatorAddress = strPDU.Substring(num + 2, length);
			this.dataCodingScheme = strPDU.Substring(num + length + 4, 2);
			this.serviceCenterTimeStamp = strPDU.Substring(num + length + 6, 14);
			this.userDataLenghth = strPDU.Substring(num + length + 20, 2);
			int num3 = Convert.ToInt32(this.userDataLenghth, 16) * 2;
			bool flag2 = (Convert.ToInt32(this.protocolDataUnitType, 16) & 64) != 0;
			DecodedMessage result;
			if (flag2)
			{
				bool flag3 = this.dataCodingScheme.Substring(1, 1) == "8";
				if (flag3)
				{
					this.userDataLenghth = ((int)(Convert.ToInt16(strPDU.Substring(num + length + 20, 2), 16) - 6)).ToString("X2");
					this.userData = strPDU.Substring(num + length + 22 + 12);
					result = new DecodedMessage(strPDU.Substring(num + length + 22 + 8, 4) + strPDU.Substring(num + length + 22 + 6, 2), this.ServiceCenterAddress, string.Concat(new string[]
					{
						this.ServiceCenterTimeStamp.Substring(0, 4),
						"-",
						this.ServiceCenterTimeStamp.Substring(4, 2),
						"-",
						this.ServiceCenterTimeStamp.Substring(6, 2),
						" ",
						this.ServiceCenterTimeStamp.Substring(8, 2),
						":",
						this.ServiceCenterTimeStamp.Substring(10, 2),
						":",
						this.ServiceCenterTimeStamp.Substring(12, 2)
					}), this.OriginatorAddress, this.UserData);
				}
				else
				{
					this.userData = strPDU.Substring(num + length + 22 + 12 + 2);
					char ch = (char)((uint)Convert.ToByte(strPDU.Substring(num + length + 22 + 12, 2), 16) >> 1);
					result = new DecodedMessage(strPDU.Substring(num + length + 22 + 8, 4) + strPDU.Substring(num + length + 22 + 6, 2), this.ServiceCenterAddress, string.Concat(new string[]
					{
						this.ServiceCenterTimeStamp.Substring(0, 4),
						"-",
						this.ServiceCenterTimeStamp.Substring(4, 2),
						"-",
						this.ServiceCenterTimeStamp.Substring(6, 2),
						" ",
						this.ServiceCenterTimeStamp.Substring(8, 2),
						":",
						this.ServiceCenterTimeStamp.Substring(10, 2),
						":",
						this.ServiceCenterTimeStamp.Substring(12, 2)
					}), this.OriginatorAddress, ch.ToString() + this.UserData);
				}
			}
			else
			{
				this.userData = strPDU.Substring(num + length + 22);
				result = new DecodedMessage("010100", this.ServiceCenterAddress, string.Concat(new string[]
				{
					this.ServiceCenterTimeStamp.Substring(0, 4),
					"-",
					this.ServiceCenterTimeStamp.Substring(4, 2),
					"-",
					this.ServiceCenterTimeStamp.Substring(6, 2),
					" ",
					this.ServiceCenterTimeStamp.Substring(8, 2),
					":",
					this.ServiceCenterTimeStamp.Substring(10, 2),
					":",
					this.ServiceCenterTimeStamp.Substring(12, 2)
				}), this.OriginatorAddress, this.UserData);
			}
			return result;
		}
	}
}
