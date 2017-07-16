using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[Description("文件上传进度条"), ToolboxData("<{0}:TimUploadProgressBar runat=\"server\"></{0}:TimUploadProgressBar>")]
	public sealed class TimUploadProgressBar : WebControl, INamingContainer
	{
		private string _uploadID;

		private bool _uploadFilesInfoElementVisible = true;

		private bool _uploadCurrentInfoElementVisible = true;

		private bool _uploadSpeedInfoElementVisible = true;

		private bool _uploadTimeInfoElementVisible = true;

		private bool _uploadProgressInfoElementVisible = true;

		internal string UploadID
		{
			get
			{
				TimUploadProgressBar current = TimUploadProgressBar.GetCurrent(this.Page);
				bool flag = current != null;
				string result;
				if (flag)
				{
					result = current.UploadID.ToString();
				}
				else
				{
					result = this._uploadID;
				}
				return result;
			}
		}

		internal string ProgressDisplayClientID
		{
			get
			{
				TimUploadProgressBar current = TimUploadProgressBar.GetCurrent(this.Page);
				bool flag = current != null;
				string result;
				if (flag)
				{
					result = current.ClientID;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(true), Description("所有上传文件信息区域是否隐藏")]
		public bool UploadFilesInfoElementVisible
		{
			get
			{
				return this._uploadFilesInfoElementVisible;
			}
			set
			{
				this._uploadFilesInfoElementVisible = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(true), Description("当前上传信息区域是否隐藏")]
		public bool UploadCurrentInfoElementVisible
		{
			get
			{
				return this._uploadCurrentInfoElementVisible;
			}
			set
			{
				this._uploadCurrentInfoElementVisible = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(true), Description("当前上传速度区域是否隐藏")]
		public bool UploadSpeedInfoElementVisible
		{
			get
			{
				return this._uploadSpeedInfoElementVisible;
			}
			set
			{
				this._uploadSpeedInfoElementVisible = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(true), Description("上传时间信息区域是否隐藏")]
		public bool UploadTimeInfoElementVisible
		{
			get
			{
				return this._uploadTimeInfoElementVisible;
			}
			set
			{
				this._uploadTimeInfoElementVisible = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(true), Description("上传进度信息区域是否隐藏")]
		public bool UploadProgressInfoElementVisible
		{
			get
			{
				return this._uploadProgressInfoElementVisible;
			}
			set
			{
				this._uploadProgressInfoElementVisible = value;
			}
		}

		public TimUploadProgressBar()
		{
			base.Load += new EventHandler(this.GetUploadId);
			base.PreRender += new EventHandler(this.ReRenderRegiste);
			base.Unload += new EventHandler(this.UnloadID);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			writer.Write("<div id=\"" + this.ID + "\" style=\"display: none\">");
			writer.Write("<table width=\"100%\" cellpadding=0 cellspacing=0>");
			bool uploadFilesInfoElementVisible = this.UploadFilesInfoElementVisible;
			if (uploadFilesInfoElementVisible)
			{
				writer.Write("<tr>");
				writer.Write("<td>");
				writer.Write("文件总大小： 共<span class=\"sufilecount\" id=\"" + this.ID + "_filecountelementtext\"> </span>");
				writer.Write("个文件, <span class=\"sucontentlengthtext\" id=\"" + this.ID + "_contentlengthtext\"></span>");
				writer.Write("</td>");
				writer.Write("</tr>");
			}
			bool uploadCurrentInfoElementVisible = this.UploadCurrentInfoElementVisible;
			if (uploadCurrentInfoElementVisible)
			{
				writer.Write("<tr>");
				writer.Write("<td>");
				writer.Write("正在上传： <span class=\"sucurrentfilename\" id=\"" + this.ID + "_currentfilenameelement\"></span>");
				writer.Write(", 文件 <span class=\"sucurrentfileindex\" id=\"" + this.ID + "_currentfileindex\"></span>");
				writer.Write("共 <span class=\"sufilecount\" id=\"" + this.ID + "_filecountelement\"></span>");
				writer.Write("</td>");
				writer.Write("</tr>");
			}
			bool uploadSpeedInfoElementVisible = this.UploadSpeedInfoElementVisible;
			if (uploadSpeedInfoElementVisible)
			{
				writer.Write("<tr>");
				writer.Write("<td>");
				writer.Write("上传速度： <span class=\"suspeedtext\" id=\"" + this.ID + "_speedtextelement\"></span>");
				writer.Write("</td>");
				writer.Write("</tr>");
			}
			bool uploadTimeInfoElementVisible = this.UploadTimeInfoElementVisible;
			if (uploadTimeInfoElementVisible)
			{
				writer.Write("<tr>");
				writer.Write("<td>");
				writer.Write("剩余时间： <span class=\"sutimeremainingtext\" id=\"" + this.ID + "_timeremainingtextelement\"></span>");
				writer.Write("</td>");
				writer.Write("</tr>");
			}
			bool uploadProgressInfoElementVisible = this.UploadProgressInfoElementVisible;
			if (uploadProgressInfoElementVisible)
			{
				writer.Write("<tr>");
				writer.Write("<td>");
				writer.Write("<div style=\"border: #449cde 1px solid; height: 16px;overflow: hidden;\">");
				bool designMode = base.DesignMode;
				if (designMode)
				{
					writer.Write("<div class=\"suprogressbar\" id=\"" + this.ID + "_uploadprogressbarelement\" style=\"width: 20%; height: 16px; background-color: #449cde\"></div>");
				}
				else
				{
					writer.Write("<div class=\"suprogressbar\" id=\"" + this.ID + "_uploadprogressbarelement\" style=\"width: 0%; height: 16px; background-color: #449cde\"></div>");
				}
				writer.Write("<div style=\"position: relative; top: -16px; text-align: center; overflow: hidden; font-size : 9pt\">");
				writer.Write("<span class=\"supercentcompletetext\" id=\"" + this.ID + "_percentcompletetextelement\">10%</span>");
				writer.Write("</div>");
				writer.Write("</div>");
				writer.Write("</td>");
				writer.Write("</tr>");
			}
			writer.Write("</TABLE>");
			writer.Write("</DIV>");
		}

		private void UnloadID(object sender, EventArgs e)
		{
			bool flag = TimUploadProgressBar.GetCurrent(this.Page) == null && this.UploadID != null;
			if (flag)
			{
				HttpUploadModule.RemoveStatus(this.UploadID);
			}
		}

		private void ReRenderRegiste(object sender, EventArgs e)
		{
			string path = this.Page.ResolveClientUrl("~/Scripts/Tim/");
			HtmlForm HtmlForm = this.Page.Form;
			bool flag = this.ProgressDisplayClientID == null;
			if (flag)
			{
				HtmlForm.Enctype = "multipart/form-data";
			}
			string arrayValue = string.Concat(new string[]
			{
				"new TimUploadProgressBar(\"",
				this.ID,
				"\", \"",
				base.ResolveUrl("~/UtoUpload.ashx"),
				"\", \"",
				HtmlForm.ClientID
			});
			bool flag2 = this.UploadID != null;
			if (flag2)
			{
				arrayValue = arrayValue + "\", \"" + this.UploadID;
			}
			bool flag3 = this.ProgressDisplayClientID != null;
			if (flag3)
			{
				arrayValue = arrayValue + "\", \"" + this.ProgressDisplayClientID;
			}
			arrayValue += "\")";
			bool flag4 = !this.Page.ClientScript.IsClientScriptBlockRegistered("UtoUpload");
			if (flag4)
			{
				this.Page.ClientScript.RegisterClientScriptBlock(typeof(TimUploadProgressBar), "UtoUpload", string.Format("<script type='text/javascript' src='{0}UtoUpload.js?v={1}'></script>\n", path, TimCtrlUtils.Md5Version));
			}
			bool flag5 = !this.Page.ClientScript.IsClientScriptBlockRegistered("TimUploadProgressBar");
			if (flag5)
			{
				this.Page.ClientScript.RegisterClientScriptBlock(typeof(TimUploadProgressBar), "TimUploadProgressBar", string.Format("<script type='text/javascript' src='{0}TimUploadProgressBar.js?v={1}'></script>\n", path, TimCtrlUtils.Md5Version));
			}
			this.Page.ClientScript.RegisterArrayDeclaration("UtoUpload_ProgressDisplayList", arrayValue);
			bool flag6 = this.ProgressDisplayClientID == null;
			if (flag6)
			{
				this.Page.ClientScript.RegisterOnSubmitStatement(typeof(TimUploadProgressBar), "ProgressDisplaySubmit", "UtoUpload_Submit()");
			}
		}

		private void GetUploadId(object sender, EventArgs e)
		{
			this._uploadID = this.Page.Request.QueryString["UploadId"];
		}

		public static TimUploadProgressBar GetCurrent(Page page)
		{
			bool flag = page == null;
			if (flag)
			{
				throw new ArgumentNullException("page");
			}
			return page.Items[typeof(TimUploadProgressBar)] as TimUploadProgressBar;
		}
	}
}
