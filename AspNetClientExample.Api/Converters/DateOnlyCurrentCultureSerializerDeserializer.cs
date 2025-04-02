using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp;
using RestSharp.Serializers;

namespace AspNetClientExample.Api.Converters;

public sealed class DateOnlyCurrentCultureJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.Parse(reader.GetString()!, CultureInfo.CurrentCulture);
    }

    public override DateOnly ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.Parse(reader.GetString()!, CultureInfo.CurrentCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(CultureInfo.CurrentCulture));
    }

    public override void WriteAsPropertyName(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.ToString(CultureInfo.CurrentCulture));
    }
}

public sealed class DateOnlyCurrentCultureSerializerDeserializer : ISerializer, IDeserializer
{
    //public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //{
    //    return DateOnly.Parse(reader.GetString()!, CultureInfo.CurrentCulture);
    //}

    //public override DateOnly ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //{
    //    return DateOnly.Parse(reader.GetString()!, CultureInfo.CurrentCulture);
    //}

    //public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    //{
    //    writer.WriteStringValue(value.ToString(CultureInfo.CurrentCulture));
    //}

    //public override void WriteAsPropertyName(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    //{
    //    writer.WritePropertyName(value.ToString(CultureInfo.CurrentCulture));
    //}

    private readonly JsonSerializerOptions _options;

    public DateOnlyCurrentCultureSerializerDeserializer(JsonSerializerOptions? options = null)
    {
        _options = options ?? new JsonSerializerOptions
        {
            Converters = { new DateOnlyCurrentCultureJsonConverter() }
        };
    }

    public ContentType ContentType { get; set; } = ContentType.Json;

    public string? Serialize(object? obj)
    {
        return obj is null ? null : JsonSerializer.Serialize(obj, _options);
    }

    public T? Deserialize<T>(RestResponse response)
    {
        return JsonSerializer.Deserialize<T>(response.Content!, _options);
    }
}