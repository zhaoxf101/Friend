using System;
using System.Web.UI;

namespace TIM.T_WEBCTRL
{
	public class TimXGridActionHeaderTemplate : Control, ITemplate
	{
		private bool m_addVisible = false;

		private bool m_allowAdd = false;

		private string m_onAddClientClick = string.Empty;

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

		public string OnAddClientClick
		{
			get
			{
				return this.m_onAddClientClick;
			}
			set
			{
				this.m_onAddClientClick = value;
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			this.Page.RegisterRequiresPostBack(this);
			base.OnPreRender(e);
			this.Page.ClientScript.GetPostBackEventReference(this, "");
		}

		public void InstantiateIn(Control container)
		{
			bool addVisible = this.AddVisible;
			if (addVisible)
			{
				TimImageButton btnAdd = new TimImageButton();
				btnAdd.ID = "XBtn_Add";
				btnAdd.ToolTip = "新增";
				btnAdd.Enabled = this.AllowAdd;
				btnAdd.ImageUrl = "~/Images/Tim/XGrid_Add.gif";
				btnAdd.OnClientClick = this.OnAddClientClick;
				container.Controls.Add(btnAdd);
			}
		}
	}
}
