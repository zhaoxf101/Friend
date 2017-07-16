using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;

namespace TIM.T_WEBCTRL
{
	public sealed class RequestRecorderModule : IHttpModule
	{
		private string recorderOutputLocation;

		public void Init(HttpApplication context)
		{
			context.PreRequestHandlerExecute += new EventHandler(this.context_BeginRequest);
		}

		private HttpWorkerRequest GetWorkerRequest(HttpContext context)
		{
			return (HttpWorkerRequest)((IServiceProvider)HttpContext.Current).GetService(typeof(HttpWorkerRequest));
		}

		private string GetRecorderText()
		{
			bool flag = this.recorderOutputLocation == null;
			if (flag)
			{
				this.recorderOutputLocation = ConfigurationManager.AppSettings["recorderOutputLocation"];
				bool flag2 = this.recorderOutputLocation != null && !Path.IsPathRooted(this.recorderOutputLocation);
				if (flag2)
				{
					this.recorderOutputLocation = HttpContext.Current.Server.MapPath(this.recorderOutputLocation);
				}
				bool flag3 = this.recorderOutputLocation == null || !Directory.Exists(this.recorderOutputLocation);
				if (flag3)
				{
					this.recorderOutputLocation = HttpContext.Current.Server.MapPath("~/");
				}
			}
			string text2 = Path.Combine(this.recorderOutputLocation, "Record");
			string text3 = ".bin";
			int num = 0;
			string text4;
			do
			{
				num++;
				text4 = string.Concat(new string[]
				{
					text2,
					"[",
					num.ToString(),
					"]",
					text3
				});
			}
			while (File.Exists(text4));
			return text4;
		}

		private void context_BeginRequest(object sender, EventArgs e)
		{
			HttpApplication application = sender as HttpApplication;
			bool flag = this.IsUploadRequest(application.Request);
			if (flag)
			{
				HttpWorkerRequest request = this.GetWorkerRequest(application.Context);
				Encoding contentEncoding = application.Context.Request.ContentEncoding;
				bool flag2 = request != null;
				if (flag2)
				{
					RequestStream requeststream = new RequestStream(request);
					FileStream stream = File.Create(this.GetRecorderText());
					byte[] buffer = new byte[8192];
					int count = 0;
					int num2 = 0;
					int num3 = int.Parse(request.GetKnownRequestHeader(11));
					while (num2 < num3 && (count = requeststream.Read(buffer, 0, 8192)) > 0)
					{
						stream.Write(buffer, 0, count);
						num2 += count;
					}
					stream.Close();
				}
			}
		}

		private bool IsUploadRequest(HttpRequest request)
		{
			return string.Compare(request.ContentType, 0, "multipart/form-data", 0, 19, true, CultureInfo.InvariantCulture) == 0;
		}

		public void Dispose()
		{
		}
	}
}
