using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class DeptUtils
	{
		internal static Dept GetObject(DataRow row)
		{
			return new Dept
			{
				DeptId = row["DEPT_DEPTID"].ToString().Trim(),
				DeptName = row["DEPT_DEPTNAME"].ToString().Trim(),
				FzrId = row["DEPT_FZRID"].ToString().Trim(),
				Disabled = row["DEPT_DISABLED"].ToString().Trim().ToBool(),
				CreaterId = row["CREATERID"].ToString().Trim(),
				Creater = row["CREATER"].ToString().Trim(),
				CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime(),
				ModifierId = row["MODIFIERID"].ToString().Trim(),
				Modifier = row["MODIFIER"].ToString().Trim(),
				ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime()
			};
		}

		public static Dept GetDept(string deptId)
		{
			Dept dept = null;
			DeptCache deptCache = (DeptCache)new DeptCache().GetData();
			int index = deptCache.dvDeptBy_DeptId.Find(deptId);
			bool flag = index >= 0;
			if (flag)
			{
				Dept dept2 = new Dept();
				dept = DeptUtils.GetObject(deptCache.dvDeptBy_DeptId[index].Row);
			}
			return dept;
		}

		public static List<Dept> GetDeptByName(string deptName)
		{
			List<Dept> list = new List<Dept>();
			DataRowView[] array = ((DeptCache)new DeptCache().GetData()).dvDeptBy_DeptName.FindRows(deptName);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				Dept dept = new Dept();
				Dept @object = DeptUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}

		public static List<Dept> GetDepts()
		{
			List<Dept> list = new List<Dept>();
			foreach (DataRow row in ((DeptCache)new DeptCache().GetData()).dtDept.Rows)
			{
				Dept dept = new Dept();
				Dept @object = DeptUtils.GetObject(row);
				list.Add(@object);
			}
			return list;
		}
	}
}
