namespace lab.swagger.Infrastructure.Middleware;

/// <summary>
/// </summary>
public static class PipeLineLogMiddlewareExtension
{
    /// <summary>
    /// Uses the middleware log.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static IApplicationBuilder PipeLineLog(this IApplicationBuilder builder, string msg)
    {
        return builder.UseMiddleware<PipeLineLogMiddleware>(msg);
    }
}