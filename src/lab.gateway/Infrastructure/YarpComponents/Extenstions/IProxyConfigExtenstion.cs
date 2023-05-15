using lab.gateway.Models;
using Yarp.ReverseProxy.Configuration;

namespace lab.gateway.Infrastructure.YarpComponents.Extenstions;

public static class ProxyConfigExtenstion
{
    public static GatewayConfig ToGatewayConfig(this IProxyConfig proxyConfig)
    {
        return new GatewayConfig(proxyConfig);
    }
}