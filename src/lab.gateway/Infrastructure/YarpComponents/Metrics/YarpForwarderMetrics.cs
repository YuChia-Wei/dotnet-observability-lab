using System.Diagnostics.Metrics;
using Yarp.Telemetry.Consumption;

namespace lab.gateway.Infrastructure.YarpComponents.Metrics;

public sealed class YarpForwarderMetrics : IMetricsConsumer<ForwarderMetrics>
{
    private static readonly Counter<long> RequestsStarted = MyMeter.Meter.CreateCounter<long>(
        "yarp_proxy_requests_started",
        description: "Number of requests initiated through the proxy"
    );

    private static readonly Counter<long> RequestsFailed = MyMeter.Meter.CreateCounter<long>(
        "yarp_proxy_requests_failed",
        description: "Number of proxy requests that failed"
    );

    private static readonly ObservableGauge<long> CurrentRequests = MyMeter.Meter.CreateObservableGauge<long>(
        "yarp_proxy_current_requests",
        () => _currentRequest,
        description: "Number of active proxy requests that have started but not yet completed or failed"
    );

    private static long _currentRequest;

    public void OnMetrics(ForwarderMetrics previous, ForwarderMetrics current)
    {
        RequestsStarted.Add(current.RequestsStarted);
        RequestsFailed.Add(current.RequestsFailed);
        _currentRequest = current.CurrentRequests;
    }
}