using System.Diagnostics.Metrics;
using Yarp.Telemetry.Consumption;

namespace lab.gateway.Infrastructure.YarpComponents.Metrics;

/// <summary>
/// Collects outbound http metrics and exposes them using System.Diagnostics
/// </summary>
public sealed class YarpOutboundHttpMetrics : IMetricsConsumer<HttpMetrics>
{
    private static readonly Counter<long> OutboundRequestsStarted = MyMeter.Meter.CreateCounter<long>(
        "yarp_outbound_http_requests_started",
        description: "Number of outbound requests initiated by the proxy"
    );

    private static readonly Counter<long> OutboundRequestsFailed = MyMeter.Meter.CreateCounter<long>(
        "yarp_outbound_http_requests_failed",
        description: "Number of outbound requests failed"
    );

    private static readonly ObservableGauge<double> OutboundCurrentRequests = MyMeter.Meter.CreateObservableGauge<double>(
        "yarp_outbound_http_current_requests",
        description: "Number of active outbound requests that have started but not yet completed or failed",
        observeValue: () => _outboundCurrentRequests
    );

    private static readonly ObservableGauge<double> OutboundCurrentHttp11Connections = MyMeter.Meter.CreateObservableGauge<double>(
        "yarp_outbound_http11_connections",
        description: "Number of currently open HTTP 1.1 connections",
        observeValue: () => _outboundCurrentHttp11Connections
    );

    private static readonly ObservableGauge<double> OutboundCurrentHttp20Connections = MyMeter.Meter.CreateObservableGauge<double>(
        "yarp_outbound_http20_connections",
        description: "Number of active proxy requests that have started but not yet completed or failed",
        observeValue: () => _outboundCurrentHttp20Connections
    );

    private static readonly Histogram<double> OutboundHttp11RequestQueueDuration = MyMeter.Meter.CreateHistogram<double>(
        "yarp_outbound_http11_request_queue_duration",
        "milliseconds",
        "Average time spent on queue for HTTP 1.1 requests that hit the MaxConnectionsPerServer limit in the last metrics interval"
    );

    private static readonly Histogram<double> OutboundHttp20RequestQueueDuration = MyMeter.Meter.CreateHistogram<double>(
        "yarp_outbound_http20_request_queue_duration",
        "milliseconds",
        "Average time spent on queue for HTTP 2.0 requests that hit the MAX_CONCURRENT_STREAMS limit on the connection in the last metrics interval"
    );

    private static double _outboundCurrentRequests;
    private static double _outboundCurrentHttp11Connections;
    private static double _outboundCurrentHttp20Connections;

    public void OnMetrics(HttpMetrics previous, HttpMetrics current)
    {
        OutboundRequestsStarted.Add(current.RequestsStarted);
        OutboundRequestsFailed.Add(current.RequestsFailed);
        _outboundCurrentRequests = current.CurrentRequests;
        _outboundCurrentHttp11Connections = current.CurrentHttp11Connections;
        _outboundCurrentHttp20Connections = current.CurrentHttp20Connections;
        OutboundHttp11RequestQueueDuration.Record(current.Http11RequestsQueueDuration.TotalMilliseconds);
        OutboundHttp20RequestQueueDuration.Record(current.Http20RequestsQueueDuration.TotalMilliseconds);
    }
}