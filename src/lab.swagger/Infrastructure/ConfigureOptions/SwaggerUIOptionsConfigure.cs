using lab.swagger.Components.Interfaces;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace lab.swagger.Infrastructure.ConfigureOptions;

/// <summary>
/// SwaggerUiOption
/// </summary>
/// <remarks>https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1093</remarks>
public class SwaggerUiOptionsConfigure : IConfigureOptions<SwaggerUIOptions>
{
    private readonly IOpenApiDocumentEndpointService _openApiDocumentEndpointService;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="openApiDocumentEndpointService"></param>
    public SwaggerUiOptionsConfigure(
        IOpenApiDocumentEndpointService openApiDocumentEndpointService)
    {
        this._openApiDocumentEndpointService = openApiDocumentEndpointService;
    }

    /// <summary>
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerUIOptions options)
    {
        //Not really safe, but cant await here :(
        var services = this._openApiDocumentEndpointService.GetListAsync().GetAwaiter().GetResult();

        // options.RoutePrefix = "";

        // Clear the list of services before adding more
        options.ConfigObject.Urls = null;

        foreach (var service in services.Where(o => o.IsSwaggerEnabled))
        {
            options.SwaggerEndpoint($"/api/doc/{service.ServiceName}",
                                    $"{service.ServiceName}");
        }
    }
}