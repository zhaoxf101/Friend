using System;
using System.Configuration;
using System.Xml;

namespace TIM.T_WEBCTRL
{
	public class StatusManagerConfigurationSectionHandler : IConfigurationSectionHandler
	{
		public object Create(object parent, object configContext, XmlNode section)
		{
			return new StatusManagerConfigurationSection(section);
		}
	}
}
