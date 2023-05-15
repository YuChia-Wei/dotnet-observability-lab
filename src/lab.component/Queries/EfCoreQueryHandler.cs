using lab.component.Entities;
using lab.component.Repository;
using Mediator;
using observability.component.Tracing;

namespace lab.component.Queries;

[MethodTracing]
public class EfCoreQueryHandler : IQueryHandler<EfCoreQuery, IEnumerable<WeatherForecast>>
{
    private readonly WeatherForecastEfCoreRepository _repository;

    public EfCoreQueryHandler(WeatherForecastEfCoreRepository repository)
    {
        this._repository = repository;
    }

    public async ValueTask<IEnumerable<WeatherForecast>> Handle(EfCoreQuery query, CancellationToken cancellationToken)
    {
        var weatherForecasts = await this._repository.GetListAsync(query.StartDate, query.EndDate, cancellationToken);
        return weatherForecasts;
    }
}