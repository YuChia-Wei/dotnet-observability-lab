using lab.swagger.Components.Interfaces;
using lab.swagger.Components.OptionModels;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace lab.swagger.Components.Implements;

/// <summary>
/// Open Api 文件儲存庫
/// </summary>
public class OpenApiDocumentRepository : IOpenApiDocumentRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public OpenApiDocumentRepository(IHttpClientFactory httpClientFactory)
    {
        this._httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// 取得 Open Api 文件
    /// </summary>
    /// <param name="apiJsonEndpointOption"></param>
    /// <returns></returns>
    public async Task<OpenApiDocument> GetOpenApiDocumentAsync(OpenApiDocEndpointOption apiJsonEndpointOption)
    {
        var openApiDocument = await this.DownloadOpenApiDocumentAsync(apiJsonEndpointOption);

        return openApiDocument;
    }

    /// <summary>
    /// 下載外部的 OpenApi 文件
    /// </summary>
    /// <param name="apiJsonEndpointOption"></param>
    /// <returns></returns>
    private async Task<OpenApiDocument> DownloadOpenApiDocumentAsync(OpenApiDocEndpointOption apiJsonEndpointOption)
    {
        var httpClient = this._httpClientFactory.CreateClient();

        var stream = await httpClient.GetStreamAsync(apiJsonEndpointOption.JsonUri);

        return new OpenApiStreamReader().Read(stream, out _);
    }
}