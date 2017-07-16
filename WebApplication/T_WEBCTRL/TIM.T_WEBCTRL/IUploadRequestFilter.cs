using System;
using System.Web;

namespace TIM.T_WEBCTRL
{
	public interface IUploadRequestFilter
	{
		bool ShouldHandleRequest(HttpRequest request);
	}
}
