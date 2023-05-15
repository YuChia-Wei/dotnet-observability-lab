using lab.swagger.Components.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lab.swagger.ApiControllers;

/// <summary>
/// 取得 OpenApi 文件
/// </summary>
[Route("api/doc")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class OpenApiDocumentController : ControllerBase
{
    private readonly IOpenApiDocumentService _openApiDocumentService;

    /// <summary>
    /// ctor
    /// </summary>
    public OpenApiDocumentController(IOpenApiDocumentService openApiDocumentService)
    {
        this._openApiDocumentService = openApiDocumentService;
    }

    /// <summary>
    /// 取得特定服務
    /// </summary>
    /// <param name="serviceName"></param>
    /// <returns></returns>
    [HttpGet("{serviceName}")]
    public async Task<string> Get([FromRoute] string serviceName)
    {
        var httpContextRequest = this.HttpContext.Request;
        var requestUri = $"{httpContextRequest.Scheme}://{httpContextRequest.Host}";
        var jsonAsync = await this._openApiDocumentService.GetJsonAsync(serviceName, requestUri);
        return jsonAsync;
    }
}