﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace lab.dotnet6.webapi.Infrastructure.Extensions;

public static class SwaggerGenOptionsExtensions
{
    /// <summary>
    /// Maps <see cref="DateOnly" /> and <see cref="TimeOnly" /> to string.
    /// </summary>
    public static void UseDateOnlyTimeOnlyStringConverters(this SwaggerGenOptions options)
    {
        options.MapType<DateOnly>(() => new OpenApiSchema
        {
            Type = "string",
            Format = "date"
        });
        options.MapType<TimeOnly>(() => new OpenApiSchema
        {
            Type = "string",
            Format = "time",
            Example = OpenApiAnyFactory.CreateFromJson("\"13:45:42.0000000\"")
        });
    }
}