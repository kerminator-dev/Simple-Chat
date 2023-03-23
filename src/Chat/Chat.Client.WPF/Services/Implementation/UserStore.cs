using Chat.Client.WPF.Entities;
using Chat.Client.WPF.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Chat.Client.WPF.Services.Implementation
{
    internal class UserStore : IStore<string, UserEntity>
    {
        private readonly ConcurrentDictionary<string, UserEntity> _users;

        public UserStore()
        {
            _users = new ConcurrentDictionary<string, UserEntity>();
        }

        public event Action<string, UserEntity>? OnNewItemAdded;
        public event Action<string, UserEntity>? OnItemUpdated;
        public event Action<string, UserEntity>? OnItemRemoved;

        public Task Create(UserEntity user)
        {
            if (_users.TryAdd(user.Username, user))
            {
                OnNewItemAdded?.Invoke(user.Username, user);
            }

            return Task.CompletedTask; 
        }

        public Task Delete(UserEntity user)
        {
            if (_users.TryRemove(user.Username, out user))
            {
                OnItemRemoved?.Invoke(user.Username, user);
            }

            return Task.CompletedTask;
        }

        public Task<UserEntity> Get(string username)
        {
            return Task.FromResult(_users[username]);
        }

        public Task Update(UserEntity user)
        {
            var oldUser = Get(user.Username).Result;

            if (oldUser == null)
                return Task.CompletedTask;

            if (_users.TryUpdate(user.Username, user, oldUser))
            {
                OnItemUpdated?.Invoke(user.Username, user);
            }

            return Task.CompletedTask;
        }
    }
}
