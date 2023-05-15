using lab.component.Entities;
using lab.component.Repository;
using Mediator;
using observability.component.Tracing;

namespace lab.component.Queries;

[MethodTracing]
public class OutboundApiQueryHandler : IQueryHandler<OutboundApiQuery, IEnumerable<WeatherForecast>>
{
    private readonly WeatherForecastOutboundApiRepository _repository;

    public OutboundApiQueryHandler(WeatherForecastOutboundApiRepository repository)
    {
        this._repository = repository;
    }

    public async ValueTask<IEnumerable<WeatherForecast>> Handle(OutboundApiQuery query, CancellationToken cancellationToken)
    {
        var weatherForecasts = await this._repository.GetListAsync(query.StartDate, query.EndDate, cancellationToken);
        return weatherForecasts;
    }
}