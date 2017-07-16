using System;

namespace TIM.T_TEMPLET.Reporting
{
	public class RdSetNode
	{
		private RdDocument m_document = null;

		internal RdDocument Document
		{
			get
			{
				return this.m_document;
			}
			set
			{
				this.m_document = value;
			}
		}

		internal RdSetNode(RdDocument document)
		{
			bool flag = document == null;
			if (flag)
			{
				throw new Exception("报表样式无法解析！");
			}
			this.m_document = document;
		}
	}
}
