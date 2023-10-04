FROM ghcr.io/yuchia-wei/otel-dotnet-auto-instrumentation:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["observability.component/observability.component.csproj", "observability.component/"]
COPY ["lab.gateway/lab.gateway.csproj", "lab.gateway/"]
RUN dotnet restore "lab.gateway/lab.gateway.csproj"
COPY . .
WORKDIR "/src/lab.gateway"
RUN dotnet build "lab.gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lab.gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lab.gateway.dll"]
