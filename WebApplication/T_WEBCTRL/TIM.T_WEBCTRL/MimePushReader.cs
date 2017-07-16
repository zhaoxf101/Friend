using System;
using System.IO;
using System.Text;

namespace TIM.T_WEBCTRL
{
	internal sealed class MimePushReader
	{
		private enum MimeReaderState
		{
			ReadingHeaders,
			ReadingBody,
			CheckingEnd,
			Finished
		}

		private byte[] boundary;

		private IMimePushHandler handler;

		private Stream stream;

		private Encoding encoding;

		public MimePushReader(Stream s, byte[] b, IMimePushHandler h, Encoding e)
		{
			this.stream = s;
			this.handler = h;
			this.boundary = b;
			this.encoding = e;
		}

		public void Parse()
		{
			try
			{
				MimePushReader.MimeReaderState readingHeaders = MimePushReader.MimeReaderState.ReadingHeaders;
				MimeHeaderReader headerReader = new MimeHeaderReader(this.encoding);
				byte[] buffer = new byte[8192];
				int num = this.stream.Read(buffer, 0, buffer.Length);
				int index = this.IndexOf(buffer, this.boundary, 0, num) + this.boundary.Length;
				bool flag = buffer[index] == 13;
				if (flag)
				{
					index += 2;
				}
				else
				{
					bool flag2 = buffer[index] == 10;
					if (flag2)
					{
						index++;
					}
				}
				int num2 = 0;
				while (readingHeaders != MimePushReader.MimeReaderState.Finished)
				{
					switch (readingHeaders)
					{
					case MimePushReader.MimeReaderState.ReadingHeaders:
					{
						index += headerReader.Read(buffer, index);
						bool headerComplete = headerReader.HeaderComplete;
						if (headerComplete)
						{
							readingHeaders = MimePushReader.MimeReaderState.ReadingBody;
							this.handler.BeginPart(headerReader.Headers);
							headerReader.Reset();
						}
						break;
					}
					case MimePushReader.MimeReaderState.ReadingBody:
					{
						int num3 = this.IndexOf(buffer, this.boundary, index, num);
						bool flag3 = num3 != -1;
						if (flag3)
						{
							int length = num3 - index;
							bool flag4 = length >= 2;
							if (flag4)
							{
								bool flag5 = buffer[num3 - 2] == 13;
								if (flag5)
								{
									length -= 2;
								}
								else
								{
									bool flag6 = buffer[num3 - 2] == 10;
									if (flag6)
									{
										length--;
									}
								}
							}
							this.handler.PartData(ref buffer, index, length);
							bool flag7 = num3 < num - this.boundary.Length;
							if (flag7)
							{
								bool flag8 = num3 <= num - (this.boundary.Length + 2);
								if (flag8)
								{
									bool isLast = this.encoding.GetString(buffer, num3 + this.boundary.Length, 2) == "--";
									bool flag9 = isLast;
									if (flag9)
									{
										readingHeaders = MimePushReader.MimeReaderState.Finished;
									}
									else
									{
										readingHeaders = MimePushReader.MimeReaderState.ReadingHeaders;
									}
									this.handler.EndPart(isLast);
									index += num3 + this.boundary.Length - index + 2;
								}
								else
								{
									readingHeaders = MimePushReader.MimeReaderState.CheckingEnd;
									bool flag10 = num3 + 2 - num == 1;
									if (flag10)
									{
										num3 = 1;
										buffer[0] = buffer[num - 1];
									}
									else
									{
										num3 = 0;
									}
									num = this.stream.Read(buffer, num3, buffer.Length - num3);
									index = 0;
									num += num3;
								}
							}
							else
							{
								num3 -= num3 - index - length;
								int count = num - num3;
								Buffer.BlockCopy(buffer, num3, buffer, 0, count);
								num = this.stream.Read(buffer, count, buffer.Length - count);
								index = 0;
								num += count;
							}
						}
						else
						{
							this.handler.PartData(ref buffer, index, num - index);
							index += num - index;
						}
						break;
					}
					case MimePushReader.MimeReaderState.CheckingEnd:
					{
						bool flag11 = this.encoding.GetString(buffer, 0, 2) != "--";
						if (flag11)
						{
							index += 2;
							readingHeaders = MimePushReader.MimeReaderState.ReadingHeaders;
						}
						else
						{
							readingHeaders = MimePushReader.MimeReaderState.Finished;
						}
						this.handler.EndPart(readingHeaders == MimePushReader.MimeReaderState.Finished);
						break;
					}
					}
					bool flag12 = readingHeaders != MimePushReader.MimeReaderState.Finished && index >= num;
					if (flag12)
					{
						num = this.stream.Read(buffer, 0, buffer.Length);
						index = 0;
						bool flag13 = num != 0;
						if (!flag13)
						{
							bool flag14 = num2 == 10;
							if (flag14)
							{
								throw new UploadException(UploadTerminationReason.Disconnected);
							}
							num2++;
						}
					}
				}
			}
			catch
			{
				this.handler.EndPart(false);
				throw;
			}
		}

		private int IndexOf(byte[] buffer, byte[] pattern, int start, int end)
		{
			int index = 0;
			int num2 = this.IndexOfAny(buffer, pattern[0], start, end);
			bool flag = num2 != -1;
			int result;
			if (flag)
			{
				while (num2 + index < end)
				{
					bool flag2 = buffer[num2 + index] == pattern[index];
					if (flag2)
					{
						index++;
						bool flag3 = index == pattern.Length;
						if (flag3)
						{
							result = num2;
							return result;
						}
					}
					else
					{
						num2 = this.IndexOfAny(buffer, pattern[0], num2 + index, end);
						bool flag4 = num2 == -1;
						if (flag4)
						{
							result = num2;
							return result;
						}
						index = 0;
					}
				}
			}
			result = num2;
			return result;
		}

		private int IndexOfAny(byte[] arraybyte, byte comparebyte, int start, int end)
		{
			bool flag = end < arraybyte.Length;
			int result;
			if (flag)
			{
				for (int i = start; i < end; i++)
				{
					bool flag2 = arraybyte[i] == comparebyte;
					if (flag2)
					{
						result = i;
						return result;
					}
				}
			}
			else
			{
				for (int j = start; j < arraybyte.Length; j++)
				{
					bool flag3 = arraybyte[j] == comparebyte;
					if (flag3)
					{
						result = j;
						return result;
					}
				}
			}
			result = -1;
			return result;
		}
	}
}
