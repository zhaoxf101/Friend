using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class Parameter : DbEntity
	{
		private string m_pmId = string.Empty;

		private string m_pmMc = string.Empty;

		private ParameterType m_type = ParameterType.Null;

		private ParameterControlType m_controlType = ParameterControlType.Null;

		private string m_desc = string.Empty;

		private string m_values = string.Empty;

		private string m_defaultValue = string.Empty;

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

		public string PmMc
		{
			get
			{
				return this.m_pmMc;
			}
			set
			{
				this.m_pmMc = value;
			}
		}

		public ParameterType Type
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

		public ParameterControlType ControlType
		{
			get
			{
				return this.m_controlType;
			}
			set
			{
				this.m_controlType = value;
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

		public string Values
		{
			get
			{
				return this.m_values;
			}
			set
			{
				this.m_values = value;
			}
		}

		public string DefaultValue
		{
			get
			{
				return this.m_defaultValue;
			}
			set
			{
				this.m_defaultValue = value;
			}
		}
	}
}
