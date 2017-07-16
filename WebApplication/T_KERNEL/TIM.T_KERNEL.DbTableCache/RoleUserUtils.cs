using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class RoleUserUtils
	{
		private static RoleUser GetObject(DataRow row)
		{
			return new RoleUser
			{
				RoleId = row["ROLEUSER_ROLEID"].ToString().Trim(),
				UserId = row["ROLEUSER_USERID"].ToString().Trim(),
				CreaterId = row["CREATERID"].ToString().Trim(),
				Creater = row["CREATER"].ToString().Trim(),
				CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime(),
				ModifierId = row["MODIFIERID"].ToString().Trim(),
				Modifier = row["MODIFIER"].ToString().Trim(),
				ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime()
			};
		}

		public static List<RoleUser> GetUsers(string roleId)
		{
			List<RoleUser> list = new List<RoleUser>();
			DataRowView[] array = ((RoleUserCache)new RoleUserCache().GetData()).dvRoleUserBy_RoleId.FindRows(roleId);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				RoleUser roleUser = new RoleUser();
				RoleUser @object = RoleUserUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}

		public static List<RoleUser> GetRoles(string userId)
		{
			List<RoleUser> list = new List<RoleUser>();
			DataRowView[] array = ((RoleUserCache)new RoleUserCache().GetData()).dvRoleUserBy_UserId.FindRows(userId);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				RoleUser roleUser = new RoleUser();
				RoleUser @object = RoleUserUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}
	}
}
