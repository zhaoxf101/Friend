using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class UserGroup : DbEntity
	{
		private string m_ugId = string.Empty;

		private string m_ugName = string.Empty;

		private bool m_disabled = false;

		public string UgId
		{
			get
			{
				return this.m_ugId;
			}
			set
			{
				this.m_ugId = value;
			}
		}

		public string UgName
		{
			get
			{
				return this.m_ugName;
			}
			set
			{
				this.m_ugName = value;
			}
		}

		public bool Disabled
		{
			get
			{
				return this.m_disabled;
			}
			set
			{
				this.m_disabled = value;
			}
		}
	}
}
