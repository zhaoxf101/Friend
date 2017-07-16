using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIM.T_KERNEL;
using TIM.T_KERNEL.Data;
using TIM.T_KERNEL.Helper;
using TIM.T_TEMPLET.Enum;
using TIM.T_TEMPLET.Master;
using TIM.T_WEBCTRL;
using TIM.T_WORKFLOW;

namespace TIM.T_TEMPLET.Page
{
	public class TBillXGridBase : PageBase
	{
		private TBillXGrid m_curMaster = null;

		private bool MasterIsEdit = false;

		private bool MasterInsert = true;

		private bool MasterEdit = true;

		private bool MasterDelete = true;

		private bool m_seriallyInsert = true;

		private Dictionary<int, XGridSetMap> m_listXGridSetMap = new Dictionary<int, XGridSetMap>();

		public TBillXGrid CurMaster
		{
			get
			{
				return this.m_curMaster;
			}
			set
			{
				this.m_curMaster = value;
			}
		}

		protected bool SeriallyInsert
		{
			get
			{
				return this.m_seriallyInsert;
			}
			set
			{
				this.m_seriallyInsert = value;
			}
		}

		protected Dictionary<int, XGridSetMap> ListXGridSetMap
		{
			get
			{
				return this.m_listXGridSetMap;
			}
		}

		protected void AddXGridSetMap(XGridSetMap xGridSetMap)
		{
			this.ListXGridSetMap.Add(this.ListXGridSetMap.Count, xGridSetMap);
		}

		private void BuildEvent()
		{
			this.CurMaster.OnInsert += new TMasterBase.Insert(this.CurMaster_OnInsert);
			this.CurMaster.OnCopy += new TMasterBase.Copy(this.CurMaster_OnCopy);
			this.CurMaster.OnEdit += new TMasterBase.Edit(this.CurMaster_OnEdit);
			this.CurMaster.OnSave += new TMasterBase.Save(this.CurMaster_OnSave);
			this.CurMaster.OnCancel += new TMasterBase.Cancel(this.CurMaster_OnCancel);
			this.CurMaster.OnDelete += new TMasterBase.Delete(this.CurMaster_OnDelete);
			this.CurMaster.OnPrint += new TMasterBase.Print(this.CurMaster_OnPrint);
			this.CurMaster.OnPrintMenuItem += new TMasterBase.PrintMenuItem(this.CurMaster_OnPrintMenuItem);
			this.CurMaster.OnPreview += new TMasterBase.Preview(this.CurMaster_OnPreview);
			this.CurMaster.OnPreviewMenuItem += new TMasterBase.PreviewMenuItem(this.CurMaster_OnPreviewMenuItem);
			this.CurMaster.OnReportStyle += new TMasterBase.ReportStyle(this.CurMaster_OnReportStyle);
			this.CurMaster.OnAttach += new TMasterBase.Attach(this.CurMaster_OnAttach);
			foreach (KeyValuePair<int, XGridSetMap> item in this.ListXGridSetMap)
			{
				item.Value.XGrid.AddClick += new TimXGrid.ImageClickEventHandler(this.XGrid_AddClick);
				item.Value.XGrid.DeleteClick += new TimXGrid.ImageClickEventHandler(this.XGrid_DeleteClick);
				item.Value.XGrid.RowDataBound += new GridViewRowEventHandler(this.XGrid_RowDataBound);
				item.Value.XGrid.RowDataBound += new GridViewRowEventHandler(this.XGrid_RowDataBound_Templet);
			}
		}

		private void XGrid_AddClick(object sender, ImageClickEventArgs e)
		{
			TimXGrid xGrid = sender as TimXGrid;
			this.RecoverPointXGridData(xGrid);
			DataRow row = xGrid.XData.NewRow();
			bool seriallyInsert = this.SeriallyInsert;
			if (seriallyInsert)
			{
				xGrid.XData.Rows.Add(row);
			}
			else
			{
				int selectedIndex = xGrid.SelectedIndex;
				bool flag = selectedIndex < 0;
				if (flag)
				{
					xGrid.XData.Rows.Add(row);
				}
				else
				{
					xGrid.XData.Rows.InsertAt(row, selectedIndex + 1);
				}
			}
			xGrid.DataSource = xGrid.XData;
			xGrid.DataBind();
			bool workflow = base.CurEntity.Entity.Workflow;
			if (workflow)
			{
				bool flag2 = base.PageOperState == PageState.INSERT || base.PageOperState == PageState.COPY || base.PageOperState == PageState.EDIT;
				if (flag2)
				{
					this.SetWorkflowFieldRightForXGrid(base.WorkflowId);
				}
			}
			this.PlaceUpdateContent();
		}

		private void XGrid_DeleteClick(object sender, ImageClickEventArgs e)
		{
			TimXGrid xGrid = sender as TimXGrid;
			this.RecoverPointXGridData(xGrid);
			xGrid.XData.Rows.Remove(xGrid.XData.Rows[xGrid.SelectedIndex]);
			xGrid.DataSource = xGrid.XData;
			xGrid.DataBind();
			this.PlaceUpdateContent();
		}

		protected virtual void XGrid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
		}

		private void XGrid_RowDataBound_Templet(object sender, GridViewRowEventArgs e)
		{
		}

		private void ClearXGridData()
		{
			foreach (KeyValuePair<int, XGridSetMap> item in this.ListXGridSetMap)
			{
				item.Value.XGrid.XData = item.Value.GridEntity.RecordSet;
				item.Value.XGrid.DataSource = item.Value.GridEntity.RecordSet;
				item.Value.XGrid.DataBind();
			}
		}

		protected virtual void InitInsert()
		{
		}

		private void InitInsertCtrlValue()
		{
			base.ModifierId = base.UserId;
			base.WorkflowRunId = 0;
			foreach (KeyValuePair<string, FieldMapAttribute> item in base.CurEntity.Fields)
			{
				TempletUtils.SetInsertCtrlVal(this.CurMaster.CphContent, item.Value);
			}
			this.SetFileRelatedCtrlNull();
			this.ClearXGridData();
		}

		protected virtual void InitCopy()
		{
		}

		private void InitCopyCtrlValue()
		{
			base.ModifierId = base.UserId;
			base.WorkflowRunId = 0;
			foreach (KeyValuePair<string, FieldMapAttribute> item in base.CurEntity.Fields)
			{
				TempletUtils.SetCopyCtrlVal(this.CurMaster.CphContent, item.Value);
			}
			this.SetFileRelatedCtrlNull();
		}

		private void SetFileRelatedCtrlNull()
		{
			bool beDoc = base.CurEntity.Entity.BeDoc;
			if (beDoc)
			{
				this.CurMaster._Maintenance.TempletFileGroup.Value = "";
				this.CurMaster._Maintenance.TempletFiles.Value = "";
				this.RecoverBtnAttachText();
				this.PlaceUpdateMaintenanceTemplet();
			}
		}

		protected virtual void InitEdit()
		{
		}

		protected virtual void InitNull()
		{
		}

		private void InitNullCtrlValue()
		{
			base.ModifierId = base.UserId;
			base.WorkflowRunId = 0;
			foreach (KeyValuePair<string, FieldMapAttribute> item in base.CurEntity.Fields)
			{
				TempletUtils.SetNullCtrlVal(this.CurMaster.CphContent, item.Value);
			}
			this.ClearXGridData();
		}

		protected void CurMaster_OnInsert(object sender, EventArgs e)
		{
			bool flag = base.CanInsertTemplet();
			if (flag)
			{
				base.PageOperState = PageState.INSERT;
				this.InitInsertCtrlValue();
				this.InitInsert();
				base.SetMasterCtrlState();
				this.SetPageCtrlState();
				this.PlaceUpdateMenu();
				this.PlaceUpdateContent();
			}
		}

		private void CurMaster_OnCopy(object sender, EventArgs e)
		{
			bool flag = base.CanInsertTemplet();
			if (flag)
			{
				base.PageOperState = PageState.COPY;
				this.InitCopyCtrlValue();
				this.InitCopy();
				base.SetMasterCtrlState();
				this.SetPageCtrlState();
				this.PlaceUpdateMenu();
				this.PlaceUpdateContent();
			}
		}

		private void CurMaster_OnEdit(object sender, EventArgs e)
		{
			this.RecoverData();
			bool flag = base.CanEditTemplet();
			if (flag)
			{
				base.PageOperState = PageState.EDIT;
				this.InitEdit();
				base.SetMasterCtrlState();
				this.SetPageCtrlState();
				this.PlaceUpdateMenu();
				this.PlaceUpdateContent();
			}
		}

		protected sealed override bool OnSave()
		{
			bool result = true;
			bool flag = result;
			if (flag)
			{
				result = this.OnPreSave();
			}
			this.RecoverData();
			bool flag2 = result;
			if (flag2)
			{
				result = this.VerifyNull();
			}
			bool flag3 = result;
			if (flag3)
			{
				result = this.VerifyLength();
			}
			bool flag4 = result;
			if (flag4)
			{
				result = this.TempletVerifyBusinessLogic();
			}
			bool flag5 = base.PageOperState == PageState.INSERT || base.PageOperState == PageState.COPY;
			bool result2;
			if (flag5)
			{
				base.CurEntity.InsertSql = base.CurEntity.BuildInsertSql();
				bool flag6 = result;
				if (flag6)
				{
					result = base.OnSave();
				}
				bool flag7 = result && base.CurEntity.Entity.Workflow;
				if (flag7)
				{
					bool flag8 = base.WorkflowRunId == 0;
					if (flag8)
					{
						bool flag9 = !base.Workflow.CreateInstance(base.WorkflowId);
						if (flag9)
						{
							result2 = false;
							return result2;
						}
						base.CurEntity.WorkflowId = base.Workflow.WfId;
						base.CurEntity.WorkflowRunId = base.Workflow.WfRunId;
						base.WorkflowRunId = base.Workflow.WfRunId;
					}
				}
				bool flag10 = result && base.CurEntity.Entity.BeDoc;
				if (flag10)
				{
					base.CurEntity.FileGroupId = this.CurMaster._Maintenance.TempletFileGroup.Value.ToDouble();
					base.CurEntity.GroupFiles = this.CurMaster._Maintenance.TempletFiles.Value.ToDouble();
				}
				bool flag11 = result;
				if (flag11)
				{
					result = base.CurEntity.Insert();
				}
				bool workflow = base.CurEntity.Entity.Workflow;
				if (workflow)
				{
					bool flag12 = result;
					if (flag12)
					{
						result = base.Workflow.CreatedComplete();
					}
				}
			}
			else
			{
				base.CurEntity.UpdateSql = base.CurEntity.BuildUpdateSql();
				bool flag13 = result;
				if (flag13)
				{
					result = base.OnSave();
				}
				bool flag14 = result;
				if (flag14)
				{
					result = base.CurEntity.Update();
				}
				bool flag15 = result && base.CurEntity.Entity.Workflow;
				if (flag15)
				{
					bool flag16 = base.WorkflowRunId != 0;
					if (flag16)
					{
						bool flag17 = !base.Workflow.OnlySave(base.WorkflowId, base.WorkflowRunId);
						if (flag17)
						{
							result2 = false;
							return result2;
						}
					}
				}
				bool flag18 = result;
				if (flag18)
				{
					result = this.OnDeleteBill();
				}
			}
			bool flag19 = result;
			if (flag19)
			{
				foreach (KeyValuePair<int, XGridSetMap> tab in this.ListXGridSetMap)
				{
					tab.Value.GridEntity.AllowPagingQuery = false;
					foreach (KeyValuePair<string, RelatedEntityAttribute> item in base.CurEntity.RelatedObject)
					{
						string[] keyList = item.Value.SlaveMainKey.Split(new char[]
						{
							','
						});
						for (int keyIndex = 0; keyIndex < keyList.Length; keyIndex++)
						{
							bool flag20 = tab.Value.XGrid.XData.Columns.Contains(keyList[keyIndex]);
							if (flag20)
							{
								object keyValue = base.CurEntity.GetField(item.Value.MasterMainKey.Split(new char[]
								{
									','
								})[keyIndex]);
								for (int i = 0; i < tab.Value.XGrid.XData.Rows.Count; i++)
								{
									tab.Value.XGrid.XData.Rows[i][keyList[keyIndex]] = keyValue;
								}
							}
						}
					}
					tab.Value.GridEntity.RecordSet = tab.Value.XGrid.XData;
					for (int j = 0; j < tab.Value.XGrid.XData.Rows.Count; j++)
					{
						tab.Value.GridEntity.InsertSql = tab.Value.GridEntity.BuildInsertSql();
						tab.Value.GridEntity.ActivedIndex = j;
						bool flag21 = !tab.Value.GridEntity.Insert();
						if (flag21)
						{
							base.PromptDialog(tab.Value.GridEntity.PromptMessage);
							result2 = false;
							return result2;
						}
					}
				}
			}
			bool flag22 = result;
			if (flag22)
			{
				result = this.OnSaveComplete();
			}
			result2 = result;
			return result2;
		}

		private void CurMaster_OnSave(object sender, EventArgs e)
		{
			bool result = false;
			bool flag = !this.OnPreSaveSwitch();
			if (!flag)
			{
				Database db = LogicContext.GetDatabase();
				db.BeginTrans();
				try
				{
					result = this.OnSave();
					bool flag2 = result;
					if (flag2)
					{
						db.CommitTrans();
					}
					else
					{
						db.RollbackTrans();
					}
				}
				catch
				{
					db.RollbackTrans();
				}
				bool flag3 = result;
				if (flag3)
				{
					base.PageOperState = PageState.VIEW;
					this.OnSaveCompleteSwitch();
					base.SetMasterCtrlState();
					this.SetPageCtrlState();
					this.PlaceUpdateMenu();
					this.PlaceUpdateContent();
				}
			}
		}

		private void CurMaster_OnCancel(object sender, EventArgs e)
		{
			bool flag = base.PageOperState == PageState.INSERT || base.PageOperState == PageState.COPY;
			if (flag)
			{
				base.PageOperState = PageState.NULL;
				this.InitNullCtrlValue();
				this.InitNull();
			}
			else
			{
				base.PageOperState = PageState.VIEW;
				this.OnPreLoadRecord();
				this.OnLoadRecord();
				this.OnLoadRecordComplete();
			}
			base.SetMasterCtrlState();
			this.SetPageCtrlState();
			this.PlaceUpdateMenu();
			this.PlaceUpdateContent();
		}

		protected internal override void RecoverData()
		{
			this.RecoverXGridData();
			this.OnRecoverXGridComplete();
			base.GetCtrlValByPage(this.CurMaster.CphContent);
			DataRow row = base.CurEntity.RecordSet.NewRow();
			foreach (KeyValuePair<string, FieldMapAttribute> item in base.CurEntity.Fields)
			{
				bool flag = !string.IsNullOrWhiteSpace(item.Value.DbField);
				if (flag)
				{
					bool flag2 = item.Value.DbType == TimDbType.DateTime && string.IsNullOrWhiteSpace(item.Value.NewValue);
					if (flag2)
					{
						row[item.Value.DbField] = DBNull.Value;
					}
					else
					{
						bool flag3 = item.Value.DbType == TimDbType.Float && string.IsNullOrWhiteSpace(item.Value.NewValue);
						if (flag3)
						{
							row[item.Value.DbField] = 0;
						}
						else
						{
							row[item.Value.DbField] = item.Value.NewValue;
						}
					}
				}
			}
			base.CurEntity.RecordSet.Rows.Add(row);
			base.CurEntity.ActivedIndex = 0;
		}

		internal void RecoverXGridData()
		{
			foreach (KeyValuePair<int, XGridSetMap> tab in this.ListXGridSetMap)
			{
				this.RecoverPointXGridData(tab.Value.XGrid);
			}
		}

		internal void RecoverPointXGridData(TimXGrid xGrid)
		{
			for (int rowIndex = 0; rowIndex < xGrid.Rows.Count; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < xGrid.Columns.Count; columnIndex++)
				{
					bool flag = xGrid.Columns[columnIndex] is TimXDateField;
					if (flag)
					{
						bool flag2 = (xGrid.Rows[rowIndex].Cells[columnIndex].Controls[0] as TimDate).Value == TimCtrlUtils.UltDateTime;
						if (flag2)
						{
							xGrid.XData.Rows[rowIndex][(xGrid.Columns[columnIndex] as BoundField).DataField] = DBNull.Value;
						}
						else
						{
							xGrid.XData.Rows[rowIndex][(xGrid.Columns[columnIndex] as BoundField).DataField] = (xGrid.Rows[rowIndex].Cells[columnIndex].Controls[0] as TimDate).Value;
						}
					}
					else
					{
						bool flag3 = xGrid.Columns[columnIndex] is TimXDateTimeField;
						if (flag3)
						{
							bool flag4 = (xGrid.Rows[rowIndex].Cells[columnIndex].Controls[0] as TimDateTime).Value == TimCtrlUtils.UltDateTime;
							if (flag4)
							{
								xGrid.XData.Rows[rowIndex][(xGrid.Columns[columnIndex] as BoundField).DataField] = DBNull.Value;
							}
							else
							{
								xGrid.XData.Rows[rowIndex][(xGrid.Columns[columnIndex] as BoundField).DataField] = (xGrid.Rows[rowIndex].Cells[columnIndex].Controls[0] as TimDateTime).Value;
							}
						}
						else
						{
							bool flag5 = xGrid.Columns[columnIndex] is TimXNumericTextBoxField;
							if (flag5)
							{
								xGrid.XData.Rows[rowIndex][(xGrid.Columns[columnIndex] as BoundField).DataField] = (xGrid.Rows[rowIndex].Cells[columnIndex].Controls[0] as TimNumericTextBox).Value;
							}
							else
							{
								bool flag6 = xGrid.Columns[columnIndex] is TimXNumericUpDownField;
								if (flag6)
								{
									xGrid.XData.Rows[rowIndex][(xGrid.Columns[columnIndex] as BoundField).DataField] = (xGrid.Rows[rowIndex].Cells[columnIndex].Controls[0] as TimNumericUpDown).Value;
								}
								else
								{
									bool flag7 = xGrid.Columns[columnIndex] is TimXTextBoxField;
									if (flag7)
									{
										xGrid.XData.Rows[rowIndex][(xGrid.Columns[columnIndex] as BoundField).DataField] = (xGrid.Rows[rowIndex].Cells[columnIndex].Controls[0] as TimTextBox).Text;
									}
									else
									{
										bool flag8 = xGrid.Columns[columnIndex] is TimXCheckBoxField;
										if (flag8)
										{
											xGrid.XData.Rows[rowIndex][(xGrid.Columns[columnIndex] as BoundField).DataField] = (xGrid.Rows[rowIndex].Cells[columnIndex].Controls[0] as TimCheckBox).Value;
										}
										else
										{
											bool flag9 = xGrid.Columns[columnIndex] is TimXBoundField;
											if (flag9)
											{
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private bool OnDeleteBill()
		{
			bool result;
			foreach (KeyValuePair<string, RelatedEntityAttribute> item in base.CurEntity.RelatedObject)
			{
				EntityManager emObj = (EntityManager)base.CurEntity.GetType().GetProperty(item.Value.Name).GetValue(base.CurEntity, null);
				emObj.AllowPagingQuery = false;
				emObj.RecordSetSql = emObj.BuildRecordSetSql();
				string[] keyList = item.Value.SlaveMainKey.Split(new char[]
				{
					','
				});
				for (int keyIndex = 0; keyIndex < keyList.Length; keyIndex++)
				{
					bool flag = emObj.RecordSetSql.ParamByName(keyList[keyIndex]) != null;
					if (flag)
					{
						emObj.RecordSetSql.AddParam(keyList[keyIndex], base.CurEntity.Fields[item.Value.MasterMainKey.Split(new char[]
						{
							','
						})[keyIndex]].DbType, base.CurEntity.Fields[item.Value.MasterMainKey.Split(new char[]
						{
							','
						})[keyIndex]].Len, base.CurEntity.GetField(item.Value.MasterMainKey.Split(new char[]
						{
							','
						})[keyIndex]));
					}
				}
				emObj.QueryRecordSet();
				for (int i = 0; i < emObj.RecordSet.Rows.Count; i++)
				{
					emObj.DeleteSql = emObj.BuildDeleteSql();
					emObj.ActivedIndex = i;
					bool flag2 = !emObj.Delete();
					if (flag2)
					{
						base.PromptDialog(emObj.PromptMessage);
						result = false;
						return result;
					}
				}
			}
			result = true;
			return result;
		}

		protected sealed override bool OnDelete()
		{
			bool result = true;
			this.RecoverData();
			base.CurEntity.DeleteSql = base.CurEntity.BuildDeleteSql();
			bool flag = result;
			if (flag)
			{
				result = this.OnPreDelete();
			}
			bool flag2 = result;
			if (flag2)
			{
				result = base.CanDeleteTemplet();
			}
			bool flag3 = result;
			if (flag3)
			{
				result = base.OnDelete();
			}
			bool flag4 = result;
			if (flag4)
			{
				result = base.CurEntity.Delete();
			}
			bool workflow = base.CurEntity.Entity.Workflow;
			if (workflow)
			{
				bool flag5 = result;
				if (flag5)
				{
					result = base.Workflow.DeleteWorkflowRI(base.WorkflowId, base.WorkflowRunId);
				}
			}
			bool flag6 = result;
			if (flag6)
			{
				result = this.OnDeleteComplete();
			}
			return result;
		}

		private void CurMaster_OnDelete(object sender, EventArgs e)
		{
			bool result = false;
			Database db = LogicContext.GetDatabase();
			db.BeginTrans();
			try
			{
				result = this.OnDelete();
				bool flag = result;
				if (flag)
				{
					db.CommitTrans();
				}
				else
				{
					db.RollbackTrans();
				}
			}
			catch
			{
				db.RollbackTrans();
			}
			bool flag2 = result;
			if (flag2)
			{
				base.CloseDialog();
			}
		}

		private void CurMaster_OnPrint(object sender, EventArgs e)
		{
		}

		private void CurMaster_OnPrintMenuItem(object sender, string itemValue)
		{
			base.ReportParse.ReportDataSetAdd(base.CurEntity.Entity.Table, base.CurEntity, this.ListXGridSetMap);
			this.OnPreLoadRecord();
			base.CurEntity.RecordSql = base.CurEntity.BuildRecordSql();
			base.CurEntity.QueryRecord();
			base.ReportParse.ReportRecordSetAdd(base.CurEntity.Entity.Table, base.CurEntity, base.CurEntity.BuildRecordSql(), this.ListXGridSetMap);
			base.ReportParse.PreparePrintOrPreview(base.PageAmIdPlusClassName, itemValue, "Print");
			base.RegisterScript("PreviewMenuItem", "window.setTimeout('SetReport()', 1);function SetReport() {" + base.ReportParse.run + "}");
		}

		private void CurMaster_OnPreview(object sender, EventArgs e)
		{
		}

		private void CurMaster_OnPreviewMenuItem(object sender, string itemValue)
		{
			base.ReportParse.ReportDataSetAdd(base.CurEntity.Entity.Table, base.CurEntity, this.ListXGridSetMap);
			this.OnPreLoadRecord();
			base.CurEntity.RecordSql = base.CurEntity.BuildRecordSql();
			base.CurEntity.QueryRecord();
			base.ReportParse.ReportRecordSetAdd(base.CurEntity.Entity.Table, base.CurEntity, base.CurEntity.BuildRecordSql(), this.ListXGridSetMap);
			base.ReportParse.PreparePrintOrPreview(base.PageAmIdPlusClassName, itemValue, "Preview");
			base.RegisterScript("PreviewMenuItem", "window.setTimeout('SetReport()', 1);function SetReport() {" + base.ReportParse.run + "}");
		}

		private void CurMaster_OnReportStyle(object sender, EventArgs e)
		{
			PageParameter pageParams = new PageParameter();
			pageParams.UrlPath = this.Page.ResolveUrl("~") + "T_TEMPLET/CommForm/ReportStyleList.aspx";
			pageParams.AddString("STYLEID", base.PageAmIdPlusClassName);
			base.ReportParse.ReportDataSetAdd(base.CurEntity.Entity.Table, base.CurEntity, this.ListXGridSetMap);
			base.ReportParse.PrepareSetMode();
			pageParams.AddExtString("REPORTSTYLE", base.ReportParse.ReportXMLDataSets);
			base.OpenDialog(pageParams);
		}

		private void CurMaster_OnAttach(object sender, EventArgs e)
		{
			this.RecoverData();
			base.OpenAttachDialog(this.CurMaster);
		}

		protected override void OnLoadRecord()
		{
			base.CurEntity.RecordSql = base.CurEntity.BuildRecordSql();
			base.CurEntity.QueryRecord();
			bool flag = base.CurEntity.RecordCount > 0;
			if (flag)
			{
				this.FillCtrlValue();
				this.FillXGridSetMap();
			}
		}

		private void FillCtrlValue()
		{
			foreach (KeyValuePair<string, FieldMapAttribute> item in base.CurEntity.Fields)
			{
				item.Value.OldValue = item.Value.NewValue;
				item.Value.NewValue = base.CurEntity.GetField(item.Value.DbField).ToString();
				bool flag = item.Value.DbField == "MODIFIERID";
				if (flag)
				{
					base.ModifierId = base.CurEntity.GetField(item.Value.DbField).ToString().Trim();
				}
				bool flag2 = base.CurEntity.Entity.BeDoc && (item.Value.CtrlId == "Templet_FileGroup" || item.Value.CtrlId == "Templet_Files");
				if (flag2)
				{
					TempletUtils.SetCtrlValByRecord(this.CurMaster._Maintenance.UpTempletPlace, item.Value);
				}
				else
				{
					TempletUtils.SetCtrlValByRecord(this.CurMaster.CphContent, item.Value);
				}
			}
		}

		private void FillXGridSetMap()
		{
			foreach (KeyValuePair<int, XGridSetMap> tab in this.ListXGridSetMap)
			{
				tab.Value.GridEntity.AllowPagingQuery = false;
				tab.Value.GridEntity.RecordSetSql = tab.Value.GridEntity.BuildRecordSetSql();
				foreach (KeyValuePair<string, RelatedEntityAttribute> item in base.CurEntity.RelatedObject)
				{
					string[] keyList = item.Value.SlaveMainKey.Split(new char[]
					{
						','
					});
					for (int keyIndex = 0; keyIndex < keyList.Length; keyIndex++)
					{
						bool flag = tab.Value.GridEntity.RecordSetSql.ParamByName(keyList[keyIndex]) != null;
						if (flag)
						{
							tab.Value.GridEntity.RecordSetSql.AddParam(keyList[keyIndex], base.CurEntity.Fields[item.Value.MasterMainKey.Split(new char[]
							{
								','
							})[keyIndex]].DbType, base.CurEntity.Fields[item.Value.MasterMainKey.Split(new char[]
							{
								','
							})[keyIndex]].Len, base.CurEntity.GetField(item.Value.MasterMainKey.Split(new char[]
							{
								','
							})[keyIndex]));
						}
					}
				}
				tab.Value.GridEntity.QueryRecordSet();
				tab.Value.XGrid.DataSource = tab.Value.GridEntity.RecordSet;
				tab.Value.XGrid.XData = tab.Value.GridEntity.RecordSet;
				tab.Value.XGrid.DataBind();
			}
		}

		private void BuildWorkflowButtonEvent()
		{
			this.CurMaster.OnDirectSubmit += new TMasterBase.DirectSubmit(this.CurMaster_OnDirectSubmit);
			this.CurMaster.OnSubmit += new TMasterBase.Submit(this.CurMaster_OnSubmit);
			this.CurMaster.OnBack += new TMasterBase.Back(this.CurMaster_OnBack);
			this.CurMaster.OnWithdraw += new TMasterBase.Withdraw(this.CurMaster_OnWithdraw);
			this.CurMaster.OnDeliverTo += new TMasterBase.DeliverTo(this.CurMaster_OnDeliverTo);
			this.CurMaster.OnVeto += new TMasterBase.Veto(this.CurMaster_OnVeto);
			this.CurMaster.OnFlowBlock += new TMasterBase.FlowBlock(this.CurMaster_OnFlowBlock);
			this.CurMaster.OnWorkflowTrace += new TMasterBase.WorkflowTrace(this.CurMaster_OnWorkflowTrace);
		}

		private void CurMaster_OnDirectSubmit(object sender, EventArgs e)
		{
			bool flag = base.PageOperState == PageState.INSERT || base.PageOperState == PageState.COPY || base.PageOperState == PageState.EDIT;
			if (flag)
			{
				this.CurMaster_OnSave(sender, e);
			}
			else
			{
				bool flag2 = base.PageOperState == PageState.VIEW;
				if (flag2)
				{
					this.RecoverData();
				}
			}
			base.OnDirectSubmitIn(PageClassification.Editing);
		}

		private void CurMaster_OnSubmit(object sender, EventArgs e)
		{
			bool flag = base.PageOperState == PageState.INSERT || base.PageOperState == PageState.COPY || base.PageOperState == PageState.EDIT;
			if (flag)
			{
				this.CurMaster_OnSave(sender, e);
			}
			else
			{
				bool flag2 = base.PageOperState == PageState.VIEW;
				if (flag2)
				{
					this.RecoverData();
				}
			}
			base.OnSubmitIn(PageClassification.Editing);
		}

		private void CurMaster_OnBack(object sender, EventArgs e)
		{
			base.OnBackIn(PageClassification.Editing);
		}

		private void CurMaster_OnWithdraw(object sender, EventArgs e)
		{
			base.OnWithdrawIn(PageClassification.Editing);
		}

		private void CurMaster_OnDeliverTo(object sender, EventArgs e)
		{
			base.OnDeliverToIn(PageClassification.Editing);
		}

		private void CurMaster_OnVeto(object sender, EventArgs e)
		{
			base.OnVetoIn(PageClassification.Editing);
		}

		private void CurMaster_OnFlowBlock(string workflowAction, string nextWfId, string nextWfpId, string todo, string opinion)
		{
			this.RecoverData();
			base.OnFlowBlockIn(PageClassification.Editing, workflowAction, nextWfId, nextWfpId, todo, opinion);
		}

		private void CurMaster_OnWorkflowTrace(object sender, EventArgs e)
		{
			base.OnWorkflowTraceIn(PageClassification.Editing);
		}

		protected sealed override void OnInit()
		{
			this.BuildEvent();
			bool workflow = base.CurEntity.Entity.Workflow;
			if (workflow)
			{
				this.BuildWorkflowButtonEvent();
			}
		}

		protected override void OnPreLoad()
		{
			this.MasterIsEdit = (string.IsNullOrWhiteSpace(base.PageParam.GetString("MASTERISEDIT")) ? this.MasterIsEdit : base.PageParam.GetString("MASTERISEDIT").ToBool());
			this.MasterInsert = (string.IsNullOrWhiteSpace(base.PageParam.GetString("MASTERINSERT")) ? this.MasterInsert : base.PageParam.GetString("MASTERINSERT").ToBool());
			this.MasterEdit = (string.IsNullOrWhiteSpace(base.PageParam.GetString("MASTEREDIT")) ? this.MasterEdit : base.PageParam.GetString("MASTEREDIT").ToBool());
			this.MasterDelete = (string.IsNullOrWhiteSpace(base.PageParam.GetString("MASTERDELETE")) ? this.MasterDelete : base.PageParam.GetString("MASTERDELETE").ToBool());
		}

		protected override void OnLoad()
		{
			bool flag = !base.IsPostBack;
			if (flag)
			{
				switch (base.PageOperState)
				{
				case PageState.VIEW:
				case PageState.COPY:
				case PageState.EDIT:
					this.OnPreLoadRecord();
					this.OnLoadRecord();
					this.OnLoadRecordComplete();
					break;
				}
				base.SwitchPageOperState();
			}
		}

		protected sealed override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);
			bool workflow = base.CurEntity.Entity.Workflow;
			if (workflow)
			{
				base.WorkflowId = base.Workflow.GetWorkflowId(base.CurEntity.Entity.WorkflowBusinessId);
			}
			this.OnLoadComplete();
			bool flag = !base.IsPostBack;
			if (flag)
			{
				base.SetMasterCtrlState();
				this.SetPageCtrlState();
				base.SetCtrlMaxLength(this.CurMaster.CphContent);
			}
			TimButtonMenu expr_80 = this.CurMaster.BtnDelete;
			expr_80.OnClientClick += "if (confirm('您确定要删除当前记录？') == false) return false;";
			bool flag2 = !base.IsPostBack;
			if (flag2)
			{
				string adjust = string.Empty;
				foreach (KeyValuePair<int, XGridSetMap> tab in this.ListXGridSetMap)
				{
					adjust = string.Concat(new string[]
					{
						adjust,
						"XGridAdjust('",
						tab.Value.XGrid.ClientID,
						"','",
						tab.Value.XGrid.HeadDivClientId,
						"','",
						tab.Value.XGrid.BodyDivClientId,
						"');"
					});
				}
				base.RegisterScript("XGridAdjust", adjust);
			}
		}

		internal override void SwitchTempletPageOperState()
		{
			switch (base.PageOperState)
			{
			case PageState.INSERT:
			{
				bool flag = base.CanInsertTemplet();
				if (flag)
				{
					this.InitInsertCtrlValue();
					this.InitInsert();
				}
				else
				{
					base.PageOperState = PageState.NULL;
				}
				break;
			}
			case PageState.COPY:
			{
				bool flag2 = base.CanInsertTemplet();
				if (flag2)
				{
					this.InitCopyCtrlValue();
					this.InitCopy();
				}
				else
				{
					base.PageOperState = PageState.VIEW;
				}
				break;
			}
			case PageState.EDIT:
			{
				bool flag3 = base.CanEditTemplet();
				if (flag3)
				{
					this.InitEdit();
				}
				else
				{
					base.PageOperState = PageState.VIEW;
				}
				break;
			}
			}
		}

		internal override void SetTempletCtrlState()
		{
			bool workflow = base.CurEntity.Entity.Workflow;
			if (workflow)
			{
				base.Workflow.Init(base.WorkflowId, base.WorkflowRunId);
			}
			else
			{
				this.CurMaster.BtnWorkflow.Visible = base.CurEntity.Entity.Workflow;
			}
			this.CurMaster.BtnAttach.Visible = base.CurEntity.Entity.BeDoc;
			this.CurMaster.BtnPrint.Visible = (this.CurMaster.BtnPreview.Visible = base.CanPrintTemplet());
			this.CurMaster.BtnReportStyle.Visible = base.CanDesignTemplet();
			switch (base.PageOperState)
			{
			case PageState.VIEW:
			{
				this.CurMaster.BtnInsert.Enabled = (this.CurMaster.BtnCopy.Enabled = (base.CanInsertTemplet() && !this.MasterIsEdit && this.MasterInsert));
				this.CurMaster.BtnEdit.Enabled = (base.CanEditTemplet() && !this.MasterIsEdit && this.MasterEdit);
				this.CurMaster.BtnSave.Enabled = false;
				this.CurMaster.BtnCancel.Enabled = false;
				this.CurMaster.BtnDelete.Enabled = (base.CanDeleteTemplet() && !this.MasterIsEdit && this.MasterDelete);
				this.CurMaster.BtnPrint.Enabled = this.CurMaster.BtnPrint.Visible;
				this.CurMaster.BtnPreview.Enabled = this.CurMaster.BtnPrint.Visible;
				this.CurMaster.BtnReportStyle.Enabled = this.CurMaster.BtnReportStyle.Visible;
				bool workflow2 = base.CurEntity.Entity.Workflow;
				if (workflow2)
				{
					this.CurMaster.SubmitPermission = base.Workflow.GetActionPermission(WorkflowAction.S);
					this.CurMaster.BackPermission = base.Workflow.GetActionPermission(WorkflowAction.B);
					this.CurMaster.WithdrawPermission = base.Workflow.GetActionPermission(WorkflowAction.W);
					this.CurMaster.DeliverToPermission = base.Workflow.GetActionPermission(WorkflowAction.D);
					this.CurMaster.VetoPermission = base.Workflow.GetActionPermission(WorkflowAction.V);
					this.CurMaster.TracePermission = base.Workflow.GetActionPermission(WorkflowAction.T);
				}
				break;
			}
			case PageState.NULL:
				this.CurMaster.BtnInsert.Enabled = base.CanInsertTemplet();
				this.CurMaster.BtnCopy.Enabled = false;
				this.CurMaster.BtnEdit.Enabled = false;
				this.CurMaster.BtnSave.Enabled = false;
				this.CurMaster.BtnCancel.Enabled = false;
				this.CurMaster.BtnDelete.Enabled = false;
				this.CurMaster.BtnPrint.Enabled = false;
				this.CurMaster.BtnPreview.Enabled = false;
				this.CurMaster.BtnReportStyle.Enabled = false;
				break;
			case PageState.INSERT:
			{
				this.CurMaster.BtnInsert.Enabled = false;
				this.CurMaster.BtnCopy.Enabled = false;
				this.CurMaster.BtnEdit.Enabled = false;
				this.CurMaster.BtnSave.Enabled = true;
				this.CurMaster.BtnCancel.Enabled = true;
				this.CurMaster.BtnDelete.Enabled = false;
				this.CurMaster.BtnPrint.Enabled = false;
				this.CurMaster.BtnPreview.Enabled = false;
				this.CurMaster.BtnReportStyle.Enabled = false;
				bool workflow3 = base.CurEntity.Entity.Workflow;
				if (workflow3)
				{
					this.CurMaster.SubmitPermission = true;
				}
				break;
			}
			case PageState.COPY:
			{
				this.CurMaster.BtnInsert.Enabled = false;
				this.CurMaster.BtnCopy.Enabled = false;
				this.CurMaster.BtnEdit.Enabled = false;
				this.CurMaster.BtnSave.Enabled = true;
				this.CurMaster.BtnCancel.Enabled = true;
				this.CurMaster.BtnDelete.Enabled = false;
				this.CurMaster.BtnPrint.Enabled = false;
				this.CurMaster.BtnPreview.Enabled = false;
				this.CurMaster.BtnReportStyle.Enabled = false;
				bool workflow4 = base.CurEntity.Entity.Workflow;
				if (workflow4)
				{
					this.CurMaster.SubmitPermission = true;
				}
				break;
			}
			case PageState.EDIT:
			{
				this.CurMaster.BtnInsert.Enabled = false;
				this.CurMaster.BtnCopy.Enabled = false;
				this.CurMaster.BtnEdit.Enabled = false;
				this.CurMaster.BtnSave.Enabled = true;
				this.CurMaster.BtnCancel.Enabled = true;
				this.CurMaster.BtnDelete.Enabled = false;
				this.CurMaster.BtnPrint.Enabled = false;
				this.CurMaster.BtnPreview.Enabled = false;
				this.CurMaster.BtnReportStyle.Enabled = false;
				bool workflow5 = base.CurEntity.Entity.Workflow;
				if (workflow5)
				{
					this.CurMaster.SubmitPermission = base.Workflow.GetActionPermission(WorkflowAction.S);
				}
				break;
			}
			}
			bool workflow6 = base.CurEntity.Entity.Workflow;
			if (workflow6)
			{
				this.CurMaster.SetWorkflowButtonPermission();
			}
			bool beDoc = base.CurEntity.Entity.BeDoc;
			if (beDoc)
			{
				bool flag = !string.IsNullOrWhiteSpace(this.CurMaster._Maintenance.TempletFiles.Value);
				if (flag)
				{
					this.CurMaster.BtnAttach.Text = string.Format("附件[{0}]", this.CurMaster._Maintenance.TempletFiles.Value);
				}
			}
			bool flag2 = base.CurEntity != null;
			if (flag2)
			{
				base.CurEntity.PromptMessage = string.Empty;
			}
		}

		protected override void RecoverBtnAttachText()
		{
			bool beDoc = base.CurEntity.Entity.BeDoc;
			if (beDoc)
			{
				bool flag = string.IsNullOrWhiteSpace(this.CurMaster._Maintenance.TempletFiles.Value);
				if (flag)
				{
					this.CurMaster.BtnAttach.Text = "附件";
				}
			}
		}

		private void SetPageCtrlState()
		{
			bool flag = base.PageOperState == PageState.VIEW || base.PageOperState == PageState.NULL;
			if (flag)
			{
				base.SetPageCtrlState(this.CurMaster.CphContent.Controls, true);
			}
			else
			{
				base.SetPageCtrlState(this.CurMaster.CphContent.Controls, false);
			}
			bool workflow = base.CurEntity.Entity.Workflow;
			if (workflow)
			{
				base.SetWorkflowFieldRight(base.WorkflowId);
				bool flag2 = base.PageOperState == PageState.INSERT || base.PageOperState == PageState.COPY || base.PageOperState == PageState.EDIT;
				if (flag2)
				{
					this.SetWorkflowFieldRightForXGrid(base.WorkflowId);
				}
			}
			this.SetCtrlState();
			this.SwitchControlValue();
			this.SetPageUrlAppendParam();
			base.RegisterScript("TempletClientAppendVar", "var _PageUrlAppendParam = '" + base.PageUrlAppendParam.EncodedParameters + "';");
		}

		private void SetWorkflowFieldRightForXGrid(string wfId)
		{
			bool flag = string.IsNullOrWhiteSpace(wfId);
			if (!flag)
			{
				string wfpId = string.Empty;
				bool flag2 = string.IsNullOrWhiteSpace(base.Workflow.WfpId);
				if (flag2)
				{
					wfpId = base.Workflow.GetBeginWfpId(wfId);
				}
				else
				{
					wfpId = base.Workflow.WfpId;
				}
				List<WFFR> lstWffr = WFFRUtils.GetWFFR(wfId, wfpId);
				string tableName = string.Empty;
				string columnName = string.Empty;
				foreach (WFFR wffr in lstWffr)
				{
					bool flag3 = wffr.ControlId.IndexOf(".") < 0 || wffr.Right > WffrRightType.R;
					if (!flag3)
					{
						tableName = wffr.ControlId.Split(new char[]
						{
							'.'
						})[0];
						columnName = wffr.ControlId.Split(new char[]
						{
							'.'
						})[1];
						foreach (KeyValuePair<int, XGridSetMap> tab in this.ListXGridSetMap)
						{
							bool flag4 = tab.Value.GridEntity.Entity.Table.Equals(tableName);
							if (flag4)
							{
								bool flag5 = !tab.Value.XGrid.Enabled;
								if (flag5)
								{
									break;
								}
								bool flag6 = columnName.Equals("ADD", StringComparison.OrdinalIgnoreCase);
								if (flag6)
								{
									((WebControl)tab.Value.XGrid.HeaderRow.Cells[0].Controls[0]).Enabled = false;
								}
								else
								{
									bool flag7 = columnName.Equals("EDIT", StringComparison.OrdinalIgnoreCase);
									if (flag7)
									{
										for (int rowIndex = 0; rowIndex < tab.Value.XGrid.Rows.Count; rowIndex++)
										{
											foreach (Control ctrl in tab.Value.XGrid.Rows[rowIndex].Cells[0].Controls)
											{
												bool flag8 = ctrl is WebControl && ctrl.ID.Equals("XBtn_Edit", StringComparison.OrdinalIgnoreCase);
												if (flag8)
												{
													((WebControl)ctrl).Enabled = false;
												}
											}
										}
									}
									else
									{
										bool flag9 = columnName.Equals("DELETE", StringComparison.OrdinalIgnoreCase);
										if (!flag9)
										{
											for (int columnIndex = 0; columnIndex < tab.Value.XGrid.Columns.Count; columnIndex++)
											{
												bool flag10 = tab.Value.XGrid.Columns[columnIndex] is BoundField && ((BoundField)tab.Value.XGrid.Columns[columnIndex]).DataField.Equals(columnName);
												if (flag10)
												{
													for (int rowIndex2 = 0; rowIndex2 < tab.Value.XGrid.Rows.Count; rowIndex2++)
													{
														((WebControl)tab.Value.XGrid.Rows[rowIndex2].Cells[columnIndex].Controls[0]).Enabled = false;
													}
												}
											}
											break;
										}
										for (int rowIndex3 = 0; rowIndex3 < tab.Value.XGrid.Rows.Count; rowIndex3++)
										{
											foreach (Control ctrl2 in tab.Value.XGrid.Rows[rowIndex3].Cells[0].Controls)
											{
												bool flag11 = ctrl2 is WebControl && ctrl2.ID.Equals("XBtn_Delete", StringComparison.OrdinalIgnoreCase);
												if (flag11)
												{
													((WebControl)ctrl2).Enabled = false;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		protected override void SetMenu_HideAllStdBtn()
		{
			this.CurMaster.SetMenu_HideAllStdBtn();
		}

		protected override void SetMenu_OnlyViewEdit()
		{
			this.CurMaster.SetMenu_OnlyViewEdit();
		}

		protected override void SetMenu_OnlyAttach()
		{
			this.CurMaster.SetMenu_OnlyAttach();
		}

		protected override void PlaceUpdateScript()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpScriptPlace.Update();
			}
		}

		protected void PlaceUpdateMenu()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpMenuPlace.Update();
			}
		}

		protected void PlaceUpdateContent()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpContentPlace.Update();
				string adjust = string.Empty;
				foreach (KeyValuePair<int, XGridSetMap> tab in this.ListXGridSetMap)
				{
					adjust = string.Concat(new string[]
					{
						adjust,
						"XGridAdjust('",
						tab.Value.XGrid.ClientID,
						"','",
						tab.Value.XGrid.HeadDivClientId,
						"','",
						tab.Value.XGrid.BodyDivClientId,
						"');"
					});
				}
				base.RegisterScript("XGridAdjust", adjust);
			}
		}

		protected void PlaceUpdateTemplet()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpTempletPlace.Update();
			}
		}

		protected void PlaceUpdateMaintenanceTemplet()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpTempletMaintenancePlace.Update();
			}
		}
	}
}
