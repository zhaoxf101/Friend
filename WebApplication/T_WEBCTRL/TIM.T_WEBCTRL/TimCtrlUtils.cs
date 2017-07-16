using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Hosting;
using System.Web.UI;

namespace TIM.T_WEBCTRL
{
	public class TimCtrlUtils
	{
		public const string Version = "1.14.0520.0001";

		public const string ScriptsLocation = "~/Scripts/Tim/";

		public const string ImagesLocation = "~/Images/Tim/";

		public const string DateFormatSeparator = "-";

		public const string TimeFormatSeparator = ":";

		public const string ThousandSeparator = ",";

		public const string DecimalSeparator = ".";

		public const string ReadOnlyBackgroundColor = "#F5FFFA";

		public const string ControlBorderColor = "#CDCDCD";

		public const int BorderLeftRightWidth = 2;

		public static DateTime UltDateTime = new DateTime(1899, 12, 31);

		private static string m_md5Version = string.Empty;

		private static bool _HasRead = false;

		private static object _Lock = new object();

		public static string Md5Version
		{
			get
			{
				TimCtrlUtils.UpdateMd5Version();
				return TimCtrlUtils.m_md5Version;
			}
		}

		internal static void WriteEndSymbol(HtmlTextWriter writer, TimCtrlEndSymbol symbol)
		{
			bool flag = symbol > TimCtrlEndSymbol.Null;
			if (flag)
			{
				string strTag = null;
				switch (symbol)
				{
				case TimCtrlEndSymbol.Null:
					strTag = "";
					break;
				case TimCtrlEndSymbol.Star:
					strTag = "&#42";
					break;
				case TimCtrlEndSymbol.Percent:
					strTag = "&#37";
					break;
				}
				writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "red");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(strTag);
				writer.RenderEndTag();
				writer.Write(writer.NewLine);
			}
		}

		private static void UpdateMd5Version()
		{
			bool flag = !TimCtrlUtils._HasRead;
			if (flag)
			{
				object @lock = TimCtrlUtils._Lock;
				lock (@lock)
				{
					bool flag3 = !TimCtrlUtils._HasRead;
					if (flag3)
					{
						TimCtrlUtils.m_md5Version = TimCtrlUtils.GetMd5Version();
						TimCtrlUtils._HasRead = true;
					}
				}
			}
		}

		private static string GetMd5Version()
		{
			string version = "";
			string filePath = HostingEnvironment.MapPath("~\\bin\\T_WEBCTRL.dll");
			bool flag = File.Exists(filePath);
			string result;
			if (flag)
			{
				try
				{
					DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(filePath);
					MD5 md5 = MD5.Create();
					byte[] bs = Encoding.UTF8.GetBytes(lastWriteTimeUtc.Ticks.ToString());
					byte[] hs = md5.ComputeHash(bs);
					StringBuilder sb = new StringBuilder();
					byte[] array = hs;
					for (int i = 0; i < array.Length; i++)
					{
						byte b = array[i];
						sb.Append(b.ToString("x2"));
					}
					result = sb.ToString();
					return result;
				}
				catch
				{
				}
			}
			result = version;
			return result;
		}
	}
}
