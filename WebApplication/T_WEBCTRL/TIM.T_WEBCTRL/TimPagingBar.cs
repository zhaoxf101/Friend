using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[DefaultEvent("PageChanged"), DefaultProperty("PageSize"), Designer(typeof(TimPagingBarDesigner)), ParseChildren(false), PersistChildren(false), ToolboxData("<{0}:TimPagingBar runat=server></{0}:TimPagingBar>"), ANPDescription("desc_AspNetPager")]
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TimPagingBar : Panel, INamingContainer, IPostBackEventHandler, IPostBackDataHandler
	{
		private enum NavigationButton : byte
		{
			First,
			Prev,
			Next,
			Last,
			Refresh
		}

		private string inputPageIndex;

		private string currentUrl;

		private string queryString;

		private TimPagingBar cloneFrom;

		private static readonly object EventPageChanging = new object();

		private static readonly object EventPageChanged = new object();

		private const string scriptRegItemName = "IsANPScriptsRegistered";

		public event PageChangingEventHandler PageChanging
		{
			add
			{
				base.Events.AddHandler(TimPagingBar.EventPageChanging, value);
			}
			remove
			{
				base.Events.RemoveHandler(TimPagingBar.EventPageChanging, value);
			}
		}

		public event EventHandler PageChanged
		{
			add
			{
				base.Events.AddHandler(TimPagingBar.EventPageChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(TimPagingBar.EventPageChanged, value);
			}
		}

		[Browsable(true), DefaultValue(false), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_ShowNavigationToolTip")]
		public bool ShowNavigationToolTip
		{
			get
			{
				bool flag = this.cloneFrom != null;
				bool result;
				if (flag)
				{
					result = this.cloneFrom.ShowNavigationToolTip;
				}
				else
				{
					object obj = this.ViewState["ShowNvToolTip"];
					result = (obj != null && (bool)obj);
				}
				return result;
			}
			set
			{
				this.ViewState["ShowNvToolTip"] = value;
			}
		}

		[Browsable(true), Themeable(true), ANPCategory("cat_Navigation"), ANPDefaultValue("def_NavigationToolTipTextFormatString"), ANPDescription("desc_NavigationToolTipTextFormatString")]
		public string NavigationToolTipTextFormatString
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.NavigationToolTipTextFormatString;
				}
				else
				{
					object obj = this.ViewState["NvToolTipFormatString"];
					bool flag2 = obj == null;
					if (flag2)
					{
						bool showNavigationToolTip = this.ShowNavigationToolTip;
						if (showNavigationToolTip)
						{
							result = SR.GetString("def_NavigationToolTipTextFormatString");
						}
						else
						{
							result = null;
						}
					}
					else
					{
						result = (string)obj;
					}
				}
				return result;
			}
			set
			{
				string tip = value;
				bool flag = tip.Trim().Length < 1 && tip.IndexOf("{0}") < 0;
				if (flag)
				{
					tip = "{0}";
				}
				this.ViewState["NvToolTipFormatString"] = tip;
			}
		}

		[Browsable(true), DefaultValue(""), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_NBTFormatString")]
		public string NumericButtonTextFormatString
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.NumericButtonTextFormatString;
				}
				else
				{
					object obj = this.ViewState["NumericButtonTextFormatString"];
					result = ((obj == null) ? string.Empty : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["NumericButtonTextFormatString"] = value;
			}
		}

		[Browsable(true), DefaultValue(""), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_CPBTextFormatString")]
		public string CurrentPageButtonTextFormatString
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.CurrentPageButtonTextFormatString;
				}
				else
				{
					object obj = this.ViewState["CurrentPageButtonTextFormatString"];
					result = ((obj == null) ? this.NumericButtonTextFormatString : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["CurrentPageButtonTextFormatString"] = value;
			}
		}

		[Browsable(true), DefaultValue(PagingButtonType.Text), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_PagingButtonType")]
		public PagingButtonType PagingButtonType
		{
			get
			{
				bool flag = this.cloneFrom != null;
				PagingButtonType result;
				if (flag)
				{
					result = this.cloneFrom.PagingButtonType;
				}
				else
				{
					object obj = this.ViewState["PagingButtonType"];
					result = ((obj == null) ? PagingButtonType.Text : ((PagingButtonType)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["PagingButtonType"] = value;
			}
		}

		[Browsable(true), DefaultValue(PagingButtonType.Text), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_NumericButtonType")]
		public PagingButtonType NumericButtonType
		{
			get
			{
				bool flag = this.cloneFrom != null;
				PagingButtonType result;
				if (flag)
				{
					result = this.cloneFrom.NumericButtonType;
				}
				else
				{
					object obj = this.ViewState["NumericButtonType"];
					result = ((obj == null) ? this.PagingButtonType : ((PagingButtonType)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["NumericButtonType"] = value;
			}
		}

		[Browsable(true), DefaultValue(PagingButtonLayoutType.None), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_PagingButtonLayoutType")]
		public PagingButtonLayoutType PagingButtonLayoutType
		{
			get
			{
				bool flag = this.cloneFrom != null;
				PagingButtonLayoutType result;
				if (flag)
				{
					result = this.cloneFrom.PagingButtonLayoutType;
				}
				else
				{
					object obj = this.ViewState["PagingButtonLayoutType"];
					result = ((obj == null) ? PagingButtonLayoutType.None : ((PagingButtonLayoutType)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["PagingButtonLayoutType"] = value;
			}
		}

		[Browsable(true), DefaultValue(PagingButtonPosition.Fixed), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_CurrentPageButtonPosition")]
		public PagingButtonPosition CurrentPageButtonPosition
		{
			get
			{
				bool flag = this.cloneFrom != null;
				PagingButtonPosition result;
				if (flag)
				{
					result = this.cloneFrom.CurrentPageButtonPosition;
				}
				else
				{
					object obj = this.ViewState["CurrentPageButtonPosition"];
					result = ((obj == null) ? PagingButtonPosition.Fixed : ((PagingButtonPosition)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["CurrentPageButtonPosition"] = value;
			}
		}

		[Browsable(true), DefaultValue(NavigationButtonPosition.BothSides), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_NavigationButtonsPosition")]
		public NavigationButtonPosition NavigationButtonsPosition
		{
			get
			{
				bool flag = this.cloneFrom != null;
				NavigationButtonPosition result;
				if (flag)
				{
					result = this.cloneFrom.NavigationButtonsPosition;
				}
				else
				{
					object obj = this.ViewState["NavigationButtonsPosition"];
					result = ((obj == null) ? NavigationButtonPosition.BothSides : ((NavigationButtonPosition)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["NavigationButtonsPosition"] = value;
			}
		}

		[Browsable(true), DefaultValue(PagingButtonType.Text), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_NavigationButtonType")]
		public PagingButtonType NavigationButtonType
		{
			get
			{
				bool flag = this.cloneFrom != null;
				PagingButtonType result;
				if (flag)
				{
					result = this.cloneFrom.NavigationButtonType;
				}
				else
				{
					object obj = this.ViewState["NavigationButtonType"];
					result = ((obj == null) ? this.PagingButtonType : ((PagingButtonType)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["NavigationButtonType"] = value;
			}
		}

		[Browsable(true), DefaultValue(""), TypeConverter(typeof(TargetConverter)), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_UrlPagingTarget")]
		public string UrlPagingTarget
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.UrlPagingTarget;
				}
				else
				{
					result = (string)this.ViewState["UrlPagingTarget"];
				}
				return result;
			}
			set
			{
				this.ViewState["UrlPagingTarget"] = value;
			}
		}

		[Browsable(true), DefaultValue(PagingButtonType.Text), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_MoreButtonType")]
		public PagingButtonType MoreButtonType
		{
			get
			{
				bool flag = this.cloneFrom != null;
				PagingButtonType result;
				if (flag)
				{
					result = this.cloneFrom.MoreButtonType;
				}
				else
				{
					object obj = this.ViewState["MoreButtonType"];
					result = ((obj == null) ? this.PagingButtonType : ((PagingButtonType)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["MoreButtonType"] = value;
			}
		}

		[Browsable(true), DefaultValue(typeof(Unit), "5px"), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_PagingButtonSpacing")]
		public Unit PagingButtonSpacing
		{
			get
			{
				bool flag = this.cloneFrom != null;
				Unit result;
				if (flag)
				{
					result = this.cloneFrom.PagingButtonSpacing;
				}
				else
				{
					object obj = this.ViewState["PagingButtonSpacing"];
					result = ((obj == null) ? Unit.Pixel(5) : Unit.Parse(obj.ToString()));
				}
				return result;
			}
			set
			{
				this.ViewState["PagingButtonSpacing"] = value;
			}
		}

		[Browsable(true), DefaultValue(true), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_ShowFirstLast")]
		public bool ShowFirstLast
		{
			get
			{
				bool flag = this.cloneFrom != null;
				bool result;
				if (flag)
				{
					result = this.cloneFrom.ShowFirstLast;
				}
				else
				{
					object obj = this.ViewState["ShowFirstLast"];
					result = (obj == null || (bool)obj);
				}
				return result;
			}
			set
			{
				this.ViewState["ShowFirstLast"] = value;
			}
		}

		[Browsable(true), DefaultValue(true), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_ShowPrevNext")]
		public bool ShowPrevNext
		{
			get
			{
				bool flag = this.cloneFrom != null;
				bool result;
				if (flag)
				{
					result = this.cloneFrom.ShowPrevNext;
				}
				else
				{
					object obj = this.ViewState["ShowPrevNext"];
					result = (obj == null || (bool)obj);
				}
				return result;
			}
			set
			{
				this.ViewState["ShowPrevNext"] = value;
			}
		}

		[Browsable(true), DefaultValue(true), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_ShowPageIndex")]
		public bool ShowPageIndex
		{
			get
			{
				bool flag = this.cloneFrom != null;
				bool result;
				if (flag)
				{
					result = this.cloneFrom.ShowPageIndex;
				}
				else
				{
					object obj = this.ViewState["ShowPageIndex"];
					result = (obj != null && (bool)obj);
				}
				return result;
			}
			set
			{
				this.ViewState["ShowPageIndex"] = value;
			}
		}

		[Browsable(true), DefaultValue(true), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_ShowMoreButtons")]
		public bool ShowMoreButtons
		{
			get
			{
				bool flag = this.cloneFrom != null;
				bool result;
				if (flag)
				{
					result = this.cloneFrom.ShowMoreButtons;
				}
				else
				{
					object obj = this.ViewState["ShowMoreButtons"];
					result = (obj != null && (bool)obj);
				}
				return result;
			}
			set
			{
				this.ViewState["ShowMoreButtons"] = value;
			}
		}

		[Browsable(true), DefaultValue("&lt;&lt;"), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_FirstPageText")]
		public string FirstPageText
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.FirstPageText;
				}
				else
				{
					object obj = this.ViewState["FirstPageText"];
					result = ((obj == null) ? "首页" : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["FirstPageText"] = value;
			}
		}

		[Browsable(true), DefaultValue("&lt;"), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_PrevPageText")]
		public string PrevPageText
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.PrevPageText;
				}
				else
				{
					object obj = this.ViewState["PrevPageText"];
					result = ((obj == null) ? "上一页" : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["PrevPageText"] = value;
			}
		}

		[Browsable(true), DefaultValue("&gt;"), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_NextPageText")]
		public string NextPageText
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.NextPageText;
				}
				else
				{
					object obj = this.ViewState["NextPageText"];
					result = ((obj == null) ? "下一页" : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["NextPageText"] = value;
			}
		}

		[Browsable(true), DefaultValue("&gt;&gt;"), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_LastPageText")]
		public string LastPageText
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.LastPageText;
				}
				else
				{
					object obj = this.ViewState["LastPageText"];
					result = ((obj == null) ? "尾页" : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["LastPageText"] = value;
			}
		}

		[Browsable(true), DefaultValue(10), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_NumericButtonCount")]
		public int NumericButtonCount
		{
			get
			{
				bool flag = this.cloneFrom != null;
				int result;
				if (flag)
				{
					result = this.cloneFrom.NumericButtonCount;
				}
				else
				{
					object obj = this.ViewState["NumericButtonCount"];
					result = ((obj == null) ? 10 : ((int)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["NumericButtonCount"] = value;
			}
		}

		[Browsable(true), DefaultValue(true), Themeable(true), ANPCategory("cat_Navigation"), ANPDescription("desc_ShowDisabledButtons")]
		public bool ShowDisabledButtons
		{
			get
			{
				bool flag = this.cloneFrom != null;
				bool result;
				if (flag)
				{
					result = this.cloneFrom.ShowDisabledButtons;
				}
				else
				{
					object obj = this.ViewState["ShowDisabledButtons"];
					result = (obj == null || (bool)obj);
				}
				return result;
			}
			set
			{
				this.ViewState["ShowDisabledButtons"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), ANPDescription("desc_ImagePath")]
		public string ImagePath
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.ImagePath;
				}
				else
				{
					string imgPath = (string)this.ViewState["ImagePath"];
					bool flag2 = imgPath != null;
					if (flag2)
					{
						imgPath = base.ResolveUrl(imgPath);
					}
					result = imgPath;
				}
				return result;
			}
			set
			{
				string imgPath = value.Trim().Replace("\\", "/");
				this.ViewState["ImagePath"] = (imgPath.EndsWith("/") ? imgPath : (imgPath + "/"));
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(".gif"), Themeable(true), ANPDescription("desc_ButtonImageExtension")]
		public string ButtonImageExtension
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.ButtonImageExtension;
				}
				else
				{
					object obj = this.ViewState["ButtonImageExtension"];
					result = ((obj == null) ? ".gif" : ((string)obj));
				}
				return result;
			}
			set
			{
				string ext = value.Trim();
				this.ViewState["ButtonImageExtension"] = (ext.StartsWith(".") ? ext : ("." + ext));
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), Themeable(true), ANPDescription("desc_ButtonImageNameExtension")]
		public string ButtonImageNameExtension
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.ButtonImageNameExtension;
				}
				else
				{
					object obj = this.ViewState["ButtonImageNameExtension"];
					result = ((obj == null) ? null : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["ButtonImageNameExtension"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), Themeable(true), ANPDescription("desc_CpiButtonImageNameExtension")]
		public string CpiButtonImageNameExtension
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.CpiButtonImageNameExtension;
				}
				else
				{
					object obj = this.ViewState["CpiButtonImageNameExtension"];
					result = ((obj == null) ? this.ButtonImageNameExtension : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["CpiButtonImageNameExtension"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), Themeable(true), ANPDescription("desc_DisabledButtonImageNameExtension")]
		public string DisabledButtonImageNameExtension
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.DisabledButtonImageNameExtension;
				}
				else
				{
					object obj = this.ViewState["DisabledButtonImageNameExtension"];
					result = ((obj == null) ? this.ButtonImageNameExtension : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["DisabledButtonImageNameExtension"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(ImageAlign.NotSet), ANPDescription("desc_ButtonImageAlign")]
		public ImageAlign ButtonImageAlign
		{
			get
			{
				bool flag = this.cloneFrom != null;
				ImageAlign result;
				if (flag)
				{
					result = this.cloneFrom.ButtonImageAlign;
				}
				else
				{
					object obj = this.ViewState["ButtonImageAlign"];
					result = ((obj == null) ? ImageAlign.NotSet : ((ImageAlign)obj));
				}
				return result;
			}
			set
			{
				bool flag = value != ImageAlign.Right && value != ImageAlign.Left;
				if (flag)
				{
					this.ViewState["ButtonImageAlign"] = value;
				}
			}
		}

		[Browsable(true), DefaultValue(false), ANPCategory("cat_Paging"), ANPDescription("desc_UrlPaging")]
		public bool UrlPaging
		{
			get
			{
				bool flag = this.cloneFrom != null;
				bool result;
				if (flag)
				{
					result = this.cloneFrom.UrlPaging;
				}
				else
				{
					object obj = this.ViewState["UrlPaging"];
					result = (obj != null && (bool)obj);
				}
				return result;
			}
			set
			{
				this.ViewState["UrlPaging"] = value;
			}
		}

		[Browsable(true), DefaultValue("page"), ANPCategory("cat_Paging"), ANPDescription("desc_UrlPageIndexName")]
		public string UrlPageIndexName
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.UrlPageIndexName;
				}
				else
				{
					object obj = this.ViewState["UrlPageIndexName"];
					result = ((obj == null) ? "page" : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["UrlPageIndexName"] = value;
			}
		}

		[Browsable(true), DefaultValue(""), ANPCategory("cat_Paging"), ANPDescription("desc_UrlPageSizeName")]
		public string UrlPageSizeName
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.UrlPageSizeName;
				}
				else
				{
					result = (string)this.ViewState["UrlPageSizeName"];
				}
				return result;
			}
			set
			{
				this.ViewState["UrlPageSizeName"] = value;
			}
		}

		[Browsable(true), DefaultValue(false), ANPCategory("cat_Paging"), ANPDescription("desc_ReverseUrlPageIndex")]
		public bool ReverseUrlPageIndex
		{
			get
			{
				bool flag = this.cloneFrom != null;
				bool result;
				if (flag)
				{
					result = this.cloneFrom.ReverseUrlPageIndex;
				}
				else
				{
					object obj = this.ViewState["ReverseUrlPageIndex"];
					result = (obj != null && (bool)obj);
				}
				return result;
			}
			set
			{
				this.ViewState["ReverseUrlPageIndex"] = value;
			}
		}

		[Browsable(true), DefaultValue(1), ANPCategory("cat_Paging"), ANPDescription("desc_CurrentPageIndex")]
		public int CurrentPageIndex
		{
			get
			{
				bool flag = this.cloneFrom != null;
				int result;
				if (flag)
				{
					result = this.cloneFrom.CurrentPageIndex;
				}
				else
				{
					object cpage = this.ViewState["CurrentPageIndex"];
					int pindex = (cpage == null) ? 1 : ((int)cpage);
					bool flag2 = pindex > this.PageCount && this.PageCount > 0;
					if (flag2)
					{
						result = this.PageCount;
					}
					else
					{
						bool flag3 = pindex < 1;
						if (flag3)
						{
							result = 1;
						}
						else
						{
							result = pindex;
						}
					}
				}
				return result;
			}
			set
			{
				int cpage = value;
				bool flag = cpage < 1;
				if (flag)
				{
					cpage = 1;
				}
				else
				{
					bool flag2 = cpage > this.PageCount;
					if (flag2)
					{
						cpage = this.PageCount;
					}
				}
				this.ViewState["CurrentPageIndex"] = cpage;
			}
		}

		[Browsable(false), Category("Data"), DefaultValue(0), ANPDescription("desc_RecordCount")]
		public int RecordCount
		{
			get
			{
				bool flag = this.cloneFrom != null;
				int result;
				if (flag)
				{
					result = this.cloneFrom.RecordCount;
				}
				else
				{
					object obj = this.ViewState["Recordcount"];
					result = ((obj == null) ? 0 : ((int)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["Recordcount"] = value;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PagesRemain
		{
			get
			{
				return this.PageCount - this.CurrentPageIndex;
			}
		}

		[Browsable(true), DefaultValue(10), ANPCategory("cat_Paging"), ANPDescription("desc_PageSize")]
		public int PageSize
		{
			get
			{
				bool flag = !string.IsNullOrEmpty(this.UrlPageSizeName) && !base.DesignMode;
				int result;
				if (flag)
				{
					int pageSize;
					bool flag2 = int.TryParse(this.Page.Request.QueryString[this.UrlPageSizeName], out pageSize);
					if (flag2)
					{
						bool flag3 = pageSize > 0;
						if (flag3)
						{
							result = pageSize;
							return result;
						}
					}
				}
				bool flag4 = this.cloneFrom != null;
				if (flag4)
				{
					result = this.cloneFrom.PageSize;
				}
				else
				{
					object obj = this.ViewState["PageSize"];
					result = ((obj == null) ? 10 : ((int)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["PageSize"] = value;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int RecordsRemain
		{
			get
			{
				bool flag = this.CurrentPageIndex < this.PageCount;
				int result;
				if (flag)
				{
					result = this.RecordCount - this.CurrentPageIndex * this.PageSize;
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int StartRecordIndex
		{
			get
			{
				return (this.CurrentPageIndex - 1) * this.PageSize + 1;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int EndRecordIndex
		{
			get
			{
				return this.RecordCount - this.RecordsRemain;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PageCount
		{
			get
			{
				bool flag = this.RecordCount == 0;
				int result;
				if (flag)
				{
					result = 1;
				}
				else
				{
					result = (int)Math.Ceiling((double)this.RecordCount / (double)this.PageSize);
				}
				return result;
			}
		}

		[Browsable(true), DefaultValue(ShowPageIndexBox.Auto), ANPCategory("cat_PageIndexBox"), ANPDescription("desc_ShowPageIndexBox")]
		public ShowPageIndexBox ShowPageIndexBox
		{
			get
			{
				bool flag = this.cloneFrom != null;
				ShowPageIndexBox result;
				if (flag)
				{
					result = this.cloneFrom.ShowPageIndexBox;
				}
				else
				{
					object obj = this.ViewState["ShowPageIndexBox"];
					result = ((obj == null) ? ShowPageIndexBox.Auto : ((ShowPageIndexBox)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["ShowPageIndexBox"] = value;
			}
		}

		[Browsable(true), DefaultValue(PageIndexBoxType.TextBox), ANPCategory("cat_PageIndexBox"), ANPDescription("desc_PageIndexBoxType")]
		public PageIndexBoxType PageIndexBoxType
		{
			get
			{
				bool flag = this.cloneFrom != null;
				PageIndexBoxType result;
				if (flag)
				{
					result = this.cloneFrom.PageIndexBoxType;
				}
				else
				{
					object obj = this.ViewState["PageIndexBoxType"];
					result = ((obj == null) ? PageIndexBoxType.TextBox : ((PageIndexBoxType)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["PageIndexBoxType"] = value;
			}
		}

		[Browsable(true), DefaultValue(null), ANPCategory("cat_PageIndexBox"), ANPDescription("desc_PageIndexBoxClass")]
		public string PageIndexBoxClass
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.PageIndexBoxClass;
				}
				else
				{
					object obj = this.ViewState["PageIndexBoxClass"];
					result = ((obj == null) ? null : ((string)obj));
				}
				return result;
			}
			set
			{
				bool flag = value.Trim().Length > 0;
				if (flag)
				{
					this.ViewState["PageIndexBoxClass"] = value;
				}
			}
		}

		[Browsable(true), DefaultValue(null), ANPCategory("cat_PageIndexBox"), ANPDescription("desc_PageIndexBoxStyle")]
		public string PageIndexBoxStyle
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.PageIndexBoxStyle;
				}
				else
				{
					object obj = this.ViewState["PageIndexBoxStyle"];
					result = ((obj == null) ? null : ((string)obj));
				}
				return result;
			}
			set
			{
				bool flag = value.Trim().Length > 0;
				if (flag)
				{
					this.ViewState["PageIndexBoxStyle"] = value;
				}
			}
		}

		[Browsable(true), DefaultValue(null), Themeable(true), ANPCategory("cat_PageIndexBox"), ANPDescription("desc_TextBeforePageIndexBox")]
		public string TextBeforePageIndexBox
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.TextBeforePageIndexBox;
				}
				else
				{
					object obj = this.ViewState["TextBeforePageIndexBox"];
					result = ((obj == null) ? "转到第" : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["TextBeforePageIndexBox"] = value;
			}
		}

		[Browsable(true), DefaultValue(null), Themeable(true), ANPCategory("cat_PageIndexBox"), ANPDescription("desc_TextAfterPageIndexBox")]
		public string TextAfterPageIndexBox
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.TextAfterPageIndexBox;
				}
				else
				{
					object obj = this.ViewState["TextAfterPageIndexBox"];
					result = ((obj == null) ? "页/共%PageCount%页%RecordCount%条" : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["TextAfterPageIndexBox"] = value;
			}
		}

		[Browsable(true), DefaultValue("go"), Themeable(true), ANPCategory("cat_PageIndexBox"), ANPDescription("desc_SubmitButtonText")]
		public string SubmitButtonText
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.SubmitButtonText;
				}
				else
				{
					object obj = this.ViewState["SubmitButtonText"];
					result = ((obj == null) ? "go" : ((string)obj));
				}
				return result;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					value = "go";
				}
				this.ViewState["SubmitButtonText"] = value;
			}
		}

		[Browsable(true), DefaultValue(null), ANPCategory("cat_PageIndexBox"), ANPDescription("desc_SubmitButtonClass")]
		public string SubmitButtonClass
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.SubmitButtonClass;
				}
				else
				{
					result = (string)this.ViewState["SubmitButtonClass"];
				}
				return result;
			}
			set
			{
				this.ViewState["SubmitButtonClass"] = value;
			}
		}

		[Browsable(true), DefaultValue(null), ANPCategory("cat_PageIndexBox"), ANPDescription("desc_SubmitButtonStyle")]
		public string SubmitButtonStyle
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.SubmitButtonStyle;
				}
				else
				{
					result = (string)this.ViewState["SubmitButtonStyle"];
				}
				return result;
			}
			set
			{
				this.ViewState["SubmitButtonStyle"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(""), ANPDescription("desc_SubmitButtonImageUrl")]
		public string SubmitButtonImageUrl
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.SubmitButtonImageUrl;
				}
				else
				{
					result = (string)this.ViewState["SubmitButtonImageUrl"];
				}
				return result;
			}
			set
			{
				this.ViewState["SubmitButtonImageUrl"] = value;
			}
		}

		[Browsable(true), DefaultValue(30), Themeable(true), ANPCategory("cat_PageIndexBox"), ANPDescription("desc_ShowBoxThreshold")]
		public int ShowBoxThreshold
		{
			get
			{
				bool flag = this.cloneFrom != null;
				int result;
				if (flag)
				{
					result = this.cloneFrom.ShowBoxThreshold;
				}
				else
				{
					object obj = this.ViewState["ShowBoxThreshold"];
					result = ((obj == null) ? 0 : ((int)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["ShowBoxThreshold"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(ShowCustomInfoSection.Never), Themeable(true), ANPDescription("desc_ShowCustomInfoSection")]
		public ShowCustomInfoSection ShowCustomInfoSection
		{
			get
			{
				bool flag = this.cloneFrom != null;
				ShowCustomInfoSection result;
				if (flag)
				{
					result = this.cloneFrom.ShowCustomInfoSection;
				}
				else
				{
					object obj = this.ViewState["ShowCustomInfoSection"];
					result = ((obj == null) ? ShowCustomInfoSection.Right : ((ShowCustomInfoSection)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["ShowCustomInfoSection"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(HorizontalAlign.NotSet), ANPDescription("desc_CustomInfoTextAlign")]
		public HorizontalAlign CustomInfoTextAlign
		{
			get
			{
				bool flag = this.cloneFrom != null;
				HorizontalAlign result;
				if (flag)
				{
					result = this.cloneFrom.CustomInfoTextAlign;
				}
				else
				{
					object obj = this.ViewState["CustomInfoTextAlign"];
					result = ((obj == null) ? HorizontalAlign.NotSet : ((HorizontalAlign)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["CustomInfoTextAlign"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(typeof(Unit), "40%"), ANPDescription("desc_CustomInfoSectionWidth")]
		public Unit CustomInfoSectionWidth
		{
			get
			{
				bool flag = this.cloneFrom != null;
				Unit result;
				if (flag)
				{
					result = this.cloneFrom.CustomInfoSectionWidth;
				}
				else
				{
					object obj = this.ViewState["CustomInfoSectionWidth"];
					result = ((obj == null) ? Unit.Percentage(40.0) : ((Unit)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["CustomInfoSectionWidth"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), ANPDescription("desc_CustomInfoClass")]
		public string CustomInfoClass
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.CustomInfoClass;
				}
				else
				{
					object obj = this.ViewState["CustomInfoClass"];
					result = ((obj == null) ? this.CssClass : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["CustomInfoClass"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), ANPDescription("desc_CustomInfoStyle")]
		public string CustomInfoStyle
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.CustomInfoStyle;
				}
				else
				{
					object obj = this.ViewState["CustomInfoStyle"];
					result = ((obj == null) ? base.Style.Value : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["CustomInfoStyle"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue("Page %CurrentPageIndex% of %PageCount%"), Themeable(true), ANPDescription("desc_CustomInfoHTML")]
		public string CustomInfoHTML
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.CustomInfoHTML;
				}
				else
				{
					object obj = this.ViewState["CustomInfoText"];
					result = ((obj == null) ? "总记录数：%RecordCount%，总页数：%PageCount%，当前为第%CurrentPageIndex%页" : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["CustomInfoText"] = value;
			}
		}

		public override string CssClass
		{
			get
			{
				return "TimPagingBar";
			}
			set
			{
				base.CssClass = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), ANPDescription("desc_CurrentPageButtonStyle")]
		public string CurrentPageButtonStyle
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.CurrentPageButtonStyle;
				}
				else
				{
					object obj = this.ViewState["CPBStyle"];
					result = ((obj == null) ? null : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["CPBStyle"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), ANPDescription("desc_CurrentPageButtonClass")]
		public string CurrentPageButtonClass
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.CurrentPageButtonClass;
				}
				else
				{
					object obj = this.ViewState["CPBClass"];
					result = ((obj == null) ? null : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["CPBClass"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), Themeable(true), ANPDescription("desc_PagingButtonsClass")]
		public string PagingButtonsClass
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.PagingButtonsClass;
				}
				else
				{
					object obj = this.ViewState["PagingButtonsClass"];
					result = ((obj == null) ? null : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["PagingButtonsClass"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), Themeable(true), ANPDescription("desc_PagingButtonsStyle")]
		public string PagingButtonsStyle
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.PagingButtonsStyle;
				}
				else
				{
					object obj = this.ViewState["PagingButtonsStyle"];
					result = ((obj == null) ? null : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["PagingButtonsStyle"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), Themeable(true), ANPDescription("desc_FirstLastButtonsClass")]
		public string FirstLastButtonsClass
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.FirstLastButtonsClass;
				}
				else
				{
					object obj = this.ViewState["FirstLastButtonsClass"];
					result = ((obj == null) ? this.PagingButtonsClass : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["FirstLastButtonsClass"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), Themeable(true), ANPDescription("desc_FirstLastButtonsStyle")]
		public string FirstLastButtonsStyle
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.FirstLastButtonsStyle;
				}
				else
				{
					object obj = this.ViewState["FirstLastButtonsStyle"];
					result = ((obj == null) ? this.PagingButtonsStyle : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["FirstLastButtonsStyle"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), Themeable(true), ANPDescription("desc_PrevNextButtonsClass")]
		public string PrevNextButtonsClass
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.PrevNextButtonsClass;
				}
				else
				{
					object obj = this.ViewState["PrevNextButtonsClass"];
					result = ((obj == null) ? this.PagingButtonsClass : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["PrevNextButtonsClass"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), Themeable(true), ANPDescription("desc_PrevNextButtonsStyle")]
		public string PrevNextButtonsStyle
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.PrevNextButtonsStyle;
				}
				else
				{
					object obj = this.ViewState["PrevNextButtonsStyle"];
					result = ((obj == null) ? this.PagingButtonsStyle : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["PrevNextButtonsStyle"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), Themeable(true), ANPDescription("desc_MoreButtonsClass")]
		public string MoreButtonsClass
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.MoreButtonsClass;
				}
				else
				{
					object obj = this.ViewState["MoreButtonsClass"];
					result = ((obj == null) ? this.PagingButtonsClass : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["MoreButtonsClass"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(null), Themeable(true), ANPDescription("desc_MoreButtonsStyle")]
		public string MoreButtonsStyle
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string result;
				if (flag)
				{
					result = this.cloneFrom.MoreButtonsStyle;
				}
				else
				{
					object obj = this.ViewState["MoreButtonsStyle"];
					result = ((obj == null) ? this.PagingButtonsStyle : ((string)obj));
				}
				return result;
			}
			set
			{
				this.ViewState["MoreButtonsStyle"] = value;
			}
		}

		[Browsable(true), Category("Behavior"), DefaultValue(false), TypeConverter(typeof(AspNetPagerIDConverter)), Themeable(false), ANPDescription("desc_CloneFrom")]
		public string CloneFrom
		{
			get
			{
				return (string)this.ViewState["CloneFrom"];
			}
			set
			{
				bool flag = value != null && string.Empty == value.Trim();
				if (flag)
				{
					throw new ArgumentNullException("CloneFrom", SR.GetString("def_EmptyCloneFrom"));
				}
				bool flag2 = this.ID.Equals(value, StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					throw new ArgumentException(SR.GetString("def_RecursiveCloneFrom"), "CloneFrom");
				}
				this.ViewState["CloneFrom"] = value;
			}
		}

		public override bool EnableTheming
		{
			get
			{
				bool flag = this.cloneFrom != null;
				bool enableTheming;
				if (flag)
				{
					enableTheming = this.cloneFrom.EnableTheming;
				}
				else
				{
					enableTheming = base.EnableTheming;
				}
				return enableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		public override string SkinID
		{
			get
			{
				bool flag = this.cloneFrom != null;
				string skinID;
				if (flag)
				{
					skinID = this.cloneFrom.SkinID;
				}
				else
				{
					skinID = base.SkinID;
				}
				return skinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		[Browsable(true), Category("Behavior"), DefaultValue(false), Themeable(true), ANPDescription("desc_EnableUrlWriting")]
		public bool EnableUrlRewriting
		{
			get
			{
				object obj = this.ViewState["UrlRewriting"];
				bool flag = obj == null;
				bool result;
				if (flag)
				{
					bool flag2 = this.cloneFrom != null;
					result = (flag2 && this.cloneFrom.EnableUrlRewriting);
				}
				else
				{
					result = (bool)obj;
				}
				return result;
			}
			set
			{
				this.ViewState["UrlRewriting"] = value;
				if (value)
				{
					this.UrlPaging = true;
				}
			}
		}

		[Browsable(true), Category("Behavior"), DefaultValue(null), Themeable(true), ANPDescription("desc_UrlRewritePattern")]
		public string UrlRewritePattern
		{
			get
			{
				object obj = this.ViewState["URPattern"];
				bool flag = obj == null;
				string result;
				if (flag)
				{
					bool flag2 = this.cloneFrom != null;
					if (flag2)
					{
						result = this.cloneFrom.UrlRewritePattern;
					}
					else
					{
						bool enableUrlRewriting = this.EnableUrlRewriting;
						if (enableUrlRewriting)
						{
							bool flag3 = !base.DesignMode;
							if (flag3)
							{
								string filePath = this.Page.Request.FilePath;
								result = Path.GetFileNameWithoutExtension(filePath) + "_{0}" + Path.GetExtension(filePath);
								return result;
							}
						}
						result = null;
					}
				}
				else
				{
					result = (string)obj;
				}
				return result;
			}
			set
			{
				this.ViewState["URPattern"] = value;
			}
		}

		[Browsable(true), Category("Behavior"), DefaultValue(null), Themeable(true), ANPDescription("desc_FirstPageUrlRewritePattern")]
		public string FirstPageUrlRewritePattern
		{
			get
			{
				object obj = this.ViewState["FPURPattern"];
				bool flag = obj == null;
				string result;
				if (flag)
				{
					bool flag2 = this.cloneFrom != null;
					if (flag2)
					{
						result = this.cloneFrom.FirstPageUrlRewritePattern;
					}
					else
					{
						result = this.UrlRewritePattern;
					}
				}
				else
				{
					result = (string)obj;
				}
				return result;
			}
			set
			{
				this.ViewState["FPURPattern"] = value;
			}
		}

		[Browsable(true), Category("Behavior"), DefaultValue(true), Themeable(true), ANPDescription("desc_AlwaysShow")]
		public bool AlwaysShow
		{
			get
			{
				object obj = this.ViewState["AlwaysShow"];
				bool flag = obj == null;
				bool result;
				if (flag)
				{
					bool flag2 = this.cloneFrom != null;
					result = (!flag2 || this.cloneFrom.AlwaysShow);
				}
				else
				{
					result = (bool)obj;
				}
				return result;
			}
			set
			{
				this.ViewState["AlwaysShow"] = value;
			}
		}

		[Browsable(true), Category("Behavior"), DefaultValue(false), Themeable(true), ANPDescription("desc_AlwaysShowFirstLastPageNumber")]
		public bool AlwaysShowFirstLastPageNumber
		{
			get
			{
				object obj = this.ViewState["AlwaysShowFirstLastPageNumber"];
				bool flag = obj == null;
				bool result;
				if (flag)
				{
					bool flag2 = this.cloneFrom != null;
					result = (flag2 && this.cloneFrom.AlwaysShowFirstLastPageNumber);
				}
				else
				{
					result = (bool)obj;
				}
				return result;
			}
			set
			{
				this.ViewState["AlwaysShowFirstLastPageNumber"] = value;
			}
		}

		public override bool Wrap
		{
			get
			{
				return base.Wrap;
			}
			set
			{
				base.Wrap = false;
			}
		}

		[Browsable(true), Category("Data"), Themeable(true), ANPDefaultValue("def_PIOutOfRangeMsg"), ANPDescription("desc_PIOutOfRangeMsg")]
		public string PageIndexOutOfRangeErrorMessage
		{
			get
			{
				object obj = this.ViewState["PIOutOfRangeErrorMsg"];
				bool flag = obj == null;
				string result;
				if (flag)
				{
					bool flag2 = this.cloneFrom != null;
					if (flag2)
					{
						result = this.cloneFrom.PageIndexOutOfRangeErrorMessage;
					}
					else
					{
						result = SR.GetString("def_PIOutOfRangeMsg");
					}
				}
				else
				{
					result = (string)obj;
				}
				return result;
			}
			set
			{
				this.ViewState["PIOutOfRangeErrorMsg"] = value;
			}
		}

		[Browsable(true), Category("Data"), Themeable(true), ANPDefaultValue("def_InvalidPIErrorMsg"), ANPDescription("desc_InvalidPIErrorMsg")]
		public string InvalidPageIndexErrorMessage
		{
			get
			{
				object obj = this.ViewState["InvalidPIErrorMsg"];
				bool flag = obj == null;
				string result;
				if (flag)
				{
					bool flag2 = this.cloneFrom != null;
					if (flag2)
					{
						result = this.cloneFrom.InvalidPageIndexErrorMessage;
					}
					else
					{
						result = SR.GetString("def_InvalidPIErrorMsg");
					}
				}
				else
				{
					result = (string)obj;
				}
				return result;
			}
			set
			{
				this.ViewState["InvalidPIErrorMsg"] = value;
			}
		}

		[Browsable(true), Category("Appearance"), Themeable(true), ANPDefaultValue("Table"), ANPDescription("desc_LayoutType")]
		public LayoutType LayoutType
		{
			get
			{
				bool flag = this.cloneFrom != null;
				LayoutType result;
				if (flag)
				{
					result = this.cloneFrom.LayoutType;
				}
				else
				{
					object obj = this.ViewState["LayoutType"];
					result = LayoutType.Table;
				}
				return result;
			}
			set
			{
				this.ViewState["LayoutType"] = value;
			}
		}

		public TimPagingBar()
		{
			this.Height = new Unit(30);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			bool flag = this.CloneFrom != null && string.Empty != this.CloneFrom.Trim();
			if (flag)
			{
				TimPagingBar ctrl = this.Parent.FindControl(this.CloneFrom) as TimPagingBar;
				bool flag2 = ctrl == null;
				if (flag2)
				{
					string errStr = SR.GetString("def_CloneFromTypeError");
					throw new ArgumentException(errStr.Replace("%controlID%", this.CloneFrom), "CloneFrom");
				}
				bool flag3 = ctrl.cloneFrom != null && this == ctrl.cloneFrom;
				if (flag3)
				{
					string errStr2 = SR.GetString("def_RecursiveCloneFrom");
					throw new ArgumentException(errStr2, "CloneFrom");
				}
				this.cloneFrom = ctrl;
				this.CssClass = this.cloneFrom.CssClass;
				this.Width = this.cloneFrom.Width;
				this.Height = this.cloneFrom.Height;
				this.HorizontalAlign = this.cloneFrom.HorizontalAlign;
				this.BackColor = this.cloneFrom.BackColor;
				this.BackImageUrl = this.cloneFrom.BackImageUrl;
				this.BorderColor = this.cloneFrom.BorderColor;
				this.BorderStyle = this.cloneFrom.BorderStyle;
				this.BorderWidth = this.cloneFrom.BorderWidth;
				this.Font.CopyFrom(this.cloneFrom.Font);
				this.ForeColor = this.cloneFrom.ForeColor;
				this.EnableViewState = this.cloneFrom.EnableViewState;
				this.Enabled = this.cloneFrom.Enabled;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			bool urlPaging = this.UrlPaging;
			if (urlPaging)
			{
				this.currentUrl = this.Page.Request.Path;
				this.queryString = this.Page.Request.ServerVariables["Query_String"];
				bool flag = !string.IsNullOrEmpty(this.queryString) && this.queryString.StartsWith("?");
				if (flag)
				{
					this.queryString = this.queryString.TrimStart(new char[]
					{
						'?'
					});
				}
				bool flag2 = !this.Page.IsPostBack && this.cloneFrom == null;
				if (flag2)
				{
					int index;
					int.TryParse(this.Page.Request.QueryString[this.UrlPageIndexName], out index);
					bool flag3 = index <= 0;
					if (flag3)
					{
						index = 1;
					}
					else
					{
						bool reverseUrlPageIndex = this.ReverseUrlPageIndex;
						if (reverseUrlPageIndex)
						{
							index = this.PageCount - index + 1;
						}
					}
					PageChangingEventArgs args = new PageChangingEventArgs(index);
					this.OnPageChanging(args);
				}
			}
			else
			{
				this.inputPageIndex = this.Page.Request.Form[this.UniqueID + "_input"];
			}
			base.OnLoad(e);
			bool flag4 = (this.UrlPaging || (!this.UrlPaging && this.PageIndexBoxType == PageIndexBoxType.TextBox)) && (this.ShowPageIndexBox == ShowPageIndexBox.Always || (this.ShowPageIndexBox == ShowPageIndexBox.Auto && this.PageCount >= this.ShowBoxThreshold));
			if (flag4)
			{
				HttpContext.Current.Items["IsANPScriptsRegistered"] = true;
			}
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool flag = this.Page != null && !this.UrlPaging;
			if (flag)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			bool flag2 = !base.DesignMode && HttpContext.Current.Items["IsANPScriptsRegistered"] != null && HttpContext.Current.Items["isANPScriptRegistered"] == null;
			if (flag2)
			{
				writer.Write("<script type=\"text/javascript\" src=\"");
				writer.Write(this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "TIM.T_WEBCTRL.TimPagingBar.TimPagingBar.js"));
				writer.WriteLine("\"></script>");
				HttpContext.Current.Items["isANPScriptRegistered"] = true;
			}
			base.AddAttributesToRender(writer);
		}

		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			bool showPager = this.PageCount > 1 || (this.PageCount <= 1 && this.AlwaysShow);
			bool flag = !showPager;
			if (flag)
			{
				writer.Write("<!--");
				writer.Write(SR.GetString("def_AutoHideInfo"));
				writer.Write("-->");
			}
			else
			{
				base.RenderBeginTag(writer);
			}
		}

		public override void RenderEndTag(HtmlTextWriter writer)
		{
			bool flag = this.PageCount > 1 || (this.PageCount <= 1 && this.AlwaysShow);
			if (flag)
			{
				base.RenderEndTag(writer);
			}
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			bool flag = this.PageCount <= 1 && !this.AlwaysShow;
			if (!flag)
			{
				writer.Indent = 0;
				bool flag2 = this.ShowCustomInfoSection > ShowCustomInfoSection.Never;
				if (flag2)
				{
					bool flag3 = this.LayoutType == LayoutType.Table;
					if (flag3)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
						writer.AddAttribute(HtmlTextWriterAttribute.Style, base.Style.Value);
						bool flag4 = this.Height != Unit.Empty;
						if (flag4)
						{
							writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
						}
						writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
						writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
						writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
						writer.RenderBeginTag(HtmlTextWriterTag.Table);
						writer.RenderBeginTag(HtmlTextWriterTag.Tr);
					}
					bool flag5 = this.ShowCustomInfoSection == ShowCustomInfoSection.Left;
					if (flag5)
					{
						this.RenderCustomInfoSection(writer);
						this.RenderNavigationSection(writer);
					}
					else
					{
						this.RenderNavigationSection(writer);
						this.RenderCustomInfoSection(writer);
					}
					bool flag6 = this.LayoutType == LayoutType.Table;
					if (flag6)
					{
						writer.RenderEndTag();
						writer.RenderEndTag();
					}
				}
				else
				{
					this.RenderPagingElements(writer);
				}
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		private static void addMoreListItem(HtmlTextWriter writer, int pageIndex)
		{
			writer.Write("<option value=\"");
			writer.Write(pageIndex);
			writer.Write("\">......</option>");
		}

		private void listPageIndices(HtmlTextWriter writer, int startIndex, int endIndex)
		{
			for (int i = startIndex; i <= endIndex; i++)
			{
				writer.Write("<option value=\"");
				writer.Write(i);
				writer.Write("\"");
				bool flag = i == this.CurrentPageIndex;
				if (flag)
				{
					writer.Write(" selected=\"true\"");
				}
				writer.Write(">");
				writer.Write(i);
				writer.Write("</option>");
			}
		}

		private void RenderCustomInfoSection(HtmlTextWriter writer)
		{
			bool flag = this.Height != Unit.Empty;
			if (flag)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
			}
			string customUnit = this.CustomInfoSectionWidth.ToString();
			bool flag2 = this.CustomInfoClass != null && this.CustomInfoClass.Trim().Length > 0;
			if (flag2)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CustomInfoClass);
			}
			bool flag3 = this.CustomInfoStyle != null && this.CustomInfoStyle.Trim().Length > 0;
			if (flag3)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Style, this.CustomInfoStyle);
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, customUnit);
			bool flag4 = this.CustomInfoTextAlign > HorizontalAlign.NotSet;
			if (flag4)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Align, this.CustomInfoTextAlign.ToString().ToLower());
			}
			bool flag5 = this.LayoutType == LayoutType.Div;
			if (flag5)
			{
				writer.AddStyleAttribute("float", "left");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "bottom");
				writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "true");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
			}
			writer.Write(this.GetCustomInfoHtml(this.CustomInfoHTML));
			writer.RenderEndTag();
		}

		private void RenderNavigationSection(HtmlTextWriter writer)
		{
			bool flag = this.CustomInfoSectionWidth.Type == UnitType.Percentage;
			if (flag)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, Unit.Percentage(100.0 - this.CustomInfoSectionWidth.Value).ToString());
			}
			bool flag2 = this.HorizontalAlign > HorizontalAlign.NotSet;
			if (flag2)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Align, this.HorizontalAlign.ToString().ToLower());
			}
			bool flag3 = !string.IsNullOrEmpty(this.CssClass);
			if (flag3)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
			}
			bool flag4 = this.LayoutType == LayoutType.Div;
			if (flag4)
			{
				writer.AddStyleAttribute("float", "left");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "bottom");
				writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "true");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
			}
			this.RenderPagingElements(writer);
			writer.RenderEndTag();
		}

		private void RenderPagingElements(HtmlTextWriter writer)
		{
			int startIndex = (this.CurrentPageIndex - 1) / this.NumericButtonCount * this.NumericButtonCount;
			bool flag = this.PageCount > this.NumericButtonCount && this.CurrentPageButtonPosition != PagingButtonPosition.Fixed;
			if (flag)
			{
				switch (this.CurrentPageButtonPosition)
				{
				case PagingButtonPosition.Beginning:
				{
					startIndex = this.CurrentPageIndex - 1;
					bool flag2 = startIndex + this.NumericButtonCount > this.PageCount;
					if (flag2)
					{
						startIndex = this.PageCount - this.NumericButtonCount;
					}
					break;
				}
				case PagingButtonPosition.End:
				{
					bool flag3 = this.CurrentPageIndex > this.NumericButtonCount;
					if (flag3)
					{
						startIndex = this.CurrentPageIndex - this.NumericButtonCount;
					}
					break;
				}
				case PagingButtonPosition.Center:
				{
					int startOffset = this.CurrentPageIndex - (int)Math.Ceiling((double)this.NumericButtonCount / 2.0);
					bool flag4 = startOffset > 0;
					if (flag4)
					{
						startIndex = startOffset;
						bool flag5 = startIndex > this.PageCount - this.NumericButtonCount;
						if (flag5)
						{
							startIndex = this.PageCount - this.NumericButtonCount;
						}
					}
					break;
				}
				}
			}
			int endIndex = (startIndex + this.NumericButtonCount > this.PageCount) ? this.PageCount : (startIndex + this.NumericButtonCount);
			bool flag6 = this.PagingButtonLayoutType == PagingButtonLayoutType.UnorderedList;
			if (flag6)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			}
			bool flag7 = this.NavigationButtonsPosition == NavigationButtonPosition.Left || this.NavigationButtonsPosition == NavigationButtonPosition.BothSides;
			if (flag7)
			{
				this.CreateNavigationButton(writer, TimPagingBar.NavigationButton.First);
				this.CreateNavigationButton(writer, TimPagingBar.NavigationButton.Prev);
				bool flag8 = this.NavigationButtonsPosition == NavigationButtonPosition.Left;
				if (flag8)
				{
					this.CreateNavigationButton(writer, TimPagingBar.NavigationButton.Next);
					this.CreateNavigationButton(writer, TimPagingBar.NavigationButton.Last);
				}
			}
			bool flag9 = this.AlwaysShowFirstLastPageNumber && startIndex > 0;
			if (flag9)
			{
				this.CreateNumericButton(writer, 1);
			}
			bool flag10 = this.ShowMoreButtons && ((!this.AlwaysShowFirstLastPageNumber && startIndex > 0) || (this.AlwaysShowFirstLastPageNumber && startIndex > 1));
			if (flag10)
			{
				this.CreateMoreButton(writer, startIndex);
			}
			bool showPageIndex = this.ShowPageIndex;
			if (showPageIndex)
			{
				for (int i = startIndex + 1; i <= endIndex; i++)
				{
					this.CreateNumericButton(writer, i);
				}
			}
			bool flag11 = this.ShowMoreButtons && this.PageCount > this.NumericButtonCount && ((!this.AlwaysShowFirstLastPageNumber && endIndex < this.PageCount) || (this.AlwaysShowFirstLastPageNumber && this.PageCount > endIndex + 1));
			if (flag11)
			{
				this.CreateMoreButton(writer, endIndex + 1);
			}
			bool flag12 = this.AlwaysShowFirstLastPageNumber && endIndex < this.PageCount;
			if (flag12)
			{
				this.CreateNumericButton(writer, this.PageCount);
			}
			bool flag13 = this.NavigationButtonsPosition == NavigationButtonPosition.Right || this.NavigationButtonsPosition == NavigationButtonPosition.BothSides;
			if (flag13)
			{
				bool flag14 = this.NavigationButtonsPosition == NavigationButtonPosition.Right;
				if (flag14)
				{
					this.CreateNavigationButton(writer, TimPagingBar.NavigationButton.First);
					this.CreateNavigationButton(writer, TimPagingBar.NavigationButton.Prev);
				}
				this.CreateNavigationButton(writer, TimPagingBar.NavigationButton.Next);
				this.CreateNavigationButton(writer, TimPagingBar.NavigationButton.Last);
				this.CreateNavigationButton(writer, TimPagingBar.NavigationButton.Refresh);
			}
			bool flag15 = this.PagingButtonLayoutType == PagingButtonLayoutType.UnorderedList;
			if (flag15)
			{
				writer.RenderEndTag();
			}
			bool flag16 = this.ShowPageIndexBox == ShowPageIndexBox.Always || (this.ShowPageIndexBox == ShowPageIndexBox.Auto && this.PageCount >= this.ShowBoxThreshold);
			if (flag16)
			{
				string boxClientId = this.UniqueID + "_input";
				writer.Write("&nbsp;&nbsp;");
				bool flag17 = !string.IsNullOrEmpty(this.TextBeforePageIndexBox);
				if (flag17)
				{
					writer.Write(this.TextBeforePageIndexBox);
				}
				bool flag18 = this.PageIndexBoxType == PageIndexBoxType.TextBox;
				if (flag18)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "60px");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "14px");
					writer.AddAttribute(HtmlTextWriterAttribute.Value, this.CurrentPageIndex.ToString());
					bool flag19 = !string.IsNullOrEmpty(this.PageIndexBoxStyle);
					if (flag19)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Style, this.PageIndexBoxStyle);
					}
					bool flag20 = !string.IsNullOrEmpty(this.PageIndexBoxClass);
					if (flag20)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Class, this.PageIndexBoxClass);
					}
					bool flag21 = !this.Enabled || (this.PageCount <= 1 && this.AlwaysShow);
					if (flag21)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Name, boxClientId);
					writer.AddAttribute(HtmlTextWriterAttribute.Id, boxClientId);
					string chkInputScript = string.Concat(new object[]
					{
						"ANP_checkInput('",
						boxClientId,
						"',",
						this.PageCount,
						",'",
						this.PageIndexOutOfRangeErrorMessage,
						"','",
						this.InvalidPageIndexErrorMessage,
						"')"
					});
					string keydownScript = "ANP_keydown(event,'" + this.UniqueID + "_btn');";
					string keyupScript = "ANP_keyup('" + this.UniqueID + "_input');";
					string fp = this.GetHrefString(1);
					string clickScript = string.Concat(new object[]
					{
						"if(",
						chkInputScript,
						"){ANP_goToPage('",
						boxClientId,
						"','",
						this.UrlPageIndexName,
						"','",
						fp,
						"','",
						this.GetHrefString(-1),
						"','",
						this.UrlPagingTarget,
						"',",
						this.PageCount,
						",",
						this.ReverseUrlPageIndex ? "true" : "false",
						");};return false;"
					});
					writer.AddAttribute("onkeydown", keydownScript, false);
					writer.AddAttribute("onkeyup", keyupScript, false);
					writer.RenderBeginTag(HtmlTextWriterTag.Input);
					writer.RenderEndTag();
					bool flag22 = !string.IsNullOrEmpty(this.TextAfterPageIndexBox);
					if (flag22)
					{
						writer.Write(this.GetCustomInfoHtml(this.TextAfterPageIndexBox));
					}
					bool flag23 = !string.IsNullOrEmpty(this.SubmitButtonImageUrl);
					if (flag23)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Type, "image");
						writer.AddAttribute(HtmlTextWriterAttribute.Src, this.SubmitButtonImageUrl);
					}
					else
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Type, this.UrlPaging ? "button" : "submit");
						writer.AddAttribute(HtmlTextWriterAttribute.Style, "width: 0px; height: 0px;visibility: hidden;");
						writer.AddAttribute(HtmlTextWriterAttribute.Value, this.SubmitButtonText);
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
					writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID + "_btn");
					bool flag24 = !string.IsNullOrEmpty(this.SubmitButtonClass);
					if (flag24)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Class, this.SubmitButtonClass);
					}
					bool flag25 = !string.IsNullOrEmpty(this.SubmitButtonStyle);
					if (flag25)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Style, this.SubmitButtonStyle);
					}
					bool flag26 = !this.Enabled || (this.PageCount <= 1 && this.AlwaysShow);
					if (flag26)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Onclick, this.UrlPaging ? clickScript : string.Concat(new string[]
					{
						"if(",
						chkInputScript,
						"){",
						this.Page.ClientScript.GetPostBackEventReference(this, ""),
						";return false;} else{return false}"
					}), false);
					writer.RenderBeginTag(HtmlTextWriterTag.Input);
					writer.RenderEndTag();
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Name, boxClientId);
					writer.AddAttribute(HtmlTextWriterAttribute.Id, boxClientId);
					writer.AddAttribute(HtmlTextWriterAttribute.Onchange, this.UrlPaging ? string.Concat(new object[]
					{
						"ANP_goToPage('",
						boxClientId,
						"','",
						this.UrlPageIndexName,
						"','",
						this.GetHrefString(1),
						"','",
						this.GetHrefString(-1),
						"','",
						this.UrlPagingTarget,
						"',",
						this.PageCount,
						",",
						this.ReverseUrlPageIndex ? "true" : "false",
						")"
					}) : this.Page.ClientScript.GetPostBackEventReference(this, null), false);
					bool flag27 = !string.IsNullOrEmpty(this.PageIndexBoxStyle);
					if (flag27)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Style, this.PageIndexBoxStyle);
					}
					bool flag28 = !string.IsNullOrEmpty(this.PageIndexBoxClass);
					if (flag28)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Class, this.PageIndexBoxClass);
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Select);
					bool flag29 = this.PageCount > 80;
					if (flag29)
					{
						bool flag30 = this.CurrentPageIndex <= 15;
						if (flag30)
						{
							this.listPageIndices(writer, 1, 15);
							TimPagingBar.addMoreListItem(writer, 16);
							this.listPageIndices(writer, this.PageCount - 4, this.PageCount);
						}
						else
						{
							bool flag31 = this.CurrentPageIndex >= this.PageCount - 14;
							if (flag31)
							{
								this.listPageIndices(writer, 1, 5);
								TimPagingBar.addMoreListItem(writer, this.PageCount - 15);
								this.listPageIndices(writer, this.PageCount - 14, this.PageCount);
							}
							else
							{
								this.listPageIndices(writer, 1, 5);
								TimPagingBar.addMoreListItem(writer, this.CurrentPageIndex - 6);
								this.listPageIndices(writer, this.CurrentPageIndex - 5, this.CurrentPageIndex + 5);
								TimPagingBar.addMoreListItem(writer, this.CurrentPageIndex + 6);
								this.listPageIndices(writer, this.PageCount - 4, this.PageCount);
							}
						}
					}
					else
					{
						this.listPageIndices(writer, 1, this.PageCount);
					}
					writer.RenderEndTag();
					bool flag32 = !string.IsNullOrEmpty(this.TextAfterPageIndexBox);
					if (flag32)
					{
						writer.Write(this.TextAfterPageIndexBox);
					}
				}
			}
		}

		private string GetHrefString(int pageIndex)
		{
			bool urlPaging = this.UrlPaging;
			string result;
			if (urlPaging)
			{
				int urlPageIndex = pageIndex;
				string plcHolder = "{" + this.UrlPageIndexName + "}";
				bool reverseUrlPageIndex = this.ReverseUrlPageIndex;
				if (reverseUrlPageIndex)
				{
					urlPageIndex = ((pageIndex == -1) ? -1 : (this.PageCount - pageIndex + 1));
				}
				bool enableUrlRewriting = this.EnableUrlRewriting;
				if (enableUrlRewriting)
				{
					string pattern = (pageIndex == 1 && !string.IsNullOrEmpty(this.FirstPageUrlRewritePattern)) ? this.FirstPageUrlRewritePattern : this.UrlRewritePattern;
					Regex reg = new Regex("(?<p>%(?<m>[^%]+)%)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
					MatchCollection mts = reg.Matches(pattern);
					NameValueCollection urlParams = TimPagingBar.ConvertQueryStringToCollection(this.queryString);
					string url = pattern;
					foreach (Match i in mts)
					{
						string prmValue = urlParams[i.Groups["m"].Value];
						url = url.Replace(i.Groups["p"].Value, prmValue);
					}
					result = base.ResolveUrl(string.Format(url, (urlPageIndex == -1) ? plcHolder : urlPageIndex.ToString()));
				}
				else
				{
					result = this.BuildUrlString((urlPageIndex == -1) ? plcHolder : (((urlPageIndex == 1 && !this.ReverseUrlPageIndex) || (this.ReverseUrlPageIndex && urlPageIndex == this.PageCount)) ? null : urlPageIndex.ToString()));
				}
			}
			else
			{
				result = this.Page.ClientScript.GetPostBackClientHyperlink(this, pageIndex.ToString());
			}
			return result;
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

        private string GetCustomInfoHtml(string origText)
		{
			bool flag = !string.IsNullOrEmpty(origText) && origText.IndexOf('%') >= 0;
			string result;
			if (flag)
			{
				string[] props = new string[]
				{
					"recordcount",
					"pagecount",
					"currentpageindex",
					"startrecordindex",
					"endrecordindex",
					"pagesize",
					"pagesremain",
					"recordsremain"
				};
				StringBuilder sb = new StringBuilder(origText);
				Regex reg = new Regex("(?<ph>%(?<pname>\\w{8,})%)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
				MatchCollection mts = reg.Matches(origText);
				foreach (Match i in mts)
				{
					string p = i.Groups["pname"].Value.ToLower();
					bool flag2 = Array.IndexOf<string>(props, p) >= 0;
					if (flag2)
					{
						string repValue = null;
						string text = p;
						uint num = ComputeStringHash(text);
						if (num <= 1816522103u)
						{
							if (num <= 1212059509u)
							{
								if (num != 983019034u)
								{
									if (num == 1212059509u)
									{
										if (text == "recordsremain")
										{
											repValue = this.RecordsRemain.ToString();
										}
									}
								}
								else if (text == "startrecordindex")
								{
									repValue = this.StartRecordIndex.ToString();
								}
							}
							else if (num != 1473379191u)
							{
								if (num == 1816522103u)
								{
									if (text == "currentpageindex")
									{
										repValue = this.CurrentPageIndex.ToString();
									}
								}
							}
							else if (text == "pagesize")
							{
								repValue = this.PageSize.ToString();
							}
						}
						else if (num <= 2683616005u)
						{
							if (num != 1971311717u)
							{
								if (num == 2683616005u)
								{
									if (text == "pagecount")
									{
										repValue = this.PageCount.ToString();
									}
								}
							}
							else if (text == "endrecordindex")
							{
								repValue = this.EndRecordIndex.ToString();
							}
						}
						else if (num != 3272188659u)
						{
							if (num == 3481252127u)
							{
								if (text == "pagesremain")
								{
									repValue = this.PagesRemain.ToString();
								}
							}
						}
						else if (text == "recordcount")
						{
							repValue = this.RecordCount.ToString();
						}
						bool flag3 = repValue != null;
						if (flag3)
						{
							sb.Replace(i.Groups["ph"].Value, repValue);
						}
					}
				}
				result = sb.ToString();
			}
			else
			{
				result = origText;
			}
			return result;
		}

		private static NameValueCollection ConvertQueryStringToCollection(string s)
		{
			NameValueCollection prms = new NameValueCollection();
			int num = (s != null) ? s.Length : 0;
			for (int i = 0; i < num; i++)
			{
				int startIndex = i;
				int num2 = -1;
				while (i < num)
				{
					char ch = s[i];
					bool flag = ch == '=';
					if (flag)
					{
						bool flag2 = num2 < 0;
						if (flag2)
						{
							num2 = i;
						}
					}
					else
					{
						bool flag3 = ch == '&';
						if (flag3)
						{
							break;
						}
					}
					i++;
				}
				string skey = null;
				bool flag4 = num2 >= 0;
				string svalue;
				if (flag4)
				{
					skey = s.Substring(startIndex, num2 - startIndex);
					svalue = s.Substring(num2 + 1, i - num2 - 1);
				}
				else
				{
					svalue = s.Substring(startIndex, i - startIndex);
				}
				prms.Add(skey, svalue);
				bool flag5 = i == num - 1 && s[i] == '&';
				if (flag5)
				{
					prms.Add(null, string.Empty);
				}
			}
			return prms;
		}

		private string BuildUrlString(string pageIndex)
		{
			StringBuilder ubuilder = new StringBuilder(80);
			bool keyFound = false;
			string amp = "";
			bool flag = !string.IsNullOrEmpty(this.queryString);
			if (flag)
			{
				string[] prms = this.queryString.Split(new char[]
				{
					'&'
				});
				string[] array = prms;
				for (int i = 0; i < array.Length; i++)
				{
					string pm = array[i];
					string[] nvArr = pm.Split(new char[]
					{
						'='
					});
					string pName = nvArr[0];
					bool flag2 = !string.IsNullOrEmpty(pName);
					if (flag2)
					{
						bool flag3 = pName.Equals(this.UrlPageIndexName, StringComparison.InvariantCultureIgnoreCase);
						if (flag3)
						{
							keyFound = true;
							bool flag4 = !string.IsNullOrEmpty(pageIndex);
							if (flag4)
							{
								ubuilder.Append(amp).Append(pName).Append("=").Append(pageIndex);
								amp = "&amp;";
							}
						}
						else
						{
							ubuilder.Append(amp).Append(pName);
							bool flag5 = nvArr.Length > 1;
							if (flag5)
							{
								ubuilder.Append("=").Append(nvArr[1]);
							}
							amp = "&amp;";
						}
					}
				}
			}
			bool flag6 = !keyFound && !string.IsNullOrEmpty(pageIndex);
			if (flag6)
			{
				ubuilder.Append(amp).Append(this.UrlPageIndexName).Append("=").Append(pageIndex);
			}
			bool flag7 = ubuilder.Length > 0;
			if (flag7)
			{
				ubuilder.Insert(0, "?");
			}
			ubuilder.Insert(0, Path.GetFileName(this.currentUrl));
			return ubuilder.ToString();
		}

		private void CreateNavigationButton(HtmlTextWriter writer, TimPagingBar.NavigationButton btn)
		{
			bool flag = !this.ShowFirstLast && (btn == TimPagingBar.NavigationButton.First || btn == TimPagingBar.NavigationButton.Last || btn == TimPagingBar.NavigationButton.Refresh);
			if (!flag)
			{
				bool flag2 = !this.ShowPrevNext && (btn == TimPagingBar.NavigationButton.Prev || btn == TimPagingBar.NavigationButton.Next || btn == TimPagingBar.NavigationButton.Refresh);
				if (!flag2)
				{
					bool flag3 = this.PagingButtonLayoutType != PagingButtonLayoutType.None;
					if (flag3)
					{
						bool flag4 = btn == TimPagingBar.NavigationButton.First || btn == TimPagingBar.NavigationButton.Last;
						if (flag4)
						{
							this.AddClassAndStyle(this.FirstLastButtonsClass, this.FirstLastButtonsStyle, writer);
						}
						else
						{
							this.AddClassAndStyle(this.PrevNextButtonsClass, this.PrevNextButtonsStyle, writer);
						}
					}
					this.AddPagingButtonLayoutTag(writer);
					string btnname = btn.ToString().ToLower();
					bool isImgBtn = this.NavigationButtonType == PagingButtonType.Image;
					bool flag5 = btn == TimPagingBar.NavigationButton.First || btn == TimPagingBar.NavigationButton.Prev;
					if (flag5)
					{
						bool disabled = this.CurrentPageIndex <= 1 | !this.Enabled;
						bool flag6 = !this.ShowDisabledButtons & disabled;
						if (flag6)
						{
							return;
						}
						int pageIndex = (btn == TimPagingBar.NavigationButton.First) ? 1 : (this.CurrentPageIndex - 1);
						this.writeSpacingStyle(writer);
						bool flag7 = this.PagingButtonLayoutType == PagingButtonLayoutType.None;
						if (flag7)
						{
							bool flag8 = btn == TimPagingBar.NavigationButton.First || btn == TimPagingBar.NavigationButton.Last;
							if (flag8)
							{
								this.AddClassAndStyle(this.FirstLastButtonsClass, this.FirstLastButtonsStyle, writer);
							}
							else
							{
								this.AddClassAndStyle(this.PrevNextButtonsClass, this.PrevNextButtonsStyle, writer);
							}
						}
						bool flag9 = isImgBtn;
						if (flag9)
						{
							bool flag10 = !disabled;
							if (flag10)
							{
								writer.AddAttribute("href", this.GetHrefString(pageIndex), false);
								this.AddToolTip(writer, pageIndex);
								this.AddHyperlinkTarget(writer);
								writer.RenderBeginTag(HtmlTextWriterTag.A);
								writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.ButtonImageNameExtension + this.ButtonImageExtension);
								writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
								bool flag11 = this.ButtonImageAlign > ImageAlign.NotSet;
								if (flag11)
								{
									writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
								}
								writer.RenderBeginTag(HtmlTextWriterTag.Img);
								writer.RenderEndTag();
								writer.RenderEndTag();
							}
							else
							{
								writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.DisabledButtonImageNameExtension + this.ButtonImageExtension);
								writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
								bool flag12 = this.ButtonImageAlign > ImageAlign.NotSet;
								if (flag12)
								{
									writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
								}
								writer.RenderBeginTag(HtmlTextWriterTag.Img);
								writer.RenderEndTag();
							}
						}
						else
						{
							string linktext = (btn == TimPagingBar.NavigationButton.Prev) ? this.PrevPageText : this.FirstPageText;
							bool flag13 = disabled;
							if (flag13)
							{
								writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
							}
							else
							{
								this.AddToolTip(writer, pageIndex);
								this.AddHyperlinkTarget(writer);
								writer.AddAttribute("href", this.GetHrefString(pageIndex), false);
							}
							writer.RenderBeginTag(HtmlTextWriterTag.A);
							writer.Write(linktext);
							writer.RenderEndTag();
						}
					}
					else
					{
						bool flag14 = btn == TimPagingBar.NavigationButton.Next || btn == TimPagingBar.NavigationButton.Last;
						if (flag14)
						{
							bool disabled = this.CurrentPageIndex >= this.PageCount | !this.Enabled;
							bool flag15 = !this.ShowDisabledButtons & disabled;
							if (flag15)
							{
								return;
							}
							int pageIndex = (btn == TimPagingBar.NavigationButton.Last) ? this.PageCount : (this.CurrentPageIndex + 1);
							this.writeSpacingStyle(writer);
							bool flag16 = this.PagingButtonLayoutType == PagingButtonLayoutType.None;
							if (flag16)
							{
								bool flag17 = btn == TimPagingBar.NavigationButton.First || btn == TimPagingBar.NavigationButton.Last;
								if (flag17)
								{
									this.AddClassAndStyle(this.FirstLastButtonsClass, this.FirstLastButtonsStyle, writer);
								}
								else
								{
									this.AddClassAndStyle(this.PrevNextButtonsClass, this.PrevNextButtonsStyle, writer);
								}
							}
							bool flag18 = isImgBtn;
							if (flag18)
							{
								bool flag19 = !disabled;
								if (flag19)
								{
									writer.AddAttribute("href", this.GetHrefString(pageIndex), false);
									this.AddToolTip(writer, pageIndex);
									this.AddHyperlinkTarget(writer);
									writer.RenderBeginTag(HtmlTextWriterTag.A);
									writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.ButtonImageNameExtension + this.ButtonImageExtension);
									writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
									bool flag20 = this.ButtonImageAlign > ImageAlign.NotSet;
									if (flag20)
									{
										writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
									}
									writer.RenderBeginTag(HtmlTextWriterTag.Img);
									writer.RenderEndTag();
									writer.RenderEndTag();
								}
								else
								{
									writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.DisabledButtonImageNameExtension + this.ButtonImageExtension);
									writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
									bool flag21 = this.ButtonImageAlign > ImageAlign.NotSet;
									if (flag21)
									{
										writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
									}
									writer.RenderBeginTag(HtmlTextWriterTag.Img);
									writer.RenderEndTag();
								}
							}
							else
							{
								string linktext = (btn == TimPagingBar.NavigationButton.Next) ? this.NextPageText : this.LastPageText;
								bool flag22 = disabled;
								if (flag22)
								{
									writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
								}
								else
								{
									this.AddToolTip(writer, pageIndex);
									writer.AddAttribute("href", this.GetHrefString(pageIndex), false);
									this.AddHyperlinkTarget(writer);
								}
								writer.RenderBeginTag(HtmlTextWriterTag.A);
								writer.Write(linktext);
								writer.RenderEndTag();
							}
						}
						else
						{
							bool flag23 = btn == TimPagingBar.NavigationButton.Refresh;
							if (flag23)
							{
								this.writeSpacingStyle(writer);
								this.AddToolTip(writer, this.CurrentPageIndex);
								this.AddHyperlinkTarget(writer);
								writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(this.CurrentPageIndex), false);
								writer.RenderBeginTag(HtmlTextWriterTag.A);
								writer.Write("刷新");
								writer.RenderEndTag();
							}
						}
					}
					bool flag24 = this.PagingButtonLayoutType != PagingButtonLayoutType.None;
					if (flag24)
					{
						writer.RenderEndTag();
					}
				}
			}
		}

		private void AddToolTip(HtmlTextWriter writer, int pageIndex)
		{
			bool showNavigationToolTip = this.ShowNavigationToolTip;
			if (showNavigationToolTip)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, string.Format(this.NavigationToolTipTextFormatString, pageIndex));
			}
		}

		private void AddPagingButtonLayoutTag(HtmlTextWriter writer)
		{
			bool flag = this.PagingButtonLayoutType == PagingButtonLayoutType.UnorderedList;
			if (flag)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
			}
			else
			{
				bool flag2 = this.PagingButtonLayoutType == PagingButtonLayoutType.Span;
				if (flag2)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
				}
			}
		}

		private void CreateNumericButton(HtmlTextWriter writer, int index)
		{
			bool isCurrent = index == this.CurrentPageIndex;
			bool flag = (!isCurrent && this.PagingButtonLayoutType != PagingButtonLayoutType.None) || (isCurrent && this.PagingButtonLayoutType == PagingButtonLayoutType.UnorderedList);
			if (flag)
			{
				bool flag2 = !isCurrent;
				if (flag2)
				{
					this.AddClassAndStyle(this.PagingButtonsClass, this.PagingButtonsStyle, writer);
				}
				this.AddPagingButtonLayoutTag(writer);
			}
			bool flag3 = this.NumericButtonType == PagingButtonType.Image;
			if (flag3)
			{
				this.writeSpacingStyle(writer);
				bool flag4 = !isCurrent;
				if (flag4)
				{
					bool enabled = this.Enabled;
					if (enabled)
					{
						writer.AddAttribute("href", this.GetHrefString(index), false);
					}
					this.AddClassAndStyle(this.PagingButtonsClass, this.PagingButtonsStyle, writer);
					this.AddToolTip(writer, index);
					this.AddHyperlinkTarget(writer);
					writer.RenderBeginTag(HtmlTextWriterTag.A);
					this.CreateNumericImages(writer, index, false);
					writer.RenderEndTag();
				}
				else
				{
					bool flag5 = !string.IsNullOrEmpty(this.CurrentPageButtonClass);
					if (flag5)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CurrentPageButtonClass);
					}
					bool flag6 = !string.IsNullOrEmpty(this.CurrentPageButtonStyle);
					if (flag6)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Style, this.CurrentPageButtonStyle);
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					this.CreateNumericImages(writer, index, true);
					writer.RenderEndTag();
				}
			}
			else
			{
				this.writeSpacingStyle(writer);
				bool flag7 = isCurrent;
				if (flag7)
				{
					bool flag8 = string.IsNullOrEmpty(this.CurrentPageButtonClass) && string.IsNullOrEmpty(this.CurrentPageButtonStyle);
					if (flag8)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "Bold");
						writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "red");
					}
					else
					{
						bool flag9 = !string.IsNullOrEmpty(this.CurrentPageButtonClass);
						if (flag9)
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CurrentPageButtonClass);
						}
						bool flag10 = !string.IsNullOrEmpty(this.CurrentPageButtonStyle);
						if (flag10)
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Style, this.CurrentPageButtonStyle);
						}
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					bool flag11 = !string.IsNullOrEmpty(this.CurrentPageButtonTextFormatString);
					if (flag11)
					{
						writer.Write(string.Format(this.CurrentPageButtonTextFormatString, index));
					}
					else
					{
						writer.Write(index);
					}
					writer.RenderEndTag();
				}
				else
				{
					bool enabled2 = this.Enabled;
					if (enabled2)
					{
						writer.AddAttribute("href", this.GetHrefString(index), false);
						this.AddClassAndStyle(this.PagingButtonsClass, this.PagingButtonsStyle, writer);
					}
					this.AddToolTip(writer, index);
					this.AddHyperlinkTarget(writer);
					writer.RenderBeginTag(HtmlTextWriterTag.A);
					bool flag12 = !string.IsNullOrEmpty(this.NumericButtonTextFormatString);
					if (flag12)
					{
						writer.Write(string.Format(this.NumericButtonTextFormatString, index));
					}
					else
					{
						writer.Write(index);
					}
					writer.RenderEndTag();
				}
			}
			bool flag13 = (!isCurrent && this.PagingButtonLayoutType != PagingButtonLayoutType.None) || (isCurrent && this.PagingButtonLayoutType == PagingButtonLayoutType.UnorderedList);
			if (flag13)
			{
				writer.RenderEndTag();
			}
		}

		private void CreateNumericImages(HtmlTextWriter writer, int index, bool isCurrent)
		{
			this.AddPagingButtonLayoutTag(writer);
			string indexStr = index.ToString();
			for (int i = 0; i < indexStr.Length; i++)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, string.Concat(new object[]
				{
					this.ImagePath,
					indexStr[i],
					isCurrent ? this.CpiButtonImageNameExtension : this.ButtonImageNameExtension,
					this.ButtonImageExtension
				}));
				bool flag = this.ButtonImageAlign > ImageAlign.NotSet;
				if (flag)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
			bool flag2 = this.PagingButtonLayoutType != PagingButtonLayoutType.None;
			if (flag2)
			{
				writer.RenderEndTag();
			}
		}

		private void CreateMoreButton(HtmlTextWriter writer, int pageIndex)
		{
			this.AddClassAndStyle(this.MoreButtonsClass, this.MoreButtonsStyle, writer);
			this.AddPagingButtonLayoutTag(writer);
			this.writeSpacingStyle(writer);
			bool enabled = this.Enabled;
			if (enabled)
			{
				writer.AddAttribute("href", this.GetHrefString(pageIndex), false);
				this.AddToolTip(writer, pageIndex);
				this.AddHyperlinkTarget(writer);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			bool flag = this.MoreButtonType == PagingButtonType.Image;
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + "more" + this.ButtonImageNameExtension + this.ButtonImageExtension);
				writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
				bool flag2 = this.ButtonImageAlign > ImageAlign.NotSet;
				if (flag2)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
			else
			{
				writer.Write("...");
			}
			writer.RenderEndTag();
			bool flag3 = this.PagingButtonLayoutType != PagingButtonLayoutType.None;
			if (flag3)
			{
				writer.RenderEndTag();
			}
		}

		private void writeSpacingStyle(HtmlTextWriter writer)
		{
			bool flag = this.PagingButtonSpacing.Value != 0.0;
			if (flag)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.MarginRight, this.PagingButtonSpacing.ToString());
			}
		}

		private void AddHyperlinkTarget(HtmlTextWriter writer)
		{
			bool flag = !string.IsNullOrEmpty(this.UrlPagingTarget);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, this.UrlPagingTarget);
			}
		}

		private void AddClassAndStyle(string clsname, string style, HtmlTextWriter writer)
		{
			bool flag = !string.IsNullOrEmpty(clsname);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, clsname);
			}
			bool flag2 = !string.IsNullOrEmpty(style);
			if (flag2)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Style, style);
			}
		}

		protected virtual void OnPageChanging(PageChangingEventArgs e)
		{
			PageChangingEventHandler handler = (PageChangingEventHandler)base.Events[TimPagingBar.EventPageChanging];
			bool flag = handler != null;
			if (flag)
			{
				handler(this, e);
				bool flag2 = !e.Cancel || this.UrlPaging;
				if (flag2)
				{
					this.CurrentPageIndex = e.NewPageIndex;
					this.OnPageChanged(EventArgs.Empty);
				}
			}
			else
			{
				this.CurrentPageIndex = e.NewPageIndex;
				this.OnPageChanged(EventArgs.Empty);
			}
		}

		protected virtual void OnPageChanged(EventArgs e)
		{
			EventHandler handler = (EventHandler)base.Events[TimPagingBar.EventPageChanged];
			bool flag = handler != null;
			if (flag)
			{
				handler(this, e);
			}
		}

		public virtual void GoToPage(int pageIndex)
		{
			this.OnPageChanging(new PageChangingEventArgs(pageIndex));
		}

		public void RaisePostBackEvent(string args)
		{
			int pageIndex = this.CurrentPageIndex;
			try
			{
				bool flag = string.IsNullOrEmpty(args);
				if (flag)
				{
					args = this.inputPageIndex;
				}
				pageIndex = int.Parse(args);
			}
			catch
			{
			}
			PageChangingEventArgs pcArgs = new PageChangingEventArgs(pageIndex);
			bool flag2 = this.cloneFrom != null;
			if (flag2)
			{
				this.cloneFrom.OnPageChanging(pcArgs);
			}
			else
			{
				this.OnPageChanging(pcArgs);
			}
		}

		public virtual bool LoadPostData(string pkey, NameValueCollection pcol)
		{
			string str = pcol[this.UniqueID + "_input"];
			bool flag = str != null && str.Trim().Length > 0;
			if (flag)
			{
				try
				{
					int pindex = int.Parse(str);
					bool flag2 = pindex > 0 && pindex <= this.PageCount;
					if (flag2)
					{
						this.inputPageIndex = str;
						this.Page.RegisterRequiresRaiseEvent(this);
					}
				}
				catch
				{
				}
			}
			return false;
		}

		public virtual void RaisePostDataChangedEvent()
		{
		}
	}
}
