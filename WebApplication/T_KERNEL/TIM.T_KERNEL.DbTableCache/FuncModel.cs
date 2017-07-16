using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class FuncModel
	{
		private int m_id = 0;

		private string m_name = string.Empty;

		private int m_order = 0;

		private int m_fatherId = 0;

		private ModuleType m_type = ModuleType.A;

		public int Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		public int Order
		{
			get
			{
				return this.m_order;
			}
			set
			{
				this.m_order = value;
			}
		}

		public int FatherId
		{
			get
			{
				return this.m_fatherId;
			}
			set
			{
				this.m_fatherId = value;
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
	}
}
