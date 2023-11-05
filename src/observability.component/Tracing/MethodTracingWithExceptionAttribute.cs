using System;
using AspectInjector.Broker;

namespace observability.component.Tracing;

/// <summary>
/// 有使用 open telemetry api 來設定該 span 的例外資料
/// </summary>
[Injection(typeof(MethodTracingAspectWithException))]
public class MethodTracingWithExceptionAttribute : Attribute
{
}