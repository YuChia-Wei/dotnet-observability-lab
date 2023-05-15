namespace lab.swagger.Components.OptionModels;

/// <summary>
/// Api Endpoint 設定檔
/// </summary>
public class ApiEndpointsSettingOption
{
    // public static string SectionName = "ApiEndpointSetting";

    /// <summary>
    /// </summary>
    public IEnumerable<OpenApiDocEndpointOption> ApiEndpoints { get; set; } 
}