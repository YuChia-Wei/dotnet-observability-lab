using System.Diagnostics.Metrics;
using Yarp.Telemetry.Consumption;

namespace lab.gateway.Infrastructure.YarpComponents.Metrics;

public sealed class YarpDnsMetrics : IMetricsConsumer<NameResolutionMetrics>
{
    private static readonly Counter<long> DnsLookupsRequested = MyMeter.Meter.CreateCounter<long>(
        "yarp_dns_lookups_requested",
        description: "Number of DNS lookups requested"
    );

    private static readonly ObservableGauge<double> AverageLookupDuration = MyMeter.Meter.CreateObservableGauge<double>(
        "yarp_dns_average_lookup_duration",
        () => _averageLookupDurationTotalMilliseconds,
        "milliseconds",
        "Average DNS lookup duration"
    );

    private static double _averageLookupDurationTotalMilliseconds;

    public void OnMetrics(NameResolutionMetrics previous, NameResolutionMetrics current)
    {
        DnsLookupsRequested.Add(current.DnsLookupsRequested);
        _averageLookupDurationTotalMilliseconds = current.AverageLookupDuration.TotalMilliseconds;
    }
}