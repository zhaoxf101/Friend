using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class UgUser : DbEntity
	{
		private string m_ugId = string.Empty;

		private string m_userId = string.Empty;

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
	}
}
