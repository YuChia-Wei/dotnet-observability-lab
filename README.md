# dotnet observability lab

此份練習專案需要使用我另外一個 [otel-dotnet-auto-instrumentation](https://github.com/YuChia-Wei/otel-dotnet-auto-instrumentation) 所建立出來的 image，請先使用該專案建立對應的 image base 在本機中 (因為我還沒研究 github action 怎麼輸出多個不同 tag 的作法 😛😛)

## architecture

![](./doc/img/architecture-diagram.png)

## dotnet 6 dateOnly / timeOnly

🚨🚨🚨this is memo!🚨🚨🚨

🚩🚩🚩只有 .net 6 有這議題， .net 7 已內建支援🚩🚩🚩

- https://stackoverflow.com/questions/69187622/how-can-i-use-dateonly-timeonly-query-parameters-in-asp-net-core-6
- https://github.com/maxkoshevoi/DateOnlyTimeOnly.AspNet/tree/main
