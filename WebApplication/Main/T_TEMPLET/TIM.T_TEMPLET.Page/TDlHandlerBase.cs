using System;
using System.IO;
using System.Text;
using System.Web;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Common;
using TIM.T_KERNEL.Protocols;

namespace TIM.T_TEMPLET.Page
{
	public abstract class TDlHandlerBase : IHttpHandler
	{
		private HttpServerUtility m_server = null;

		private HttpContext m_context = null;

		private HttpResponse m_response = null;

		private HttpRequest m_request = null;

		private PageParameter m_pageParam = null;

		private LogicContext m_lgcContext = null;

		private LogicSession m_lgcSession = null;

		private string m_promptMessage = string.Empty;

		private bool m_inlineView = false;

		public HttpServerUtility Server
		{
			get
			{
				return this.m_server;
			}
		}

		public HttpContext Context
		{
			get
			{
				return this.m_context;
			}
		}

		public HttpResponse Response
		{
			get
			{
				return this.m_response;
			}
		}

		public HttpRequest Request
		{
			get
			{
				return this.m_request;
			}
		}

		public PageParameter PageParam
		{
			get
			{
				return this.m_pageParam;
			}
		}

		public LogicContext LgcContext
		{
			get
			{
				return this.m_lgcContext;
			}
		}

		public LogicSession LgcSession
		{
			get
			{
				return this.m_lgcSession;
			}
			set
			{
				this.m_lgcSession = value;
			}
		}

		public string PromptMessage
		{
			get
			{
				return this.m_promptMessage;
			}
			set
			{
				this.m_promptMessage = value;
			}
		}

		public bool InlineView
		{
			get
			{
				return this.m_inlineView;
			}
			set
			{
				this.m_inlineView = value;
			}
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			this.m_server = context.Server;
			this.m_context = context;
			this.m_request = this.m_context.Request;
			this.m_response = this.m_context.Response;
			this.m_lgcContext = LogicContext.Current;
			this.m_lgcSession = LogicContext.GetLogicSession();
			this.m_pageParam = new PageParameter();
			this.m_pageParam.ReadPageParameter(this.Request.QueryString);
			context.Server.ScriptTimeout = 3600;
			this.ParsePageParam();
			try
			{
				this.PrepareData();
			}
			catch (Exception ex)
			{
				this.m_promptMessage = ex.Message;
			}
			this.SendHeader();
			this.SendData();
			this.ClearData();
		}

		protected abstract string GetFileName();

		protected abstract long GetLength();

		protected virtual string GetContentType()
		{
			return this.FileExt2ContentType(Path.GetExtension(this.GetFileName()));
		}

		protected string FileExt2ContentType(string fileExt)
		{
			return MimeContentType.GetContentType(fileExt);
		}

		protected string LinkTempPath(string fileName)
		{
			return RunDirectory.LinkTempPath(fileName);
		}

		protected virtual void ParsePageParam()
		{
		}

		protected virtual void PrepareData()
		{
		}

		private void SendHeader()
		{
			this.m_response.Clear();
			this.m_response.StatusCode = 200;
			this.m_response.HeaderEncoding = Encoding.UTF8;
			this.m_response.AppendHeader("Content-Length", this.GetLength().ToString());
			string fileName = this.GetFileName();
			string userAgent = this.m_context.Request.UserAgent.ToLower();
			bool isIE = userAgent.IndexOf("msie") > -1 || userAgent.IndexOf("like gecko") > -1;
			bool isFirefox = userAgent.IndexOf("firefox") > -1;
			bool flag = isIE;
			if (flag)
			{
				fileName = Uri.EscapeUriString(fileName);
			}
			else
			{
				bool flag2 = isFirefox;
				if (flag2)
				{
					fileName = "\"" + fileName + "\"";
				}
			}
			bool inlineView = this.m_inlineView;
			string disposition;
			if (inlineView)
			{
				disposition = "inline;filename=" + fileName;
			}
			else
			{
				disposition = "attachment;filename=" + fileName;
			}
			this.m_response.AppendHeader("Content-Disposition", disposition);
			this.m_response.ContentType = this.GetContentType();
			this.m_response.AppendHeader("Content-Transfer-Encoding", "binary");
		}

		protected virtual bool SendData()
		{
			return true;
		}

		protected virtual bool ClearData()
		{
			return true;
		}

		protected bool SendBufferData(byte[] data)
		{
			int bytesLeft = data.Length;
			bool flag = bytesLeft == 0;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool isClientConnected = this.m_response.IsClientConnected;
				if (isClientConnected)
				{
					this.m_response.OutputStream.Write(data, 0, bytesLeft);
					this.m_response.Flush();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		protected bool SendStreamData(Stream data)
		{
			bool flag = data != null;
			if (flag)
			{
				data.Position = 0L;
			}
			long bytesLeft = data.Length;
			bool flag2 = bytesLeft == 0L;
			bool result;
			if (flag2)
			{
				result = true;
			}
			else
			{
				int bufferSize = (bytesLeft > 32768L) ? 32768 : ((int)bytesLeft);
				byte[] buffer = new byte[bufferSize];
				while (bytesLeft > 0L)
				{
					bool isClientConnected = this.m_response.IsClientConnected;
					if (!isClientConnected)
					{
						result = false;
						return result;
					}
					int bytesRead = (bytesLeft > (long)bufferSize) ? bufferSize : ((int)bytesLeft);
					bytesRead = data.Read(buffer, 0, bytesRead);
					bool flag3 = bytesRead == 0;
					if (flag3)
					{
						result = false;
						return result;
					}
					this.m_response.OutputStream.Write(buffer, 0, bytesRead);
					this.m_response.Flush();
					bytesLeft -= (long)bytesRead;
				}
				result = true;
			}
			return result;
		}
	}
}
