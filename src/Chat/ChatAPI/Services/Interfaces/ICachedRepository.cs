using Microsoft.Extensions.Caching.Memory;

namespace ChatAPI.Services.Interfaces
{
    public interface ICache<TKey, TValue>
    {
        public bool TryGetValue(TKey key, out TValue? value);
        TValue Set(TKey key, TValue value, TimeSpan expiresAfter);
        TValue Set(TKey key, TValue value);
        void Remove(TKey key);
        void Clear();
    }
}
