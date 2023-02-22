namespace ChatAPI.Mappings
{
    public class ConnectionMapping<TKey>
    {
        private readonly Dictionary<TKey, HashSet<string>> _connections;

        public int Count => _connections.Count;

        public ConnectionMapping()
        {
            _connections = new Dictionary<TKey, HashSet<string>>();
        }

        public bool Contains(TKey key)
        {
            return _connections.ContainsKey(key);
        }

        public void Add(TKey key, string connectionId)
        {
            lock(_connections)
            {
                if (!_connections.TryGetValue(key, out HashSet<string> connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
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
                if (!_connections.TryGetValue(key, out HashSet<string> connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }

        public IEnumerable<string> GetConnections(TKey key)
        {
            if (_connections.TryGetValue(key, out HashSet<string> connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }
    }
}
