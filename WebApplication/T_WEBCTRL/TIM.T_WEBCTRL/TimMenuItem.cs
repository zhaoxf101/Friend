using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ParseChildren(true)]
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TimMenuItem : IStateManager
	{
		private string _value;

		private string _text;

		private string _fatherValue;

		private bool _enabled;

		private Color _ChartColor = (Color)new WebColorConverter().ConvertFromString("#F6F6F6");

		private bool _remark;

		[Category("attribute"), DefaultValue(""), Description("文本")]
		public string Text
		{
			get
			{
				bool flag = this._text != null;
				string result;
				if (flag)
				{
					result = this._text;
				}
				else
				{
					result = string.Empty;
				}
				return result;
			}
			set
			{
				this._text = value;
			}
		}

		[Category("attribute"), DefaultValue(""), Description("值")]
		public string Value
		{
			get
			{
				bool flag = this._value != null;
				string result;
				if (flag)
				{
					result = this._value;
				}
				else
				{
					result = string.Empty;
				}
				return result;
			}
			set
			{
				this._value = value;
			}
		}

		[Category("attribute"), DefaultValue(true), Description("是否可用")]
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}

		[Category("attribute"), DefaultValue(""), Description("父节点值")]
		public string FatherValue
		{
			get
			{
				return this._fatherValue;
			}
			set
			{
				this._fatherValue = value;
			}
		}

		[Bindable(false), Category("attribute"), DefaultValue(typeof(Color), "#F6F6F6"), Description("图例颜色"), TypeConverter(typeof(WebColorConverter))]
		public Color ChartColor
		{
			get
			{
				return this._ChartColor;
			}
			set
			{
				this._ChartColor = value;
			}
		}

		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._remark;
			}
		}

		public TimMenuItem()
		{
			this.Text = "";
			this.Value = "";
			this.Enabled = true;
			this.FatherValue = "";
			this.ChartColor = (Color)new WebColorConverter().ConvertFromString("#F6F6F6");
		}

		public TimMenuItem(string text, string value)
		{
			this.Text = text;
			this.Value = value;
			this.Enabled = true;
			this.FatherValue = "";
			this.ChartColor = (Color)new WebColorConverter().ConvertFromString("#F6F6F6");
		}

		public TimMenuItem(string text, string value, string fatherV)
		{
			this.Text = text;
			this.Value = value;
			this.Enabled = true;
			this.FatherValue = fatherV;
			this.ChartColor = (Color)new WebColorConverter().ConvertFromString("#F6F6F6");
		}

		public TimMenuItem(string text, string value, Color chartColor)
		{
			this.Text = text;
			this.Value = value;
			this.Enabled = true;
			this.FatherValue = "";
			this.ChartColor = chartColor;
		}

		public TimMenuItem GetFatherNode(TimMenuItemCollection col)
		{
			TimMenuItem tn = null;
			bool flag = col != null;
			if (flag)
			{
				for (int i = 0; i < col.Count; i++)
				{
					bool flag2 = col[i].Value == this.FatherValue;
					if (flag2)
					{
						tn = col[i];
					}
				}
			}
			return tn;
		}

		public TimMenuItemCollection GetChilds(TimMenuItemCollection col)
		{
			TimMenuItemCollection tnc = new TimMenuItemCollection();
			bool flag = col != null;
			TimMenuItemCollection result;
			if (flag)
			{
				for (int i = 0; i < col.Count; i++)
				{
					bool flag2 = col[i].FatherValue == this.Value;
					if (flag2)
					{
						tnc.Add(col[i]);
					}
				}
				result = tnc;
			}
			else
			{
				result = null;
			}
			return result;
		}

		void IStateManager.LoadViewState(object state)
		{
			bool flag = state != null;
			if (flag)
			{
				object[] states = (object[])state;
				bool flag2 = states[0] != null;
				if (flag2)
				{
					this._value = (string)states[0];
				}
				bool flag3 = states[1] != null;
				if (flag3)
				{
					this._text = (string)states[1];
				}
				bool flag4 = states[2] != null;
				if (flag4)
				{
					this._enabled = (bool)states[2];
				}
				bool flag5 = states[3] != null;
				if (flag5)
				{
					this._fatherValue = (string)states[3];
				}
				bool flag6 = states[4] != null;
				if (flag6)
				{
					this._ChartColor = (Color)states[4];
				}
			}
		}

		object IStateManager.SaveViewState()
		{
			object[] objs = new object[5];
			bool flag = this._value != string.Empty;
			if (flag)
			{
				objs[0] = this._value;
			}
			bool flag2 = this._text != string.Empty;
			if (flag2)
			{
				objs[1] = this._text;
			}
			bool flag3 = !this._enabled;
			if (flag3)
			{
				objs[2] = this._enabled;
			}
			bool flag4 = this._fatherValue != string.Empty;
			if (flag4)
			{
				objs[3] = this._fatherValue;
			}
			bool flag5 = !this._ChartColor.IsEmpty;
			if (flag5)
			{
				objs[4] = this._ChartColor;
			}
			return objs;
		}

		void IStateManager.TrackViewState()
		{
			this._remark = true;
		}
	}
}
