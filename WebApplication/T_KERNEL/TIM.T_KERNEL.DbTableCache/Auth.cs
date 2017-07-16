using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class Auth : DbEntity
	{
		public string SessionId
		{
			get;
			set;
		}

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

		public DateTime LoginTime
		{
			get;
			set;
		}

		public LogicSessionType LoginType
		{
			get;
			set;
		}

		public string ClientIp
		{
			get;
			set;
		}

		public string ClientName
		{
			get;
			set;
		}

		public string DbId
		{
			get;
			set;
		}

		public DateTime LastRefresh
		{
			get;
			set;
		}

		public DateTime LastRequest
		{
			get;
			set;
		}

		public DateTime UpdateTime
		{
			get;
			set;
		}

		public string ExInfo
		{
			get;
			set;
		}
	}
}
