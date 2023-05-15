using System;
using AspectInjector.Broker;

namespace observability.component.Tracing;

[Injection(typeof(MethodTracingAspect))]
public class MethodTracingAttribute : Attribute
{
}