using System;
using System.Collections;
using System.Text;
using TIM.T_HSQL;

namespace TIM.T_KERNEL.Data
{
	public class HSQL
	{
		public string ClassId = string.Empty;

		public string SqlId = string.Empty;

		internal bool IsCached = false;

		internal int Length = 0;

		public bool Raw = false;

		private Database m_database;

		private bool m_sqlModified;

		internal string m_text;

		private ArrayList m_sql = new ArrayList();

		private HSQLParameters m_params = new HSQLParameters();

		private ArrayList m_paramList = new ArrayList();

		public ArrayList Params = new ArrayList();

		public ArrayList ParamList
		{
			get
			{
				this.ParseSQL();
				return this.m_paramList;
			}
		}

		public bool Modified
		{
			get
			{
				return this.m_sqlModified;
			}
			set
			{
				this.m_sqlModified = value;
			}
		}

		public int Count
		{
			get
			{
				return this.m_sql.Count;
			}
		}

		public string Text
		{
			get
			{
				string result = null;
				foreach (string s in this.m_sql)
				{
					bool flag = result == null;
					if (flag)
					{
						result = s;
					}
					else
					{
						result = result + "\n" + s;
					}
				}
				return result;
			}
			set
			{
				string[] lines = value.Split(new char[]
				{
					'\n'
				});
				this.m_sql.Clear();
				this.m_text = "";
				string[] array = lines;
				for (int i = 0; i < array.Length; i++)
				{
					string s = array[i];
					this.m_sql.Add(s);
					this.m_text += s;
				}
				this.m_sqlModified = true;
			}
		}

		public string this[int index]
		{
			get
			{
				return this.m_sql[index].ToString();
			}
			set
			{
				this.m_sql[index] = value;
				this.m_sqlModified = true;
			}
		}

		public HSQL(Database database)
		{
			this.m_database = database;
			this.m_sqlModified = true;
		}

		private string TransSQL(string sql)
		{
			bool flag = string.IsNullOrEmpty(sql);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string rtn = "";
				try
				{
					rtn = SQLEngine.TransSQL(this.m_database.DriverName, sql);
				}
				catch (Exception ex)
				{
					throw ex;
				}
				result = rtn;
			}
			return result;
		}

		private bool IsNameDelimiter(char ch)
		{
			return ch == ' ' || ch == ',' || ch == ';' || ch == ')' || ch == '\n' || ch == '\r';
		}

		private void ParseSQL()
		{
			bool flag = !this.m_sqlModified;
			if (!flag)
			{
				bool raw = this.Raw;
				string orgSQL;
				if (raw)
				{
					orgSQL = this.Text;
				}
				else
				{
					orgSQL = this.TransSQL(this.Text);
				}
				this.m_text = "";
				this.m_paramList.Clear();
				int i = 0;
				int StartPos = 0;
				int len = orgSQL.Length;
				string dpfName = this.m_database.DbProviderFactory.ToString();
				while (i < len)
				{
					char c = orgSQL[i];
					if (c != '\'')
					{
						if (c != ':')
						{
							i++;
						}
						else
						{
							bool flag2 = dpfName == "System.Data.SqlClient.SqlClientFactory";
							int NameStart;
							if (flag2)
							{
								this.m_text = this.m_text + orgSQL.Substring(StartPos, i - StartPos) + "@";
								i = (NameStart = i + 1);
								while (i < len && !this.IsNameDelimiter(orgSQL[i]))
								{
									i++;
								}
								this.m_text += orgSQL.Substring(NameStart, i - NameStart).Trim().ToUpper();
							}
							else
							{
								bool flag3 = dpfName == "System.Data.OracleClient.OracleClientFactory" || dpfName == "Oracle.DataAccess.Client.OracleClientFactory";
								if (flag3)
								{
									this.m_text = this.m_text + orgSQL.Substring(StartPos, i - StartPos) + ":";
									i = (NameStart = i + 1);
									while (i < len && !this.IsNameDelimiter(orgSQL[i]))
									{
										i++;
									}
									this.m_text += orgSQL.Substring(NameStart, i - NameStart).Trim().ToUpper();
								}
								else
								{
									this.m_text = this.m_text + orgSQL.Substring(StartPos, i - StartPos) + "?";
									i = (NameStart = i + 1);
									while (i < len && !this.IsNameDelimiter(orgSQL[i]))
									{
										i++;
									}
								}
							}
							this.m_paramList.Add(orgSQL.Substring(NameStart, i - NameStart).Trim().ToUpper());
							bool flag4 = !this.Params.Contains(orgSQL.Substring(NameStart, i - NameStart).Trim().ToUpper());
							if (flag4)
							{
								this.Params.Add(orgSQL.Substring(NameStart, i - NameStart).Trim().ToUpper());
							}
							StartPos = i;
						}
					}
					else
					{
						do
						{
							i++;
						}
						while (i < len && orgSQL[i] != '\'');
						i++;
					}
				}
				bool flag5 = StartPos < len;
				if (flag5)
				{
					this.m_text += orgSQL.Substring(StartPos);
				}
				this.m_params.SyncParams(this.m_paramList);
				this.m_sqlModified = false;
			}
		}

		public void AddParamBySourceColumn(string name, TimDbType type, int size, string SourceColumn)
		{
			this.ParseSQL();
			this.ParamByName(name).ParamterType = type;
			this.ParamByName(name).Size = size;
			this.ParamByName(name).SourceColumn = SourceColumn;
		}

		public void AddParam(string name, TimDbType type, int size)
		{
			this.ParseSQL();
			this.ParamByName(name).ParamterType = type;
			this.ParamByName(name).Size = size;
		}

		public void AddParam(string name, TimDbType type, int size, object value)
		{
			this.ParseSQL();
			if (type != TimDbType.Blob)
			{
				this.ParamByName(name).ParamterType = type;
				this.ParamByName(name).Size = size;
				this.ParamByName(name).Value = value;
			}
			else
			{
				this.ParamByName(name).ParamterType = type;
				this.ParamByName(name).Size = size;
				bool flag = value is string;
				if (flag)
				{
					this.ParamByName(name).Value = Encoding.Default.GetBytes((string)value);
				}
				else
				{
					bool flag2 = value is byte[];
					if (flag2)
					{
						this.ParamByName(name).Value = value;
					}
				}
			}
		}

		public override string ToString()
		{
			this.ParseSQL();
			return this.m_text;
		}

		public void Clear()
		{
			this.ClassId = string.Empty;
			this.SqlId = string.Empty;
			bool flag = this.Count == 0 && string.IsNullOrEmpty(this.m_text);
			if (!flag)
			{
				this.ParamList.Clear();
				this.Params.Clear();
				this.m_text = "";
				this.m_sql.Clear();
				this.m_params.Clear();
				this.m_sqlModified = true;
			}
		}

		public void Add(string sqlLine)
		{
			this.m_sql.Add(sqlLine);
			this.m_sqlModified = true;
		}

		public HSQLParameter ParamByName(string name)
		{
			this.ParseSQL();
			return this.m_params[name];
		}
	}
}
