using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TIM.T_TEMPLET.Page
{
	public class PortalColumn
	{
		private Unit m_width = new Unit("48%");

		private List<PortalPanel> m_panels = new List<PortalPanel>();

		[JsonProperty(PropertyName = "width")]
		public Unit Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		[JsonProperty(PropertyName = "panels")]
		public List<PortalPanel> Panels
		{
			get
			{
				return this.m_panels;
			}
			set
			{
				this.m_panels = value;
			}
		}
	}
}
