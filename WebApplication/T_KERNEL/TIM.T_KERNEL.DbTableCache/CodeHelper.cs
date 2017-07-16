using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class CodeHelper : DbEntity
	{
		private string m_codeId = string.Empty;

		private string m_codeName = string.Empty;

		private int m_mdId = 0;

		public string CodeId
		{
			get
			{
				return this.m_codeId;
			}
			set
			{
				this.m_codeId = value;
			}
		}

		public string CodeName
		{
			get
			{
				return this.m_codeName;
			}
			set
			{
				this.m_codeName = value;
			}
		}

		public int MdId
		{
			get
			{
				return this.m_mdId;
			}
			set
			{
				this.m_mdId = value;
			}
		}
	}
}
