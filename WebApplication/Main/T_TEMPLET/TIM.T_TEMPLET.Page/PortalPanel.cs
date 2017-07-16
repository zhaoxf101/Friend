using Newtonsoft.Json;
using System;
using System.Web.UI.WebControls;
using TIM.T_KERNEL.Helper;

namespace TIM.T_TEMPLET.Page
{
	public class PortalPanel
	{
		private int m_rowIndex = 0;

		private int m_columnIndex = 0;

		private string m_title = string.Empty;

		private Unit m_width = new Unit("100%");

		private Unit m_height = 300;

		private string m_content = string.Empty;

		private string m_url = string.Empty;

		private string m_frameName = string.Empty;

		private string m_data = string.Empty;

		private bool m_showClose = false;

		private bool m_showToggle = false;

		private string m_icon = string.Empty;

		[JsonIgnore]
		public int RowIndex
		{
			get
			{
				return this.m_rowIndex;
			}
			set
			{
				this.m_rowIndex = value;
			}
		}

		[JsonIgnore]
		public int ColumnIndex
		{
			get
			{
				return this.m_columnIndex;
			}
			set
			{
				this.m_columnIndex = value;
			}
		}

		[JsonProperty(PropertyName = "title")]
		public string Title
		{
			get
			{
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		[JsonProperty(PropertyName = "width")]
		public Unit Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		[JsonIgnore]
		public Unit Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		[JsonConverter(typeof(HeightPercent)), JsonProperty(PropertyName = "height")]
		private string HeightToInt
		{
			get
			{
				bool flag = this.Height.Type == UnitType.Pixel;
				string result;
				if (flag)
				{
					result = this.Height.Value.ToString();
				}
				else
				{
					bool flag2 = this.Height.Type == UnitType.Percentage;
					if (flag2)
					{
						result = (this.Height.Value / 100.0).RoundX(2).ToString() + " * $(window).height()";
					}
					else
					{
						result = this.Height.Value.ToString();
					}
				}
				return result;
			}
		}

		[JsonProperty(PropertyName = "content")]
		public string Content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		[JsonProperty(PropertyName = "url")]
		public string Url
		{
			get
			{
				return this.m_url;
			}
			set
			{
				this.m_url = value;
			}
		}

		[JsonIgnore]
		public string FrameName
		{
			get
			{
				return this.m_frameName;
			}
			set
			{
				this.m_frameName = value;
			}
		}

		[JsonIgnore]
		public string Data
		{
			get
			{
				return this.m_data;
			}
			set
			{
				this.m_data = value;
			}
		}

		[JsonProperty(PropertyName = "showClose")]
		public bool ShowClose
		{
			get
			{
				return this.m_showClose;
			}
			set
			{
				this.m_showClose = value;
			}
		}

		[JsonProperty(PropertyName = "showToggle")]
		public bool ShowToggle
		{
			get
			{
				return this.m_showToggle;
			}
			set
			{
				this.m_showToggle = value;
			}
		}

		[JsonIgnore, JsonProperty(PropertyName = "icon")]
		public string Icon
		{
			get
			{
				return this.m_icon;
			}
			set
			{
				this.m_icon = value;
			}
		}

		public PortalPanel(int rowIndex, int columnIndex, string title, Unit height, string url)
		{
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = columnIndex;
			this.m_title = title;
			this.m_height = height;
			this.m_url = url;
		}

		public PortalPanel(int rowIndex, int columnIndex, string title, Unit height, string content, string url)
		{
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = columnIndex;
			this.m_title = title;
			this.m_height = height;
			this.m_content = content;
			this.m_url = url;
		}

		public PortalPanel(int rowIndex, int columnIndex, string title, Unit width, Unit height, string url)
		{
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = columnIndex;
			this.m_title = title;
			this.m_width = width;
			this.m_height = height;
			this.m_url = url;
		}
	}
}
