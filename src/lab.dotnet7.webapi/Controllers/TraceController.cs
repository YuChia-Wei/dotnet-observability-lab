using lab.component.Entities;
using lab.component.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using observability.component.Tracing;

namespace lab.dotnet7.webapi.Controllers;

[ApiController]
[Route("[controller]")]
[MethodTracing]
public class TraceController : ControllerBase
{
    private readonly IMediator _mediator;

    public TraceController(IMediator mediator)
    {
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

    [HttpGet("outbound-api")]
    public async Task<IActionResult> Http([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        // var client = this._factory.CreateClient();
        //
        // await client.GetAsync("https://google.com");
        // await client.GetAsync("https://ipinfo.io");
        // await client.GetAsync("https://networkcalc.com/api/ip/192.168.1.1/24");
        //
        // return this.Ok(new { Message = "http client tracing sample" });
        var query = new OutboundApiQuery
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var weatherForecast = await this._mediator.Send<IEnumerable<WeatherForecast>>(query);

        return this.Ok(weatherForecast);
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