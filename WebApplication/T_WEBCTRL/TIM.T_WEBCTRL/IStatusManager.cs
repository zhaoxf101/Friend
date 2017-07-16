using System;

namespace TIM.T_WEBCTRL
{
	public interface IStatusManager
	{
		UploadStatus GetUploadStatus(string uploadId);

		void RemoveStaleStatus(int staleMinutes);

		void RemoveStatus(string uploadId);

		void StatusChanged(UploadStatus status);

		void UploadStarted(UploadStatus status);
	}
}
