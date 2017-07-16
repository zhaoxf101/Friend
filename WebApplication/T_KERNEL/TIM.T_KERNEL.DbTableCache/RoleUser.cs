using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class RoleUser : DbEntity
	{
		private string m_roleId = string.Empty;

		private string m_userId = string.Empty;

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
