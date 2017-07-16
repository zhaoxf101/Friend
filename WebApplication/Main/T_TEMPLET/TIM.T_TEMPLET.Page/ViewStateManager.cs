using System;
using System.Web.UI;

namespace TIM.T_TEMPLET.Page
{
	public class ViewStateManager
	{
		protected internal const string _PageOperStateKey = "PageOperState";

		protected internal const string _ModifierIdKey = "ModifierId";

		protected internal const string _PageWorkflowId = "WorkflowId";

		protected internal const string _PageWorkflowRunId = "WorkflowRunId";

		private StateBag m_viewState = null;

		private StateBag ViewState
		{
			get
			{
				return this.m_viewState;
			}
			set
			{
				this.m_viewState = value;
			}
		}

		public object this[string key]
		{
			get
			{
				return this.m_viewState[key];
			}
			set
			{
				this.m_viewState[key] = value;
			}
		}

		public ViewStateManager(StateBag viewState)
		{
			this.m_viewState = viewState;
		}
	}
}
