using System.Diagnostics.Metrics;

namespace lab.gateway.Infrastructure.YarpComponents.Metrics;

/// <summary>
/// https://learn.microsoft.com/zh-tw/dotnet/core/diagnostics/compare-metric-apis#systemdiagnosticsmetrics
/// </summary>
public static class MyMeter
{
    public static readonly Meter Meter = new(Environment.GetEnvironmentVariable("OTEL_SERVICE_NAME") ??
                                             AppDomain.CurrentDomain.FriendlyName.ToLower().Replace('.', '-'));
}