using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace TIM.T_TEMPLET.Reporting
{
	internal class Expressions
	{
		internal delegate object TOnUnknownFunction(string functionName, ArrayList paramList);

		internal delegate object TOnUnknownVariable(string variable);

		private ExpressionFunc _function = null;

		private ExpressionsVar _variable = null;

		private List<string> m_expFunctions = new List<string>();

		private List<string> m_expVariables = new List<string>();

		private string m_expression = string.Empty;

		private object m_value = null;

		private List<TgteToken> _compiledExpression = new List<TgteToken>();

		private int TokenStart;

		private TgteToken LastToken;

		private TgteToken CurToken;

		private TgteToken StackTop;

		private List<string> TempVars;

		private List<string> TempFuncs;

		private List<TgteToken> TempList;

		private List<TgteToken> Trash;

		private Stack<TgteToken> stack;

		private Dictionary<TgteTokenKind, int> TokenPriority = new Dictionary<TgteTokenKind, int>();

		[method: CompilerGenerated]
		////[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal event Expressions.TOnUnknownFunction OnUnknownFunction;

		[method: CompilerGenerated]
		////[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		internal event Expressions.TOnUnknownVariable OnUnknownVariable;

		internal List<string> ExpFunctions
		{
			get
			{
				return this.m_expFunctions;
			}
			set
			{
				this.m_expFunctions = value;
			}
		}

		internal List<string> ExpVariables
		{
			get
			{
				return this.m_expVariables;
			}
			set
			{
				this.m_expVariables = value;
			}
		}

		internal string Expression
		{
			get
			{
				return this.m_expression;
			}
			set
			{
				this.m_expression = value;
				bool flag = this.Compile(value);
				if (flag)
				{
					this.m_expression = value;
				}
				else
				{
					this.m_expression = "";
				}
			}
		}

		internal object Value
		{
			get
			{
				this.m_value = this.VarCompute();
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		internal Expressions()
		{
			this._function = new ExpressionFunc();
			this._variable = new ExpressionsVar();
			this.InitTokenPriority();
		}

		internal void SetExpression(string value)
		{
			bool flag = string.IsNullOrEmpty(value);
			if (!flag)
			{
				bool flag2 = string.IsNullOrEmpty(value.Trim());
				if (!flag2)
				{
					bool flag3 = this.m_expression == value;
					if (!flag3)
					{
						this.m_expression = value;
					}
				}
			}
		}

		internal void Clear()
		{
			this._compiledExpression = new List<TgteToken>();
			this.m_expVariables.Clear();
			this.m_expFunctions.Clear();
		}

		internal bool Compile(string exp)
		{
			bool result = false;
			this.TempVars = new List<string>();
			this.TempFuncs = new List<string>();
			this.TempList = new List<TgteToken>();
			this.Trash = new List<TgteToken>();
			this.stack = new Stack<TgteToken>();
			this.Clear();
			this.TokenStart = 0;
			this.LastToken = null;
			this.StackTop = null;
			bool isEof = false;
			while (!isEof)
			{
				this.CurToken = this.GetToken();
				switch (this.CurToken.TokenKind)
				{
				case TgteTokenKind.etkEOL:
					while (this.stack.Count > 0)
					{
						this.TempList.Add(this.stack.Pop());
					}
					this._compiledExpression = this.TempList;
					this.ExpVariables = this.TempVars;
					this.ExpFunctions = this.TempFuncs;
					this.Trash.Add(this.CurToken);
					isEof = true;
					result = true;
					break;
				case TgteTokenKind.etkNumber:
				case TgteTokenKind.etkString:
				case TgteTokenKind.etkVariable:
					this.TempList.Add(this.CurToken);
					break;
				case TgteTokenKind.etkFunction:
					this.CurToken.OutputPos = this.TempList.Count;
					this.stack.Push(this.CurToken);
					break;
				case TgteTokenKind.etkPlus:
				case TgteTokenKind.etkMinus:
				case TgteTokenKind.etkTimes:
				case TgteTokenKind.etkDivide:
				case TgteTokenKind.etkMOD:
				case TgteTokenKind.etkOR:
				case TgteTokenKind.etkAND:
				case TgteTokenKind.etkXOR:
				case TgteTokenKind.etkNOT:
				case TgteTokenKind.etkShiftLeft:
				case TgteTokenKind.etkShiftRight:
				case TgteTokenKind.etkEqual:
				case TgteTokenKind.etkGreater:
				case TgteTokenKind.etkGreaterEqual:
				case TgteTokenKind.etkLess:
				case TgteTokenKind.etkLessEqual:
				case TgteTokenKind.etkNotEqual:
					while (this.stack.Count > 0)
					{
						this.StackTop = this.stack.Peek();
						bool flag = this.ComparePriority(this.StackTop, this.CurToken) < 0;
						if (flag)
						{
							break;
						}
						this.TempList.Add(this.StackTop);
						this.stack.Pop();
					}
					this.stack.Push(this.CurToken);
					break;
				case TgteTokenKind.etkLeftParenthesis:
					this.CurToken.ParametersStart = this.TokenStart.ToString();
					this.stack.Push(this.CurToken);
					break;
				case TgteTokenKind.etkRightParenthesis:
					while (this.stack.Count > 0)
					{
						this.StackTop = this.stack.Pop();
						bool flag2 = this.StackTop.TokenKind == TgteTokenKind.etkFunction;
						if (flag2)
						{
							bool flag3 = this.TempList.Count != this.StackTop.OutputPos;
							if (flag3)
							{
								this.StackTop.ParametersCount++;
							}
							this.StackTop.EndPos = this.CurToken.StartPos;
							this.TempList.Add(this.StackTop);
							break;
						}
						bool flag4 = this.StackTop.TokenKind == TgteTokenKind.etkLeftParenthesis;
						if (flag4)
						{
							this.Trash.Add(this.StackTop);
							break;
						}
						this.TempList.Add(this.StackTop);
					}
					this.Trash.Add(this.CurToken);
					break;
				case TgteTokenKind.etkComma:
					while (this.stack.Count > 0)
					{
						this.StackTop = this.stack.Peek();
						bool flag5 = this.StackTop.TokenKind == TgteTokenKind.etkFunction;
						if (flag5)
						{
							this.StackTop.ParametersCount++;
							break;
						}
						this.TempList.Add(this.StackTop);
						this.stack.Pop();
					}
					this.Trash.Add(this.CurToken);
					break;
				}
			}
			return result;
		}

		internal int ComparePriority(TgteToken stackTop, TgteToken curToken)
		{
			return this.TokenPriority[stackTop.TokenKind] - this.TokenPriority[curToken.TokenKind];
		}

        internal static uint ComputeStringHash(string s)
        {
            uint num = 0;
            if (s != null)
            {
                num = 0x811c9dc5;
                for (int i = 0; i < s.Length; i++)
                {
                    num = (s[i] ^ num) * 0x1000193;
                }
            }
            return num;
        }

        internal TgteToken GetToken()
		{
			this.SkipSpace();
			int StartPos = this.TokenStart;
			string curChar = null;
			bool flag = this.TokenStart < this.m_expression.Length;
			if (flag)
			{
				curChar = this.m_expression.Substring(this.TokenStart, 1);
			}
			string text = curChar;
			uint num = ComputeStringHash(text);
			TgteToken result;
			if (num <= 3473672221u)
			{
				if (num <= 923577301u)
				{
					if (num <= 755801111u)
					{
						if (num <= 671913016u)
						{
							if (num <= 537692064u)
							{
								if (num != 0u)
								{
									if (num != 537692064u)
									{
										goto IL_1001;
									}
									if (!(text == "%"))
									{
										goto IL_1001;
									}
									result = this.GetKey(TgteTokenKind.etkMOD);
									goto IL_100A;
								}
								else
								{
									if (text != null)
									{
										goto IL_1001;
									}
									result = this.GetKey(TgteTokenKind.etkEOL);
									goto IL_100A;
								}
							}
							else if (num != 571247302u)
							{
								if (num != 588024921u)
								{
									if (num != 671913016u)
									{
										goto IL_1001;
									}
									if (!(text == "-"))
									{
										goto IL_1001;
									}
									bool flag2 = this.LastToken != null && (this.LastToken.TokenKind == TgteTokenKind.etkNumber || this.LastToken.TokenKind == TgteTokenKind.etkString || this.LastToken.TokenKind == TgteTokenKind.etkVariable || this.LastToken.TokenKind == TgteTokenKind.etkRightParenthesis);
									if (flag2)
									{
										result = this.GetKey(TgteTokenKind.etkMinus);
									}
									else
									{
										result = this.GetNumber();
									}
									goto IL_100A;
								}
								else
								{
									if (!(text == "&"))
									{
										goto IL_1001;
									}
									result = this.GetKey(TgteTokenKind.etkAND);
									goto IL_100A;
								}
							}
							else
							{
								if (!(text == "'"))
								{
									goto IL_1001;
								}
								result = this.GetString();
								goto IL_100A;
							}
						}
						else if (num <= 705468254u)
						{
							if (num != 688690635u)
							{
								if (num != 705468254u)
								{
									goto IL_1001;
								}
								if (!(text == "/"))
								{
									goto IL_1001;
								}
								result = this.GetKey(TgteTokenKind.etkDivide);
								goto IL_100A;
							}
							else
							{
								if (!(text == ","))
								{
									goto IL_1001;
								}
								result = this.GetKey(TgteTokenKind.etkComma);
								goto IL_100A;
							}
						}
						else if (num != 722245873u)
						{
							if (num != 739023492u)
							{
								if (num != 755801111u)
								{
									goto IL_1001;
								}
								if (!(text == "("))
								{
									goto IL_1001;
								}
								result = this.GetKey(TgteTokenKind.etkLeftParenthesis);
								goto IL_100A;
							}
							else
							{
								if (!(text == ")"))
								{
									goto IL_1001;
								}
								result = this.GetKey(TgteTokenKind.etkRightParenthesis);
								goto IL_100A;
							}
						}
						else if (!(text == "."))
						{
							goto IL_1001;
						}
					}
					else if (num <= 839689206u)
					{
						if (num <= 789356349u)
						{
							if (num != 772578730u)
							{
								if (num != 789356349u)
								{
									goto IL_1001;
								}
								if (!(text == "*"))
								{
									goto IL_1001;
								}
								result = this.GetKey(TgteTokenKind.etkTimes);
								goto IL_100A;
							}
							else
							{
								if (!(text == "+"))
								{
									goto IL_1001;
								}
								bool flag3 = this.LastToken != null && (this.LastToken.TokenKind == TgteTokenKind.etkNumber || this.LastToken.TokenKind == TgteTokenKind.etkString || this.LastToken.TokenKind == TgteTokenKind.etkVariable || this.LastToken.TokenKind == TgteTokenKind.etkRightParenthesis);
								if (flag3)
								{
									result = this.GetKey(TgteTokenKind.etkPlus);
								}
								else
								{
									result = this.GetNumber();
								}
								goto IL_100A;
							}
						}
						else if (num != 806133968u)
						{
							if (num != 822911587u)
							{
								if (num != 839689206u)
								{
									goto IL_1001;
								}
								if (!(text == "7"))
								{
									goto IL_1001;
								}
							}
							else if (!(text == "4"))
							{
								goto IL_1001;
							}
						}
						else if (!(text == "5"))
						{
							goto IL_1001;
						}
					}
					else if (num <= 873244444u)
					{
						if (num != 856466825u)
						{
							if (num != 873244444u)
							{
								goto IL_1001;
							}
							if (!(text == "1"))
							{
								goto IL_1001;
							}
						}
						else if (!(text == "6"))
						{
							goto IL_1001;
						}
					}
					else if (num != 890022063u)
					{
						if (num != 906799682u)
						{
							if (num != 923577301u)
							{
								goto IL_1001;
							}
							if (!(text == "2"))
							{
								goto IL_1001;
							}
						}
						else if (!(text == "3"))
						{
							goto IL_1001;
						}
					}
					else if (!(text == "0"))
					{
						goto IL_1001;
					}
				}
				else if (num <= 3289118412u)
				{
					if (num <= 1024243015u)
					{
						if (num <= 957132539u)
						{
							if (num != 940354920u)
							{
								if (num != 957132539u)
								{
									goto IL_1001;
								}
								if (!(text == "<"))
								{
									goto IL_1001;
								}
								string tmpChar = null;
								bool flag4 = this.TokenStart + 1 < this.m_expression.Length;
								if (flag4)
								{
									tmpChar = this.m_expression.Substring(this.TokenStart + 1, 1);
								}
								string a = tmpChar;
								if (!(a == "<"))
								{
									if (!(a == "="))
									{
										if (!(a == ">"))
										{
											result = this.GetKey(TgteTokenKind.etkLess);
										}
										else
										{
											result = this.GetKey(TgteTokenKind.etkNotEqual);
											this.TokenStart++;
										}
									}
									else
									{
										result = this.GetKey(TgteTokenKind.etkLessEqual);
										this.TokenStart++;
									}
								}
								else
								{
									result = this.GetKey(TgteTokenKind.etkShiftLeft);
									this.TokenStart++;
								}
								goto IL_100A;
							}
							else
							{
								if (!(text == "="))
								{
									goto IL_1001;
								}
								result = this.GetKey(TgteTokenKind.etkEqual);
								goto IL_100A;
							}
						}
						else if (num != 990687777u)
						{
							if (num != 1007465396u)
							{
								if (num != 1024243015u)
								{
									goto IL_1001;
								}
								if (!(text == "8"))
								{
									goto IL_1001;
								}
							}
							else if (!(text == "9"))
							{
								goto IL_1001;
							}
						}
						else
						{
							if (!(text == ">"))
							{
								goto IL_1001;
							}
							string tmpChar = null;
							bool flag5 = this.TokenStart + 1 < this.m_expression.Length;
							if (flag5)
							{
								tmpChar = this.m_expression.Substring(this.TokenStart + 1, 1);
							}
							string a2 = tmpChar;
							if (!(a2 == ">"))
							{
								if (!(a2 == "="))
								{
									result = this.GetKey(TgteTokenKind.etkGreater);
								}
								else
								{
									result = this.GetKey(TgteTokenKind.etkGreaterEqual);
									this.TokenStart++;
								}
							}
							else
							{
								result = this.GetKey(TgteTokenKind.etkShiftRight);
								this.TokenStart++;
							}
							goto IL_100A;
						}
					}
					else if (num <= 3238785555u)
					{
						if (num != 3222007936u)
						{
							if (num != 3238785555u)
							{
								goto IL_1001;
							}
							if (!(text == "D"))
							{
								goto IL_1001;
							}
							goto IL_FF8;
						}
						else
						{
							if (!(text == "E"))
							{
								goto IL_1001;
							}
							goto IL_FF8;
						}
					}
					else if (num != 3255563174u)
					{
						if (num != 3272340793u)
						{
							if (num != 3289118412u)
							{
								goto IL_1001;
							}
							if (!(text == "A"))
							{
								goto IL_1001;
							}
							goto IL_FF8;
						}
						else
						{
							if (!(text == "F"))
							{
								goto IL_1001;
							}
							goto IL_FF8;
						}
					}
					else
					{
						if (!(text == "G"))
						{
							goto IL_1001;
						}
						goto IL_FF8;
					}
				}
				else if (num <= 3389784126u)
				{
					if (num <= 3339451269u)
					{
						if (num != 3322673650u)
						{
							if (num != 3339451269u)
							{
								goto IL_1001;
							}
							if (!(text == "B"))
							{
								goto IL_1001;
							}
							goto IL_FF8;
						}
						else
						{
							if (!(text == "C"))
							{
								goto IL_1001;
							}
							goto IL_FF8;
						}
					}
					else if (num != 3356228888u)
					{
						if (num != 3373006507u)
						{
							if (num != 3389784126u)
							{
								goto IL_1001;
							}
							if (!(text == "O"))
							{
								goto IL_1001;
							}
							goto IL_FF8;
						}
						else
						{
							if (!(text == "L"))
							{
								goto IL_1001;
							}
							goto IL_FF8;
						}
					}
					else
					{
						if (!(text == "M"))
						{
							goto IL_1001;
						}
						goto IL_FF8;
					}
				}
				else if (num <= 3423339364u)
				{
					if (num != 3406561745u)
					{
						if (num != 3423339364u)
						{
							goto IL_1001;
						}
						if (!(text == "I"))
						{
							goto IL_1001;
						}
						goto IL_FF8;
					}
					else
					{
						if (!(text == "N"))
						{
							goto IL_1001;
						}
						goto IL_FF8;
					}
				}
				else if (num != 3440116983u)
				{
					if (num != 3456894602u)
					{
						if (num != 3473672221u)
						{
							goto IL_1001;
						}
						if (!(text == "J"))
						{
							goto IL_1001;
						}
						goto IL_FF8;
					}
					else
					{
						if (!(text == "K"))
						{
							goto IL_1001;
						}
						goto IL_FF8;
					}
				}
				else
				{
					if (!(text == "H"))
					{
						goto IL_1001;
					}
					goto IL_FF8;
				}
				result = this.GetNumber();
				goto IL_100A;
			}
			if (num <= 3876335077u)
			{
				if (num <= 3675003649u)
				{
					if (num <= 3557560316u)
					{
						if (num <= 3507227459u)
						{
							if (num != 3490449840u)
							{
								if (num != 3507227459u)
								{
									goto IL_1001;
								}
								if (!(text == "T"))
								{
									goto IL_1001;
								}
							}
							else if (!(text == "U"))
							{
								goto IL_1001;
							}
						}
						else if (num != 3524005078u)
						{
							if (num != 3540782697u)
							{
								if (num != 3557560316u)
								{
									goto IL_1001;
								}
								if (!(text == "Q"))
								{
									goto IL_1001;
								}
							}
							else if (!(text == "V"))
							{
								goto IL_1001;
							}
						}
						else if (!(text == "W"))
						{
							goto IL_1001;
						}
					}
					else if (num <= 3591115554u)
					{
						if (num != 3574337935u)
						{
							if (num != 3591115554u)
							{
								goto IL_1001;
							}
							if (!(text == "S"))
							{
								goto IL_1001;
							}
						}
						else if (!(text == "P"))
						{
							goto IL_1001;
						}
					}
					else if (num != 3607893173u)
					{
						if (num != 3658226030u)
						{
							if (num != 3675003649u)
							{
								goto IL_1001;
							}
							if (!(text == "^"))
							{
								goto IL_1001;
							}
							result = this.GetKey(TgteTokenKind.etkXOR);
							goto IL_100A;
						}
						else if (!(text == "_"))
						{
							goto IL_1001;
						}
					}
					else if (!(text == "R"))
					{
						goto IL_1001;
					}
				}
				else if (num <= 3775669363u)
				{
					if (num <= 3708558887u)
					{
						if (num != 3691781268u)
						{
							if (num != 3708558887u)
							{
								goto IL_1001;
							}
							if (!(text == "X"))
							{
								goto IL_1001;
							}
						}
						else if (!(text == "Y"))
						{
							goto IL_1001;
						}
					}
					else if (num != 3742114125u)
					{
						if (num != 3758891744u)
						{
							if (num != 3775669363u)
							{
								goto IL_1001;
							}
							if (!(text == "d"))
							{
								goto IL_1001;
							}
						}
						else if (!(text == "e"))
						{
							goto IL_1001;
						}
					}
					else if (!(text == "Z"))
					{
						goto IL_1001;
					}
				}
				else if (num <= 3809224601u)
				{
					if (num != 3792446982u)
					{
						if (num != 3809224601u)
						{
							goto IL_1001;
						}
						if (!(text == "f"))
						{
							goto IL_1001;
						}
					}
					else if (!(text == "g"))
					{
						goto IL_1001;
					}
				}
				else if (num != 3826002220u)
				{
					if (num != 3859557458u)
					{
						if (num != 3876335077u)
						{
							goto IL_1001;
						}
						if (!(text == "b"))
						{
							goto IL_1001;
						}
					}
					else if (!(text == "c"))
					{
						goto IL_1001;
					}
				}
				else if (!(text == "a"))
				{
					goto IL_1001;
				}
			}
			else if (num <= 4044111267u)
			{
				if (num <= 3960223172u)
				{
					if (num <= 3909890315u)
					{
						if (num != 3893112696u)
						{
							if (num != 3909890315u)
							{
								goto IL_1001;
							}
							if (!(text == "l"))
							{
								goto IL_1001;
							}
						}
						else if (!(text == "m"))
						{
							goto IL_1001;
						}
					}
					else if (num != 3926667934u)
					{
						if (num != 3943445553u)
						{
							if (num != 3960223172u)
							{
								goto IL_1001;
							}
							if (!(text == "i"))
							{
								goto IL_1001;
							}
						}
						else if (!(text == "n"))
						{
							goto IL_1001;
						}
					}
					else if (!(text == "o"))
					{
						goto IL_1001;
					}
				}
				else if (num <= 3993778410u)
				{
					if (num != 3977000791u)
					{
						if (num != 3993778410u)
						{
							goto IL_1001;
						}
						if (!(text == "k"))
						{
							goto IL_1001;
						}
					}
					else if (!(text == "h"))
					{
						goto IL_1001;
					}
				}
				else if (num != 4010556029u)
				{
					if (num != 4027333648u)
					{
						if (num != 4044111267u)
						{
							goto IL_1001;
						}
						if (!(text == "t"))
						{
							goto IL_1001;
						}
					}
					else if (!(text == "u"))
					{
						goto IL_1001;
					}
				}
				else if (!(text == "j"))
				{
					goto IL_1001;
				}
			}
			else if (num <= 4127999362u)
			{
				if (num <= 4077666505u)
				{
					if (num != 4060888886u)
					{
						if (num != 4077666505u)
						{
							goto IL_1001;
						}
						if (!(text == "v"))
						{
							goto IL_1001;
						}
					}
					else if (!(text == "w"))
					{
						goto IL_1001;
					}
				}
				else if (num != 4094444124u)
				{
					if (num != 4111221743u)
					{
						if (num != 4127999362u)
						{
							goto IL_1001;
						}
						if (!(text == "s"))
						{
							goto IL_1001;
						}
					}
					else if (!(text == "p"))
					{
						goto IL_1001;
					}
				}
				else if (!(text == "q"))
				{
					goto IL_1001;
				}
			}
			else if (num <= 4211887457u)
			{
				if (num != 4144776981u)
				{
					if (num != 4178332219u)
					{
						if (num != 4211887457u)
						{
							goto IL_1001;
						}
						if (!(text == "~"))
						{
							goto IL_1001;
						}
						result = this.GetKey(TgteTokenKind.etkNOT);
						goto IL_100A;
					}
					else
					{
						if (!(text == "|"))
						{
							goto IL_1001;
						}
						result = this.GetKey(TgteTokenKind.etkOR);
						goto IL_100A;
					}
				}
				else if (!(text == "r"))
				{
					goto IL_1001;
				}
			}
			else if (num != 4228665076u)
			{
				if (num != 4245442695u)
				{
					if (num != 4278997933u)
					{
						goto IL_1001;
					}
					if (!(text == "z"))
					{
						goto IL_1001;
					}
				}
				else if (!(text == "x"))
				{
					goto IL_1001;
				}
			}
			else if (!(text == "y"))
			{
				goto IL_1001;
			}
			IL_FF8:
			result = this.GetVariable();
			goto IL_100A;
			IL_1001:
			result = this.GetVariable();
			IL_100A:
			bool flag6 = result != null;
			if (flag6)
			{
				result.StartPos = StartPos;
			}
			bool flag7 = result.TokenKind == TgteTokenKind.etkVariable;
			if (flag7)
			{
				bool flag8 = this.TempVars.IndexOf(result.Token.ToString().ToUpper()) == -1;
				if (flag8)
				{
					this.TempVars.Add(result.Token.ToString().ToUpper());
				}
			}
			else
			{
				bool flag9 = result.TokenKind == TgteTokenKind.etkFunction;
				if (flag9)
				{
					bool flag10 = this.TempFuncs.IndexOf(result.Token.ToString().ToUpper()) == -1;
					if (flag10)
					{
						this.TempFuncs.Add(result.Token.ToString().ToUpper());
					}
				}
			}
			this.LastToken = result;
			return result;
		}

		internal TgteToken GetKey(TgteTokenKind kind)
		{
			TgteToken result = new TgteToken();
			result.TokenKind = kind;
			result.Token = null;
			result.ParametersCount = 0;
			this.TokenStart++;
			return result;
		}

		internal TgteToken GetNumber()
		{
			TgteToken result = new TgteToken();
			int TokenEnd = this.TokenStart + 1;
			while (TokenEnd < this.m_expression.Length && "0123456789.eE".IndexOf(this.m_expression.Substring(TokenEnd, 1)) >= 0)
			{
				TokenEnd++;
			}
			bool flag = this.m_expression.Substring(TokenEnd - 1, 1).ToUpper() == "E" && (this.m_expression.Substring(TokenEnd, 1) == "+" || this.m_expression.Substring(TokenEnd, 1) == "-");
			if (flag)
			{
				TokenEnd++;
				while (TokenEnd < this.m_expression.Length && "0123456789".IndexOf(this.m_expression.Substring(TokenEnd, 1)) >= 0)
				{
					TokenEnd++;
				}
			}
			string tmpChar = this.m_expression.Substring(this.TokenStart, TokenEnd - this.TokenStart);
			double Number = Utils.Str2Double(tmpChar, 0.0);
			result.TokenKind = TgteTokenKind.etkNumber;
			result.Token = Number;
			result.ParametersCount = 0;
			this.TokenStart = TokenEnd;
			return result;
		}

		internal TgteToken GetString()
		{
			TgteToken result = new TgteToken();
			int TokenEnd = this.TokenStart + 1;
			while (true)
			{
				while (TokenEnd < this.m_expression.Length && this.m_expression.Substring(TokenEnd, 1) != "'")
				{
					TokenEnd++;
				}
				bool flag = TokenEnd + 1 < this.m_expression.Length && this.m_expression.Substring(TokenEnd + 1, 1).ToUpper() == "'";
				if (!flag)
				{
					break;
				}
				TokenEnd += 2;
			}
			string tmpChar = this.m_expression.Substring(this.TokenStart + 1, TokenEnd - this.TokenStart - 1);
			result.TokenKind = TgteTokenKind.etkString;
			result.Token = tmpChar;
			result.ParametersCount = 0;
			this.TokenStart = TokenEnd + 1;
			return result;
		}

		internal TgteToken GetVariable()
		{
			TgteToken result = new TgteToken();
			int TokenEnd = this.TokenStart;
			ASCIIEncoding asciiEncoding = new ASCIIEncoding();
			while (TokenEnd < this.m_expression.Length && ("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_:.".IndexOf(this.m_expression.Substring(TokenEnd, 1)) >= 0 || asciiEncoding.GetBytes(this.m_expression.Substring(TokenEnd, 1))[0] == 63 || (128 <= asciiEncoding.GetBytes(this.m_expression.Substring(TokenEnd, 1))[0] && asciiEncoding.GetBytes(this.m_expression.Substring(TokenEnd, 1))[0] >= 255)))
			{
				TokenEnd++;
			}
			int tmpTokenEnd = TokenEnd;
			while (tmpTokenEnd < this.m_expression.Length && this.m_expression.Substring(tmpTokenEnd, 1) == " ")
			{
				tmpTokenEnd++;
			}
			bool flag = tmpTokenEnd < this.m_expression.Length && this.m_expression.Substring(tmpTokenEnd, 1) == "(";
			if (flag)
			{
				result.TokenKind = TgteTokenKind.etkFunction;
				tmpTokenEnd++;
			}
			else
			{
				result.TokenKind = TgteTokenKind.etkVariable;
			}
			string tmpChar = this.m_expression.Substring(this.TokenStart, TokenEnd - this.TokenStart);
			result.Token = tmpChar.ToUpper();
			result.ParametersCount = 0;
			this.TokenStart = tmpTokenEnd;
			return result;
		}

		internal void SkipSpace()
		{
			while (this.TokenStart < this.m_expression.Length && this.m_expression.Substring(this.TokenStart, 1) == " ")
			{
				this.TokenStart++;
			}
		}

		internal object DirectCompute(string OP, ArrayList value)
		{
			object result = null;
			FunctionTriple func = this._function.Find(OP);
			bool flag = func != null;
			if (flag)
			{
				result = this._function.Run(OP, value);
			}
			return result;
		}

		internal object VarCompute()
		{
			this.stack = new Stack<TgteToken>();
			this.Trash = new List<TgteToken>();
			int i = 0;
			while (i < this._compiledExpression.Count)
			{
				this.CurToken = this._compiledExpression[i];
				switch (this.CurToken.TokenKind)
				{
				case TgteTokenKind.etkNumber:
				case TgteTokenKind.etkString:
				case TgteTokenKind.etkVariable:
					this.stack.Push(this.CurToken);
					break;
				case TgteTokenKind.etkFunction:
					this.ComputeFunction();
					break;
				case TgteTokenKind.etkPlus:
					this.ComputePlus();
					break;
				case TgteTokenKind.etkMinus:
					this.ComputeMinus();
					break;
				case TgteTokenKind.etkTimes:
					this.ComputeTimes();
					break;
				case TgteTokenKind.etkDivide:
					this.ComputeDivide();
					break;
				case TgteTokenKind.etkMOD:
					this.ComputeMOD();
					break;
				case TgteTokenKind.etkOR:
					this.ComputeOR();
					break;
				case TgteTokenKind.etkAND:
					this.ComputeAND();
					break;
				case TgteTokenKind.etkXOR:
					this.ComputeXOR();
					break;
				case TgteTokenKind.etkNOT:
					this.ComputeNOT();
					break;
				case TgteTokenKind.etkShiftLeft:
					this.ComputeShiftLeft();
					break;
				case TgteTokenKind.etkShiftRight:
					this.ComputeShiftRight();
					break;
				case TgteTokenKind.etkEqual:
					this.ComputeEqual();
					break;
				case TgteTokenKind.etkGreater:
					this.ComputeGreater();
					break;
				case TgteTokenKind.etkGreaterEqual:
					this.ComputeGreaterEqual();
					break;
				case TgteTokenKind.etkLess:
					this.ComputeLess();
					break;
				case TgteTokenKind.etkLessEqual:
					this.ComputeLessEqual();
					break;
				case TgteTokenKind.etkNotEqual:
					this.ComputeNotEqual();
					break;
				}
				IL_173:
				i++;
				continue;
				goto IL_173;
			}
			bool flag = this.stack.Count != 1;
			if (flag)
			{
				throw new Exception("计算异常");
			}
			return this.PopVariant();
		}

		internal object PopVariant()
		{
			object result = null;
			TgteToken token = this.stack.Pop();
			switch (token.TokenKind)
			{
			case TgteTokenKind.etkNumber:
			case TgteTokenKind.etkString:
				result = token.Token;
				break;
			case TgteTokenKind.etkVariable:
			{
				bool flag = this._variable.Find(token.Token.ToString()) == null;
				if (flag)
				{
					result = this.OnUnknownVariable(token.Token.ToString());
				}
				else
				{
					result = this._variable.Find(token.Token.ToString());
				}
				break;
			}
			}
			return result;
		}

		internal double PopNumber()
		{
			object result = this.PopVariant();
			bool flag = result == null || result.ToString() == "" || result.ToString() == "ERROR";
			double result2;
			if (flag)
			{
				result2 = 0.0;
			}
			else
			{
				double tmp = Utils.Str2Double(result.ToString(), 0.0);
				result2 = tmp;
			}
			return result2;
		}

		internal string PopString()
		{
			object result = this.PopVariant();
			bool flag = result != null;
			string result2;
			if (flag)
			{
				result2 = result.ToString();
			}
			else
			{
				result2 = "";
			}
			return result2;
		}

		internal void PushNumber(double num)
		{
			TgteToken node = new TgteToken();
			node.TokenKind = TgteTokenKind.etkNumber;
			node.Token = num;
			this.stack.Push(node);
			this.Trash.Add(node);
		}

		internal void PushString(string str)
		{
			TgteToken node = new TgteToken();
			node.TokenKind = TgteTokenKind.etkString;
			node.Token = str;
			this.stack.Push(node);
			this.Trash.Add(node);
		}

		internal void ComputePlus()
		{
			object v2 = this.PopVariant();
			object v3 = this.PopVariant();
			bool flag = v3.GetType() == v2.GetType();
			if (flag)
			{
				bool flag2 = v3 is double;
				if (flag2)
				{
					this.PushNumber((double)v3 + (double)v2);
				}
				else
				{
					this.PushString((string)v3 + (string)v2);
				}
			}
			else
			{
				bool flag3 = v3 is double && (v2.ToString().Trim() == "" || v2.ToString().Trim() == "ERROR");
				if (flag3)
				{
					this.PushNumber((double)v3);
				}
				else
				{
					bool flag4 = v2 is double && (v3.ToString().Trim() == "" || v3.ToString().Trim() == "ERROR");
					if (flag4)
					{
						this.PushNumber((double)v2);
					}
					else
					{
						this.PushString(v3.ToString() + v2.ToString());
					}
				}
			}
		}

		internal void ComputeMinus()
		{
			double v2 = this.PopNumber();
			double v3 = this.PopNumber();
			this.PushNumber(v3 - v2);
		}

		internal void ComputeTimes()
		{
			double v2 = this.PopNumber();
			double v3 = this.PopNumber();
			this.PushNumber(v3 * v2);
		}

		internal void ComputeDivide()
		{
			double v2 = this.PopNumber();
			double v3 = this.PopNumber();
			bool flag = Math.Abs(v2) >= 1E-10;
			if (flag)
			{
				this.PushNumber(v3 / v2);
			}
			else
			{
				this.PushNumber(0.0);
			}
		}

		internal void ComputeMOD()
		{
			int v2 = (int)Math.Round(this.PopNumber());
			int v3 = (int)Math.Round(this.PopNumber());
			this.PushNumber((double)(v3 % v2));
		}

		internal void ComputeOR()
		{
			int v2 = (int)Math.Round(this.PopNumber());
			int v3 = (int)Math.Round(this.PopNumber());
			this.PushNumber((double)(v3 | v2));
		}

		internal void ComputeAND()
		{
			int v2 = (int)Math.Round(this.PopNumber());
			int v3 = (int)Math.Round(this.PopNumber());
			this.PushNumber((double)(v3 & v2));
		}

		internal void ComputeXOR()
		{
			int v2 = (int)Math.Round(this.PopNumber());
			int v3 = (int)Math.Round(this.PopNumber());
			this.PushNumber((double)(v3 ^ v2));
		}

		internal void ComputeNOT()
		{
			int v = (int)Math.Round(this.PopNumber());
			this.PushNumber((v == 1) ? 0.0 : 1.0);
		}

		internal object ResetVarToZero(object obj)
		{
			bool flag = obj == null || (obj is string && obj.ToString() == "");
			object result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				result = obj;
			}
			return result;
		}

		internal void ComputeEqual()
		{
			object v2 = this.ResetVarToZero(this.PopVariant());
			object v3 = this.ResetVarToZero(this.PopVariant());
			bool flag = v3 is double && v2 is double;
			if (flag)
			{
				this.PushNumber(((double)v3 == (double)v2) ? 1.0 : 0.0);
			}
			else
			{
				this.PushNumber((v3.ToString() == v2.ToString()) ? 1.0 : 0.0);
			}
		}

		internal void ComputeNotEqual()
		{
			object v2 = this.ResetVarToZero(this.PopVariant());
			object v3 = this.ResetVarToZero(this.PopVariant());
			bool flag = v3 is double && v2 is double;
			if (flag)
			{
				this.PushNumber(((double)v3 == (double)v2) ? 0.0 : 1.0);
			}
			else
			{
				this.PushNumber((v3.ToString() == v2.ToString()) ? 0.0 : 1.0);
			}
		}

		internal void ComputeGreater()
		{
			object v2 = this.ResetVarToZero(this.PopVariant());
			object v3 = this.ResetVarToZero(this.PopVariant());
			bool flag = v3 is double && v2 is double;
			if (flag)
			{
				this.PushNumber(((double)v3 > (double)v2) ? 1.0 : 0.0);
			}
			else
			{
				this.PushNumber((string.Compare(v3.ToString(), v2.ToString()) > 0) ? 1.0 : 0.0);
			}
		}

		internal void ComputeGreaterEqual()
		{
			object v2 = this.ResetVarToZero(this.PopVariant());
			object v3 = this.ResetVarToZero(this.PopVariant());
			bool flag = v3 is double && v2 is double;
			if (flag)
			{
				this.PushNumber(((double)v3 >= (double)v2) ? 1.0 : 0.0);
			}
			else
			{
				this.PushNumber((string.Compare(v3.ToString(), v2.ToString()) >= 0) ? 1.0 : 0.0);
			}
		}

		internal void ComputeLess()
		{
			object v2 = this.ResetVarToZero(this.PopVariant());
			object v3 = this.ResetVarToZero(this.PopVariant());
			bool flag = v3 is double && v2 is double;
			if (flag)
			{
				this.PushNumber(((double)v3 < (double)v2) ? 1.0 : 0.0);
			}
			else
			{
				this.PushNumber((string.Compare(v3.ToString(), v2.ToString()) < 0) ? 1.0 : 0.0);
			}
		}

		internal void ComputeLessEqual()
		{
			object v2 = this.ResetVarToZero(this.PopVariant());
			object v3 = this.ResetVarToZero(this.PopVariant());
			bool flag = v3 is double && v2 is double;
			if (flag)
			{
				this.PushNumber(((double)v3 <= (double)v2) ? 1.0 : 0.0);
			}
			else
			{
				this.PushNumber((string.Compare(v3.ToString(), v2.ToString()) <= 0) ? 1.0 : 0.0);
			}
		}

		internal void ComputeShiftLeft()
		{
			int v2 = (int)Math.Round(this.PopNumber());
			int v3 = (int)Math.Round(this.PopNumber());
			this.PushNumber((double)(v3 << v2));
		}

		internal void ComputeShiftRight()
		{
			int v2 = (int)Math.Round(this.PopNumber());
			int v3 = (int)Math.Round(this.PopNumber());
			this.PushNumber((double)(v3 >> v2));
		}

		internal void ComputeFunction()
		{
			ArrayList funcParams = new ArrayList();
			ArrayList tmpParams = new ArrayList();
			try
			{
				for (int i = 0; i < this.CurToken.ParametersCount; i++)
				{
					tmpParams.Add(this.PopVariant());
				}
				for (int j = tmpParams.Count - 1; j >= 0; j--)
				{
					funcParams.Add(tmpParams[j]);
				}
				FunctionTriple func = this._function.Find(this.CurToken.Token.ToString());
				bool flag = func != null;
				object result;
				if (flag)
				{
					result = this._function.Run(this.CurToken.Token.ToString(), funcParams);
				}
				else
				{
					result = this.OnUnknownFunction(this.CurToken.Token.ToString(), funcParams);
				}
				bool flag2 = result is double;
				if (flag2)
				{
					this.PushNumber((double)result);
				}
				else
				{
					this.PushString(result.ToString());
				}
			}
			catch
			{
				this.PushString("ERROR:" + this.CurToken.Token.ToString() + "函数计算异常！");
			}
		}

		private void InitTokenPriority()
		{
			this.TokenPriority.Add(TgteTokenKind.etkEOL, -1);
			this.TokenPriority.Add(TgteTokenKind.etkNumber, -1);
			this.TokenPriority.Add(TgteTokenKind.etkString, -1);
			this.TokenPriority.Add(TgteTokenKind.etkVariable, -1);
			this.TokenPriority.Add(TgteTokenKind.etkFunction, -1);
			this.TokenPriority.Add(TgteTokenKind.etkPlus, 6);
			this.TokenPriority.Add(TgteTokenKind.etkMinus, 6);
			this.TokenPriority.Add(TgteTokenKind.etkTimes, 7);
			this.TokenPriority.Add(TgteTokenKind.etkDivide, 7);
			this.TokenPriority.Add(TgteTokenKind.etkMOD, 7);
			this.TokenPriority.Add(TgteTokenKind.etkOR, 1);
			this.TokenPriority.Add(TgteTokenKind.etkAND, 3);
			this.TokenPriority.Add(TgteTokenKind.etkXOR, 2);
			this.TokenPriority.Add(TgteTokenKind.etkNOT, 8);
			this.TokenPriority.Add(TgteTokenKind.etkLeftParenthesis, -1);
			this.TokenPriority.Add(TgteTokenKind.etkRightParenthesis, -1);
			this.TokenPriority.Add(TgteTokenKind.etkShiftLeft, 5);
			this.TokenPriority.Add(TgteTokenKind.etkShiftRight, 5);
			this.TokenPriority.Add(TgteTokenKind.etkComma, -1);
			this.TokenPriority.Add(TgteTokenKind.etkEqual, 4);
			this.TokenPriority.Add(TgteTokenKind.etkGreater, 4);
			this.TokenPriority.Add(TgteTokenKind.etkGreaterEqual, 4);
			this.TokenPriority.Add(TgteTokenKind.etkLess, 4);
			this.TokenPriority.Add(TgteTokenKind.etkLessEqual, 4);
			this.TokenPriority.Add(TgteTokenKind.etkNotEqual, 4);
		}
	}
}
