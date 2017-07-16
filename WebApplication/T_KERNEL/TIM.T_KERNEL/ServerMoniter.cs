using System;
using System.Collections;
using System.Diagnostics;
using System.Management;
using System.Text;
using System.Threading;
using System.Web;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL
{
	public class ServerMoniter
	{
		private static int GetPhisicalMemory()
		{
			ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher
			{
				Query = new SelectQuery("Win32_PhysicalMemory ", "", new string[]
				{
					"Capacity"
				})
			}.Get().GetEnumerator();
			long num = 0L;
			int result;
			while (enumerator.MoveNext())
			{
				ManagementBaseObject current = enumerator.Current;
				bool flag = current.Properties["Capacity"].Value != null;
				if (flag)
				{
					try
					{
						num += long.Parse(current.Properties["Capacity"].Value.ToString());
					}
					catch
					{
						result = 0;
						return result;
					}
					continue;
				}
			}
			result = (int)(num / 1024L / 1024L / 1024L);
			return result;
		}

		private static int GetFreePhysicalMemory()
		{
			long num = 0L;
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementClass("Win32_OperatingSystem").GetInstances().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ManagementObject managementObject = (ManagementObject)enumerator.Current;
					bool flag = managementObject["FreePhysicalMemory"] != null;
					if (flag)
					{
						num = 1024L * long.Parse(managementObject["FreePhysicalMemory"].ToString());
					}
				}
			}
			return (int)(num / 1024L / 1024L);
		}

		public static string RuntimeInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder("<h3>服务监测数据</h3>");
			stringBuilder2.Append("<table>");
			int workerThreads = 0;
			int completionPortThreads = 0;
			ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
			stringBuilder2.Append("<tr><td><b>可用辅助线程的数目</b></td><td>");
			stringBuilder2.Append(workerThreads);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>可用异步 I/O 线程的数目</b></td><td>");
			stringBuilder2.Append(completionPortThreads);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>当前平台标识符和版本号</b></td><td>");
			stringBuilder2.Append(Environment.OSVersion.ToString());
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>当前操作系统是否为 64 位操作系统</b></td><td>");
			stringBuilder2.Append(Environment.Is64BitOperatingSystem.ToYesOrNo());
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>当前进程是否为 64 位进程</b></td><td>");
			stringBuilder2.Append(Environment.Is64BitProcess.ToYesOrNo());
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>处理器数</b></td><td>");
			stringBuilder2.Append(Environment.ProcessorCount.ToString());
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>物理内存</b></td><td>");
			stringBuilder2.Append(ServerMoniter.GetPhisicalMemory().ToString() + "GB");
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>可用内存</b></td><td>");
			stringBuilder2.Append(ServerMoniter.GetFreePhysicalMemory().ToString() + "MB");
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>系统运行分钟数</b></td><td>");
			stringBuilder2.Append((Environment.TickCount / 60000).ToString());
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>NetBIOS 名称</b></td><td>");
			stringBuilder2.Append(Environment.MachineName);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>IIS关联的网络域名</b></td><td>");
			stringBuilder2.Append(Environment.UserDomainName);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>IIS用户名</b></td><td>");
			stringBuilder2.Append(Environment.UserName);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>公共语言运行时版本</b></td><td>");
			stringBuilder2.Append(Environment.Version);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>IIS 版本</b></td><td>");
			stringBuilder2.Append(HttpRuntime.IISVersion.ToString());
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>托管管道模式</b></td><td>");
			stringBuilder2.Append(HttpRuntime.UsingIntegratedPipeline ? "集成" : "经典");
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>AppDomainAppId</b></td><td>");
			stringBuilder2.Append(HttpRuntime.AppDomainAppId);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>AppDomainAppPath</b></td><td>");
			stringBuilder2.Append(HttpRuntime.AppDomainAppPath);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>AppDomainAppVirtualPath</b></td><td>");
			stringBuilder2.Append(HttpRuntime.AppDomainAppVirtualPath);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>AppDomainId</b></td><td>");
			stringBuilder2.Append(HttpRuntime.AppDomainId);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>AspInstallDirectory</b></td><td>");
			stringBuilder2.Append(HttpRuntime.AspInstallDirectory);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>BinDirectory</b></td><td>");
			stringBuilder2.Append(HttpRuntime.BinDirectory);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>ClrInstallDirectory</b></td><td>");
			stringBuilder2.Append(HttpRuntime.ClrInstallDirectory);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>Temp ASP.NET Directory</b></td><td>");
			stringBuilder2.Append(HttpRuntime.CodegenDir);
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("<tr><td><b>Machine Config Directory</b></td><td>");
			stringBuilder2.Append(HttpRuntime.MachineConfigurationDirectory);
			stringBuilder2.Append("</td></tr>");
			double num = (double)Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0;
			stringBuilder2.Append("<tr><td><b>内存占用</b></td><td>");
			stringBuilder2.Append(num.RoundX(2).ToString() + "MB");
			stringBuilder2.Append("</td></tr>");
			stringBuilder2.Append("</td></tr></table>");
			HttpApplication applicationInstance = HttpContext.Current.ApplicationInstance;
			string[] allKeys = applicationInstance.Modules.AllKeys;
			for (int i = 0; i < allKeys.Length; i++)
			{
				string index = allKeys[i];
				stringBuilder2.Append(applicationInstance.Modules[index].ToString() + "<br/>");
			}
			stringBuilder2.Append("<h4>环境变量</h4>");
			foreach (DictionaryEntry dictionaryEntry in Environment.GetEnvironmentVariables())
			{
				stringBuilder2.Append(string.Format("{0} = {1}", dictionaryEntry.Key, dictionaryEntry.Value) + "<br/>");
			}
			return stringBuilder2.ToString();
		}
	}
}
