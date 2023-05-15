using System;
using System.Data;
using Dapper;
using lab.component.EfCore;
using lab.component.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace lab.component.Extenstion;

/// <summary>
/// Dapper DbConnection
/// </summary>
public static class RegisterExtension
{
    private static readonly string DbConnectionString = Environment.GetEnvironmentVariable("DBCONNECTION") ??
                                                        "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=postgres;Pooling=true;";
    // "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=postgres;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";

    private static readonly string RedisConnectionString = Environment.GetEnvironmentVariable("REDISCONNECTION") ??
                                                           "redis:6379";

    /// <summary>
    /// Adds the Dapper Helper and SqlConnection.
    /// </summary>
    /// <remarks></remarks>
    /// <param name="services">The services.</param>
    public static void RegisterDepend(this IServiceCollection services)
    {
        //https://github.com/DapperLib/Dapper/issues/1715#issuecomment-1478021548
        SqlMapper.AddTypeHandler(typeof(TimeOnly), new TimeOnlyTypeHandler());
        SqlMapper.AddTypeHandler(typeof(DateOnly), new DateOnlyTypeHandler());

        services.AddHttpClient();

        services.AddScoped<WeatherForecastDapperRepository>();
        services.AddScoped<WeatherForecastEfCoreRepository>();
        services.AddScoped<WeatherForecastOutboundApiRepository>();
        services.AddScoped<WeatherForecastRedisRepository>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = RedisConnectionString;
            options.InstanceName = $"{AppDomain.CurrentDomain.FriendlyName}:";
        });

        services.AddDbContext<TestContext>(
            (provider, builder) =>
            {
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

                builder.UseLoggerFactory(loggerFactory)
                       .UseNpgsql(DbConnectionString)
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            },
            ServiceLifetime.Scoped,
            ServiceLifetime.Singleton);

        services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(DbConnectionString));
    }

    //https://github.com/DapperLib/Dapper/issues/1715#issuecomment-1478021548
    private class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly> // Dapper handler for DateOnly
    {
        public override DateOnly Parse(object value)
        {
            return DateOnly.FromDateTime((DateTime)value);
        }

        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.DbType = DbType.Date;
            parameter.Value = value;
        }
    }

    //https://github.com/DapperLib/Dapper/issues/1715#issuecomment-1478021548
    private class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly> // Dapper handler for TimeOnly
    {
        public override TimeOnly Parse(object value)
        {
            if (value.GetType() == typeof(DateTime))
            {
                return TimeOnly.FromDateTime((DateTime)value);
            }
            else if (value.GetType() == typeof(TimeSpan))
            {
                return TimeOnly.FromTimeSpan((TimeSpan)value);
            }

            return default;
        }

        public override void SetValue(IDbDataParameter parameter, TimeOnly value)
        {
            parameter.DbType = DbType.Time;
            parameter.Value = value;
        }
    }
}