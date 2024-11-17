using lab.component.Entities;
using lab.component.Repository;
using Mediator;
using observability.component.Tracing;

namespace lab.component.Queries;

[MethodTracing]
public class OutboundExceptionApiQueryHandler : IQueryHandler<OutboundExceptionApiQuery, IEnumerable<WeatherForecast>>
{
    private readonly WeatherForecastOutboundApiRepository _repository;

    public OutboundExceptionApiQueryHandler(WeatherForecastOutboundApiRepository repository)
    {
        this._repository = repository;
    }

    public async ValueTask<IEnumerable<WeatherForecast>> Handle(OutboundExceptionApiQuery query, CancellationToken cancellationToken)
    {
        var weatherForecasts = await this._repository.GetExceptionApiAsync(query.StartDate, query.EndDate, cancellationToken);
        return weatherForecasts;
    }
}