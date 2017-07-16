using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	internal class AspNetPagerIDConverter : ControlIDConverter
	{
		protected override bool FilterControl(Control control)
		{
			return control is TimPagingBar;
		}
	}
}
