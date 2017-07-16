using System;
using System.IO;
using TIM.T_KERNEL.Helper;
using TIM.T_TEMPLET.Page;

namespace TIM.T_TEMPLET.DFS
{
	public class DfsFileDownload : TDlHandlerBase
	{
		private double _FileId = 0.0;

		private string _FileName = string.Empty;

		private string _FileExtName = string.Empty;

		private string _DownloadFileFullPath = string.Empty;

		private FileStream _FileStream;

		protected override void ParsePageParam()
		{
			this._FileId = base.Server.UrlDecode(base.PageParam.GetString("FILEID")).ToDouble();
			this._FileName = base.Server.UrlDecode(base.PageParam.GetString("FILENAME"));
			this._FileExtName = base.Server.UrlDecode(base.PageParam.GetString("EXTNAME"));
		}

		protected override string GetFileName()
		{
			return this._FileName + this._FileExtName;
		}

		protected override long GetLength()
		{
			bool flag = this._FileStream != null;
			long result;
			if (flag)
			{
				result = this._FileStream.Length;
			}
			else
			{
				result = 0L;
			}
			return result;
		}

		protected override void PrepareData()
		{
			this._DownloadFileFullPath = FileService.GetFilePath(this._FileId);
			bool flag = File.Exists(this._DownloadFileFullPath);
			if (flag)
			{
				this._FileStream = File.OpenRead(this._DownloadFileFullPath);
				return;
			}
			throw new Exception("文件已被他人删除，请刷新后查看！");
		}

		protected override bool SendData()
		{
			bool flag = this._FileStream != null;
			return flag && base.SendStreamData(this._FileStream);
		}

		protected override bool ClearData()
		{
			bool flag = this._FileStream != null;
			if (flag)
			{
				this._FileStream.Close();
			}
			return true;
		}
	}
}
