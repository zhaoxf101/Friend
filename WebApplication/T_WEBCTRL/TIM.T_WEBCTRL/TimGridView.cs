using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[ParseChildren(true), PersistChildren(false), ToolboxData("<{0}:TimGridView runat=server></{0}:TimGridView>")]
	public class TimGridView : GridView, IPostBackDataHandler
	{
		public delegate void UtoRowDoubleClickEventHandler(object sender, UtoRowDoubleClickEventArgs e);

		private int m_emptyRows = 10;

		private Unit m_headDivHeight = 23;

		private Unit m_bodyDivHeight = 123;

		private string m_headDivClientId = string.Empty;

		private string m_bodyDivClientId = string.Empty;

		private string m_onClientDblClick = string.Empty;

		private static readonly object _UtoRowDoubleClick = new object();

		public event TimGridView.UtoRowDoubleClickEventHandler RowDoubleClick
		{
			add
			{
				base.Events.AddHandler(TimGridView._UtoRowDoubleClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(TimGridView._UtoRowDoubleClick, value);
			}
		}

		public virtual bool CheckBox
		{
			get
			{
				object o = this.ViewState["CheckBox"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["CheckBox"] = value;
			}
		}

		public int EmptyRows
		{
			get
			{
				return this.m_emptyRows;
			}
			set
			{
				this.m_emptyRows = value;
			}
		}

		private Unit HeadDivHeight
		{
			get
			{
				return this.m_headDivHeight;
			}
		}

		public Unit BodyDivHeight
		{
			get
			{
				return this.m_bodyDivHeight;
			}
			set
			{
				this.m_bodyDivHeight = value;
			}
		}

		public string HeadDivClientId
		{
			get
			{
				return this.m_headDivClientId;
			}
		}

		public string BodyDivClientId
		{
			get
			{
				return this.m_bodyDivClientId;
			}
		}

		public string OnClientDblClick
		{
			get
			{
				return this.m_onClientDblClick;
			}
			set
			{
				this.m_onClientDblClick = value;
			}
		}

		public override int SelectedIndex
		{
			get
			{
				return base.SelectedIndex;
			}
			set
			{
				base.SelectedIndex = value;
			}
		}

		public TimGridView()
		{
			this.EnableViewState = false;
			this.ShowHeaderWhenEmpty = true;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.m_headDivHeight = base.HeaderStyle.Height;
			this.m_headDivClientId = this.ClientID + "_HeadDiv";
			this.m_bodyDivClientId = this.ClientID + "_BodyDiv";
		}

		protected override void OnInit(EventArgs e)
		{
			double totalGridWidth = 0.0;
			foreach (DataControlField field in this.Columns)
			{
				totalGridWidth += field.ItemStyle.Width.Value;
				field.HeaderStyle.Width = field.ItemStyle.Width;
			}
			this.Width = Unit.Parse(totalGridWidth.ToString());
			string scirptKey = "TimGridView";
			bool flag = !this.Page.ClientScript.IsClientScriptBlockRegistered(scirptKey);
			if (flag)
			{
				ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), scirptKey, "<script type=\"text/javascript\" src=\"" + this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "TIM.T_WEBCTRL.TimGridView.TimGridView.js") + "\"></script>", false);
			}
			base.OnInit(e);
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_HeadDiv");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "hidden");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_BodyDiv");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.BodyDivHeight.ToString());
			writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
			writer.AddAttribute("onscroll", this.ClientID + "_HeadDiv.scrollLeft=scrollLeft");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.RenderContents(writer);
			writer.RenderEndTag();
		}

		protected override void OnPreRender(EventArgs e)
		{
			this.Page.ClientScript.RegisterHiddenField(this.ClientID + "_selectedIndex", this.SelectedIndex.ToString());
			this.Page.RegisterRequiresPostBack(this);
			base.OnPreRender(e);
			this.Page.ClientScript.GetPostBackEventReference(this, "");
		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Attributes.Add("style", "table-layout:fixed");
			base.Style["word-break"] = "break-all";
			base.Attributes.Add("onMouseOver", "showTips(event);");
			base.Attributes.Add("selectedIndex", this.SelectedIndex.ToString());
			base.Attributes.Add("selectedRow_bgColor", ColorTranslator.ToHtml(base.SelectedRowStyle.BackColor));
			base.Attributes.Add("selectedRow_foreColor", ColorTranslator.ToHtml(base.SelectedRowStyle.ForeColor));
			base.Render(writer);
			ScriptManager.RegisterStartupScript(this, base.GetType(), this.ClientID + "_Initialize", string.Concat(new string[]
			{
				"<script type=\"text/javascript\" >gvObj_",
				this.ClientID,
				" = new TimGridView('",
				this.ClientID,
				"');</script>"
			}), false);
			ScriptManager.RegisterStartupScript(this, base.GetType(), this.ClientID + "_SetSelectedIndex", string.Format("document.getElementById('{0}_selectedIndex').value='{1}';", this.ClientID, this.SelectedIndex), true);
		}

		private void RenderEmptyGrid(HtmlTextWriter writer)
		{
			bool flag = this.Rows.Count == 0 && !base.DesignMode && this.Visible;
			if (flag)
			{
				Table EmptyTable = new Table();
				EmptyTable.BorderColor = this.BorderColor;
				EmptyTable.BorderWidth = this.BorderWidth;
				EmptyTable.Font.Name = this.Font.Name;
				EmptyTable.Font.Size = this.Font.Size;
				EmptyTable.CellPadding = 0;
				EmptyTable.CellSpacing = 0;
				EmptyTable.Attributes["cursor"] = "default";
				EmptyTable.Attributes["SelectedIndex"] = "-1";
				EmptyTable.Style["display"] = base.Style["display"];
				EmptyTable.Style["table-layout"] = "fixed";
				EmptyTable.Attributes["class"] = "TimGridView";
				EmptyTable.ID = this.ClientID;
				EmptyTable.Width = this.Width;
				EmptyTable.BackColor = this.BackColor;
				EmptyTable.CellPadding = this.CellPadding;
				EmptyTable.GridLines = this.GridLines;
				TableHeaderRow HeadRow = new TableHeaderRow();
				HeadRow.Height = base.HeaderStyle.Height;
				HeadRow.BackColor = base.HeaderStyle.BackColor;
				HeadRow.ForeColor = base.HeaderStyle.ForeColor;
				HeadRow.Font.Name = base.HeaderStyle.Font.Name;
				HeadRow.Font.Size = base.HeaderStyle.Font.Size;
				HeadRow.HorizontalAlign = base.HeaderStyle.HorizontalAlign;
				HeadRow.VerticalAlign = base.HeaderStyle.VerticalAlign;
				bool flag2 = this.Columns.Count > 0;
				if (flag2)
				{
					for (int i = 0; i < this.Columns.Count; i++)
					{
						bool visible = this.Columns[i].Visible;
						if (visible)
						{
							TableHeaderCell Cell = new TableHeaderCell();
							Cell.Text = this.Columns[i].HeaderText;
							Cell.BackColor = base.HeaderStyle.BackColor;
							Cell.Width = this.Columns[i].ItemStyle.Width;
							HeadRow.Cells.Add(Cell);
						}
					}
				}
				EmptyTable.Rows.Add(HeadRow);
				EmptyTable.RenderControl(writer);
			}
		}

		protected override void OnRowDataBound(GridViewRowEventArgs e)
		{
			bool flag = e.Row.RowType == DataControlRowType.DataRow;
			if (flag)
			{
				e.Row.Attributes.Add("id", string.Format("{0}_row{1}", this.ClientID, e.Row.RowIndex));
				e.Row.Attributes.Add("rowIndex", e.Row.RowIndex.ToString());
				e.Row.Attributes.Add("onclick", "checkedRow(this,event);");
				TimGridView.UtoRowDoubleClickEventHandler Handler = (TimGridView.UtoRowDoubleClickEventHandler)base.Events[TimGridView._UtoRowDoubleClick];
				bool flag2 = Handler != null;
				if (flag2)
				{
					e.Row.Attributes.Add("ondblclick", this.Page.ClientScript.GetPostBackEventReference(this, string.Format("RowDblClick_{0}", e.Row.RowIndex)));
				}
				else
				{
					bool flag3 = !string.IsNullOrEmpty(this.OnClientDblClick);
					if (flag3)
					{
						e.Row.Attributes.Add("ondblclick", this.OnClientDblClick);
					}
				}
			}
			bool flag4 = !base.DesignMode;
			if (flag4)
			{
				this.FormatCellText(e);
			}
			base.OnRowDataBound(e);
		}

		private string GetUrlString(string url)
		{
			return url.StartsWith("~") ? this.Page.ResolveClientUrl(url) : url;
		}

		private void FormatCellText(GridViewRowEventArgs e)
		{
			bool flag = e.Row.RowType == DataControlRowType.DataRow;
			if (flag)
			{
				int i = 0;
				while (i < this.Columns.Count)
				{
					bool flag2 = this.Columns[i] is TimBoundField;
					if (flag2)
					{
						TimBoundField bf = this.Columns[i] as TimBoundField;
						TableCell cell = e.Row.Cells[i];
						string checkedImgPath = "~/Images/Tim/CheckBoxChecked.gif";
						string unCheckedImgPath = "~/Images/Tim/CheckBoxUnChecked.gif";
						bool flag3 = string.IsNullOrWhiteSpace(cell.Text) || cell.Text == "&nbsp;";
						if (flag3)
						{
							bool flag4 = bf.Mode == BoundFieldMode.CheckBox;
							if (flag4)
							{
								cell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
								cell.Text = string.Format("<img src=\"{0}\" style=\"vertical-align:middle;\"/>", this.GetUrlString(unCheckedImgPath));
							}
							else
							{
								cell.Text = string.Empty;
							}
						}
						else
						{
							bool wordBreak = bf.WordBreak;
							if (wordBreak)
							{
								cell.Attributes["class"] = "wb";
							}
							else
							{
								bool flag5 = bf.Mode == BoundFieldMode.String && bf.Tips;
								if (flag5)
								{
									cell.Attributes["class"] = "tips";
								}
								else
								{
									bool flag6 = bf.Mode == BoundFieldMode.Date;
									if (flag6)
									{
										DateTime cellValue;
										bool flag7 = DateTime.TryParse(cell.Text.Trim(), out cellValue);
										if (flag7)
										{
											cell.Text = cellValue.ToString("yyyy-MM-dd");
										}
									}
									else
									{
										bool flag8 = bf.Mode == BoundFieldMode.DateTime;
										if (flag8)
										{
											DateTime cellValue2;
											bool flag9 = DateTime.TryParse(cell.Text.Trim(), out cellValue2);
											if (flag9)
											{
												cell.Text = cellValue2.ToString("yyyy-MM-dd HH:mm:ss");
											}
										}
										else
										{
											bool flag10 = bf.Mode == BoundFieldMode.Time;
											if (flag10)
											{
												DateTime cellValue3;
												bool flag11 = DateTime.TryParse(cell.Text.Trim(), out cellValue3);
												if (flag11)
												{
													cell.Text = cellValue3.ToString("HH:mm");
												}
											}
											else
											{
												bool flag12 = bf.Mode == BoundFieldMode.Numeric;
												if (flag12)
												{
													bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
													double cellValue4 = 0.0;
													double.TryParse(cell.Text.Trim(), out cellValue4);
													bool flag13 = !bf.ShowZero && cellValue4 == 0.0;
													if (flag13)
													{
														cell.Text = "";
													}
													else
													{
														cell.Text = cellValue4.ToString(string.Format("N{0}", bf.DecimalPlaces));
													}
												}
												else
												{
													bool flag14 = bf.Mode == BoundFieldMode.CheckBox;
													if (flag14)
													{
														cell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
														bool flag15 = cell.Text.Trim() == "Y";
														if (flag15)
														{
															cell.Text = string.Format("<img src=\"{0}\" style=\"vertical-align:middle;\"'/>", this.GetUrlString(checkedImgPath));
														}
														else
														{
															bool flag16 = cell.Text.Trim() == "N";
															if (flag16)
															{
																cell.Text = string.Format("<img src=\"{0}\" style=\"vertical-align:middle;\"/>", this.GetUrlString(unCheckedImgPath));
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
					IL_348:
					i++;
					continue;
					goto IL_348;
				}
			}
		}

		protected virtual void OnRowDoubleClick(UtoRowDoubleClickEventArgs e)
		{
			TimGridView.UtoRowDoubleClickEventHandler Handler = (TimGridView.UtoRowDoubleClickEventHandler)base.Events[TimGridView._UtoRowDoubleClick];
			bool flag = Handler != null;
			if (flag)
			{
				Handler(this, e);
			}
		}

		protected override void RaisePostBackEvent(string EventArgument)
		{
			bool flag = EventArgument.StartsWith("RowDblClick_");
			if (flag)
			{
				int RowDoubleClicked = int.Parse(EventArgument.Substring(12));
				this.SelectedIndex = RowDoubleClicked;
				this.OnRowDoubleClick(null);
			}
		}

		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.SelectedIndex = Convert.ToInt32(postCollection[this.ClientID + "_selectedIndex"]);
			return true;
		}

		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
		}
	}
}
