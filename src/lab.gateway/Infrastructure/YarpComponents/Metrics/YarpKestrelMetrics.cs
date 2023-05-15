using System.Diagnostics.Metrics;
using Yarp.Telemetry.Consumption;

namespace lab.gateway.Infrastructure.YarpComponents.Metrics;

public sealed class YarpKestrelMetrics : IMetricsConsumer<KestrelMetrics>
{
    private static readonly Counter<long> TotalConnections = MyMeter.Meter.CreateCounter<long>(
        "yarp_kestrel_total_connections",
        description: "Number of incoming connections opened"
    );

    private static readonly Counter<long> TotalTlsHandshakes = MyMeter.Meter.CreateCounter<long>(
        "yarp_kestrel_total_tls_Handshakes",
        description: "Number of TLS handshakes started"
    );

    private static readonly ObservableGauge<double> CurrentTlsHandshakes = MyMeter.Meter.CreateObservableGauge<double>(
        "yarp_kestrel_current_tls_handshakes",
        description: "Number of active TLS handshakes that have started but not yet completed or failed",
        observeValue: () => _currentTlsHandshakes
    );

    private static readonly Counter<long> FailedTlsHandshakes = MyMeter.Meter.CreateCounter<long>(
        "yarp_kestrel_failed_tls_handshakes",
        description: "Number of TLS handshakes that failed"
    );

    private static readonly ObservableGauge<double> CurrentConnections = MyMeter.Meter.CreateObservableGauge<double>(
        "yarp_kestrel_current_connections",
        description: "Number of currently open incoming connections",
        observeValue: () => _currentConnections
    );

    private static readonly ObservableGauge<double> ConnectionQueueLength = MyMeter.Meter.CreateObservableGauge<double>(
        "yarp_kestrel_connection_queue_length",
        description: "Number of connections on the queue.",
        observeValue: () => _connectionQueueLength
    );

    private static readonly ObservableGauge<double> RequestQueueLength = MyMeter.Meter.CreateObservableGauge<double>(
        "yarp_kestrel_request_queue_length",
        description: "Number of requests on the queue",
        observeValue: () => _requestQueueLength
    );

    private static double _currentTlsHandshakes;
    private static double _currentConnections;
    private static double _connectionQueueLength;
    private static double _requestQueueLength;

    public void OnMetrics(KestrelMetrics previous, KestrelMetrics current)
    {
        TotalConnections.Add(current.TotalConnections);
        TotalTlsHandshakes.Add(current.TotalTlsHandshakes);
        _currentTlsHandshakes = current.CurrentTlsHandshakes;
        FailedTlsHandshakes.Add(current.FailedTlsHandshakes);
        _currentConnections = current.CurrentConnections;
        _connectionQueueLength = current.ConnectionQueueLength;
        _requestQueueLength = current.RequestQueueLength;
    }
}