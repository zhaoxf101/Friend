using System;

namespace TIM.T_WEBCTRL
{
	public enum UploadTerminationReason
	{
		NotTerminated,
		Error,
		MaxRequestLengthExceeded,
		Disconnected,
		Custom,
		FileFilter
	}
}
