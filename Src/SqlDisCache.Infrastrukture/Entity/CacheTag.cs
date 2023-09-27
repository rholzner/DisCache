namespace SqlDisCache.Infrastrukture.Entity;

public class CacheTag
{
    public string Id { get; set; }

    public IList<TestCache> CachItems { get; set; }
}
