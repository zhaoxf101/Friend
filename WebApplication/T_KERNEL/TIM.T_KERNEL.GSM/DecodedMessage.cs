using System;
using System.Collections.Generic;

namespace TIM.T_KERNEL.GSM
{
	public class DecodedMessage
	{
		private SortedDictionary<int, string> sd = new SortedDictionary<int, string>();

		private List<int> li = new List<int>();

		private int current;

		public readonly string Flag;

		public readonly string ServiceCenterAddress;

		public DateTime SendTime;

		public readonly string PhoneNumber;

		public int Total
		{
			get
			{
				return this.sd.Count;
			}
		}

		public bool IsCompleted
		{
			get
			{
				return this.li.Count == this.sd.Count;
			}
		}

		public string SmsContent
		{
			get
			{
				string str = string.Empty;
				foreach (string str2 in this.sd.Values)
				{
					str = ((str2.Length != 0 && str2 != null) ? (str + str2) : (str + "(...)"));
				}
				return str;
			}
		}

		public DecodedMessage(string serviceCenterAddress, string sendTime, string phoneNumber, string smsContent) : this("010100", serviceCenterAddress, sendTime, phoneNumber, smsContent)
		{
		}

		public DecodedMessage(string head, string serviceCenterAddress, string sendTime, string phoneNumber, string smsContent)
		{
			this.ServiceCenterAddress = serviceCenterAddress;
			this.SendTime = DateTime.Parse(sendTime);
			this.PhoneNumber = phoneNumber;
			this.Flag = head.Substring(4, 2);
			this.current = (int)Convert.ToInt16(head.Substring(2, 2), 16);
			for (int key = 1; key <= (int)Convert.ToInt16(head.Substring(0, 2), 16); key++)
			{
				this.sd.Add(key, "");
			}
			this.li.Add((int)Convert.ToInt16(head.Substring(2, 2), 16));
			this.sd[this.li[0]] = smsContent;
		}

		public void Add(DecodedMessage dm)
		{
			bool flag = this.Flag != dm.Flag || dm.PhoneNumber != this.PhoneNumber;
			if (flag)
			{
				throw new ArgumentException("不是本条的一部分");
			}
			int index = dm.current;
			bool flag2 = this.li.Contains(index);
			if (!flag2)
			{
				this.SendTime = dm.SendTime;
				this.sd[index] = dm.sd[index];
			}
		}

		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.Total.ToString("X2"),
				this.current.ToString("X2"),
				this.Flag,
				",",
				this.ServiceCenterAddress,
				",",
				this.PhoneNumber,
				",",
				this.SendTime.ToString("yyyyMMddHHmmss"),
				",",
				this.SmsContent
			});
		}
	}
}
