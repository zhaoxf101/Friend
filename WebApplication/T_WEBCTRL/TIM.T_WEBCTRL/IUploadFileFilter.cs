using System;

namespace TIM.T_WEBCTRL
{
	public interface IUploadFileFilter
	{
		bool ShouldHandleFile(UploadedFile file);
	}
}
