using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TIM.T_INDEX.Tips
{
	public class Tips : Page
	{
		protected HtmlForm form1;

		protected LinkButton btnCertDownload;

		protected LinkButton btnDownload;

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void btnCertDownload_Click(object sender, EventArgs e)
		{
			string s = "-----BEGIN CERTIFICATE-----\r\nMIID5jCCAtKgAwIBAgIQ65gGIHvADqFBvS8evky31TAJBgUrDgMCHQUAMFExETAP\r\nBgNVBAMTCFVhbnVlLmNuMQswCQYDVQQGEwJDTjEvMC0GA1UEChMmVWFudWUgSW5m\r\nb3JtYXRpb24gVGVjaG5vbG9neSBDby4sIEx0ZC4wIBcNMDgxMjMxMTYwMDAwWhgP\r\nMjA5OTEyMzAxNjAwMDBaMFExETAPBgNVBAMTCFVhbnVlLmNuMQswCQYDVQQGEwJD\r\nTjEvMC0GA1UEChMmVWFudWUgSW5mb3JtYXRpb24gVGVjaG5vbG9neSBDby4sIEx0\r\nZC4wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCzd6MeehtCi1LTCO1d\r\nr2/jZac4rX6XaI6K6Czn68x86YabXr4muUzFQLFzYTIKqCdEtDekXrQtvliH7Kcp\r\n58wYLCDwYZ2P7G2FH53zWtSzi1e9TFA6vROs6gDrEatAkQwZrtGrf8Mjimu0l/Fj\r\nbiyj21p3E4ulnav388BhwlDq61S13lak9HlA/f9vQhsrtqj7e59OFr08m8ZDzvYV\r\nS3b7l6QvoZs9QxXbWiZwF4BuyadZdHbXkaBVjfj75HCM4e3KtHb6TTtGiKYqDZ/N\r\neXFYJo3dJvTLzc0Y6yTEI3iwV+x347bNFKgopdD7XddmWuG9V97WybDHakZGA+Tk\r\nFfbjAgMBAAGjgb8wgbwwIAYDVR0EAQH/BBYwFDAOMAwGCisGAQQBgjcCARYDAgeA\r\nMBMGA1UdJQQMMAoGCCsGAQUFBwMDMIGCBgNVHQEEezB5gBAEvLVCDfJHfNyxGTSO\r\nlOAmoVMwUTERMA8GA1UEAxMIVWFudWUuY24xCzAJBgNVBAYTAkNOMS8wLQYDVQQK\r\nEyZVYW51ZSBJbmZvcm1hdGlvbiBUZWNobm9sb2d5IENvLiwgTHRkLoIQ65gGIHvA\r\nDqFBvS8evky31TAJBgUrDgMCHQUAA4IBAQAV5YyvXKsZ2JM1153qu6NJlVeBiLil\r\n6rPX4D3qo2rwyKPjit/Q/wuR5X7yLNvd/s3r0lgI+kjmM0O/b21JBDY/fF3QP1zT\r\nWQM24JdFENuLv+NKsh3t891tOzrMQrLwMYkrkHnpuPEQ5XWqQ+9nLjYTVt30E1Pq\r\nJvyD1vfiDSv8vjgdHPA++4WFRslg+sCBEIslzvsVIiyvEZU6MC1Qlz4Ikmk5LU+e\r\nhEhdeUonr96w9P0OFqExtmW56nQ71rddRpzfFXznnqQiIHD65E1TK9klu/DwerHi\r\nCGFQsNBWGUnxBPzpv+qfEJb8z9RqWVQitJdKHH6FZNbCEJcolnMIUpCh\r\n-----END CERTIFICATE-----\r\n";
			base.Response.Clear();
			base.Response.Buffer = true;
			base.Response.ContentType = "application/x-pkcs12";
			base.Response.AppendHeader("Content-Disposition", "attachment;filename=\"Uanue.cer\"");
			base.Response.AddHeader("Content-Transfer-Encoding", "binary");
			base.Response.Write(s);
			base.Response.End();
		}

		protected void btnDownload_Click(object sender, EventArgs e)
		{
			byte[] bytes = Encoding.ASCII.GetBytes("*HOST*");
			int num = bytes.Length;
			string s = "http://" + base.Request.Url.Host;
			byte[] bytes2 = Encoding.Default.GetBytes(s);
			bool flag = false;
			bool flag2 = bytes2.Length > 108;
			if (flag2)
			{
				flag = true;
			}
			int num2 = 32768;
			byte[] array = new byte[num2];
			string path = base.MapPath("~\\Ocx\\MSIE_SET.exe");
			FileStream fileStream = File.OpenRead(path);
			try
			{
				base.Response.Clear();
				base.Response.HeaderEncoding = Encoding.Default;
				base.Response.ContentType = "application/octet-stream";
				base.Response.AppendHeader("Content-Length", fileStream.Length.ToString());
				base.Response.AppendHeader("Content-Disposition", "attachment;filename=\"MSIE_SET.exe\"");
				base.Response.AddHeader("Content-Transfer-Encoding", "binary");
				int i = (int)fileStream.Length;
				int num3 = 0;
				int num4 = num2;
				while (i > 0)
				{
					int num5 = (i > num4) ? num4 : i;
					num5 = fileStream.Read(array, num3, num5);
					i -= num5;
					num3 += num5;
					bool flag3 = flag;
					if (flag3)
					{
						base.Response.OutputStream.Write(array, 0, num3);
						num3 = 0;
						num4 = num2;
					}
					else
					{
						int num6 = this.Find(array, num3, bytes);
						bool flag4 = num6 > 0;
						if (flag4)
						{
							base.Response.OutputStream.Write(array, 0, num6);
							base.Response.OutputStream.Write(bytes2, 0, bytes2.Length);
							num3 -= num6;
							bool flag5 = num3 > bytes2.Length;
							if (flag5)
							{
								num6 += bytes2.Length;
								num3 -= bytes2.Length;
								base.Response.OutputStream.Write(array, num6, num3);
							}
							else
							{
								fileStream.Seek((long)(bytes2.Length - num3), SeekOrigin.Current);
							}
							flag = true;
							num3 = 0;
							num4 = num2;
						}
						else
						{
							int num7 = num3 - num;
							base.Response.OutputStream.Write(array, 0, num7);
							System.Buffer.BlockCopy(array, num7, array, 0, num);
							num3 = num;
							num4 = num2 - num;
						}
					}
					base.Response.Flush();
				}
				base.Response.End();
			}
			finally
			{
				fileStream.Close();
			}
		}

		private int Find(byte[] buffer, int length, byte[] src)
		{
			byte b = src[0];
			int result;
			for (int i = 0; i < length - src.Length; i++)
			{
				bool flag = buffer[i] == b;
				if (flag)
				{
					bool flag2 = true;
					for (int j = 0; j < src.Length; j++)
					{
						bool flag3 = buffer[i + j] != src[j];
						if (flag3)
						{
							flag2 = false;
							break;
						}
					}
					bool flag4 = flag2;
					if (flag4)
					{
						result = i + src.Length;
						return result;
					}
				}
			}
			result = -1;
			return result;
		}
	}
}
