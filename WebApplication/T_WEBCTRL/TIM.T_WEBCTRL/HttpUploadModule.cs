using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace TIM.T_WEBCTRL
{
	public sealed class HttpUploadModule : IHttpModule
	{
		private DateTime _dateTimeNow = DateTime.Now;

		private long _maxRequestLength;

		internal static IStatusManager StatusManager
		{
			get
			{
				string str = UtoUploadConfiguration.StatusManager["manager"];
				bool flag = str != null && str.Length != 0;
				StatusManagerType applicationState;
				if (flag)
				{
					applicationState = (StatusManagerType)Enum.Parse(typeof(StatusManagerType), str, true);
				}
				else
				{
					applicationState = StatusManagerType.ApplicationState;
				}
				StatusManagerType statusManagerType = applicationState;
				string assemblyQualifiedName;
				if (statusManagerType != StatusManagerType.ApplicationState)
				{
					if (statusManagerType != StatusManagerType.SqlClient)
					{
						assemblyQualifiedName = UtoUploadConfiguration.StatusManager["type"];
					}
					else
					{
						assemblyQualifiedName = typeof(SqlClientStatusManager).AssemblyQualifiedName;
					}
				}
				else
				{
					assemblyQualifiedName = typeof(ApplicationStateStatusManager).AssemblyQualifiedName;
				}
				IStatusManager manager = ConfigurationHashThread.CreateConfigurationHashObject(assemblyQualifiedName, new object[]
				{
					UtoUploadConfiguration.StatusManager
				}) as IStatusManager;
				bool flag2 = manager == null;
				if (flag2)
				{
					throw new ApplicationException("Could not instantiate manager, or manager was not specified.");
				}
				return manager;
			}
		}

		private static IUploadRequestFilter UploadRequestFilter
		{
			get
			{
				IUploadRequestFilter filter = null;
				string str;
				IUploadRequestFilter result;
				try
				{
					str = UtoUploadConfiguration.UploadParser["requestFilter"];
				}
				catch
				{
					result = null;
					return result;
				}
				bool flag = !string.IsNullOrEmpty(str);
				if (flag)
				{
					filter = (ConfigurationHashThread.CreateConfigurationHashObject(str, new object[0]) as IUploadRequestFilter);
					bool flag2 = filter == null;
					if (flag2)
					{
						throw new ApplicationException("Could not instantiate upload request filter.");
					}
				}
				result = filter;
				return result;
			}
		}

		internal static IUploadStreamProvider UploadStreamProvider
		{
			get
			{
				string str = UtoUploadConfiguration.UploadStreamProvider["provider"];
				bool flag = str != null && str.Length != 0;
				UploadStreamProviderType file;
				if (flag)
				{
					file = (UploadStreamProviderType)Enum.Parse(typeof(UploadStreamProviderType), str, true);
				}
				else
				{
					file = UploadStreamProviderType.File;
				}
				UploadStreamProviderType uploadStreamProviderType = file;
				string assemblyQualifiedName;
				if (uploadStreamProviderType != UploadStreamProviderType.File)
				{
					if (uploadStreamProviderType != UploadStreamProviderType.SqlClient)
					{
						assemblyQualifiedName = UtoUploadConfiguration.UploadStreamProvider["type"];
					}
					else
					{
						assemblyQualifiedName = typeof(SqlClientUploadStreamProvider).AssemblyQualifiedName;
					}
				}
				else
				{
					assemblyQualifiedName = typeof(FileUploadStreamProvider).AssemblyQualifiedName;
				}
				IUploadStreamProvider provider = ConfigurationHashThread.CreateConfigurationHashObject(assemblyQualifiedName, UtoUploadConfiguration.UploadStreamProvider.GetConfigurationHashKey(), new object[]
				{
					UtoUploadConfiguration.UploadStreamProvider
				}) as IUploadStreamProvider;
				bool flag2 = provider == null;
				if (flag2)
				{
					throw new ApplicationException("Could not instantiate provider, or provider was not specified.");
				}
				return provider;
			}
		}

		internal static IUploadFileFilter UploadFileFilter
		{
			get
			{
				IUploadFileFilter filter = null;
				string str = UtoUploadConfiguration.UploadParser["fileFilter"];
				bool flag = !string.IsNullOrEmpty(str);
				if (flag)
				{
					filter = (ConfigurationHashThread.CreateConfigurationHashObject(str, new object[0]) as IUploadFileFilter);
					bool flag2 = filter == null;
					if (flag2)
					{
						throw new ApplicationException("Could not instantiate upload file filter.");
					}
				}
				return filter;
			}
		}

		private static IUploadLengthFilter UploadLengthFilter
		{
			get
			{
				IUploadLengthFilter filter = null;
				string str = UtoUploadConfiguration.UploadParser["lengthFilter"];
				bool flag = !string.IsNullOrEmpty(str);
				if (flag)
				{
					filter = (ConfigurationHashThread.CreateConfigurationHashObject(str, new object[0]) as IUploadLengthFilter);
					bool flag2 = filter == null;
					if (flag2)
					{
						throw new ApplicationException("Could not instantiate upload length filter.");
					}
				}
				return filter;
			}
		}

		public static ReadOnlyCollection<UploadedFile> GetUploadedFiles()
		{
			UploadStatus uploadStatus = HttpUploadModule.GetUploadStatus();
			bool flag = uploadStatus != null;
			ReadOnlyCollection<UploadedFile> result;
			if (flag)
			{
				result = uploadStatus.GetUploadedFiles();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static UploadStatus GetUploadStatus()
		{
			return HttpUploadModule.GetUploadStatus(HttpContext.Current);
		}

		public static UploadStatus GetUploadStatus(string uploadId)
		{
			return HttpUploadModule.StatusManager.GetUploadStatus(uploadId);
		}

		public static UploadStatus GetUploadStatus(HttpContext context)
		{
			return context.Items["_UtoUpload_uploadStatus"] as UploadStatus;
		}

		public static void RemoveStatus(UploadStatus status)
		{
			HttpUploadModule.RemoveStatus(status.UploadId);
		}

		public static void RemoveStatus(string uploadId)
		{
			HttpUploadModule.StatusManager.RemoveStatus(uploadId);
		}

		private void GetMaxRequestLength(HttpContext context)
		{
			object section = null;
			try
			{
				section = context.GetSection("system.web/httpRuntime");
			}
			catch
			{
			}
			bool flag = section != null;
			if (flag)
			{
				this._maxRequestLength = (long)(((HttpRuntimeSection)section).MaxRequestLength * 1024);
			}
			bool flag2 = this._maxRequestLength <= 0L;
			if (flag2)
			{
				this._maxRequestLength = -1L;
			}
		}

		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private void InjectTextParts(HttpRequest request, byte[] textParts)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
			Type type = request.GetType();
			FieldInfo field = type.GetField("_arrRawContent", bindingAttr);
			bool flag = field == null;
			if (flag)
			{
				field = type.GetField("_rawContent", bindingAttr);
			}
			FieldInfo info2 = type.GetField("_iContentLength", bindingAttr);
			bool flag2 = info2 == null;
			if (flag2)
			{
				info2 = type.GetField("_contentLength", bindingAttr);
			}
			bool flag3 = field != null && info2 != null;
			if (flag3)
			{
				field.SetValue(request, textParts);
				info2.SetValue(request, textParts.Length);
			}
		}

		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private void InjectTextParts(HttpWorkerRequest request, byte[] textParts)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
			Type baseType = request.GetType();
			while (baseType != null && baseType.FullName != "System.Web.Hosting.ISAPIWorkerRequest" && baseType.FullName != "Microsoft.VisualStudio.WebHost.Request" && baseType.FullName != "Cassini.Request")
			{
				baseType = baseType.BaseType;
			}
			string text = null;
			bool flag = baseType != null && (text = baseType.FullName) != null;
			if (flag)
			{
				bool flag2 = text != "System.Web.Hosting.ISAPIWorkerRequest";
				if (flag2)
				{
					bool flag3 = text != "Cassini.Request" && text != "Microsoft.VisualStudio.WebHost.Request";
					if (!flag3)
					{
						baseType.GetField("_contentLength", bindingAttr).SetValue(request, textParts.Length);
						baseType.GetField("_preloadedContent", bindingAttr).SetValue(request, textParts);
						baseType.GetField("_preloadedContentLength", bindingAttr).SetValue(request, textParts.Length);
					}
				}
				else
				{
					baseType.GetField("_contentAvailLength", bindingAttr).SetValue(request, textParts.Length);
					baseType.GetField("_contentTotalLength", bindingAttr).SetValue(request, textParts.Length);
					baseType.GetField("_preloadedContent", bindingAttr).SetValue(request, textParts);
					baseType.GetField("_preloadedContentRead", bindingAttr).SetValue(request, true);
				}
			}
		}

		public void Init(HttpApplication context)
		{
			string str = UtoUploadConfiguration.UploadParser["attachEvent"];
			bool flag = !string.IsNullOrEmpty(str) && string.Equals(str, "beginrequest", StringComparison.InvariantCultureIgnoreCase);
			if (flag)
			{
				context.BeginRequest += new EventHandler(this.context_BeginRequest);
			}
			else
			{
				context.PreRequestHandlerExecute += new EventHandler(this.context_BeginRequest);
			}
			context.BeginRequest += new EventHandler(this.BeginRequestHandler);
			context.Error += new EventHandler(this.context_Error);
		}

		private void context_Error(object sender, EventArgs e)
		{
			HttpApplication application = sender as HttpApplication;
			bool flag = this.IsUploadRequest(application.Request);
			if (flag)
			{
				UploadStatus uploadStatus = HttpUploadModule.GetUploadStatus(application.Context);
				this.ClearUploadStatus(uploadStatus);
				uploadStatus.SetState(UploadState.Terminated, UploadTerminationReason.Error, application.Context.AllErrors, application.Context.AllErrors[0].Message);
			}
		}

		private void ClearUploadStatus(UploadStatus uploadstatus)
		{
			bool flag = uploadstatus.UploadedFiles != null && uploadstatus.UploadedFiles.Count > 0;
			if (flag)
			{
				foreach (UploadedFile file in uploadstatus.UploadedFiles)
				{
					HttpUploadModule.UploadStreamProvider.RemoveOutput(file);
				}
				uploadstatus.UploadedFiles.Clear();
			}
		}

		private void BeginRequestHandler(object sender, EventArgs e)
		{
		}

		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private HttpWorkerRequest GetWorkerRequest(HttpContext context)
		{
			return (HttpWorkerRequest)((IServiceProvider)HttpContext.Current).GetService(typeof(HttpWorkerRequest));
		}

		private void ProofreadTime()
		{
			bool flag = DateTime.Now.Subtract(this._dateTimeNow).TotalMinutes >= 10.0;
			if (flag)
			{
				this._dateTimeNow = DateTime.Now;
				HttpUploadModule.StatusManager.RemoveStaleStatus(10);
			}
		}

		private void context_BeginRequest(object sender, EventArgs e)
		{
			HttpApplication application = sender as HttpApplication;
			bool flag = this.IsUploadRequest(application.Request) && UtoUploadConfiguration.HandleRequests;
			if (flag)
			{
				this.ProofreadTime();
				HttpWorkerRequest worker = this.GetWorkerRequest(application.Context);
				Encoding contentEncoding = application.Context.Request.ContentEncoding;
				UploadLog.Log("Received upload request");
				bool flag2 = worker != null;
				if (flag2)
				{
					long num = long.Parse(worker.GetKnownRequestHeader(11));
					byte[] boundary = this.ExtractBoundary(application.Request.ContentType, contentEncoding);
					string str = application.Request.QueryString["UploadId"];
					bool flag3 = string.IsNullOrEmpty(str);
					if (flag3)
					{
						str = Guid.NewGuid().ToString();
					}
					UploadStatus uploadStatus = HttpUploadModule.StatusManager.GetUploadStatus(str);
					bool flag4 = uploadStatus == null;
					if (flag4)
					{
						uploadStatus = new UploadStatus(str);
						HttpUploadModule.StatusManager.UploadStarted(uploadStatus);
					}
					uploadStatus.x98532580ab8a33a1(num);
					bool flag5 = this.IsOversizedRequest(application.Request, num);
					if (flag5)
					{
						UploadLog.Log("Request too big... aborting");
						application.CompleteRequest();
						uploadStatus.SetState(UploadState.Terminated, UploadTerminationReason.MaxRequestLengthExceeded);
						this.RegisterIn(application.Context, uploadStatus);
					}
					else
					{
						UploadLog.Log("Starting upload", str);
						UploadLog.Log(worker, str);
						uploadStatus.SetState(UploadState.ReceivingData);
						this.RegisterIn(application.Context, uploadStatus);
						MimeUploadHandler UploadHandler = null;
						try
						{
							UploadHandler = new MimeUploadHandler(new RequestStream(worker), boundary, uploadStatus, contentEncoding, HttpUploadModule.UploadStreamProvider);
							UploadHandler.Parse();
							byte[] bytes = contentEncoding.GetBytes(UploadHandler.TextParts);
							bool flag6 = worker.GetType().FullName.StartsWith("Mono.");
							if (flag6)
							{
								this.InjectTextParts(application.Context.Request, bytes);
							}
							else
							{
								bool flag7 = worker.GetType().Name == "IIS7WorkerRequest";
								if (flag7)
								{
									this.InvokeMember(application.Context.Request, bytes);
								}
								else
								{
									this.InjectTextParts(worker, bytes);
								}
							}
							uploadStatus.SetState(UploadState.Complete);
							UploadLog.Log("Upload request complete", str);
						}
						catch (UploadException exception)
						{
							UploadLog.Log("Upload canceled or disconnected", str);
							bool flag8 = uploadStatus != null;
							if (flag8)
							{
								bool flag9 = UploadHandler != null;
								if (flag9)
								{
									UploadHandler.CancelParse();
								}
								this.ClearUploadStatus(uploadStatus);
							}
							UploadStatus status2 = HttpUploadModule.StatusManager.GetUploadStatus(str);
							bool flag10 = status2 != null && status2.State != UploadState.Terminated;
							if (flag10)
							{
								status2.SetState(UploadState.Terminated, exception.Reason, exception.Message);
							}
							application.CompleteRequest();
						}
					}
				}
			}
		}

		private void RegisterIn(HttpContext context, UploadStatus uploadstatus)
		{
			context.Items["_UtoUpload_uploadStatus"] = uploadstatus;
		}

		private bool IsOversizedRequest(HttpRequest request, long contentLength)
		{
			bool flag = (contentLength > UtoUploadConfiguration.MaxRequestLength || contentLength < 0L) && UtoUploadConfiguration.MaxRequestLength != -1L;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = HttpUploadModule.UploadLengthFilter == null;
				result = (!flag2 && HttpUploadModule.UploadLengthFilter.IsOversizedRequest(request));
			}
			return result;
		}

		private byte[] ExtractBoundary(string contentType, Encoding encoding)
		{
			int index = contentType.IndexOf("boundary=");
			bool flag = index > 0;
			byte[] result;
			if (flag)
			{
				result = encoding.GetBytes("--" + contentType.Substring(index + 9));
			}
			else
			{
				result = null;
			}
			return result;
		}

		private bool IsUploadRequest(HttpRequest request)
		{
			bool flag = !request.RequestType.Equals("POST", StringComparison.InvariantCultureIgnoreCase) || string.Compare(request.ContentType, 0, "multipart/form-data", 0, 19, true, CultureInfo.InvariantCulture) != 0;
			return !flag && (HttpUploadModule.UploadRequestFilter == null || HttpUploadModule.UploadRequestFilter.ShouldHandleRequest(request));
		}

		public void Dispose()
		{
		}

		private void ReleaseRequestState(object sender, EventArgs e)
		{
			HttpResponse response = HttpContext.Current.Response;
			HttpRequest request = HttpContext.Current.Request;
			string str = request.Path.Substring(0, request.Path.LastIndexOf("/") + 1);
			bool flag = response.ContentType.Equals("text/html", StringComparison.InvariantCultureIgnoreCase) && !str.Equals("UtoUpload.axd", StringComparison.InvariantCultureIgnoreCase) && !request.UserHostAddress.Equals("127.0.0.1", StringComparison.InvariantCultureIgnoreCase) && !request.UserHostName.Equals("localhost", StringComparison.InvariantCultureIgnoreCase);
			if (flag)
			{
				response.Filter = new FilterStream(response.Filter, response.ContentEncoding);
			}
		}

		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private void InvokeMember(HttpRequest request, byte[] textParts)
		{
			object target = ConfigurationHashThread.CreateInstance("System.Web.HttpRawUploadedContent, System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", new object[]
			{
				textParts.Length,
				textParts.Length
			}, BindingFlags.Instance | BindingFlags.NonPublic);
			Type type = target.GetType();
			type.InvokeMember("AddBytes", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, target, new object[]
			{
				textParts,
				0,
				textParts.Length
			});
			type.InvokeMember("DoneAddingBytes", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, target, null);
			request.GetType().InvokeMember("_rawContent", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField, null, request, new object[]
			{
				target
			});
		}
	}
}
