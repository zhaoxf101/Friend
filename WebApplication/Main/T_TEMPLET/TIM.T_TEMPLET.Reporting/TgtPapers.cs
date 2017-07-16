using System;
using System.Collections.Generic;

namespace TIM.T_TEMPLET.Reporting
{
	internal static class TgtPapers
	{
		internal const int CDefaultPaperCount = 82;

		internal const int CCustomPaper = 0;

		internal static Dictionary<int, RdPaper> m_items;

		static TgtPapers()
		{
			TgtPapers.m_items = new Dictionary<int, RdPaper>();
			TgtPapers.m_items.Add(0, new RdPaper(0, "自定义大小", 0, 0));
			TgtPapers.m_items.Add(16, new RdPaper(16, "10 X 14 inches", 10000, 14000));
			TgtPapers.m_items.Add(17, new RdPaper(17, "11 X 17 inches", 11000, 17000));
			TgtPapers.m_items.Add(90, new RdPaper(90, "12 X 11 inches", 12000, 11000));
			TgtPapers.m_items.Add(8, new RdPaper(8, "A3", 11693, 16535));
			TgtPapers.m_items.Add(76, new RdPaper(76, "A3 Rotated", 16535, 11693));
			TgtPapers.m_items.Add(9, new RdPaper(9, "A4", 8268, 11693));
			TgtPapers.m_items.Add(77, new RdPaper(77, "A4 Rotated", 11693, 8268));
			TgtPapers.m_items.Add(10, new RdPaper(10, "A4 Small", 8268, 11693));
			TgtPapers.m_items.Add(11, new RdPaper(11, "A5", 5827, 8268));
			TgtPapers.m_items.Add(78, new RdPaper(78, "A5 Rotated", 8268, 5827));
			TgtPapers.m_items.Add(70, new RdPaper(70, "A6", 4134, 5827));
			TgtPapers.m_items.Add(83, new RdPaper(83, "A6 Rotated", 5827, 4134));
			TgtPapers.m_items.Add(12, new RdPaper(12, "B4", 9843, 13937));
			TgtPapers.m_items.Add(79, new RdPaper(79, "B4 (JIS) Rotated", 14331, 10118));
			TgtPapers.m_items.Add(13, new RdPaper(13, "B5", 7165, 10118));
			TgtPapers.m_items.Add(80, new RdPaper(80, "B5 (JIS) Rotated", 10118, 7165));
			TgtPapers.m_items.Add(88, new RdPaper(88, "B6 (JIS)", 5039, 7165));
			TgtPapers.m_items.Add(89, new RdPaper(89, "B6 (JIS) Rotated", 7165, 5039));
			TgtPapers.m_items.Add(24, new RdPaper(24, "C Sheet", 17000, 22000));
			TgtPapers.m_items.Add(69, new RdPaper(69, "Double Japanese Postcard", 7874, 5827));
			TgtPapers.m_items.Add(82, new RdPaper(82, "Double Japanese Postcard Rotated", 5827, 7874));
			TgtPapers.m_items.Add(25, new RdPaper(25, "D Sheet", 22000, 34000));
			TgtPapers.m_items.Add(19, new RdPaper(19, "Envelope #9", 3875, 8875));
			TgtPapers.m_items.Add(20, new RdPaper(20, "Envelope #10", 4125, 9500));
			TgtPapers.m_items.Add(21, new RdPaper(21, "Envelope #11", 4500, 10375));
			TgtPapers.m_items.Add(22, new RdPaper(22, "Envelope #12", 4750, 11000));
			TgtPapers.m_items.Add(23, new RdPaper(23, "Envelope #13", 5000, 11500));
			TgtPapers.m_items.Add(29, new RdPaper(29, "Envelope C3", 12756, 18031));
			TgtPapers.m_items.Add(30, new RdPaper(30, "Envelope C4", 9016, 12756));
			TgtPapers.m_items.Add(28, new RdPaper(28, "Envelope C5", 6378, 9016));
			TgtPapers.m_items.Add(31, new RdPaper(31, "Envelope C6", 4488, 6378));
			TgtPapers.m_items.Add(32, new RdPaper(32, "Envelope C65", 4488, 9016));
			TgtPapers.m_items.Add(33, new RdPaper(33, "Envelope B4", 9843, 13898));
			TgtPapers.m_items.Add(34, new RdPaper(34, "Envelope B5", 6929, 9843));
			TgtPapers.m_items.Add(35, new RdPaper(35, "Envelope B6", 6929, 4921));
			TgtPapers.m_items.Add(27, new RdPaper(27, "Envelope DL", 4331, 8661));
			TgtPapers.m_items.Add(36, new RdPaper(36, "Envelope Italy", 4331, 9055));
			TgtPapers.m_items.Add(37, new RdPaper(37, "Envelope Monarch ", 3875, 7500));
			TgtPapers.m_items.Add(38, new RdPaper(38, "Envelope Personal", 3625, 6500));
			TgtPapers.m_items.Add(26, new RdPaper(26, "E Sheet", 34000, 44000));
			TgtPapers.m_items.Add(7, new RdPaper(7, "Executive", 7250, 10500));
			TgtPapers.m_items.Add(39, new RdPaper(39, "US Std Fanfold", 14875, 11000));
			TgtPapers.m_items.Add(40, new RdPaper(40, "German Std Fanfold", 8500, 12000));
			TgtPapers.m_items.Add(41, new RdPaper(41, "German Legal FanFold", 8500, 13000));
			TgtPapers.m_items.Add(14, new RdPaper(14, "Folio", 8500, 13000));
			TgtPapers.m_items.Add(81, new RdPaper(81, "Japanese Postcard Rotated", 5827, 3937));
			TgtPapers.m_items.Add(4, new RdPaper(4, "Ledger", 17000, 11000));
			TgtPapers.m_items.Add(5, new RdPaper(5, "Legal", 8500, 14000));
			TgtPapers.m_items.Add(1, new RdPaper(1, "Letter", 8500, 11000));
			TgtPapers.m_items.Add(75, new RdPaper(75, "Letter Rotated", 11000, 8500));
			TgtPapers.m_items.Add(2, new RdPaper(2, "Letter Small", 8500, 11000));
			TgtPapers.m_items.Add(18, new RdPaper(18, "Note", 8500, 11000));
			TgtPapers.m_items.Add(93, new RdPaper(93, "PRC 16K", 5748, 8465));
			TgtPapers.m_items.Add(106, new RdPaper(106, "PRC 16K Rotated", 8465, 5748));
			TgtPapers.m_items.Add(94, new RdPaper(94, "PRC 32K", 3819, 5945));
			TgtPapers.m_items.Add(107, new RdPaper(107, "PRC 32K Rotated", 5945, 3819));
			TgtPapers.m_items.Add(95, new RdPaper(95, "PRC 32K (Big)", 3819, 5945));
			TgtPapers.m_items.Add(108, new RdPaper(108, "PRC 32K (Big) Rotated", 5945, 3819));
			TgtPapers.m_items.Add(96, new RdPaper(96, "PRC Envelope #1", 4016, 6496));
			TgtPapers.m_items.Add(109, new RdPaper(109, "PRC Envelope #1 Rotated", 6496, 4016));
			TgtPapers.m_items.Add(97, new RdPaper(97, "PRC Envelope #2", 4016, 6929));
			TgtPapers.m_items.Add(110, new RdPaper(110, "PRC Envelope #2 Rotated", 6929, 4016));
			TgtPapers.m_items.Add(98, new RdPaper(98, "PRC Envelope #3", 4921, 6929));
			TgtPapers.m_items.Add(111, new RdPaper(111, "PRC Envelope #3 Rotated", 6929, 4921));
			TgtPapers.m_items.Add(99, new RdPaper(99, "PRC Envelope #4", 4331, 8189));
			TgtPapers.m_items.Add(112, new RdPaper(112, "PRC Envelope #4 Rotated", 8189, 4331));
			TgtPapers.m_items.Add(100, new RdPaper(100, "PRC Envelope #5", 4331, 8661));
			TgtPapers.m_items.Add(113, new RdPaper(113, "PRC Envelope #5 Rotated", 8661, 4331));
			TgtPapers.m_items.Add(101, new RdPaper(101, "PRC Envelope #6", 4724, 9055));
			TgtPapers.m_items.Add(114, new RdPaper(114, "PRC Envelope #6 Rotated", 9055, 4724));
			TgtPapers.m_items.Add(102, new RdPaper(102, "PRC Envelope #7", 6299, 9055));
			TgtPapers.m_items.Add(115, new RdPaper(115, "PRC Envelope #7 Rotated", 9055, 6299));
			TgtPapers.m_items.Add(103, new RdPaper(103, "PRC Envelope #8", 4724, 12165));
			TgtPapers.m_items.Add(116, new RdPaper(116, "PRC Envelope #8 Rotated", 12165, 4724));
			TgtPapers.m_items.Add(104, new RdPaper(104, "PRC Envelope #9", 9016, 12756));
			TgtPapers.m_items.Add(117, new RdPaper(117, "PRC Envelope #9 Rotated", 12756, 9016));
			TgtPapers.m_items.Add(105, new RdPaper(105, "PRC Envelope #10", 12756, 18031));
			TgtPapers.m_items.Add(118, new RdPaper(118, "PRC Envelope #10 Rotated", 18031, 12756));
			TgtPapers.m_items.Add(15, new RdPaper(15, "Quarto", 8465, 10827));
			TgtPapers.m_items.Add(6, new RdPaper(6, "Statement", 5500, 8500));
			TgtPapers.m_items.Add(3, new RdPaper(3, "Tabloid", 11000, 17000));
		}
	}
}
