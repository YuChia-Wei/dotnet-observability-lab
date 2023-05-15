using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace lab.swagger.Infrastructure.Middleware;

/// <summary>
/// Wrapper over SwaggerUI middleware to support reloading the options at runtime
/// </summary>
/// <remarks>https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1093</remarks>
public class DynamicSwaggerUiEndpointMiddleware : IMiddleware
{
    private readonly IWebHostEnvironment _hostingEnv;
    private readonly ILoggerFactory _loggerFactory;
    private readonly SwaggerUIOptions _swaggerUiOptions;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="hostingEnv"></param>
    /// <param name="loggerFactory"></param>
    /// <param name="options"></param>
    public DynamicSwaggerUiEndpointMiddleware(
        IWebHostEnvironment hostingEnv,
        ILoggerFactory loggerFactory,
        IOptionsSnapshot<SwaggerUIOptions> options)
    {
        this._hostingEnv = hostingEnv;
        this._loggerFactory = loggerFactory;
        this._swaggerUiOptions = options.Value;
    }

    /// <summary>
    /// invoke
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await new SwaggerUIMiddleware(next, this._hostingEnv, this._loggerFactory, this._swaggerUiOptions).Invoke(context);
    }
}