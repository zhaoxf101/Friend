using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ToolboxData("<{0}:TimNumericTextBox runat=server></{0}:TimNumericTextBox>")]
	public class TimNumericTextBox : TextBox, IScriptControl, IPostBackDataHandler
	{
		private ScriptManager _sm;

		private string m_dbField = string.Empty;

		private TimCtrlEndSymbol m_symbol = TimCtrlEndSymbol.Null;

		private double m_min = 0.0;

		private double m_max = 0.0;

		private double m_rawValue = 0.0;

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
		public double Min
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
		public double Max
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

		[DefaultValue(2), Description("小数位数")]
		public int DecimalPlaces
		{
			get
			{
				object obj = this.ViewState["DecimalPlaces"];
				bool flag = obj == null;
				int result2;
				if (flag)
				{
					result2 = 0;
				}
				else
				{
					int result = (int)obj;
					result2 = ((result < 0) ? 0 : result);
				}
				return result2;
			}
			set
			{
				this.ViewState["DecimalPlaces"] = value;
			}
		}

		[DefaultValue(true), Description("文本格式化为设定的数值格式")]
		public bool FormatText
		{
			get
			{
				object obj = this.ViewState["FormatText"];
				return obj == null || (bool)obj;
			}
			set
			{
				double preRawValue = this.RawValue;
				this.ViewState["FormatText"] = value;
				this.Text = this.ValueToText(this.Value, value);
				this.RawValue = preRawValue;
			}
		}

		[Browsable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				double tmpValue = 0.0;
				double.TryParse(value, out tmpValue);
				base.Text = this.ValueToText(tmpValue, this.FormatText);
				this.RawValue = this.VerifyValue(tmpValue);
			}
		}

		[DefaultValue(0), Description("控件的值")]
		public double Value
		{
			get
			{
				return this.VerifyValue(this.RawValue);
			}
			set
			{
				double preRawValue = this.VerifyValue(value);
				this.Text = this.ValueToText(preRawValue, this.FormatText);
				this.RawValue = preRawValue;
			}
		}

		[DefaultValue(0), Description("控件的原始值")]
		private double RawValue
		{
			get
			{
				return this.m_rawValue;
			}
			set
			{
				this.m_rawValue = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("零是否显示")]
		public bool ShowZero
		{
			get
			{
				object obj = this.ViewState["ShowZero"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ShowZero"] = value;
			}
		}

		public TimNumericTextBox()
		{
			this.DecimalPlaces = 2;
			this.FormatText = true;
			this.Value = 0.0;
			this.Width = new Unit(200);
			this.Height = new Unit(20);
		}

		protected override void OnPreRender(EventArgs e)
		{
			ScriptManager.RegisterHiddenField(this, string.Format("{0}_RawValue", this.ClientID), this.Value.ToString());
			bool flag = !base.DesignMode;
			if (flag)
			{
				this._sm = ScriptManager.GetCurrent(this.Page);
				bool flag2 = this._sm == null;
				if (flag2)
				{
					throw new HttpException("A ScriptManager control must exist on the current page.");
				}
				this._sm.RegisterScriptControl<TimNumericTextBox>(this);
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
			bool flag2 = !this.Enabled || this.ReadOnly;
			if (flag2)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#F5FFFA");
			}
			base.Attributes["AutoPostBack"] = this.AutoPostBack.ToString();
			base.Render(writer);
			TimCtrlUtils.WriteEndSymbol(writer, this.Symbol);
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddStyleAttribute("text-align", "right");
			writer.AddAttribute("DecimalPlaces", this.DecimalPlaces.ToString());
			writer.AddAttribute("ShowZero", this.ShowZero.ToString());
			writer.AddAttribute("NegativeColor", "#000000");
			writer.AddAttribute("RawMin", this.Min.ToString());
			writer.AddAttribute("RawMax", this.Max.ToString());
			writer.AddAttribute("onkeydown", "Enter2Tab(this,event);");
			bool showZero = this.ShowZero;
			if (showZero)
			{
				writer.AddAttribute("RawValue", this.RawValue.ToString());
			}
			else
			{
				writer.AddAttribute("RawValue", this.RawValue.ToString().Equals("0") ? "" : this.RawValue.ToString());
			}
		}

		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool flag = !string.IsNullOrEmpty(postCollection[string.Format("{0}_RawValue", this.ClientID)]);
			if (flag)
			{
				double tmpRawValue = 0.0;
				double.TryParse(postCollection[string.Format("{0}_RawValue", this.ClientID)], out tmpRawValue);
				this.RawValue = tmpRawValue;
			}
			return base.LoadPostData(postDataKey, postCollection);
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
					new ScriptReference(string.Format("{0}TimNumericTextBox.js?v=" + TimCtrlUtils.Md5Version, path))
				};
			}
			else
			{
				result = new ScriptReference[]
				{
					new ScriptReference(string.Format("{0}TimNumericTextBox.js?v=" + TimCtrlUtils.Md5Version, path))
				};
			}
			return result;
		}

		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor Descriptor = new ScriptControlDescriptor("TIM.T_WEBCTRL.TimNumericTextBox", this.ClientID);
			Descriptor.AddProperty("serverId", this.ID);
			Descriptor.AddProperty("clientId", this.ClientID);
			Descriptor.AddProperty("decimalPlaces", this.DecimalPlaces);
			Descriptor.AddProperty("showZero", this.ShowZero);
			Descriptor.AddProperty("formatText", this.FormatText);
			Descriptor.AddProperty("min", this.Min);
			Descriptor.AddProperty("max", this.Max);
			Descriptor.AddProperty("value", this.Value);
			return new ScriptControlDescriptor[]
			{
				Descriptor
			};
		}

		private double VerifyValue(double value)
		{
			bool flag = this.Min != this.Max;
			double result;
			if (flag)
			{
				bool flag2 = this.Min > this.Max;
				if (flag2)
				{
					this.Max = 1.7976931348623157E+308;
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

		private string TextToValue(string text)
		{
			string result = text.Trim();
			try
			{
				result = Convert.ToDouble(result.Replace(",", "")).ToString(string.Format("N{0}", this.DecimalPlaces));
			}
			catch
			{
				result = string.Empty;
			}
			bool flag = result == string.Empty;
			if (flag)
			{
				result = "0";
			}
			else
			{
				bool flag2 = result == "-";
				if (flag2)
				{
					result = "-0";
				}
			}
			return result;
		}

		private string ValueToText(double value, bool formatText)
		{
			string result;
			if (formatText)
			{
				bool flag = !this.ShowZero && Math.Round(value, this.DecimalPlaces) == 0.0;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = value.ToString(string.Format("N{0}", this.DecimalPlaces));
				}
			}
			else
			{
				bool flag2 = !this.ShowZero && value == 0.0;
				if (flag2)
				{
					result = string.Empty;
				}
				else
				{
					result = value.ToString(string.Format("N{0}", this.DecimalPlaces)).Replace(",", "");
				}
			}
			return result;
		}
	}
}
