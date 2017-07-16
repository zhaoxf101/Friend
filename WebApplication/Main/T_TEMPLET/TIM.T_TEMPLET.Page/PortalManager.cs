using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TIM.T_TEMPLET.Page
{
	public class PortalManager
	{
		private bool m_draggable = false;

		private SortedList<int, PortalRow> m_rows = new SortedList<int, PortalRow>();

		[JsonProperty(PropertyName = "draggable")]
		public bool Draggable
		{
			get
			{
				return this.m_draggable;
			}
			set
			{
				this.m_draggable = value;
			}
		}

		[JsonIgnore]
		public SortedList<int, PortalRow> Rows
		{
			get
			{
				return this.m_rows;
			}
			set
			{
				this.m_rows = value;
			}
		}

		[JsonProperty(PropertyName = "rows")]
		private IList<PortalRow> RowsToList
		{
			get
			{
				return this.Rows.Values;
			}
		}

		public void AddPortalPanel(PortalPanel panel)
		{
			bool flag = !this.Rows.ContainsKey(panel.RowIndex);
			if (flag)
			{
				PortalColumn pc = new PortalColumn();
				pc.Panels.Add(panel);
				PortalRow pr = new PortalRow();
				pr.Columns.Add(panel.ColumnIndex, pc);
				this.Rows.Add(panel.RowIndex, pr);
			}
			else
			{
				bool flag2 = !this.Rows[panel.RowIndex].Columns.ContainsKey(panel.ColumnIndex);
				if (flag2)
				{
					PortalColumn pc2 = new PortalColumn();
					pc2.Panels.Add(panel);
					this.Rows[panel.RowIndex].Columns.Add(panel.ColumnIndex, pc2);
				}
			}
		}

		public void AddPortalPanel(PortalPanel panel, Unit width)
		{
			bool flag = !this.Rows.ContainsKey(panel.RowIndex);
			if (flag)
			{
				PortalColumn pc = new PortalColumn();
				pc.Width = width;
				pc.Panels.Add(panel);
				PortalRow pr = new PortalRow();
				pr.Columns.Add(panel.ColumnIndex, pc);
				this.Rows.Add(panel.RowIndex, pr);
			}
			else
			{
				bool flag2 = !this.Rows[panel.RowIndex].Columns.ContainsKey(panel.ColumnIndex);
				if (flag2)
				{
					PortalColumn pc2 = new PortalColumn();
					pc2.Width = width;
					pc2.Panels.Add(panel);
					this.Rows[panel.RowIndex].Columns.Add(panel.ColumnIndex, pc2);
				}
			}
		}

		public string PortalToJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
