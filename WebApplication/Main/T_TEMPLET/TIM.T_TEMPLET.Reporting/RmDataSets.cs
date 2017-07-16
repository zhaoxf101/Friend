using System;
using System.Collections.Generic;

namespace TIM.T_TEMPLET.Reporting
{
	internal class RmDataSets : RmSetNode
	{
		private RdDocument _rep = null;

		private Dictionary<string, RmDataSet> m_items = null;

		internal Dictionary<string, RmDataSet> Items
		{
			get
			{
				return this.m_items;
			}
			set
			{
				this.m_items = value;
			}
		}

		internal RmDataSets(RmReportingMaker builder) : base(builder)
		{
			this.m_items = new Dictionary<string, RmDataSet>();
		}

		internal void Clear()
		{
			this.Items.Clear();
		}

		internal RmDataSet FindDataset(string datasetName)
		{
			bool flag = this.Items.ContainsKey(datasetName);
			RmDataSet result;
			if (flag)
			{
				result = this.Items[datasetName];
			}
			else
			{
				result = null;
			}
			return result;
		}

		internal void Refresh()
		{
			this._rep = base.Builder.Template;
			this.Clear();
			this.GetDatasetsFromTemplate();
			this.RefreshRelations();
			this.RefreshRootdataset();
			this.RefreshRowDataset();
		}

		internal void GetDatasetsFromTemplate()
		{
			foreach (KeyValuePair<string, RdDataSet> obj in this._rep.DataSets.List)
			{
				RmDataSet _gtrbDataset = new RmDataSet(this);
				bool flag = obj.Value == null || string.IsNullOrEmpty(obj.Value.Sql);
				if (flag)
				{
					_gtrbDataset.FDataSet = null;
				}
				else
				{
					base.Builder.OnGetDataSet(obj.Value.Name, obj.Value.Sql);
				}
				_gtrbDataset.RecNo = 1;
				_gtrbDataset.FgtrDataSet = obj.Value;
				_gtrbDataset.Eof = false;
				this.Items.Add(obj.Value.Name, _gtrbDataset);
			}
		}

		internal void RefreshRelations()
		{
			foreach (KeyValuePair<string, RdDataSet> obj in this._rep.DataSets.List)
			{
				bool flag = obj.Value != null && obj.Value.MasterDataSet != null;
				if (flag)
				{
					this.Items[obj.Value.Name].Master = this.Items[obj.Value.MasterDataSet.Name];
				}
				else
				{
					this.Items[obj.Value.Name].Master = null;
				}
			}
		}

		internal void RefreshRootdataset()
		{
			foreach (KeyValuePair<string, RmDataSet> obj in this.Items)
			{
				RmDataSet mdataset = obj.Value;
				while (mdataset.Master != null)
				{
					mdataset = mdataset.Master;
				}
				obj.Value.Root = mdataset;
			}
		}

		internal void RefreshRowDataset()
		{
			for (int i = 1; i <= this._rep.Rows.RowCount; i++)
			{
				RdRow row = this._rep.Rows[i];
				bool flag = string.IsNullOrEmpty(row.DataSet);
				if (flag)
				{
					row.Data = null;
				}
				else
				{
					RmDataSet dataset = this.FindDataset(row.DataSet);
					row.Data = dataset;
					bool flag2 = dataset != null;
					if (flag2)
					{
						RdRowType rowType = row.RowType;
						if (rowType != RdRowType.rtColumnHeader)
						{
							if (rowType == RdRowType.rtColumnFooter)
							{
								dataset.ColumnFooter.Add(dataset.ColumnFooter.Count, row);
							}
						}
						else
						{
							dataset.ColumnHeader.Add(dataset.ColumnHeader.Count, row);
						}
					}
				}
			}
		}
	}
}
