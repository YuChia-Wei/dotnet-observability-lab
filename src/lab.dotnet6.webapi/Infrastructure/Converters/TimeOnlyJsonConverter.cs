﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace lab.dotnet6.webapi.Infrastructure.Converters;

public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.Parse(reader.GetString()!);
    }

    public override TimeOnly ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        var isoTime = value.ToString("O");
        writer.WriteStringValue(isoTime);
    }

    public override void WriteAsPropertyName(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        var isoTime = value.ToString("O");
        writer.WritePropertyName(isoTime);
    }
}