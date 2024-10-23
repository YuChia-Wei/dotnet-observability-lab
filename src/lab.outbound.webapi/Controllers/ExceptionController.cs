using lab.component.Entities;
using lab.component.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace lab.outbound.webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ExceptionController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<ExceptionController> _logger;

    private readonly IMediator _mediator;

    public ExceptionController(ILogger<ExceptionController> logger, IMediator mediator)
    {
        this._logger = logger;
        this._mediator = mediator;
    }

    [HttpGet("{dataSource}")]
    [ProducesDefaultResponseType(typeof(IEnumerable<WeatherForecast>))]
    public async Task<IActionResult> ThrowException(
        [FromRoute] string dataSource,
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate)
    {
        throw new ArgumentException();
    }
}