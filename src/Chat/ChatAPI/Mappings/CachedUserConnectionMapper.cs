using System.Collections.Concurrent;

namespace ChatAPI.Mappings
{
    public class CachedUserConnectionMapper<TKey> where TKey : class
    {
        private readonly ConcurrentDictionary<TKey, HashSet<string>> _connections;

        public CachedUserConnectionMapper()
        {
            _connections = new ConcurrentDictionary<TKey, HashSet<string>>();
        }

        public bool Contains(TKey key)
        {
            return _connections.ContainsKey(key);
        }

        public void Add(TKey key, string connectionId)
        {
            lock(_connections)
            {
                if (!_connections.TryGetValue(key, out HashSet<string>? connections))
                {
                    connections = new HashSet<string>();
                    _connections.TryAdd(key, connections);
                }

                lock(connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public void Remove(TKey key, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.TryGetValue(key, out HashSet<string>? connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key, out var _);
                    }
                }
            }
        }

        public IEnumerable<string> GetConnections(TKey key)
        {
            if (_connections.TryGetValue(key, out HashSet<string>? connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public IEnumerable<string> GetConnections(IEnumerable<TKey> keys)
        {
            return _connections.Where((c) => keys.Contains(c.Key))
                               .SelectMany(s => s.Value);
        }

        public IEnumerable<TKey> GetAllKeysExcept(TKey exceptKey)
        {
            return _connections.Where(c => !c.Key.Equals(exceptKey)).Select(c => c.Key).ToList();
        }
    }
}
