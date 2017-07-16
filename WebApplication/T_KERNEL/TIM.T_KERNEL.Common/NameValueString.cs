using System;
using System.Collections.Generic;
using System.Text;

namespace TIM.T_KERNEL.Common
{
	public class NameValueString : Dictionary<string, string>
	{
		private string m_naviteText = string.Empty;

		private string m_lineText = string.Empty;

		public string NaviteText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, string> keyValuePair in this)
				{
					stringBuilder.Append(keyValuePair.Key + "=" + keyValuePair.Value + ";");
				}
				this.m_naviteText = stringBuilder.ToString().TrimEnd(new char[]
				{
					';'
				});
				return this.m_naviteText;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(value);
				if (!flag)
				{
					this.m_naviteText = value;
					base.Clear();
					this.SplitString();
				}
			}
		}

		public string LineText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, string> keyValuePair in this)
				{
					stringBuilder.AppendLine(keyValuePair.Key + "=" + keyValuePair.Value);
				}
				return stringBuilder.ToString();
			}
			set
			{
				this.m_lineText = value;
				base.Clear();
				char[] chArray = new char[]
				{
					'\r',
					'\n'
				};
				string[] array = value.Split(chArray);
				for (int i = 0; i < array.Length; i++)
				{
					string str2 = array[i];
					bool flag = !string.IsNullOrEmpty(str2);
					if (flag)
					{
						base.Add(str2.Split(new char[]
						{
							'='
						})[0].Trim(), str2.Split(new char[]
						{
							'='
						})[1].Trim());
					}
				}
			}
		}

		public string EncodedText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, string> keyValuePair in this)
				{
					stringBuilder.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
				}
				return stringBuilder.ToString().TrimEnd(new char[]
				{
					'&'
				});
			}
		}

		public new string this[string key]
		{
			get
			{
				bool flag = string.IsNullOrEmpty(key);
				if (flag)
				{
					throw new Exception("键/值对中键名不允许为空！");
				}
				string str = string.Empty;
				base.TryGetValue(key, out str);
				return str;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(key);
				if (flag)
				{
					throw new Exception("键/值对中键名不允许为空！");
				}
				this[key] = value;
			}
		}

		public NameValueString() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		private void SplitString()
		{
			string str = string.Empty;
			string str2 = string.Empty;
			string str3 = this.m_naviteText;
			char[] chArray = new char[]
			{
				';'
			};
			string[] array = str3.Split(chArray);
			for (int i = 0; i < array.Length; i++)
			{
				string str4 = array[i];
				bool flag = str4.Split(new char[]
				{
					'='
				}).Length == 2;
				if (flag)
				{
					string key = str4.Split(new char[]
					{
						'='
					})[0].Trim();
					string str5 = str4.Split(new char[]
					{
						'='
					})[1].Trim();
					bool flag2 = string.IsNullOrEmpty(key);
					if (flag2)
					{
						throw new Exception("键/值对字符串解析错误！");
					}
					bool flag3 = !base.ContainsKey(key);
					if (flag3)
					{
						base.Add(key, str5);
					}
				}
			}
		}
	}
}
