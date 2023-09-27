using Microsoft.Extensions.Caching.Distributed;

namespace SqlDisCache.Domain.Helpers;

public interface ISqlDisCache
{
    void Set<T>(string key, T value, DistributedCacheEntryOptions options);
    Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken));


    T? Get<T>(string key);
    bool TryGet<T>(string key, out T cachedData);
    Task<T?> GetAsync<T>(string key, CancellationToken token = default(CancellationToken));


    void Refresh(string key);
    Task RefreshAsync(string key, CancellationToken token = default(CancellationToken));


    void Remove(string key);
    Task RemoveAsync(string key, CancellationToken token = default(CancellationToken));
}
