using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class MePage : DbEntity
	{
		private int m_mdId = 0;

		private string m_comId = string.Empty;

		private string m_wfbId = string.Empty;

		private string m_wfId = string.Empty;

		private string m_pageUrl = string.Empty;

		private ModuleType m_type = ModuleType.A;

		public int MdId
		{
			get
			{
				return this.m_mdId;
			}
			set
			{
				this.m_mdId = value;
			}
		}

		public string ComId
		{
			get
			{
				return this.m_comId;
			}
			set
			{
				this.m_comId = value;
			}
		}

		public string WfbId
		{
			get
			{
				return this.m_wfbId;
			}
			set
			{
				this.m_wfbId = value;
			}
		}

		public string WfId
		{
			get
			{
				return this.m_wfId;
			}
			set
			{
				this.m_wfId = value;
			}
		}

		public string PageUrl
		{
			get
			{
				return this.m_pageUrl;
			}
			set
			{
				this.m_pageUrl = value;
			}
		}

		public ModuleType Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}
	}
}
