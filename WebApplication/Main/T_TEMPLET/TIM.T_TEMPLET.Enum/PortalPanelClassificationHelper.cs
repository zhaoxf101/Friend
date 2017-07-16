using System;

namespace TIM.T_TEMPLET.Enum
{
	public static class PortalPanelClassificationHelper
	{
		public static PortalPanelClassification ToPortalPanelClassification(this string value)
		{
			PortalPanelClassification result = PortalPanelClassification.G;
			string a = value.ToUpper();
			if (!(a == "G"))
			{
				if (a == "C")
				{
					result = PortalPanelClassification.C;
				}
			}
			else
			{
				result = PortalPanelClassification.G;
			}
			return result;
		}
	}
}
