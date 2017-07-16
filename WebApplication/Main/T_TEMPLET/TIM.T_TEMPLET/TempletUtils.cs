using System;
using System.Web.UI;
using TIM.T_KERNEL.Helper;
using TIM.T_TEMPLET.Page;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET
{
	public class TempletUtils
	{
		internal static void SetInsertCtrlVal(Control place, FieldMapAttribute fieldMap)
		{
			bool flag = string.IsNullOrEmpty(fieldMap.CtrlId);
			if (!flag)
			{
				Control ctrl = place.FindControl(fieldMap.CtrlId);
				bool flag2 = ctrl == null;
				if (!flag2)
				{
					switch (fieldMap.CtrlType)
					{
					case ControlType.HiddenField:
						((TimHiddenField)ctrl).Value = fieldMap.DefValue;
						break;
					case ControlType.TextBox:
						((TimTextBox)ctrl).Text = fieldMap.DefValue;
						break;
					case ControlType.NumericTextBox:
						((TimNumericTextBox)ctrl).Value = (string.IsNullOrEmpty(fieldMap.DefValue.TrimEnd(new char[]
						{
							' '
						})) ? 0.0 : fieldMap.DefValue.ToDouble());
						break;
					case ControlType.NumericUpDown:
						((TimNumericUpDown)ctrl).Value = (string.IsNullOrEmpty(fieldMap.DefValue.TrimEnd(new char[]
						{
							' '
						})) ? 0 : fieldMap.DefValue.ToInt());
						break;
					case ControlType.CheckBox:
						((TimCheckBox)ctrl).Checked = fieldMap.DefValue.Trim().ToBool();
						break;
					case ControlType.CheckBoxList:
						((TimCheckBoxList)ctrl).CheckedValue = fieldMap.DefValue;
						break;
					case ControlType.DropDownList:
						((TimDropDownList)ctrl).SelectedValue = fieldMap.DefValue.Trim();
						break;
					case ControlType.DateTime:
						((TimDateTime)ctrl).Text = fieldMap.DefValue.Trim();
						break;
					case ControlType.Date:
						((TimDate)ctrl).Text = (string.IsNullOrEmpty(fieldMap.DefValue) ? "" : fieldMap.DefValue.Trim().ToDate().ToShortDateString());
						break;
					case ControlType.CheckedBoxList:
						((TimCheckedBoxList)ctrl).CheckedValue = fieldMap.DefValue;
						break;
					case ControlType.HtmlEditor:
						((TimCKEditor)ctrl).Text = fieldMap.DefValue;
						break;
					}
				}
			}
		}

		internal static void SetCopyCtrlVal(Control place, FieldMapAttribute fieldMap)
		{
			bool flag = string.IsNullOrEmpty(fieldMap.CtrlId);
			if (!flag)
			{
				bool key = fieldMap.Key;
				if (key)
				{
					Control ctrl = place.FindControl(fieldMap.CtrlId);
					bool flag2 = ctrl == null;
					if (!flag2)
					{
						switch (fieldMap.CtrlType)
						{
						case ControlType.HiddenField:
							((TimHiddenField)ctrl).Value = fieldMap.DefValue;
							break;
						case ControlType.TextBox:
							((TimTextBox)ctrl).Text = fieldMap.DefValue;
							break;
						case ControlType.NumericTextBox:
							((TimNumericTextBox)ctrl).Value = (string.IsNullOrEmpty(fieldMap.DefValue.TrimEnd(new char[]
							{
								' '
							})) ? 0.0 : fieldMap.DefValue.ToDouble());
							break;
						case ControlType.NumericUpDown:
							((TimNumericUpDown)ctrl).Value = (string.IsNullOrEmpty(fieldMap.DefValue.TrimEnd(new char[]
							{
								' '
							})) ? 0 : fieldMap.DefValue.ToInt());
							break;
						case ControlType.CheckBox:
							((TimCheckBox)ctrl).Checked = fieldMap.DefValue.Trim().ToBool();
							break;
						case ControlType.CheckBoxList:
							((TimCheckBoxList)ctrl).CheckedValue = fieldMap.DefValue;
							break;
						case ControlType.DropDownList:
							((TimDropDownList)ctrl).SelectedValue = fieldMap.DefValue.Trim();
							break;
						case ControlType.DateTime:
							((TimDateTime)ctrl).Text = fieldMap.DefValue.Trim();
							break;
						case ControlType.Date:
							((TimDate)ctrl).Text = (string.IsNullOrEmpty(fieldMap.DefValue) ? "" : fieldMap.DefValue.Trim().ToDate().ToShortDateString());
							break;
						case ControlType.CheckedBoxList:
							((TimCheckedBoxList)ctrl).CheckedValue = fieldMap.DefValue;
							break;
						case ControlType.HtmlEditor:
							((TimCKEditor)ctrl).Text = fieldMap.DefValue;
							break;
						}
					}
				}
			}
		}

		internal static void SetNullCtrlVal(Control place, FieldMapAttribute fieldMap)
		{
			bool flag = string.IsNullOrEmpty(fieldMap.CtrlId);
			if (!flag)
			{
				Control ctrl = place.FindControl(fieldMap.CtrlId);
				bool flag2 = ctrl == null;
				if (!flag2)
				{
					switch (fieldMap.CtrlType)
					{
					case ControlType.HiddenField:
						((TimHiddenField)ctrl).Value = "";
						break;
					case ControlType.TextBox:
						((TimTextBox)ctrl).Text = "";
						break;
					case ControlType.NumericTextBox:
						((TimNumericTextBox)ctrl).Value = 0.0;
						break;
					case ControlType.NumericUpDown:
						((TimNumericUpDown)ctrl).Value = 0;
						break;
					case ControlType.CheckBox:
						((TimCheckBox)ctrl).Checked = false;
						break;
					case ControlType.CheckBoxList:
						((TimCheckBoxList)ctrl).CheckedValue = "";
						break;
					case ControlType.DropDownList:
						((TimDropDownList)ctrl).SelectedValue = "";
						break;
					case ControlType.DateTime:
						((TimDateTime)ctrl).Text = "";
						break;
					case ControlType.Date:
						((TimDate)ctrl).Text = "";
						break;
					case ControlType.CheckedBoxList:
						((TimCheckedBoxList)ctrl).CheckedValue = "";
						break;
					case ControlType.HtmlEditor:
						((TimCKEditor)ctrl).Text = "";
						break;
					}
				}
			}
		}

		internal static void SetCtrlValByRecord(Control place, FieldMapAttribute fieldMap)
		{
			bool flag = string.IsNullOrEmpty(fieldMap.CtrlId);
			if (!flag)
			{
				Control ctrl = place.FindControl(fieldMap.CtrlId);
				bool flag2 = ctrl == null;
				if (!flag2)
				{
					switch (fieldMap.CtrlType)
					{
					case ControlType.HiddenField:
						((TimHiddenField)ctrl).Value = fieldMap.NewValue.TrimEnd(new char[]
						{
							' '
						});
						break;
					case ControlType.TextBox:
						((TimTextBox)ctrl).Text = fieldMap.NewValue.TrimEnd(new char[]
						{
							' '
						});
						break;
					case ControlType.NumericTextBox:
						((TimNumericTextBox)ctrl).Value = (string.IsNullOrEmpty(fieldMap.NewValue) ? 0.0 : fieldMap.NewValue.TrimEnd(new char[]
						{
							' '
						}).ToDouble());
						break;
					case ControlType.NumericUpDown:
						((TimNumericUpDown)ctrl).Value = (string.IsNullOrEmpty(fieldMap.NewValue) ? 0 : fieldMap.NewValue.TrimEnd(new char[]
						{
							' '
						}).ToInt());
						break;
					case ControlType.CheckBox:
						((TimCheckBox)ctrl).Checked = fieldMap.NewValue.Trim().ToBool();
						break;
					case ControlType.CheckBoxList:
						((TimCheckBoxList)ctrl).CheckedValue = fieldMap.NewValue.Trim();
						break;
					case ControlType.DropDownList:
						((TimDropDownList)ctrl).SelectedValue = fieldMap.NewValue.Trim();
						break;
					case ControlType.DateTime:
						((TimDateTime)ctrl).Text = fieldMap.NewValue.Trim();
						break;
					case ControlType.Date:
						((TimDate)ctrl).Text = (string.IsNullOrEmpty(fieldMap.NewValue) ? "" : fieldMap.NewValue.Trim().ToDate().ToShortDateString());
						break;
					case ControlType.CheckedBoxList:
						((TimCheckedBoxList)ctrl).CheckedValue = fieldMap.NewValue.Trim();
						break;
					case ControlType.HtmlEditor:
						((TimCKEditor)ctrl).Text = fieldMap.NewValue.Trim();
						break;
					}
				}
			}
		}

		internal static void GetCtrlValByPage(Control place, FieldMapAttribute fieldMap)
		{
			bool flag = string.IsNullOrEmpty(fieldMap.CtrlId);
			if (!flag)
			{
				Control ctrl = place.FindControl(fieldMap.CtrlId);
				bool flag2 = ctrl == null;
				if (!flag2)
				{
					switch (fieldMap.CtrlType)
					{
					case ControlType.HiddenField:
						fieldMap.NewValue = ((TimHiddenField)ctrl).Value.TrimEnd(new char[]
						{
							' '
						});
						break;
					case ControlType.TextBox:
						fieldMap.NewValue = ((TimTextBox)ctrl).Text.TrimEnd(new char[]
						{
							' '
						});
						break;
					case ControlType.NumericTextBox:
						fieldMap.NewValue = ((TimNumericTextBox)ctrl).Value.ToString();
						break;
					case ControlType.NumericUpDown:
						fieldMap.NewValue = ((TimNumericUpDown)ctrl).Value.ToString();
						break;
					case ControlType.CheckBox:
						fieldMap.NewValue = ((TimCheckBox)ctrl).Checked.ToYOrN();
						break;
					case ControlType.CheckBoxList:
						fieldMap.NewValue = ((TimCheckBoxList)ctrl).CheckedValue;
						break;
					case ControlType.DropDownList:
						fieldMap.NewValue = ((TimDropDownList)ctrl).SelectedValue;
						break;
					case ControlType.DateTime:
						fieldMap.NewValue = ((TimDateTime)ctrl).Text;
						break;
					case ControlType.Date:
						fieldMap.NewValue = ((TimDate)ctrl).Text;
						break;
					case ControlType.CheckedBoxList:
						fieldMap.NewValue = ((TimCheckedBoxList)ctrl).CheckedValue;
						break;
					case ControlType.HtmlEditor:
						fieldMap.NewValue = ((TimCKEditor)ctrl).Text;
						break;
					}
					bool key = fieldMap.Key;
					if (key)
					{
						fieldMap.NewValue = (string.IsNullOrWhiteSpace(fieldMap.NewValue.Trim()) ? " " : fieldMap.NewValue.Trim());
					}
				}
			}
		}
	}
}
