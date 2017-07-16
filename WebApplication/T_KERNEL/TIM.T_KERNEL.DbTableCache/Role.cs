using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class Role : DbEntity
	{
		private string m_roleId = string.Empty;

		private string m_roleName = string.Empty;

		private string m_desc = string.Empty;

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

		public string RoleName
		{
			get
			{
				return this.m_roleName;
			}
			set
			{
				this.m_roleName = value;
			}
		}

		public string Desc
		{
			get
			{
				return this.m_desc;
			}
			set
			{
				this.m_desc = value;
			}
		}
	}
}
