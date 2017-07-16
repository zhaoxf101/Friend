using System;
using System.Text;
using System.Web;
using TIM.T_KERNEL;

namespace TIM.T_TEMPLET.Page
{
	public abstract class TJsonHandlerBase : IHttpHandler
	{
		private HttpServerUtility m_server = null;

		private HttpContext m_context = null;

		private HttpResponse m_response = null;

		private HttpRequest m_request = null;

		private PageParameter m_pageParam = null;

		private LogicContext m_lgcContext = null;

		private LogicSession m_lgcSession = null;

		private string m_promptMessage = string.Empty;

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
			this.m_response.HeaderEncoding = Encoding.Default;
			this.m_context.Response.ContentEncoding.GetBytes("utf-8");
		}

		protected virtual bool SendData()
		{
			return true;
		}

		protected virtual bool ClearData()
		{
			return true;
		}

		protected bool SendJsonData(string jsonData)
		{
			bool flag = string.IsNullOrWhiteSpace(jsonData);
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
					this.m_response.Write(jsonData);
					this.m_response.Flush();
					this.m_response.End();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}
	}
}
