using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Helper;
using TIM.T_KERNEL.Security;
using TIM.T_KERNEL.Utils;

namespace TIM.T_KERNEL.DbTableCache
{
	public class UserUtils
	{
		internal static User GetObject(DataRow row)
		{
			return new User
			{
				UserId = row["USERS_USERID"].ToString().Trim(),
				UserName = row["USERS_USERNAME"].ToString().Trim(),
				Password = row["USERS_PASSWORD"].ToString().Trim(),
				Abbr = row["USERS_ABBR"].ToString().Trim(),
				Type = ((row["USERS_TYPE"].ToString().Trim() == "S") ? UserType.S : UserType.U),
				Tel = row["USERS_TEL"].ToString().Trim(),
				Mobile = row["USERS_MOBILE"].ToString().Trim(),
				Email = row["USERS_EMAIL"].ToString().Trim(),
				Disabled = (row["USERS_TYPE"].ToString().Trim() == "Y"),
				PasswordSetTime = row["USERS_PASSWORDSETTIME"].ToString().Trim().ToDateTime(),
				Mac = row["USERS_MAC"].ToString().Trim(),
				CreaterId = row["CREATERID"].ToString().Trim(),
				Creater = row["CREATER"].ToString().Trim(),
				CreatedTime = row["CREATEDTIME"].ToString().Trim().ToDateTime(),
				ModifierId = row["MODIFIERID"].ToString().Trim(),
				Modifier = row["MODIFIER"].ToString().Trim(),
				ModifiedTime = row["MODIFIEDTIME"].ToString().Trim().ToDateTime()
			};
		}

		public static User GetUser(string userId)
		{
			User user = null;
			UserCache userCache = (UserCache)new UserCache().GetData();
			int index = userCache.dvUserBy_UserId.Find(userId);
			bool flag = index >= 0;
			if (flag)
			{
				User user2 = new User();
				user = UserUtils.GetObject(userCache.dvUserBy_UserId[index].Row);
			}
			return user;
		}

		public static List<User> GetUserByName(string userName)
		{
			List<User> list = new List<User>();
			DataRowView[] array = ((UserCache)new UserCache().GetData()).dvUserBy_UserName.FindRows(userName);
			for (int i = 0; i < array.Length; i++)
			{
				DataRowView dataRowView = array[i];
				User user = new User();
				User @object = UserUtils.GetObject(dataRowView.Row);
				list.Add(@object);
			}
			return list;
		}

		public static User GetUserByMac(string macAddress)
		{
			User user = null;
			UserCache userCache = (UserCache)new UserCache().GetData();
			int index = userCache.dvUserBy_Mac.Find(macAddress);
			bool flag = index >= 0;
			if (flag)
			{
				User user2 = new User();
				user = UserUtils.GetObject(userCache.dvUserBy_Mac[index].Row);
			}
			return user;
		}

		public static List<User> GetAllUser()
		{
			List<User> list = new List<User>();
			foreach (DataRow row in ((UserCache)new UserCache().GetData()).dtUser.Rows)
			{
				User user = new User();
				User @object = UserUtils.GetObject(row);
				list.Add(@object);
			}
			return list;
		}

		public static bool Login(string userId, string password, string clientIp)
		{
			bool flag = string.IsNullOrEmpty(userId);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				userId = userId.ToUpper();
				User user = UserUtils.GetUser(userId);
				bool flag2 = user == null;
				if (flag2)
				{
					throw new Exception("用户名或密码错误！");
				}
				bool disabled = user.Disabled;
				if (disabled)
				{
					throw new Exception("当前用户名已被停用！");
				}
				bool flag3 = user.Password != PasswordSec.Encode(userId, password);
				if (flag3)
				{
					throw new Exception("用户名或密码错误！");
				}
				LogicSession logicSession = AuthUtils.SignIn(userId, clientIp);
				bool flag4 = logicSession != null;
				if (flag4)
				{
					logicSession.UserName = user.UserName;
					LogicContext current = LogicContext.Current;
					bool flag5 = current != null;
					if (flag5)
					{
						current.SetLogicSession(logicSession);
					}
				}
				result = true;
			}
			return result;
		}

		public static bool Logout()
		{
			AuthUtils.SignOut();
			return true;
		}

		public static bool UpdatePsw(string userId, string psw, string newPsw, string confirmNewPsw)
		{
			bool flag = string.IsNullOrEmpty(userId);
			if (flag)
			{
				throw new Exception("用户名或密码错误！");
			}
			bool flag2 = newPsw != confirmNewPsw;
			if (flag2)
			{
				throw new Exception("两次输入的密码不匹配！");
			}
			User user = UserUtils.GetUser(userId);
			bool flag3 = user == null;
			if (flag3)
			{
				throw new Exception("用户名或密码错误！");
			}
			bool disabled = user.Disabled;
			if (disabled)
			{
				throw new Exception("当前用户名已被停用！");
			}
			SystemInfo systemInfo = SystemInfoUtils.GetSystemInfo();
			bool flag4 = systemInfo != null && systemInfo.PswLength > 0 && systemInfo.PswLength > newPsw.Length;
			if (flag4)
			{
				throw new Exception(string.Format("密码长度小于系统指定最短长度({0})！", systemInfo.PswLength));
			}
			bool flag5 = user.Password != PasswordSec.Encode(userId, psw);
			if (flag5)
			{
				throw new Exception("用户名或密码错误！");
			}
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Clear();
			sql.Add("UPDATE USERS SET");
			sql.Add("USERS_PASSWORD = :USERS_PASSWORD");
			sql.Add("WHERE USERS_USERID = :USERS_USERID");
			sql.ParamByName("USERS_PASSWORD").Value = PasswordSec.Encode(userId, newPsw);
			sql.ParamByName("USERS_USERID").Value = userId;
			bool flag6 = database.ExecSQL(sql) != 1;
			if (flag6)
			{
				throw new Exception(string.Format("用户({0})密码修改失败！", userId));
			}
			CacheEvent.TableIsUpdated("USERS");
			return true;
		}

		public static bool ResetPsw(string userId, string resetPsw)
		{
			Database database = LogicContext.GetDatabase();
			HSQL sql = new HSQL(database);
			sql.Clear();
			sql.Add("UPDATE USERS SET");
			sql.Add("USERS_PASSWORD = :USERS_PASSWORD");
			sql.Add("WHERE USERS_USERID = :USERS_USERID");
			sql.ParamByName("USERS_PASSWORD").Value = PasswordSec.Encode(userId, resetPsw);
			sql.ParamByName("USERS_USERID").Value = userId;
			bool flag = database.ExecSQL(sql) != 1;
			if (flag)
			{
				throw new Exception(string.Format("用户({0})密码修改失败！", userId));
			}
			CacheEvent.TableIsUpdated("USERS");
			return true;
		}

		public static bool GenMobileDeviceRec(string mac, string remark)
		{
			bool flag = string.IsNullOrWhiteSpace(mac);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Database database = LogicContext.GetDatabase();
				HSQL sql = new HSQL(database);
				sql.Clear();
				sql.Add("SELECT MOBILEDEVICE_DEVICEID FROM MOBILEDEVICE WHERE  MOBILEDEVICE_MAC = :MOBILEDEVICE_MAC");
				sql.ParamByName("MOBILEDEVICE_MAC").Value = mac;
				object obj = database.ExecScalar(sql);
				bool flag2 = obj != null;
				if (flag2)
				{
					sql.Clear();
					sql.Add("UPDATE MOBILEDEVICE SET");
					sql.Add(" MOBILEDEVICE_PROCESSED = 'N'");
					sql.Add(",MODIFIERID = :MODIFIERID");
					sql.Add(",MODIFIER = :MODIFIER");
					sql.Add(",MODIFIEDTIME = :MODIFIEDTIME");
					sql.Add("WHERE MOBILEDEVICE_DEVICEID = :MOBILEDEVICE_DEVICEID");
					sql.ParamByName("MOBILEDEVICE_DEVICEID").Value = obj;
					sql.ParamByName("MODIFIERID").Value = "ADMIN";
					sql.ParamByName("MODIFIER").Value = "管理员";
					sql.ParamByName("MODIFIEDTIME").Value = DateTime.Now;
				}
				else
				{
					sql.Clear();
					sql.Add("INSERT INTO MOBILEDEVICE(MOBILEDEVICE_DEVICEID,MOBILEDEVICE_MAC,MOBILEDEVICE_PROCESSED,MOBILEDEVICE_REMARK");
					sql.Add(",CREATERID,CREATER,CREATEDTIME,MODIFIERID,MODIFIER,MODIFIEDTIME)");
					sql.Add("VALUES");
					sql.Add("(:MOBILEDEVICE_DEVICEID,:MOBILEDEVICE_MAC,:MOBILEDEVICE_PROCESSED,:MOBILEDEVICE_REMARK");
					sql.Add(",:CREATERID,:CREATER,:CREATEDTIME,:MODIFIERID,:MODIFIER,:MODIFIEDTIME)");
					sql.ParamByName("MOBILEDEVICE_DEVICEID").Value = TimIdUtils.GenUtoId("MDEVICEID", "M", 8);
					sql.ParamByName("MOBILEDEVICE_MAC").Value = mac;
					sql.ParamByName("MOBILEDEVICE_PROCESSED").Value = "N";
					sql.ParamByName("MOBILEDEVICE_REMARK").Value = remark;
					sql.ParamByName("CREATERID").Value = "ADMIN";
					sql.ParamByName("CREATER").Value = "管理员";
					sql.ParamByName("CREATEDTIME").Value = DateTime.Now;
					sql.ParamByName("MODIFIERID").Value = "ADMIN";
					sql.ParamByName("MODIFIER").Value = "管理员";
					sql.ParamByName("MODIFIEDTIME").Value = DateTime.Now;
					database.ExecSQL(sql);
				}
				result = true;
			}
			return result;
		}
	}
}
