using System;
using System.Collections.Specialized;

namespace TIM.T_WEBCTRL
{
	internal interface IMimePushHandler
	{
		void BeginPart(NameValueCollection headers);

		void EndPart(bool isLast);

		void PartData(ref byte[] data, int start, int length);
	}
}
