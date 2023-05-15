using Microsoft.AspNetCore.HttpOverrides;
using lab.component.Extenstion;
using lab.dotnet6.webapi.Infrastructure.Extensions;

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

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.UseDateOnlyTimeOnlyStringConverters());
builder.Services.AddDateOnlyTimeOnlyStringConverters();

builder.Services.AddHealthChecks();

// 註冊元件
builder.Services.RegisterDepend();

builder.Services.AddMediator(o => o.ServiceLifetime = ServiceLifetime.Scoped);

var app = builder.Build();

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

app.MapControllers();

app.Run();