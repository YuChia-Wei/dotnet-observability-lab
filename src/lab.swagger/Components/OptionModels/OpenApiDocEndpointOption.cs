using Microsoft.OpenApi.Models;

namespace lab.swagger.Components.OptionModels;

/// <summary>
/// WebAPI 資訊
/// </summary>
public class OpenApiDocEndpointOption
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public OpenApiDocEndpointOption(string serviceName, Uri jsonUri)
    {
        this.ServiceName = serviceName;
        this.JsonUri = jsonUri;
        this.ServerHostList = Array.Empty<string>();
        this.IsSwaggerEnabled = true;
    }

    /// <summary>
    /// enable
    /// </summary>
    public bool IsSwaggerEnabled { get; set; }

    /// <summary>
    /// Service Name (with version)
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// Swagger Json Uri
    /// </summary>
    public Uri JsonUri { get; set; }

    /// <summary>
    /// Server Host List
    /// </summary>
    public IEnumerable<string> ServerHostList { get; set; }

    /// <summary>
    /// 把紀錄中的 Api 路徑轉為 OpenApiDocument 型別中使用的 OpenApiServer 物件
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OpenApiServer> GetOpenApiServerList()
    {
        var openApiServers = new List<OpenApiServer>();

        if (!(this.ServerHostList?.Any() ?? false))
        {
            openApiServers.Add(new OpenApiServer { Url = $"{this.JsonUri.Scheme}://{this.JsonUri.Authority}" });
        }

        openApiServers.AddRange(this.ServerHostList!.Select(serverHost => new OpenApiServer { Url = serverHost }));

        return openApiServers;
    }

    /// <summary>
    /// 將相對路徑轉換為完整的絕對路徑 (使用送要求的網址決定相對路徑前面的路徑內容)
    /// </summary>
    /// <param name="requestUri"></param>
    public void ParseRelateUri(string requestUri)
    {
        if (this.JsonUri.IsAbsoluteUri)
        {
            return;
        }

        var jsonUriOriginalString = this.JsonUri.OriginalString;
        var endpointUri = jsonUriOriginalString.StartsWith("/")
                              ? jsonUriOriginalString.Remove(0, 1)
                              : jsonUriOriginalString;

        this.JsonUri = new Uri($"{requestUri}/{endpointUri}");
    }
}