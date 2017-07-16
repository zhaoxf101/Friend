using System;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class PmReferUtils
	{
		internal static PmRefer GetObject(DataRow row)
		{
			return new PmRefer
			{
				PmId = row["PMREFER_PMID"].ToString().Trim(),
				MdId = row["PMREFER_MDID"].ToString().Trim().ToInt(),
				ComId = row["PMREFER_COMID"].ToString().Trim()
			};
		}

		public static PmRefer GetPmRefer(string pmId, int mdId)
		{
			PmRefer pmRefer = null;
			PmReferCache pmReferCache = (PmReferCache)new PmReferCache().GetData();
			int index = pmReferCache.dvPmReferBy_PmId_MdId.Find(new object[]
			{
				pmId,
				mdId
			});
			bool flag = index >= 0;
			if (flag)
			{
				PmRefer pmRefer2 = new PmRefer();
				pmRefer = PmReferUtils.GetObject(pmReferCache.dvPmReferBy_PmId_MdId[index].Row);
			}
			return pmRefer;
		}
	}
}
