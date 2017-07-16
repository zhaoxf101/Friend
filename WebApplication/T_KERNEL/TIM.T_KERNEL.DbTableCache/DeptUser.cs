using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class DeptUser : DbEntity
	{
		private string m_deptId = string.Empty;

		private string m_userId = string.Empty;

		public string DeptId
		{
			get
			{
				return this.m_deptId;
			}
			set
			{
				this.m_deptId = value;
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
