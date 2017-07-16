using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TIM.T_KERNEL.Compute
{
	public class ComputeEngine
	{
		public delegate bool UnknownFunction(string functionName, ArrayList paramList, out object result);

		public delegate bool UnknownVariable(string variable, out object result);

		private string exp;

		private int expIdx;

		private string token;

		private ComputeEngineTokenType tokType;

		private string FunctionName;

		private int FunctionIndex;

		private ArrayList FunctionParameters;

		private MethodInfo[] mm;

		private SortedList InnerFunctions;

		private ComputeEngineInnerFunc InnerF;

		private object ExternFuncClass;

		private Type ExternFuncType;

		private MethodInfo[] ExternMethod;

		private SortedList ExternFunctions;

		private Dictionary<string, string> ExternFuncNameList;

		private Dictionary<string, object> InnerVariables;

		private Dictionary<string, object> ExternVariables;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event ComputeEngine.UnknownFunction OnUnknownFunction;

		[method: CompilerGenerated]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event ComputeEngine.UnknownVariable OnUnknownVariable;

		private bool ComputeEngine_OnUnknownVariable(string variable, out object result)
		{
			result = 0;
			return false;
		}

		private bool ComputeEngine_OnUnknownFunction(string functionName, ArrayList paramList, out object result)
		{
			result = 0;
			return false;
		}

		public ComputeEngine()
		{
			this.OnUnknownFunction += new ComputeEngine.UnknownFunction(this.ComputeEngine_OnUnknownFunction);
			this.OnUnknownVariable += new ComputeEngine.UnknownVariable(this.ComputeEngine_OnUnknownVariable);
			this.InnerF = new ComputeEngineInnerFunc();
			this.InnerFunctions = new SortedList();
			this.ExternFunctions = new SortedList();
			this.AddInnerFunctions();
			this.ExternFuncNameList = new Dictionary<string, string>();
			this.InnerVariables = new Dictionary<string, object>();
			this.ExternVariables = new Dictionary<string, object>();
			this.AddInnerVariables();
		}

		public object Evaluate(string expstr)
		{
			object result = null;
			this.exp = expstr;
			this.expIdx = 0;
			this.GetToken();
			bool flag = this.tokType == ComputeEngineTokenType.TkNone && this.token == "";
			if (flag)
			{
				throw new ComputeEngineParserException("计算表达式不存在！");
			}
			this.EvalExp0(out result);
			bool flag2 = this.token != "";
			if (flag2)
			{
				throw new ComputeEngineParserException("计算表达式语法错误！");
			}
			return result;
		}

		private double GetDoubleParameter(int ParamIndex)
		{
			bool flag = ParamIndex >= this.FunctionParameters.Count || ParamIndex < 0;
			if (flag)
			{
				throw new ComputeEngineParserException("参数Index超出范围！");
			}
			bool flag2 = !this.FunctionParameters[ParamIndex].GetType().ToString().ToUpper().Equals("SYSTEM.DOUBLE");
			if (flag2)
			{
				throw new ComputeEngineParserException("参数类型不符合期望值Double！");
			}
			return Convert.ToDouble(this.FunctionParameters[ParamIndex]);
		}

		private string GetStringParameter(int ParamIndex)
		{
			bool flag = ParamIndex >= this.FunctionParameters.Count || ParamIndex < 0;
			if (flag)
			{
				throw new ComputeEngineParserException("参数Index超出范围！");
			}
			bool flag2 = !this.FunctionParameters[ParamIndex].GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
			if (flag2)
			{
				throw new ComputeEngineParserException("参数类型不符合期望值String！");
			}
			return this.FunctionParameters[ParamIndex].ToString();
		}

		private string GetParameterInfo(int ParamIndex)
		{
			bool flag = ParamIndex >= this.FunctionParameters.Count || ParamIndex < 0;
			if (flag)
			{
				throw new ComputeEngineParserException("参数Index超出范围！");
			}
			return this.FunctionParameters[ParamIndex].GetType().ToString();
		}

		private void GetToken()
		{
			this.tokType = ComputeEngineTokenType.TkNone;
			this.token = "";
			bool flag = this.expIdx == this.exp.Length;
			if (!flag)
			{
				while (this.expIdx < this.exp.Length && char.IsWhiteSpace(this.exp[this.expIdx]))
				{
					this.expIdx++;
				}
				bool flag2 = this.expIdx == this.exp.Length;
				if (!flag2)
				{
					bool flag3 = this.IsDelim(this.exp[this.expIdx]);
					if (flag3)
					{
						this.token += this.exp[this.expIdx].ToString();
						bool flag4 = this.exp[this.expIdx] == '<';
						if (flag4)
						{
							this.expIdx++;
							bool b = false;
							bool flag5 = this.exp[this.expIdx] == '<' || this.exp[this.expIdx] == '=' || this.exp[this.expIdx] == '>';
							if (flag5)
							{
								this.token += this.exp[this.expIdx].ToString();
								b = true;
							}
							bool flag6 = !b;
							if (flag6)
							{
								this.expIdx--;
							}
						}
						else
						{
							bool flag7 = this.exp[this.expIdx] == '>';
							if (flag7)
							{
								this.expIdx++;
								bool b = false;
								bool flag8 = this.exp[this.expIdx] == '=' || this.exp[this.expIdx] == '>';
								if (flag8)
								{
									this.token += this.exp[this.expIdx].ToString();
									b = true;
								}
								bool flag9 = !b;
								if (flag9)
								{
									this.expIdx--;
								}
							}
						}
						this.expIdx++;
						this.tokType = ComputeEngineTokenType.TkDelimiter;
					}
					else
					{
						bool flag10 = char.IsLetter(this.exp[this.expIdx]) || this.exp[this.expIdx] == '_';
						if (flag10)
						{
							this.token += this.exp[this.expIdx].ToString();
							this.expIdx++;
							bool flag11 = this.expIdx == this.exp.Length;
							if (!flag11)
							{
								while (char.IsLetter(this.exp[this.expIdx]) || char.IsDigit(this.exp[this.expIdx]) || this.exp[this.expIdx] == '.')
								{
									this.token += this.exp[this.expIdx].ToString();
									this.expIdx++;
									bool flag12 = this.expIdx == this.exp.Length;
									if (flag12)
									{
										break;
									}
								}
								bool flag13 = this.expIdx != this.exp.Length;
								if (flag13)
								{
									bool flag14 = this.exp[this.expIdx] == '(';
									if (flag14)
									{
										this.tokType = ComputeEngineTokenType.TkFunction;
									}
									else
									{
										this.tokType = ComputeEngineTokenType.TkVariable;
										this.ReplaceVariable();
									}
								}
								else
								{
									this.tokType = ComputeEngineTokenType.TkVariable;
									this.ReplaceVariable();
								}
							}
						}
						else
						{
							bool flag15 = char.IsDigit(this.exp[this.expIdx]);
							if (flag15)
							{
								while (!this.IsDelim(this.exp[this.expIdx]))
								{
									this.token += this.exp[this.expIdx].ToString();
									this.expIdx++;
									bool flag16 = this.expIdx >= this.exp.Length;
									if (flag16)
									{
										break;
									}
								}
								this.tokType = ComputeEngineTokenType.TkNumber;
							}
							else
							{
								bool flag17 = this.exp[this.expIdx] == '"' || this.exp[this.expIdx] == '\'';
								if (!flag17)
								{
									throw new ComputeEngineParserException("计算表达式定义非法符号" + this.exp[this.expIdx].ToString() + "！");
								}
								this.expIdx++;
								while (this.exp[this.expIdx] != '"' && this.exp[this.expIdx] != '\'')
								{
									this.token += this.exp[this.expIdx].ToString();
									this.expIdx++;
									bool flag18 = this.expIdx > this.exp.Length;
									if (flag18)
									{
										break;
									}
								}
								this.expIdx++;
								this.tokType = ComputeEngineTokenType.TkString;
							}
						}
					}
				}
			}
		}

		private bool IsDelim(char c)
		{
			return "+-/*%&(),|^~<>=".IndexOf(c) != -1;
		}

		private void ReplaceVariable()
		{
			string tokenUpp = this.token.ToUpper();
			bool flag = this.InnerVariables.ContainsKey(tokenUpp);
			if (flag)
			{
				try
				{
					this.token = double.Parse(this.InnerVariables[tokenUpp].ToString().Trim()).ToString();
					this.tokType = ComputeEngineTokenType.TkNumber;
				}
				catch
				{
					this.token = this.InnerVariables[tokenUpp].ToString().Trim();
					this.tokType = ComputeEngineTokenType.TkString;
				}
			}
			else
			{
				bool flag2 = this.ExternVariables.ContainsKey(tokenUpp);
				if (flag2)
				{
					try
					{
						this.token = double.Parse(this.ExternVariables[tokenUpp].ToString().Trim()).ToString();
						this.tokType = ComputeEngineTokenType.TkNumber;
					}
					catch
					{
						this.token = this.ExternVariables[tokenUpp].ToString().Trim();
						this.tokType = ComputeEngineTokenType.TkString;
					}
				}
				else
				{
					object variable = new object();
					bool result = this.EvalUnkownVariable(tokenUpp, out variable);
					bool flag3 = result;
					if (!flag3)
					{
						throw new ComputeEngineParserException("未知变量：" + this.token + "！");
					}
					try
					{
						this.token = double.Parse(variable.ToString().Trim()).ToString();
						this.tokType = ComputeEngineTokenType.TkNumber;
					}
					catch
					{
						this.token = variable.ToString();
						this.tokType = ComputeEngineTokenType.TkString;
					}
				}
			}
		}

		private void EvalExp0(out object result)
		{
			object partialResult = null;
			this.EvalExp01(out result);
			while (this.token == "|")
			{
				int pos = this.expIdx;
				this.GetToken();
				this.EvalExp01(out partialResult);
				bool flag = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING") || partialResult.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
				if (flag)
				{
					throw new ComputeEngineParserException("计算表达式位置：" + pos + " 运算|参数类型错误！");
				}
				result = ((int)result | (int)partialResult);
			}
		}

		private void EvalExp01(out object result)
		{
			object partialResult = null;
			this.EvalExp02(out result);
			while (this.token == "^")
			{
				int pos = this.expIdx;
				this.GetToken();
				this.EvalExp02(out partialResult);
				bool flag = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING") || partialResult.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
				if (flag)
				{
					throw new ComputeEngineParserException("计算表达式位置：" + pos + " 运算^参数类型错误！！");
				}
				result = ((int)result ^ (int)partialResult);
			}
		}

		private void EvalExp02(out object result)
		{
			object partialResult = null;
			this.EvalExp1(out result);
			while (this.token == "&")
			{
				int pos = this.expIdx;
				this.GetToken();
				this.EvalExp1(out partialResult);
				bool flag = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING") || partialResult.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
				if (flag)
				{
					throw new ComputeEngineParserException("计算表达式位置：" + pos + " 运算&参数类型错误！");
				}
				result = ((int)result & (int)partialResult);
			}
		}

		private void EvalExp1(out object result)
		{
			object partialResult = null;
			this.EvalExp11(out result);
			string op;
			while ((op = this.token) == "=" || op == "<" || op == "<=" || op == ">" || op == ">=" || op == "<>")
			{
				int pos = this.expIdx;
				this.GetToken();
				this.EvalExp11(out partialResult);
				bool flag = result.GetType().ToString() != partialResult.GetType().ToString();
				if (flag)
				{
					throw new ComputeEngineParserException(string.Concat(new object[]
					{
						"计算表达式位置：",
						pos,
						" 运算",
						op,
						"参数类型不匹配！"
					}));
				}
				string a = op;
				if (!(a == "="))
				{
					if (!(a == "<"))
					{
						if (!(a == "<="))
						{
							if (!(a == ">"))
							{
								if (!(a == ">="))
								{
									if (a == "<>")
									{
										bool flag2 = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
										if (flag2)
										{
											result = ((result.ToString().Trim() != partialResult.ToString().Trim()) ? 1 : 0);
										}
										else
										{
											result = ((Convert.ToDouble(result) != Convert.ToDouble(partialResult)) ? 1 : 0);
										}
									}
								}
								else
								{
									bool flag3 = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
									if (flag3)
									{
										result = ((string.Compare(result.ToString().Trim(), partialResult.ToString().Trim()) >= 0) ? 1 : 0);
									}
									else
									{
										result = ((Convert.ToDouble(result) >= Convert.ToDouble(partialResult)) ? 1 : 0);
									}
								}
							}
							else
							{
								bool flag4 = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
								if (flag4)
								{
									result = ((string.Compare(result.ToString().Trim(), partialResult.ToString().Trim()) > 0) ? 1 : 0);
								}
								else
								{
									result = ((Convert.ToDouble(result) > Convert.ToDouble(partialResult)) ? 1 : 0);
								}
							}
						}
						else
						{
							bool flag5 = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
							if (flag5)
							{
								result = ((string.Compare(result.ToString().Trim(), partialResult.ToString().Trim()) <= 0) ? 1 : 0);
							}
							else
							{
								result = ((Convert.ToDouble(result) <= Convert.ToDouble(partialResult)) ? 1 : 0);
							}
						}
					}
					else
					{
						bool flag6 = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
						if (flag6)
						{
							result = ((string.Compare(result.ToString().Trim(), partialResult.ToString().Trim()) < 0) ? 1 : 0);
						}
						else
						{
							result = ((Convert.ToDouble(result) < Convert.ToDouble(partialResult)) ? 1 : 0);
						}
					}
				}
				else
				{
					bool flag7 = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
					if (flag7)
					{
						result = ((result.ToString().Trim() == partialResult.ToString().Trim()) ? 1 : 0);
					}
					else
					{
						result = ((Convert.ToDouble(result) == Convert.ToDouble(partialResult)) ? 1 : 0);
					}
				}
			}
		}

		private void EvalExp11(out object result)
		{
			object partialResult = null;
			this.EvalExp2(out result);
			string op;
			while ((op = this.token) == "<<" || op == ">>")
			{
				int pos = this.expIdx;
				this.GetToken();
				this.EvalExp2(out partialResult);
				bool flag = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING") || partialResult.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
				if (flag)
				{
					throw new ComputeEngineParserException(string.Concat(new object[]
					{
						"计算表达式位置：",
						pos,
						" 运算",
						op,
						"参数类型错误！"
					}));
				}
				string a = op;
				if (!(a == "<<"))
				{
					if (a == ">>")
					{
						double x = Convert.ToDouble(partialResult);
						bool flag2 = x == 0.0;
						if (flag2)
						{
							result = Convert.ToDouble(result);
						}
						else
						{
							result = Convert.ToDouble(result) * (double)((int)Math.Pow(10.0, (double)((int)Convert.ToDouble(partialResult))));
						}
					}
				}
				else
				{
					double x = Convert.ToDouble(partialResult);
					bool flag3 = x == 0.0;
					if (flag3)
					{
						result = Convert.ToDouble(result);
					}
					else
					{
						result = Convert.ToDouble(result) / (double)((int)Math.Pow(10.0, (double)((int)Convert.ToDouble(partialResult))));
					}
				}
			}
		}

		private void EvalExp2(out object result)
		{
			object partialResult = null;
			this.EvalExp3(out result);
			string op;
			while ((op = this.token) == "+" || op == "-")
			{
				int pos = this.expIdx;
				this.GetToken();
				this.EvalExp3(out partialResult);
				string a = op;
				if (!(a == "-"))
				{
					if (a == "+")
					{
						bool flag = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING") || partialResult.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
						if (flag)
						{
							throw new ComputeEngineParserException(string.Concat(new object[]
							{
								"位置：",
								pos,
								" 运算",
								op,
								"参数类型错误！"
							}));
						}
						result = Convert.ToDouble(result) + Convert.ToDouble(partialResult);
					}
				}
				else
				{
					bool flag2 = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING") || partialResult.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
					if (flag2)
					{
						throw new ComputeEngineParserException(string.Concat(new object[]
						{
							"位置：",
							pos,
							" 运算",
							op,
							"参数类型错误！"
						}));
					}
					result = Convert.ToDouble(result) - Convert.ToDouble(partialResult);
				}
			}
		}

		private void EvalExp3(out object result)
		{
			object partialResult = null;
			this.EvalExp4(out result);
			string op;
			while ((op = this.token) == "*" || op == "/" || op == "%")
			{
				int pos = this.expIdx;
				this.GetToken();
				this.EvalExp4(out partialResult);
				string a = op;
				if (!(a == "*"))
				{
					if (!(a == "/"))
					{
						if (a == "%")
						{
							bool flag = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING") || partialResult.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
							if (flag)
							{
								throw new ComputeEngineParserException(string.Concat(new object[]
								{
									"位置：",
									pos,
									" 运算",
									op,
									"参数类型错误！"
								}));
							}
							bool flag2 = Convert.ToDouble(partialResult) == 0.0;
							if (flag2)
							{
								result = 0;
							}
							else
							{
								result = Convert.ToDouble(result) % Convert.ToDouble(partialResult);
							}
						}
					}
					else
					{
						bool flag3 = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING") || partialResult.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
						if (flag3)
						{
							throw new ComputeEngineParserException(string.Concat(new object[]
							{
								"位置：",
								pos,
								" 运算",
								op,
								"参数类型错误！"
							}));
						}
						bool flag4 = Convert.ToDouble(partialResult) == 0.0;
						if (flag4)
						{
							result = 0;
						}
						else
						{
							result = Convert.ToDouble(result) / Convert.ToDouble(partialResult);
						}
					}
				}
				else
				{
					bool flag5 = result.GetType().ToString().ToUpper().Equals("SYSTEM.STRING") || partialResult.GetType().ToString().ToUpper().Equals("SYSTEM.STRING");
					if (flag5)
					{
						throw new ComputeEngineParserException(string.Concat(new object[]
						{
							"位置：",
							pos,
							" 运算",
							op,
							"参数类型错误！"
						}));
					}
					result = Convert.ToDouble(result) * Convert.ToDouble(partialResult);
				}
			}
		}

		private void EvalExp4(out object result)
		{
			object pramResult = null;
			bool flag = this.tokType == ComputeEngineTokenType.TkFunction;
			if (flag)
			{
				int pos = this.expIdx;
				string FunctionNameA = this.token.ToUpper();
				this.GetToken();
				bool flag2 = this.tokType == ComputeEngineTokenType.TkDelimiter && this.token == "(";
				if (!flag2)
				{
					throw new ComputeEngineParserException(string.Concat(new object[]
					{
						"计算表达式位置：",
						pos,
						" 函数",
						FunctionNameA,
						"语法错误！"
					}));
				}
				ArrayList p = new ArrayList();
				int i = this.expIdx;
				this.GetToken();
				bool flag3 = this.tokType != ComputeEngineTokenType.TkDelimiter || !(this.token == ")");
				if (flag3)
				{
					this.expIdx = i;
					while (true)
					{
						this.GetToken();
						bool flag4 = this.tokType == ComputeEngineTokenType.TkNone && this.token == "";
						if (flag4)
						{
							break;
						}
						p.Add(null);
						bool flag5 = this.tokType == ComputeEngineTokenType.TkDelimiter && (this.token == "," || this.token == ")");
						if (!flag5)
						{
							this.EvalExp0(out pramResult);
							p[p.Count - 1] = pramResult;
						}
						if (this.tokType != ComputeEngineTokenType.TkDelimiter || !(this.token == ","))
						{
							goto IL_165;
						}
					}
					throw new ComputeEngineParserException("计算表达式函数：" + FunctionNameA + "语法错误！");
				}
				IL_165:
				bool flag6 = this.tokType != ComputeEngineTokenType.TkDelimiter || !(this.token == ")");
				if (flag6)
				{
					throw new ComputeEngineParserException("计算表达式函数：" + FunctionNameA + "语法错误！");
				}
				this.GetToken();
				int j = this.InnerFunctions.IndexOfKey(FunctionNameA);
				bool flag7 = j != -1;
				if (flag7)
				{
					this.FunctionName = FunctionNameA;
					this.FunctionIndex = j;
					this.FunctionParameters = p;
					this.EvalInnerFunction(out result);
					this.FunctionParameters.Clear();
				}
				else
				{
					string funcName = null;
					bool flag8 = this.ExternFuncNameList.ContainsKey(FunctionNameA);
					if (flag8)
					{
						funcName = this.ExternFuncNameList[FunctionNameA];
						j = this.ExternFunctions.IndexOfKey(funcName);
					}
					bool flag9 = j != -1;
					if (flag9)
					{
						this.FunctionName = funcName;
						this.FunctionIndex = j;
						this.FunctionParameters = p;
						this.EvalExternFunction(out result);
						this.FunctionParameters.Clear();
					}
					else
					{
						bool flag10 = !this.EvalUnknownFunction(FunctionNameA, p, out result);
						if (flag10)
						{
							throw new ComputeEngineParserException("计算表达式未知函数：" + FunctionNameA + "！");
						}
					}
				}
			}
			else
			{
				this.EvalExp5(out result);
			}
		}

		private void EvalExp5(out object result)
		{
			string op = "";
			bool flag = this.tokType == ComputeEngineTokenType.TkDelimiter && (this.token == "+" || this.token == "-" || this.token == "~");
			if (flag)
			{
				op = this.token;
				this.GetToken();
			}
			this.EvalExp6(out result);
			bool flag2 = op == "-" || op == "~";
			if (flag2)
			{
				result = -1.0 * Convert.ToDouble(result);
			}
		}

		private void EvalExp6(out object result)
		{
			bool flag = this.token == "(";
			if (flag)
			{
				this.GetToken();
				this.EvalExp0(out result);
				bool flag2 = this.token != ")";
				if (flag2)
				{
					throw new ComputeEngineParserException("计算表达式位置：" + this.expIdx + " 缺少右括号！");
				}
				this.GetToken();
			}
			else
			{
				bool flag3 = this.tokType == ComputeEngineTokenType.TkFunction;
				if (flag3)
				{
					this.EvalExp4(out result);
				}
				else
				{
					this.Atom(out result);
				}
			}
		}

		private void Atom(out object result)
		{
			ComputeEngineTokenType computeEngineTokenType = this.tokType;
			if (computeEngineTokenType != ComputeEngineTokenType.TkNumber)
			{
				if (computeEngineTokenType != ComputeEngineTokenType.TkString)
				{
					result = null;
					throw new ComputeEngineParserException("计算表达式语法错误！");
				}
				result = this.token;
				this.GetToken();
			}
			else
			{
				try
				{
					result = double.Parse(this.token);
				}
				catch (FormatException)
				{
					throw new ComputeEngineParserException("计算表达式语法错误！");
				}
				this.GetToken();
			}
		}

		private void EvalInnerFunction(out object result)
		{
			object[] args = new object[]
			{
				this.FunctionParameters
			};
			try
			{
				result = this.mm[(int)this.InnerFunctions.GetByIndex(this.FunctionIndex)].Invoke(this.InnerF, args);
			}
			catch
			{
				throw new ComputeEngineParserException("计算表达式函数(" + this.FunctionName + ")计算错误！");
			}
		}

		private void EvalExternFunction(out object result)
		{
			object[] args = new object[]
			{
				this.FunctionParameters
			};
			try
			{
				result = this.ExternMethod[(int)this.ExternFunctions.GetByIndex(this.FunctionIndex)].Invoke(this.ExternFuncClass, args);
			}
			catch
			{
				throw new ComputeEngineParserException("计算表达式函数(" + this.FunctionName + ")计算错误！");
			}
		}

		private bool EvalUnknownFunction(string functionName, ArrayList paramList, out object result)
		{
			return this.OnUnknownFunction(functionName, paramList, out result);
		}

		private bool EvalUnkownVariable(string variable, out object result)
		{
			return this.OnUnknownVariable(variable, out result);
		}

		private void AddInnerFunctions()
		{
			this.mm = typeof(ComputeEngineInnerFunc).GetMethods();
			for (int i = 0; i < this.mm.Length; i++)
			{
				bool flag = char.IsUpper(this.mm[i].Name, 1);
				if (flag)
				{
					this.InnerFunctions.Add(this.mm[i].Name, i);
				}
			}
		}

		public void ClassRegister(Type FuncClassType)
		{
			bool flag = FuncClassType.IsClass && FuncClassType.IsPublic;
			if (flag)
			{
				this.ExternFuncClass = Activator.CreateInstance(FuncClassType);
				this.ExternFuncType = FuncClassType;
				this.ExternMethod = this.ExternFuncType.GetMethods();
				return;
			}
			throw new ComputeEngineParserException("注册的类型声明不合法！");
		}

		public void ClassRegister(Type FuncClassType, object e)
		{
			bool flag = FuncClassType.IsClass && FuncClassType.IsPublic;
			if (flag)
			{
				this.ExternFuncClass = Activator.CreateInstance(FuncClassType, new object[]
				{
					e
				});
				this.ExternFuncType = FuncClassType;
				this.ExternMethod = this.ExternFuncType.GetMethods();
				return;
			}
			throw new ComputeEngineParserException("注册的类型声明不合法！");
		}

		public void FunctionRegister(string FunctionName)
		{
			bool flag = string.IsNullOrEmpty(FunctionName);
			if (flag)
			{
				throw new ComputeEngineParserException("注册的计算函数名不允许为空！");
			}
			int i = 0;
			while (i < this.ExternMethod.Length)
			{
				bool flag2 = FunctionName.Equals(this.ExternMethod[i].Name);
				if (flag2)
				{
					bool flag3 = this.ContainsFunc(FunctionName);
					if (flag3)
					{
						throw new ComputeEngineParserException("不允许注册相同的函数名！");
					}
					bool flag4 = this.ContainsRegFuncName(FunctionName);
					if (flag4)
					{
						throw new ComputeEngineParserException("计算函数别名已被注册！");
					}
					this.ExternFunctions.Add(FunctionName, i);
					this.ExternFuncNameList.Add(FunctionName.ToUpper(), FunctionName);
					break;
				}
				else
				{
					i++;
				}
			}
		}

		public void FunctionRegister(string RegFuncName, string FunctionName)
		{
			bool flag = string.IsNullOrEmpty(RegFuncName) || string.IsNullOrEmpty(FunctionName);
			if (flag)
			{
				throw new ComputeEngineParserException("注册的计算函数名不允许为空！");
			}
			int i = 0;
			while (i < this.ExternMethod.Length)
			{
				bool flag2 = FunctionName.Equals(this.ExternMethod[i].Name);
				if (flag2)
				{
					bool flag3 = this.ContainsFunc(FunctionName);
					if (flag3)
					{
						throw new ComputeEngineParserException("不允许注册相同的函数名！");
					}
					bool flag4 = this.ContainsRegFuncName(RegFuncName);
					if (flag4)
					{
						throw new ComputeEngineParserException("计算函数别名已被注册！");
					}
					this.ExternFunctions.Add(FunctionName, i);
					this.ExternFuncNameList.Add(RegFuncName.ToUpper(), FunctionName);
					break;
				}
				else
				{
					i++;
				}
			}
		}

		public void FunctionUnRegister(string FunctionName)
		{
			bool flag = this.ExternFunctions.IndexOfKey(FunctionName) != -1;
			if (flag)
			{
				this.ExternFunctions.Remove(FunctionName);
				string sRegFuncName = "";
				foreach (KeyValuePair<string, string> pair in this.ExternFuncNameList)
				{
					bool flag2 = pair.Value.Equals(FunctionName);
					if (flag2)
					{
						sRegFuncName = pair.Key;
						break;
					}
				}
				bool flag3 = string.IsNullOrEmpty(sRegFuncName);
				if (flag3)
				{
					this.ExternFuncNameList.Remove(sRegFuncName);
				}
				return;
			}
			throw new ComputeEngineParserException("注销函数（" + FunctionName + "）失败，不存在此自定义函数！");
		}

		public void ClassRegisterAll(Type FuncClassType)
		{
			bool flag = FuncClassType.IsClass && FuncClassType.IsPublic;
			if (flag)
			{
				this.ExternFuncClass = Activator.CreateInstance(FuncClassType);
				this.ExternFuncType = FuncClassType;
				this.ExternMethod = this.ExternFuncType.GetMethods();
				for (int i = 0; i < this.ExternMethod.Length; i++)
				{
					string sFunc = this.ExternMethod[i].Name;
					bool flag2 = this.ContainsFunc(sFunc);
					if (flag2)
					{
						throw new ComputeEngineParserException("不允许注册相同的函数名！");
					}
					this.ExternFunctions.Add(sFunc, i);
				}
				return;
			}
			throw new ComputeEngineParserException("注册的类型声明不合法！");
		}

		public void ClassRegisterAll(Type FuncClassType, object e)
		{
			bool flag = FuncClassType.IsClass && FuncClassType.IsPublic;
			if (flag)
			{
				this.ExternFuncClass = Activator.CreateInstance(FuncClassType, new object[]
				{
					e
				});
				this.ExternFuncType = FuncClassType;
				this.ExternMethod = this.ExternFuncType.GetMethods();
				for (int i = 0; i < this.ExternMethod.Length; i++)
				{
					string sFunc = this.ExternMethod[i].Name;
					bool flag2 = this.ContainsFunc(sFunc);
					if (flag2)
					{
						throw new ComputeEngineParserException("不允许注册相同的函数名！");
					}
					this.ExternFunctions.Add(sFunc, i);
				}
				return;
			}
			throw new ComputeEngineParserException("注册的类型声明不合法！");
		}

		public void ClassUnRegisterAll(Type FuncClassType)
		{
			this.ExternFuncType = FuncClassType;
			this.ExternMethod = this.ExternFuncType.GetMethods();
			int iCount = this.ExternMethod.Length;
			string[] sRegFuncList = new string[iCount];
			for (int i = 0; i < iCount; i++)
			{
				string sFunc = this.ExternMethod[i].Name;
				this.ExternFunctions.Remove(sFunc);
				foreach (KeyValuePair<string, string> pair in this.ExternFuncNameList)
				{
					bool flag = pair.Value.Equals(sFunc);
					if (flag)
					{
						sRegFuncList[i] = pair.Key;
					}
				}
			}
			for (int j = 0; j < iCount; j++)
			{
				bool flag2 = !string.IsNullOrEmpty(sRegFuncList[j]);
				if (flag2)
				{
					this.ExternFuncNameList.Remove(sRegFuncList[j]);
				}
			}
		}

		private void AddInnerVariables()
		{
			this.InnerVariables.Add("PI", 3.1415926535897931);
		}

		public void VariableRegister(string name, object value)
		{
			string nameUpp = name.ToUpper();
			string var = value.ToString().Trim();
			bool flag = var.Contains("*") || var.Contains("/") || var.Contains("&") || var.Contains("(") || var.Contains(")") || var.Contains(",") || var.Contains("|") || var.Contains("^") || var.Contains("~") || var.Contains("<") || var.Contains(">") || var.Contains("=");
			if (flag)
			{
				throw new ComputeEngineParserException("注册变量失败，变量中不允许含有操作符！");
			}
			bool flag2 = this.ExternVariables.ContainsKey(nameUpp) || this.InnerVariables.ContainsKey(nameUpp);
			if (flag2)
			{
				throw new ComputeEngineParserException("不允许注册相同的变量名！");
			}
			this.ExternVariables.Add(nameUpp, value);
		}

		public void VariableUnRegister(string name)
		{
			string nameUpp = name.ToUpper();
			bool flag = !this.ExternVariables.ContainsKey(nameUpp);
			if (flag)
			{
				throw new ComputeEngineParserException("注销变量（" + name + "）失败，不存在此自定义变量！");
			}
			this.ExternVariables.Remove(nameUpp);
		}

		public bool ContainsFunc(string FuncName)
		{
			int i = -1;
			foreach (DictionaryEntry pair in this.InnerFunctions)
			{
				string func = pair.Key.ToString();
				bool flag = func.ToUpper() == FuncName;
				if (flag)
				{
					i = this.ExternFunctions.IndexOfKey(func);
					break;
				}
			}
			bool flag2 = i != -1;
			bool result;
			if (flag2)
			{
				result = true;
			}
			else
			{
				bool flag3 = this.ExternFuncNameList.ContainsValue(FuncName);
				result = flag3;
			}
			return result;
		}

		public bool ContainsRegFuncName(string RegFuncName)
		{
			int i = -1;
			string sFuncName = RegFuncName.ToUpper();
			foreach (DictionaryEntry pair in this.InnerFunctions)
			{
				string func = pair.Key.ToString();
				bool flag = func.ToUpper().Equals(sFuncName);
				if (flag)
				{
					i = this.ExternFunctions.IndexOfKey(func);
					break;
				}
			}
			bool flag2 = i != -1;
			bool result;
			if (flag2)
			{
				result = true;
			}
			else
			{
				bool flag3 = this.ExternFuncNameList.ContainsKey(sFuncName);
				result = flag3;
			}
			return result;
		}

		public bool ContainsVar(string VarName)
		{
			string sVarUpp = VarName.ToUpper();
			return this.InnerVariables.ContainsKey(sVarUpp) || this.ExternVariables.ContainsKey(sVarUpp);
		}
	}
}
