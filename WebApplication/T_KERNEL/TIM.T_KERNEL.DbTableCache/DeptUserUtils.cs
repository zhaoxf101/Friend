using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class DeptUserUtils
	{
		private static DeptUser GetObject(DataRow row)
		{
			return new DeptUser
			{
				DeptId = row["DEPTUSER_DEPTID"].ToString().Trim(),
				UserId = row["DEPTUSER_USERID"].ToString().Trim(),
				CreaterId = row["CREATERID"].ToString().Trim(),
				Creater = row["CREATER"].ToString().Trim(),
				CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime(),
				ModifierId = row["MODIFIERID"].ToString().Trim(),
				Modifier = row["MODIFIER"].ToString().Trim(),
				ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime()
			};
		}

		public static List<DeptUser> GetUsers(string deptId)
		{
			List<DeptUser> list = new List<DeptUser>();
			DataRowView[] array = ((DeptUserCache)new DeptUserCache().GetData()).dvDeptUserBy_DeptId.FindRows(deptId);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				DeptUser deptUser = new DeptUser();
				DeptUser @object = DeptUserUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}

		public static List<DeptUser> GetDepts(string userId)
		{
			List<DeptUser> list = new List<DeptUser>();
			DataRowView[] array = ((DeptUserCache)new DeptUserCache().GetData()).dvDeptUserBy_UserId.FindRows(userId);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				DeptUser deptUser = new DeptUser();
				DeptUser @object = DeptUserUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}
	}
}
