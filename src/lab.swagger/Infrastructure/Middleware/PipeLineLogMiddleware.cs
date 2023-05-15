namespace lab.swagger.Infrastructure.Middleware;

/// <summary>
/// pipe line log Middleware
/// </summary>
public class PipeLineLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string msg;

    /// <summary>
    /// Initializes a new instance of the <see cref="PipeLineLogMiddleware" /> class.
    /// </summary>
    /// <param name="next">The next.</param>
    /// <param name="msg"></param>
    public PipeLineLogMiddleware(RequestDelegate next, string msg)
    {
        this._next = next;
        this.msg = msg;
    }

    /// <summary>
    /// Invokes the asynchronous.
    /// </summary>
    /// <param name="context">The context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("===============");
        Console.WriteLine($"Path: " + context.Request.Path);
        Console.WriteLine($"Host: " + context.Request.Host);
        Console.WriteLine($"Protocol: " + context.Request.Protocol);
        Console.WriteLine($"Scheme: " + context.Request.Scheme);
        Console.WriteLine($"Msg:  {this.msg}");
        Console.WriteLine("===============");
        await this._next(context);
    }
}