using lab.swagger.Components.Interfaces;
using lab.swagger.Components.OptionModels;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;

namespace lab.swagger.Components.Implements;

/// <inheritdoc />
public class OpenApiDocumentService : IOpenApiDocumentService
{
    private readonly IOpenApiDocumentEndpointRepository _openApiDocumentEndpointRepository;
    private readonly IOpenApiDocumentRepository _openApiDocumentRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="openApiDocumentEndpointRepository"></param>
    /// <param name="openApiDocumentRepository"></param>
    public OpenApiDocumentService(
        IOpenApiDocumentEndpointRepository openApiDocumentEndpointRepository,
        IOpenApiDocumentRepository openApiDocumentRepository)
    {
        this._openApiDocumentRepository = openApiDocumentRepository;
        this._openApiDocumentEndpointRepository = openApiDocumentEndpointRepository;
    }

    public async Task<string> GetJsonAsync(string serviceName, string requestUri)
    {
        var apiJsonEndpoint = this.GetOpenApiDocEndpoint(serviceName, requestUri);

        var openApiServers = apiJsonEndpoint.GetOpenApiServerList();

        var openApiDocument = await this._openApiDocumentRepository.GetOpenApiDocumentAsync(apiJsonEndpoint);

        FillApiServerList(openApiDocument, openApiServers);

        return openApiDocument.SerializeAsJson<OpenApiDocument>(OpenApiSpecVersion.OpenApi3_0);
    }

    private static void FillApiServerList(OpenApiDocument openApiDocument, IEnumerable<OpenApiServer> openApiServers)
    {
        foreach (var openApiServer in openApiServers.Where(o => openApiDocument.Servers.All<OpenApiServer>(doc => doc.Url != o.Url)))
        {
            openApiDocument.Servers.Add(openApiServer);
        }
    }

    private OpenApiDocEndpointOption GetOpenApiDocEndpoint(string serviceName, string requestUri)
    {
        var apiJsonEndpoint = this._openApiDocumentEndpointRepository.GetAsync(serviceName);

        apiJsonEndpoint.ParseRelateUri(requestUri);
        return apiJsonEndpoint;
    }
}