using System;
using System.Web.UI;
using TIM.T_TEMPLET.Master;

namespace TIM.T_TEMPLET.Page
{
	public class TPortalBase : PageBase
	{
		private TPortal m_curMaster = null;

		public TPortal CurMaster
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

		protected sealed override void OnInit()
		{
			base.OnInit();
			bool flag = this.CurSM != null;
			if (flag)
			{
				this.CurSM.Scripts.Add(new ScriptReference("TIM.T_TEMPLET.Scripts.PortalManager.js", "T_TEMPLET"));
			}
		}

		protected override void OnLoad()
		{
			bool flag = !base.IsPostBack;
			if (flag)
			{
				this.OnPreLoadRecord();
				this.OnLoadRecord();
				this.OnLoadRecordComplete();
			}
		}
	}
}
