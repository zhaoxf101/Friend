using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.UI;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Common;
using TIM.T_KERNEL.Helper;

namespace TIM.T_TEMPLET.Page
{
	public class PageParameter
	{
		internal const string PageStateUrlKey = "SK";

		internal const string PageTimeUrlKey = "TK";

		internal const string PageAmIdKey = "AMID";

		internal const string PageExAmIdKey = "EXAMID";

		internal const string PageServerKey = "PPSKEY";

		internal const string PageViewStateKey = "PPVSKEY";

		internal const string WorkflowIdKey = "WFID";

		internal const string WorkflowRunIdKey = "WFRUNID";

		internal const string MasterIsEdit = "MASTERISEDIT";

		internal const string MasterInsert = "MASTERINSERT";

		internal const string MasterEdit = "MASTEREDIT";

		internal const string MasterDelete = "MASTERDELETE";

		private string m_urlPath = string.Empty;

		private int m_amId = 0;

		private int m_width = 900;

		private int m_height = 600;

		private PageState m_state = PageState.VIEW;

		private string m_title = string.Empty;

		private bool m_allowClose = true;

		private bool m_allowMax = false;

		private NameValueString m_urlPageParam = null;

		private Dictionary<string, object> m_extParams;

		public string UrlPath
		{
			get
			{
				return this.m_urlPath;
			}
			set
			{
				this.m_urlPath = value;
			}
		}

		public int AmId
		{
			get
			{
				return this.m_amId;
			}
			set
			{
				this.m_amId = value;
			}
		}

		public int Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		public int Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		public PageState State
		{
			get
			{
				return this.m_state;
			}
			set
			{
				this.m_state = value;
			}
		}

		public string Title
		{
			get
			{
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		public bool AllowClose
		{
			get
			{
				return this.m_allowClose;
			}
			set
			{
				this.m_allowClose = value;
			}
		}

		public bool AllowMax
		{
			get
			{
				return this.m_allowMax;
			}
			set
			{
				this.m_allowMax = value;
			}
		}

		private Dictionary<string, object> ExtParams
		{
			get
			{
				bool flag = this.m_extParams == null;
				if (flag)
				{
					this.m_extParams = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
				}
				return this.m_extParams;
			}
			set
			{
				this.m_extParams = value;
			}
		}

		public string EncodedParameters
		{
			get
			{
				return this.m_urlPageParam.EncodedText;
			}
		}

		public string EncodeTagParameters
		{
			get
			{
				return this.m_urlPageParam.EncodedText;
			}
		}

		public void AddExtString(string name, string value)
		{
			this.ExtParams.Add(name, value);
		}

		public string GetExtRawString(string name)
		{
			object obj = this.GetExtObject(name);
			bool flag = obj == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = (string)obj;
			}
			return result;
		}

		private object GetExtObject(string name)
		{
			bool flag = this.m_extParams == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				object obj;
				bool flag2 = this.m_extParams.TryGetValue(name, out obj);
				if (flag2)
				{
					result = obj;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public PageParameter()
		{
			this.m_urlPageParam = new NameValueString();
		}

		public void AddString(string name, string value)
		{
			this.m_urlPageParam.Add(name, value);
		}

		public void AddBool(string name, bool value)
		{
			this.m_urlPageParam.Add(name, value ? "Y" : "N");
		}

		public void AddInt(string name, int value)
		{
			this.m_urlPageParam.Add(name, value.ToString());
		}

		public void AddFloat(string name, float value)
		{
			this.m_urlPageParam.Add(name, value.ToString("R"));
		}

		public void AddDouble(string name, double value)
		{
			this.m_urlPageParam.Add(name, value.ToString("R"));
		}

		public void AddDecimal(string name, decimal value)
		{
			this.m_urlPageParam.Add(name, value.ToString());
		}

		public void AddDateTime(string name, DateTime value)
		{
			this.m_urlPageParam.Add(name, value.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		public string GetString(string name)
		{
			string result = string.Empty;
			bool flag = this.m_urlPageParam.ContainsKey(name);
			if (flag)
			{
				result = this.m_urlPageParam[name];
			}
			return result;
		}

		public bool GetBool(string name)
		{
			bool result = false;
			bool flag = this.m_urlPageParam.ContainsKey(name);
			if (flag)
			{
				result = this.m_urlPageParam[name].ToBool();
			}
			return result;
		}

		public int GetInt(string name)
		{
			int result = 0;
			bool flag = this.m_urlPageParam.ContainsKey(name);
			if (flag)
			{
				result = this.m_urlPageParam[name].ToInt();
			}
			return result;
		}

		public float GetFloat(string name)
		{
			float result = 0f;
			bool flag = this.m_urlPageParam.ContainsKey(name);
			if (flag)
			{
				result = this.m_urlPageParam[name].ToFloat();
			}
			return result;
		}

		public double GetDouble(string name)
		{
			double result = 0.0;
			bool flag = this.m_urlPageParam.ContainsKey(name);
			if (flag)
			{
				result = this.m_urlPageParam[name].ToDouble();
			}
			return result;
		}

		public decimal GetDecimal(string name)
		{
			decimal result = 0m;
			bool flag = this.m_urlPageParam.ContainsKey(name);
			if (flag)
			{
				result = this.m_urlPageParam[name].ToDecimal();
			}
			return result;
		}

		public DateTime GetDateTime(string name)
		{
			DateTime result = AppRuntime.UltDateTime;
			bool flag = this.m_urlPageParam.ContainsKey(name);
			if (flag)
			{
				result = this.m_urlPageParam[name].ToDateTime();
			}
			return result;
		}

		internal void ReadPageParameter(NameValueCollection nvc)
		{
			foreach (string name in nvc.Keys)
			{
				bool flag = name != null;
				if (flag)
				{
					this.AddString(name, nvc.GetValues(name)[0]);
				}
			}
		}

		internal void ReadPageParameter(NameValueCollection nvc, bool firstRead, StateBag viewState)
		{
			foreach (string name in nvc.Keys)
			{
				bool flag = name != null;
				if (flag)
				{
					this.AddString(name, nvc.GetValues(name)[0]);
				}
			}
			bool flag2 = viewState == null;
			if (flag2)
			{
				HttpContext.Current.Session.Remove("PPSKEY");
			}
			else if (firstRead)
			{
				HttpContext ctx = HttpContext.Current;
				object obj = ctx.Session["PPSKEY"];
				bool flag3 = obj != null;
				if (flag3)
				{
					this.m_extParams = (Dictionary<string, object>)obj;
					viewState["PPVSKEY"] = this.m_extParams;
					ctx.Session.Remove("PPSKEY");
				}
			}
			else
			{
				object obj2 = viewState["PPVSKEY"];
				bool flag4 = obj2 != null;
				if (flag4)
				{
					this.m_extParams = (Dictionary<string, object>)obj2;
				}
			}
		}

		private void ReadPageParameter(HttpRequest request)
		{
			foreach (string name in request.QueryString.Keys)
			{
				bool flag = name != null;
				if (flag)
				{
					this.AddString(name, request.Params.GetValues(name)[0]);
				}
			}
		}

		internal void AddNameValueString(NameValueString nv)
		{
			foreach (string name in nv.Keys)
			{
				bool flag = name != null;
				if (flag)
				{
					this.AddString(name, nv[name]);
				}
			}
		}

		public void SaveParams()
		{
			bool flag = this.m_extParams != null && this.m_extParams.Count > 0;
			if (flag)
			{
				HttpContext ctx = HttpContext.Current;
				ctx.Session["PPSKEY"] = this.m_extParams;
			}
		}

		public string CombineUrl()
		{
			string result = string.Empty;
			this.AddString("TK", DateTime.Now.Ticks.ToString());
			StringBuilder sb = new StringBuilder();
			sb.Append(this.UrlPath);
			sb.Append("?");
			sb.Append(this.EncodeTagParameters);
			return sb.ToString();
		}

		public string CombineUrl(bool beState)
		{
			string result = string.Empty;
			if (beState)
			{
				this.AddString("SK", this.State.ToString());
			}
			this.AddString("TK", DateTime.Now.Ticks.ToString());
			StringBuilder sb = new StringBuilder();
			bool flag = !string.IsNullOrWhiteSpace(this.UrlPath);
			if (flag)
			{
				sb.Append(this.UrlPath);
				sb.Append("?");
			}
			sb.Append(this.EncodeTagParameters);
			return sb.ToString();
		}
	}
}
