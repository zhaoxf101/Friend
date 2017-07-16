using System;

namespace TIM.T_TEMPLET.Reporting
{
	internal class TgteToken
	{
		internal TgteTokenKind TokenKind;

		internal object Token;

		internal int StartPos;

		internal int ParametersCount;

		internal int OutputPos;

		internal int EndPos;

		internal string ParametersStart;
	}
}
