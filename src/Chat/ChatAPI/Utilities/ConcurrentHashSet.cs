namespace Chat.WebAPI.Utilities
{
    public class ConcurrentHashSet<T> : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock;
        private readonly HashSet<T> _hashSet;

        public ConcurrentHashSet()
        {
            _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _hashSet = new HashSet<T>();
        }

        public bool Add(T item)
        {
            try
            {
                _lock.EnterWriteLock();
                return _hashSet.Add(item);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }
        public void Add(IEnumerable<T> items)
        {
            try
            {
                _lock.EnterWriteLock();

                foreach (var item in items)
                   _ = _hashSet.Add(item);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            try
            {
                _lock.EnterWriteLock();
                _hashSet.Clear();
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }

        public bool Contains(T item)
        {
            try
            {
                _lock.EnterReadLock();
                return _hashSet.Contains(item);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }

        public bool Remove(T item)
        {
            try
            {
                _lock.EnterWriteLock();
                return _hashSet.Remove(item);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }


        public void Remove(IEnumerable<T> items)
        {
            try
            {
                _lock.EnterWriteLock();
                foreach (var item in items)
                   _ =_hashSet.Remove(item);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }

        public int Count
        {
            get
            {
                try
                {
                    _lock.EnterReadLock();
                    return _hashSet.Count;
                }
                finally
                {
                    if (_lock.IsReadLockHeld) _lock.ExitReadLock();
                }
            }
        }

        public T[] ToArray()
        {
            return _hashSet.ToArray();
        }


        #region Dispose
        public void Dispose()
        {
            _lock?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
