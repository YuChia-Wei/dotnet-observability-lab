using lab.component.EfCore;
using lab.component.Entities;
using Microsoft.EntityFrameworkCore;
using observability.component.Tracing;

namespace lab.component.Repository;

[MethodTracing]
public class WeatherForecastEfCoreRepository
{
    private readonly TestContext _dbContext;

    public WeatherForecastEfCoreRepository(TestContext dbContext)
    {
        this._dbContext = dbContext;
    }

    /// <summary>
    /// Get by date range
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<WeatherForecast>> GetListAsync(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        var queryable = this._dbContext.Set<WeatherForecast>().AsNoTracking();
        var weatherForecasts = await queryable.Where(o => o.Date >= startDate && o.Date <= endDate)
                                              .ToListAsync(cancellationToken);
        return weatherForecasts;
    }
}