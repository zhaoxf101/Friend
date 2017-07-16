using System;
using System.Collections.Generic;
using System.Data;
using TIM.T_KERNEL.Helper;

namespace TIM.T_KERNEL.DbTableCache
{
	public class PmConfigUtils
	{
		internal static PmConfig GetObject(DataRow row)
		{
			PmConfig pmConfig = new PmConfig();
			pmConfig.PmId = row["PMCONFIG_PMID"].ToString().Trim();
			pmConfig.RoleId = row["PMCONFIG_ROLEID"].ToString().Trim();
			pmConfig.MdId = row["PMCONFIG_MDID"].ToString().Trim().ToInt();
			pmConfig.UserId = row["PMCONFIG_USERID"].ToString().Trim();
			pmConfig.Value = row["PMCONFIG_VALUE"].ToString().Trim();
			string a = row["PMCONFIG_TYPE"].ToString().Trim();
			if (!(a == "1"))
			{
				if (!(a == "2"))
				{
					if (!(a == "3"))
					{
						if (!(a == "4"))
						{
							if (!(a == "5"))
							{
								if (!(a == "6"))
								{
									pmConfig.Type = PmConfigType.Null;
								}
								else
								{
									pmConfig.Type = PmConfigType.UserMdId;
								}
							}
							else
							{
								pmConfig.Type = PmConfigType.RoleMdId;
							}
						}
						else
						{
							pmConfig.Type = PmConfigType.User;
						}
					}
					else
					{
						pmConfig.Type = PmConfigType.Role;
					}
				}
				else
				{
					pmConfig.Type = PmConfigType.MdId;
				}
			}
			else
			{
				pmConfig.Type = PmConfigType.System;
			}
			return pmConfig;
		}

		private static PmConfig GetPmConfig(string roleId, string userId, int mdId, string pmId)
		{
			PmConfig pmConfig = null;
			PmConfigCache pmConfigCache = (PmConfigCache)new PmConfigCache().GetData();
			int index = pmConfigCache.dvPmConfigBy_RoleId_UserId_MdId_PmId.Find(new object[]
			{
				roleId,
				userId,
				mdId,
				pmId
			});
			bool flag = index >= 0;
			if (flag)
			{
				PmConfig pmConfig2 = new PmConfig();
				pmConfig = PmConfigUtils.GetObject(pmConfigCache.dvPmConfigBy_RoleId_UserId_MdId_PmId[index].Row);
			}
			return pmConfig;
		}

		public static string GetParamValue(int amId, string pmId)
		{
			string str = string.Empty;
			bool flag = string.IsNullOrEmpty(pmId);
			if (flag)
			{
				throw new Exception("参数名不允许为空");
			}
			pmId = pmId.ToUpper();
			bool flag2 = amId == 0 && LogicContext.Current != null && LogicContext.Current.AmId != 0;
			if (flag2)
			{
				amId = LogicContext.Current.AmId;
			}
			bool flag3 = ModuleUtils.IsExtModule(amId);
			if (flag3)
			{
			}
			Parameter parameter = ParameterUtils.GetParameter(pmId);
			bool flag4 = parameter == null;
			if (flag4)
			{
				throw new Exception(string.Format("参数【{0}】不存在，无法获取对应参数值！", pmId.ToString().Trim()));
			}
			bool flag5 = parameter != null && parameter.Type != ParameterType.S;
			string result;
			if (flag5)
			{
				string userId = LogicContext.Current.UserId;
				bool flag6 = UserUtils.GetUser(userId) == null;
				if (flag6)
				{
					result = string.Empty;
					return result;
				}
				List<RoleUser> roles = RoleUserUtils.GetRoles(userId);
				PmConfig pmConfig = PmConfigUtils.GetPmConfig("", userId, amId, pmId);
				bool flag7 = pmConfig != null;
				if (flag7)
				{
					result = pmConfig.Value;
					return result;
				}
				foreach (RoleUser roleUser in roles)
				{
					PmConfig pmConfig2 = PmConfigUtils.GetPmConfig(roleUser.RoleId, "", amId, pmId);
					bool flag8 = pmConfig2 != null;
					if (flag8)
					{
						result = pmConfig2.Value;
						return result;
					}
				}
				PmConfig pmConfig3 = PmConfigUtils.GetPmConfig("", userId, 0, pmId);
				bool flag9 = pmConfig3 != null;
				if (flag9)
				{
					result = pmConfig3.Value;
					return result;
				}
				foreach (RoleUser roleUser2 in roles)
				{
					PmConfig pmConfig4 = PmConfigUtils.GetPmConfig(roleUser2.RoleId, "", 0, pmId);
					bool flag10 = pmConfig4 != null;
					if (flag10)
					{
						result = pmConfig4.Value;
						return result;
					}
				}
				PmConfig pmConfig5 = PmConfigUtils.GetPmConfig("", "", amId, pmId);
				bool flag11 = pmConfig5 != null;
				if (flag11)
				{
					result = pmConfig5.Value;
					return result;
				}
			}
			PmConfig pmConfig6 = PmConfigUtils.GetPmConfig("", "", 0, pmId);
			bool flag12 = pmConfig6 != null;
			if (flag12)
			{
				result = pmConfig6.Value;
			}
			else
			{
				bool flag13 = parameter != null;
				if (flag13)
				{
					result = parameter.DefaultValue;
				}
				else
				{
					result = str;
				}
			}
			return result;
		}
	}
}
