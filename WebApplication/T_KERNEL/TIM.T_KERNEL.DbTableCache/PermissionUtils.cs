using System;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class PermissionUtils
	{
		internal static Permission GetObject(DataRow row)
		{
			return new Permission
			{
				Type = ((row["PERMISSION_TYPE"].ToString().Trim() == PermissionType.R.ToString()) ? PermissionType.R : PermissionType.U),
				Id = row["PERMISSION_ID"].ToString().Trim(),
				AmId = row["PERMISSION_AMID"].ToString().Trim().ToInt(),
				Insert = row["PERMISSION_INSERT"].ToString().Trim().ToBool(),
				Edit = row["PERMISSION_EDIT"].ToString().Trim().ToBool(),
				Delete = row["PERMISSION_DELETE"].ToString().Trim().ToBool(),
				Print = row["PERMISSION_PRINT"].ToString().Trim().ToBool(),
				View = row["PERMISSION_VIEW"].ToString().Trim().ToBool(),
				Design = row["PERMISSION_DESIGN"].ToString().Trim().ToBool(),
				CreaterId = row["CREATERID"].ToString().Trim(),
				Creater = row["CREATER"].ToString().Trim(),
				CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime(),
				ModifierId = row["MODIFIERID"].ToString().Trim(),
				Modifier = row["MODIFIER"].ToString().Trim(),
				ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime()
			};
		}

		public static Permission GetPermission(PermissionType rightType, string id, int amId)
		{
			Permission permission = new Permission();
			PermissionCache permissionCache = (PermissionCache)new PermissionCache().GetData();
			int index = permissionCache.dvPermissionBy_Type_Id_MdId.Find(new object[]
			{
				rightType.ToString(),
				id,
				amId
			});
			bool flag = index >= 0;
			if (flag)
			{
				Permission permission2 = new Permission();
				permission = PermissionUtils.GetObject(permissionCache.dvPermissionBy_Type_Id_MdId[index].Row);
			}
			return permission;
		}

		public static UserModulePermission GetUserModulePermission(string userId, int amId)
		{
			UserModulePermission modulePermission = new UserModulePermission();
			User user = UserUtils.GetUser(userId);
			bool flag = user == null;
			UserModulePermission result;
			if (flag)
			{
				result = modulePermission;
			}
			else
			{
				Module module = ModuleUtils.GetModule(amId);
				bool flag2 = module == null;
				if (flag2)
				{
					result = modulePermission;
				}
				else
				{
					modulePermission.UserId = user.UserId;
					modulePermission.AmId = module.MdId;
					bool flag3 = (user.Type == UserType.S && amId >= 102010001 && amId <= 102019999) || !ModuleUtils.IsRequirePermission(module);
					if (flag3)
					{
						modulePermission.View = (modulePermission.Insert = (modulePermission.Edit = (modulePermission.Delete = (modulePermission.Print = (modulePermission.Design = true)))));
						result = modulePermission;
					}
					else
					{
						Permission permission = PermissionUtils.GetPermission(PermissionType.U, userId, amId);
						bool flag4 = permission != null;
						if (flag4)
						{
							modulePermission.Insert = permission.Insert;
							modulePermission.Edit = permission.Edit;
							modulePermission.Delete = permission.Delete;
							modulePermission.Print = permission.Print;
							modulePermission.View = permission.View;
							modulePermission.Design = permission.Design;
							modulePermission.View = (modulePermission.Insert || modulePermission.Edit || modulePermission.Delete || modulePermission.Print || modulePermission.View || modulePermission.Design);
						}
						foreach (RoleUser roleUser in RoleUserUtils.GetRoles(userId))
						{
							Permission permission2 = PermissionUtils.GetPermission(PermissionType.R, roleUser.RoleId, amId);
							bool flag5 = permission2 != null;
							if (flag5)
							{
								modulePermission.Insert = (permission2.Insert || modulePermission.Insert);
								modulePermission.Edit = (permission2.Edit || modulePermission.Edit);
								modulePermission.Delete = (permission2.Delete || modulePermission.Delete);
								modulePermission.Print = (permission2.Print || modulePermission.Print);
								modulePermission.View = (permission2.View || modulePermission.View);
								modulePermission.Design = (permission2.Design || modulePermission.Design);
								modulePermission.View = (modulePermission.Insert || modulePermission.Edit || modulePermission.Delete || modulePermission.Print || modulePermission.View || modulePermission.Design);
							}
						}
						result = modulePermission;
					}
				}
			}
			return result;
		}
	}
}
