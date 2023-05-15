using lab.component.Entities;
using lab.component.Repository;
using Mediator;
using observability.component.Tracing;

namespace lab.component.Queries;

[MethodTracing]
public class DapperQueryHandler : IQueryHandler<DapperQuery, IEnumerable<WeatherForecast>>
{
    private readonly WeatherForecastDapperRepository _repository;

    public DapperQueryHandler(WeatherForecastDapperRepository repository)
    {
        this._repository = repository;
    }

    public async ValueTask<IEnumerable<WeatherForecast>> Handle(DapperQuery query, CancellationToken cancellationToken)
    {
        var weatherForecasts = await this._repository.GetListAsync(query.StartDate, query.EndDate, cancellationToken);
        return weatherForecasts;
    }
}