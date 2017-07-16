using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIM.T_WEBCTRL;

namespace TIM.T_TEMPLET.Master
{
	public class Maintenance : MasterPage
	{
		protected ContentPlaceHolder NestedHead;

		protected ContentPlaceHolder NestedBody;

		protected UpdatePanel UpTemplet;

		protected TimHiddenField Templet_Action;

		protected TimHiddenField Templet_NextWfId;

		protected TimHiddenField Templet_NextWfpId;

		protected TimHiddenField Templet_Todo;

		protected TimHiddenField Templet_Opinion;

		protected TimHiddenField Templet_FileGroup;

		protected TimHiddenField Templet_Files;

		protected TimHiddenField Templet_FilesUploadId;

		protected TimHiddenField Templet_CodeHelperParam;

		protected TimButton btnFlowBlock;

		protected ContentPlaceHolder NestedTemplet;

		protected ContentPlaceHolder NestedSync;

		public UpdatePanel UpScriptPlace
		{
			get
			{
				return ((Site)base.Master).UpScriptPlace;
			}
		}

		public TimButton BtnFlowBlock
		{
			get
			{
				return this.btnFlowBlock;
			}
		}

		public TimHiddenField TempletAction
		{
			get
			{
				return this.Templet_Action;
			}
		}

		public TimHiddenField TempletNextWfId
		{
			get
			{
				return this.Templet_NextWfId;
			}
		}

		public TimHiddenField TempletNextWfpId
		{
			get
			{
				return this.Templet_NextWfpId;
			}
		}

		public TimHiddenField TempletTodo
		{
			get
			{
				return this.Templet_Todo;
			}
		}

		public TimHiddenField TempletOpinion
		{
			get
			{
				return this.Templet_Opinion;
			}
		}

		public TimHiddenField TempletFileGroup
		{
			get
			{
				return this.Templet_FileGroup;
			}
		}

		public TimHiddenField TempletFiles
		{
			get
			{
				return this.Templet_Files;
			}
		}

		public TimHiddenField TempletCodeHelperParam
		{
			get
			{
				return this.Templet_CodeHelperParam;
			}
		}

		public UpdatePanel UpTempletPlace
		{
			get
			{
				return this.UpTemplet;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			this.Templet_FilesUploadId.Value = Guid.NewGuid().ToString();
			base.OnInit(e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}
	}
}
