using System;

namespace TIM.T_WEBCTRL
{
	public class UtoColumnMenuClickEventArgs
	{
		private string m_menuValue;

		public string MenuValue
		{
			get
			{
				return this.m_menuValue;
			}
		}

		public UtoColumnMenuClickEventArgs(string menuValue)
		{
			this.m_menuValue = menuValue;
		}
	}
}
