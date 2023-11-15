FROM ghcr.io/yuchia-wei/otel-dotnet-auto-instrumentation:8.0-bookworm-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim AS build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["observability.component/observability.component.csproj", "observability.component/"]
COPY ["lab.component/lab.component.csproj", "lab.component/"]
COPY ["lab.outbound.webapi/lab.outbound.webapi.csproj", "lab.outbound.webapi/"]
RUN dotnet restore "lab.outbound.webapi/lab.outbound.webapi.csproj"
COPY . .
WORKDIR "/src/lab.outbound.webapi"
RUN dotnet build "lab.outbound.webapi.csproj" \
    -f net8.0 \
    -c Release \
    -o /app/build \
    --os linux \
    --arch x64

FROM build AS publish
RUN dotnet publish "lab.outbound.webapi.csproj" \
    -f net8.0 \
    -c Release \
    -o /app/publish \
    --os linux \
    --arch x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lab.outbound.webapi.dll"]
