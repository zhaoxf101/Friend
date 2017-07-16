using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class PmConfig : DbEntity
	{
		private string m_pmId = string.Empty;

		private int m_mdId = 0;

		private string m_userId = string.Empty;

		private string m_roleId = string.Empty;

		private PmConfigType m_type = PmConfigType.Null;

		private string m_value = string.Empty;

		public string PmId
		{
			get
			{
				return this.m_pmId;
			}
			set
			{
				this.m_pmId = value;
			}
		}

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

		public string UserId
		{
			get
			{
				return this.m_userId;
			}
			set
			{
				this.m_userId = value;
			}
		}

		public string RoleId
		{
			get
			{
				return this.m_roleId;
			}
			set
			{
				this.m_roleId = value;
			}
		}

		public PmConfigType Type
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

		public string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}
	}
}
