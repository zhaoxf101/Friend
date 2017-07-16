using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;
using TIM.T_KERNEL.Menu;

namespace TIM.T_KERNEL.DbTableCache
{
	public class FuncModelUtils
	{
		internal static FuncModel GetObject(DataRow row)
		{
			FuncModel funcModel = new FuncModel();
			funcModel.Id = row["FUNCMODEL_CHILDID"].ToString().Trim().ToInt();
			funcModel.Name = row["FUNCMODEL_NAME"].ToString().Trim();
			funcModel.Order = row["FUNCMODEL_ORDER"].ToString().Trim().ToInt();
			funcModel.FatherId = row["FUNCMODEL_FATHERID"].ToString().Trim().ToInt();
			string a = row["FUNCMODEL_TYPE"].ToString().Trim();
			if (!(a == "C"))
			{
				if (!(a == "A"))
				{
					funcModel.Type = ModuleType.A;
				}
				else
				{
					funcModel.Type = ModuleType.A;
				}
			}
			else
			{
				funcModel.Type = ModuleType.C;
			}
			return funcModel;
		}

		public static List<FuncModel> GetModelByFatherId(int fatherId)
		{
			List<FuncModel> list = new List<FuncModel>();
			DataRowView[] array = ((FuncModelCache)new FuncModelCache().GetData()).dvFuncModelBy_FatherId.FindRows(fatherId);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				FuncModel funcModel = new FuncModel();
				FuncModel @object = FuncModelUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}

		public static FuncModel GetModelById(int childId)
		{
			FuncModel funcModel = null;
			FuncModelCache funcModelCache = (FuncModelCache)new FuncModelCache().GetData();
			int index = funcModelCache.dvFuncModelBy_ChildId.Find(childId);
			bool flag = index >= 0;
			if (flag)
			{
				FuncModel funcModel2 = new FuncModel();
				funcModel = FuncModelUtils.GetObject(funcModelCache.dvFuncModelBy_ChildId[index].Row);
			}
			return funcModel;
		}

		public static TimMenu GetFuncModelMenu(int mdid = 0)
		{
			return FuncModelUtils.BuildFuncModel(mdid);
		}

		private static TimMenu BuildFuncModel(int mdId)
		{
			TimMenu timMenu = new TimMenu();
			bool flag = mdId == 0;
			if (flag)
			{
				timMenu.Id = mdId;
				timMenu.Name = "应用功能模型";
				timMenu.Order = 0;
				timMenu.FatherId = 0;
				timMenu.Type = ModuleType.C;
			}
			else
			{
				FuncModel modelById = FuncModelUtils.GetModelById(mdId);
				bool flag2 = modelById != null;
				if (flag2)
				{
					timMenu.Id = modelById.Id;
					timMenu.Name = modelById.Name;
					timMenu.Order = modelById.Order;
					timMenu.FatherId = modelById.FatherId;
					timMenu.Type = modelById.Type;
				}
			}
			foreach (FuncModel funcModel in FuncModelUtils.GetModelByFatherId(mdId))
			{
				TimMenu utoMenu2 = FuncModelUtils.BuildFuncModel(funcModel.Id);
				bool flag3 = utoMenu2 != null;
				if (flag3)
				{
					timMenu.Children.Add(utoMenu2);
				}
			}
			return timMenu;
		}

		public static string JsonFuncModel(string userId)
		{
			string str = string.Empty;
			return JsonConvert.SerializeObject(FuncModelUtils.FilterUserMenu(FuncModelUtils.GetFuncModelMenu(0), userId).Children);
		}

		public static string JsonFuncModel(int fMDID, string userId)
		{
			string str = string.Empty;
			return JsonConvert.SerializeObject(FuncModelUtils.FilterUserMenu(FuncModelUtils.GetFuncModelMenu(fMDID), userId).Children);
		}

		public static TimMenu FilterUserMenu(TimMenu menu, string userId)
		{
			TimMenu timMenu = new TimMenu();
			timMenu.Id = menu.Id;
			timMenu.Name = menu.Name;
			timMenu.Order = menu.Order;
			timMenu.FatherId = menu.FatherId;
			timMenu.Type = menu.Type;
			timMenu.Url = menu.Url;
			foreach (TimMenu menu2 in menu.Children)
			{
				bool flag = menu2.Type == ModuleType.C;
				if (flag)
				{
					TimMenu timMenu2 = FuncModelUtils.FilterUserMenu(menu2, userId);
					bool flag2 = timMenu2 != null && timMenu2.Children.Count > 0;
					if (flag2)
					{
						timMenu.Children.Add(timMenu2);
					}
				}
				else
				{
					UserModulePermission modulePermission = PermissionUtils.GetUserModulePermission(userId, menu2.Id);
					bool flag3 = modulePermission != null && modulePermission.View;
					if (flag3)
					{
						timMenu.Children.Add(menu2);
					}
				}
			}
			return timMenu;
		}
	}
}
