FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["lab.dotnet9.webapi/lab.dotnet9.webapi.csproj", "lab.dotnet9.webapi/"]
RUN dotnet restore "lab.dotnet9.webapi/lab.dotnet9.webapi.csproj"
COPY . .
WORKDIR "/src/lab.dotnet9.webapi"
RUN dotnet build "lab.dotnet9.webapi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "lab.dotnet9.webapi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lab.dotnet9.webapi.dll"]
