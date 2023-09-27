using Microsoft.Extensions.DependencyInjection;
using SqlDisCache.Domain.Helpers;

namespace SqlDisCache.Application;

public static class Setup
{
    public static IServiceCollection AddSqlDisCacheApplication(this IServiceCollection serviceProvider,bool autoMigration = true)
    {
        if (autoMigration)
        {
            serviceProvider.AddHostedService<MigrationTask>();
        }

        serviceProvider.AddScoped<ISqlDisCache,SqlDisCache>();

        return serviceProvider;
    }
}
