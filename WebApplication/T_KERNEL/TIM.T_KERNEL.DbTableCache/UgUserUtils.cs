using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class UgUserUtils
	{
		private static UgUser GetObject(DataRow row)
		{
			return new UgUser
			{
				UgId = row["UGUSER_UGID"].ToString().Trim(),
				UserId = row["UGUSER_USERID"].ToString().Trim(),
				CreaterId = row["CREATERID"].ToString().Trim(),
				Creater = row["CREATER"].ToString().Trim(),
				CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime(),
				ModifierId = row["MODIFIERID"].ToString().Trim(),
				Modifier = row["MODIFIER"].ToString().Trim(),
				ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime()
			};
		}

		public static List<UgUser> GetUsers(string ugId)
		{
			List<UgUser> list = new List<UgUser>();
			DataRowView[] array = ((UgUserCache)new UgUserCache().GetData()).dvUgUserBy_UgId.FindRows(ugId);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				UgUser ugUser = new UgUser();
				UgUser @object = UgUserUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}

		public static List<UgUser> GetUserGroups(string userId)
		{
			List<UgUser> list = new List<UgUser>();
			DataRowView[] array = ((UgUserCache)new UgUserCache().GetData()).dvUgUserBy_UserId.FindRows(userId);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				UgUser ugUser = new UgUser();
				UgUser @object = UgUserUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}
	}
}
