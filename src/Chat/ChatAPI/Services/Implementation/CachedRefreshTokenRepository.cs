﻿using ChatAPI.Entities;
using ChatAPI.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace ChatAPI.Services.Implementation
{
    public class CachedRefreshTokenRepository : ICache<string, RefreshToken>
    {
        private readonly MemoryCache _cache;

        public CachedRefreshTokenRepository()
        {
            var cacheOptions = new MemoryCacheOptions();
            cacheOptions.ExpirationScanFrequency = TimeSpan.FromHours(4);

            _cache = new MemoryCache(cacheOptions);
        }

        public CachedRefreshTokenRepository(IOptions<MemoryCacheOptions> cacheOptions)
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
