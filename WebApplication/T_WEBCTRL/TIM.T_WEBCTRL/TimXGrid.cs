using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ParseChildren(true), PersistChildren(false), ToolboxData("<{0}:TimXGrid runat=server></{0}:TimXGrid>")]
	public class TimXGrid : TimGridView, IPostBackDataHandler
	{
		public delegate void ImageClickEventHandler(object sender, ImageClickEventArgs e);

		private bool m_addVisible = false;

		private bool m_editVisible = false;

		private bool m_deleteVisible = false;

		private bool m_allowAdd = false;

		private bool m_allowEdit = false;

		private bool m_allowDelete = false;

		private DataTable m_xData;

		private static readonly object _UtoAddClick = new object();

		private static readonly object _UtoEditClick = new object();

		private static readonly object _UtoDeleteClick = new object();

		public event TimXGrid.ImageClickEventHandler AddClick
		{
			add
			{
				base.Events.AddHandler(TimXGrid._UtoAddClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(TimXGrid._UtoAddClick, value);
			}
		}

		public event TimXGrid.ImageClickEventHandler EditClick
		{
			add
			{
				base.Events.AddHandler(TimXGrid._UtoEditClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(TimXGrid._UtoEditClick, value);
			}
		}

		public event TimXGrid.ImageClickEventHandler DeleteClick
		{
			add
			{
				base.Events.AddHandler(TimXGrid._UtoDeleteClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(TimXGrid._UtoDeleteClick, value);
			}
		}

		public bool AddVisible
		{
			get
			{
				return this.m_addVisible;
			}
			set
			{
				this.m_addVisible = value;
			}
		}

		public bool EditVisible
		{
			get
			{
				return this.m_editVisible;
			}
			set
			{
				this.m_editVisible = value;
			}
		}

		public bool DeleteVisible
		{
			get
			{
				return this.m_deleteVisible;
			}
			set
			{
				this.m_deleteVisible = value;
			}
		}

		public bool AllowAdd
		{
			get
			{
				return this.m_allowAdd;
			}
			set
			{
				this.m_allowAdd = value;
			}
		}

		public bool AllowEdit
		{
			get
			{
				return this.m_allowEdit;
			}
			set
			{
				this.m_allowEdit = value;
			}
		}

		public bool AllowDelete
		{
			get
			{
				return this.m_allowDelete;
			}
			set
			{
				this.m_allowDelete = value;
			}
		}

		public DataTable XData
		{
			get
			{
				bool flag = this.m_xData == null;
				if (flag)
				{
					object obj = base.ViewState["XData"];
					bool flag2 = obj != null;
					if (flag2)
					{
						this.m_xData = (DataTable)obj;
					}
					else
					{
						this.m_xData = null;
					}
				}
				return this.m_xData;
			}
			set
			{
				bool flag = !object.Equals(value, base.ViewState["XData"]);
				if (flag)
				{
					base.ViewState["XData"] = value;
					this.m_xData = value;
				}
			}
		}

		public TimXGrid()
		{
			this.EnableViewState = true;
		}

		protected override void OnInit(EventArgs e)
		{
			TemplateField actionField = new TemplateField();
			actionField.ItemStyle.Width = new Unit("50px");
			actionField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
			bool addVisible = this.AddVisible;
			if (addVisible)
			{
				actionField.HeaderTemplate = new TimXGridActionHeaderTemplate
				{
					AddVisible = this.AddVisible,
					AllowAdd = this.AllowAdd,
					OnAddClientClick = this.Page.ClientScript.GetPostBackEventReference(this, "XAdd")
				};
			}
			bool flag = this.EditVisible || this.DeleteVisible;
			if (flag)
			{
				actionField.ItemTemplate = new TimXGridActionItemTemplate
				{
					EditVisible = this.EditVisible,
					DeleteVisible = this.DeleteVisible,
					AllowEdit = this.AllowEdit,
					AllowDelete = this.AllowDelete,
					OnEditClientClick = this.Page.ClientScript.GetPostBackEventReference(this, "XEdit"),
					OnDeleteClientClick = this.Page.ClientScript.GetPostBackEventReference(this, "XDelete")
				};
			}
			bool flag2 = this.AddVisible || this.EditVisible || this.DeleteVisible;
			if (flag2)
			{
				this.Columns.Insert(0, actionField);
			}
			base.OnInit(e);
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		private void headerTemplate_AddHandler(object sender, ImageClickEventArgs e)
		{
			TimXGrid.ImageClickEventHandler Handler = (TimXGrid.ImageClickEventHandler)base.Events[TimXGrid._UtoAddClick];
			bool flag = Handler != null;
			if (flag)
			{
				Handler(this, e);
			}
		}

		private void itemTemplate_EditHandler(object sender, ImageClickEventArgs e)
		{
			TimXGrid.ImageClickEventHandler Handler = (TimXGrid.ImageClickEventHandler)base.Events[TimXGrid._UtoEditClick];
			bool flag = Handler != null;
			if (flag)
			{
				Handler(this, e);
			}
		}

		private void itemTemplate_DeleteHandler(object sender, ImageClickEventArgs e)
		{
			TimXGrid.ImageClickEventHandler Handler = (TimXGrid.ImageClickEventHandler)base.Events[TimXGrid._UtoDeleteClick];
			bool flag = Handler != null;
			if (flag)
			{
				Handler(this, e);
			}
		}

		protected virtual void OnAddClick(ImageClickEventArgs e)
		{
			TimXGrid.ImageClickEventHandler Handler = (TimXGrid.ImageClickEventHandler)base.Events[TimXGrid._UtoAddClick];
			bool flag = Handler != null;
			if (flag)
			{
				Handler(this, e);
			}
		}

		protected virtual void OnEditClick(ImageClickEventArgs e)
		{
			TimXGrid.ImageClickEventHandler Handler = (TimXGrid.ImageClickEventHandler)base.Events[TimXGrid._UtoEditClick];
			bool flag = Handler != null;
			if (flag)
			{
				Handler(this, e);
			}
		}

		protected virtual void OnDeleteClick(ImageClickEventArgs e)
		{
			TimXGrid.ImageClickEventHandler Handler = (TimXGrid.ImageClickEventHandler)base.Events[TimXGrid._UtoDeleteClick];
			bool flag = Handler != null;
			if (flag)
			{
				Handler(this, e);
			}
		}

		protected override void RaisePostBackEvent(string EventArgument)
		{
			bool flag = EventArgument.StartsWith("RowDblClick_");
			if (flag)
			{
				int RowClicked = int.Parse(EventArgument.Substring(12));
				this.SelectedIndex = RowClicked;
				this.OnRowDoubleClick(null);
			}
			else
			{
				bool flag2 = EventArgument == "XAdd";
				if (flag2)
				{
					this.OnAddClick(null);
				}
				else
				{
					bool flag3 = EventArgument == "XEdit";
					if (flag3)
					{
						this.OnEditClick(null);
					}
					else
					{
						bool flag4 = EventArgument == "XDelete";
						if (flag4)
						{
							this.OnDeleteClick(null);
						}
					}
				}
			}
		}

		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.SelectedIndex = Convert.ToInt32(postCollection[this.ClientID + "_selectedIndex"]);
			return true;
		}
	}
}
