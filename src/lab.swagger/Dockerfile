#FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
FROM ghcr.io/yuchia-wei/otel-dotnet-auto-instrumentation:9.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["lab.swagger/lab.swagger.csproj", "lab.swagger/"]
RUN dotnet restore "lab.swagger/lab.swagger.csproj"
COPY . .
WORKDIR "/src/lab.swagger"
RUN dotnet build "lab.swagger.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "lab.swagger.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lab.swagger.dll"]
