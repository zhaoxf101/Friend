using System;

namespace TIM.T_WEBCTRL
{
	[AttributeUsage(AttributeTargets.Property)]
	public class CKSerializable : Attribute
	{
		public string Name = string.Empty;

		public bool RemoveEnters = false;

		public bool ForceAddToJSON = false;

		public bool IsObject = false;
	}
}
