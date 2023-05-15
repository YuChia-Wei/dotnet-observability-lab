using lab.swagger.Components.OptionModels;

namespace lab.swagger.Components.Interfaces;

/// <summary>
/// Open Api 文件來源紀錄表
/// </summary>
public interface IOpenApiDocumentEndpointRepository
{
    /// <summary>
    /// 取得 OpenApi 文件的 Url 資料
    /// </summary>
    /// <param name="serviceName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    OpenApiDocEndpointOption GetAsync(string serviceName);

    /// <summary>
    /// 取得目前記錄的 OpenApi 文件 Url 清單
    /// </summary>
    /// <returns></returns>
    IEnumerable<OpenApiDocEndpointOption> GetList();
}