using System;
using System.Web.UI.Design;

namespace TIM.T_WEBCTRL
{
	internal class TimTreeViewDesigner : ControlDesigner
	{
		public override string GetDesignTimeHtml()
		{
			TimTreeView control = (TimTreeView)base.Component;
			string builder = string.Format("<div style=\"width:{0}; height:{1}; background-color:red\"  id=\"{2}\">TimTreeView树型控件</div>", control.Width, control.Height, control.ID);
			return base.CreatePlaceHolderDesignTimeHtml(builder);
		}
	}
}
