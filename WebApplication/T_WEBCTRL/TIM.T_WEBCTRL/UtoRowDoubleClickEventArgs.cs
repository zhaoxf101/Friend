using System;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class UtoRowDoubleClickEventArgs : EventArgs
	{
		private GridViewRow _GridViewRow;

		public GridViewRow GridViewRow
		{
			get
			{
				return this._GridViewRow;
			}
			set
			{
				this._GridViewRow = value;
			}
		}

		public UtoRowDoubleClickEventArgs(GridViewRow gridViewRow)
		{
			this._GridViewRow = gridViewRow;
		}

		public UtoRowDoubleClickEventArgs()
		{
		}
	}
}
