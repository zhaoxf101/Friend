using System;
using System.Web;

namespace TIM.T_WEBCTRL
{
	public sealed class ApplicationStateStatusManager : IStatusManager
	{
		public ApplicationStateStatusManager(NameValueConfigurationSection configuration)
		{
		}

		public UploadStatus GetUploadStatus(string uploadId)
		{
			return HttpContext.Current.Application["_UploadStatus_" + uploadId] as UploadStatus;
		}

		public void RemoveStaleStatus(int staleMinutes)
		{
		}

		public void RemoveStatus(string uploadId)
		{
			HttpContext.Current.Application.Remove("_UploadStatus_" + uploadId);
		}

		public void StatusChanged(UploadStatus status)
		{
		}

		public void UploadStarted(UploadStatus status)
		{
			HttpContext.Current.Application["_UploadStatus_" + status.UploadId] = status;
		}
	}
}
