using System;
using System.Collections.Concurrent;

namespace ChatAPI.Mappings
{
    public class ConnectionMapper<TKey> where TKey : class
    {
        private readonly ConcurrentDictionary<TKey, HashSet<string>> _connections;

        public ConnectionMapper()
        {
            _connections = new ConcurrentDictionary<TKey, HashSet<string>>();
        }

        public IEnumerable<TKey> GetAllKeysExcept(TKey exceptKey)
        {
            return _connections.Where(c => !c.Key.Equals(exceptKey)).Select(c => c.Key).ToList();
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
    }
}
