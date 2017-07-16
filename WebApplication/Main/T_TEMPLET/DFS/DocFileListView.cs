using System;
using System.Web.UI;
using TIM.T_KERNEL.Helper;
using TIM.T_TEMPLET.Page;

namespace TIM.T_TEMPLET.DFS
{
	internal class DocFileListView
	{
		public double FileGroupId
		{
			get;
			set;
		}

		public bool Enabled
		{
			get;
			set;
		}

		public string BusinessTable
		{
			get;
			set;
		}

		public string FileGroupIdField
		{
			get;
			set;
		}

		public string FilesField
		{
			get;
			set;
		}

		public string FileGroupClientId
		{
			get;
			set;
		}

		public string FilesClientId
		{
			get;
			set;
		}

		public long MaxFileSize
		{
			get;
			set;
		}

		public long MaxFileGroupSize
		{
			get;
			set;
		}

		public string ExecOn
		{
			get;
			set;
		}

		public PageParameter BuildPageParameter(Control page)
		{
			PageParameter pageParams = new PageParameter();
			pageParams.UrlPath = page.ResolveUrl("~") + "T_TEMPLET/CommForm/DocFileHandle.aspx";
			pageParams.AddString("FILEGROUPID", this.FileGroupId.ToString());
			pageParams.AddString("ENABLED", this.Enabled.ToYOrN());
			pageParams.AddString("BUSINESSTABLE", this.BusinessTable);
			pageParams.AddString("FILEGROUPIDFIELD", this.FileGroupIdField);
			pageParams.AddString("FILESFIELD", this.FilesField);
			pageParams.AddString("FILEGROUPIDCLIENTID", this.FileGroupClientId);
			pageParams.AddString("FILESCLIENTID", this.FilesClientId);
			pageParams.AddString("MAXFILESIZE", this.MaxFileSize.ToString());
			pageParams.AddString("MAXFILEGROUPSIZE", this.MaxFileGroupSize.ToString());
			pageParams.AddString("EXECON", this.ExecOn);
			return pageParams;
		}
	}
}
