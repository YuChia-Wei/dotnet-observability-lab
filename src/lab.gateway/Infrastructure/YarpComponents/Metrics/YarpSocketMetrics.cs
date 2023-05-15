using System.Diagnostics.Metrics;
using Yarp.Telemetry.Consumption;

namespace lab.gateway.Infrastructure.YarpComponents.Metrics;

public sealed class YarpSocketMetrics : IMetricsConsumer<SocketsMetrics>
{
    private static readonly Counter<long> OutgoingConnectionsEstablished = MyMeter.Meter.CreateCounter<long>(
        "yarp_sockets_outgoing_connections_established",
        description: "Number of outgoing (Connect) Socket connections established"
    );

    private static readonly Counter<long> IncomingConnectionsEstablished = MyMeter.Meter.CreateCounter<long>(
        "yarp_sockets_incoming_connections_established",
        description: "Number of incoming (Accept) Socket connections established"
    );

    private static readonly Counter<long> BytesReceived = MyMeter.Meter.CreateCounter<long>(
        "yarp_sockets_bytes_received",
        "bytes",
        "Number of bytes received"
    );

    private static readonly Counter<long> BytesSent = MyMeter.Meter.CreateCounter<long>(
        "yarp_sockets_bytes_sent",
        "bytes",
        "Number of bytes sent"
    );

    private static readonly Counter<long> DatagramsReceived = MyMeter.Meter.CreateCounter<long>(
        "yarp_sockets_datagrams_received",
        description: "Number of datagrams received"
    );

    private static readonly Counter<long> DatagramsSent = MyMeter.Meter.CreateCounter<long>(
        "yarp_sockets_datagrams_sent",
        description: "Number of datagrams Sent"
    );

    public void OnMetrics(SocketsMetrics previous, SocketsMetrics current)
    {
        OutgoingConnectionsEstablished.Add(current.OutgoingConnectionsEstablished);
        IncomingConnectionsEstablished.Add(current.IncomingConnectionsEstablished);
        BytesReceived.Add(current.BytesReceived);
        BytesSent.Add(current.BytesSent);
        DatagramsReceived.Add(current.DatagramsReceived);
        DatagramsSent.Add(current.DatagramsSent);
    }
}