using System;

namespace TIM.T_TEMPLET.Reporting
{
	internal class ReportingException : Exception
	{
		private string m_msg = string.Empty;

		public override string Message
		{
			get
			{
				return this.m_msg;
			}
		}

		internal ReportingException(string msg)
		{
			this.m_msg = msg;
		}
	}
}
