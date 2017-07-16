using System;
using System.IO.Ports;

namespace TIM.T_KERNEL.GSM
{
	public interface ICom
	{
		event EventHandler DataReceived;

		int BaudRate
		{
			get;
			set;
		}

		int DataBits
		{
			get;
			set;
		}

		bool DtrEnable
		{
			get;
			set;
		}

		Handshake Handshake
		{
			get;
			set;
		}

		bool IsOpen
		{
			get;
		}

		Parity Parity
		{
			get;
			set;
		}

		string PortName
		{
			get;
			set;
		}

		int ReadTimeout
		{
			get;
			set;
		}

		int WriteTimeout
		{
			get;
			set;
		}

		bool RtsEnable
		{
			get;
			set;
		}

		StopBits StopBits
		{
			get;
			set;
		}

		void Close();

		void DiscardInBuffer();

		void Open();

		int ReadByte();

		int ReadChar();

		string ReadExisting();

		string ReadLine();

		string ReadTo(string value);

		void Write(string text);

		void WriteLine(string text);
	}
}
