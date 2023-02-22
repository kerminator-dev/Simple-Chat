using ChatAPI.Entities;
using ChatAPI.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ChatAPI.Services.Implementation
{
    public class InMemoryUserRepository : ICache<string, User>
    {
        private readonly MemoryCache _cache;

        public InMemoryUserRepository()
        {
            var cacheOptions = new MemoryCacheOptions();
            cacheOptions.ExpirationScanFrequency = TimeSpan.FromHours(4);
            cacheOptions.SizeLimit = 100;

            _cache = new MemoryCache(cacheOptions);
        }

        public InMemoryUserRepository(MemoryCacheOptions cacheOptions)
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

            value = default(User); 
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
