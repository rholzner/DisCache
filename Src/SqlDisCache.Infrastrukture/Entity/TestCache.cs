namespace SqlDisCache.Infrastrukture.Entity;

public partial class TestCache
{
    public string Id { get; set; }

    public byte[] Value { get; set; }

    public DateTimeOffset ExpiresAtTime { get; set; }

    public long? SlidingExpirationInSeconds { get; set; }

    public DateTimeOffset? AbsoluteExpiration { get; set; }

    public IList<CacheTag>? Tags { get; set; }
}
