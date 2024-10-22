using lab.dotnet8.webapi.Infrastructure.ResponseWrapper;
using Microsoft.AspNetCore.Mvc;
using observability.component.Tracing;

namespace lab.dotnet8.webapi.Controllers;

/// <inheritdoc />
[ApiController]
[Route("[controller]")]
[MethodTracing]
public class LoggerController : ControllerBase
{
    private readonly ILogger<LoggerController> _logger;

    public LoggerController(ILogger<LoggerController> logger)
    {
        this._logger = logger;
    }

    [HttpPost("exception")]
    public Task<IActionResult> ExceptionSample()
    {
        this._logger.LogInformation("Throw Exception");

        throw new Dotnet8Exception();
    }


    /// <summary>
    /// get error response by dotnet exception
    /// </summary>
    /// <returns></returns>
    [HttpGet("exception/dotnet-exception")]
    [ProducesResponseType<ApiResponse<ApiErrorInformation>>(200)]
    public async Task<IActionResult> GetDotnetExceptionResponse()
    {
        throw new ArgumentOutOfRangeException();
    }

    /// <summary>
    /// get error response by custom exception
    /// </summary>
    /// <returns></returns>
    [HttpGet("exception/custom-exception")]
    [ProducesResponseType<ApiResponse<ApiErrorInformation>>(200)]
    public async Task<IActionResult> GetExceptionResponse()
    {
        throw new ErrorCodeException("err-my-error");
    }

    /// <summary>
    /// Send all level log.
    /// </summary>
    /// <returns></returns>
    [HttpPost("send")]
    public Task<IActionResult> SendAsync()
    {
        this._logger.LogCritical("Log Critical");
        this._logger.LogDebug("Log Debug");
        this._logger.LogError("Log Error");
        this._logger.LogTrace("Log Trace");
        this._logger.LogWarning("Log Warning");
        this._logger.LogInformation("Log Information");

        return Task.FromResult<IActionResult>(this.Ok());
    }
}