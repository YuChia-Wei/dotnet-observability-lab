using lab.component.Entities;
using observability.component.Tracing;

namespace lab.component.Repository;

[MethodTracing]
public class WeatherForecastRedisRepository
{
    /// <summary>
    /// Get by date range
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<WeatherForecast>> GetListAsync(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}