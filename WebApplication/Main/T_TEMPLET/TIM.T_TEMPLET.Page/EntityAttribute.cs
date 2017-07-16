using System;
using System.Web.Script.Serialization;

namespace TIM.T_TEMPLET.Page
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	[Serializable]
	public class EntityAttribute : Attribute
	{
		private string m_table = string.Empty;

		private bool m_workflow = false;

		private string m_workflowBusinessId = string.Empty;

		private bool m_beDoc = false;

		private bool m_modifyControl = false;

		public string Table
		{
			get
			{
				return this.m_table;
			}
			set
			{
				this.m_table = value;
			}
		}

		public bool Workflow
		{
			get
			{
				return this.m_workflow;
			}
			set
			{
				this.m_workflow = value;
			}
		}

		public string WorkflowBusinessId
		{
			get
			{
				return this.m_workflowBusinessId;
			}
			set
			{
				this.m_workflowBusinessId = value;
			}
		}

		public bool BeDoc
		{
			get
			{
				return this.m_beDoc;
			}
			set
			{
				this.m_beDoc = value;
			}
		}

		public bool ModifyControl
		{
			get
			{
				return this.m_modifyControl;
			}
			set
			{
				this.m_modifyControl = value;
			}
		}

		[ScriptIgnore]
		public override object TypeId
		{
			get
			{
				return base.TypeId;
			}
		}
	}
}
