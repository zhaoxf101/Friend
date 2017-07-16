using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Runtime.CompilerServices;

namespace TIM.T_KERNEL.GSM
{
	internal class GsmCom : ICom
	{
		private SerialPort sp = new SerialPort();

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event EventHandler DataReceived;

		public int BaudRate
		{
			get
			{
				return this.sp.BaudRate;
			}
			set
			{
				this.sp.BaudRate = value;
			}
		}

		public int DataBits
		{
			get
			{
				return this.sp.DataBits;
			}
			set
			{
				this.sp.DataBits = value;
			}
		}

		public bool DtrEnable
		{
			get
			{
				return this.sp.DtrEnable;
			}
			set
			{
				this.sp.DtrEnable = value;
			}
		}

		public Handshake Handshake
		{
			get
			{
				return this.sp.Handshake;
			}
			set
			{
				this.sp.Handshake = value;
			}
		}

		public bool IsOpen
		{
			get
			{
				return this.sp.IsOpen;
			}
		}

		public Parity Parity
		{
			get
			{
				return this.sp.Parity;
			}
			set
			{
				this.sp.Parity = value;
			}
		}

		public string PortName
		{
			get
			{
				return this.sp.PortName;
			}
			set
			{
				this.sp.PortName = value;
			}
		}

		public int ReadTimeout
		{
			get
			{
				return this.sp.ReadTimeout;
			}
			set
			{
				this.sp.ReadTimeout = value;
			}
		}

		public int WriteTimeout
		{
			get
			{
				return this.sp.WriteTimeout;
			}
			set
			{
				this.sp.WriteTimeout = value;
			}
		}

		public bool RtsEnable
		{
			get
			{
				return this.sp.RtsEnable;
			}
			set
			{
				this.sp.RtsEnable = value;
			}
		}

		public StopBits StopBits
		{
			get
			{
				return this.sp.StopBits;
			}
			set
			{
				this.sp.StopBits = value;
			}
		}

		public GsmCom()
		{
			this.sp.DataReceived += new SerialDataReceivedEventHandler(this.sp_DataReceived);
		}

		private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			this.OnDataReceived(e);
		}

		protected virtual void OnDataReceived(EventArgs e)
		{
			bool flag = this.DataReceived == null;
			if (!flag)
			{
				this.DataReceived(this, EventArgs.Empty);
			}
		}

		public void Close()
		{
			this.sp.Close();
		}

		public void DiscardInBuffer()
		{
			this.sp.DiscardInBuffer();
		}

		public void Open()
		{
			this.sp.Open();
		}

		public int ReadByte()
		{
			return this.sp.ReadByte();
		}

		public int ReadChar()
		{
			return this.sp.ReadChar();
		}

		public string ReadExisting()
		{
			return this.sp.ReadExisting();
		}

		public string ReadLine()
		{
			return this.sp.ReadLine();
		}

		public string ReadTo(string value)
		{
			return this.sp.ReadTo(value);
		}

		public void Write(string text)
		{
			this.sp.Write(text);
		}

		public void WriteLine(string text)
		{
			this.sp.WriteLine(text);
		}
	}
}
