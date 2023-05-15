using lab.swagger.Components.OptionModels;

namespace lab.swagger.Components.Interfaces;

/// <summary>
/// </summary>
public interface IOpenApiDocumentEndpointService
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<OpenApiDocEndpointOption>> GetListAsync();
}