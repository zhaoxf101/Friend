using System;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RdDataSetField
	{
		private string m_id = "";

		private string m_name = "";

		private RdFieldType m_type = RdFieldType.gfString;

		public string Id
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

		public RdFieldType Type
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
