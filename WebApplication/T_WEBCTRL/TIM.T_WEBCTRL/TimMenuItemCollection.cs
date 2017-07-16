using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

namespace TIM.T_WEBCTRL
{
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TimMenuItemCollection : CollectionBase, IStateManager, IEnumerable<TimMenuItem>, IEnumerable
	{
		private bool _Marked;

		public TimMenuItem this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				return (TimMenuItem)base.InnerList[index];
			}
			[DebuggerStepThrough]
			set
			{
				base.InnerList[index] = value;
			}
		}

		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._Marked;
			}
		}

		public TimMenuItemCollection TopNodes()
		{
			TimMenuItemCollection topNodes = new TimMenuItemCollection();
			bool flag = base.Count > 0;
			TimMenuItemCollection result;
			if (flag)
			{
				for (int i = 0; i < base.Count; i++)
				{
					bool flag2 = this[i].FatherValue == "";
					if (flag2)
					{
						topNodes.Add(this[i]);
					}
				}
				result = topNodes;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public TimMenuItem getTreeNodeByValue(string value)
		{
			bool flag = base.Count > 0;
			TimMenuItem result;
			if (flag)
			{
				for (int i = 0; i < base.Count; i++)
				{
					bool flag2 = this[i].Value == value.Trim();
					if (flag2)
					{
						result = this[i];
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public int Add(TimMenuItem value)
		{
			return base.InnerList.Add(value);
		}

		public bool Contains(TimMenuItem value)
		{
			return base.InnerList.Contains(value);
		}

		public int IndexOf(TimMenuItem value)
		{
			return base.InnerList.IndexOf(value);
		}

		public void Insert(int index, TimMenuItem value)
		{
			base.InnerList.Insert(index, value);
		}

		public void Remove(TimMenuItem value)
		{
			base.InnerList.Remove(value);
		}

		public void CopyTo(TimMenuItem[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
		}

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
		}

		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		protected virtual void TrackViewState()
		{
			this._Marked = true;
			bool flag = base.InnerList.Count > 0;
			if (flag)
			{
				for (int i = 0; i < base.InnerList.Count; i++)
				{
					((IStateManager)base.InnerList[i]).TrackViewState();
				}
			}
		}

		protected virtual void LoadViewState(object state)
		{
			bool flag = state != null;
			if (flag)
			{
				Pair pair = state as Pair;
				bool flag2 = pair != null;
				if (flag2)
				{
					ArrayList indexes = (ArrayList)pair.First;
					ArrayList states = (ArrayList)pair.Second;
					for (int i = 0; i < indexes.Count; i++)
					{
						int index = (int)indexes[i];
						bool flag3 = index >= base.InnerList.Count;
						if (flag3)
						{
							TimMenuItem column = new TimMenuItem();
							base.InnerList.Add(column);
						}
						((IStateManager)base.InnerList[index]).LoadViewState(states[i]);
					}
				}
			}
		}

		protected virtual object SaveViewState()
		{
			bool flag = base.InnerList.Count == 0;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ArrayList indexes = new ArrayList(base.InnerList.Count);
				ArrayList states = new ArrayList(base.InnerList.Count);
				for (int i = 0; i < base.InnerList.Count; i++)
				{
					object state = ((IStateManager)base.InnerList[i]).SaveViewState();
					indexes.Add(i);
					states.Add(state);
				}
				result = new Pair(indexes, states);
			}
			return result;
		}

		[IteratorStateMachine(typeof(IEnumerator<TimMenuItem>))]
		public new IEnumerator<TimMenuItem> GetEnumerator()
		{
			foreach (TimMenuItem timMenuItem in this.InnerList)
			{
				yield return timMenuItem;
				//timMenuItem = null;
			}
			//IEnumerator enumerator = null;
			//yield break;
			//yield break;
		}
	}
}
