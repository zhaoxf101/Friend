using System;
using System.Collections.Generic;
using System.Data;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RmDataSet
	{
		private RmDataSets m_fDataSets = null;

		private RdDataSet m_fgtrDataSet = null;

		private DataTable m_fDataSet = null;

		private int m_recNo = -1;

		private bool m_eof = false;

		private RmDataSet m_root = null;

		private RmDataSet m_master = null;

		private Dictionary<int, RdRow> m_columnHeader = null;

		private Dictionary<int, RdRow> m_columnFooter = null;

		internal RmDataSets FDataSets
		{
			get
			{
				return this.m_fDataSets;
			}
			set
			{
				this.m_fDataSets = value;
			}
		}

		internal RdDataSet FgtrDataSet
		{
			get
			{
				return this.m_fgtrDataSet;
			}
			set
			{
				this.m_fgtrDataSet = value;
			}
		}

		internal DataTable FDataSet
		{
			get
			{
				return this.m_fDataSet;
			}
			set
			{
				this.m_fDataSet = value;
			}
		}

		internal int RecNo
		{
			get
			{
				return this.m_recNo;
			}
			set
			{
				this.m_recNo = value;
			}
		}

		internal bool Eof
		{
			get
			{
				return this.m_eof;
			}
			set
			{
				this.m_eof = value;
			}
		}

		internal RmDataSet Root
		{
			get
			{
				return this.m_root;
			}
			set
			{
				this.m_root = value;
			}
		}

		internal RmDataSet Master
		{
			get
			{
				return this.m_master;
			}
			set
			{
				this.m_master = value;
			}
		}

		internal Dictionary<int, RdRow> ColumnHeader
		{
			get
			{
				return this.m_columnHeader;
			}
			set
			{
				this.m_columnHeader = value;
			}
		}

		internal Dictionary<int, RdRow> ColumnFooter
		{
			get
			{
				return this.m_columnFooter;
			}
			set
			{
				this.m_columnFooter = value;
			}
		}

		internal bool IsMasterOf(RmDataSet aDataset)
		{
			bool flag = aDataset == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = aDataset != this;
				if (flag2)
				{
					RmDataSet pDataset = aDataset.Master;
					while (pDataset != null && pDataset != this)
					{
						pDataset = pDataset.Master;
					}
					bool flag3 = pDataset == null;
					if (flag3)
					{
						result = false;
						return result;
					}
				}
				result = true;
			}
			return result;
		}

		internal RmDataSet(RmDataSets ADatasets)
		{
			this.m_fDataSets = ADatasets;
			this.m_columnHeader = new Dictionary<int, RdRow>();
			this.m_columnFooter = new Dictionary<int, RdRow>();
		}

		internal void First()
		{
			this.m_recNo = 1;
			this.m_eof = false;
			RmReportingMaker builder = this.m_fDataSets.Builder;
			bool flag = builder != null;
			if (flag)
			{
				builder.OnDataSetFirst(this.m_fgtrDataSet.Name, "");
			}
		}

		internal void Next()
		{
			RmReportingMaker builder = this.m_fDataSets.Builder;
			bool flag = builder != null;
			if (flag)
			{
				builder.OnDataSetNext(this.m_fgtrDataSet.Name, "");
			}
			this.m_recNo++;
		}

		internal bool IsEOF()
		{
			RmReportingMaker builder = this.m_fDataSets.Builder;
			bool flag = builder != null;
			if (flag)
			{
				string tmpEof = "N";
				builder.OnDataSetEOF(this.m_fgtrDataSet.Name, ref tmpEof);
				this.Eof = Utils.Str2Bool(tmpEof);
			}
			bool flag2 = this.FgtrDataSet.OutputLines > 0;
			bool result;
			if (flag2)
			{
				result = (this.RecNo > this.FgtrDataSet.OutputLines);
			}
			else
			{
				bool eof = this.Eof;
				if (eof)
				{
					result = (!this.FgtrDataSet.FillWithBlank || this.FgtrDataSet.ForceNewPage == 0 || (this.RecNo - 1) % this.FgtrDataSet.ForceNewPage == 0);
				}
				else
				{
					result = this.Eof;
				}
			}
			return result;
		}

		internal object Read(string fieldName)
		{
			string result = string.Empty;
			string valueType = "";
			bool flag = fieldName == "RECNO";
			if (flag)
			{
				valueType = "N";
				result = this.RecNo.ToString();
			}
			else
			{
				bool flag2 = this.Eof || (this.FgtrDataSet.OutputLines > 0 && this.RecNo > this.FgtrDataSet.OutputLines);
				if (flag2)
				{
					result = "";
					valueType = "S";
				}
				else
				{
					RmReportingMaker builder = this.m_fDataSets.Builder;
					bool flag3 = builder != null;
					if (flag3)
					{
						builder.OnDataSetRead(this.m_fgtrDataSet.Name, fieldName, ref valueType, ref result);
					}
				}
			}
			bool flag4 = valueType == "N";
			object ret;
			if (flag4)
			{
				ret = Utils.Str2Double(result, 0.0);
			}
			else
			{
				ret = result;
			}
			return ret;
		}
	}
}
