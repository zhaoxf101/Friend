using System;
using System.Collections.Generic;
using System.IO;

namespace TIM.T_WEBCTRL
{
	public sealed class UploadedFile
	{
		private Dictionary<string, string> _locationInfo;

		private string _clientName;

		private long _contentLength;

		private string _clientPath;

		private string _sourceElement;

		private string _contentType;

		public string ClientName
		{
			get
			{
				bool flag = this._clientName == null;
				if (flag)
				{
					this._clientName = Path.GetFileName(this._clientPath);
				}
				return this._clientName;
			}
		}

		public string ClientPath
		{
			get
			{
				return this._clientPath;
			}
		}

		public long ContentLength
		{
			get
			{
				return this._contentLength;
			}
		}

		public string ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		public Dictionary<string, string> LocationInfo
		{
			get
			{
				return this._locationInfo;
			}
		}

		public string ServerPath
		{
			get
			{
				string result;
				try
				{
					string serverpath = this.LocationInfo["fileName"];
				}
				catch
				{
					result = null;
					return result;
				}
				result = this.LocationInfo["fileName"];
				return result;
			}
		}

		public string SourceElement
		{
			get
			{
				return this._sourceElement;
			}
		}

		internal UploadedFile()
		{
			this._locationInfo = new Dictionary<string, string>();
		}

		internal UploadedFile(string clientPath, string contentType, string sourceElement) : this()
		{
			this._clientPath = clientPath;
			this._contentType = contentType;
			this._sourceElement = sourceElement;
		}

		public void Save(Stream outputStream)
		{
			this.Save(outputStream, true);
		}

		public void Save(Stream outputStream, bool removeOriginal)
		{
			using (Stream stream = HttpUploadModule.UploadStreamProvider.GetInputStream(this))
			{
				byte[] buffer = new byte[8192];
				int num;
				while ((num = stream.Read(buffer, 0, 8192)) > 0)
				{
					outputStream.Write(buffer, 0, num);
				}
			}
			if (removeOriginal)
			{
				HttpUploadModule.UploadStreamProvider.RemoveOutput(this);
			}
		}

		public void MoveTo(string path)
		{
			File.Move(this.ServerPath, path);
		}

		public FileStream GetFileStreamFromUploadedFile()
		{
			return new FileStream(this.ServerPath, FileMode.Open);
		}

		public void DeleteUploadedFileOnUploadLocation()
		{
			try
			{
				HttpUploadModule.UploadStreamProvider.RemoveOutput(this);
			}
			catch
			{
			}
		}

		internal object[] UploadedObjects()
		{
			string[] array = new string[this._locationInfo.Count];
			this._locationInfo.Keys.CopyTo(array, 0);
			string[][] textArray2 = new string[array.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				textArray2[i] = new string[]
				{
					array[i],
					this._locationInfo[array[i]]
				};
			}
			return new object[]
			{
				this._clientPath,
				this._contentType,
				this._sourceElement,
				this._contentLength.ToString(),
				textArray2
			};
		}

		internal void SetContentLength(long contentLength)
		{
			this._contentLength = contentLength;
		}

		internal static UploadedFile GetLosObjectArray(object[] LosObjectArray)
		{
			UploadedFile file = new UploadedFile((string)LosObjectArray[0], (string)LosObjectArray[1], (string)LosObjectArray[2]);
			file.SetContentLength((long)int.Parse((string)LosObjectArray[3]));
			string[][] textArray = (string[][])LosObjectArray[4];
			for (int i = 0; i < textArray.Length; i++)
			{
				file._locationInfo[textArray[i][0]] = textArray[i][1];
			}
			return file;
		}
	}
}
