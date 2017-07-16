using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[Designer("TIM.T_WEBCTRL.TimButtonMenuDesigner"), ParseChildren(true), PersistChildren(false), ToolboxData("<{0}:TimButtonMenu runat=server></{0}:TimButtonMenu>")]
	public class TimButtonMenu : WebControl, IScriptControl, IPostBackEventHandler
	{
		public delegate void MenuItemEventHandler(object sender, string value);

		private static object EventClick = new object();

		private static object EventDropDown = new object();

		private static object EventMenuItemClick = new object();

		private ScriptManager _sm;

		private bool ShowDropDown = false;

		private string m_dropDownImage = "~/Images/Tim/DropDownImage.gif";

		private ButtonMenuType m_buttonType = ButtonMenuType.Button;

		private string m_buttonImage = string.Empty;

		private string m_text = string.Empty;

		private TimMenuItemCollection m_items;

		private string m_onClientClick = string.Empty;

		private string m_onMenuItemClientClick = string.Empty;

		[Category("ClickEvent"), Description("按钮事件")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(TimButtonMenu.EventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(TimButtonMenu.EventClick, value);
			}
		}

		[Category("ClickEvent"), Description("下拉事件")]
		public event EventHandler DropDown
		{
			add
			{
				base.Events.AddHandler(TimButtonMenu.EventDropDown, value);
			}
			remove
			{
				base.Events.RemoveHandler(TimButtonMenu.EventDropDown, value);
			}
		}

		[Category("ClickEvent"), Description("菜单项事件")]
		public event TimButtonMenu.MenuItemEventHandler MenuItemClick
		{
			add
			{
				base.Events.AddHandler(TimButtonMenu.EventMenuItemClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(TimButtonMenu.EventMenuItemClick, value);
			}
		}

		[Bindable(false), Category("Appearnce"), DefaultValue(""), Description("下拉按钮图片"), Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		public string DropDownImage
		{
			get
			{
				return this.m_dropDownImage;
			}
			set
			{
				this.m_dropDownImage = value;
			}
		}

		[Bindable(false), Category("Appearnce"), DefaultValue(ButtonMenuType.Button), Description("按钮类型")]
		public ButtonMenuType ButtonType
		{
			get
			{
				return this.m_buttonType;
			}
			set
			{
				this.m_buttonType = value;
			}
		}

		[Bindable(false), Category("Appearnce"), DefaultValue(""), Description("按钮图标"), Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		public string ButtonImage
		{
			get
			{
				return this.m_buttonImage;
			}
			set
			{
				this.m_buttonImage = value;
			}
		}

		[Bindable(false), Category("Appearance"), DefaultValue(""), Description("按钮标题")]
		public string Text
		{
			get
			{
				string text = (string)this.ViewState["Text"];
				bool flag = text != null;
				string result;
				if (flag)
				{
					result = text;
				}
				else
				{
					result = string.Empty;
				}
				return result;
			}
			set
			{
				switch (this.ButtonType)
				{
				case ButtonMenuType.Button:
					this.Width = value.Length * 12 + 16;
					break;
				case ButtonMenuType.ImageButton:
				{
					bool flag = !string.IsNullOrWhiteSpace(this.ButtonImage);
					if (flag)
					{
						this.Width = value.Length * 12 + 16 + 20;
					}
					else
					{
						this.Width = value.Length * 12 + 16;
					}
					break;
				}
				case ButtonMenuType.DropDown:
					this.Width = value.Length * 12 + 16 + 10;
					break;
				case ButtonMenuType.ImageDropDown:
				{
					bool flag2 = !string.IsNullOrWhiteSpace(this.ButtonImage);
					if (flag2)
					{
						this.Width = value.Length * 12 + 16 + 10 + 20;
					}
					else
					{
						this.Width = value.Length * 12 + 16 + 10;
					}
					break;
				}
				}
				this.ViewState["Text"] = value;
			}
		}

		[Category("Tim"), DefaultValue(null), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TimMenuItemCollection Items
		{
			get
			{
				bool flag = this.m_items == null;
				if (flag)
				{
					this.m_items = new TimMenuItemCollection();
				}
				return this.m_items;
			}
		}

		[Category("Behavior"), DefaultValue(""), Description("按钮客户端事件")]
		public string OnClientClick
		{
			get
			{
				return this.m_onClientClick;
			}
			set
			{
				this.m_onClientClick = value;
			}
		}

		[Category("Behavior"), DefaultValue(""), Description("菜单项客户端事件")]
		public string OnMenuItemClientClick
		{
			get
			{
				return this.m_onMenuItemClientClick;
			}
			set
			{
				this.m_onMenuItemClientClick = value;
			}
		}

		protected override string TagName
		{
			get
			{
				return "div";
			}
		}

		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			string path = this.Page.ResolveClientUrl("~/Scripts/Tim/");
			bool flag = !this.Page.ClientScript.IsClientScriptBlockRegistered("TimCommon");
			IEnumerable<ScriptReference> result;
			if (flag)
			{
				result = new ScriptReference[]
				{
					new ScriptReference(string.Format("{0}TimCommon.js?v=" + TimCtrlUtils.Md5Version, path)),
					new ScriptReference(string.Format("{0}TimButtonMenu.js?v=" + TimCtrlUtils.Md5Version, path))
				};
			}
			else
			{
				result = new ScriptReference[]
				{
					new ScriptReference(string.Format("{0}TimButtonMenu.js?v=" + TimCtrlUtils.Md5Version, path))
				};
			}
			return result;
		}

		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor descriptor = new ScriptControlDescriptor("TIM.T_WEBCTRL.TimButtonMenu", this.ClientID);
			descriptor.AddProperty("uniqueID", this.UniqueID);
			descriptor.AddProperty("serverID", this.ID);
			descriptor.AddProperty("buttonType", this.ButtonType.ToString());
			descriptor.AddProperty("onClientClick", this.OnClientClick);
			descriptor.AddProperty("onMenuItemClientClick", this.OnMenuItemClientClick);
			bool flag = this.ButtonType > ButtonMenuType.Button;
			if (flag)
			{
				descriptor.AddProperty("showDropDown", this.ShowDropDown);
			}
			string menuItemJSON = this.MenuItem2JSON();
			bool flag2 = menuItemJSON != "[{}]";
			if (flag2)
			{
				descriptor.AddProperty("menuItemJSON", menuItemJSON);
			}
			else
			{
				descriptor.AddProperty("menuItemJSON", "");
			}
			EventHandler clickHandler = (EventHandler)base.Events[TimButtonMenu.EventClick];
			bool flag3 = clickHandler != null;
			if (flag3)
			{
				descriptor.AddProperty("clickCallBack", true);
			}
			else
			{
				descriptor.AddProperty("clickCallBack", false);
			}
			EventHandler dropDownHandler = (EventHandler)base.Events[TimButtonMenu.EventDropDown];
			bool flag4 = dropDownHandler != null;
			if (flag4)
			{
				descriptor.AddProperty("dropDownCallBack", true);
			}
			else
			{
				descriptor.AddProperty("dropDownCallBack", false);
			}
			TimButtonMenu.MenuItemEventHandler menuItemClickHandler = (TimButtonMenu.MenuItemEventHandler)base.Events[TimButtonMenu.EventMenuItemClick];
			bool flag5 = menuItemClickHandler != null;
			if (flag5)
			{
				descriptor.AddProperty("menuClickCallBack", true);
			}
			else
			{
				descriptor.AddProperty("menuClickCallBack", false);
			}
			return new ScriptControlDescriptor[]
			{
				descriptor
			};
		}

		public TimButtonMenu()
		{
			this.Height = new Unit(20.0, UnitType.Pixel);
			this.Width = new Unit(40.0, UnitType.Pixel);
		}

		private void OnClick(EventArgs e)
		{
			EventHandler Handler = (EventHandler)base.Events[TimButtonMenu.EventClick];
			bool flag = Handler != null;
			if (flag)
			{
				Handler(this, e);
			}
		}

		private void OnDropDown(EventArgs e)
		{
			EventHandler Handler = (EventHandler)base.Events[TimButtonMenu.EventDropDown];
			bool flag = Handler != null;
			if (flag)
			{
				Handler(this, e);
			}
		}

		private void OnMenuItemClick(string Value)
		{
			TimButtonMenu.MenuItemEventHandler Handle = (TimButtonMenu.MenuItemEventHandler)base.Events[TimButtonMenu.EventMenuItemClick];
			bool flag = Handle != null;
			if (flag)
			{
				Handle(this, Value);
			}
		}

		public void RaisePostBackEvent(string eventArgument)
		{
			bool flag = eventArgument == this.UniqueID;
			if (flag)
			{
				this.OnClick(new EventArgs());
			}
			else
			{
				bool flag2 = eventArgument == this.UniqueID + "_DropDown";
				if (flag2)
				{
					this.ShowDropDown = true;
					this.OnDropDown(new EventArgs());
				}
				else
				{
					bool flag3 = !eventArgument.Equals("");
					if (flag3)
					{
						this.OnMenuItemClick(eventArgument);
					}
				}
			}
		}

		private string GetUrlString(string url)
		{
			return url.StartsWith("~") ? this.Page.ResolveClientUrl(url) : url;
		}

		protected override void OnPreRender(EventArgs e)
		{
			bool flag = !base.DesignMode;
			if (flag)
			{
				this._sm = ScriptManager.GetCurrent(this.Page);
				bool flag2 = this._sm == null;
				if (flag2)
				{
					throw new HttpException("A ScriptManager control must exist on the current page.");
				}
				this._sm.RegisterScriptControl<TimButtonMenu>(this);
			}
			base.OnPreRender(e);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			bool flag = !base.DesignMode;
			if (flag)
			{
				this._sm.RegisterScriptDescriptors(this);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "buttonMenuDiv");
			base.Render(writer);
			this.Page.ClientScript.GetPostBackEventReference(this, "");
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "buttonMenuTable");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.AddAttribute("onselectstart", "return false;");
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
			writer.AddAttribute(HtmlTextWriterAttribute.Valign, "middle");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString());
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "ButtonTd");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			bool flag = (this.ButtonType == ButtonMenuType.ImageButton || this.ButtonType == ButtonMenuType.ImageDropDown) && !string.IsNullOrWhiteSpace(this.ButtonImage);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "middle");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "19");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "19");
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "ButtonTdIco");
				writer.AddAttribute(HtmlTextWriterAttribute.Src, this.GetUrlString("~/Images/Tim/" + this.ButtonImage));
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "true");
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "middle");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write("  ");
				writer.RenderEndTag();
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "true");
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
			writer.AddAttribute(HtmlTextWriterAttribute.Valign, "middle");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.Text);
			writer.RenderEndTag();
			writer.RenderEndTag();
			bool flag2 = this.ButtonType == ButtonMenuType.DropDown;
			if (flag2)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "buttonMenuTdDrop");
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "DropDownTd");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				bool flag3 = !this.DropDownImage.Trim().Equals("");
				if (flag3)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, this.GetUrlString(this.DropDownImage));
					writer.RenderBeginTag(HtmlTextWriterTag.Img);
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		protected override object SaveViewState()
		{
			object o = base.SaveViewState();
			object io = ((IStateManager)this.Items).SaveViewState();
			bool flag = o == null && io == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new Triplet(o, io);
			}
			return result;
		}

		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
		}

		protected override void LoadViewState(object savedState)
		{
			bool flag = savedState != null;
			if (flag)
			{
				Triplet triplet = (Triplet)savedState;
				base.LoadViewState(triplet.First);
				((IStateManager)this.Items).LoadViewState(triplet.Second);
			}
			else
			{
				base.LoadViewState(null);
			}
		}

		protected string MenuItem2JSON()
		{
			JavaScriptSerializer nodeSerializer = new JavaScriptSerializer();
			nodeSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new MenuItemsConverter()
			});
			string sb = nodeSerializer.Serialize(this.Items);
			return "[" + sb.ToString() + "]";
		}
	}
}
