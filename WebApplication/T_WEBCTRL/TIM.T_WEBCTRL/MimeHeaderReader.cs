using System;
using System.Collections.Specialized;
using System.Text;

namespace TIM.T_WEBCTRL
{
	internal sealed class MimeHeaderReader
	{
		private enum HeaderReaderState
		{
			Reading,
			FoundFirstCR,
			FoundFirstLF,
			FoundSecondCR,
			FoundSecondLF
		}

		private bool _headerComplete;

		private StringBuilder headers;

		private Encoding encoding;

		public bool HeaderComplete
		{
			get
			{
				return this._headerComplete;
			}
		}

		public NameValueCollection Headers
		{
			get
			{
				NameValueCollection values = new NameValueCollection();
				string[] array = this.headers.ToString().Split(new char[]
				{
					'\n'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					int index = text.IndexOf(':');
					bool flag = index > 0;
					if (flag)
					{
						values[text.Substring(0, index).Trim()] = text.Substring(index + 1).Trim();
					}
				}
				return values;
			}
		}

		public MimeHeaderReader(Encoding e)
		{
			this.encoding = e;
			this.Reset();
		}

		public int Read(byte[] buffer, int position)
		{
			int count = 0;
			MimeHeaderReader.HeaderReaderState reading = MimeHeaderReader.HeaderReaderState.Reading;
			int i = position;
			while (i < buffer.Length && reading != MimeHeaderReader.HeaderReaderState.FoundSecondLF)
			{
				char c = (char)buffer[i];
				if (c != '\n')
				{
					if (c != '\r')
					{
						reading = MimeHeaderReader.HeaderReaderState.Reading;
					}
					else
					{
						switch (reading)
						{
						case MimeHeaderReader.HeaderReaderState.Reading:
							reading = MimeHeaderReader.HeaderReaderState.FoundFirstCR;
							break;
						case MimeHeaderReader.HeaderReaderState.FoundFirstCR:
						case MimeHeaderReader.HeaderReaderState.FoundFirstLF:
							reading = MimeHeaderReader.HeaderReaderState.FoundSecondCR;
							break;
						}
					}
				}
				else
				{
					switch (reading)
					{
					case MimeHeaderReader.HeaderReaderState.Reading:
					case MimeHeaderReader.HeaderReaderState.FoundFirstCR:
						reading = MimeHeaderReader.HeaderReaderState.FoundFirstLF;
						break;
					case MimeHeaderReader.HeaderReaderState.FoundFirstLF:
					case MimeHeaderReader.HeaderReaderState.FoundSecondCR:
						reading = MimeHeaderReader.HeaderReaderState.FoundSecondLF;
						break;
					}
				}
				count++;
				i++;
			}
			this.headers.Append(this.encoding.GetString(buffer, position, count));
			bool flag = reading == MimeHeaderReader.HeaderReaderState.FoundSecondLF;
			int result;
			if (flag)
			{
				this._headerComplete = true;
				string text = this.headers.ToString(this.headers.Length - 4, 4);
				bool flag2 = text[2] == '\r';
				if (flag2)
				{
					this.headers.Length -= 4;
					result = count;
					return result;
				}
				bool flag3 = text[2] == '\n';
				if (flag3)
				{
					this.headers.Length -= 2;
				}
			}
			result = count;
			return result;
		}

		public void Reset()
		{
			this.headers = new StringBuilder();
			this._headerComplete = false;
		}
	}
}
