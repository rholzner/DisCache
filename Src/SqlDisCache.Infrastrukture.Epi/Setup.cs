using EPiServer.Framework.Cache;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SqlDisCache.Infrastrukture.Epi;

public static class Setup
{
    public static IServiceCollection UseEpiAsOutputCacheStore(this IServiceCollection services)
    {
        services.RemoveAll<IOutputCacheStore>();
        services.AddSingleton<IOutputCacheStore, DistributedOutputCacheStore>();
        return services;
    }

    public static void AddEpiPoll(this OutputCacheOptions outputCacheOptions)
    {
        outputCacheOptions.AddPolicy("EpiTest", new EpiOutputCachePolicy());
    }
}
