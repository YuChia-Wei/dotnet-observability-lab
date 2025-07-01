# dotnet observability lab

此份練習專案中的 aspnetcore base image 都使用我另外一個專案 " [otel-dotnet-auto-instrumentation](https://github.com/YuChia-Wei/otel-dotnet-auto-instrumentation) " 為基礎。

如果對該容器基底有安全性疑慮，可以自行調整容器。

另外，在此專案內的 api 與 swagger ui center、yarp gateway 都屬於簡易版

我在以下 Git Repo 中都有放置比較完整且會固定維護的版本，偶爾還會利用他們測試一些新功能，有興趣的可以瞧瞧~

- swagger hub
  - [github](https://github.com/YuChia-Wei/swagger-ui-center)
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
    > 由於 OpenTelemetry 的自動追蹤工具已經內建相關設定，如果有安裝自動追蹤工具或是使用我的基礎容器的話，可以不需要自行加入

## aspire resource

- install
  ```shell
  dotnet workload install aspire
  ```
- use template
  ```shell
  dotnet new aspire-starter --use-redis-cache --output AspireSample
  ```
- with keycloak
  [.NET Aspire Keycloak integration](https://learn.microsoft.com/en-us/dotnet/aspire/authentication/keycloak-integration?tabs=dotnet-cli)
  [Using Keycloak in .NET Aspire projects](https://nikiforovall.github.io/dotnet/keycloak/2024/06/02/aspire-support-for-keycloak.html)

## database

本機不使用 dockerfile 執行時，會需要自己準備 database，可利用 [這目錄](./docker/sql) 中的語法配合本機用 docker 執行資料庫做開發

docker 啟動資料庫的語法

```bash
docker run -d \
  --name postgres \
  --restart always \
  -e POSTGRES_DB=postgres \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=mysecretpassword \
  -v $(pwd)/sql/create_tables.sql:/docker-entrypoint-initdb.d/create_tables.sql \
  -v $(pwd)/sql/fill_tables.sql:/docker-entrypoint-initdb.d/fill_tables.sql \
  -p 5432:5432 \
  postgres:16-alpine
```

## grafana dashboard

這個 lab 支援以下兩個 grafana dashboard

- [ASP.NET Core](https://grafana.com/grafana/dashboards/19924-asp-net-core/)
- [ASP.NET Core Endpoint](https://grafana.com/grafana/dashboards/19925-asp-net-core-endpoint/)