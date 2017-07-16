using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TIM.T_TEMPLET.Master;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Page
{
	public class TTreeChartBase : PageBase
	{
		private TTreeChart m_curMaster = null;

		private int m_treeWidth = 0;

		private Hashtable m_treeImgList = new Hashtable();

		private bool m_enabledSelectedTreeNodeChanged = true;

		private bool m_enabledTreeNodeCheckChanged = false;

		private bool m_enabledTreeNodeDblClick = false;

		private bool m_enabledTreeNodeExpanded = false;

		private string[] m_dataKeyNames;

		public TTreeChart CurMaster
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

		public int TreeWidth
		{
			get
			{
				return this.m_treeWidth;
			}
			set
			{
				this.m_treeWidth = value;
			}
		}

		public Hashtable TreeImgList
		{
			get
			{
				return this.m_treeImgList;
			}
			set
			{
				this.m_treeImgList = value;
			}
		}

		public bool EnabledSelectedTreeNodeChanged
		{
			get
			{
				return this.m_enabledSelectedTreeNodeChanged;
			}
			set
			{
				this.m_enabledSelectedTreeNodeChanged = value;
			}
		}

		public bool EnabledTreeNodeCheckChanged
		{
			get
			{
				return this.m_enabledTreeNodeCheckChanged;
			}
			set
			{
				this.m_enabledTreeNodeCheckChanged = value;
			}
		}

		public bool EnabledTreeNodeDblClick
		{
			get
			{
				return this.m_enabledTreeNodeDblClick;
			}
			set
			{
				this.m_enabledTreeNodeDblClick = value;
			}
		}

		public bool EnabledTreeNodeExpanded
		{
			get
			{
				return this.m_enabledTreeNodeExpanded;
			}
			set
			{
				this.m_enabledTreeNodeExpanded = value;
			}
		}

		public new string[] DataKeyNames
		{
			get
			{
				return this.m_dataKeyNames;
			}
			set
			{
				this.m_dataKeyNames = value;
				List<string> keyList = this.m_dataKeyNames.ToList<string>();
				bool flag = !keyList.Contains("MODIFIEDTIME");
				if (flag)
				{
					keyList.Insert(0, "MODIFIEDTIME");
				}
				bool flag2 = !keyList.Contains("MODIFIER");
				if (flag2)
				{
					keyList.Insert(0, "MODIFIER");
				}
				bool flag3 = !keyList.Contains("MODIFIERID");
				if (flag3)
				{
					keyList.Insert(0, "MODIFIERID");
				}
				this.m_dataKeyNames = keyList.ToArray();
			}
		}

		private void BuildEvent()
		{
			this.CurMaster.OnTreeNodeChanged += new TMasterBase.TreeNodeChanged(this.CurMaster_OnTreeNodeChanged);
			this.CurMaster.OnTreeNodePopulate += new TMasterBase.TreeNodePopulate(this.CurMaster_OnTreeNodePopulate);
			this.CurMaster.OnTreeNodeExpand += new TMasterBase.TreeNodeExpand(this.CurMaster_OnTreeNodeExpand);
			this.CurMaster.OnTreeNodeDragDrop += new TMasterBase.TreeNodeDragDrop(this.CurMaster_OnTreeNodeDragDrop);
			this.CurMaster.OnTreeNodeCheckChanged += new TMasterBase.TreeNodeCheckChanged(this.CurMaster_OnTreeNodeCheckChanged);
			this.CurMaster.OnQuery += new TMasterBase.Query(this.CurMaster_OnQuery);
		}

		public TimTreeViewNode TreeAddNode(TimTreeViewNode fatherNode, string text, string value, string imgKey, bool selectAction, bool populateOnDemand, bool needExpandChildrenNode)
		{
			bool flag = this.TreeImgList.Count <= 0 || string.IsNullOrWhiteSpace(imgKey) || !this.TreeImgList.ContainsKey(imgKey);
			TimTreeViewNode addNode;
			if (flag)
			{
				addNode = new TimTreeViewNode(text, value);
			}
			else
			{
				addNode = new TimTreeViewNode(text, value, this.TreeImgList[imgKey].ToString());
			}
			addNode.ToolTip = text;
			addNode.PopulateOnDemand = populateOnDemand;
			bool flag2 = this.CurMaster.LeftTreeV.Nodes.Count <= 0;
			if (flag2)
			{
				this.CurMaster.LeftTreeV.SelectedNode = addNode;
			}
			bool flag3 = fatherNode == null;
			if (flag3)
			{
				this.CurMaster.LeftTreeV.Nodes.Add(addNode);
			}
			else
			{
				fatherNode.ChildNodes.Add(addNode);
			}
			bool flag4 = !selectAction;
			if (flag4)
			{
				addNode.SelectAction = TreeNodeSelectAction.None;
			}
			if (needExpandChildrenNode)
			{
			}
			return addNode;
		}

		protected void OnTreeNodePreChanged(object sender, TimTreeViewNodeEventArgs e)
		{
		}

		private void CurMaster_OnTreeNodeChanged(object sender, TimTreeViewNodeEventArgs e)
		{
			this.OnQuery();
			this.ChartDataBind();
			this.PlaceUpdateTree();
			this.PlaceUpdateMenu();
			this.PlaceUpdateContent();
		}

		private void CurMaster_OnTreeNodePopulate(object sender, TimTreeViewNodeEventArgs e)
		{
		}

		private void CurMaster_OnTreeNodeExpand(object sender, TimTreeViewNodeEventArgs e)
		{
		}

		private void CurMaster_OnTreeNodeDragDrop(object sender, TimTreeViewDragEventArgs e)
		{
		}

		private void CurMaster_OnTreeNodeCheckChanged(object sender, TimTreeViewNodeEventArgs e)
		{
		}

		protected void ClearTreeNode()
		{
			this.CurMaster.LeftTreeV.Nodes.Clear();
		}

		protected sealed override bool OnQuery()
		{
			bool result = false;
			bool flag = !this.VerifyQuery();
			bool result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				bool flag2 = !this.OnPreQuery();
				if (flag2)
				{
					result2 = result;
				}
				else
				{
					base.CurEntity.RecordSetSql = base.CurEntity.BuildRecordSetSql();
					bool flag3 = !base.OnQuery();
					if (flag3)
					{
						result2 = result;
					}
					else
					{
						base.CurEntity.QueryRecordSet();
						bool flag4 = !this.OnQueryComplete();
						result2 = (!flag4 || result);
					}
				}
			}
			return result2;
		}

		protected void CurMaster_OnQuery(object sender, EventArgs e)
		{
			bool flag = this.RebuildTree();
			if (flag)
			{
				this.PlaceUpdateTree();
			}
			bool flag2 = !this.OnQuery();
			if (!flag2)
			{
				this.ChartDataBind();
				this.PlaceUpdateContent();
			}
		}

		protected virtual void ChartDataBind()
		{
		}

		private void SetCtrlStatus()
		{
			this.CurMaster.SetCtrlStatus();
		}

		protected sealed override void OnInit()
		{
			this.BuildEvent();
			this.TreeImgList.Add("TreeFolder", "../../Images/Tim/folder.gif");
			this.TreeImgList.Add("TreeFolderOpen", "../../Images/Tim/folderopen.gif");
			this.TreeImgList.Add("TreeLeaf", "../../Images/Tim/treeleaf.gif");
			this.CurMaster.LeftTreeV.InitEvents(this.EnabledSelectedTreeNodeChanged, this.EnabledTreeNodeExpanded, this.EnabledTreeNodeCheckChanged, this.EnabledTreeNodeDblClick);
		}

		protected override void OnLoad()
		{
			bool flag = !base.IsPostBack;
			if (flag)
			{
				this.OnPreLoadRecord();
				this.OnInitTree();
				this.OnLoadRecord();
				this.OnLoadRecordComplete();
			}
		}

		protected sealed override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);
			this.OnLoadComplete();
			bool flag = !base.IsPostBack;
			if (flag)
			{
				string moduleClientVar = string.Empty;
				moduleClientVar = moduleClientVar + " var _EditingPage= '" + base.EditingPage + "';";
				base.RegisterScript("TempletClientVar", moduleClientVar);
			}
			base.SetMasterCtrlState();
			this.SetPageCtrlState();
		}

		internal override void SetTempletCtrlState()
		{
		}

		private void SetPageCtrlState()
		{
			this.SetCtrlState();
			this.SetPageUrlAppendParam();
			base.RegisterScript("TempletClientAppendVar", "var _PageUrlAppendParam = '" + base.PageUrlAppendParam.EncodedParameters + "';");
		}

		protected override void SetMenu_OnlyQuery()
		{
			this.CurMaster.SetMenu_OnlyQuery();
		}

		protected override void SetMenu_HideAllStdBtn()
		{
			this.CurMaster.SetMenu_HideAllStdBtn();
		}

		protected override void SetMenu_OnlyViewEdit()
		{
			this.CurMaster.SetMenu_OnlyViewEdit();
		}

		protected override void PlaceUpdateScript()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpScriptPlace.Update();
			}
		}

		protected void PlaceUpdateTree()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpTreePlace.Update();
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

		protected void PlaceUpdateQuery()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpQueryPlace.Update();
			}
		}

		protected void PlaceUpdateContent()
		{
			bool isPostBack = base.IsPostBack;
			if (isPostBack)
			{
				this.CurMaster.UpContentPlace.Update();
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
	}
}
