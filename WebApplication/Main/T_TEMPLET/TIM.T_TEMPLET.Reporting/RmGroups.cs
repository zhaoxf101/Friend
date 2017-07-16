using System;
using System.Collections.Generic;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RmGroups : RmSetNode
	{
		private RdDocument _rep = null;

		private Dictionary<int, RmGroup> m_items = null;

		internal Dictionary<int, RmGroup> Items
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

		public RmGroups(RmReportingMaker builder) : base(builder)
		{
			this.m_items = new Dictionary<int, RmGroup>();
		}

		public void Clear()
		{
			this.Items.Clear();
		}

		public void Refresh()
		{
			RmGroup group = null;
			RdRow row = null;
			this.Clear();
			this._rep = base.Builder.Template;
			for (int i = 1; i <= this._rep.Rows.RowCount; i++)
			{
				row = this._rep.Rows[i];
				bool flag = row.RowType == RdRowType.rtGroupHeader || row.RowType == RdRowType.rtGroupFooter;
				if (flag)
				{
					group = null;
					foreach (KeyValuePair<int, RmGroup> obj in this.m_items)
					{
						bool flag2 = obj.Value.SameGroup(row.DataSet, row.GroupBy);
						if (flag2)
						{
							group = obj.Value;
							break;
						}
					}
					bool flag3 = group == null;
					if (flag3)
					{
						group = new RmGroup(this);
						group.SetGroupBy(row.DataSet, row.GroupBy);
						this.m_items.Add(this.m_items.Count, group);
					}
					bool flag4 = row.RowType == RdRowType.rtGroupHeader;
					if (flag4)
					{
						group.Header.Add(group.Header.Count, row);
					}
					else
					{
						bool flag5 = row.RowType == RdRowType.rtGroupFooter;
						if (flag5)
						{
							group.Footer.Add(group.Footer.Count, row);
						}
					}
				}
			}
		}
	}
}
