using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PlayboxCache;
using SqlDisCache.Infrastrukture.Entity;

namespace SqlDisCache.Infrastrukture.Epi;

public class DistributedOutputCacheStore : IOutputCacheStore
{
    private readonly IDistributedCache distributedCache;
    private readonly DistributedCacheContext distributedCacheContext;

    public DistributedOutputCacheStore(DistributedCacheContext distributedCacheContext)
    {
        this.distributedCache = distributedCache;
        this.distributedCacheContext = distributedCacheContext;
    }
    public async ValueTask EvictByTagAsync(string tag, CancellationToken cancellationToken)
    {
        var toDelete = await distributedCacheContext.DistributedCache.Where(x => x.Tags.Any(x => x.Id == tag)).ToArrayAsync();
        distributedCacheContext.RemoveRange(toDelete);
        await distributedCacheContext.SaveChangesAsync();
    }

    public async ValueTask<byte[]?> GetAsync(string key, CancellationToken cancellationToken)
    {
        return await distributedCache.GetAsync(key, cancellationToken);
    }

    public async ValueTask SetAsync(string key, byte[] value, string[]? tags, TimeSpan validFor, CancellationToken cancellationToken)
    {
        await distributedCache.SetAsync(key, value, cancellationToken);

        if (tags is not null && tags.Any())
        {
            var cacheItemToTag = await distributedCacheContext.DistributedCache.FirstOrDefaultAsync(x => x.Id == key);
            if (cacheItemToTag is null)
            {
                return;
            }

            cacheItemToTag.Tags = new List<CacheTag>();

            var tagsToConnect = new List<CacheTag>();
            foreach (var tag in tags)
            {
                var hasTag = await distributedCacheContext.Tags.FirstOrDefaultAsync(x => x.Id == tag);
                if (hasTag is not null)
                {
                    cacheItemToTag.Tags.Add(hasTag);
                }

                cacheItemToTag.Tags.Add(new CacheTag()
                {
                    Id = tag,
                });
            }

            distributedCacheContext.DistributedCache.Update(cacheItemToTag);
            await distributedCacheContext.SaveChangesAsync();
        }
    }
}