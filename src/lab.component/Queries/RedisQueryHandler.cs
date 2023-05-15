using lab.component.Entities;
using lab.component.Repository;
using Mediator;
using observability.component.Tracing;

namespace lab.component.Queries;

[MethodTracing]
public class RedisQueryHandler : IQueryHandler<RedisQuery, IEnumerable<WeatherForecast>>
{
    private readonly WeatherForecastRedisRepository _repository;

    public RedisQueryHandler(WeatherForecastRedisRepository repository)
    {
        this._repository = repository;
    }

    public async ValueTask<IEnumerable<WeatherForecast>> Handle(RedisQuery query, CancellationToken cancellationToken)
    {
        var weatherForecasts = await this._repository.GetListAsync(query.StartDate, query.EndDate, cancellationToken);
        return weatherForecasts;
    }
}