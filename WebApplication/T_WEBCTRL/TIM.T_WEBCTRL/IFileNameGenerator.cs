using System;

namespace TIM.T_WEBCTRL
{
	public interface IFileNameGenerator
	{
		string GenerateFileName(UploadedFile file);
	}
}
