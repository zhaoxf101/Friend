using System;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RmSetNode
	{
		private RmReportingMaker m_builder = null;

		internal RmReportingMaker Builder
		{
			get
			{
				return this.m_builder;
			}
			set
			{
				this.m_builder = value;
			}
		}

		public RmSetNode(RmReportingMaker builder)
		{
			this.Builder = builder;
		}
	}
}
