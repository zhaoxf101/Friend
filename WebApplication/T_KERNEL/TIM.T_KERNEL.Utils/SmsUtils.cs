using System;
using System.Collections.Generic;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.DbTableCache;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.Utils
{
	public class SmsUtils
	{
		public static int GetSmsOutId()
		{
			return TimIdUtils.GenUtoId("SMSOUTID");
		}

		public static void Send(string smsFormatUsers, string content)
		{
			bool flag = string.IsNullOrWhiteSpace(smsFormatUsers) || string.IsNullOrWhiteSpace(content);
			if (!flag)
			{
				Database database = LogicContext.GetDatabase();
				int smsOutId = SmsUtils.GetSmsOutId();
				Dictionary<string, User> dictionary = smsFormatUsers.ToSmsUsers();
				try
				{
					HSQL sql = new HSQL(database);
					sql.Clear();
					database.BeginTrans();
					sql.Add("INSERT INTO SMSOUT(SMSOUT_SMSOUTID,SMSOUT_TOUSERS,SMSOUT_TOMOBILES,SMSOUT_CONTENT,SMSOUT_STATUS)");
					sql.Add("VALUES(:SMSOUT_SMSOUTID,:SMSOUT_TOUSERS,:SMSOUT_TOMOBILES,:SMSOUT_CONTENT,:SMSOUT_STATUS)");
					sql.ParamByName("SMSOUT_SMSOUTID").Value = smsOutId;
					sql.ParamByName("SMSOUT_TOUSERS").Value = smsFormatUsers;
					sql.ParamByName("SMSOUT_TOMOBILES").Value = "";
					sql.ParamByName("SMSOUT_CONTENT").Value = content;
					sql.ParamByName("SMSOUT_STATUS").Value = "D";
					database.ExecSQL(sql);
					sql.Clear();
					sql.Add("INSERT INTO SMSTODO(SMSTODO_SMSOUTINID,SMSTODO_MOBILE,SMSTODO_USERID,SMSTODO_USERNAME,SMSTODO_CONTENT,SMSTODO_RETRIES,SMSTODO_INPROC)");
					sql.Add("VALUES(:SMSTODO_SMSOUTINID,:SMSTODO_MOBILE,:SMSTODO_USERID,:SMSTODO_USERNAME,:SMSTODO_CONTENT,:SMSTODO_RETRIES,:SMSTODO_INPROC)");
					foreach (KeyValuePair<string, User> keyValuePair in dictionary)
					{
						sql.ParamByName("SMSTODO_SMSOUTINID").Value = smsOutId;
						sql.ParamByName("SMSTODO_MOBILE").Value = keyValuePair.Key;
						sql.ParamByName("SMSTODO_USERID").Value = keyValuePair.Value.UserId;
						sql.ParamByName("SMSTODO_USERNAME").Value = keyValuePair.Value.UserName;
						sql.ParamByName("SMSTODO_CONTENT").Value = content;
						sql.ParamByName("SMSTODO_RETRIES").Value = 0;
						sql.ParamByName("SMSTODO_INPROC").Value = "N";
						database.ExecSQL(sql);
					}
					database.CommitTrans();
				}
				catch (Exception ex)
				{
					database.RollbackTrans();
					throw ex;
				}
			}
		}

		public static void SendTodo(int smsOutId, string smsFormatUsers, string content)
		{
			bool flag = string.IsNullOrWhiteSpace(smsFormatUsers) || string.IsNullOrWhiteSpace(content);
			if (!flag)
			{
				Database database = LogicContext.GetDatabase();
				Dictionary<string, User> dictionary = smsFormatUsers.ToSmsUsers();
				try
				{
					HSQL sql = new HSQL(database);
					sql.Clear();
					database.BeginTrans();
					sql.Add("INSERT INTO SMSTODO(SMSTODO_SMSOUTINID,SMSTODO_MOBILE,SMSTODO_USERID,SMSTODO_USERNAME,SMSTODO_CONTENT,SMSTODO_RETRIES,SMSTODO_INPROC)");
					sql.Add("VALUES(:SMSTODO_SMSOUTINID,:SMSTODO_MOBILE,:SMSTODO_USERID,:SMSTODO_USERNAME,:SMSTODO_CONTENT,:SMSTODO_RETRIES,:SMSTODO_INPROC)");
					foreach (KeyValuePair<string, User> keyValuePair in dictionary)
					{
						sql.ParamByName("SMSTODO_SMSOUTINID").Value = smsOutId;
						sql.ParamByName("SMSTODO_MOBILE").Value = keyValuePair.Key;
						sql.ParamByName("SMSTODO_USERID").Value = keyValuePair.Value.UserId;
						sql.ParamByName("SMSTODO_USERNAME").Value = keyValuePair.Value.UserName;
						sql.ParamByName("SMSTODO_CONTENT").Value = content;
						sql.ParamByName("SMSTODO_RETRIES").Value = 0;
						sql.ParamByName("SMSTODO_INPROC").Value = "N";
						database.ExecSQL(sql);
					}
					database.CommitTrans();
				}
				catch (Exception ex)
				{
					database.RollbackTrans();
					throw ex;
				}
			}
		}
	}
}
