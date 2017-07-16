using System;
using System.Web;
using TIM.T_KERNEL;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_TEMPLET.Page;

namespace TIM.T_TEMPLET.Handler
{
	public class CodeHelperHandler : IHttpHandler
	{
		private HttpContext m_context;

		private HttpResponse m_response;

		private HttpRequest m_request;

		private PageParameter m_pageParam;

		private LogicContext m_lgc;

		private LogicSession m_session;

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			this.m_context = context;
			this.m_request = this.m_context.Request;
			this.m_response = this.m_context.Response;
			this.m_lgc = LogicContext.Current;
			this.m_pageParam = new PageParameter();
			this.m_pageParam.ReadPageParameter(this.m_request.QueryString);
			this.m_session = LogicContext.GetLogicSession();
			context.Server.ScriptTimeout = 3600;
			string codeId = this.m_pageParam.GetString("CODEID");
			string ctrlId = this.m_pageParam.GetString("CTRLID");
			string ctrlValue = this.m_pageParam.GetString("CTRLVALUE");
			CodeHelper codeHelper = CodeHelperUtils.GetCodeHelper(codeId);
			bool flag = codeHelper != null;
			if (flag)
			{
				int mdId = codeHelper.MdId;
				bool flag2 = mdId > 0;
				if (flag2)
				{
					context.Response.Write(string.Concat(new object[]
					{
						"ActiveModule.aspx?AMID=",
						mdId,
						"&",
						this.m_request.QueryString
					}));
				}
			}
		}
	}
}
