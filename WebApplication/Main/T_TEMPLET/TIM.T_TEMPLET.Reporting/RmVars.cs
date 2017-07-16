using System;
using System.Collections.Generic;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RmVars
	{
		private RdCell m_cell = null;

		private Dictionary<string, object> m_varValue = null;

		private Dictionary<string, bool> m_vars = null;

		public RdCell Cell
		{
			get
			{
				return this.m_cell;
			}
			set
			{
				this.m_cell = value;
			}
		}

		public Dictionary<string, object> VarValue
		{
			get
			{
				return this.m_varValue;
			}
			set
			{
				this.m_varValue = value;
			}
		}

		public Dictionary<string, bool> Vars
		{
			get
			{
				return this.m_vars;
			}
			set
			{
				this.m_vars = value;
			}
		}

		public RmVars(RdCell cell)
		{
			this.m_cell = cell;
		}

		internal void CopyVars(Dictionary<string, bool> fromVars)
		{
			this.Vars = new Dictionary<string, bool>();
			this.VarValue = new Dictionary<string, object>();
			foreach (KeyValuePair<string, bool> item in fromVars)
			{
				this.Vars.Add(item.Key, item.Value);
				this.VarValue.Add(item.Key, "");
			}
		}

		internal bool VarIsCell(string varName)
		{
			bool flag = this.Vars.ContainsKey(varName);
			return flag && this.Vars[varName];
		}

		internal void SetVarValue(string varName, object value)
		{
			bool flag = this.VarValue.ContainsKey(varName);
			if (flag)
			{
				this.VarValue[varName] = value;
			}
		}
	}
}
