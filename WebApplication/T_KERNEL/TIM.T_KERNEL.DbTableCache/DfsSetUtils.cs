using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class DfsSetUtils
	{
		internal static DfsSet GetObject(DataRow row)
		{
			return new DfsSet
			{
				FsId = row["DFSSET_FSID"].ToString().Trim(),
				FsName = row["DFSSET_FSNAME"].ToString().Trim(),
				ServerSite = row["DFSSET_SERVER"].ToString().Trim(),
				PathLocation = row["DFSSET_PATH"].ToString().Trim(),
				CreaterId = row["CREATERID"].ToString().Trim(),
				Creater = row["CREATER"].ToString().Trim(),
				CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime(),
				ModifierId = row["MODIFIERID"].ToString().Trim(),
				Modifier = row["MODIFIER"].ToString().Trim(),
				ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime()
			};
		}

		public static DfsSet GetDfsSet(string fsId)
		{
			DfsSet dfsSet = null;
			DfsSetCache dfsSetCache = (DfsSetCache)new DfsSetCache().GetData();
			int index = dfsSetCache.dvDfsSetBy_FsId.Find(fsId);
			bool flag = index >= 0;
			if (flag)
			{
				DfsSet dfsSet2 = new DfsSet();
				dfsSet = DfsSetUtils.GetObject(dfsSetCache.dvDfsSetBy_FsId[index].Row);
			}
			return dfsSet;
		}

		public static DfsSet GetMainDfsSet()
		{
			return DfsSetUtils.GetDfsSet("DFSMAIN");
		}

		public static List<DfsSet> GetDfsSets()
		{
			List<DfsSet> list = new List<DfsSet>();
			foreach (DataRow row in ((DfsSetCache)new DfsSetCache().GetData()).dtDfsSet.Rows)
			{
				DfsSet dfsSet = new DfsSet();
				DfsSet @object = DfsSetUtils.GetObject(row);
				list.Add(@object);
			}
			return list;
		}
	}
}
