using System;
using System.IO;
using System.Web;

namespace TIM.T_WEBCTRL
{
	public sealed class FileUploadStreamProvider : IUploadStreamProvider
	{
		public enum ExistingAction
		{
			Overwrite,
			Exception,
			Rename
		}

		public enum FileNameMethod
		{
			Client,
			Guid,
			Custom
		}

		private FileUploadStreamProvider.FileNameMethod _fileNameMethod;

		private FileUploadStreamProvider.ExistingAction _existingAction;

		private IFileNameGenerator _fileNameGenerator;

		private string _location;

		public const string FileNameKey = "fileName";

		public FileUploadStreamProvider(NameValueConfigurationSection configuration)
		{
			this._location = configuration["location"];
			bool flag = this._location == null;
			if (flag)
			{
				throw new ApplicationException();
			}
			bool flag2 = !Path.IsPathRooted(this._location);
			if (flag2)
			{
				this._location = HttpContext.Current.Server.MapPath(this._location);
			}
			string text = configuration["existingAction"];
			bool flag3 = text != null && text.Length != 0;
			if (flag3)
			{
				this._existingAction = (FileUploadStreamProvider.ExistingAction)Enum.Parse(typeof(FileUploadStreamProvider.ExistingAction), text, true);
			}
			else
			{
				this._existingAction = FileUploadStreamProvider.ExistingAction.Exception;
			}
			string text2 = configuration["fileNameMethod"];
			bool flag4 = text2 != null && text2.Length != 0;
			if (flag4)
			{
				this._fileNameMethod = (FileUploadStreamProvider.FileNameMethod)Enum.Parse(typeof(FileUploadStreamProvider.FileNameMethod), text2, true);
			}
			else
			{
				this._fileNameMethod = FileUploadStreamProvider.FileNameMethod.Client;
			}
			bool flag5 = this._fileNameMethod == FileUploadStreamProvider.FileNameMethod.Custom;
			if (flag5)
			{
				this._fileNameGenerator = (ConfigurationHashThread.CreateInstance(configuration["fileNameGenerator"], new object[]
				{
					configuration
				}) as IFileNameGenerator);
				bool flag6 = this._fileNameGenerator == null;
				if (flag6)
				{
					throw new ApplicationException("无法对FileNameGenerator进行实例化.");
				}
			}
		}

		public Stream GetInputStream(UploadedFile file)
		{
			return File.OpenRead(file.LocationInfo["fileName"]);
		}

		public Stream GetOutputStream(UploadedFile file)
		{
			FileUploadStreamProvider.FileNameMethod fileNameMethod = this._fileNameMethod;
			string clientName;
			if (fileNameMethod != FileUploadStreamProvider.FileNameMethod.Guid)
			{
				if (fileNameMethod != FileUploadStreamProvider.FileNameMethod.Custom)
				{
					clientName = file.ClientName;
				}
				else
				{
					clientName = this._fileNameGenerator.GenerateFileName(file);
					bool flag = clientName.StartsWith("~");
					if (flag)
					{
						clientName = HttpContext.Current.Server.MapPath(clientName);
					}
				}
			}
			else
			{
				clientName = Guid.NewGuid().ToString("n");
			}
			bool flag2 = !Path.IsPathRooted(clientName);
			if (flag2)
			{
				clientName = Path.Combine(this._location, clientName);
			}
			string directoryName = Path.GetDirectoryName(clientName);
			bool flag3 = !Directory.Exists(directoryName);
			if (flag3)
			{
				Directory.CreateDirectory(directoryName);
			}
			file.LocationInfo["fileName"] = clientName;
			Stream stream = null;
			Stream stream2;
			try
			{
				switch (this._existingAction)
				{
				case FileUploadStreamProvider.ExistingAction.Overwrite:
					stream = new FileStream(clientName, FileMode.Create);
					break;
				case FileUploadStreamProvider.ExistingAction.Exception:
					stream = new FileStream(clientName, FileMode.CreateNew);
					break;
				case FileUploadStreamProvider.ExistingAction.Rename:
				{
					string fileName = Path.GetFileName(clientName);
					int index = fileName.IndexOf('.');
					bool flag4 = index == -1;
					string text4;
					string text5;
					if (flag4)
					{
						text4 = clientName;
						text5 = string.Empty;
					}
					else
					{
						text4 = Path.Combine(Path.GetDirectoryName(clientName), fileName.Substring(0, index));
						text5 = fileName.Substring(index);
					}
					int num2 = 0;
					do
					{
						num2++;
						clientName = string.Concat(new string[]
						{
							text4,
							"[",
							num2.ToString(),
							"]",
							text5
						});
						bool flag5 = !File.Exists(clientName);
						if (flag5)
						{
							file.LocationInfo["fileName"] = clientName;
							try
							{
								stream = new FileStream(clientName, FileMode.CreateNew);
							}
							catch
							{
							}
						}
					}
					while (stream == null);
					break;
				}
				}
				stream2 = stream;
			}
			catch
			{
				bool flag6 = stream != null;
				if (flag6)
				{
					stream.Close();
					this.RemoveOutput(file);
				}
				file.LocationInfo.Clear();
				throw;
			}
			return stream2;
		}

		public void RemoveOutput(UploadedFile file)
		{
			bool flag = file.LocationInfo.Count > 0;
			if (flag)
			{
				File.Delete(file.LocationInfo["fileName"]);
			}
		}
	}
}
