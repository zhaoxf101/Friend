using System;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class MePageUtils
	{
		internal static MePage GetObject(DataRow row)
		{
			MePage mePage = new MePage();
			mePage.MdId = row["MDID"].ToString().Trim().ToInt();
			mePage.ComId = row["COMID"].ToString().Trim();
			mePage.WfbId = row["WFBID"].ToString().Trim();
			mePage.WfId = row["WFID"].ToString().Trim();
			mePage.PageUrl = row["PAGEURL"].ToString().Trim();
			ModuleType result;
			Enum.TryParse<ModuleType>(row["MEPAGE_TYPE"].ToString().Trim(), out result);
			mePage.Type = result;
			return mePage;
		}

		public static MePage GetMePage(int mdId, string wfId)
		{
			MePage mePage = null;
			MePageCache mePageCache = (MePageCache)new MePageCache().GetData();
			int index = mePageCache.dvMePageBy_MdId_WfbId.Find(new object[]
			{
				mdId,
				wfId
			});
			bool flag = index >= 0;
			if (flag)
			{
				MePage mePage2 = new MePage();
				mePage = MePageUtils.GetObject(mePageCache.dvMePageBy_MdId_WfbId[index].Row);
			}
			return mePage;
		}

		public static MePage GetMePage(int mdId)
		{
			MePage mePage = null;
			MePageCache mePageCache = (MePageCache)new MePageCache().GetData();
			int index = mePageCache.dvMePageBy_MdId.Find(mdId);
			bool flag = index >= 0;
			if (flag)
			{
				MePage mePage2 = new MePage();
				mePage = MePageUtils.GetObject(mePageCache.dvMePageBy_MdId[index].Row);
			}
			return mePage;
		}
	}
}
