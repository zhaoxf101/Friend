using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace TIM.T_TEMPLET.Reporting
{
	internal class HtmlToText
	{
		protected class TextBuilder
		{
			private StringBuilder _text;

			private StringBuilder _currLine;

			private int _emptyLines;

			private bool _preformatted;

			public bool Preformatted
			{
				get
				{
					return this._preformatted;
				}
				set
				{
					if (value)
					{
						bool flag = this._currLine.Length > 0;
						if (flag)
						{
							this.FlushCurrLine();
						}
						this._emptyLines = 0;
					}
					this._preformatted = value;
				}
			}

			public TextBuilder()
			{
				this._text = new StringBuilder();
				this._currLine = new StringBuilder();
				this._emptyLines = 0;
				this._preformatted = false;
			}

			public void Clear()
			{
				this._text.Length = 0;
				this._currLine.Length = 0;
				this._emptyLines = 0;
			}

			public void Write(string s)
			{
				for (int i = 0; i < s.Length; i++)
				{
					char c = s[i];
					this.Write(c);
				}
			}

			public void Write(char c)
			{
				bool preformatted = this._preformatted;
				if (preformatted)
				{
					this._text.Append(c);
				}
				else
				{
					bool flag = c == '\r';
					if (!flag)
					{
						bool flag2 = c == '\n';
						if (flag2)
						{
							this.FlushCurrLine();
						}
						else
						{
							bool flag3 = char.IsWhiteSpace(c);
							if (flag3)
							{
								int len = this._currLine.Length;
								bool flag4 = len == 0 || !char.IsWhiteSpace(this._currLine[len - 1]);
								if (flag4)
								{
									this._currLine.Append(' ');
								}
							}
							else
							{
								this._currLine.Append(c);
							}
						}
					}
				}
			}

			protected void FlushCurrLine()
			{
				string line = this._currLine.ToString().Trim();
				this._text.AppendLine(line.Replace("&nbsp;", " "));
				this._currLine.Length = 0;
			}

			public override string ToString()
			{
				bool flag = this._currLine.Length > 0;
				if (flag)
				{
					this.FlushCurrLine();
				}
				return this._text.ToString();
			}
		}

		protected static Dictionary<string, string> _tags;

		protected static List<string> _ignoreTags;

		protected HtmlToText.TextBuilder _text;

		protected string _html;

		protected int _pos;

		protected bool EndOfText
		{
			get
			{
				return this._pos >= this._html.Length;
			}
		}

		static HtmlToText()
		{
			HtmlToText._tags = new Dictionary<string, string>();
			HtmlToText._tags.Add("address", "\n");
			HtmlToText._tags.Add("blockquote", "\n");
			HtmlToText._tags.Add("div", "\n");
			HtmlToText._tags.Add("dl", "\n");
			HtmlToText._tags.Add("fieldset", "\n");
			HtmlToText._tags.Add("form", "\n");
			HtmlToText._tags.Add("h1", "\n");
			HtmlToText._tags.Add("/h1", "\n");
			HtmlToText._tags.Add("h2", "\n");
			HtmlToText._tags.Add("/h2", "\n");
			HtmlToText._tags.Add("h3", "\n");
			HtmlToText._tags.Add("/h3", "\n");
			HtmlToText._tags.Add("h4", "\n");
			HtmlToText._tags.Add("/h4", "\n");
			HtmlToText._tags.Add("h5", "\n");
			HtmlToText._tags.Add("/h5", "\n");
			HtmlToText._tags.Add("h6", "\n");
			HtmlToText._tags.Add("/h6", "\n");
			HtmlToText._tags.Add("p", "\n");
			HtmlToText._tags.Add("/p", "\n");
			HtmlToText._tags.Add("table", "\n");
			HtmlToText._tags.Add("/table", "\n");
			HtmlToText._tags.Add("ul", "\n");
			HtmlToText._tags.Add("/ul", "\n");
			HtmlToText._tags.Add("ol", "\n");
			HtmlToText._tags.Add("/ol", "\n");
			HtmlToText._tags.Add("/li", "\n");
			HtmlToText._tags.Add("br", "\n");
			HtmlToText._tags.Add("/td", "\t");
			HtmlToText._tags.Add("/tr", "\n");
			HtmlToText._tags.Add("/pre", "\n");
			HtmlToText._ignoreTags = new List<string>();
		}

		public string Convert(string html)
		{
			this._text = new HtmlToText.TextBuilder();
			this._html = html;
			this._pos = 0;
			while (!this.EndOfText)
			{
				bool flag = this.Peek() == '<';
				if (flag)
				{
					bool selfClosing;
					string tag = this.ParseTag(out selfClosing);
					bool flag2 = tag == "body";
					if (flag2)
					{
						this._text.Clear();
					}
					else
					{
						bool flag3 = tag == "/body";
						if (flag3)
						{
							this._pos = this._html.Length;
						}
						else
						{
							bool flag4 = tag == "pre";
							if (flag4)
							{
								this._text.Preformatted = true;
								this.EatWhitespaceToNextLine();
							}
							else
							{
								bool flag5 = tag == "/pre";
								if (flag5)
								{
									this._text.Preformatted = false;
								}
							}
						}
					}
					string value;
					bool flag6 = HtmlToText._tags.TryGetValue(tag, out value);
					if (flag6)
					{
						this._text.Write(value);
					}
					bool flag7 = HtmlToText._ignoreTags.Contains(tag);
					if (flag7)
					{
						this.EatInnerContent(tag);
					}
				}
				else
				{
					bool flag8 = char.IsWhiteSpace(this.Peek());
					if (flag8)
					{
						this._text.Write(this._text.Preformatted ? this.Peek() : ' ');
						this.MoveAhead();
					}
					else
					{
						this._text.Write(this.Peek());
						this.MoveAhead();
					}
				}
			}
			return HttpUtility.HtmlDecode(this._text.ToString());
		}

		protected string ParseTag(out bool selfClosing)
		{
			string tag = string.Empty;
			selfClosing = false;
			bool flag = this.Peek() == '<';
			if (flag)
			{
				this.MoveAhead();
				this.EatWhitespace();
				int start = this._pos;
				bool flag2 = this.Peek() == '/';
				if (flag2)
				{
					this.MoveAhead();
				}
				while (!this.EndOfText && !char.IsWhiteSpace(this.Peek()) && this.Peek() != '/' && this.Peek() != '>')
				{
					this.MoveAhead();
				}
				tag = this._html.Substring(start, this._pos - start).ToLower();
				while (!this.EndOfText && this.Peek() != '>')
				{
					bool flag3 = this.Peek() == '"' || this.Peek() == '\'';
					if (flag3)
					{
						this.EatQuotedValue();
					}
					else
					{
						bool flag4 = this.Peek() == '/';
						if (flag4)
						{
							selfClosing = true;
						}
						this.MoveAhead();
					}
				}
				this.MoveAhead();
			}
			return tag;
		}

		protected void EatInnerContent(string tag)
		{
			string endTag = "/" + tag;
			while (!this.EndOfText)
			{
				bool flag = this.Peek() == '<';
				if (flag)
				{
					bool selfClosing;
					bool flag2 = this.ParseTag(out selfClosing) == endTag;
					if (flag2)
					{
						break;
					}
					bool flag3 = !selfClosing && !tag.StartsWith("/");
					if (flag3)
					{
						this.EatInnerContent(tag);
					}
				}
				else
				{
					this.MoveAhead();
				}
			}
		}

		protected char Peek()
		{
			return (this._pos < this._html.Length) ? this._html[this._pos] : '\0';
		}

		protected void MoveAhead()
		{
			this._pos = Math.Min(this._pos + 1, this._html.Length);
		}

		protected void EatWhitespace()
		{
			while (char.IsWhiteSpace(this.Peek()))
			{
				this.MoveAhead();
			}
		}

		protected void EatWhitespaceToNextLine()
		{
			while (char.IsWhiteSpace(this.Peek()))
			{
				char c = this.Peek();
				this.MoveAhead();
				bool flag = c == '\n';
				if (flag)
				{
					break;
				}
			}
		}

		protected void EatQuotedValue()
		{
			char c = this.Peek();
			bool flag = c == '"' || c == '\'';
			if (flag)
			{
				this.MoveAhead();
				int start = this._pos;
				this._pos = this._html.IndexOfAny(new char[]
				{
					c,
					'\r',
					'\n'
				}, this._pos);
				bool flag2 = this._pos < 0;
				if (flag2)
				{
					this._pos = this._html.Length;
				}
				else
				{
					this.MoveAhead();
				}
			}
		}
	}
}
