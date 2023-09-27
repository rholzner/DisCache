using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlayboxCache;
using SqlDisCache.Domain.Helpers;

namespace SqlDisCache.Infrastrukture.Helpers;

internal class DatabaseHelper : IDatabaseHelper
{
    private readonly DistributedCacheContext db;
    private readonly ILogger<DatabaseHelper> logger;

    public DatabaseHelper(DistributedCacheContext db, ILogger<DatabaseHelper> logger)
    {
        this.db = db;
        this.logger = logger;
    }
    public async ValueTask MigrateDatabase()
    {
        try
        {
            db.Database.EnsureCreated();
            await db.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "DatabaseHelper:MigrateDatabase error in migration step");
        }
    }
}



