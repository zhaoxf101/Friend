using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TIM.T_KERNEL.GSM
{
	public class GsmModem
	{
		private Queue<int> _NewMsgIndexQueue = new Queue<int>();

		private string _MsgCenterNo = string.Empty;

		private bool m_autoDelMsg = true;

		private ICom _GsmCom;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event EventHandler SmsRecieved;

		public bool AutoDelMsg
		{
			get
			{
				return this.m_autoDelMsg;
			}
			set
			{
				this.m_autoDelMsg = value;
			}
		}

		public GsmModem() : this("COM1", 9600)
		{
		}

		public GsmModem(string comPort, int baudRate)
		{
			this._GsmCom = new GsmCom();
			this._GsmCom.PortName = comPort;
			this._GsmCom.BaudRate = baudRate;
			this._GsmCom.ReadTimeout = 100000;
			this._GsmCom.WriteTimeout = 100000;
			this._GsmCom.RtsEnable = true;
			this._GsmCom.DataReceived += new EventHandler(this.sp_DataReceived);
		}

		public void Open()
		{
			bool isOpen = this._GsmCom.IsOpen;
			if (isOpen)
			{
				this._GsmCom.Close();
			}
			Thread.Sleep(50);
			this._GsmCom.Open();
			bool flag = !this._GsmCom.IsOpen;
			if (!flag)
			{
				this._GsmCom.Write("ATE0\r");
				Thread.Sleep(1000);
				this._GsmCom.Write("AT+CMGF=0\r");
				Thread.Sleep(1000);
				this._GsmCom.Write("AT+CNMI=2,1\r");
				Thread.Sleep(1000);
			}
		}

		public void Close()
		{
			this._GsmCom.Close();
		}

		public string GetMachineNo()
		{
			string str = this.SendAT("AT+CGMI");
			bool flag = str.Substring(str.Length - 4, 3).Trim() == "OK";
			if (flag)
			{
				return str.Substring(0, str.Length - 5).Trim();
			}
			throw new Exception("获取机器码失败");
		}

		public void SetMsgCenterNo(string msgCenterNo)
		{
			this._MsgCenterNo = msgCenterNo;
		}

		public string GetMsgCenterNo()
		{
			string str = string.Empty;
			bool flag = this._MsgCenterNo != null && this._MsgCenterNo.Length != 0;
			string result;
			if (flag)
			{
				result = this._MsgCenterNo;
			}
			else
			{
				string str2 = this.SendAT("AT+CSCA?");
				bool flag2 = !(str2.Substring(str2.Length - 4, 3).Trim() == "OK");
				if (flag2)
				{
					throw new Exception("获取短信中心失败");
				}
				result = str2.Split(new char[]
				{
					'"'
				})[1].Trim();
			}
			return result;
		}

		public string SendAT(string ATCom)
		{
			string str = string.Empty;
			this._GsmCom.DiscardInBuffer();
			this._GsmCom.DataReceived -= new EventHandler(this.sp_DataReceived);
			try
			{
				this._GsmCom.Write(ATCom + "\r");
			}
			catch (Exception ex_46)
			{
				this._GsmCom.DataReceived += new EventHandler(this.sp_DataReceived);
				throw new Exception("Send AT Write!");
			}
			Thread.Sleep(1000);
			string result;
			try
			{
				string str2 = string.Empty;
				while (str2.Trim() != "OK" && str2.Trim() != "ERROR")
				{
					str2 = this._GsmCom.ReadLine();
					str += str2;
				}
				result = str;
			}
			catch (Exception ex_C7)
			{
				throw new Exception("Send AT ReadLine!" + str);
			}
			finally
			{
				this._GsmCom.DataReceived += new EventHandler(this.sp_DataReceived);
			}
			return result;
		}

		public int SendMsg(string phone, string msg)
		{
			Thread.Sleep(5000);
			int num = 0;
			PDUEncoding pduEncoding = new PDUEncoding();
			pduEncoding.ServiceCenterAddress = this._MsgCenterNo;
			string str = string.Empty;
			foreach (CodedMessage codedMessage in pduEncoding.PDUEncoder(phone, msg))
			{
				this._GsmCom.DataReceived -= new EventHandler(this.sp_DataReceived);
				this._GsmCom.Write("AT+CMGS=" + codedMessage.Length.ToString() + "\r");
				Thread.Sleep(1000);
				this._GsmCom.ReadTo(">");
				this._GsmCom.DiscardInBuffer();
				this._GsmCom.DataReceived += new EventHandler(this.sp_DataReceived);
				string message;
				do
				{
					message = this.SendAT(codedMessage.PduCode + "\u001a");
				}
				while (!message.Contains("OK"));
				bool flag = !message.Contains("OK");
				if (flag)
				{
					throw new Exception(message);
				}
				num++;
			}
			return num;
		}

		public List<DecodedMessage> GetUnreadMsg()
		{
			List<DecodedMessage> list = new List<DecodedMessage>();
			string[] strArray = null;
			string str = string.Empty;
			string str2 = this.SendAT("AT+CMGL=0");
			bool flag = str2.Contains("OK");
			if (flag)
			{
				strArray = str2.Split(new char[]
				{
					'\r'
				});
			}
			PDUEncoding pduEncoding = new PDUEncoding();
			string[] array = strArray;
			for (int i = 0; i < array.Length; i++)
			{
				string strPDU = array[i];
				bool flag2 = strPDU != null && strPDU.Length > 18;
				if (flag2)
				{
					list.Add(pduEncoding.PDUDecoder(strPDU));
				}
			}
			return list;
		}

		public DecodedMessage ReadNewMsg()
		{
			return this.ReadMsgByIndex(this._NewMsgIndexQueue.Dequeue());
		}

		public DecodedMessage ReadMsgByIndex(int index)
		{
			string str = string.Empty;
			PDUEncoding pduEncoding = new PDUEncoding();
			string str2;
			try
			{
				str2 = this.SendAT("AT+CMGR=" + index.ToString());
			}
			catch (Exception ex)
			{
				throw ex;
			}
			bool flag = str2.Trim() == "ERROR";
			if (flag)
			{
				throw new Exception("没有此短信");
			}
			string strPDU = str2.Split(new char[]
			{
				'\r'
			})[2];
			bool autoDelMsg = this.AutoDelMsg;
			if (autoDelMsg)
			{
				try
				{
					this.DeleteMsgByIndex(index);
				}
				catch
				{
				}
			}
			return pduEncoding.PDUDecoder(strPDU);
		}

		public void DeleteMsgByIndex(int index)
		{
			bool flag = !(this.SendAT("AT+CMGD=" + index.ToString()).Trim() == "OK");
			if (flag)
			{
				throw new Exception("删除失败");
			}
		}

		private void sp_DataReceived(object sender, EventArgs e)
		{
			string str = this._GsmCom.ReadLine();
			bool flag = str.Length <= 8 || !(str.Substring(0, 6) == "+CMTI:");
			if (!flag)
			{
				this._NewMsgIndexQueue.Enqueue(Convert.ToInt32(str.Split(new char[]
				{
					','
				})[1]));
				this.OnSmsRecieved(e);
			}
		}

		protected virtual void OnSmsRecieved(EventArgs e)
		{
			bool flag = this.SmsRecieved == null;
			if (!flag)
			{
				this.SmsRecieved(this, e);
			}
		}
	}
}
