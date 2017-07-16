using System;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class SystemInfoUtils
	{
		internal static SystemInfo GetObject(DataRow row)
		{
			return new SystemInfo
			{
				Id = row["SYSTEM_ID"].ToString().Trim(),
				Name = row["SYSTEM_NAME"].ToString().Trim(),
				PswLength = row["SYSTEM_PSWLENGTH"].ToString().Trim().ToInt(),
				PswDays = row["SYSTEM_PSWDAYS"].ToString().Trim().ToInt(),
				PswWarnDays = row["SYSTEM_PSWWARNDAYS"].ToString().Trim().ToInt(),
				PswNew = (row["SYSTEM_PSWNEW"].ToString().Trim() == "Y"),
				PswHistoryCount = row["SYSTEM_PSWHISTORYCOUNT"].ToString().Trim().ToInt(),
				LimitedDate = row["SYSTEM_LIMITEDDATE"].ToString().Trim().ToDate(),
				IsValid = true,
				CreaterId = row["CREATERID"].ToString().Trim(),
				Creater = row["CREATER"].ToString().Trim(),
				CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime(),
				ModifierId = row["MODIFIERID"].ToString().Trim(),
				Modifier = row["MODIFIER"].ToString().Trim(),
				ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime()
			};
		}

		public static SystemInfo GetSystemInfo(string systemId)
		{
			SystemInfo systemInfo = null;
			SystemInfoCache systemInfoCache = (SystemInfoCache)new SystemInfoCache().GetData();
			int index = systemInfoCache.dvSystemBy_Id.Find(systemId);
			bool flag = index >= 0;
			if (flag)
			{
				SystemInfo systemInfo2 = new SystemInfo();
				systemInfo = SystemInfoUtils.GetObject(systemInfoCache.dvSystemBy_Id[index].Row);
			}
			return systemInfo;
		}

		public static SystemInfo GetSystemInfo()
		{
			return SystemInfoUtils.GetSystemInfo("0000000001");
		}
	}
}
