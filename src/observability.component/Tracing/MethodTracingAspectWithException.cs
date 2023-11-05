using System;
using System.Diagnostics;
using AspectInjector.Broker;
using OpenTelemetry.Trace;

namespace observability.component.Tracing;

[Aspect(Scope.PerInstance)]
public class MethodTracingAspectWithException
{
    [Advice(Kind.Around, Targets = Target.Method)]
    public object Around([Argument(Source.Name)] string name,
                         [Argument(Source.Arguments)] object[] args,
                         [Argument(Source.Type)] Type hostType,
                         [Argument(Source.Target)] Func<object[], object> target)
    {
        using var startActivity = TracingActivitySource.RegisteredActivity.StartActivity($"{hostType.Name}.{name}");
        try
        {
            var result = target(args);
            return result;
        }
        catch (Exception e)
        {
            //這邊不知道什麼原因，Rider 有跳可能為 null 的警告
            startActivity.SetStatus(ActivityStatusCode.Error);
            startActivity.RecordException(e);
            throw;
        }
    }
}