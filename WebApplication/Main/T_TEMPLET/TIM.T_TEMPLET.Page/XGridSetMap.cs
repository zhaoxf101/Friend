using System;
using System.Web.UI;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Page
{
	public class XGridSetMap
	{
		private string m_tabId = string.Empty;

		private TimXGrid m_xGrid = null;

		private EntityManager m_gridEntity = null;

		private UpdatePanel m_updPanel = null;

		public string TabId
		{
			get
			{
				return this.m_tabId;
			}
			set
			{
				this.m_tabId = value;
			}
		}

		public TimXGrid XGrid
		{
			get
			{
				return this.m_xGrid;
			}
			set
			{
				this.m_xGrid = value;
			}
		}

		public EntityManager GridEntity
		{
			get
			{
				return this.m_gridEntity;
			}
			set
			{
				this.m_gridEntity = value;
			}
		}

		public UpdatePanel UpdPanel
		{
			get
			{
				return this.m_updPanel;
			}
			set
			{
				this.m_updPanel = value;
			}
		}

		public XGridSetMap(TimXGrid grid, EntityManager entity)
		{
			this.TabId = string.Empty;
			this.XGrid = grid;
			this.GridEntity = entity;
			this.UpdPanel = null;
		}

		public XGridSetMap(TimXGrid grid, EntityManager entity, UpdatePanel updPanel)
		{
			this.TabId = string.Empty;
			this.XGrid = grid;
			this.GridEntity = entity;
			this.UpdPanel = updPanel;
		}

		public XGridSetMap(string tabId, TimXGrid grid, EntityManager entity, UpdatePanel updPanel)
		{
			this.TabId = tabId;
			this.XGrid = grid;
			this.GridEntity = entity;
			this.UpdPanel = updPanel;
		}
	}
}
