using System;

namespace TIM.T_TEMPLET.Page
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class RelatedEntityAttribute : Attribute
	{
		private string m_name = string.Empty;

		private string m_related = string.Empty;

		private string m_masterMainKey = string.Empty;

		private string m_slaveMainKey = string.Empty;

		private bool m_deleteSlave = true;

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

		public string Related
		{
			get
			{
				return this.m_related;
			}
			set
			{
				this.m_related = value;
			}
		}

		public string MasterMainKey
		{
			get
			{
				return this.m_masterMainKey;
			}
			set
			{
				this.m_masterMainKey = value;
			}
		}

		public string SlaveMainKey
		{
			get
			{
				return this.m_slaveMainKey;
			}
			set
			{
				this.m_slaveMainKey = value;
			}
		}

		public bool DeleteSlave
		{
			get
			{
				return this.m_deleteSlave;
			}
			set
			{
				this.m_deleteSlave = value;
			}
		}
	}
}
