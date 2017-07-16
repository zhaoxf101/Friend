using Newtonsoft.Json;
using System;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	public class TimTreeViewNode
	{
		[JsonIgnore]
		private string _Value = string.Empty;

		[JsonIgnore]
		private bool _Expanded = false;

		[JsonIgnore]
		private bool _Checked = false;

		[JsonIgnore]
		private string _Text = string.Empty;

		[JsonIgnore]
		private bool _CanAddBrotherForChild = true;

		[JsonIgnore]
		private bool _CanIndent = true;

		[JsonIgnore]
		private bool _Draggable = true;

		[JsonIgnore]
		private string _Extend = string.Empty;

		[JsonIgnore]
		private bool _Folder = true;

		[JsonIgnore]
		private string _ImageUrl = string.Empty;

		[JsonIgnore]
		private string _ImageExpandedUrl = string.Empty;

		[JsonIgnore]
		private bool _IsRoot = false;

		[JsonIgnore]
		private bool _PopulateOnDemand = false;

		[JsonIgnore]
		private bool _Selected = false;

		[JsonIgnore]
		private TreeNodeSelectAction m_selectAction = TreeNodeSelectAction.Select;

		[JsonIgnore]
		private bool _ShowCheckBox = false;

		[JsonIgnore]
		private string _Target = string.Empty;

		[JsonIgnore]
		private string _ValueFix = string.Empty;

		[JsonIgnore]
		private string _ToolTip = string.Empty;

		[JsonIgnore]
		internal TimTreeView _Owner = null;

		[JsonIgnore]
		private TimTreeViewNode _Parent = null;

		[JsonIgnore]
		private TimTreeViewNodeCollection _ChildNodes = null;

		[JsonProperty("val")]
		public string Value
		{
			get
			{
				return this._Value.Replace("&quot;", "\"");
			}
			set
			{
				this._Value = value.Replace("\r\n", "").Replace("\"", "&quot;");
			}
		}

		[JsonIgnore]
		public string ValueWithoutFix
		{
			get
			{
				bool flag = this.ValueFix.Length > 0 && this.Value.EndsWith(this.ValueFix);
				string result;
				if (flag)
				{
					result = this.Value.Remove(this.Value.Length - this.ValueFix.Length);
				}
				else
				{
					result = this.Value;
				}
				return result;
			}
		}

		[JsonProperty("epd")]
		public bool Expanded
		{
			get
			{
				return this._Expanded;
			}
			set
			{
				this._Expanded = value;
			}
		}

		[JsonProperty("ckd")]
		public bool Checked
		{
			get
			{
				return this._Checked;
			}
			set
			{
				this._Checked = value;
			}
		}

		[JsonProperty("txt")]
		public string Text
		{
			get
			{
				return this._Text.Replace("&quot;", "\"");
			}
			set
			{
				this._Text = value.Replace("\r\n", "").Replace("\"", "&quot;");
			}
		}

		[JsonProperty("cab")]
		public bool CanAddBrotherForChild
		{
			get
			{
				return this._CanAddBrotherForChild;
			}
			set
			{
				this._CanAddBrotherForChild = value;
			}
		}

		[JsonProperty("cit")]
		public bool CanIndent
		{
			get
			{
				return this._CanIndent;
			}
			set
			{
				this._CanIndent = value;
			}
		}

		[JsonIgnore]
		public int Depth
		{
			get
			{
				int dep = 0;
				TimTreeViewNode prt = this.Parent;
				while (prt != null && !prt.IsRoot)
				{
					dep++;
					prt = prt.Parent;
				}
				return dep;
			}
		}

		[JsonProperty("dra")]
		public bool Draggable
		{
			get
			{
				return this._Draggable;
			}
			set
			{
				this._Draggable = value;
			}
		}

		[JsonProperty("ext")]
		public string Extend
		{
			get
			{
				return this._Extend.Replace("&quot;", "\"");
			}
			set
			{
				this._Extend = value.Replace("\r\n", "").Replace("\"", "&quot;");
			}
		}

		[JsonIgnore]
		public string Fix
		{
			get
			{
				bool flag = this.Parent != null && !this.Parent.IsRoot;
				string result;
				if (flag)
				{
					result = this.Parent.Fix + this.Parent.ChildNodes.IndexOf(this).ToString() + "~";
				}
				else
				{
					result = this.Owner.Nodes.IndexOf(this).ToString() + "~";
				}
				return result;
			}
		}

		[JsonProperty("fld")]
		public bool Folder
		{
			get
			{
				return this._Folder;
			}
			set
			{
				this._Folder = value;
			}
		}

		[JsonProperty("img")]
		public string ImageUrl
		{
			get
			{
				return this._ImageUrl;
			}
			set
			{
				this._ImageUrl = value;
			}
		}

		[JsonProperty("omg")]
		public string ImageExpandedUrl
		{
			get
			{
				return this._ImageExpandedUrl;
			}
			set
			{
				this._ImageExpandedUrl = value;
			}
		}

		[JsonIgnore]
		internal bool IsRoot
		{
			get
			{
				return this._IsRoot;
			}
			set
			{
				this._IsRoot = value;
			}
		}

		[JsonProperty("pop")]
		public bool PopulateOnDemand
		{
			get
			{
				return this._PopulateOnDemand;
			}
			set
			{
				this._PopulateOnDemand = value;
			}
		}

		[JsonIgnore]
		public bool Selected
		{
			get
			{
				return this.Value.Equals(this.Owner.SelectedValue);
			}
			set
			{
				this._Selected = value;
				bool flag = this.Owner != null;
				if (flag)
				{
					this.Owner.SelectedNode = this;
				}
			}
		}

		[JsonIgnore]
		public TreeNodeSelectAction SelectAction
		{
			get
			{
				return this.m_selectAction;
			}
			set
			{
				this.m_selectAction = value;
			}
		}

		[JsonProperty("act")]
		public string GetSelectAction
		{
			get
			{
				return this.m_selectAction.ToString();
			}
			set
			{
				if (!(value == "Select"))
				{
					if (!(value == "SelectExpand"))
					{
						if (!(value == "Expand"))
						{
							if (!(value == "None"))
							{
								this.m_selectAction = TreeNodeSelectAction.Select;
							}
							else
							{
								this.m_selectAction = TreeNodeSelectAction.None;
							}
						}
						else
						{
							this.m_selectAction = TreeNodeSelectAction.Expand;
						}
					}
					else
					{
						this.m_selectAction = TreeNodeSelectAction.SelectExpand;
					}
				}
				else
				{
					this.m_selectAction = TreeNodeSelectAction.Select;
				}
			}
		}

		[JsonProperty("cbx")]
		public bool ShowCheckBox
		{
			get
			{
				return this._ShowCheckBox;
			}
			set
			{
				this._ShowCheckBox = value;
			}
		}

		[JsonProperty("url")]
		public string Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				this._Target = value;
			}
		}

		[JsonProperty("fix")]
		public string ValueFix
		{
			get
			{
				return this._ValueFix;
			}
			set
			{
				this._ValueFix = value;
			}
		}

		[JsonIgnore]
		public string ValuePath
		{
			get
			{
				string path = this.Value;
				TimTreeViewNode prt = this.Parent;
				while (prt != null && !prt.IsRoot)
				{
					path = prt.Value + "/" + path;
					prt = prt.Parent;
				}
				return path;
			}
		}

		[JsonIgnore]
		public string ValuePathWithoutFix
		{
			get
			{
				string path = this.ValueWithoutFix;
				TimTreeViewNode prt = this.Parent;
				while (prt != null && !prt.IsRoot)
				{
					path = prt.ValueWithoutFix + "/" + path;
					prt = prt.Parent;
				}
				return path;
			}
		}

		[JsonProperty("tip")]
		public string ToolTip
		{
			get
			{
				return this._ToolTip.Replace("&quot;", "\"");
			}
			set
			{
				this._ToolTip = value.Replace("\r\n", "").Replace("\"", "&quot;");
			}
		}

		[JsonIgnore]
		public TimTreeView Owner
		{
			get
			{
				return this._Owner;
			}
		}

		[JsonIgnore]
		public TimTreeViewNode Parent
		{
			get
			{
				return this._Parent;
			}
			set
			{
				this._Parent = value;
			}
		}

		[JsonProperty("nds")]
		public TimTreeViewNodeCollection ChildNodes
		{
			get
			{
				bool flag = this._ChildNodes == null;
				if (flag)
				{
					this._ChildNodes = new TimTreeViewNodeCollection(this);
				}
				return this._ChildNodes;
			}
			set
			{
				this._ChildNodes = value;
			}
		}

		public TimTreeViewNode()
		{
		}

		public TimTreeViewNode(string text) : this(text, null, null, null)
		{
		}

		public TimTreeViewNode(string text, string value) : this(text, value, null, null)
		{
		}

		protected internal TimTreeViewNode(TimTreeView owner, bool isRoot) : this()
		{
			this._Owner = owner;
			this._IsRoot = isRoot;
		}

		public TimTreeViewNode(string text, string value, string imageUrl) : this(text, value, imageUrl, null)
		{
		}

		public TimTreeViewNode(string text, string value, string imageUrl, string target)
		{
			bool flag = text != null;
			if (flag)
			{
				this.Text = text.Replace("\r\n", "").Replace("\"", "&quot;");
			}
			bool flag2 = value != null;
			if (flag2)
			{
				this.Value = value.Replace("\r\n", "").Replace("\"", "&quot;");
			}
			bool flag3 = !string.IsNullOrEmpty(imageUrl);
			if (flag3)
			{
				this.ImageUrl = imageUrl;
			}
			bool flag4 = !string.IsNullOrEmpty(target);
			if (flag4)
			{
				this.Target = target;
			}
		}

		private TimTreeViewNode Clone()
		{
			return new TimTreeViewNode
			{
				Checked = this.Checked,
				Expanded = this.Expanded,
				Extend = this.Extend,
				ImageUrl = this.ImageUrl,
				ImageExpandedUrl = this.ImageExpandedUrl,
				Selected = this.Selected,
				ShowCheckBox = this.ShowCheckBox,
				Target = this.Target,
				Text = this.Text,
				Value = this.Value,
				ToolTip = this.ToolTip
			};
		}

		public void Collapse()
		{
			this.Expanded = false;
		}

		public void CollapseAll()
		{
			this.SetExpandedRecursive(false);
		}

		internal void CreateFamilyRelation(TimTreeView treeView, string selectedValue, TimTreeViewPostType postType)
		{
			this.CreateFamilyRelation(treeView, selectedValue, treeView.TargetValue, treeView.SourceValue, treeView.PopulatedValue, treeView.CheckedValue, treeView.ContextValue, postType);
		}

		internal void CreateFamilyRelation(TimTreeView treeView, string selectedValue, string targetValue, string sourceValue, string populatedValue, string checkedValue, string contextValue, TimTreeViewPostType postType)
		{
			this._Owner = treeView;
			this.ChildNodes.Owner = this;
			this.SetPropNode(treeView, this, selectedValue, targetValue, sourceValue, populatedValue, checkedValue, contextValue, postType);
			foreach (TimTreeViewNode child in this.ChildNodes)
			{
				child.Parent = this;
				child._Owner = treeView;
				bool @checked = child.Checked;
				if (@checked)
				{
					treeView.CheckedNodes.Add(child);
				}
				this.SetPropNode(treeView, child, selectedValue, targetValue, sourceValue, populatedValue, checkedValue, contextValue, postType);
				child.CreateFamilyRelation(treeView, selectedValue, targetValue, sourceValue, populatedValue, checkedValue, contextValue, postType);
			}
		}

		private void SetPropNode(TimTreeView treeView, TimTreeViewNode node, string selectedValue, string targetValue, string sourceValue, string populatedValue, string checkedValue, string contextValue, TimTreeViewPostType postType)
		{
			bool flag = postType == TimTreeViewPostType.Other;
			if (flag)
			{
				bool flag2 = node.Value.Equals(selectedValue);
				if (flag2)
				{
					treeView.SelectedNode = node;
				}
				bool flag3 = node.Value.Equals(checkedValue);
				if (flag3)
				{
					treeView.CheckedNode = node;
				}
				bool flag4 = node.Value.Equals(contextValue);
				if (flag4)
				{
					treeView.ContextNode = node;
				}
				bool flag5 = node.Value.Equals(populatedValue) && !populatedValue.Trim().Equals(string.Empty);
				if (flag5)
				{
					treeView.PopulatedNode = node;
				}
			}
			else
			{
				bool flag6 = postType == TimTreeViewPostType.Target;
				if (flag6)
				{
					bool flag7 = node.Value.Equals(targetValue);
					if (flag7)
					{
						treeView.TargetNode = node;
					}
				}
				else
				{
					bool flag8 = postType == TimTreeViewPostType.Source;
					if (flag8)
					{
						bool flag9 = node.Value.Equals(sourceValue);
						if (flag9)
						{
							treeView.SourceNode = node;
						}
					}
					else
					{
						bool flag10 = postType == TimTreeViewPostType.Pop;
						if (flag10)
						{
							bool flag11 = node.Value.Equals(populatedValue);
							if (flag11)
							{
								treeView.PopulatedNode = node;
							}
						}
					}
				}
			}
		}

		public void Expand()
		{
			bool flag = this.PopulateOnDemand && this.ChildNodes.Count == 0;
			if (flag)
			{
				this.PopulateOnDemand = false;
				this.Owner.OnTreeNodePopulate(new TimTreeViewNodeEventArgs(this));
			}
			bool flag2 = this.ChildNodes.Count > 0;
			if (flag2)
			{
				this.Expanded = true;
			}
		}

		public void ExpandAll()
		{
			this.SetExpandedRecursive(true);
		}

		public TimTreeViewNode FindChildNodeByValue(string value)
		{
			return this.ChildNodes.FindNodeByValue(value);
		}

		public TimTreeViewNode FindChildNodeByValueFromAllLeaf(string value)
		{
			return this.ChildNodes.FindNodeByValueFromAllLeaf(value);
		}

		public void Select()
		{
			this.Selected = true;
		}

		private void SetExpandedRecursive(bool value)
		{
			if (value)
			{
				this.Expand();
			}
			else
			{
				this.Collapse();
			}
			bool flag = this.ChildNodes.Count > 0;
			if (flag)
			{
				for (int i = 0; i < this.ChildNodes.Count; i++)
				{
					this.ChildNodes[i].SetExpandedRecursive(value);
				}
			}
		}
	}
}
