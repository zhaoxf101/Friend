using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class Component : DbEntity
	{
		private string m_comId = string.Empty;

		private string m_comName = string.Empty;

		private int m_mdIdStart = 0;

		private int m_mdIdEnd = 0;

		private bool m_disabled = false;

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

		public string ComName
		{
			get
			{
				return this.m_comName;
			}
			set
			{
				this.m_comName = value;
			}
		}

		public int MdIdStart
		{
			get
			{
				return this.m_mdIdStart;
			}
			set
			{
				this.m_mdIdStart = value;
			}
		}

		public int MdIdEnd
		{
			get
			{
				return this.m_mdIdEnd;
			}
			set
			{
				this.m_mdIdEnd = value;
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
