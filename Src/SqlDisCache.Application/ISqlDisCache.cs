using Microsoft.Extensions.Caching.Distributed;
using SqlDisCache.Domain.Helpers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace SqlDisCache.Application;

internal class SqlDisCache : ISqlDisCache
{
    private readonly IDistributedCache distributedCache;

    public SqlDisCache(IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;
    }

    public T? Get<T>(string key)
    {
        var r = distributedCache.Get(key);
        if (r == null || !r.Any())
        {
            return default(T?);
        }
        return FromByteArray<T>(r);
    }


    private byte[]? ToByteArray<T>(T obj)
    {
        if (obj == null)
            return null;

        return JsonSerializer.SerializeToUtf8Bytes(obj);
    }

    private T? FromByteArray<T>(byte[] data)
    {
        if (data == null)
            return default(T);

        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream(data))
        {
            T? ex = JsonSerializer.Deserialize<T>(ms);
            return ex;
        }
    }


    public async Task<T?> GetAsync<T>(string key, CancellationToken token = default)
    {
        var r = await distributedCache.GetAsync(key, token);
        if (r is null)
        {
            return default(T?);
        }

        return FromByteArray<T>(r);
    }

    public void Refresh(string key)
    {
        distributedCache.Refresh(key);
    }

    public Task RefreshAsync(string key, CancellationToken token = default)
    {
        return distributedCache.RefreshAsync(key, token);
    }

    public void Remove(string key)
    {
        distributedCache.Remove(key);
    }

    public Task RemoveAsync(string key, CancellationToken token = default)
    {
        return distributedCache.RemoveAsync(key, token);
    }

    public void Set<T>(string key, T value, DistributedCacheEntryOptions options)
    {
        var bytes = ToByteArray(value);
        distributedCache.Set(key, bytes, options);
    }

    public Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        var bytes = ToByteArray(value);
        return distributedCache.SetAsync(key, bytes, options, token);
    }

    public bool TryGet<T>(string key, out T cachedData)
    {
        var r = Get<T>(key);
        cachedData = r;
        return r is not null;
    }
}
