using System;

namespace TIM.T_TEMPLET.DFS
{
	public class DocFileInfo
	{
		private string m_fsId = string.Empty;

		private double m_fileGroupId = 0.0;

		private double m_fileId = 0.0;

		private string m_fileName = string.Empty;

		private string m_extName = string.Empty;

		private long m_fileSize = 0L;

		public string FsId
		{
			get
			{
				return this.m_fsId;
			}
			set
			{
				this.m_fsId = value;
			}
		}

		public double FileGroupId
		{
			get
			{
				return this.m_fileGroupId;
			}
			set
			{
				this.m_fileGroupId = value;
			}
		}

		public double FileId
		{
			get
			{
				return this.m_fileId;
			}
			set
			{
				this.m_fileId = value;
			}
		}

		public string FileName
		{
			get
			{
				return this.m_fileName;
			}
			set
			{
				this.m_fileName = value;
			}
		}

		public string ExtName
		{
			get
			{
				return this.m_extName;
			}
			set
			{
				this.m_extName = value;
			}
		}

		public long FileSize
		{
			get
			{
				return this.m_fileSize;
			}
			set
			{
				this.m_fileSize = value;
			}
		}
	}
}
