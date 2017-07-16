using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class User : DbEntity
	{
		public string UserId
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public string Abbr
		{
			get;
			set;
		}

		public UserType Type
		{
			get;
			set;
		}

		public string Tel
		{
			get;
			set;
		}

		public string Mobile
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public bool Disabled
		{
			get;
			set;
		}

		public DateTime PasswordSetTime
		{
			get;
			set;
		}

		public string Mac
		{
			get;
			set;
		}
	}
}
