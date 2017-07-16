using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class PmRefer : DbEntity
	{
		private string m_pmId = string.Empty;

		private int m_mdId = 0;

		private string m_comId = string.Empty;

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
	}
}
