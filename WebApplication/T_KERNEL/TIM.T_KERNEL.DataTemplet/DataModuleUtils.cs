using System;
using System.Collections.Concurrent;
using System.IO;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_KERNEL.Log;

namespace TIM.T_KERNEL.DataTemplet
{
	internal class DataModuleUtils
	{
		private static ConcurrentDictionary<int, IDataModule> m_dicMdId_DataModule = new ConcurrentDictionary<int, IDataModule>();

		private static ConcurrentDictionary<string, IDataModule> m_dicMdName_DataModule = new ConcurrentDictionary<string, IDataModule>();

		public static void Init(TIM.T_KERNEL.DbTableCache.DllComponent component)
		{
			foreach (DllModule dllModule in component.Modules)
			{
				bool flag = dllModule.Type == ModuleType.D && dllModule.CallObjectType != null;
				if (flag)
				{
					IDataModule dataModule = (IDataModule)Activator.CreateInstance(dllModule.CallObjectType);
					bool flag2 = !DataModuleUtils.m_dicMdId_DataModule.ContainsKey(dllModule.MdId);
					if (flag2)
					{
						DataModuleUtils.m_dicMdId_DataModule.TryAdd(dllModule.MdId, dataModule);
					}
					bool flag3 = !DataModuleUtils.m_dicMdName_DataModule.ContainsKey(dllModule.MdName);
					if (flag3)
					{
						DataModuleUtils.m_dicMdName_DataModule.TryAdd(dllModule.MdName, dataModule);
					}
				}
			}
		}

		public static void LosedInit()
		{
			FileInfo[] files = new DirectoryInfo(AppRuntime.AppRootPath + "\\bin").GetFiles("T_*.dll");
			for (int i = 0; i < files.Length; i++)
			{
				FileSystemInfo fileSystemInfo = files[i];
				TIM.T_KERNEL.DbTableCache.DllComponent instance = new TIM.T_KERNEL.DbTableCache.DllComponent(fileSystemInfo.FullName).Instance;
				bool flag = instance != null && !string.IsNullOrWhiteSpace(instance.ComId) && !string.IsNullOrWhiteSpace(instance.ComName);
				if (flag)
				{
					DataModuleUtils.Init(instance);
				}
			}
		}

		internal static string GS(int mdId, string param)
		{
			string str = "";
			IDataModule dataModule = null;
			DataModuleUtils.m_dicMdId_DataModule.TryGetValue(mdId, out dataModule);
			bool flag = dataModule != null;
			if (flag)
			{
				str = dataModule.GS(param);
			}
			return str;
		}

		internal static string GS(string mdName, string param)
		{
			string str = "";
			IDataModule dataModule = null;
			DataModuleUtils.m_dicMdName_DataModule.TryGetValue(mdName, out dataModule);
			bool flag = dataModule != null;
			if (flag)
			{
				str = dataModule.GS(param);
			}
			return str;
		}

		internal static double GN(int mdId, string param)
		{
			double num = 0.0;
			IDataModule dataModule = null;
			DataModuleUtils.m_dicMdId_DataModule.TryGetValue(mdId, out dataModule);
			bool flag = dataModule != null;
			if (flag)
			{
				num = dataModule.GN(param);
			}
			else
			{
				AppEventLog.Debug(string.Format("不存在[{0}]数据模块", mdId));
			}
			return num;
		}

		internal static double GN(string mdName, string param)
		{
			double num = 0.0;
			IDataModule dataModule = null;
			DataModuleUtils.m_dicMdName_DataModule.TryGetValue(mdName, out dataModule);
			bool flag = dataModule != null;
			if (flag)
			{
				num = dataModule.GN(param);
			}
			return num;
		}
	}
}
