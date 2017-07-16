using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class FixRowColumn
	{
		private bool m_isFixHeader;

		private bool m_isFixPager;

		private string m_fixRowIndices;

		private string m_fixColumnIndices;

		private Unit m_tableWidth;

		private Unit m_tableHeight;

		private bool _enableScrollState;

		[Category("扩展"), DefaultValue(false), Description("固定表头否？"), NotifyParentProperty(true)]
		public virtual bool IsFixHeader
		{
			get
			{
				return this.m_isFixHeader;
			}
			set
			{
				this.m_isFixHeader = value;
			}
		}

		[Category("扩展"), DefaultValue(false), Description("固定分页行否？"), NotifyParentProperty(true)]
		public virtual bool IsFixPager
		{
			get
			{
				return this.m_isFixPager;
			}
			set
			{
				this.m_isFixPager = value;
			}
		}

		[Category("扩展"), Description("需要固定的行的索引（用逗号“,”分隔）"), NotifyParentProperty(true)]
		public virtual string FixRowIndices
		{
			get
			{
				return this.m_fixRowIndices;
			}
			set
			{
				this.m_fixRowIndices = value;
			}
		}

		[Category("扩展"), Description("需要固定的列的索引（用逗号“,”分隔）"), NotifyParentProperty(true)]
		public virtual string FixColumnIndices
		{
			get
			{
				return this.m_fixColumnIndices;
			}
			set
			{
				this.m_fixColumnIndices = value;
			}
		}

		[Category("扩展"), Description("表格的宽度"), NotifyParentProperty(true)]
		public Unit TableWidth
		{
			get
			{
				return this.m_tableWidth;
			}
			set
			{
				this.m_tableWidth = value;
			}
		}

		[Category("扩展"), Description("表格的高度"), NotifyParentProperty(true)]
		public Unit TableHeight
		{
			get
			{
				return this.m_tableHeight;
			}
			set
			{
				this.m_tableHeight = value;
			}
		}

		[Category("扩展"), DefaultValue(false), Description("是否保持滚动条的状态"), NotifyParentProperty(true)]
		public bool EnableScrollState
		{
			get
			{
				return this._enableScrollState;
			}
			set
			{
				this._enableScrollState = value;
			}
		}

		public override string ToString()
		{
			return "FixRowCol";
		}
	}
}
