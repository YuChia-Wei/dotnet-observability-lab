using lab.swagger.Components.Interfaces;
using lab.swagger.Components.OptionModels;
using Microsoft.Extensions.Options;

namespace lab.swagger.Components.Implements;

/// <summary>
/// OpenApiDocumentEndpoint 紀錄
/// </summary>
public class OpenApiDocumentEndpointRepository : IOpenApiDocumentEndpointRepository
{
    private readonly ILogger _logger;
    private readonly IOptionsMonitor<ApiEndpointsSettingOption> _optionsMonitor;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public OpenApiDocumentEndpointRepository(IOptionsMonitor<ApiEndpointsSettingOption> optionsMonitor, ILoggerFactory loggerFactory)
    {
        this._optionsMonitor = optionsMonitor;
        this._logger = loggerFactory.CreateLogger<OpenApiDocumentEndpointRepository>();
    }

    /// <summary>
    /// 取得 OpenApi 文件的 Url 資料
    /// </summary>
    /// <param name="serviceName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public OpenApiDocEndpointOption GetAsync(string serviceName)
    {
        return this.GetApiEndpoints()
                   .First(o => o.ServiceName.Equals(serviceName, StringComparison.CurrentCultureIgnoreCase));
    }

    /// <summary>
    /// 取得目前記錄的 OpenApi 文件 Url 清單
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OpenApiDocEndpointOption> GetList()
    {
        return this.GetApiEndpoints();
    }

    private IEnumerable<OpenApiDocEndpointOption> GetApiEndpoints()
    {
        try
        {
            // var apiEndpointsSettingOption = this._optionsMonitor.Get(nameof(ApiEndpointsSettingOption));
            var apiEndpointsSettingOption = this._optionsMonitor.CurrentValue;

            return apiEndpointsSettingOption.ApiEndpoints;
        }
        catch (Exception e)
        {
            this._logger.Log(LogLevel.Warning, $"無法取得 OpenApi 設定資料，無法使用\n例外訊息: {e}");

            return new[] { new OpenApiDocEndpointOption(AppDomain.CurrentDomain.FriendlyName, new Uri("/swagger/v1/swagger.json", UriKind.Relative)) };
        }
    }
}