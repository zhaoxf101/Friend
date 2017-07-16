using System;
using System.Reflection;

namespace TIM.T_TEMPLET.Reporting
{
	internal class FunctionTriple
	{
		private string m_name = string.Empty;

		private object m_instance = null;

		private MethodInfo m_method = null;

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

		public object Instance
		{
			get
			{
				return this.m_instance;
			}
			set
			{
				this.m_instance = value;
			}
		}

		public MethodInfo Method
		{
			get
			{
				return this.m_method;
			}
			set
			{
				this.m_method = value;
			}
		}

		public FunctionTriple(string name, object instance, MethodInfo method)
		{
			this.m_name = name;
			this.m_instance = instance;
			this.m_method = method;
		}
	}
}
