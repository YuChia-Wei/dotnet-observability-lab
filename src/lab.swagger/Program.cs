using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using lab.swagger.Components.Implements;
using lab.swagger.Components.Interfaces;
using lab.swagger.Components.OptionModels;
using lab.swagger.Infrastructure.ConfigureOptions;
using lab.swagger.Infrastructure.Middleware;
using lab.swagger.Infrastructure.ServiceCollectionExtension;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("swagger-endpoints.json", true, true);

builder.Services.Configure<ApiEndpointsSettingOption>(builder.Configuration.GetSection("ApiEndpointSetting"));

builder.Services.AddLogging();

// 處理中文轉碼
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin,
                                                 UnicodeRanges.CjkUnifiedIdeographs));

// API Url Path 使用小寫
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services
       .AddControllers()
       .AddJsonOptions(options =>
       {
           // ViewModel 與 Parameter 顯示為小駝峰命名
           options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
       });

builder.Services.AddOpenApiDocGenerate();

builder.Services.AddHttpClient();

// 註冊 Service
builder.Services.AddScoped<IOpenApiDocumentEndpointService, OpenApiDocumentEndpointService>();
builder.Services.AddScoped<IOpenApiDocumentService, OpenApiDocumentService>();

// 註冊 Repository
builder.Services.AddScoped<IOpenApiDocumentEndpointRepository, OpenApiDocumentEndpointRepository>();
builder.Services.AddScoped<IOpenApiDocumentRepository, OpenApiDocumentRepository>();
builder.Services.AddScoped<IConfigureOptions<SwaggerUIOptions>, SwaggerUiOptionsConfigure>();
builder.Services.AddScoped<DynamicSwaggerUiEndpointMiddleware>();

// 開啟 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("CorsPolicy");

app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });

app.UseRouting();

app.UseHealthChecks("/health");

app.UseSwagger();

//使用動態的 Swagger UI Endpoint List (Swagger UI Option)
app.UseDynamicSwaggerUiEndpoint();

app.MapDefaultControllerRoute();

app.MapControllers();

app.Run();