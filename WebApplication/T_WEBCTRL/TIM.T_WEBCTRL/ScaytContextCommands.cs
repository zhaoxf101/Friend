using System;

namespace TIM.T_WEBCTRL
{
	[Flags]
	public enum ScaytContextCommands
	{
		Off = 0,
		All = 1,
		Ignore = 2,
		Ignoreall = 4,
		Add = 8
	}
}
