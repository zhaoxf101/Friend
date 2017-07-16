using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class Point : DbEntity
	{
		public string PointId
		{
			get;
			set;
		}

		public string PointName
		{
			get;
			set;
		}

		public string PointType
		{
			get;
			set;
		}

		public float Min
		{
			get;
			set;
		}

		public float Max
		{
			get;
			set;
		}

		public float UpperLimit
		{
			get;
			set;
		}

		public float LowerLimit
		{
			get;
			set;
		}
	}
}
