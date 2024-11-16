using System.Text.Json.Serialization;
using System.Text.Json;

namespace DiabetesApp.API.ExtandJson
{
	public class DateOnlyConverter : JsonConverter<DateOnly>
	{
		public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return DateOnly.Parse(reader.GetString()!);
		}

		public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
		}
	}

	public class TimeOnlyConverter : JsonConverter<TimeOnly>
	{
		public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return TimeOnly.Parse(reader.GetString()!);
		}

		public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString("HH:mm"));
		}
	}

}
