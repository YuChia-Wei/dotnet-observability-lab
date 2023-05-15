using lab.swagger.Components.Interfaces;
using lab.swagger.Components.OptionModels;

namespace lab.swagger.Components.Implements;

/// <summary>
/// </summary>
public class OpenApiDocumentEndpointService : IOpenApiDocumentEndpointService
{
    private readonly IOpenApiDocumentEndpointRepository _openApiDocumentEndpointRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="openApiDocumentEndpointRepository"></param>
    public OpenApiDocumentEndpointService(IOpenApiDocumentEndpointRepository openApiDocumentEndpointRepository)
    {
        this._openApiDocumentEndpointRepository = openApiDocumentEndpointRepository;
    }

    /// <summary>
    /// Get OpenApi Doc Endpoint List
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<OpenApiDocEndpointOption>> GetListAsync()
    {
        return this._openApiDocumentEndpointRepository.GetList();
    }
}