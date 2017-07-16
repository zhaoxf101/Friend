using System;
using TIM.T_KERNEL.Common;

namespace TIM.T_KERNEL.Protocols
{
	public static class MimeContentType
	{
		private static NameValueString mime;

		static MimeContentType()
		{
			MimeContentType.mime = new NameValueString();
			MimeContentType.mime.LineText = ".zip=application/x-zip-compressed\r\n.rar=application/x-rar-compressed\r\n.txt=text/plain\r\n.htm=text/html\r\n.html=text/html\r\n.xml=text/xml\r\n.shtml=magnus-internal/parsed-html\r\n.js=application/x-javascript\r\n.swf=application/x-shockwave-flash\r\n\r\n.doc=application/msword\r\n.dot=application/msword\r\n.pps=application/vndms-pps\r\n.ppt=application/vndms-powerpoint\r\n.xls=application/vndcase.ms-excel\r\n.docm=application/vnd.ms-word.document.macroEnabled.12\r\n.docx=application/vnd.openxmlformats-officedocument.wordprocessingml.document\r\n.dotm=application/vnd.ms-word.template.macroEnabled.12\r\n.dotx=application/vnd.openxmlformats-officedocument.wordprocessingml.template\r\n.potm=application/vnd.ms-powerpoint.template.macroEnabled.12\r\n.potx=application/vnd.openxmlformats-officedocument.presentationml.template\r\n.ppam=application/vnd.ms-powerpoint.addin.macroEnabled.12\r\n.ppsm=application/vnd.ms-powerpoint.slideshow.macroEnabled.12\r\n.ppsx=application/vnd.openxmlformats-officedocument.presentationml.slideshow\r\n.pptm=application/vnd.ms-powerpoint.presentation.macroEnabled.12\r\n.pptx=application/vnd.openxmlformats-officedocument.presentationml.presentation\r\n.xlam=application/vnd.ms-excel.addin.macroEnabled.12\r\n.xlsb=application/vnd.ms-excel.sheet.binary.macroEnabled.12\r\n.xlsm=application/vnd.ms-excel.sheet.macroEnabled.12\r\n.xlsx=application/vnd.openxmlformats-officedocument.spreadsheetml.sheet\r\n.xltm=application/vnd.ms-excel.template.macroEnabled.12\r\n.xltx=application/vnd.openxmlformats-officedocument.spreadsheetml.template\r\n.pdf=application/pdf\r\n.rtf=application/msword\r\n.wps=application/vndms-works\r\n\r\n.ico=image/x-icon\r\n.bmp=image/bmp\r\n.jpe=image/jpeg\r\n.jpeg=image/jpeg\r\n.jpg=image/jpeg\r\n.gif=image/gif\r\n.png=image/png\r\n.tif=image/tiff\r\n.tiff=image/tiff\r\n\r\n.323=text/h323\r\n.asf=video/x-ms-asf\r\n.asx=video/x-ms-asf\r\n.avi=video/x-msvideo\r\n.mpeg=video/mpeg\r\n.mpg=video/mpeg\r\n.rmvb=audio/x-pn-realaudio\r\n.mid=audio/mid\r\n.midi=audio/mid\r\n.mp3=audio/mpeg\r\n.wav=audio/wav\r\n";
		}

		public static string GetContentType(string fileExt)
		{
			string str = string.Empty;
			bool flag = !fileExt.StartsWith(".");
			if (flag)
			{
				fileExt = "." + fileExt;
			}
			string str2 = MimeContentType.mime[fileExt];
			bool flag2 = string.IsNullOrEmpty(str2);
			if (flag2)
			{
				str2 = "application/octet-stream";
			}
			return str2;
		}
	}
}
