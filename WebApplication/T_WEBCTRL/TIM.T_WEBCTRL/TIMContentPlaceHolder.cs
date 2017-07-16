using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class TIMContentPlaceHolder : ContentPlaceHolder
	{
		public override Control FindControl(string id)
		{
			Control found = base.FindControl(id);
			bool flag = found == null;
			if (flag)
			{
				found = this.Page.FindControl(id);
			}
			bool flag2 = found == null;
			if (flag2)
			{
				found = this.FindControlExtend(id, this.Controls);
			}
			bool flag3 = found == null;
			if (flag3)
			{
				found = this.FindControlExtend(id, this.Page.Controls);
			}
			return found;
		}

		private Control FindControlExtend(string id, ControlCollection controls)
		{
			Control found = null;
			foreach (Control control in controls)
			{
				bool flag = control.ID == id;
				if (flag)
				{
					found = control;
					break;
				}
				bool flag2 = control.Controls.Count > 0;
				if (flag2)
				{
					found = this.FindControlExtend(id, control.Controls);
					bool flag3 = found != null;
					if (flag3)
					{
						break;
					}
				}
			}
			return found;
		}
	}
}
