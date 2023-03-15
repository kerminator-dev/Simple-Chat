using ChatAPI.Entities;
using ChatAPI.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Chat.WebAPI.Services.Implementation
{
    public class CachedUserRepository : ICache<string, User>
    {
        private readonly MemoryCache _cache;

        public CachedUserRepository()
        {
            var cacheOptions = new MemoryCacheOptions();
            cacheOptions.ExpirationScanFrequency = TimeSpan.FromHours(4);

            _cache = new MemoryCache(cacheOptions);
        }

        public CachedUserRepository(MemoryCacheOptions cacheOptions)
        {
            _cache = new MemoryCache(cacheOptions);
        }

        public User Set(string key, User value, TimeSpan absoluteExpirationRelativeToNow)
        {
            return _cache.Set(key, value, absoluteExpirationRelativeToNow);
        }

        public User Set(string key, User value)
        {
            return _cache.Set(key, value);
        }

        public bool TryGetValue(string key, out User? value)
        {
            if (_cache.TryGetValue(key, out value))
                return true;

            value = default;
            return false;
        }

        public void Clear()
        {
            _cache.Clear();
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
