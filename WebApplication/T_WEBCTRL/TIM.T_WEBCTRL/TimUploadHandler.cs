using System;
using System.Globalization;
using System.Text;
using System.Web;
using System.Xml;

namespace TIM.T_WEBCTRL
{
	public sealed class TimUploadHandler : IHttpHandler
	{
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/xml";
			context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
			context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			context.Response.Cache.SetNoStore();
			context.Response.Cache.SetNoServerCaching();
			context.Response.Cache.SetExpires(DateTime.Now);
			string uploadId = context.Request.QueryString["UploadId"];
			using (XmlTextWriter writer = new XmlTextWriter(context.Response.OutputStream, context.Response.ContentEncoding))
			{
				writer.WriteStartDocument();
				writer.WriteStartElement("uploadStatus");
				UploadStatus uploadStatus = HttpUploadModule.GetUploadStatus(uploadId);
				bool flag = uploadStatus != null;
				if (flag)
				{
					float num = (float)uploadStatus.Position / (float)uploadStatus.ContentLength;
					writer.WriteAttributeString("state", uploadStatus.State.ToString());
					writer.WriteAttributeString("reason", uploadStatus.Reason.ToString());
					bool flag2 = !string.IsNullOrEmpty(uploadStatus.ErrorMessage);
					if (flag2)
					{
						writer.WriteAttributeString("errorMessage", uploadStatus.ErrorMessage);
					}
					writer.WriteAttributeString("percentComplete", (num * 100f).ToString("##0.00", CultureInfo.InvariantCulture));
					writer.WriteAttributeString("contentLengthText", this.SwitchContentlength(uploadStatus.ContentLength));
					writer.WriteAttributeString("transferredLengthText", this.SwitchContentlength(uploadStatus.ContentLength - (uploadStatus.ContentLength - uploadStatus.Position)));
					writer.WriteAttributeString("remainingLengthText", this.SwitchContentlength(uploadStatus.ContentLength - uploadStatus.Position));
					writer.WriteAttributeString("currentFile", uploadStatus.CurrentFileName);
					writer.WriteAttributeString("currentFileIndex", uploadStatus.UploadedFiles.Count.ToString());
					TimeSpan span = DateTime.Now.Subtract(uploadStatus.Start);
					writer.WriteAttributeString("timeElapsedText", this.SwitchTimeSpan(span));
					bool flag3 = uploadStatus.Position > 0L;
					if (flag3)
					{
						double num2 = (double)span.Ticks / (double)uploadStatus.Position;
						double num3 = (double)(uploadStatus.ContentLength - uploadStatus.Position) * num2;
						TimeSpan span2 = new TimeSpan((long)num3);
						writer.WriteAttributeString("timeRemainingText", this.SwitchTimeSpan(span2));
					}
					else
					{
						writer.WriteAttributeString("timeRemainingText", "[calculating]");
					}
					double num4 = (double)uploadStatus.Position / span.TotalSeconds;
					bool flag4 = num >= 1f;
					if (flag4)
					{
						writer.WriteAttributeString("speedText", "正在处理，请稍等...");
					}
					else
					{
						writer.WriteAttributeString("speedText", this.SwitchContentlength((long)num4) + "/sec");
					}
				}
				writer.WriteEndElement();
				writer.WriteEndDocument();
			}
		}

		private string SwitchContentlength(long contentlength)
		{
			bool flag = contentlength <= 999L;
			string result;
			if (flag)
			{
				result = contentlength.ToString() + " bytes";
			}
			else
			{
				bool flag2 = contentlength <= 1022976L;
				if (flag2)
				{
					result = this.SwitchContentlengthDigits((double)contentlength / 1024.0) + " KB";
				}
				else
				{
					bool flag3 = contentlength <= 1047527424L;
					if (flag3)
					{
						result = this.SwitchContentlengthDigits((double)contentlength / 1048576.0) + " MB";
					}
					else
					{
						bool flag4 = contentlength <= 1072668082176L;
						if (flag4)
						{
							result = this.SwitchContentlengthDigits((double)contentlength / 1073741824.0) + " GB";
						}
						else
						{
							bool flag5 = contentlength <= 1098412116148224L;
							if (flag5)
							{
								result = this.SwitchContentlengthDigits((double)contentlength / 1099511627776.0) + " TB";
							}
							else
							{
								bool flag6 = (double)contentlength <= 1.1247740069357814E+18;
								if (flag6)
								{
									result = this.SwitchContentlengthDigits((double)contentlength / 1125899906842624.0) + " PB";
								}
								else
								{
									bool flag7 = (double)contentlength <= 1.1517685831022401E+21;
									if (flag7)
									{
										result = this.SwitchContentlengthDigits((double)contentlength / 1.152921504606847E+18) + " EB";
									}
									else
									{
										bool flag8 = (double)contentlength <= 1.1794110290966939E+24;
										if (flag8)
										{
											result = this.SwitchContentlengthDigits((double)contentlength / 1.1805916207174113E+21) + " ZB";
										}
										else
										{
											result = this.SwitchContentlengthDigits((double)contentlength / 1.2089258196146292E+24) + " YB";
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		private string SwitchContentlengthDigits(double contentlength)
		{
			bool flag = contentlength >= 100.0;
			string result;
			if (flag)
			{
				result = ((int)contentlength).ToString();
			}
			else
			{
				bool flag2 = contentlength >= 10.0;
				if (flag2)
				{
					result = contentlength.ToString("0.0");
				}
				else
				{
					result = contentlength.ToString("0.00");
				}
			}
			return result;
		}

		private string SwitchTimeSpan(TimeSpan timespan)
		{
			StringBuilder builder = new StringBuilder();
			bool flag = timespan.Hours > 0;
			if (flag)
			{
				builder.Append(timespan.Hours);
				builder.Append(" 小时");
			}
			bool flag2 = timespan.Minutes > 0;
			if (flag2)
			{
				bool flag3 = builder.Length > 0;
				if (flag3)
				{
					bool flag4 = timespan.Minutes > 0;
					if (flag4)
					{
						builder.Append(" ");
					}
				}
				builder.Append(timespan.Minutes);
				builder.Append(" 分");
			}
			bool flag5 = timespan.Seconds > 0;
			if (flag5)
			{
				bool flag6 = builder.Length > 0;
				if (flag6)
				{
					builder.Append(" ");
				}
				builder.Append(timespan.Seconds);
				builder.Append(" 秒");
			}
			return builder.ToString();
		}
	}
}
