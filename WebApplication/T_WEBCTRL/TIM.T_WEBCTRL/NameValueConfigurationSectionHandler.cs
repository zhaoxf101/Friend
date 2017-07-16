using System;
using System.Configuration;
using System.Xml;

namespace TIM.T_WEBCTRL
{
	public class NameValueConfigurationSectionHandler : IConfigurationSectionHandler
	{
		public object Create(object parent, object configContext, XmlNode section)
		{
			return new NameValueConfigurationSection(parent as NameValueConfigurationSection, section);
		}
	}
}
