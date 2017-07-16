using System;

namespace TIM.T_KERNEL.Data
{
	public class HSQLParameter
	{
		private TimDbType pType = TimDbType.Null;

		private string m_name = null;

		private object m_value = null;

		public int Size = 0;

		public string SourceColumn = null;

		public TimDbType ParamterType
		{
			get
			{
				return this.pType;
			}
			set
			{
				this.pType = value;
			}
		}

		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		public object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		public string ValueText
		{
			get
			{
				return this.m_value.ToString();
			}
			set
			{
				this.m_value = value;
				this.pType = TimDbType.Text;
			}
		}

		public HSQLParameter(string name)
		{
			this.m_name = name;
		}

		public HSQLParameter(string name, object value)
		{
			this.m_name = name;
			this.m_value = value;
		}

		public void SetValue(object pValue, TimDbType pt)
		{
			this.m_value = pValue;
			this.pType = pt;
		}

		public void Clear()
		{
			this.m_value = null;
		}
	}
}
