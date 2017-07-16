using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TIM.T_TEMPLET.Page
{
	public class PortalRow
	{
		private SortedList<int, PortalColumn> m_columns = new SortedList<int, PortalColumn>();

		[JsonIgnore]
		public SortedList<int, PortalColumn> Columns
		{
			get
			{
				return this.m_columns;
			}
			set
			{
				this.m_columns = value;
			}
		}

		[JsonProperty(PropertyName = "columns")]
		private IList<PortalColumn> ColumnsToList
		{
			get
			{
				return this.Columns.Values;
			}
		}
	}
}
