using Chat.Client.WPF.Entities;
using Chat.Client.WPF.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Chat.Client.WPF.Services.Implementation
{
    internal class ChatStore : IStore<string, ChatEntity>
    {
        private readonly ConcurrentDictionary<string, ChatEntity> _chats;

        public ChatStore()
        {
            _chats = new ConcurrentDictionary<string, ChatEntity>();
        }

        public event Action<string, ChatEntity>? OnNewItemAdded;
        public event Action<string, ChatEntity>? OnItemUpdated;
        public event Action<string, ChatEntity>? OnItemRemoved;

        public Task Create(ChatEntity chat)
        {
            if (_chats.TryAdd(chat.ContactUsername, chat))
            {
                OnNewItemAdded?.Invoke(chat.ContactUsername, chat);
            }

            return Task.CompletedTask;
        }

        public Task Delete(ChatEntity chat)
        {
            if (_chats.TryRemove(chat.ContactUsername, out chat))
            {
                OnItemRemoved?.Invoke(chat.ContactUsername, chat);
            }

            return Task.CompletedTask;
        }

        public Task<ChatEntity> Get(string contactName)
        {
            return Task.FromResult(_chats[contactName]);
        }

        public Task Update(ChatEntity chat)
        {
            var oldChat = Get(chat.ContactUsername).Result;

            if (oldChat == null)
                return Task.CompletedTask;

            if (_chats.TryUpdate(chat.ContactUsername, chat, oldChat))
            {
                OnItemUpdated?.Invoke(chat.ContactUsername, chat);
            }

            return Task.CompletedTask;
        }
    }
}
