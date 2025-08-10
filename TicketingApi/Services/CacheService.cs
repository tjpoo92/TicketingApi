using Microsoft.Extensions.Caching.Memory;

namespace TicketingApi.Services;

public interface ICacheService
{
    T? Get<T>(string key);
    void Set<T>(string key, T value, TimeSpan? expiration = null);
    void Remove(string key);
    bool TryGetValue<T>(string key, out T? value);
}

public class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T? Get<T>(string key)
    {
        return _memoryCache.Get<T>(key);
    }

    public void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
        {
            options.AbsoluteExpirationRelativeToNow = expiration;
        }
        _memoryCache.Set(key, value, options);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }

    public bool TryGetValue<T>(string key, out T? value)
    {
        return _memoryCache.TryGetValue(key, out value);
    }
}
