using lab.component.Entities;
using lab.component.Repository;
using Mediator;
using observability.component.Tracing;

namespace lab.component.Queries;

[MethodTracing]
public class OutboundApiHttpClientExceptionQueryHandler : IQueryHandler<OutboundApiHttpClientExceptionQuery, IEnumerable<WeatherForecast>>
{
    private readonly WeatherForecastOutboundApiRepository _repository;

    public OutboundApiHttpClientExceptionQueryHandler(WeatherForecastOutboundApiRepository repository)
    {
        this._repository = repository;
    }

    public async ValueTask<IEnumerable<WeatherForecast>> Handle(OutboundApiHttpClientExceptionQuery query, CancellationToken cancellationToken)
    {
        var weatherForecasts = await this._repository.GetHttpClientExceptionAsync(query.StartDate, query.EndDate, cancellationToken);
        return weatherForecasts;
    }
}