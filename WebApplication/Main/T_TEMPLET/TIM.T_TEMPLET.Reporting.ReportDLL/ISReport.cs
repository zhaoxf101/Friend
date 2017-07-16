using System;
using System.Runtime.InteropServices;

namespace TIM.T_TEMPLET.Reporting.ReportDLL
{
	public class ISReport
	{
		[DllImport("TranReport.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "tranRpt")]
		public static extern int TranRpt(string a, string b);
	}
}
