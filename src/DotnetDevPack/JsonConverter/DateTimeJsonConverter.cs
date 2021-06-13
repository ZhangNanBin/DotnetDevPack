namespace DotnetDevPack.JsonConverter
{
  using System;
  using System.Text.Json;
  using System.Text.Json.Serialization;

  /// <summary>
  /// 时间转换器
  /// </summary>
  public class DateTimeJsonConverter : JsonConverter<DateTime>
  {
    private string FormatString = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    /// 构造器
    /// </summary>
    public DateTimeJsonConverter()
    {
    }

    /// <summary>
    /// 构造器
    /// </summary>
    public DateTimeJsonConverter(string formatString)
    {
      FormatString = formatString;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      return DateTime.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString(FormatString));
    }
  }
}
