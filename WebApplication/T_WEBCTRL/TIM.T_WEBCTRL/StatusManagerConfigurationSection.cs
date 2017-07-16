using System;
using System.Xml;

namespace TIM.T_WEBCTRL
{
	public class StatusManagerConfigurationSection : NameValueConfigurationSection
	{
		private int _updateInterval;

		private bool _noInterval;

		public int UpdateInterval
		{
			get
			{
				bool flag = !this._noInterval;
				if (flag)
				{
					string text = base["updateInterval"];
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						this._updateInterval = int.Parse(text);
					}
					this._noInterval = true;
				}
				return this._updateInterval;
			}
		}

		internal StatusManagerConfigurationSection(XmlNode section) : base(section)
		{
		}

		internal StatusManagerConfigurationSection(NameValueConfigurationSection parent, XmlNode section) : base(parent, section)
		{
		}
	}
}
