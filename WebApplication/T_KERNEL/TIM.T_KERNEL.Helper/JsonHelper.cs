using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Data;

namespace TIM.T_KERNEL.Helper
{
	public static class JsonHelper
	{
		public static string ToJsonData(this DataTable value)
		{
			string str = string.Empty;
			return JsonConvert.SerializeObject(value, Formatting.Indented, new JsonConverter[]
			{
				new IsoDateTimeConverter
				{
					DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
				}
			});
		}
	}
}
