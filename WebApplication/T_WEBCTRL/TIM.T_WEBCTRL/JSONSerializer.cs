using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TIM.T_WEBCTRL
{
	public class JSONSerializer
	{
		public static string ToJavaScriptObjectNotation(object obj)
		{
			bool hasCommaAtEnd = false;
			StringBuilder sbJSON = new StringBuilder("{");
			Type objType = obj.GetType();
			StringBuilder oc = new StringBuilder();
			PropertyInfo[] staticPropertyInfo = CKEditorConfig.GlobalConfig.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			Dictionary<string, object> staticPropertyInfoDictionary = new Dictionary<string, object>();
			PropertyInfo[] array = staticPropertyInfo;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyInfo propertyInfo = array[i];
				staticPropertyInfoDictionary.Add(propertyInfo.Name, propertyInfo.GetValue(CKEditorConfig.GlobalConfig, new object[0]));
			}
			PropertyInfo[] properties = objType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			for (int j = 0; j < properties.Length; j++)
			{
				PropertyInfo propertyInfo2 = properties[j];
				bool canSerializable = false;
				string customName = string.Empty;
				bool removeEnters = false;
				bool forceAddToJSON = false;
				bool isObject = false;
				object[] attrs = propertyInfo2.GetCustomAttributes(false);
				object[] array2 = attrs;
				for (int k = 0; k < array2.Length; k++)
				{
					object attr = array2[k];
					bool flag = attr is CKSerializable;
					if (flag)
					{
						canSerializable = true;
						removeEnters = ((CKSerializable)attr).RemoveEnters;
						forceAddToJSON = ((CKSerializable)attr).ForceAddToJSON;
						isObject = ((CKSerializable)attr).IsObject;
						bool flag2 = !string.IsNullOrEmpty(((CKSerializable)attr).Name);
						if (flag2)
						{
							customName = ((CKSerializable)attr).Name;
						}
						break;
					}
				}
				bool flag3 = !canSerializable;
				if (!flag3)
				{
					bool flag4 = !propertyInfo2.CanRead;
					if (!flag4)
					{
						bool flag5 = customName == "ExtraOptions";
						if (flag5)
						{
							object[] array3 = (object[])propertyInfo2.GetValue(obj, new object[0]);
							for (int l = 0; l < array3.Length; l++)
							{
								object[] item = (object[])array3[l];
								oc.Append(", ").Append("\"" + item[0].ToString() + "\" : ").Append(item[1].ToString());
							}
						}
						else
						{
							bool flag6 = customName == "stylesSet" && ((string[])propertyInfo2.GetValue(obj, new object[0])).Length == 1 && !((string[])propertyInfo2.GetValue(obj, new object[0]))[0].StartsWith("{");
							if (flag6)
							{
								object staticValueToCompare = staticPropertyInfoDictionary[propertyInfo2.Name];
								bool flag7 = !JSONSerializer.campareArray((string[])propertyInfo2.GetValue(obj, new object[0]), (string[])staticValueToCompare);
								if (flag7)
								{
									string[] valueToJSON = (string[])propertyInfo2.GetValue(obj, new object[0]);
									sbJSON.Append("\"" + (string.IsNullOrEmpty(customName) ? propertyInfo2.Name : customName) + "\" : ");
									bool flag8 = (valueToJSON[0].StartsWith("[") || valueToJSON[0].StartsWith("{")) && (valueToJSON[0].EndsWith("]") || valueToJSON[0].EndsWith("}"));
									if (flag8)
									{
										sbJSON.Append(JSONSerializer.EscapeStringForJavaScript(valueToJSON[0]).TrimStart(new char[]
										{
											'\''
										}).TrimEnd(new char[]
										{
											'\''
										}));
									}
									else
									{
										sbJSON.Append("\"" + JSONSerializer.EscapeStringForJavaScript(valueToJSON[0]).TrimStart(new char[]
										{
											'\''
										}).TrimEnd(new char[]
										{
											'\''
										}) + "\"");
									}
									sbJSON.Append(", ");
								}
								goto IL_860;
							}
							bool flag9 = customName == "on" && ((object[])propertyInfo2.GetValue(obj, new object[0])).Length != 0;
							if (flag9)
							{
								object[] objArray = (object[])((object[])propertyInfo2.GetValue(obj, new object[0]))[0];
								oc.Append(", \"on\" : {").Append(objArray[0]).Append(" : ").Append(objArray[1]).Append(" } ");
							}
							else
							{
								bool flag10 = propertyInfo2.Name == "stylesheetParser_skipSelectors" || propertyInfo2.Name == "stylesheetParser_validSelectors";
								if (flag10)
								{
									string valueToJSON2 = (string)propertyInfo2.GetValue(obj, new object[0]);
									string staticValueToCompare2 = (string)staticPropertyInfoDictionary[propertyInfo2.Name];
									bool flag11 = valueToJSON2 != staticValueToCompare2;
									if (flag11)
									{
										oc.Append(", " + propertyInfo2.Name + ": " + valueToJSON2);
									}
								}
								else
								{
									bool flag12 = propertyInfo2.Name == "entities_processNumerical";
									if (flag12)
									{
										string valueToJSON3 = (string)propertyInfo2.GetValue(obj, new object[0]);
										string staticValueToCompare3 = (string)staticPropertyInfoDictionary[propertyInfo2.Name];
										bool flag13 = valueToJSON3 != staticValueToCompare3;
										if (flag13)
										{
											bool flag14 = valueToJSON3.ToLower() == false.ToString().ToLower() || valueToJSON3.ToLower() == true.ToString().ToLower();
											if (flag14)
											{
												oc.Append(", " + propertyInfo2.Name + ": " + bool.Parse(valueToJSON3).ToString());
											}
											else
											{
												oc.Append(", " + propertyInfo2.Name + ": " + valueToJSON3);
											}
										}
									}
									else
									{
										object valueToJSON4 = propertyInfo2.GetValue(obj, new object[0]);
										object staticValueToCompare4 = staticPropertyInfoDictionary[propertyInfo2.Name];
										bool flag15 = valueToJSON4 == null && staticValueToCompare4 == null;
										if (flag15)
										{
											goto IL_860;
										}
										bool flag16 = valueToJSON4.GetType().IsArray && staticValueToCompare4 == null;
										if (!flag16)
										{
											bool flag17 = !forceAddToJSON && !valueToJSON4.GetType().IsArray && !staticValueToCompare4.GetType().IsArray && staticValueToCompare4.ToString() == valueToJSON4.ToString();
											if (flag17)
											{
												goto IL_860;
											}
											bool flag18 = !forceAddToJSON && valueToJSON4.GetType().IsArray && staticValueToCompare4.GetType().IsArray;
											if (flag18)
											{
												bool flag19 = valueToJSON4.GetType() == typeof(int[]) && JSONSerializer.campareArray((int[])valueToJSON4, (int[])staticValueToCompare4);
												if (flag19)
												{
													goto IL_860;
												}
												bool flag20 = valueToJSON4.GetType() == typeof(string[]) && JSONSerializer.campareArray((string[])valueToJSON4, (string[])staticValueToCompare4);
												if (flag20)
												{
													goto IL_860;
												}
												bool flag21 = valueToJSON4.GetType() == typeof(object[][]) && JSONSerializer.campareArray((object[][])valueToJSON4, (object[][])staticValueToCompare4);
												if (flag21)
												{
													goto IL_860;
												}
												bool flag22 = valueToJSON4.GetType() == typeof(object[]) && JSONSerializer.campareArray((object[])valueToJSON4, (object[])staticValueToCompare4);
												if (flag22)
												{
													goto IL_860;
												}
											}
										}
										sbJSON.Append("\"" + (string.IsNullOrEmpty(customName) ? propertyInfo2.Name : customName) + "\" : ");
										bool flag23 = propertyInfo2.Name == "toolbar" || propertyInfo2.Name == "toolbar_Basic" || propertyInfo2.Name == "toolbar_Full";
										if (flag23)
										{
											bool flag24 = propertyInfo2.Name == "toolbar";
											object toolObj;
											if (flag24)
											{
												toolObj = propertyInfo2.GetValue(obj, new object[0]);
											}
											else
											{
												toolObj = ((object[])propertyInfo2.GetValue(obj, new object[0]))[0];
											}
											bool flag25 = toolObj.GetType() == typeof(string) && ((string)toolObj).StartsWith("[") && ((string)toolObj).EndsWith("]");
											if (flag25)
											{
												isObject = true;
												JSONSerializer.GetFieldOrPropertyValue(ref sbJSON, toolObj, removeEnters, isObject);
												sbJSON.Append(", ");
												goto IL_860;
											}
										}
										JSONSerializer.GetFieldOrPropertyValue(ref sbJSON, propertyInfo2.GetValue(obj, new object[0]), removeEnters, isObject);
										sbJSON.Append(", ");
									}
								}
							}
						}
						hasCommaAtEnd = true;
					}
				}
				IL_860:;
			}
			bool flag26 = hasCommaAtEnd;
			if (flag26)
			{
				sbJSON.Length -= 2;
			}
			sbJSON.Append(oc.ToString()).Append("}");
			return sbJSON.ToString();
		}

		private static bool campareArray(string[] obj1, string[] obj2)
		{
			bool flag = !obj1.GetType().IsArray || !obj2.GetType().IsArray;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = obj1.Length != obj2.Length;
				if (flag2)
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < obj1.Length; i++)
					{
						bool flag3 = obj1[i].ToString() != obj2[i].ToString();
						if (flag3)
						{
							result = false;
							return result;
						}
					}
					result = true;
				}
			}
			return result;
		}

		private static bool campareArray(int[] obj1, int[] obj2)
		{
			bool flag = !obj1.GetType().IsArray || !obj2.GetType().IsArray;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = obj1.Length != obj2.Length;
				if (flag2)
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < obj1.Length; i++)
					{
						bool flag3 = obj1[i].ToString() != obj2[i].ToString();
						if (flag3)
						{
							result = false;
							return result;
						}
					}
					result = true;
				}
			}
			return result;
		}

		private static bool campareArray(object[][] obj1, object[][] obj2)
		{
			bool flag = !obj1.GetType().IsArray || !obj2.GetType().IsArray;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = obj1.Length != obj2.Length;
				if (flag2)
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < obj1.Length; i++)
					{
						bool flag3 = !JSONSerializer.campareArray(obj1[i], obj2[i]);
						if (flag3)
						{
							result = false;
							return result;
						}
					}
					result = true;
				}
			}
			return result;
		}

		private static bool campareArray(object[] obj1, object[] obj2)
		{
			bool flag = !obj1.GetType().IsArray || !obj2.GetType().IsArray;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = obj1.Length != obj2.Length;
				if (flag2)
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < obj1.Length; i++)
					{
						bool flag3 = obj1[i].GetType().IsArray && !JSONSerializer.campareArray((object[])obj1[i], (object[])obj2[i]);
						if (flag3)
						{
							result = false;
							return result;
						}
						bool flag4 = obj1[i].ToString() != obj2[i].ToString();
						if (flag4)
						{
							result = false;
							return result;
						}
					}
					result = true;
				}
			}
			return result;
		}

		private static void GetFieldOrPropertyValue(ref StringBuilder sbJSON, object value, bool removeEnters, bool isObject)
		{
			Type type = value.GetType();
			bool flag = type == typeof(DateTime);
			if (flag)
			{
				sbJSON.Append("new Date(" + ((DateTime)value - DateTime.Parse("1/1/1970")).TotalMilliseconds.ToString() + ")");
			}
			else
			{
				bool flag2 = type == typeof(string);
				if (flag2)
				{
					string app = isObject ? string.Empty : "\"";
					sbJSON.Append(app + JSONSerializer.EscapeStringForJavaScript(removeEnters ? ((string)value).Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\b", "") : ((string)value)) + app);
				}
				else
				{
					bool flag3 = type == typeof(short) || type == typeof(int) || type == typeof(long);
					if (flag3)
					{
						sbJSON.Append(value.ToString());
					}
					else
					{
						bool flag4 = type == typeof(decimal) || type == typeof(double) || type == typeof(float);
						if (flag4)
						{
							sbJSON.Append(value.ToString().Replace(',', '.'));
						}
						else
						{
							bool isArray = type.IsArray;
							if (isArray)
							{
								bool hasCommaAtEnd = false;
								sbJSON.Append("[");
								foreach (object o in ((Array)value))
								{
									JSONSerializer.GetFieldOrPropertyValue(ref sbJSON, o, removeEnters, isObject);
									sbJSON.Append(", ");
									hasCommaAtEnd = true;
								}
								bool flag5 = hasCommaAtEnd;
								if (flag5)
								{
									sbJSON.Length -= 2;
								}
								sbJSON.Append("]");
							}
							else
							{
								bool flag6 = type == typeof(bool);
								if (flag6)
								{
									sbJSON.Append(value.ToString().ToLower());
								}
								else
								{
									sbJSON.Append(JSONSerializer.EscapeStringForJavaScript(value.ToString()));
								}
							}
						}
					}
				}
			}
		}

		private static string EscapeStringForJavaScript(string input)
		{
			input = input.Replace("\\", "\\\\");
			input = input.Replace("\b", "\\u0008");
			input = input.Replace("\t", "\\u0009");
			input = input.Replace("\n", "\\u000a");
			input = input.Replace("\f", "\\u000c");
			input = input.Replace("\r", "\\u000d");
			input = input.Replace("\"", "\"");
			input = input.Replace("\"", "\\\"");
			return input;
		}
	}
}
