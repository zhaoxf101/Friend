using System;
using System.Web;
using System.Web.UI.WebControls;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Helper;
using TIM.T_TEMPLET.DFS;
using TIM.T_TEMPLET.Master;
using TIM.T_TEMPLET.Page;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.CommForm
{
	public class DocFileHandle : TListingBase
	{
		private DocFile _MasterEntity = new DocFile();

		private DocFileListView filelist;

		protected FileUpload fileUploadFile;

		protected TimButtonMenu btnUploadFile;

		protected TimButtonMenu btnDownloadFile;

		protected TimButtonMenu btnDeleteFile;

		protected TimGridView gvMaster;

		protected TimUploadProgressBar ProgressBar;

		protected TimHiddenField hidFileGroup;

		protected TimButton btnPostFile;

		public new TListing Master
		{
			get
			{
				return (TListing)base.Master;
			}
		}

		protected override void InitModuleInfo()
		{
			base.MdId = 101021003;
			base.MdName = "文档附件";
			base.Width = 900;
			base.Height = 600;
		}

		protected override void InitTemplet()
		{
			base.CurMaster = this.Master;
			base.CurEntity = this._MasterEntity;
			base.CurGrid = this.gvMaster;
			base.DataKeyNames = new string[]
			{
				"DFSGROUP_GROUPID",
				"DFSGROUP_FILEID",
				"DFSFILE_FSID",
				"DFSFILE_FILEID",
				"DFSFILE_FILENAME",
				"DFSFILE_EXTNAME"
			};
		}

		protected override void SetCtrlState()
		{
			this.SetMenu_HideAllStdBtn();
			bool flag = !this.filelist.Enabled;
			if (flag)
			{
				this.btnUploadFile.Enabled = false;
				this.btnDeleteFile.Enabled = false;
			}
		}

		protected override void OnInitComplete()
		{
			TimButtonMenu timButtonMenu = this.btnDownloadFile;
			timButtonMenu.OnClientClick = string.Concat(new string[]
			{
				timButtonMenu.OnClientClick,
				"if ($('#",
				base.CurGrid.ClientID,
				"') == null) {alert('当前没有可选记录！'); return false;} if ($('#",
				base.CurGrid.ClientID,
				"').attr('selectedIndex') == -1) {alert('请选择处理记录！'); return false;}"
			});
			timButtonMenu = this.btnDeleteFile;
			timButtonMenu.OnClientClick = string.Concat(new string[]
			{
				timButtonMenu.OnClientClick,
				"if ($('#",
				base.CurGrid.ClientID,
				"') == null) {alert('当前没有可选记录！'); return false;} if ($('#",
				base.CurGrid.ClientID,
				"').attr('selectedIndex') == -1) {alert('请选择处理记录！'); return false;}"
			});
			TimButtonMenu expr_B3 = this.btnDeleteFile;
			expr_B3.OnClientClick += "if (confirm('您确定要删除所选附件？') == false) return false;";
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected override bool OnPreQuery()
		{
			this._MasterEntity.QueryFileGroupId = this.hidFileGroup.Value;
			return true;
		}

		protected override void OnLoadRecord()
		{
			bool flag = !base.IsPostBack;
			if (flag)
			{
				base.CurMaster_OnQuery(null, null);
			}
		}

		protected override void OnPreLoad()
		{
			this.filelist = new DocFileListView();
			this.filelist.Enabled = base.PageParam.GetString("ENABLED").ToBool();
			this.filelist.BusinessTable = base.PageParam.GetString("BUSINESSTABLE");
			this.filelist.FileGroupIdField = base.PageParam.GetString("FILEGROUPIDFIELD");
			this.filelist.FilesField = base.PageParam.GetString("FILESFIELD");
			this.filelist.FileGroupClientId = base.PageParam.GetString("FILEGROUPIDCLIENTID");
			this.filelist.FilesClientId = base.PageParam.GetString("FILESCLIENTID");
			this.filelist.MaxFileSize = (long)base.PageParam.GetString("MAXFILESIZE").ToInt();
			this.filelist.MaxFileGroupSize = (long)base.PageParam.GetString("MAXFILEGROUPSIZE").ToInt();
			this.filelist.ExecOn = base.PageParam.GetString("EXECON");
			bool flag = !base.IsPostBack;
			if (flag)
			{
				this.filelist.FileGroupId = base.PageParam.GetString("FILEGROUPID").ToDouble();
				this.hidFileGroup.Value = this.filelist.FileGroupId.ToString();
			}
			else
			{
				this.filelist.FileGroupId = this.hidFileGroup.Value.ToDouble();
			}
			base.CurGrid.OnClientDblClick = "return false;";
		}

		protected override void OnLoadComplete()
		{
		}

		protected override void CurGrid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			bool flag = e.Row.RowType == DataControlRowType.DataRow;
			if (flag)
			{
				e.Row.Cells[2].Text = e.Row.Cells[2].Text.FormatFileSize();
			}
		}

		protected void gvMaster_RowDoubleClick(object sender, UtoRowDoubleClickEventArgs e)
		{
			string fileName = base.CurGrid.DataKeys[base.CurGrid.SelectedIndex]["DFSFILE_FILENAME"].ToString();
			fileName = fileName.Replace(";", "_").Replace(",", "_");
			fileName = HttpContext.Current.Server.UrlEncode(fileName);
			fileName = fileName.Replace("'", "\\'");
			string fileExtName = base.CurGrid.DataKeys[base.CurGrid.SelectedIndex]["DFSFILE_EXTNAME"].ToString();
			fileExtName = HttpContext.Current.Server.UrlEncode(fileExtName);
			string downloadScript = string.Format("dfsDownloadFile('DfsFileDownload.ashx?FILEID={0}&FILENAME={1}&EXTNAME={2}&TK={3}');", new object[]
			{
				base.CurGrid.DataKeys[base.CurGrid.SelectedIndex]["DFSFILE_FILEID"].ToString(),
				fileName,
				fileExtName,
				DateTime.Now.Ticks.ToString()
			});
			base.RegisterScript("downloadFile", downloadScript);
			base.PlaceUpdateContent();
		}

		protected void btnDownloadFile_Click(object sender, EventArgs e)
		{
			string fileName = base.CurGrid.DataKeys[base.CurGrid.SelectedIndex]["DFSFILE_FILENAME"].ToString();
			fileName = fileName.Replace(";", "_").Replace(",", "_");
			fileName = HttpContext.Current.Server.UrlEncode(fileName);
			fileName = fileName.Replace("'", "\\'");
			string fileExtName = base.CurGrid.DataKeys[base.CurGrid.SelectedIndex]["DFSFILE_EXTNAME"].ToString();
			fileExtName = HttpContext.Current.Server.UrlEncode(fileExtName);
			string downloadScript = string.Format("dfsDownloadFile('DfsFileDownload.ashx?FILEID={0}&FILENAME={1}&EXTNAME={2}&TK={3}');", new object[]
			{
				base.CurGrid.DataKeys[base.CurGrid.SelectedIndex]["DFSFILE_FILEID"].ToString(),
				fileName,
				fileExtName,
				DateTime.Now.Ticks.ToString()
			});
			base.RegisterScript("downloadFile", downloadScript);
		}

		protected void btnDeleteFile_Click(object sender, EventArgs e)
		{
			bool flag = !FileService.DeleteFile(base.CurGrid.DataKeys[base.CurGrid.SelectedIndex]["DFSGROUP_GROUPID"].ToString().ToDouble(), base.CurGrid.DataKeys[base.CurGrid.SelectedIndex]["DFSFILE_FILEID"].ToString().ToDouble());
			if (flag)
			{
				base.PromptDialog("文件已被他人删除，请刷新后查看！");
			}
			else
			{
				this.UpdateBusinessFileGroup();
				int files = this.UpdateBusinessFiles();
				this.UpdateTempletFileFieldRI(files);
				base.CurMaster_OnQuery(null, null);
			}
		}

		protected void btnPostFile_Click(object sender, EventArgs e)
		{
			int files = 0;
			UploadStatus uploadstatus = HttpUploadModule.GetUploadStatus();
			bool flag = uploadstatus.Reason == UploadTerminationReason.NotTerminated;
			if (flag)
			{
				foreach (UploadedFile file in uploadstatus.GetUploadedFiles())
				{
					string clientFileName = file.ClientName;
					bool flag2 = file.ContentLength.Equals(0L);
					if (flag2)
					{
						base.PromptDialog(string.Format("[{0}]文件大小为0字节，系统不支持文件大小为0字节的文件上传，请重新选择文件上传。", clientFileName));
						return;
					}
					bool flag3 = !this.filelist.MaxFileSize.Equals(0.0) && file.ContentLength > this.filelist.MaxFileSize;
					if (flag3)
					{
						base.PromptDialog(string.Format("[{0}]文件大小已超出规定的 {1}上限，上传失败。", clientFileName, this.filelist.MaxFileSize.FormatFileSize()));
						return;
					}
					DocFileInfo fileInfo = FileService.AddFile(file.ServerPath, clientFileName, file.ContentLength, this.hidFileGroup.Value.ToDouble());
					this.hidFileGroup.Value = fileInfo.FileGroupId.ToString();
					this.filelist.FileGroupId = fileInfo.FileGroupId;
					this.UpdateBusinessFileGroup();
					files = this.UpdateBusinessFiles();
				}
			}
			this.UpdateTempletFileFieldRI(files);
			base.CurMaster_OnQuery(null, null);
		}

		private int UpdateBusinessFiles()
		{
			int files = 0;
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Add("SELECT COUNT(DFSGROUP_FILEID) FROM DFSGROUP WHERE DFSGROUP_GROUPID = :DFSGROUP_GROUPID");
			hsql.AddParam("DFSGROUP_GROUPID", TimDbType.Float, 0, this.hidFileGroup.Value.ToDouble());
			object objFiles = db.ExecScalar(hsql);
			bool flag = objFiles != null;
			if (flag)
			{
				files = objFiles.ToString().ToInt();
				hsql.Clear();
				hsql.Add(string.Format("UPDATE {0} SET {1} = {2} WHERE {3}", new object[]
				{
					this.filelist.BusinessTable,
					this.filelist.FilesField,
					files.ToString(),
					this.filelist.ExecOn
				}));
				db.ExecSQL(hsql);
			}
			return files;
		}

		private void UpdateBusinessFileGroup()
		{
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Add(string.Format("UPDATE {0} SET {1} = {2} WHERE {3}", new object[]
			{
				this.filelist.BusinessTable,
				this.filelist.FileGroupIdField,
				this.hidFileGroup.Value,
				this.filelist.ExecOn
			}));
			db.ExecSQL(hsql);
		}

		private void UpdateTempletFileFieldRI(int files)
		{
			string runscript = string.Concat(new string[]
			{
				"function SetFileField(){$('#SiteTemplet_Templet_FileGroup', frameElement.dialog.options.opener.document).val('",
				this.filelist.FileGroupId.ToString(),
				"');$('#SiteTemplet_Templet_Files', frameElement.dialog.options.opener.document).val('",
				files.ToString(),
				"');$('#",
				this.filelist.FileGroupClientId,
				"', frameElement.dialog.options.opener.document).val('",
				this.filelist.FileGroupId.ToString(),
				"');$('#",
				this.filelist.FilesClientId,
				"', frameElement.dialog.options.opener.document).val('",
				files.ToString(),
				"');$('#SiteBody_NestedBody_btnAttachButtonTd', frameElement.dialog.options.opener.document).html('附件[",
				files.ToString(),
				"]');}"
			});
			base.RegisterScript("SetFileField", runscript + "SetFileField();");
		}
	}
}
