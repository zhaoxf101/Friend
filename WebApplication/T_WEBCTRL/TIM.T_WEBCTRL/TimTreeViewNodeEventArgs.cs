using System;

namespace TIM.T_WEBCTRL
{
	public sealed class TimTreeViewNodeEventArgs : EventArgs
	{
		private TimTreeViewNode _Node;

		public TimTreeViewNode Node
		{
			get
			{
				return this._Node;
			}
		}

		public TimTreeViewNodeEventArgs(TimTreeViewNode node)
		{
			this._Node = node;
		}
	}
}
