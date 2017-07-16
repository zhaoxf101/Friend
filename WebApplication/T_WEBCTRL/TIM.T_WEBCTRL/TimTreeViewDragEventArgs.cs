using System;

namespace TIM.T_WEBCTRL
{
	public sealed class TimTreeViewDragEventArgs : EventArgs
	{
		private TimTreeViewDropMode _DropDownMode = TimTreeViewDropMode.AppendLast;

		private TimTreeViewNode _Target;

		private TimTreeViewNode _TargetBranch;

		private string _TargetTreeID = string.Empty;

		private TimTreeViewNode _Source;

		private TimTreeViewNode _SourceBranch;

		private string _SourceTreeID = string.Empty;

		private bool _UpdateSource = false;

		private string _ErrorMessage = string.Empty;

		private bool _Result = true;

		public TimTreeViewDropMode DropDownMode
		{
			get
			{
				return this._DropDownMode;
			}
		}

		public TimTreeViewNode Target
		{
			get
			{
				return this._Target;
			}
		}

		public string TargetTreeID
		{
			get
			{
				return this._TargetTreeID;
			}
		}

		public TimTreeViewNode Source
		{
			get
			{
				return this._Source;
			}
		}

		public string SourceTreeID
		{
			get
			{
				return this._SourceTreeID;
			}
		}

		public bool UpdateSource
		{
			get
			{
				return this._UpdateSource;
			}
			set
			{
				this._UpdateSource = value;
			}
		}

		public string ErrorMessage
		{
			get
			{
				return this._ErrorMessage;
			}
			set
			{
				this._ErrorMessage = value;
			}
		}

		public bool Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
			}
		}

		public TimTreeViewDragEventArgs(TimTreeViewNode target, TimTreeViewNode source)
		{
			this._Target = target;
			this._Source = source;
		}

		internal void initArgs(TimTreeViewNode targetBranch, TimTreeViewNode sourceBranch, string targetId, string SourceId, TimTreeViewDropMode mode)
		{
			this._TargetBranch = targetBranch;
			this._SourceBranch = sourceBranch;
			this._TargetTreeID = targetId;
			this._SourceTreeID = SourceId;
			this._DropDownMode = mode;
		}
	}
}
