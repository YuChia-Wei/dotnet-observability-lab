# dotnet observability lab

此份練習專案中的 aspnetcore base image 都使用我另外一個專案 " [otel-dotnet-auto-instrumentation](https://github.com/YuChia-Wei/otel-dotnet-auto-instrumentation) " 為基礎。
如果對該容器基底有安全性疑慮，可以自行調整容器。

另外，此份專案有兩個比較特別的功能專案，分別是 swagger hub 跟 api gateway (使用 yarp 建立)，可以在我的 github 中找到單獨的 repo
- swagger hub
  - [github]()
- yarp gateway
  - [github](https://github.com/YuChia-Wei/application-gateway-lab)
  > 我正打算開發 yarp 專用的 gateway 管理工具 [yarp-controller](https://github.com/YuChia-Wei/yarp-controller)，不過還只在想法階段，希望哪天可以建起來。
- webapi
  - [github](https://github.com/YuChia-Wei/dotnet-webapi-lab)

## architecture

![](./doc/img/architecture-diagram.png)

## dotnet 6 dateOnly / timeOnly

🚨🚨🚨this is memo!🚨🚨🚨

🚩🚩🚩只有 .net 6 有這議題， .net 7 已內建支援🚩🚩🚩

- https://stackoverflow.com/questions/69187622/how-can-i-use-dateonly-timeonly-query-parameters-in-asp-net-core-6
- https://github.com/maxkoshevoi/DateOnlyTimeOnly.AspNet/tree/main

## dotnet 8

- [dotnet doc: all metrics](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/built-in-metrics-aspnetcore?view=aspnetcore-8.0)
- [Grafana AspNetCore Dashboard](https://github.com/JamesNK/aspnetcore-grafana/tree/main/dashboards)
- dotnet 8 meter name
  - Microsoft.AspNetCore.Hosting
  - Microsoft.AspNetCore.Server.Kestrel
  - sample:
    ```csharp
    builder.AddMeter("Microsoft.AspNetCore.Hosting",
                     "Microsoft.AspNetCore.Server.Kestrel");
    ```
