using System;
using System.Collections;
using System.Reflection;
using System.Threading;

namespace TIM.T_WEBCTRL
{
	internal static class ConfigurationHashThread
	{
		private static ReaderWriterLock _rwlock = new ReaderWriterLock();

		private static Hashtable _configurationHashTable = new Hashtable();

		internal static object CreateConfigurationHashObject(string configurationtext, string configurationHashKey, params object[] objectarray)
		{
			bool flag = string.IsNullOrEmpty(configurationtext);
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ConfigurationHashThread._rwlock.AcquireReaderLock(1000);
				string str = configurationtext;
				bool flag2 = !string.IsNullOrEmpty(configurationHashKey);
				if (flag2)
				{
					str = str + "#" + configurationHashKey;
				}
				object obj2 = ConfigurationHashThread._configurationHashTable[str];
				ConfigurationHashThread._rwlock.ReleaseReaderLock();
				bool flag3 = obj2 == null;
				if (flag3)
				{
					obj2 = ConfigurationHashThread.CreateInstance(configurationtext, objectarray);
					bool flag4 = obj2 != null;
					if (flag4)
					{
						ConfigurationHashThread._rwlock.AcquireWriterLock(1000);
						ConfigurationHashThread._configurationHashTable[str] = obj2;
						ConfigurationHashThread._rwlock.ReleaseWriterLock();
					}
				}
				result = obj2;
			}
			return result;
		}

		internal static object CreateInstance(string configurationtext, object[] objectarray)
		{
			return ConfigurationHashThread.CreateInstance(configurationtext, objectarray, BindingFlags.Default);
		}

		internal static object CreateInstance(string configurationtext, object[] objectarray, BindingFlags xebf45bdcaa1fd1e1)
		{
			object obj2 = null;
			bool flag = !string.IsNullOrEmpty(configurationtext);
			object result;
			if (flag)
			{
				string[] strArray = configurationtext.Split(new char[]
				{
					','
				}, 2);
				Assembly assembly = null;
				try
				{
					bool flag2 = strArray.Length == 1;
					if (flag2)
					{
						assembly = Assembly.Load("App_Code");
					}
					else
					{
						assembly = Assembly.Load(strArray[1].Trim());
					}
				}
				catch
				{
				}
				bool flag3 = assembly == null;
				if (flag3)
				{
					result = obj2;
					return result;
				}
				try
				{
					obj2 = assembly.CreateInstance(strArray[0].Trim(), false, xebf45bdcaa1fd1e1, null, objectarray, null, null);
				}
				catch (TargetInvocationException exception)
				{
					throw exception.InnerException;
				}
				catch
				{
					try
					{
						obj2 = assembly.CreateInstance(strArray[0].Trim(), false, xebf45bdcaa1fd1e1, null, null, null, null);
					}
					catch
					{
					}
				}
			}
			result = obj2;
			return result;
		}

		internal static object CreateConfigurationHashObject(string configurationtext, params object[] objectarray)
		{
			return ConfigurationHashThread.CreateConfigurationHashObject(configurationtext, null, objectarray);
		}
	}
}
