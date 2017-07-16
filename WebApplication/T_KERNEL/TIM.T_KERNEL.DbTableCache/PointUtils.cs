using System;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class PointUtils
	{
		internal static Point GetObject(DataRow row)
		{
			return new Point
			{
				PointId = row["SSCDB_TAGNAME"].ToString().Trim(),
				PointName = row["SSCDB_TAGBQMC"].ToString().Trim(),
				Min = row["SSCDB_MIN"].ToString().ToFloat(),
				Max = row["SSCDB_MAX"].ToString().ToFloat(),
				UpperLimit = row["SSCDB_EMAX"].ToString().ToFloat(),
				LowerLimit = row["SSCDB_EMIN"].ToString().ToFloat()
			};
		}

		public static Point GetPoint(string pointId)
		{
			Point point = null;
			PointCache pointCache = (PointCache)new PointCache().GetData();
			int index = pointCache.dvPointBy_PointId.Find(pointId);
			bool flag = index >= 0;
			if (flag)
			{
				Point point2 = new Point();
				point = PointUtils.GetObject(pointCache.dvPointBy_PointId[index].Row);
			}
			return point;
		}
	}
}
