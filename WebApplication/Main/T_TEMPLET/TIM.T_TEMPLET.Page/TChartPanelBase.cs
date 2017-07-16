using System;
using TIM.T_TEMPLET.Master;

namespace TIM.T_TEMPLET.Page
{
	public class TChartPanelBase : PageBase
	{
		private TChartPanel m_curMaster = null;

		public TChartPanel CurMaster
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
