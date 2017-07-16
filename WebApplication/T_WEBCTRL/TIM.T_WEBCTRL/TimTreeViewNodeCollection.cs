using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TIM.T_WEBCTRL
{
	public class TimTreeViewNodeCollection : List<TimTreeViewNode>
	{
		[JsonIgnore]
		private TimTreeViewNode _Owner = null;

		[JsonIgnore]
		private bool _IsCheckedNodes = false;

		[JsonIgnore]
		internal TimTreeViewNode Owner
		{
			get
			{
				return this._Owner;
			}
			set
			{
				this._Owner = value;
			}
		}

		[JsonIgnore]
		internal bool IsCheckedNodes
		{
			get
			{
				return this._IsCheckedNodes;
			}
			set
			{
				this._IsCheckedNodes = value;
			}
		}

		public TimTreeViewNodeCollection() : this(null, false)
		{
		}

		public TimTreeViewNodeCollection(TimTreeViewNode owner) : this(owner, false)
		{
		}

		internal TimTreeViewNodeCollection(TimTreeViewNode owner, bool isCheckedNodes)
		{
			this._Owner = owner;
			this.IsCheckedNodes = isCheckedNodes;
		}

		public new void Add(TimTreeViewNode node)
		{
			this.AddAt(base.Count, node);
		}

		public void AddAt(int index, TimTreeViewNode child)
		{
			bool flag = child == null;
			if (flag)
			{
				throw new ArgumentNullException("child");
			}
			bool flag2 = !this.IsCheckedNodes;
			if (flag2)
			{
				bool flag3 = child.Owner != null;
				if (flag3)
				{
					child.Owner.Nodes.Remove(child);
				}
				bool flag4 = child.Parent != null;
				if (flag4)
				{
					child.Parent.ChildNodes.Remove(child);
				}
				bool flag5 = this._Owner != null;
				if (flag5)
				{
					child.Parent = this._Owner;
					child._Owner = this._Owner.Owner;
				}
				bool flag6 = this._Owner != null;
				if (flag6)
				{
					bool flag7 = this._Owner.Parent != null;
					if (flag7)
					{
						this._Owner.Parent.CanIndent = false;
					}
				}
			}
			base.Insert(index, child);
		}

		public new void Clear()
		{
			bool flag = this._Owner != null;
			if (flag)
			{
				bool isRoot = this._Owner.IsRoot;
				if (isRoot)
				{
					this._Owner.Owner.Clear();
				}
			}
			bool flag2 = base.Count == 0;
			if (!flag2)
			{
				foreach (TimTreeViewNode node in this)
				{
					node.ChildNodes.Clear();
					node.Parent = null;
					node._Owner = null;
				}
				base.Clear();
			}
		}

		public new void Remove(TimTreeViewNode node)
		{
			bool flag = node == null;
			if (flag)
			{
				throw new ArgumentNullException("value");
			}
			base.Remove(node);
		}

		public new void RemoveAt(int index)
		{
			TimTreeViewNode node = base[index];
			node.Parent = null;
			base.RemoveAt(index);
		}

		internal void CreateFamilyRelation(TimTreeView treeView, string selectedValue, TimTreeViewPostType postType)
		{
			foreach (TimTreeViewNode child in this)
			{
				child.CreateFamilyRelation(treeView, selectedValue, postType);
			}
		}

		public TimTreeViewNode FindNodeByValue(string value)
		{
			TimTreeViewNode result;
			for (int i = 0; i < base.Count; i++)
			{
				bool flag = value == base[i].Value;
				if (flag)
				{
					result = base[i];
					return result;
				}
			}
			result = null;
			return result;
		}

		public TimTreeViewNode FindNodeByValueFromAllLeaf(string value)
		{
			bool flag = string.IsNullOrEmpty(value);
			TimTreeViewNode result;
			if (flag)
			{
				result = null;
			}
			else
			{
				for (int i = 0; i < base.Count; i++)
				{
					bool flag2 = value == base[i].Value;
					if (flag2)
					{
						result = base[i];
						return result;
					}
					TimTreeViewNode node = base[i].FindChildNodeByValueFromAllLeaf(value);
					bool flag3 = node != null;
					if (flag3)
					{
						result = node;
						return result;
					}
				}
				result = null;
			}
			return result;
		}
	}
}
