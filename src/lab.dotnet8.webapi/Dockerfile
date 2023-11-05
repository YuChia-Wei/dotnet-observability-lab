FROM ghcr.io/yuchia-wei/otel-dotnet-auto-instrumentation:8.0-bookworm-slim AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /src
COPY ["observability.component/observability.component.csproj", "observability.component/"]
COPY ["lab.component/lab.component.csproj", "lab.component/"]
COPY ["lab.dotnet8.webapi/lab.dotnet8.webapi.csproj", "lab.dotnet8.webapi/"]
RUN dotnet restore "lab.dotnet8.webapi/lab.dotnet8.webapi.csproj"
COPY . .
WORKDIR "/src/lab.dotnet8.webapi"
RUN dotnet build "lab.dotnet8.webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lab.dotnet8.webapi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lab.dotnet8.webapi.dll"]
