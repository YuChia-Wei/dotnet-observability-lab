using Microsoft.OpenApi.Models;

namespace lab.swagger.Infrastructure.ServiceCollectionExtension;

/// <summary>
/// Swagger 設定
/// </summary>
public static class SwaggerSettingExtension
{
    /// <summary>
    /// 加入 Open Api 文件產生設定 (swagger Gen)
    /// </summary>
    /// <param name="services"></param>
    public static void AddOpenApiDocGenerate(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Swagger UI V1",
                Version = "v1"
            });

            var basePath = AppContext.BaseDirectory;
            var xmlFiles = Directory.EnumerateFiles(basePath, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var xmlFile in xmlFiles)
            {
                options.IncludeXmlComments(xmlFile);
            }
        });
    }
}