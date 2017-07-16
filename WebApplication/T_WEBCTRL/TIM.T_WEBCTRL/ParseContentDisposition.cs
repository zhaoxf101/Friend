using System;
using System.Collections.Generic;

namespace TIM.T_WEBCTRL
{
	internal static class ParseContentDisposition
	{
		internal static string[] RemoveChar(string contentText, char removechar)
		{
			int index = contentText.IndexOf(removechar);
			bool flag = index > 0;
			string[] result;
			if (flag)
			{
				result = new string[]
				{
					contentText.Substring(0, index),
					contentText.Substring(index + 1)
				};
			}
			else
			{
				result = new string[]
				{
					contentText
				};
			}
			return result;
		}

		internal static string[] GetContentTextArray(string contentText)
		{
			List<string> list = new List<string>();
			int startIndex = 0;
			int index = 0;
			while (index < contentText.Length)
			{
				index = contentText.IndexOf('"', startIndex);
				int num3 = contentText.IndexOf(';', startIndex);
				bool flag = index == -1 || num3 < index;
				if (flag)
				{
					index = num3;
				}
				else
				{
					index = contentText.IndexOf('"', index + 1);
					index = contentText.IndexOf(';', index);
				}
				bool flag2 = index == -1;
				if (flag2)
				{
					index = contentText.Length;
				}
				list.Add(contentText.Substring(startIndex, index - startIndex));
				startIndex = index + 1;
			}
			return list.ToArray();
		}
	}
}
