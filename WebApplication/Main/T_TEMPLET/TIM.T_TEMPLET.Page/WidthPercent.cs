using Newtonsoft.Json;
using System;

namespace TIM.T_TEMPLET.Page
{
	public class WidthPercent : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteRawValue(value.ToString());
		}
	}
}
