using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class TimXGridActionItemTemplate : ITemplate
	{
		private bool m_editVisible = false;

		private bool m_deleteVisible = false;

		private bool m_allowEdit = false;

		private bool m_allowDelete = false;

		private string m_onEditClientClick = string.Empty;

		private string m_onDeleteClientClick = string.Empty;

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

		public string OnEditClientClick
		{
			get
			{
				return this.m_onEditClientClick;
			}
			set
			{
				this.m_onEditClientClick = value;
			}
		}

		public string OnDeleteClientClick
		{
			get
			{
				return this.m_onDeleteClientClick;
			}
			set
			{
				this.m_onDeleteClientClick = value;
			}
		}

		public void InstantiateIn(Control container)
		{
			TimImageButton btnEdit = null;
			TimImageButton btnDelete = null;
			bool editVisible = this.EditVisible;
			if (editVisible)
			{
				btnEdit = new TimImageButton();
				btnEdit.ID = "XBtn_Edit";
				btnEdit.ToolTip = "编辑";
				btnEdit.Enabled = this.AllowEdit;
				btnEdit.ImageUrl = "~/Images/Tim/XGrid_Edit.gif";
				btnEdit.OnClientClick = "checkedRow($(this).closest('tr')[0],event);" + this.OnEditClientClick + "return false;";
			}
			bool deleteVisible = this.DeleteVisible;
			if (deleteVisible)
			{
				btnDelete = new TimImageButton();
				btnDelete.ID = "XBtn_Delete";
				btnDelete.ToolTip = "删除";
				btnDelete.Enabled = this.AllowDelete;
				btnDelete.ImageUrl = "~/Images/Tim/XGrid_Delete.gif";
				btnDelete.OnClientClick = "checkedRow($(this).closest('tr')[0],event);if (confirm('您确定要删除当前记录？') == false) return false;" + this.OnDeleteClientClick;
			}
			bool editVisible2 = this.EditVisible;
			if (editVisible2)
			{
				container.Controls.Add(btnEdit);
			}
			bool flag = this.EditVisible && this.DeleteVisible;
			if (flag)
			{
				container.Controls.Add(new Label
				{
					Text = "&nbsp;|&nbsp;"
				});
			}
			bool deleteVisible2 = this.DeleteVisible;
			if (deleteVisible2)
			{
				container.Controls.Add(btnDelete);
			}
		}
	}
}
