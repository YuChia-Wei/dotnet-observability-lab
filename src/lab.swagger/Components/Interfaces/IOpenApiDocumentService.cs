namespace lab.swagger.Components.Interfaces;

public interface IOpenApiDocumentService
{
    /// <summary>
    /// 取得 OpenApiDocument Json
    /// </summary>
    /// <param name="serviceName"></param>
    /// <param name="requestUri"></param>
    /// <returns></returns>
    Task<string> GetJsonAsync(string serviceName, string requestUri);
}