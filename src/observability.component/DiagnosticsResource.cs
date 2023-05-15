using System;

namespace observability.component;

public static class DiagnosticsResource
{
    public static string Name()
    {
        return Environment.GetEnvironmentVariable("OTEL_SERVICE_NAME") ??
               AppDomain.CurrentDomain.FriendlyName.ToLower().Replace('.', '-');
    }
}