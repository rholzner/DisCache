using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlayboxCache;
using SqlDisCache.Domain.Helpers;
using SqlDisCache.Infrastrukture.Helpers;

namespace SqlDisCache.Infrastrukture;

public static class Setup
{
    public static IServiceCollection AddSqlDisCacheInfrastruktur(this IServiceCollection serviceProvider, Action<DbContextOptionsBuilder>? optionsAction = null, string connectionString = null)
    {
        serviceProvider.AddTransient<IDatabaseHelper, DatabaseHelper>();

        if (optionsAction is not null)
        {
            serviceProvider.AddDbContext<DistributedCacheContext>(optionsAction);
        }
        else
        {
            serviceProvider.AddDbContext<DistributedCacheContext>(
                o => o.UseSqlServer(connectionString,
                opt =>
                {
                    opt.MigrationsHistoryTable("DistributedCacheContextsMigrations", "DistributedCacheContext");
                }));
        }


        serviceProvider.AddDistributedSqlServerCache(opt =>
        {
            opt.ConnectionString = connectionString;
            opt.SchemaName = "dbo";
            opt.TableName = "DistributedCache";
        });


        return serviceProvider;
    }
}