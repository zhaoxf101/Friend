using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TIM.T_WEBCTRL
{
	internal sealed class FilterStream : Stream
	{
		private Stream _stream;

		private StringBuilder _filterStringBuilder = new StringBuilder();

		private Encoding _encoding;

		public override bool CanRead
		{
			get
			{
				return false;
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
				throw new NotSupportedException();
			}
		}

		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public FilterStream(Stream baseStream, Encoding encoding)
		{
			this._stream = baseStream;
			this._encoding = encoding;
		}

		public override void Close()
		{
			new Regex("type\\s*=[\\s|\"]*file", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			Match match = new Regex("</body>", RegexOptions.IgnoreCase).Match(this._filterStringBuilder.ToString());
			bool success = match.Success;
			if (success)
			{
				int index = match.Index;
			}
			else
			{
				int index = this._filterStringBuilder.Length;
			}
			byte[] bytes = this._encoding.GetBytes(this._filterStringBuilder.ToString());
			bool flag = bytes.Length != 0;
			if (flag)
			{
				this._stream.Write(bytes, 0, bytes.Length);
			}
			this._stream.Close();
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long length)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			this._filterStringBuilder.Append(this._encoding.GetString(buffer, offset, count));
		}
	}
}
