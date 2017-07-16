using System;

namespace TIM.T_KERNEL.DbTableCache
{
	public class Permission : DbEntity
	{
		private PermissionType m_type = PermissionType.R;

		private string m_id = string.Empty;

		private int m_amId = 0;

		private bool m_insert = false;

		private bool m_edit = false;

		private bool m_delete = false;

		private bool m_print = false;

		private bool m_view = false;

		private bool m_design = false;

		public PermissionType Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		public string Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		public int AmId
		{
			get
			{
				return this.m_amId;
			}
			set
			{
				this.m_amId = value;
			}
		}

		public bool Insert
		{
			get
			{
				return this.m_insert;
			}
			set
			{
				this.m_insert = value;
			}
		}

		public bool Edit
		{
			get
			{
				return this.m_edit;
			}
			set
			{
				this.m_edit = value;
			}
		}

		public bool Delete
		{
			get
			{
				return this.m_delete;
			}
			set
			{
				this.m_delete = value;
			}
		}

		public bool Print
		{
			get
			{
				return this.m_print;
			}
			set
			{
				this.m_print = value;
			}
		}

		public bool View
		{
			get
			{
				return this.m_view;
			}
			set
			{
				this.m_view = value;
			}
		}

		public bool Design
		{
			get
			{
				return this.m_design;
			}
			set
			{
				this.m_design = value;
			}
		}
	}
}
