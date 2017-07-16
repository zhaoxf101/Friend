using System;

namespace TIM.T_WEBCTRL
{
	public sealed class TimTreeViewMenuClickEventArgs : EventArgs
	{
		private string _MenuValue;

		private TimTreeViewNode _Node;

		public string MenuValue
		{
			get
			{
				return this._MenuValue;
			}
		}

		public TimTreeViewNode Node
		{
			get
			{
				return this._Node;
			}
		}

		public TimTreeViewMenuClickEventArgs(string menuValue, TimTreeViewNode node)
		{
			this._MenuValue = menuValue;
			this._Node = node;
		}
	}
}
