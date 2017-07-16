using System;
using System.Collections.Generic;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RmGroup
	{
		private RmGroups m_groups = null;

		private RmDataSet m_dataset = null;

		private Dictionary<int, RdRow> m_header = null;

		private Dictionary<int, RdRow> m_footer = null;

		private Dictionary<int, RmGroupItem> m_items = null;

		internal RmGroups Groups
		{
			get
			{
				return this.m_groups;
			}
			set
			{
				this.m_groups = value;
			}
		}

		internal RmDataSet Dataset
		{
			get
			{
				return this.m_dataset;
			}
			set
			{
				this.m_dataset = value;
			}
		}

		internal Dictionary<int, RdRow> Header
		{
			get
			{
				return this.m_header;
			}
			set
			{
				this.m_header = value;
			}
		}

		internal Dictionary<int, RdRow> Footer
		{
			get
			{
				return this.m_footer;
			}
			set
			{
				this.m_footer = value;
			}
		}

		internal Dictionary<int, RmGroupItem> Items
		{
			get
			{
				return this.m_items;
			}
			set
			{
				this.m_items = value;
			}
		}

		public RmGroup(RmGroups groups)
		{
			this.m_groups = groups;
			this.m_header = new Dictionary<int, RdRow>();
			this.m_footer = new Dictionary<int, RdRow>();
			this.m_items = new Dictionary<int, RmGroupItem>();
		}

		internal bool SameGroup(string datasetName, string groupBy)
		{
			RmDataSet datasetRec = this.m_groups.Builder.DataSets.FindDataset(datasetName);
			bool flag = datasetRec != this.m_dataset;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				groupBy = groupBy.Replace(";", " ").Replace(",", " ");
				foreach (KeyValuePair<int, RmGroupItem> obj in this.m_items)
				{
					bool flag2 = groupBy.IndexOf(obj.Value.DatasetName + "." + obj.Value.FieldName) < 0;
					if (flag2)
					{
						break;
					}
					groupBy.Replace(obj.Value.DatasetName + "." + obj.Value.FieldName, "");
				}
				bool flag3 = string.IsNullOrEmpty(groupBy.Trim(new char[]
				{
					' '
				}));
				result = flag3;
			}
			return result;
		}

		internal void SetGroupBy(string datasetName, string groupBy)
		{
			string aDatasetName = string.Empty;
			string aFieldName = string.Empty;
			this.m_dataset = this.m_groups.Builder.DataSets.FindDataset(datasetName);
			string[] arrGroupBy = groupBy.Split(new char[]
			{
				';',
				','
			});
			for (int i = 0; i < arrGroupBy.Length; i++)
			{
				string item = arrGroupBy[i].Trim();
				int index = item.IndexOf('.');
				bool flag = index >= 0;
				if (flag)
				{
					aDatasetName = item.Split(new char[]
					{
						'.'
					})[0];
					aFieldName = item.Split(new char[]
					{
						'.'
					})[1];
					bool flag2 = !string.IsNullOrEmpty(aDatasetName) && !string.IsNullOrEmpty(aFieldName);
					if (flag2)
					{
						RmGroupItem aItem = new RmGroupItem(this);
						aItem.DatasetName = aDatasetName;
						aItem.FieldName = aFieldName;
						this.m_items.Add(this.m_items.Count, aItem);
					}
				}
			}
		}
	}
}
