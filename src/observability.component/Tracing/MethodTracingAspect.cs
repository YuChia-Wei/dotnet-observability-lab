using System;
using AspectInjector.Broker;

namespace observability.component.Tracing;

[Aspect(Scope.PerInstance)]
public class MethodTracingAspect
{
    [Advice(Kind.Around, Targets = Target.Method)]
    public object Around(
        [Argument(Source.Name)] string name,
        [Argument(Source.Arguments)] object[] args,
        [Argument(Source.Type)] Type hostType,
        [Argument(Source.Target)] Func<object[], object> target)
    {
        using var startActivity = TracingActivitySource.RegisteredActivity.StartActivity($"{hostType.Name}.{name}");
        var result = target(args);
        return result;
    }
}