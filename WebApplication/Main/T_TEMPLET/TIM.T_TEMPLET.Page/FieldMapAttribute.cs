using System;
using System.Web.Script.Serialization;
using TIM.T_KERNEL.Data;

namespace TIM.T_TEMPLET.Page
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	[Serializable]
	public class FieldMapAttribute : Attribute
	{
		private string m_dbField = string.Empty;

		private string m_desc = string.Empty;

		private bool m_key = false;

		private TimDbType m_dbType = TimDbType.Null;

		private bool m_null = true;

		private int m_len = 0;

		private string m_ctrlId = string.Empty;

		private ControlType m_ctrlType = ControlType.TextBox;

		private string m_defValue = string.Empty;

		private string m_oldValue = string.Empty;

		private string m_newValue = string.Empty;

		private bool m_printVisible = true;

		public string DbField
		{
			get
			{
				return this.m_dbField;
			}
			set
			{
				this.m_dbField = value;
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

		public bool Key
		{
			get
			{
				return this.m_key;
			}
			set
			{
				this.m_key = value;
			}
		}

		public TimDbType DbType
		{
			get
			{
				return this.m_dbType;
			}
			set
			{
				this.m_dbType = value;
			}
		}

		public bool Null
		{
			get
			{
				return this.m_null;
			}
			set
			{
				this.m_null = value;
			}
		}

		public int Len
		{
			get
			{
				return this.m_len;
			}
			set
			{
				this.m_len = value;
			}
		}

		public string CtrlId
		{
			get
			{
				return this.m_ctrlId;
			}
			set
			{
				this.m_ctrlId = value;
			}
		}

		public ControlType CtrlType
		{
			get
			{
				return this.m_ctrlType;
			}
			set
			{
				this.m_ctrlType = value;
			}
		}

		public string DefValue
		{
			get
			{
				return this.m_defValue;
			}
			set
			{
				this.m_defValue = value;
			}
		}

		public string OldValue
		{
			get
			{
				return this.m_oldValue;
			}
			set
			{
				this.m_oldValue = value;
			}
		}

		public string NewValue
		{
			get
			{
				return this.m_newValue;
			}
			set
			{
				this.m_newValue = value;
			}
		}

		public bool PrintVisible
		{
			get
			{
				return this.m_printVisible;
			}
			set
			{
				this.m_printVisible = value;
			}
		}

		[ScriptIgnore]
		public override object TypeId
		{
			get
			{
				return base.TypeId;
			}
		}
	}
}
