using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SqlDisCache.Application;
using SqlDisCache.Infrastrukture;

namespace SqlDisCache.Manager;

public static class Setup
{
    public static IServiceCollection AddSqlDisCache(this IServiceCollection serviceProvider, Action<DbContextOptionsBuilder>? optionsAction = null, string connectionString = null)
    {
        serviceProvider.AddSqlDisCacheInfrastruktur(optionsAction, connectionString);
        serviceProvider.AddSqlDisCacheApplication();

        return serviceProvider;
    }
}