using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class Module : DbEntity
	{
		private int m_mdId = 0;

		private string m_comId = string.Empty;

		private string m_mdName = string.Empty;

		private ModuleType m_type = ModuleType.A;

		private string m_url = string.Empty;

		private string m_wfbId = string.Empty;

		private int m_stdMdId = 0;

		private string m_attribute = string.Empty;

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

		public string MdName
		{
			get
			{
				return this.m_mdName;
			}
			set
			{
				this.m_mdName = value;
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

		public string Url
		{
			get
			{
				return this.m_url;
			}
			set
			{
				this.m_url = value;
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

		public int StdMdId
		{
			get
			{
				return this.m_stdMdId;
			}
			set
			{
				this.m_stdMdId = value;
			}
		}

		public string Attribute
		{
			get
			{
				return this.m_attribute;
			}
			set
			{
				this.m_attribute = value;
			}
		}
	}
}
