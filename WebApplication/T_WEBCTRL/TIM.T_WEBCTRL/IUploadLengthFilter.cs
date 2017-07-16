using System;
using System.Web;

namespace TIM.T_WEBCTRL
{
	public interface IUploadLengthFilter
	{
		bool IsOversizedRequest(HttpRequest request);
	}
}
