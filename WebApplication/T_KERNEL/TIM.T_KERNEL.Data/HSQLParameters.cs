using System;
using System.Collections;

namespace TIM.T_KERNEL.Data
{
	public class HSQLParameters
	{
		private Hashtable m_items = new Hashtable();

		public int Count
		{
			get
			{
				return this.m_items.Count;
			}
		}

		public HSQLParameter this[string name]
		{
			get
			{
				return (HSQLParameter)this.m_items[name.ToUpper()];
			}
		}

		public HSQLParameter AddParam(string name)
		{
			return this.AddParam(name, null);
		}

		public HSQLParameter AddParam(string name, object value)
		{
			bool flag = this.ParamExists(name);
			if (flag)
			{
				throw new Exception(string.Format("指定参数[{0}]已存在，不能重复加入", name));
			}
			HSQLParameter hsqlParameter = new HSQLParameter(name, value);
			this.m_items.Add(name.ToUpper(), hsqlParameter);
			return hsqlParameter;
		}

		public bool ParamExists(string name)
		{
			return this.m_items.ContainsKey(name.ToUpper());
		}

		public void SyncParams(ArrayList paramList)
		{
			foreach (DictionaryEntry dictionaryEntry in this.m_items)
			{
				bool flag = !paramList.Contains(dictionaryEntry.Key);
				if (flag)
				{
					this.m_items.Remove(dictionaryEntry.Key);
				}
			}
			foreach (string name in paramList)
			{
				bool flag2 = !this.m_items.ContainsKey(name);
				if (flag2)
				{
					this.AddParam(name);
				}
			}
		}

		public void Clear()
		{
			this.m_items.Clear();
		}
	}
}
