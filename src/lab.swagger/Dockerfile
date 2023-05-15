# 這邊 image 尚不存在，可參考 https://github.com/YuChia-Wei/otel-dotnet-auto-instrumentation 自行建立對應的 image
FROM ghcr.io/yuchia-wei/otel-dotnet-auto-instrumentation:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["lab.swagger/lab.swagger.csproj", "lab.swagger/"]
RUN dotnet restore "lab.swagger/lab.swagger.csproj"
COPY . .
WORKDIR "/src/lab.swagger"
RUN dotnet build "lab.swagger.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lab.swagger.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lab.swagger.dll"]
