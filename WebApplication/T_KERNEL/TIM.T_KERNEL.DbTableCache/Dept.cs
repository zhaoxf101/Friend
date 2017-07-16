using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class Dept : DbEntity
	{
		private string m_deptId = string.Empty;

		private string m_deptName = string.Empty;

		private string m_fzrId = string.Empty;

		private bool m_disabled = false;

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

		public string DeptName
		{
			get
			{
				return this.m_deptName;
			}
			set
			{
				this.m_deptName = value;
			}
		}

		public string FzrId
		{
			get
			{
				return this.m_fzrId;
			}
			set
			{
				this.m_fzrId = value;
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
