using System.Diagnostics;
using System.Net.Mime;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using lab.component.Extenstion;
using lab.dotnet8.webapi.Infrastructure.ResponseWrapper;
using lab.dotnet8.webapi.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin()
                         .AllowAnyHeader()
                         .AllowAnyMethod();
    });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                               ForwardedHeaders.XForwardedProto |
                               ForwardedHeaders.XForwardedHost;
});

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiResponseWrappingFilter>();
    options.Filters.Add<ExceptionWrappingFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//via. https://github.com/dotnet/aspnet-api-versioning
builder.Services.AddApiVersioning(options =>
       {
           options.ReportApiVersions = true;
           options.AssumeDefaultVersionWhenUnspecified = true;
           options.DefaultApiVersion = new ApiVersion(1, 0);
       })
       //加上這個可以使用 namespace 當作版本控制來源
       .AddMvc(o => o.Conventions.Add(new VersionByNamespaceConvention()))
       .AddApiExplorer(options =>
       {
           options.GroupNameFormat = "'v'VVV";
           options.SubstituteApiVersionInUrl = true;
       });

builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

// 註冊元件
builder.Services.RegisterDepend();

builder.Services.AddMediator(o => o.ServiceLifetime = ServiceLifetime.Scoped);

//aspire component
if (Environment.GetEnvironmentVariable("TRIGGER_BY_ASPIRE") == "TRUE")
{
    // builder.AddServiceDefaults();
}

var app = builder.Build();

//使用自訂物件樣式回應
app.UseExceptionHandler(applicationBuilder =>
{
    applicationBuilder.Run(async context =>
    {
        // 取得 ILogger 以便另外撰寫日誌
        var logger = context.RequestServices.GetRequiredService<ILogger>();

        // 取得 IExceptionHandlerPathFeature 的資料，以便後續針對例外內容進行處理
        var exception = context.Features.Get<IExceptionHandlerPathFeature>()!.Error;

        logger.LogError("Exception occured: {ExceptionMessage} , Exception Description: {ExceptionDescription} ",
                        exception.Message,
                        exception.ToString());

        // 建立包含錯誤資訊的 api response 物件
        var failResultViewModel = new ApiResponse<ApiErrorInformation>
        {
            // 取得該次錯誤時的追蹤編號以便設定在 error information 中
            Id = Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString(),
            ApiVersion = context.ApiVersioningFeature().RawRequestedApiVersion,
            RequestPath = $"{context.Request.Path}.{context.Request.Method}",
            Data = exception.GetApiErrorInformation()
        };

        await context.Response.WriteAsJsonAsync(failResultViewModel);
    });
});

app.UseCors("CorsPolicy");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                       ForwardedHeaders.XForwardedProto |
                       ForwardedHeaders.XForwardedHost
});

app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");

// 純 API 服務不建議使用強制 https 轉址
// via https://learn.microsoft.com/zh-tw/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0&tabs=visual-studio
// via https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0&tabs=visual-studio
// app.UseHttpsRedirection();

app.UseAuthorization();

//aspire middleware
if (Environment.GetEnvironmentVariable("TRIGGER_BY_ASPIRE") == "TRUE")
{
    // app.MapDefaultEndpoints();
}

app.MapControllers();

app.Run();