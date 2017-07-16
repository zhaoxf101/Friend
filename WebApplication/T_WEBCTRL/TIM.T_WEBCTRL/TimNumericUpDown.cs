using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ToolboxBitmap(typeof(TimNumericUpDown), "TimNumericUpDown.TimNumericUpDown.ico"), ToolboxData("<{0}:TimNumericUpDown runat=server></{0}:TimNumericUpDown>")]
	public class TimNumericUpDown : WebControl, IPostBackDataHandler, IPostBackEventHandler, IScriptControl
	{
		private ScriptManager _SM;

		private string m_dbField = string.Empty;

		private TimCtrlEndSymbol m_symbol = TimCtrlEndSymbol.Null;

		private int m_min = 0;

		private int m_max = 0;

		private int m_increment = 1;

		private int m_decrement = 1;

		private string m_text = string.Empty;

		private bool m_autoPostBack = false;

		private static object EventTextChanged = new object();

		private string CtrlID = string.Empty;

		private string InputID = string.Empty;

		private string UpButtonID = string.Empty;

		private string DownButtonID = string.Empty;

		private int _ImageButtonWidth = 18;

		private int _ImageButtonHeight = 15;

		protected TextBox _InputTextBox;

		protected HtmlImage _ImageButton;

		protected HtmlGenericControl _ImageButtonContainer;

		[Category("Action"), Description("内容更改时发生")]
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(TimNumericUpDown.EventTextChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(TimNumericUpDown.EventTextChanged, value);
			}
		}

		[Category("Behavior"), Description("数据字段")]
		public string DbField
		{
			get
			{
				return this.m_dbField;
			}
			set
			{
				this.m_dbField = value;
			}
		}

		[Category("Behavior"), Description("文本之后的符号")]
		public TimCtrlEndSymbol Symbol
		{
			get
			{
				return this.m_symbol;
			}
			set
			{
				this.m_symbol = value;
			}
		}

		[Description("最小值")]
		public int Min
		{
			get
			{
				return this.m_min;
			}
			set
			{
				this.m_min = value;
			}
		}

		[Description("最大值")]
		public int Max
		{
			get
			{
				return this.m_max;
			}
			set
			{
				this.m_max = value;
			}
		}

		[DefaultValue(1), Description("递增")]
		public int Increment
		{
			get
			{
				return this.m_increment;
			}
			set
			{
				this.m_increment = value;
			}
		}

		[DefaultValue(1), Description("递减")]
		public int Decrement
		{
			get
			{
				return this.m_decrement;
			}
			set
			{
				this.m_decrement = value;
			}
		}

		[Category("Behavior"), DefaultValue(true), Description("零是否显示")]
		public bool ShowZero
		{
			get
			{
				object obj = this.ViewState["ShowZero"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowZero"] = value;
			}
		}

		public string Text
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.m_text);
				string result;
				if (flag)
				{
					result = "0";
				}
				else
				{
					result = this.m_text;
				}
				return result;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					bool showZero = this.ShowZero;
					if (showZero)
					{
						this.m_text = "0";
					}
					else
					{
						this.m_text = value;
					}
				}
				else
				{
					bool flag2 = !this.ShowZero && value.Trim().Equals("0");
					if (flag2)
					{
						this.m_text = "";
					}
					this.m_text = value;
				}
			}
		}

		public int Value
		{
			get
			{
				int tmpValue = 0;
				int.TryParse(this.Text, out tmpValue);
				return this.VerifyValue(tmpValue);
			}
			set
			{
				this.Text = this.VerifyValue(value).ToString();
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("控件值改变时是否回发")]
		public bool AutoPostBack
		{
			get
			{
				return this.m_autoPostBack;
			}
			set
			{
				this.m_autoPostBack = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("是否只读")]
		public bool ReadOnly
		{
			get
			{
				return !this.Enabled;
			}
			set
			{
				this.Enabled = !value;
			}
		}

		private int VerifyValue(int value)
		{
			bool flag = this.Min != this.Max;
			int result;
			if (flag)
			{
				bool flag2 = this.Min > this.Max;
				if (flag2)
				{
					this.Max = 2147483647;
				}
				bool flag3 = value < this.Min;
				if (flag3)
				{
					result = this.Min;
					return result;
				}
				bool flag4 = value > this.Max;
				if (flag4)
				{
					result = this.Max;
					return result;
				}
			}
			result = value;
			return result;
		}

		public TimNumericUpDown() : base(HtmlTextWriterTag.Div)
		{
			this._InputTextBox = new TextBox();
			this._ImageButton = new HtmlImage();
			this._ImageButtonContainer = new HtmlGenericControl();
			this.Width = Unit.Parse("100px");
			this.Height = Unit.Parse("20px");
			this.BorderWidth = Unit.Parse("1px");
			this.BorderColor = ColorTranslator.FromHtml("#CDCDCD");
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.CtrlID = this.ID;
			this.InputID = this.ClientID + "_Input";
			this.UpButtonID = this.ClientID + "_UpButton";
			this.DownButtonID = this.ClientID + "_DownButton";
			this._InputTextBox.ID = this.InputID;
			bool flag = string.IsNullOrEmpty(this.m_text);
			if (flag)
			{
				bool flag2 = this.Min != 0;
				if (flag2)
				{
					this.Text = this.Min.ToString();
				}
				else
				{
					this.Text = "0";
				}
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			bool flag = !base.DesignMode;
			if (flag)
			{
				this.Page.RegisterRequiresPostBack(this);
				this._SM = ScriptManager.GetCurrent(this.Page);
				bool flag2 = this._SM == null;
				if (flag2)
				{
					throw new HttpException("A ScriptManager control must exist on the current page.");
				}
				this._SM.RegisterScriptControl<TimNumericUpDown>(this);
			}
			base.OnPreRender(e);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			bool flag = !base.DesignMode;
			if (flag)
			{
				this._SM.RegisterScriptDescriptors(this);
			}
			bool flag2 = !this.Enabled || this.ReadOnly;
			if (flag2)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#F5FFFA");
				this._InputTextBox.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F5FFFA");
			}
			base.Render(writer);
			TimCtrlUtils.WriteEndSymbol(writer, this.Symbol);
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			this.RenderNumericUpDown(writer);
		}

		private string GetImageButtonStyle()
		{
			return string.Format("margin-right:1px; background-color:#E2E2E2; height:{0}; width:{1};border-left:1px solid #ffffff; border-top:1px solid #ffffff; border-right:1px solid #666666; border-bottom:1px solid #666666;", this._ImageButtonHeight.ToString() + "px", this._ImageButtonWidth.ToString() + "px");
		}

		protected virtual void RenderNumericUpDown(HtmlTextWriter writer)
		{
			writer.AddAttribute("face", "宋体");
			writer.RenderBeginTag(HtmlTextWriterTag.Font);
			this.RenderCustomControl(writer);
			writer.RenderEndTag();
		}

		protected virtual void RenderCustomControl(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.CtrlID);
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Width, this.Width.ToString());
			writer.AddAttribute(HtmlTextWriterAttribute.Height, this.Height.ToString());
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Bordercolor, ColorTranslator.ToHtml(this.BorderColor));
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.AddAttribute("style", "border-style:none;");
			writer.AddAttribute("rowspan", "2");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderTextBox(writer);
			writer.RenderEndTag();
			this._ImageButtonHeight = Convert.ToInt32((this.Height.Value - this.BorderWidth.Value * 2.0) / 2.0) - 1;
			writer.AddAttribute(HtmlTextWriterAttribute.Width, (this._ImageButtonWidth + 1).ToString() + "px");
			writer.AddAttribute(HtmlTextWriterAttribute.Valign, "middle");
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
			writer.AddAttribute("style", "border-style:none; font-size:1px;");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderUpButton(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.AddAttribute(HtmlTextWriterAttribute.Width, (this._ImageButtonWidth + 1).ToString() + "px");
			writer.AddAttribute(HtmlTextWriterAttribute.Valign, "middle");
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
			writer.AddAttribute("style", "border-style:none; font-size:1px;");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderDownButton(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		protected virtual void RenderTextBox(HtmlTextWriter writer)
		{
			this._InputTextBox.Width = new Unit(this.Width.Value - (double)this._ImageButtonWidth - 4.0, UnitType.Pixel);
			this._InputTextBox.Height = new Unit(this.Height.Value - 4.0, UnitType.Pixel);
			this._InputTextBox.BorderWidth = Unit.Parse("0px");
			this._InputTextBox.BorderStyle = BorderStyle.None;
			this._InputTextBox.Font.Name = this.Font.Name;
			this._InputTextBox.Font.Size = this.Font.Size;
			this._InputTextBox.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
			bool flag = string.IsNullOrEmpty(this.m_text);
			if (flag)
			{
				bool showZero = this.ShowZero;
				if (showZero)
				{
					this._InputTextBox.Text = "0";
				}
				else
				{
					this._InputTextBox.Text = this.m_text;
				}
			}
			else
			{
				bool flag2 = !this.ShowZero && this.m_text.Trim().Equals("0");
				if (flag2)
				{
					this._InputTextBox.Text = "";
				}
				this._InputTextBox.Text = this.m_text;
			}
			this._InputTextBox.RenderControl(writer);
		}

		private string GetUrlString(string url)
		{
			return url.StartsWith("~") ? this.Page.ResolveClientUrl(url) : url;
		}

		protected virtual void RenderUpButton(HtmlTextWriter writer)
		{
			string _ButtonImagePath = "~/Images/Tim/";
			writer.Write(writer.NewLine);
			this._ImageButton.Align = "absmiddle";
			bool enabled = this.Enabled;
			if (enabled)
			{
				this._ImageButton.Src = _ButtonImagePath + "TimNumericUpEnabled.gif";
			}
			else
			{
				this._ImageButton.Src = _ButtonImagePath + "TimNumericUpDisabled.gif";
			}
			this._ImageButton.EnableViewState = false;
			this._ImageButton.Style.Add("margin-top", string.Format("{0}px", (this._ImageButtonHeight - 4) / 2));
			this._ImageButton.Attributes["onselectstart"] = "return false";
			this._ImageButtonContainer.TagName = "div";
			this._ImageButtonContainer.ID = this.UpButtonID;
			this._ImageButtonContainer.Attributes.Add("style", this.GetImageButtonStyle());
			this._ImageButtonContainer.Attributes.Add("onselectstart", "return false");
			this._ImageButtonContainer.Controls.Add(this._ImageButton);
			this._ImageButtonContainer.RenderControl(writer);
		}

		protected virtual void RenderDownButton(HtmlTextWriter writer)
		{
			string _ButtonImagePath = "~/Images/Tim/";
			writer.Write(writer.NewLine);
			this._ImageButton.Align = "absmiddle";
			bool enabled = this.Enabled;
			if (enabled)
			{
				this._ImageButton.Src = _ButtonImagePath + "TimNumericDownEnabled.gif";
			}
			else
			{
				this._ImageButton.Src = _ButtonImagePath + "TimNumericDownDisabled.gif";
			}
			this._ImageButton.EnableViewState = false;
			this._ImageButton.Style.Add("margin-top", string.Format("{0}px", (this._ImageButtonHeight - 4) / 2));
			this._ImageButton.Attributes["onselectstart"] = "return false";
			this._ImageButtonContainer.TagName = "div";
			this._ImageButtonContainer.ID = this.DownButtonID;
			this._ImageButtonContainer.Attributes.Add("style", this.GetImageButtonStyle());
			this._ImageButtonContainer.Attributes.Add("onselectstart", "return false");
			this._ImageButtonContainer.Controls.Add(this._ImageButton);
			this._ImageButtonContainer.RenderControl(writer);
		}

		private void OnTextChanged(EventArgs e)
		{
			EventHandler Handle = (EventHandler)base.Events[TimNumericUpDown.EventTextChanged];
			bool flag = Handle != null;
			if (flag)
			{
				Handle(this, e);
			}
		}

		public void RaisePostDataChangedEvent()
		{
		}

		public void RaisePostBackEvent(string eventArgument)
		{
			this.OnTextChanged(EventArgs.Empty);
		}

		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string value = postCollection[string.Format("{0}_Input", this.ClientID)];
			this.m_text = HttpUtility.HtmlDecode(value);
			return true;
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
					new ScriptReference(string.Format("{0}TimNumericUpDown.js?v=" + TimCtrlUtils.Md5Version, path))
				};
			}
			else
			{
				result = new ScriptReference[]
				{
					new ScriptReference(string.Format("{0}TimNumericUpDown.js?v=" + TimCtrlUtils.Md5Version, path))
				};
			}
			return result;
		}

		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor Descriptor = new ScriptControlDescriptor("TIM.T_WEBCTRL.TimNumericUpDown", this.ClientID);
			Descriptor.AddProperty("clientid", this.ClientID);
			Descriptor.AddProperty("serverid", this.ID);
			Descriptor.AddProperty("uniqueid", this.UniqueID);
			bool flag = !this.Enabled || this.ReadOnly;
			if (flag)
			{
				Descriptor.AddProperty("enabled", "N");
			}
			else
			{
				Descriptor.AddProperty("enabled", "Y");
			}
			Descriptor.AddProperty("showZero", this.ShowZero);
			Descriptor.AddProperty("min", this.Min);
			Descriptor.AddProperty("max", this.Max);
			Descriptor.AddProperty("increment", this.Increment);
			Descriptor.AddProperty("decrement", this.Decrement);
			Descriptor.AddProperty("autopostback", this.AutoPostBack);
			Descriptor.AddProperty("preValue", this.Text);
			return new ScriptControlDescriptor[]
			{
				Descriptor
			};
		}
	}
}
