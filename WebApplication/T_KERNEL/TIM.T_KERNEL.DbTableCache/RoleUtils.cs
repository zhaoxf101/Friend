using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class RoleUtils
	{
		internal static Role GetObject(DataRow row)
		{
			return new Role
			{
				RoleId = row["ROLE_ROLEID"].ToString().Trim(),
				RoleName = row["ROLE_ROLENAME"].ToString().Trim(),
				Desc = row["ROLE_DESC"].ToString().Trim(),
				CreaterId = row["CREATERID"].ToString().Trim(),
				Creater = row["CREATER"].ToString().Trim(),
				CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime(),
				ModifierId = row["MODIFIERID"].ToString().Trim(),
				Modifier = row["MODIFIER"].ToString().Trim(),
				ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime()
			};
		}

		public static Role GetRole(string roleId)
		{
			Role role = null;
			RoleCache roleCache = (RoleCache)new RoleCache().GetData();
			int index = roleCache.dvRoleBy_RoleId.Find(roleId);
			bool flag = index >= 0;
			if (flag)
			{
				Role role2 = new Role();
				role = RoleUtils.GetObject(roleCache.dvRoleBy_RoleId[index].Row);
			}
			return role;
		}

		public static List<Role> GetRoleByName(string roleName)
		{
			List<Role> list = new List<Role>();
			DataRowView[] array = ((RoleCache)new RoleCache().GetData()).dvRoleBy_RoleName.FindRows(roleName);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				Role role = new Role();
				Role @object = RoleUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}

		public static List<Role> GetRoles()
		{
			List<Role> list = new List<Role>();
			foreach (DataRow row in ((RoleCache)new RoleCache().GetData()).dtRole.Rows)
			{
				Role role = new Role();
				Role @object = RoleUtils.GetObject(row);
				list.Add(@object);
			}
			return list;
		}
	}
}
