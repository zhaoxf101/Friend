using System;
using System.Management;
using System.Runtime.InteropServices;

namespace TIM.T_KERNEL.Security
{
	internal class ServerHardwareInfo
	{
		[DllImport("kernel32.dll")]
		public static extern int GetVolumeInformation(string lpRootPathName, string lpVolumeNameBuffer, int nVolumeNameSize, ref int lpVolumeSerialNumber, int lpMaximumComponentLength, int lpFileSystemFlags, string lpFileSystemNameBuffer, int nFileSystemNameSize);

		public static string GetHardDiskVol()
		{
			int retVal = 0;
			int a = 0;
			int b = 0;
			string str = null;
			int i = ServerHardwareInfo.GetVolumeInformation("c:\\", str, 256, ref retVal, a, b, null, 256);
			return retVal.ToString();
		}

		public static string GetHardDiskID()
		{
			string result;
			try
			{
				ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
				string strHardDiskID = null;
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = searcher.Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject mo = (ManagementObject)enumerator.Current;
						strHardDiskID = mo["SerialNumber"].ToString().Trim();
					}
				}
				result = strHardDiskID;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static string GetNetCardMAC()
		{
			string result;
			try
			{
				ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
				ManagementObjectCollection moc = mc.GetInstances();
				string str = "";
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = moc.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject mo = (ManagementObject)enumerator.Current;
						bool flag = (bool)mo["IPEnabled"];
						if (flag)
						{
							str += mo["MacAddress"].ToString();
						}
					}
				}
				result = str;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static string GetCpuID()
		{
			string result;
			try
			{
				ManagementClass mc = new ManagementClass("Win32_Processor");
				ManagementObjectCollection moc = mc.GetInstances();
				string strCpuID = null;
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = moc.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject mo = (ManagementObject)enumerator.Current;
						strCpuID = mo.Properties["ProcessorId"].Value.ToString();
					}
				}
				result = strCpuID;
			}
			catch
			{
				result = "";
			}
			return result;
		}
	}
}
