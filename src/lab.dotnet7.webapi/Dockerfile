FROM ghcr.io/yuchia-wei/otel-dotnet-auto-instrumentation:7.0-bookworm-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim AS build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["observability.component/observability.component.csproj", "observability.component/"]
COPY ["lab.component/lab.component.csproj", "lab.component/"]
COPY ["lab.dotnet7.webapi/lab.dotnet7.webapi.csproj", "lab.dotnet7.webapi/"]
RUN dotnet restore "lab.dotnet7.webapi/lab.dotnet7.webapi.csproj"
COPY . .
WORKDIR "/src/lab.dotnet7.webapi"
RUN dotnet build "lab.dotnet7.webapi.csproj" \
    -f net7.0 \
    -c Release \
    -o /app/build \
    --os linux \
    --arch x64

FROM build AS publish
RUN dotnet publish "lab.dotnet7.webapi.csproj" \
    -f net7.0 \
    -c Release \
    -o /app/publish \
    --os linux \
    --arch x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lab.dotnet7.webapi.dll"]
