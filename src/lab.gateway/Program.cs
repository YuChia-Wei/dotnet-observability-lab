using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using lab.gateway.Infrastructure.YarpComponents.Extenstions;
using lab.gateway.Infrastructure.YarpComponents.Metrics;
using lab.gateway.Models;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .AddJsonFile("ReverseProxy-ClustersSetting.json", true, true)
       .AddJsonFile("ReverseProxy-RoutesSetting.json", true, true);

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

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddHealthChecks();

builder.Services.AddW3CLogging(logging =>
{
    // Log all W3C fields
    logging.LoggingFields = W3CLoggingFields.All;

    logging.FileSizeLimit = 5 * 1024 * 1024;
    logging.RetainedFileCountLimit = 2;

    logging.FileName = $"{AppDomain.CurrentDomain.FriendlyName}_{Environment.MachineName}_";
    logging.FlushInterval = TimeSpan.FromSeconds(2);

    logging.AdditionalRequestHeaders.Add("x-forwarded-for");
});

builder.Services.AddHttpClient();

builder.Services.AddHttpForwarder();

// Add the reverse proxy capability to the server
builder.Services
       .AddReverseProxy()
       .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddAllYarpMetrics();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                               ForwardedHeaders.XForwardedProto |
                               ForwardedHeaders.XForwardedHost;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                       ForwardedHeaders.XForwardedProto |
                       ForwardedHeaders.XForwardedHost
});

// 攔截 favicon.ico 的路徑，因為 Proxy 的 Route 設定中尚未發現排除特定路徑的設定方式
app.MapGet("/favicon.ico", () => "");
// app.Map("/favicon.ico", () => Results.File("favicon.ico", "images/ico"));

app.UseW3CLogging();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseHealthChecks("/health");

app.MapGet("/gateway-config",
           GatewayConfig ([FromServices] IProxyConfigProvider proxyConfig) => proxyConfig.GetConfig().ToGatewayConfig());

app.MapReverseProxy();

app.Run();