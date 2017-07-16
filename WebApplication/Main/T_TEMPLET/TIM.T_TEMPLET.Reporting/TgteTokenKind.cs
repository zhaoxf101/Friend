using System;

namespace TIM.T_TEMPLET.Reporting
{
	internal enum TgteTokenKind
	{
		etkEOL,
		etkNumber,
		etkString,
		etkVariable,
		etkFunction,
		etkPlus,
		etkMinus,
		etkTimes,
		etkDivide,
		etkMOD,
		etkOR,
		etkAND,
		etkXOR,
		etkNOT,
		etkLeftParenthesis,
		etkRightParenthesis,
		etkShiftLeft,
		etkShiftRight,
		etkComma,
		etkEqual,
		etkGreater,
		etkGreaterEqual,
		etkLess,
		etkLessEqual,
		etkNotEqual
	}
}
