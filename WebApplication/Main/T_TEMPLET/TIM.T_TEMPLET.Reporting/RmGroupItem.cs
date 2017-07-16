using System;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RmGroupItem
	{
		private RmGroup m_group = null;

		private string m_datasetName = string.Empty;

		private string m_fieldName = string.Empty;

		internal RmGroup Group
		{
			get
			{
				return this.m_group;
			}
			set
			{
				this.m_group = value;
			}
		}

		public string DatasetName
		{
			get
			{
				return this.m_datasetName;
			}
			set
			{
				this.m_datasetName = value;
			}
		}

		public string FieldName
		{
			get
			{
				return this.m_fieldName;
			}
			set
			{
				this.m_fieldName = value;
			}
		}

		public RmGroupItem(RmGroup group)
		{
			this.m_group = group;
		}
	}
}
