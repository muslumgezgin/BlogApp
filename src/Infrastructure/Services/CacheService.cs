using Blog.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task CreateAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null)
    {
        if (absoluteExpiration == null)
        {
            absoluteExpiration = TimeSpan.FromMinutes(15);
        }

        _cache.Set(key, value, absoluteExpiration.Value);

        return Task.CompletedTask;
    }



    public Task<T?> GetAsync<T>(string key)
    {
        var value = _cache.Get<T>(key);

        return Task.FromResult(value);

    }
}