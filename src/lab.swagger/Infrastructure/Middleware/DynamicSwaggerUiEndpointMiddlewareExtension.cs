namespace lab.swagger.Infrastructure.Middleware;

/// <summary>
/// </summary>
public static class DynamicSwaggerUiEndpointMiddlewareExtension
{
    /// <summary>
    /// 使用動態 Swagger Define 選單 (右上角下拉選單)
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseDynamicSwaggerUiEndpoint(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DynamicSwaggerUiEndpointMiddleware>();
    }
}