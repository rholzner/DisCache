using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SqlDisCache.Domain.Helpers;

namespace SqlDisCache.Application;

internal class MigrationTask : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<MigrationTask> logger;

    public MigrationTask(IServiceProvider serviceProvider, ILogger<MigrationTask> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation($"{nameof(MigrationTask)} is running.{Environment.NewLine}");

        using (var scope = serviceProvider.CreateScope())
        {
            var databaseHelper = scope.ServiceProvider.GetService<IDatabaseHelper>();
            if (databaseHelper is null)
            {
                logger.LogError("SqlDisCache:MigrationTask did not find service IDatabaseHelper, there for we can not ensure database and table is created");
                return;
            }

            await databaseHelper.MigrateDatabase();
        }
    }
}
