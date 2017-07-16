using System;
using System.Collections.Generic;

namespace TIM.T_TEMPLET.Reporting
{
	internal class ExpressionsVar
	{
		private Dictionary<string, object> m_globalVariable = new Dictionary<string, object>();

		public ExpressionsVar()
		{
			this.RegisterGlobalVariable();
		}

		private void RegisterGlobalVariable()
		{
			this.m_globalVariable.Add("PI", 3.1415926535897931);
		}

		public void Add(string name, object value)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (!flag)
			{
				string _nameUpper = name.Trim().ToUpper();
				bool flag2 = !this.m_globalVariable.ContainsKey(_nameUpper);
				if (flag2)
				{
					this.m_globalVariable.Add(_nameUpper, value);
				}
			}
		}

		public bool Remove(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				string _nameUpper = name.Trim().ToUpper();
				result = this.m_globalVariable.Remove(_nameUpper);
			}
			return result;
		}

		public object Find(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string _nameUpper = name.Trim().ToUpper();
				bool flag2 = this.m_globalVariable.ContainsKey(_nameUpper);
				if (flag2)
				{
					result = this.m_globalVariable[_nameUpper];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}
	}
}
