using System;
using System.IO;

namespace TIM.T_WEBCTRL
{
	public interface IUploadStreamProvider
	{
		Stream GetInputStream(UploadedFile file);

		Stream GetOutputStream(UploadedFile file);

		void RemoveOutput(UploadedFile file);
	}
}
