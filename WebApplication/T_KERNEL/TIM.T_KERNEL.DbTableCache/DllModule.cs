using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class DllModule : Module
	{
		private Type m_callObjectType = null;

		private object m_callModuleObj = null;

		public Type CallObjectType
		{
			get
			{
				return this.m_callObjectType;
			}
			set
			{
				this.m_callObjectType = value;
			}
		}

		public object CallModuleObj
		{
			get
			{
				return this.m_callModuleObj;
			}
			set
			{
				this.m_callModuleObj = value;
			}
		}
	}
}
