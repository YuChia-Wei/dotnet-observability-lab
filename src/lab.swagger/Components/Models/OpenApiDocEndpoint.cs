namespace lab.swagger.Components.Models;

/// <summary>
/// WebAPI 資訊
/// </summary>
public class OpenApiDocEndpoint
{
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
}