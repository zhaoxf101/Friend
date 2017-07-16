using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace TIM.T_WEBCTRL
{
	internal sealed class MimeUploadHandler : IMimePushHandler
	{
		private byte[] _boundary;

		private UploadStatus _uploadStatus;

		private StringBuilder _textParts;

		private long _length;

		private IUploadStreamProvider _UploadStreamProvider;

		private Stream _outputStream;

		private Stream _requeststream;

		private Encoding encoding;

		public long ContentLength
		{
			get
			{
				return this._requeststream.Length;
			}
		}

		public bool IsComplete
		{
			get
			{
				return (long)this.Position >= this.ContentLength;
			}
		}

		public int Position
		{
			get
			{
				return (int)this._requeststream.Position;
			}
		}

		public string TextParts
		{
			get
			{
				return this._textParts.ToString();
			}
		}

		public UploadStatus UploadStatus
		{
			get
			{
				return this._uploadStatus;
			}
		}

		public MimeUploadHandler(Stream s, byte[] boundary, UploadStatus status, Encoding e, IUploadStreamProvider provider)
		{
			this._requeststream = s;
			this._boundary = boundary;
			this._uploadStatus = status;
			this._UploadStreamProvider = provider;
			this.encoding = e;
		}

		public void BeginPart(NameValueCollection headers)
		{
			UploadedFile file = this.GetUploadFile(headers);
			bool flag = file != null;
			if (flag)
			{
				UploadLog.Log("Starting file part", this._uploadStatus.UploadId);
				UploadLog.Log(file, this._uploadStatus.UploadId);
				bool flag2 = HttpUploadModule.UploadFileFilter != null && !HttpUploadModule.UploadFileFilter.ShouldHandleFile(file);
				if (flag2)
				{
					throw new UploadException(UploadTerminationReason.FileFilter);
				}
				this._outputStream = this._UploadStreamProvider.GetOutputStream(file);
				this._length = 0L;
				this._uploadStatus.UploadedFiles.Add(file);
				this._uploadStatus.SetCurrentFileName(file.ClientName);
			}
			else
			{
				UploadLog.Log("Starting non-file part", this._uploadStatus.UploadId);
				this._textParts.Append(this.encoding.GetString(this._boundary) + "\r\n");
				for (int i = 0; i < headers.Count; i++)
				{
					this._textParts.Append(headers.Keys[i] + ": " + headers[i] + "\r\n");
				}
				this._textParts.Append("\r\n");
			}
		}

		public void CancelParse()
		{
			bool flag = this._outputStream != null;
			if (flag)
			{
				this._outputStream.Dispose();
				this._outputStream = null;
			}
		}

		public void EndPart(bool isLast)
		{
			UploadLog.Log("Part complete", this._uploadStatus.UploadId);
			bool flag = this._outputStream != null;
			if (flag)
			{
				this._uploadStatus.UploadedFiles[this._uploadStatus.UploadedFiles.Count - 1].SetContentLength(this._length);
				this._outputStream.Dispose();
				this._outputStream = null;
			}
			else
			{
				this._textParts.Append("\r\n");
			}
			bool flag2 = isLast && this._textParts.Length > 0;
			if (flag2)
			{
				this._textParts.Append(this.encoding.GetString(this._boundary) + "--\r\n\r\n");
			}
		}

		public void Parse()
		{
			this._textParts = new StringBuilder();
			new MimePushReader(this._requeststream, this._boundary, this, this.encoding).Parse();
		}

		public void PartData(ref byte[] data, int start, int length)
		{
			bool flag = this._outputStream != null;
			if (flag)
			{
				this._outputStream.Write(data, start, length);
				this._length += (long)length;
			}
			else
			{
				this._textParts.Append(this.encoding.GetString(data, start, length));
			}
			this._uploadStatus.SetPosition(this._requeststream.Position);
			UploadStatus uploadStatus = HttpUploadModule.StatusManager.GetUploadStatus(this._uploadStatus.UploadId);
			bool flag2 = uploadStatus != null && uploadStatus.State == UploadState.Terminated;
			if (flag2)
			{
				throw new UploadException(uploadStatus.Reason);
			}
		}

		private UploadedFile GetUploadFile(NameValueCollection headers)
		{
			string str = headers["content-disposition"];
			bool flag = str != null;
			UploadedFile result;
			if (flag)
			{
				string[] strArray = ParseContentDisposition.GetContentTextArray(str);
				bool flag2 = strArray.Length > 2;
				if (flag2)
				{
					string clientPath = ParseContentDisposition.RemoveChar(strArray[2], '=')[1];
					bool flag3 = clientPath != "\"\"";
					if (flag3)
					{
						clientPath = clientPath.Replace("\"", string.Empty);
						result = new UploadedFile(clientPath, headers["content-type"], ParseContentDisposition.RemoveChar(strArray[1], '=')[1].Replace("\"", string.Empty));
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		private string[] RemoveChar(string contentText, char removechar)
		{
			int index = contentText.IndexOf(removechar);
			bool flag = index > 0;
			string[] result;
			if (flag)
			{
				result = new string[]
				{
					contentText.Substring(0, index),
					contentText.Substring(index + 1)
				};
			}
			else
			{
				result = new string[]
				{
					contentText
				};
			}
			return result;
		}

		private string[] GetContentTextArray(string contentText)
		{
			List<string> list = new List<string>();
			int startIndex = 0;
			int index = 0;
			while (index < contentText.Length)
			{
				index = contentText.IndexOf('"', startIndex);
				int num3 = contentText.IndexOf(';', startIndex);
				bool flag = index == -1 || num3 < index;
				if (flag)
				{
					index = num3;
				}
				else
				{
					index = contentText.IndexOf('"', index + 1);
					index = contentText.IndexOf(';', index);
				}
				bool flag2 = index == -1;
				if (flag2)
				{
					index = contentText.Length;
				}
				list.Add(contentText.Substring(startIndex, index - startIndex));
				startIndex = index + 1;
			}
			return list.ToArray();
		}
	}
}
