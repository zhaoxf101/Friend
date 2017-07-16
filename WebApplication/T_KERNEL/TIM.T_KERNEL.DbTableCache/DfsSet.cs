using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class DfsSet : DbEntity
	{
		private string m_fsId = string.Empty;

		private string m_fsName = string.Empty;

		private string m_serverSite = string.Empty;

		private string m_pathLocation = string.Empty;

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

		public string FsName
		{
			get
			{
				return this.m_fsName;
			}
			set
			{
				this.m_fsName = value;
			}
		}

		public string ServerSite
		{
			get
			{
				return this.m_serverSite;
			}
			set
			{
				this.m_serverSite = value;
			}
		}

		public string PathLocation
		{
			get
			{
				return this.m_pathLocation;
			}
			set
			{
				this.m_pathLocation = value;
			}
		}
	}
}
