using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.DbTableCache;

namespace TIM.T_KERNEL.Menu
{
	[JsonObject(MemberSerialization.OptIn)]
	public class TimMenu
	{
		private int m_id = 0;

		private string m_name = string.Empty;

		private int m_order = 0;

		private int m_fatherId = 0;

		private ModuleType m_type = ModuleType.A;

		private string m_url = "ActiveModule.aspx?AMID=";

		private List<TimMenu> m_children = new List<TimMenu>();

		[JsonIgnore]
		public int Id
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

		[JsonProperty(PropertyName = "tabid")]
		public string TabId
		{
			get
			{
				return "T" + this.Id;
			}
		}

		[JsonProperty(PropertyName = "text")]
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

		[JsonIgnore]
		public int Order
		{
			get
			{
				return this.m_order;
			}
			set
			{
				this.m_order = value;
			}
		}

		[JsonIgnore]
		public int FatherId
		{
			get
			{
				return this.m_fatherId;
			}
			set
			{
				this.m_fatherId = value;
			}
		}

		[JsonIgnore]
		public ModuleType Type
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

		[JsonProperty(PropertyName = "type")]
		public string TypeStr
		{
			get
			{
				return this.m_type.ToString();
			}
		}

		[JsonProperty(PropertyName = "url")]
		public string Url
		{
			get
			{
				bool flag = this.m_type == ModuleType.A;
				string result;
				if (flag)
				{
					result = this.m_url + this.m_id.ToString();
				}
				else
				{
					result = string.Empty;
				}
				return result;
			}
			set
			{
				this.m_url = value;
			}
		}

		[JsonProperty(PropertyName = "children")]
		public List<TimMenu> Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				this.m_children = value;
			}
		}

		public static void GenerateSystemMenu()
		{
			HSQL hsql = new HSQL(LogicContext.GetDatabase());
		}
	}
}
