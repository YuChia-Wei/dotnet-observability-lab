using System.Data;
using Dapper;
using lab.component.Entities;
using observability.component.Tracing;

namespace lab.component.Repository;

[MethodTracing]
public class WeatherForecastDapperRepository
{
    private readonly IDbConnection _dbConnection;

    public WeatherForecastDapperRepository(IDbConnection dbConnection)
    {
        this._dbConnection = dbConnection;
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
        //!!DateOnly 有特殊處理，請注意 RegisterExtension.cs!!
        var parameter = new DynamicParameters();
        parameter.Add("startDate", startDate);
        parameter.Add("endDate", endDate);
        var sql =
            $@"SELECT w.""Date"", w.""Summary"", w.""TemperatureC""FROM ""WeatherForecast"" AS w WHERE w.""Date"" >= @startDate AND w.""Date"" <= @endDate";
        var queryAsync = await this._dbConnection.QueryAsync<WeatherForecast>(sql, parameter);
        return queryAsync;
    }
}