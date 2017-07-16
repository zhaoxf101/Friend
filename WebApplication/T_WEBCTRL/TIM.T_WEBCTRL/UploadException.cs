using System;

namespace TIM.T_WEBCTRL
{
	public sealed class UploadException : Exception
	{
		private UploadTerminationReason _reason;

		public UploadTerminationReason Reason
		{
			get
			{
				return this._reason;
			}
		}

		internal UploadException(UploadTerminationReason reason)
		{
			this._reason = reason;
		}

		public UploadException(string message) : this(message, UploadTerminationReason.Custom)
		{
		}

		internal UploadException(string message, UploadTerminationReason reason) : base(message)
		{
			this._reason = reason;
		}
	}
}
