using Mediator;
using Microsoft.AspNetCore.Mvc;
using lab.component.Entities;
using lab.component.Queries;

namespace lab.outbound.webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly IMediator _mediator;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
    {
        this._logger = logger;
        this._mediator = mediator;
    }

    [HttpGet("dapper")]
    [ProducesDefaultResponseType(typeof(IEnumerable<WeatherForecast>))]
    public async Task<IActionResult> Dapper([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        var query = new DapperQuery
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var weatherForecast = await this._mediator.Send<IEnumerable<WeatherForecast>>(query);

        return this.Ok(weatherForecast);
    }

    [HttpGet("ef-core")]
    [ProducesDefaultResponseType(typeof(IEnumerable<WeatherForecast>))]
    public async Task<IActionResult> EfCore([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        var query = new EfCoreQuery
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var weatherForecast = await this._mediator.Send<IEnumerable<WeatherForecast>>(query);

        return this.Ok(weatherForecast);
    }

    [HttpGet("auto")]
    [ProducesDefaultResponseType(typeof(IEnumerable<WeatherForecast>))]
    public IActionResult Get([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        if (startDate.CompareTo(endDate) > 0)
        {
            return this.BadRequest("query date error.");
        }

        if (startDate.AddDays(30).CompareTo(endDate) < 0)
        {
            return this.BadRequest("query date range is to big.");
        }

        var temp = new List<WeatherForecast>();
        for (var date = startDate; date.Day <= endDate.Day; date = date.AddDays(1))
        {
            temp.Add(new WeatherForecast
            {
                Date = date,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
        }

        return this.Ok(temp);
    }

    [HttpGet("redis")]
    [ProducesDefaultResponseType(typeof(IEnumerable<WeatherForecast>))]
    public async Task<IActionResult> Redis([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        var query = new RedisQuery
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var weatherForecast = await this._mediator.Send<IEnumerable<WeatherForecast>>(query);

        return this.Ok(weatherForecast);
    }
}