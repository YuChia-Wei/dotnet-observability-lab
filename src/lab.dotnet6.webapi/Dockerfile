﻿# 這邊 image 尚不存在，可參考 https://github.com/YuChia-Wei/otel-dotnet-auto-instrumentation 自行建立對應的 image
FROM ghcr.io/yuchia-wei/otel-dotnet-auto-instrumentation:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["observability.component/observability.component.csproj", "observability.component/"]
COPY ["lab.component/lab.component.csproj", "lab.component/"]
COPY ["lab.dotnet6.webapi/lab.dotnet6.webapi.csproj", "lab.dotnet6.webapi/"]
RUN dotnet restore "lab.dotnet6.webapi/lab.dotnet6.webapi.csproj"
COPY . .
WORKDIR "/src/lab.dotnet6.webapi"
RUN dotnet build "lab.dotnet6.webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lab.dotnet6.webapi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lab.dotnet6.webapi.dll"]
