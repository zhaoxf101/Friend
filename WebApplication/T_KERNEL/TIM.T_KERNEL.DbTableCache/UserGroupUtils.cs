using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class UserGroupUtils
	{
		internal static UserGroup GetObject(DataRow row)
		{
			return new UserGroup
			{
				UgId = row["USERGROUP_UGID"].ToString().Trim(),
				UgName = row["USERGROUP_UGNAME"].ToString().Trim(),
				Disabled = row["USERGROUP_DISABLED"].ToString().Trim().ToBool(),
				CreaterId = row["CREATERID"].ToString().Trim(),
				Creater = row["CREATER"].ToString().Trim(),
				CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime(),
				ModifierId = row["MODIFIERID"].ToString().Trim(),
				Modifier = row["MODIFIER"].ToString().Trim(),
				ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime()
			};
		}

		public static UserGroup GetUserGroup(string ugId)
		{
			UserGroup userGroup = null;
			UserGroupCache userGroupCache = (UserGroupCache)new UserGroupCache().GetData();
			int index = userGroupCache.dvUserGroupBy_UgId.Find(ugId);
			bool flag = index >= 0;
			if (flag)
			{
				UserGroup userGroup2 = new UserGroup();
				userGroup = UserGroupUtils.GetObject(userGroupCache.dvUserGroupBy_UgId[index].Row);
			}
			return userGroup;
		}

		public static List<UserGroup> GetUserGroupByName(string ugName)
		{
			List<UserGroup> list = new List<UserGroup>();
			DataRowView[] array = ((UserGroupCache)new UserGroupCache().GetData()).dvUserGroupBy_UgName.FindRows(ugName);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				UserGroup userGroup = new UserGroup();
				UserGroup @object = UserGroupUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}

		public static List<UserGroup> GetUserGroups()
		{
			List<UserGroup> list = new List<UserGroup>();
			foreach (DataRow row in ((UserGroupCache)new UserGroupCache().GetData()).dtUserGroup.Rows)
			{
				UserGroup userGroup = new UserGroup();
				UserGroup @object = UserGroupUtils.GetObject(row);
				list.Add(@object);
			}
			return list;
		}
	}
}
