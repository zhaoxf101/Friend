using System;
using System.IO;
using System.Web;

namespace TIM.T_WEBCTRL
{
	internal sealed class RequestStream : Stream
	{
		private long _position;

		private byte[] tempBuff;

		private HttpWorkerRequest request;

		private bool isInPreloaded = true;

		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public override long Length
		{
			get
			{
				return long.Parse(this.request.GetKnownRequestHeader(11));
			}
		}

		public override long Position
		{
			get
			{
				return this._position;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public RequestStream(HttpWorkerRequest request)
		{
			this.request = request;
			this.tempBuff = request.GetPreloadedEntityBody();
			bool flag = this.tempBuff == null || this.tempBuff.Length == 0;
			if (flag)
			{
				this.isInPreloaded = false;
			}
		}

		public override void Flush()
		{
			throw new NotImplementedException();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			bool flag = this.Position == this.Length;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = this.isInPreloaded;
				if (flag2)
				{
					num = this.CopyTo(buffer, offset, count);
					bool flag3 = num < count;
					if (flag3)
					{
						bool flag4 = this.request.IsClientConnected() && !this.request.IsEntireEntityBodyIsPreloaded();
						if (flag4)
						{
							num += this.Copy(buffer, offset + num, count - num);
						}
						this.isInPreloaded = false;
					}
				}
				else
				{
					bool flag5 = this.request.IsClientConnected() && (!this.request.IsEntireEntityBodyIsPreloaded() || this.tempBuff == null || long.Parse(this.request.GetKnownRequestHeader(11)) > (long)this.tempBuff.Length);
					if (flag5)
					{
						try
						{
							num = this.Copy(buffer, offset, count);
						}
						catch
						{
							num = 0;
						}
					}
				}
				this._position += (long)num;
				bool flag6 = num == 0;
				if (flag6)
				{
					throw new UploadException(UploadTerminationReason.Disconnected);
				}
				result = num;
			}
			return result;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		private int CopyTo(byte[] buffer, int dstOffset, int copycount)
		{
			bool flag = this._position + (long)copycount < (long)this.tempBuff.Length;
			int num;
			if (flag)
			{
				num = copycount;
			}
			else
			{
				num = this.tempBuff.Length - (int)this._position;
			}
			Buffer.BlockCopy(this.tempBuff, (int)this._position, buffer, dstOffset, num);
			return num;
		}

		private int Copy(byte[] buffer, int dstOffset, int copycount)
		{
			bool flag = this._position + (long)copycount > this.Length;
			if (flag)
			{
				copycount = (int)(this.Length - this._position);
			}
			bool flag2 = dstOffset <= 0;
			int result;
			if (flag2)
			{
				result = this.request.ReadEntityBody(buffer, copycount);
			}
			else
			{
				bool flag3 = this.tempBuff == null || copycount > this.tempBuff.Length;
				if (flag3)
				{
					this.tempBuff = new byte[copycount];
				}
				int count = this.request.ReadEntityBody(this.tempBuff, copycount);
				Buffer.BlockCopy(this.tempBuff, 0, buffer, dstOffset, count);
				result = count;
			}
			return result;
		}
	}
}
