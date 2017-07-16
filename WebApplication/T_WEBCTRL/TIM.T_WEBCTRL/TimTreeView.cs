using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[DefaultEvent("SelectedNodeChanged"), DefaultProperty("Nodes"), Designer(typeof(TimTreeViewDesigner)), ControlValueProperty("SelectedValue"), SupportsEventValidation, ToolboxData("<{0}:TimTreeView runat=server></{0}:TimTreeView>")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TimTreeView : WebControl, IScriptControl, IPostBackDataHandler, IPostBackEventHandler
	{
		private ScriptManager SM = null;

		private bool _bClear = true;

		private bool _IsGtPostBack = false;

		private string _PreSelectedValue = string.Empty;

		private TimMenuItemCollection _ContextMenu;

		private string _ScrollPosition = string.Empty;

		private Style _HoverNodeStyle = null;

		private Style _LeafNodeStyle = null;

		private Style _NodeStyle = null;

		private Style _ParentNodeStyle = null;

		private Style _SelectedNodeStyle = null;

		private TimTreeViewNode _RootNode = null;

		private TimTreeViewNode _BranchRoot = null;

		private TimTreeViewNode _SelectedNode = null;

		private string _SelectedValue = string.Empty;

		private string _CheckedValue = string.Empty;

		private TimTreeViewNode _CheckedNode = null;

		private TimTreeViewNodeCollection _CheckedNodes = null;

		private TimTreeViewNode _ContextNode = null;

		private string _ContextValue = string.Empty;

		internal string PopulatedValue = string.Empty;

		internal TimTreeViewNode PopulatedNode = null;

		internal string TargetValue = string.Empty;

		internal TimTreeViewNode TargetNode = null;

		internal string SourceValue = string.Empty;

		internal TimTreeViewNode SourceNode = null;

		private static readonly object EventDragDrop = new object();

		private static readonly object EventTreeNodePopulate = new object();

		private static readonly object EventSelectedNodeChanged = new object();

		private static readonly object EventTreeLoad = new object();

		private static readonly object EventTreeNodeDblClick = new object();

		private static readonly object EventTreeNodeCheckChanged = new object();

		private static readonly object EventTreeNodeExpanded = new object();

		private static readonly object EventTreeNodeMenuClick = new object();

		[Category("Action"), Description("节点拖动事件")]
		public event DragDropEventHandler DragDrop
		{
			add
			{
				base.Events.AddHandler(TimTreeView.EventDragDrop, value);
				this.EnableServerDragDrop = true;
			}
			remove
			{
				base.Events.RemoveHandler(TimTreeView.EventDragDrop, value);
				this.EnableServerDragDrop = false;
			}
		}

		[Category("Action"), Description("子节点动态加载事件")]
		public event TreeNodePopulateEventHandler TreeNodePopulate
		{
			add
			{
				base.Events.AddHandler(TimTreeView.EventTreeNodePopulate, value);
				this.LazyLoad = true;
			}
			remove
			{
				base.Events.RemoveHandler(TimTreeView.EventTreeNodePopulate, value);
				this.LazyLoad = false;
			}
		}

		[Category("Action"), Description("切换选中树节点事件")]
		public event SelectedNodeChangedEventHandler SelectedNodeChanged
		{
			add
			{
				base.Events.AddHandler(TimTreeView.EventSelectedNodeChanged, value);
				this.EnableSelectedNodeChanged = true;
			}
			remove
			{
				base.Events.RemoveHandler(TimTreeView.EventSelectedNodeChanged, value);
				this.EnableSelectedNodeChanged = false;
			}
		}

		[Category("Action"), Description("树节点加载完事件")]
		public event TreeLoadEventHandler TreeLoad
		{
			add
			{
				base.Events.AddHandler(TimTreeView.EventTreeLoad, value);
				this.EnableTreeLoad = true;
			}
			remove
			{
				base.Events.RemoveHandler(TimTreeView.EventTreeLoad, value);
				this.EnableTreeLoad = false;
			}
		}

		[Category("Action"), Description("树节点双击事件")]
		public event TreeNodeDblClickEventHandler TreeNodeDblClick
		{
			add
			{
				base.Events.AddHandler(TimTreeView.EventTreeNodeDblClick, value);
				this.EnableTreeNodeDblClick = true;
			}
			remove
			{
				base.Events.RemoveHandler(TimTreeView.EventTreeNodeDblClick, value);
				this.EnableTreeNodeDblClick = false;
			}
		}

		[Category("Action"), Description("树节点勾选事件")]
		public event TreeNodeCheckChangedEventHandler TreeNodeCheckChanged
		{
			add
			{
				base.Events.AddHandler(TimTreeView.EventTreeNodeCheckChanged, value);
				this.EnableTreeNodeCheckChanged = true;
			}
			remove
			{
				base.Events.RemoveHandler(TimTreeView.EventTreeNodeCheckChanged, value);
				this.EnableTreeNodeCheckChanged = false;
			}
		}

		[Category("Action"), Description("子节点展开事件")]
		public event TreeNodeExpandedEventHandler TreeNodeExpanded
		{
			add
			{
				base.Events.AddHandler(TimTreeView.EventTreeNodeExpanded, value);
				this.EnableTreeNodeExpanded = true;
			}
			remove
			{
				base.Events.RemoveHandler(TimTreeView.EventTreeNodeExpanded, value);
				this.EnableTreeNodeExpanded = false;
			}
		}

		[Category("Action"), Description("子节点右键菜单事件")]
		public event TreeNodeMenuClickEventHandler TreeNodeMenuClick
		{
			add
			{
				base.Events.AddHandler(TimTreeView.EventTreeNodeMenuClick, value);
				this.EnableRightClickMenu = true;
			}
			remove
			{
				base.Events.RemoveHandler(TimTreeView.EventTreeNodeMenuClick, value);
				this.EnableRightClickMenu = false;
			}
		}

		internal bool IsGtPostBack
		{
			get
			{
				return this._IsGtPostBack;
			}
			set
			{
				this._IsGtPostBack = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("重复点击选中节点时是否触发客户端与服务端的selectedNodeChanged事件")]
		public bool ActiveEventsWhenReClick
		{
			get
			{
				object o = this.ViewState["ActiveEventsWhenReClick"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["ActiveEventsWhenReClick"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("获取或设置是否允许节点value值重复")]
		private bool AllowRepeatedValue
		{
			get
			{
				object o = this.ViewState["AllowRepeatedValue"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["AllowRepeatedValue"] = value;
			}
		}

		[Category("Behavior"), DefaultValue("~fix~"), Description("AllowRepeatedValue为true时，获取或设置value后缀标识符")]
		private string RepeatedFix
		{
			get
			{
				object o = this.ViewState["RepeatedFix"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["RepeatedFix"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("设置为延迟加载时（LazyLoad为true时），是否每次展开节点都重新加载子节点")]
		private bool AlwaysReload
		{
			get
			{
				object o = this.ViewState["AlwaysReload"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["AlwaysReload"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("获取或设置是否回发")]
		public bool AutoPostBack
		{
			get
			{
				object o = this.ViewState["AutoPostBack"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("子节点全选中时才选中父节点")]
		public bool CanCustomizeCheck
		{
			get
			{
				object o = this.ViewState["CanCustomizeCheck"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["CanCustomizeCheck"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("获取或设置是否允许为根节点添加兄弟节点")]
		public bool CanAddBrotherForRoot
		{
			get
			{
				object o = this.ViewState["CanAddBrotherForRoot"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["CanAddBrotherForRoot"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("获取或设置是否允许拖动节点图片")]
		public bool CanDragNodeImage
		{
			get
			{
				object o = this.ViewState["CanDragNodeImage"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["CanDragNodeImage"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(true), Description("获取或设置拖动节点后是否允许放下，只有在DropMode设为dmAutomatic时有效")]
		public bool CanDrop
		{
			get
			{
				object o = this.ViewState["CanDrop"];
				return o == null || (bool)o;
			}
			set
			{
				this.ViewState["CanDrop"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(""), Description("获取或设置可折叠节点的指示符所显示图像的工具提示")]
		public string CollapseImageToolTip
		{
			get
			{
				object o = this.ViewState["CollapseImageToolTip"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["CollapseImageToolTip"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(""), Description("获取或设置可折叠节点未展开时显示图片的路径")]
		public string CollapseImageUrl
		{
			get
			{
				object o = this.ViewState["CollapseImageUrl"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["CollapseImageUrl"] = value;
			}
		}

		[Category("Misc"), DefaultValue(null), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TimMenuItemCollection ContextMenu
		{
			get
			{
				bool flag = this._ContextMenu == null;
				if (flag)
				{
					this._ContextMenu = new TimMenuItemCollection();
				}
				return this._ContextMenu;
			}
		}

		[Category("Behavior"), DefaultValue(TimDropMode.dmManual), Description("获取或设置拖动模式")]
		public TimDropMode DropMode
		{
			get
			{
				object o = this.ViewState["DropMode"];
				return (o == null) ? TimDropMode.dmManual : ((TimDropMode)o);
			}
			set
			{
				this.ViewState["DropMode"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(-1), Description("获取或设置第一次显示控件时所展开的层次数，默认值为 -1，表示显示所有节点")]
		public int ExpandDepth
		{
			get
			{
				object o = this.ViewState["ExpandDepth"];
				return (o == null) ? -1 : ((int)o);
			}
			set
			{
				this.ViewState["ExpandDepth"] = ((value <= -1) ? -1 : value);
			}
		}

		[Category("Behavior"), DefaultValue(""), Description("获取或设置可展开节点的指示符所显示图像的工具提示")]
		public string ExpandImageToolTip
		{
			get
			{
				object o = this.ViewState["ExpandImageToolTip"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["ExpandImageToolTip"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(""), Description("获取或设置可展开节点的展开时显示图片的路径")]
		public string ExpandImageUrl
		{
			get
			{
				object o = this.ViewState["ExpandImageUrl"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["ExpandImageUrl"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(TimTreeViewEventDatas.All), Description("获取或设置回传数据")]
		public TimTreeViewEventDatas EventDatas
		{
			get
			{
				object o = this.ViewState["EventDatas"];
				return (o == null) ? TimTreeViewEventDatas.All : ((TimTreeViewEventDatas)o);
			}
			set
			{
				this.ViewState["EventDatas"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(""), Description("获取或设置树控件所在容器，以便回发后进行滚动条定位")]
		public string FatherContent
		{
			get
			{
				object o = this.ViewState["FatherContent"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["FatherContent"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("获取或设置拖动成功后，是否触发选中节点事件")]
		public bool FireSelectEventAfterDrop
		{
			get
			{
				object o = this.ViewState["FireSelectEventAfterDrop"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["FireSelectEventAfterDrop"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(TimTreeViewImageSet.Custom), Description("获取或设置用于控件的图像组")]
		public TimTreeViewImageSet ImageSet
		{
			get
			{
				object o = this.ViewState["ImageSet"];
				return (o == null) ? TimTreeViewImageSet.Custom : ((TimTreeViewImageSet)o);
			}
			set
			{
				this.ViewState["ImageSet"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("获取或设置拖动源树在拖动后是否保持树结构不变")]
		public bool KeepSteadyAfterDrop
		{
			get
			{
				object o = this.ViewState["KeepSteadyAfterDrop"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["KeepSteadyAfterDrop"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("是否延迟加载子节点")]
		private bool LazyLoad
		{
			get
			{
				object o = this.ViewState["LazyLoad"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["LazyLoad"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(20), Description("获取或设置子节点的左边缘与其父节点的左边缘之间的间距量（以像素为单位）")]
		public int NodeIndent
		{
			get
			{
				object o = this.ViewState["NodeIndent"];
				return (o == null) ? 20 : ((int)o);
			}
			set
			{
				this.ViewState["NodeIndent"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(TimNodeTagType.A), Description("获取或设置节点标签元素类型")]
		public TimNodeTagType NodeTagType
		{
			get
			{
				object o = this.ViewState["NodeTagType"];
				return (o == null) ? TimNodeTagType.A : ((TimNodeTagType)o);
			}
			set
			{
				this.ViewState["NodeTagType"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(""), Description("树控件在父容器中的滚动位置")]
		public string ScrollPosition
		{
			get
			{
				return this._ScrollPosition.Trim().Equals(string.Empty) ? "{top:0,left:0}" : this._ScrollPosition.Trim();
			}
			set
			{
				this._ScrollPosition = value;
			}
		}

		[Category("Behavior"), DefaultValue(TreeNodeTypes.None), Description("获取或设置是否启用多选功能")]
		public TreeNodeTypes ShowCheckBoxes
		{
			get
			{
				object o = this.ViewState["ShowCheckBoxes"];
				return (o == null) ? TreeNodeTypes.None : ((TreeNodeTypes)o);
			}
			set
			{
				this.ViewState["ShowCheckBoxes"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("获取或设置是否显示连接子节点和父节点的线条")]
		public bool ShowLines
		{
			get
			{
				object o = this.ViewState["ShowLines"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["ShowLines"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(true), Description("获取或设置是否显示节点提示信息")]
		public bool ShowTips
		{
			get
			{
				object o = this.ViewState["ShowTips"];
				return o == null || (bool)o;
			}
			set
			{
				this.ViewState["ShowTips"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(true), Description("当显示checkbox时，获取或设置是否使用客户端脚本进行勾选操作")]
		public bool UseClientAutoCheck
		{
			get
			{
				object o = this.ViewState["UseClientAutoCheck"];
				return o == null || (bool)o;
			}
			set
			{
				this.ViewState["UseClientAutoCheck"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("获取或设置是否启用右键菜单")]
		public bool UseContextMenu
		{
			get
			{
				object o = this.ViewState["UseContextMenu"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["UseContextMenu"] = value;
			}
		}

		[Category("Styles"), DefaultValue(null), Description("鼠标移上时的节点样式"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
		public Style HoverNodeStyle
		{
			get
			{
				bool flag = this._HoverNodeStyle == null;
				if (flag)
				{
					this._HoverNodeStyle = new Style();
				}
				return this._HoverNodeStyle;
			}
		}

		[Category("Styles"), DefaultValue(null), Description("叶节点样式"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
		internal Style LeafNodeStyle
		{
			get
			{
				bool flag = this._LeafNodeStyle == null;
				if (flag)
				{
					this._LeafNodeStyle = new Style();
				}
				return this._LeafNodeStyle;
			}
		}

		[Category("Styles"), DefaultValue(null), Description("节点样式"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
		public Style NodeStyle
		{
			get
			{
				bool flag = this._NodeStyle == null;
				if (flag)
				{
					this._NodeStyle = new Style();
				}
				return this._NodeStyle;
			}
		}

		[Category("Styles"), DefaultValue(null), Description("父节点样式"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
		internal Style ParentNodeStyle
		{
			get
			{
				bool flag = this._ParentNodeStyle == null;
				if (flag)
				{
					this._ParentNodeStyle = new Style();
				}
				return this._ParentNodeStyle;
			}
		}

		[Category("Styles"), DefaultValue(null), Description("选中节点样式"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
		public Style SelectedNodeStyle
		{
			get
			{
				bool flag = this._SelectedNodeStyle == null;
				if (flag)
				{
					this._SelectedNodeStyle = new Style();
				}
				return this._SelectedNodeStyle;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("AutoPostBack为true时，控制是否触发TreeLoad事件")]
		public bool EnableTreeLoad
		{
			get
			{
				object o = this.ViewState["EnableTreeLoad"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["EnableTreeLoad"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("控制是否触发DragDrop事件")]
		public bool EnableServerDragDrop
		{
			get
			{
				object o = this.ViewState["EnableServerDragDrop"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["EnableServerDragDrop"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("获取或设置右键菜单是否可用")]
		public bool EnableRightClickMenu
		{
			get
			{
				object o = this.ViewState["EnableRightClickMenu"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["EnableRightClickMenu"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("AutoPostBack为true时，控制是否触发SelectedNodeChanged事件")]
		public bool EnableSelectedNodeChanged
		{
			get
			{
				object o = this.ViewState["EnableSelectedNodeChanged"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["EnableSelectedNodeChanged"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("AutoPostBack为true时，控制是否触发TreeNodeDblClick事件")]
		public bool EnableTreeNodeDblClick
		{
			get
			{
				object o = this.ViewState["EnableTreeNodeDblClick"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["EnableTreeNodeDblClick"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("AutoPostBack为true时，控制是否触发TreeNodeCheckChanged事件")]
		public bool EnableTreeNodeCheckChanged
		{
			get
			{
				object o = this.ViewState["EnableTreeNodeCheckChanged"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["EnableTreeNodeCheckChanged"] = value;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("AutoPostBack为true时，控制是否触发TreeNodeExpanded事件")]
		public bool EnableTreeNodeExpanded
		{
			get
			{
				object o = this.ViewState["EnableTreeNodeExpanded"];
				return o != null && (bool)o;
			}
			set
			{
				this.ViewState["EnableTreeNodeExpanded"] = value;
			}
		}

		internal TimTreeViewNode RootNode
		{
			get
			{
				bool flag = this._RootNode == null;
				if (flag)
				{
					this._RootNode = new TimTreeViewNode(this, true);
				}
				return this._RootNode;
			}
		}

		[Browsable(false), Description("树节点集合")]
		public TimTreeViewNodeCollection Nodes
		{
			get
			{
				return this.RootNode.ChildNodes;
			}
			set
			{
				this.RootNode.ChildNodes = value;
			}
		}

		[Browsable(false), Description("当前选中节点所在分支的根节点")]
		public TimTreeViewNode BranchRoot
		{
			get
			{
				return this._BranchRoot;
			}
			set
			{
				this._BranchRoot = value;
			}
		}

		[Browsable(false), Description("当前选中节点")]
		public TimTreeViewNode SelectedNode
		{
			get
			{
				bool flag = this._SelectedNode == null;
				TimTreeViewNode result;
				if (flag)
				{
					result = this.Nodes.FindNodeByValueFromAllLeaf(this.SelectedValue);
				}
				else
				{
					result = this._SelectedNode;
				}
				return result;
			}
			set
			{
				this._SelectedNode = value;
				this._SelectedValue = ((value == null) ? string.Empty : value.Value);
			}
		}

		[Browsable(false), Description("当前选中节点的父节点")]
		public TimTreeViewNode SelectedParent
		{
			get
			{
				return this._SelectedNode.Parent;
			}
		}

		[Browsable(false), Description("当前树中的选中节点的value值")]
		public string SelectedValue
		{
			get
			{
				return this._SelectedValue.Replace("&quot;", "\"");
			}
			set
			{
				this._SelectedValue = value.Replace("\r\n", "").Replace("\"", "&quot;");
			}
		}

		[Browsable(false), Description("此次触发勾选事件的树节点value值")]
		public string CheckedValue
		{
			get
			{
				return this._CheckedValue.Replace("&quot;", "\"");
			}
		}

		[Browsable(false), Description("当前选中节点")]
		public TimTreeViewNode CheckedNode
		{
			get
			{
				bool flag = this._CheckedNode == null;
				TimTreeViewNode result;
				if (flag)
				{
					result = this.Nodes.FindNodeByValueFromAllLeaf(this.CheckedValue);
				}
				else
				{
					result = this._CheckedNode;
				}
				return result;
			}
			set
			{
				this._CheckedNode = value;
			}
		}

		[Browsable(false), Description("当前树中的勾选节点集合")]
		public TimTreeViewNodeCollection CheckedNodes
		{
			get
			{
				bool flag = this._CheckedNodes == null;
				if (flag)
				{
					this._CheckedNodes = new TimTreeViewNodeCollection(null, true);
				}
				return this._CheckedNodes;
			}
		}

		[Browsable(false), Description("当前选中节点")]
		public TimTreeViewNode ContextNode
		{
			get
			{
				bool flag = this._ContextNode == null;
				TimTreeViewNode result;
				if (flag)
				{
					result = this.Nodes.FindNodeByValueFromAllLeaf(this._ContextValue);
				}
				else
				{
					result = this._ContextNode;
				}
				return result;
			}
			set
			{
				this._ContextNode = value;
			}
		}

		internal string ContextValue
		{
			get
			{
				return this._ContextValue;
			}
			set
			{
				this._ContextValue = value;
			}
		}

		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		[Category("Action"), DefaultValue(""), Description("客户端节点拖动前事件")]
		public string OnClientBeforeDrag
		{
			get
			{
				object o = this.ViewState["OnClientBeforeDrag"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["OnClientBeforeDrag"] = value;
			}
		}

		[Category("Action"), DefaultValue(""), Description("客户端拖动时是否进行是否允许放下节点判断的事件")]
		public string OnClientCanDrop
		{
			get
			{
				object o = this.ViewState["OnClientCanDrop"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["OnClientCanDrop"] = value;
			}
		}

		[Category("Action"), DefaultValue(""), Description("客户端拖动节点放下前事件")]
		public string OnClientBeforeDrop
		{
			get
			{
				object o = this.ViewState["OnClientBeforeDrop"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["OnClientBeforeDrop"] = value;
			}
		}

		[Category("Action"), DefaultValue(""), Description("客户端拖动节点放下后事件")]
		public string OnClientAfterDrop
		{
			get
			{
				object o = this.ViewState["OnClientAfterDrop"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["OnClientAfterDrop"] = value;
			}
		}

		[Category("Action"), DefaultValue(""), Description("客户端节点切换事件")]
		public string OnClientSelectedNodeChanged
		{
			get
			{
				object o = this.ViewState["OnClientSelectedNodeChanged"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["OnClientSelectedNodeChanged"] = value;
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					this.EnableSelectedNodeChanged = true;
				}
			}
		}

		[Category("Action"), DefaultValue(""), Description("客户端节点双击事件")]
		public string OnClientTreeNodeDblClick
		{
			get
			{
				object o = this.ViewState["OnClientTreeNodeDblClick"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["OnClientTreeNodeDblClick"] = value;
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					this.EnableTreeNodeDblClick = true;
				}
			}
		}

		[Category("Action"), DefaultValue(""), Description("客户端节点右键菜单显示前事件")]
		public string OnClientBeforeContextMenu
		{
			get
			{
				object o = this.ViewState["OnClientBeforeContextMenu"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["OnClientBeforeContextMenu"] = value;
			}
		}

		[Category("Action"), DefaultValue(""), Description("客户端节点右键菜单点击事件")]
		public string OnClientMenuItemClick
		{
			get
			{
				object o = this.ViewState["OnClientMenuItemClick"];
				return (o == null) ? string.Empty : ((string)o);
			}
			set
			{
				this.ViewState["OnClientMenuItemClick"] = value;
			}
		}

		public TimTreeView()
		{
			this.NodeStyle.ForeColor = Color.Black;
			this.SelectedNodeStyle.ForeColor = Color.Black;
		}

		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor Descriptor = new ScriptControlDescriptor("TIM.T_WEBCTRL.TimTreeView", this.ClientID);
			Descriptor.AddProperty("activeEventsWhenReClick", this.ActiveEventsWhenReClick);
			Descriptor.AddProperty("alwaysReload", this.AlwaysReload);
			Descriptor.AddProperty("autoPostBack", this.AutoPostBack);
			Descriptor.AddProperty("canCustomizeCheck", this.CanCustomizeCheck);
			Descriptor.AddProperty("canAddBrotherForRoot", this.CanAddBrotherForRoot);
			Descriptor.AddProperty("canDragNodeImage", this.CanDragNodeImage);
			Descriptor.AddProperty("canDrop", this.CanDrop);
			Descriptor.AddProperty("clear", this._bClear);
			Descriptor.AddProperty("clientScriptLocation", this.Page.ResolveClientUrl("~/Scripts/Tim/"));
			Descriptor.AddProperty("clientImageLocation", this.Page.ResolveClientUrl("~/Images/Tim/"));
			Descriptor.AddProperty("collapseImageToolTip", this.CollapseImageToolTip);
			Descriptor.AddProperty("collapseImageUrl", this.CollapseImageUrl);
			Descriptor.AddProperty("nodeJSON", this.NodeJSON());
			Descriptor.AddProperty("dropMode", this.DropMode.ToString());
			Descriptor.AddProperty("enableRightClickMenu", this.EnableRightClickMenu);
			Descriptor.AddProperty("expandDepth", this.ExpandDepth);
			Descriptor.AddProperty("expandImageToolTip", this.ExpandImageToolTip);
			Descriptor.AddProperty("expandImageUrl", this.ExpandImageUrl);
			Descriptor.AddProperty("eventDatas", this.EventDatas.ToString());
			Descriptor.AddProperty("fatherContent", this.FatherContent.Trim());
			Descriptor.AddProperty("fireSelectEventAfterDrop", this.FireSelectEventAfterDrop);
			Descriptor.AddProperty("imageSet", this.ImageSet.ToString());
			Descriptor.AddProperty("keepSteadyAfterDrop", this.KeepSteadyAfterDrop);
			Descriptor.AddProperty("nodeIndent", this.NodeIndent);
			Descriptor.AddProperty("nodeTagType", this.NodeTagType.ToString());
			Descriptor.AddProperty("serverId", this.ID);
			Descriptor.AddProperty("showCheckBoxes", this.ShowCheckBoxes.ToString().ToUpper());
			Descriptor.AddProperty("showLines", this.ShowLines);
			Descriptor.AddProperty("showTips", this.ShowTips);
			Descriptor.AddProperty("treeDatas", this._bClear ? this.ToJSON(this.Nodes) : "[]");
			Descriptor.AddProperty("uniqueId", this.UniqueID);
			Descriptor.AddProperty("useClientAutoCheck", this.UseClientAutoCheck);
			Descriptor.AddProperty("useContextMenu", this.UseContextMenu);
			Descriptor.AddProperty("onClientBeforeContextMenu", this.OnClientBeforeContextMenu);
			Descriptor.AddProperty("onClientBeforeDrag", this.OnClientBeforeDrag);
			Descriptor.AddProperty("onClientCanDrop", this.OnClientCanDrop);
			Descriptor.AddProperty("onClientBeforeDrop", this.OnClientBeforeDrop);
			Descriptor.AddProperty("onClientAfterDrop", this.OnClientAfterDrop);
			Descriptor.AddProperty("onClientSelectedNodeChanged", this.OnClientSelectedNodeChanged);
			Descriptor.AddProperty("onClientTreeNodeDblClick", this.OnClientTreeNodeDblClick);
			Descriptor.AddProperty("onMenuItemClientClick", this.OnClientMenuItemClick);
			Descriptor.AddProperty("enableServerDragDrop", this.EnableServerDragDrop);
			Descriptor.AddProperty("enableSelectedNodeChanged", this.EnableSelectedNodeChanged);
			Descriptor.AddProperty("enableTreeNodeDblClick", this.EnableTreeNodeDblClick);
			Descriptor.AddProperty("enableTreeNodeCheckChanged", this.EnableTreeNodeCheckChanged);
			Descriptor.AddProperty("enableTreeNodeExpanded", this.EnableTreeNodeExpanded);
			return new ScriptControlDescriptor[]
			{
				Descriptor
			};
		}

		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			string path = this.Page.ResolveClientUrl("~/Scripts/Tim/");
			bool flag = !this.Page.ClientScript.IsClientScriptBlockRegistered("TimCommon");
			IEnumerable<ScriptReference> result;
			if (flag)
			{
				result = new ScriptReference[]
				{
					new ScriptReference(string.Format("{0}TimCommon.js?v=" + TimCtrlUtils.Md5Version, path)),
					new ScriptReference(string.Format("{0}TimButtonMenu.js?v=" + TimCtrlUtils.Md5Version, path)),
					new ScriptReference(string.Format("{0}TimTreeView.js?v=" + TimCtrlUtils.Md5Version, path))
				};
			}
			else
			{
				result = new ScriptReference[]
				{
					new ScriptReference(string.Format("{0}TimButtonMenu.js?v=" + TimCtrlUtils.Md5Version, path)),
					new ScriptReference(string.Format("{0}TimTreeView.js?v=" + TimCtrlUtils.Md5Version, path))
				};
			}
			return result;
		}

		protected override void OnPreRender(EventArgs e)
		{
			bool isGtPostBack = this.IsGtPostBack;
			if (!isGtPostBack)
			{
				bool flag = !base.DesignMode;
				if (flag)
				{
					this.RegisterCssToPage("TimTreeView__CssLink", "TimTreeView.css");
					this.RegisterTreeNodeStyle();
					this.RegisterHiddenField();
					this.SM = ScriptManager.GetCurrent(this.Page);
					bool flag2 = this.SM == null;
					if (flag2)
					{
						throw new HttpException("A ScriptControl must exists on current page");
					}
					this.SM.RegisterScriptControl<TimTreeView>(this);
					this.Page.RegisterRequiresPostBack(this);
					foreach (TimTreeViewNode child in this.Nodes)
					{
						bool flag3 = child.Depth < this.ExpandDepth && child.PopulateOnDemand;
						if (flag3)
						{
							child.PopulateOnDemand = false;
							bool flag4 = child.ChildNodes.Count == 0;
							if (flag4)
							{
								this.OnTreeNodePopulate(new TimTreeViewNodeEventArgs(child));
							}
						}
						this.ExpandToDepth(child);
					}
				}
				base.OnPreRender(e);
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			bool flag = !base.DesignMode;
			if (flag)
			{
				this.SM.RegisterScriptDescriptors(this);
				base.Attributes.Add("name", this.UniqueID);
			}
			base.Render(writer);
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			bool flag = HttpContext.Current != null;
			if (flag)
			{
				bool flag2 = this.ClientID == HttpContext.Current.Request["TrvId"];
				if (flag2)
				{
					this.IsGtPostBack = true;
					string sResponseContent = string.Empty;
					HttpRequest request = HttpContext.Current.Request;
					string function = request["Function"];
					this.SelectedValue = string.Empty;
					bool flag3 = "DragDrop" == function;
					if (flag3)
					{
						this.DoDragDropEvent(ref sResponseContent, request);
					}
					else
					{
						bool flag4 = "TreeNodePopulate" == function;
						if (flag4)
						{
							this.DoPopulateEvent(ref sResponseContent, request);
						}
					}
					HttpResponse response = HttpContext.Current.Response;
					response.ContentType = "text/xml";
					response.Charset = "utf-8";
					response.Output.Write(sResponseContent);
					response.End();
				}
			}
			bool enableTreeLoad = this.EnableTreeLoad;
			if (enableTreeLoad)
			{
				this.OnTreeLoad(e);
			}
		}

		protected virtual void OnDragDrop(TimTreeViewDragEventArgs e)
		{
			DragDropEventHandler handler = (DragDropEventHandler)base.Events[TimTreeView.EventDragDrop];
			bool flag = handler != null;
			if (flag)
			{
				handler(this, e);
			}
		}

		protected virtual void OnTreeLoad(EventArgs e)
		{
			TreeLoadEventHandler handler = (TreeLoadEventHandler)base.Events[TimTreeView.EventTreeLoad];
			bool flag = handler != null;
			if (flag)
			{
				handler(this, e);
			}
		}

		protected virtual void OnSelectedNodeChanged(TimTreeViewNodeEventArgs e)
		{
			SelectedNodeChangedEventHandler handler = (SelectedNodeChangedEventHandler)base.Events[TimTreeView.EventSelectedNodeChanged];
			bool flag = handler != null;
			if (flag)
			{
				handler(this, e);
			}
		}

		protected virtual void OnTreeNodeDblClick(TimTreeViewNodeEventArgs e)
		{
			TreeNodeDblClickEventHandler handler = (TreeNodeDblClickEventHandler)base.Events[TimTreeView.EventTreeNodeDblClick];
			bool flag = handler != null;
			if (flag)
			{
				handler(this, e);
			}
		}

		protected virtual void OnTreeNodeCheckChanged(TimTreeViewNodeEventArgs e)
		{
			TreeNodeCheckChangedEventHandler handler = (TreeNodeCheckChangedEventHandler)base.Events[TimTreeView.EventTreeNodeCheckChanged];
			bool flag = handler != null;
			if (flag)
			{
				handler(this, e);
			}
		}

		protected virtual void OnTreeNodeExpanded(TimTreeViewNodeEventArgs e)
		{
			TreeNodeExpandedEventHandler handler = (TreeNodeExpandedEventHandler)base.Events[TimTreeView.EventTreeNodeExpanded];
			bool flag = handler != null;
			if (flag)
			{
				handler(this, e);
			}
		}

		internal virtual void OnTreeNodePopulate(TimTreeViewNodeEventArgs e)
		{
			TreeNodePopulateEventHandler handler = (TreeNodePopulateEventHandler)base.Events[TimTreeView.EventTreeNodePopulate];
			bool flag = handler != null;
			if (flag)
			{
				handler(this, e);
			}
		}

		protected virtual void OnTreeNodeMenuClick(TimTreeViewMenuClickEventArgs e)
		{
			TreeNodeMenuClickEventHandler handler = (TreeNodeMenuClickEventHandler)base.Events[TimTreeView.EventTreeNodeMenuClick];
			bool flag = handler != null;
			if (flag)
			{
				handler(this, e);
			}
		}

		public void RaisePostBackEvent(string eventArgument)
		{
			bool flag = eventArgument.Length != 0;
			if (flag)
			{
				bool flag2 = eventArgument.StartsWith("TreeLoad");
				if (flag2)
				{
					TimTreeViewNodeEventArgs e = new TimTreeViewNodeEventArgs(this.SelectedNode);
					this.OnTreeLoad(e);
				}
				else
				{
					bool flag3 = eventArgument.StartsWith("SelectedNodeChanged");
					if (flag3)
					{
						TimTreeViewNodeEventArgs e = new TimTreeViewNodeEventArgs(this.SelectedNode);
						this.OnSelectedNodeChanged(e);
					}
					else
					{
						bool flag4 = eventArgument.StartsWith("TreeNodeDblClick");
						if (flag4)
						{
							TimTreeViewNodeEventArgs e = new TimTreeViewNodeEventArgs(this.SelectedNode);
							this.OnTreeNodeDblClick(e);
						}
						else
						{
							bool flag5 = eventArgument.StartsWith("TreeNodeExpanded");
							if (flag5)
							{
								TimTreeViewNodeEventArgs e = new TimTreeViewNodeEventArgs(this.PopulatedNode);
								this.OnTreeNodeExpanded(e);
							}
							else
							{
								bool flag6 = !eventArgument.Trim().Equals(string.Empty);
								if (flag6)
								{
									TimTreeViewMenuClickEventArgs ee = new TimTreeViewMenuClickEventArgs(eventArgument.Trim(), this.ContextNode);
									this.OnTreeNodeMenuClick(ee);
								}
							}
						}
					}
				}
			}
		}

		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool flag = "TreeNodePopulate" == HttpContext.Current.Request["Function"];
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this._bClear = false;
				bool flag2 = !this.FatherContent.Trim().Equals(string.Empty);
				if (flag2)
				{
					this.ScrollPosition = postCollection[string.Format("{0}__ScrollPosition", this.ClientID)];
				}
				this.PopulatedValue = postCollection[string.Format("{0}__PopulatedValue", this.ClientID)];
				bool flag3 = this.ShowCheckBoxes > TreeNodeTypes.None;
				if (flag3)
				{
					this._CheckedValue = postCollection[string.Format("{0}__CheckedValue", this.ClientID)];
				}
				bool useContextMenu = this.UseContextMenu;
				if (useContextMenu)
				{
					this._ContextValue = postCollection[string.Format("{0}__ContextValue", this.ClientID)];
				}
				this._PreSelectedValue = postCollection[string.Format("{0}__PreSelectedValue", this.ClientID)];
				this.GetPostObject(postCollection[string.Format("{0}__SelectedValue", this.ClientID)], postCollection[string.Format("{0}__PostData", this.ClientID)], TimTreeViewPostType.Other);
				result = true;
			}
			return result;
		}

		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		public void RaisePostDataChangedEvent()
		{
			bool flag = !string.IsNullOrEmpty(this.CheckedValue.Trim());
			if (flag)
			{
				TimTreeViewNodeEventArgs e = new TimTreeViewNodeEventArgs(this.CheckedNode);
				this.OnTreeNodeCheckChanged(e);
			}
		}

		private void ExpandToDepth(TimTreeViewNode node)
		{
			bool flag = ((node.ChildNodes.Count > 0 || (node.ChildNodes.Count == 0 && node.PopulateOnDemand)) && node.Depth < this.ExpandDepth) || -1 == this.ExpandDepth;
			if (flag)
			{
				node.Expand();
				bool flag2 = node.Depth + 1 < this.ExpandDepth;
				if (flag2)
				{
					foreach (TimTreeViewNode child in node.ChildNodes)
					{
						bool populateOnDemand = child.PopulateOnDemand;
						if (populateOnDemand)
						{
							child.PopulateOnDemand = false;
							bool flag3 = child.ChildNodes.Count == 0;
							if (flag3)
							{
								this.OnTreeNodePopulate(new TimTreeViewNodeEventArgs(child));
							}
						}
						this.ExpandToDepth(child);
					}
				}
			}
		}

		private void GetPostObject(string selected, string postData, TimTreeViewPostType postType)
		{
			bool flag = !string.IsNullOrEmpty(selected);
			if (flag)
			{
				this.SelectedValue = selected;
			}
			bool flag2 = string.IsNullOrEmpty(postData);
			if (!flag2)
			{
				TimTreeViewEventDatas eventDatas = this.EventDatas;
				if (eventDatas == TimTreeViewEventDatas.All)
				{
					this.Nodes = this.ToTreeNodeCollectioin(postData);
					this.Nodes.Owner = this.RootNode;
					this.RootNode.ChildNodes = this.Nodes;
					this.RootNode.IsRoot = true;
					this.Nodes.CreateFamilyRelation(this, this.SelectedValue, postType);
				}
			}
		}

		private TimTreeViewNodeCollection GetPostObject(string dragDropData)
		{
			return this.ToTreeNodeCollectioin(dragDropData);
		}

		private string GetPostData()
		{
			string postData = string.Empty;
			TimTreeViewEventDatas eventDatas = this.EventDatas;
			if (eventDatas == TimTreeViewEventDatas.All)
			{
				postData = this.ToJSON(this.Nodes);
			}
			return postData;
		}

		private void RegisterStyle(Style style, string name)
		{
			string cssName = "";
			if (!(name == "treeStyle"))
			{
				if (!(name == "Hover"))
				{
					if (!(name == "Leaf"))
					{
						if (!(name == "Node"))
						{
							if (!(name == "Parent"))
							{
								if (name == "Selected")
								{
									cssName = "." + this.ClientID + "_Selected";
								}
							}
							else
							{
								cssName = "";
							}
						}
						else
						{
							cssName = "." + this.ClientID + "_Node";
						}
					}
					else
					{
						cssName = "";
					}
				}
				else
				{
					cssName = "#" + this.ClientID + " A:hover";
				}
			}
			else
			{
				cssName = "#" + this.ClientID + " A:visited, A:active, A:focus, A:hover, A:link";
			}
			bool flag = !style.IsEmpty && this.Page != null && cssName.Length != 0;
			if (flag)
			{
				this.Page.Header.StyleSheet.CreateStyleRule(style, this, cssName);
			}
		}

		private void RegisterTreeNodeStyle()
		{
			bool flag = this._HoverNodeStyle != null;
			if (flag)
			{
				this.RegisterStyle(this._HoverNodeStyle, "Hover");
			}
			bool flag2 = this._LeafNodeStyle != null;
			if (flag2)
			{
				this.RegisterStyle(this._LeafNodeStyle, "Leaf");
			}
			bool flag3 = this._NodeStyle != null;
			if (flag3)
			{
				this.RegisterStyle(this._NodeStyle, "Node");
			}
			bool flag4 = this._ParentNodeStyle != null;
			if (flag4)
			{
				this.RegisterStyle(this._ParentNodeStyle, "Parent");
			}
			bool flag5 = this._SelectedNodeStyle != null;
			if (flag5)
			{
				this.RegisterStyle(this._SelectedNodeStyle, "Selected");
			}
		}

		private void RegisterCssToPage(string id, string file)
		{
			HtmlLink link = new HtmlLink();
			link.ID = id;
			link.Href = string.Format("{0}{1}", this.Page.ResolveClientUrl("~/Scripts/Tim/"), file);
			link.Attributes.Add("rel", "stylesheet");
			link.Attributes.Add("type", "text/css");
			bool flag = this.Page.Header.FindControl(id) == null;
			if (flag)
			{
				this.Page.Header.Controls.Add(link);
			}
		}

		private void RegisterHiddenField()
		{
			ScriptManager.RegisterHiddenField(this, string.Format("{0}__PostData", this.ClientID), this._bClear ? "[]" : this.GetPostData());
			ScriptManager.RegisterHiddenField(this, string.Format("{0}__SelectedValue", this.ClientID), this.SelectedValue);
			ScriptManager.RegisterHiddenField(this, string.Format("{0}__PopulatedValue", this.ClientID), string.Empty);
			ScriptManager.RegisterHiddenField(this, string.Format("{0}__PreSelectedValue", this.ClientID), this._PreSelectedValue);
			bool flag = this.ShowCheckBoxes > TreeNodeTypes.None;
			if (flag)
			{
				ScriptManager.RegisterHiddenField(this, string.Format("{0}__CheckedValue", this.ClientID), string.Empty);
			}
			bool flag2 = !string.IsNullOrEmpty(this.FatherContent.Trim());
			if (flag2)
			{
				ScriptManager.RegisterHiddenField(this, string.Format("{0}__ScrollPosition", this.ClientID), this.ScrollPosition);
			}
			bool useContextMenu = this.UseContextMenu;
			if (useContextMenu)
			{
				ScriptManager.RegisterHiddenField(this, string.Format("{0}__ContextValue", this.ClientID), string.Empty);
			}
		}

		private TimTreeViewDropMode GetDropDownMode(string type)
		{
			TimTreeViewDropMode result;
			if (!(type == "0"))
			{
				if (!(type == "1"))
				{
					if (!(type == "2"))
					{
						result = TimTreeViewDropMode.AppendLast;
					}
					else
					{
						result = TimTreeViewDropMode.InsertAfter;
					}
				}
				else
				{
					result = TimTreeViewDropMode.InsertBefore;
				}
			}
			else
			{
				result = TimTreeViewDropMode.AppendLast;
			}
			return result;
		}

		private void DoDragDropEvent(ref string result, HttpRequest request)
		{
			this.TargetValue = request["TargetValue"];
			this.SourceValue = request["SourceValue"];
			TimTreeViewDropMode dropDownMode = this.GetDropDownMode(request["DropDownMode"]);
			string targetId = request["TargetId"];
			string sourceId = request["SourceId"];
			string targetBranch = request["TargetBranch"];
			string sourceBranch = request["SourceBranch"];
			TimTreeViewNode TargetBranch = this.ToTreeNode(targetBranch);
			TargetBranch.CreateFamilyRelation(this, this.SelectedValue, TimTreeViewPostType.Target);
			TimTreeViewNode SourceBranch = this.ToTreeNode(sourceBranch);
			SourceBranch.CreateFamilyRelation(this, this.SelectedValue, TimTreeViewPostType.Source);
			TimTreeViewDragEventArgs ge = new TimTreeViewDragEventArgs(this.TargetNode, this.SourceNode);
			ge.initArgs(TargetBranch, SourceBranch, targetId, sourceId, dropDownMode);
			this.OnDragDrop(ge);
			bool flag = ge.Result && ge.UpdateSource;
			if (flag)
			{
				result = this.ToJSON(ge.Source);
			}
			else
			{
				result = string.Concat(new string[]
				{
					"{result:",
					ge.Result.ToString().ToLower(),
					",message:'",
					ge.ErrorMessage,
					"'}"
				});
			}
		}

		private void DoPopulateEvent(ref string result, HttpRequest request)
		{
			string postData = request["PostData"];
			this.PopulatedValue = request["PopulatedValue"];
			this.GetPostObject(this.SelectedValue, postData, TimTreeViewPostType.Pop);
			TimTreeViewNodeEventArgs pe = new TimTreeViewNodeEventArgs(this.PopulatedNode);
			this.OnTreeNodePopulate(pe);
			result = this.ToJSON(pe.Node);
		}

		private string NodeJSON()
		{
			JavaScriptSerializer nodeSerializer = new JavaScriptSerializer();
			nodeSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new MenuItemsConverter()
			});
			string sb = nodeSerializer.Serialize(this.ContextMenu);
			return "[" + sb.ToString() + "]";
		}

		private string ToJSON(TimTreeViewNode node)
		{
			bool flag = node == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = JsonConvert.SerializeObject(node);
			}
			return result;
		}

		private string ToJSON(TimTreeViewNodeCollection nodes)
		{
			bool flag = nodes == null;
			string result;
			if (flag)
			{
				result = "[]";
			}
			else
			{
				result = JsonConvert.SerializeObject(nodes);
			}
			return result;
		}

		private TimTreeViewNode ToTreeNode(string JSON)
		{
			return (JSON.Length == 0) ? null : JsonConvert.DeserializeObject<TimTreeViewNode>(JSON);
		}

		private TimTreeViewNodeCollection ToTreeNodeCollectioin(string JSON)
		{
			return JsonConvert.DeserializeObject<TimTreeViewNodeCollection>(JSON);
		}

		internal void Clear()
		{
			bool flag = this.BranchRoot != null;
			if (flag)
			{
				this.BranchRoot.ChildNodes.Clear();
				this.BranchRoot = null;
			}
			this.SelectedNode = null;
			this._bClear = true;
		}

		public void CollapseAll()
		{
			this.RootNode.CollapseAll();
		}

		public void ExpandAll()
		{
			this.RootNode.ExpandAll();
		}

		public void InitEvents(bool enableSelectedNodeChanged, bool enableTreeNodeExpanded, bool enableTreeNodeCheckChanged, bool enableTreeNodeDblClick)
		{
			this.EnableSelectedNodeChanged = enableSelectedNodeChanged;
			this.EnableTreeNodeExpanded = enableTreeNodeExpanded;
			this.EnableTreeNodeCheckChanged = enableTreeNodeCheckChanged;
			this.EnableTreeNodeDblClick = enableTreeNodeDblClick;
		}

		public TimTreeViewNode FindNodeByValue(string value)
		{
			return this.Nodes.FindNodeByValueFromAllLeaf(value);
		}

		public TimTreeViewNode FindNodeByValue(TimTreeViewNode node, string value)
		{
			return node.FindChildNodeByValueFromAllLeaf(value);
		}
	}
}
