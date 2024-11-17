namespace lab.dotnet8.webapi.Infrastructure.ResponseWrapper;

/// <summary>
/// 標準 api 回應
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// api 的追蹤編號
    /// </summary>
    /// <example>0B0C6D73-9D37-4527-B036-733ED304B5C3</example>
    public string Id { get; set; }

    /// <summary>
    /// api version
    /// </summary>
    public string? ApiVersion { get; set; }

    /// <summary>
    /// api request path
    /// </summary>
    public string RequestPath { get; set; }

    /// <summary>
    /// 回應資料
    /// </summary>
    public T Data { get; set; }
}