using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Web.UI;

namespace TIM.T_WEBCTRL
{
	public sealed class UploadStatus
	{
		private UploadState _state;

		private List<UploadedFile> _uploadedFiles;

		private DateTime _start = DateTime.Now;

		private long _position;

		private string _errorMessage;

		private UploadTerminationReason _reason;

		private long _contentLength;

		private Exception[] _errors;

		private DateTime _lastSave = DateTime.MinValue;

		private string _uploadId;

		private string _currentFileName;

		public Exception[] AllErrors
		{
			get
			{
				return this._errors;
			}
		}

		public long ContentLength
		{
			get
			{
				return this._contentLength;
			}
		}

		public string CurrentFileName
		{
			get
			{
				return this._currentFileName;
			}
		}

		public string ErrorMessage
		{
			get
			{
				return this._errorMessage;
			}
		}

		public long Position
		{
			get
			{
				return this._position;
			}
		}

		public UploadTerminationReason Reason
		{
			get
			{
				return this._reason;
			}
		}

		public DateTime Start
		{
			get
			{
				return this._start;
			}
		}

		public UploadState State
		{
			get
			{
				return this._state;
			}
		}

		public string UploadId
		{
			get
			{
				return this._uploadId;
			}
		}

		internal List<UploadedFile> UploadedFiles
		{
			get
			{
				return this._uploadedFiles;
			}
		}

		internal UploadStatus(string uploadId)
		{
			this._uploadId = uploadId;
			this._uploadedFiles = new List<UploadedFile>();
		}

		internal UploadStatus(long contentLength, string uploadId)
		{
			this._contentLength = contentLength;
			this._uploadId = uploadId;
			this._uploadedFiles = new List<UploadedFile>();
		}

		public ReadOnlyCollection<UploadedFile> GetUploadedFiles()
		{
			return new ReadOnlyCollection<UploadedFile>(this._uploadedFiles);
		}

		internal object[] UploadedObjects()
		{
			string[] expr_08 = new string[9];
			expr_08[0] = this._contentLength.ToString();
			expr_08[1] = this._position.ToString();
			expr_08[2] = this._start.Ticks.ToString();
			expr_08[3] = this._uploadId;
			expr_08[4] = this._currentFileName;
			int arg_5C_1 = 5;
			int num = (int)this._state;
			expr_08[arg_5C_1] = num.ToString();
			int arg_6D_1 = 6;
			num = (int)this._reason;
			expr_08[arg_6D_1] = num.ToString();
			expr_08[7] = this._errorMessage;
			expr_08[8] = this._lastSave.Ticks.ToString();
			string[] textArray = expr_08;
			object[][] objArray = new object[this.UploadedFiles.Count][];
			for (int i = 0; i < objArray.Length; i++)
			{
				objArray[i] = this.UploadedFiles[i].UploadedObjects();
			}
			return new object[]
			{
				textArray,
				objArray,
				this.AllErrors
			};
		}

		internal static UploadStatus GetUploadStatus(string contentlength)
		{
			LosFormatter formatter = new LosFormatter();
			object[] objArray = formatter.Deserialize(new StringReader(contentlength)) as object[];
			return UploadStatus.GetLosObjectArray(objArray);
		}

		internal static UploadStatus GetLosObjectArray(object[] LosObjectArray)
		{
			string[] textArray = (string[])LosObjectArray[0];
			object[][] objArray = (object[][])LosObjectArray[1];
			Exception[] exceptionArray = (Exception[])LosObjectArray[2];
			UploadStatus status = new UploadStatus((long)int.Parse(textArray[0]), textArray[3]);
			status._position = (long)int.Parse(textArray[1]);
			status._start = new DateTime(long.Parse(textArray[2]));
			status._currentFileName = textArray[4];
			status._state = (UploadState)int.Parse(textArray[5]);
			status._reason = (UploadTerminationReason)int.Parse(textArray[6]);
			status._errorMessage = textArray[7];
			status._lastSave = new DateTime(long.Parse(textArray[8]));
			object[][] array = objArray;
			for (int i = 0; i < array.Length; i++)
			{
				object[] objArray2 = array[i];
				status.UploadedFiles.Add(UploadedFile.GetLosObjectArray(objArray2));
			}
			status._errors = exceptionArray;
			return status;
		}

		internal void SetPosition(long position)
		{
			this._position = position;
			this.UpdateStatus();
		}

		internal void SetCurrentFileName(string fileName)
		{
			this._currentFileName = fileName;
			this.UpdateStatus();
		}

		internal void x98532580ab8a33a1(long contentLength)
		{
			this._start = DateTime.Now;
			this._contentLength = contentLength;
			this._state = UploadState.ReceivingData;
		}

		internal void SetState(UploadState state)
		{
			this._state = state;
			this.UpdateStatus();
		}

		internal void SetState(UploadState state, UploadTerminationReason reason)
		{
			this.SetState(state, reason, null);
		}

		internal void SetState(UploadState state, UploadTerminationReason reason, string errorMessage)
		{
			this._reason = reason;
			this._errorMessage = errorMessage;
			this.SetState(state);
		}

		internal void SetState(UploadState state, UploadTerminationReason reason, Exception[] errors, string errorMessage)
		{
			this._errors = errors;
			this.SetState(state, reason, errorMessage);
		}

		internal void UpdateStatus()
		{
			bool flag = this.State == UploadState.Complete || this.State == UploadState.Terminated || UtoUploadConfiguration.StatusManager.UpdateInterval == 0 || DateTime.Now.Subtract(this._lastSave).TotalSeconds > (double)UtoUploadConfiguration.StatusManager.UpdateInterval;
			if (flag)
			{
				HttpUploadModule.StatusManager.StatusChanged(this);
				this._lastSave = DateTime.Now;
			}
		}

		internal string GetSerializeUploadedObjects()
		{
			LosFormatter formatter = new LosFormatter();
			string result;
			using (StringWriter writer = new StringWriter())
			{
				formatter.Serialize(writer, this.UploadedObjects());
				result = writer.ToString();
			}
			return result;
		}
	}
}
