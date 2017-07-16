using System;
using System.IO;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_KERNEL.Helper;
using TIM.T_KERNEL.Utils;

namespace TIM.T_TEMPLET.DFS
{
	public class FileService
	{
		internal static double GenFileGroupId()
		{
			return (double)TimIdUtils.GenUtoId("DFS_FGID");
		}

		internal static double GenFileId()
		{
			return (double)TimIdUtils.GenUtoId("DFS_FILEID");
		}

		internal static string GetDfsFsId()
		{
			DfsSet dfsSet = DfsSetUtils.GetMainDfsSet();
			bool flag = dfsSet == null;
			if (flag)
			{
				throw new Exception("主文档服务器【DFSMAIN】没有进行配置！");
			}
			return dfsSet.FsId;
		}

		internal static string GenDfsPath(double fileId)
		{
			DfsSet dfsSet = DfsSetUtils.GetMainDfsSet();
			bool flag = dfsSet == null;
			if (flag)
			{
				throw new Exception("主文档服务器【DFSMAIN】没有进行配置！");
			}
			string result = string.Empty;
			long dfsFileName = (long)fileId;
			string partPath = string.Format("{0:X6}", dfsFileName);
			return Path.Combine(dfsSet.PathLocation, partPath.Substring(4, 2), partPath.Substring(2, 2), dfsFileName.ToString());
		}

		internal static DocFileInfo AddFile(string fileSourcePath, string fileName, long fileSize, double groupId)
		{
			DocFileInfo result = null;
			bool flag = groupId == 0.0;
			if (flag)
			{
				groupId = (double)TimIdUtils.GenUtoId("DFS_FGID");
			}
			double fileId = (double)TimIdUtils.GenUtoId("DFS_FILEID");
			string fsId = FileService.GetDfsFsId();
			string dfsFileName = fileName.ToFileName();
			string dfsExtName = fileName.ToFileExtName();
			Database db = LogicContext.GetDatabase();
			db.BeginTrans();
			try
			{
				FileService.InsertDfsFile(fsId, fileId, dfsFileName, dfsExtName, fileSize);
				FileService.InsertDfsGroup(groupId, fileId);
				File.Move(fileSourcePath, FileService.GenDfsPath(fileId));
				result = new DocFileInfo();
				result.FsId = fsId;
				result.FileGroupId = groupId;
				result.FileId = fileId;
				result.FileName = dfsFileName;
				result.ExtName = dfsExtName;
				result.FileSize = fileSize;
				db.CommitTrans();
			}
			catch (Exception ex)
			{
				db.RollbackTrans();
				throw ex;
			}
			return result;
		}

		internal static void InsertDfsFile(string fsId, double fileId, string fileName, string exName, long fileSize)
		{
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Add("INSERT INTO DFSFILE(DFSFILE_FSID,DFSFILE_FILEID,DFSFILE_FILENAME,DFSFILE_EXTNAME,DFSFILE_FILESIZE)");
			hsql.Add("VALUES(:DFSFILE_FSID,:DFSFILE_FILEID,:DFSFILE_FILENAME,:DFSFILE_EXTNAME,:DFSFILE_FILESIZE)");
			hsql.ParamByName("DFSFILE_FSID").Value = fsId;
			hsql.ParamByName("DFSFILE_FILEID").Value = fileId;
			hsql.ParamByName("DFSFILE_FILENAME").Value = fileName;
			hsql.ParamByName("DFSFILE_EXTNAME").Value = exName;
			hsql.ParamByName("DFSFILE_FILESIZE").Value = fileSize;
			int affectedRows = db.ExecSQL(hsql);
			bool flag = affectedRows < 1;
			if (flag)
			{
				throw new Exception("文件服务器异常！");
			}
		}

		internal static void InsertDfsGroup(double fileGroupId, double fileId)
		{
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Add("INSERT INTO DFSGROUP(DFSGROUP_GROUPID,DFSGROUP_FILEID)");
			hsql.Add("VALUES(:DFSGROUP_GROUPID,:DFSGROUP_FILEID)");
			hsql.AddParam("DFSGROUP_GROUPID", TimDbType.Float, 0, fileGroupId);
			hsql.AddParam("DFSGROUP_FILEID", TimDbType.Float, 0, fileId);
			int affectedRows = db.ExecSQL(hsql);
			bool flag = affectedRows < 1;
			if (flag)
			{
				throw new Exception("文件服务器异常！");
			}
		}

		internal static void CheckDfsFile(double fileId)
		{
		}

		internal static bool DeleteDfsGroup(double fileGroupId)
		{
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Add("DELETE FROM DFSGROUP");
			hsql.Add("WHERE DFSGROUP_GROUPID = :DFSGROUP_GROUPID");
			hsql.AddParam("DFSGROUP_GROUPID", TimDbType.Float, 0, fileGroupId);
			int affectedRows = db.ExecSQL(hsql);
			bool flag = affectedRows < 1;
			return !flag;
		}

		internal static bool DeleteDfsGroupFile(double fileGroupId, double fileId)
		{
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Add("DELETE FROM DFSGROUP");
			hsql.Add("WHERE DFSGROUP_GROUPID = :DFSGROUP_GROUPID");
			hsql.Add("AND DFSGROUP_FILEID = :DFSGROUP_FILEID");
			hsql.AddParam("DFSGROUP_GROUPID", TimDbType.Float, 0, fileGroupId);
			hsql.AddParam("DFSGROUP_FILEID", TimDbType.Float, 0, fileId);
			int affectedRows = db.ExecSQL(hsql);
			bool flag = affectedRows < 1;
			return !flag;
		}

		internal static bool DeleteFile(double fileGroupId, double fileId)
		{
			return FileService.DeleteDfsGroupFile(fileGroupId, fileId);
		}

		internal static string GetFilePath(double fileId)
		{
			return FileService.GenDfsPath(fileId);
		}

		public static double ShareFileGroup(double fileGroupId)
		{
			double shareGroupId = (double)TimIdUtils.GenUtoId("DFS_FGID");
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Add("INSERT INTO DFSGROUP(DFSGROUP_GROUPID,DFSGROUP_FILEID)");
			hsql.Add("SELECT :SHARE_DFSGROUP_GROUPID,DFSGROUP_FILEID");
			hsql.Add("FROM DFSGROUP");
			hsql.Add("WHERE DFSGROUP_GROUPID = :DFSGROUP_GROUPID");
			hsql.AddParam("DFSGROUP_GROUPID", TimDbType.Float, 0, fileGroupId);
			hsql.AddParam("SHARE_DFSGROUP_GROUPID", TimDbType.Float, 0, shareGroupId);
			db.ExecSQL(hsql);
			return shareGroupId;
		}

		public static double ShareFile(double fileId)
		{
			double shareGroupId = (double)TimIdUtils.GenUtoId("DFS_FGID");
			Database db = LogicContext.GetDatabase();
			HSQL hsql = new HSQL(db);
			hsql.Add("INSERT INTO DFSGROUP(DFSGROUP_GROUPID,DFSGROUP_FILEID");
			hsql.Add("VALUES(:DFSGROUP_GROUPID,:DFSGROUP_FILEID)");
			hsql.AddParam("DFSGROUP_GROUPID", TimDbType.Float, 0, shareGroupId);
			hsql.AddParam("DFSGROUP_FILEID", TimDbType.Float, 0, fileId);
			db.ExecSQL(hsql);
			return shareGroupId;
		}
	}
}
