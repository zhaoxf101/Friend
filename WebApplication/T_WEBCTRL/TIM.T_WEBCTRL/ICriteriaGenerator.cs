using System;

namespace TIM.T_WEBCTRL
{
	public interface ICriteriaGenerator
	{
		string GenerateCriteria(UploadedFile file);
	}
}
