FROM ghcr.io/yuchia-wei/otel-dotnet-auto-instrumentation:8.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["observability.component/observability.component.csproj", "observability.component/"]
COPY ["lab.gateway/lab.gateway.csproj", "lab.gateway/"]
RUN dotnet restore "lab.gateway/lab.gateway.csproj"
COPY . .
WORKDIR "/src/lab.gateway"
RUN dotnet build "lab.gateway.csproj" \
    -f net8.0 \
    -c Release \
    -o /app/build \
    --os linux-musl \
    --arch x64

FROM build AS publish
RUN dotnet publish "lab.gateway.csproj" \
    -f net8.0 \
    -c Release \
    -o /app/publish \
    --os linux-musl \
    --arch x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lab.gateway.dll"]
