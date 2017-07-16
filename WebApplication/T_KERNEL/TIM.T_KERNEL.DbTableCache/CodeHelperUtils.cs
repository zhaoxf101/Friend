using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class CodeHelperUtils
	{
		internal static CodeHelper GetObject(DataRow row)
		{
			return new CodeHelper
			{
				CodeId = row["CODEHELPER_CODEID"].ToString().Trim(),
				CodeName = row["CODEHELPER_CODENAME"].ToString().Trim(),
				MdId = row["CODEHELPER_MDID"].ToString().Trim().ToInt(),
				CreaterId = row["CREATERID"].ToString().Trim(),
				Creater = row["CREATER"].ToString().Trim(),
				CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime(),
				ModifierId = row["MODIFIERID"].ToString().Trim(),
				Modifier = row["MODIFIER"].ToString().Trim(),
				ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime()
			};
		}

		public static CodeHelper GetCodeHelper(string codeId)
		{
			CodeHelper codeHelper = null;
			CodeHelperCache codeHelperCache = (CodeHelperCache)new CodeHelperCache().GetData();
			int index = codeHelperCache.dvCodeHelperBy_CodeId.Find(codeId);
			bool flag = index >= 0;
			if (flag)
			{
				CodeHelper codeHelper2 = new CodeHelper();
				codeHelper = CodeHelperUtils.GetObject(codeHelperCache.dvCodeHelperBy_CodeId[index].Row);
			}
			return codeHelper;
		}

		public static List<CodeHelper> GetCodeHelpers()
		{
			List<CodeHelper> list = new List<CodeHelper>();
			foreach (DataRow row in ((CodeHelperCache)new CodeHelperCache().GetData()).dtCodeHelper.Rows)
			{
				CodeHelper codeHelper = new CodeHelper();
				CodeHelper @object = CodeHelperUtils.GetObject(row);
				list.Add(@object);
			}
			return list;
		}
	}
}
