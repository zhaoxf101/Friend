using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ToolboxData("<{0}:TimCalendar runat=server></{0}:TimCalendar>")]
	public class TimCalendar : Calendar
	{
		public TimCalendar()
		{
			base.DayNameFormat = DayNameFormat.Shortest;
			base.ShowGridLines = true;
			base.SelectionMode = CalendarSelectionMode.None;
			base.ShowTitle = false;
		}
	}
}
