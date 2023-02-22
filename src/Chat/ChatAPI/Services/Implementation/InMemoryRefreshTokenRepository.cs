using ChatAPI.Entities;
using ChatAPI.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ChatAPI.Services.Implementation
{
    public class InMemoryRefreshTokenRepository : ICache<string, RefreshToken>
    {
        private readonly MemoryCache _cache;

        public InMemoryRefreshTokenRepository()
        {
            var cacheOptions = new MemoryCacheOptions();
            cacheOptions.ExpirationScanFrequency = TimeSpan.FromHours(4);
            cacheOptions.SizeLimit = 100;

            _cache = new MemoryCache(cacheOptions);
        }

        public InMemoryRefreshTokenRepository(MemoryCacheOptions cacheOptions)
        {
            _cache = new MemoryCache(cacheOptions);
        }

        public RefreshToken Set(string key, RefreshToken value, TimeSpan absoluteExpirationRelativeToNow)
        {
            return _cache.Set(key, value, absoluteExpirationRelativeToNow);
        }

        public RefreshToken Set(string key, RefreshToken value)
        {
            return _cache.Set(key, value);
        }

        public bool TryGetValue(string key, out RefreshToken? value)
        {
            if (_cache.TryGetValue(key, out value))
                return true;

            value = default(RefreshToken);
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
