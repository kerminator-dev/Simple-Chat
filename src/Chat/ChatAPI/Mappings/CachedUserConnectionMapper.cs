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

        public bool Contains(TKey user)
        {
            return _connections.ContainsKey(user);
        }

        public void Add(TKey user, string connectionId)
        {
            lock(_connections)
            {
                if (!_connections.TryGetValue(user, out HashSet<string>? connections))
                {
                    connections = new HashSet<string>();
                    _connections.TryAdd(user, connections);
                }

                lock(connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public void Remove(TKey user, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.TryGetValue(user, out HashSet<string>? connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(user, out var _);
                    }
                }
            }
        }

        public IEnumerable<string> GetConnections(TKey user)
        {
            if (_connections.TryGetValue(user, out HashSet<string>? connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public IEnumerable<string> GetConnections(IEnumerable<TKey> users)
        {
            return _connections.Where((c) => users.Contains(c.Key))
                               .SelectMany(s => s.Value);
        }

        public IEnumerable<TKey> GetAllKeysExcept(TKey exceptUser)
        {
            return _connections.Where(c => !c.Key.Equals(exceptUser)).Select(c => c.Key).ToList();
        }
    }
}
