using System.Net;
using System.Text.Json;
using lab.component.Entities;
using observability.component.Tracing;

namespace lab.component.Repository;

[MethodTracing]
public class WeatherForecastOutboundApiRepository
{
    private readonly HttpClient _httpClient;

    public WeatherForecastOutboundApiRepository(IHttpClientFactory factory)
    {
        this._httpClient = factory.CreateClient();
    }

    /// <summary>
    /// Get by date range
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<WeatherForecast>> GetExceptionApiAsync(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken)
    {
        var dataSource = Random.Shared.Next(1, 4) switch
        {
            1 => "ef-core",
            2 => "auto",
            3 => "dapper",
            4 => "redis",
            _ => "auto"
        };

        var responseMessage =
            await this._httpClient.GetAsync(
                $"http://lab-outbound-webapi:8080/exception/{dataSource}?startDate={startDate}&endDate={endDate}",
                cancellationToken);

        var httpResponseMessage = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        var weatherForecasts = Parse<IEnumerable<WeatherForecast>>(httpResponseMessage);
        return weatherForecasts;
    }

    public async Task<IEnumerable<WeatherForecast>> GetHttpClientExceptionAsync(
        DateOnly queryStartDate,
        DateOnly queryEndDate,
        CancellationToken cancellationToken)
    {
        var dataSource = Random.Shared.Next(1, 4) switch
        {
            1 => "ef-core",
            2 => "auto",
            3 => "dapper",
            4 => "redis",
            _ => "auto"
        };

        var responseMessage =
            await this._httpClient.GetAsync(
                $"http://error-webapi:8080/exception/{dataSource}?startDate={queryStartDate}&endDate={queryEndDate}",
                cancellationToken);

        var httpResponseMessage = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        var weatherForecasts = Parse<IEnumerable<WeatherForecast>>(httpResponseMessage);
        return weatherForecasts;
    }

    /// <summary>
    /// Get by date range
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<WeatherForecast>> GetListAsync(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken)
    {
        var dataSource = Random.Shared.Next(1, 4) switch
        {
            1 => "ef-core",
            2 => "auto",
            3 => "dapper",
            4 => "redis",
            _ => "auto"
        };

        var responseMessage =
            await this._httpClient.GetAsync(
                $"http://lab-outbound-webapi:8080/WeatherForecast/{dataSource}?startDate={startDate}&endDate={endDate}",
                cancellationToken);

        var httpResponseMessage = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        var weatherForecasts = Parse<IEnumerable<WeatherForecast>>(httpResponseMessage);
        return weatherForecasts;
    }

    /// <summary>
    /// 解析 Response body
    /// </summary>
    /// <param name="responseBody">response body</param>
    /// <returns></returns>
    private static T Parse<T>(string responseBody)
    {
        JsonDocument content;

        try
        {
            content = JsonDocument.Parse(responseBody);
        }
        catch (Exception ex)
        {
            throw new Exception(
                $"Response Json Parse Error : {ex.Message}, 錯誤內容 : {WebUtility.HtmlEncode(responseBody)}",
                ex);
        }

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return content.Deserialize<T>(jsonSerializerOptions) ?? default(T);

        // return data == null ? default(T) : data.ToObject<T>();
    }
}