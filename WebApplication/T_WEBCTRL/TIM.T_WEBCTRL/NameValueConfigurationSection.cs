using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace TIM.T_WEBCTRL
{
	public class NameValueConfigurationSection
	{
		private Dictionary<string, string> _namevalue = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

		public string this[string key]
		{
			get
			{
				string text;
				bool flag = this._namevalue.TryGetValue(key, out text);
				string result;
				if (flag)
				{
					result = text;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		internal NameValueConfigurationSection(XmlNode section)
		{
			bool flag = section != null;
			if (flag)
			{
				foreach (XmlAttribute attribute in section.Attributes)
				{
					this._namevalue.Add(attribute.Name, attribute.Value);
				}
			}
		}

		internal NameValueConfigurationSection(NameValueConfigurationSection parent, XmlNode section)
		{
			bool flag = parent == null;
			if (flag)
			{
				this._namevalue = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
			}
			else
			{
				this._namevalue = new Dictionary<string, string>(parent._namevalue, StringComparer.InvariantCultureIgnoreCase);
			}
			bool flag2 = section != null;
			if (flag2)
			{
				foreach (XmlAttribute attribute in section.Attributes)
				{
					this._namevalue[attribute.Name] = attribute.Value;
				}
			}
		}

		internal string GetConfigurationHashKey()
		{
			StringBuilder builder = new StringBuilder();
			foreach (string str in this._namevalue.Keys)
			{
				builder.Append(str + "=" + this._namevalue[str] + ";");
			}
			MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.ASCII.GetBytes(builder.ToString());
			return Convert.ToBase64String(provider.ComputeHash(bytes));
		}
	}
}
