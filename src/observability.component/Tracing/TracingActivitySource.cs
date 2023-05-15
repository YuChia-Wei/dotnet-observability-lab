using System.Diagnostics;

namespace observability.component.Tracing;

public static class TracingActivitySource
{
    public static readonly ActivitySource RegisteredActivity = new(DiagnosticsResource.Name());
}