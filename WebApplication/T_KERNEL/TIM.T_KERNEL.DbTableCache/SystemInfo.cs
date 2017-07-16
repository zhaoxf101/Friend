using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class SystemInfo : DbEntity
	{
		public string Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int PswLength
		{
			get;
			set;
		}

		public int PswDays
		{
			get;
			set;
		}

		public int PswWarnDays
		{
			get;
			set;
		}

		public bool PswNew
		{
			get;
			set;
		}

		public int PswHistoryCount
		{
			get;
			set;
		}

		public DateTime LimitedDate
		{
			get;
			set;
		}

		public bool IsValid
		{
			get;
			set;
		}
	}
}
