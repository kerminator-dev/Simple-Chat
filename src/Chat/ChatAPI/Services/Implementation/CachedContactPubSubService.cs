using Chat.WebAPI.Services.Interfaces;
using Chat.WebAPI.Utilities;
using System.Collections.Concurrent;

namespace Chat.WebAPI.Services.Implementation
{
    /// <summary>
    /// Реализация Pub/Sub кэширования контактов
    /// </summary>
    public class CachedContactPubSubService : IPubSubService<string>
    {
        // Пользователь - список подписок
        private readonly ConcurrentDictionary<string, ConcurrentHashSet<string>> _subscribersToPublishers;
        // Издатель - список подписчиков
        private readonly ConcurrentDictionary<string, ConcurrentHashSet<string>> _publishersToSubscribers;

        /// <summary>
        /// Пример
        /// 
        /// Список подписок пользователей admin и ann:
        /// admin = { ann, tom, kevin }
        /// ann   = { admin, kevin }
        /// 
        /// Список подписчиков пользователей admin и ann:
        /// ann   = { admin }
        /// tom   = { admin }
        /// kevin = { admin, ann }
        /// admin = { ann }
        /// 
        /// Вся эта дичь нужна для двустороннего поиска - поиск подписок пользователя 
        /// и поиск подписчиков пользователя
        /// </summary>

        public CachedContactPubSubService()
        {
            _subscribersToPublishers = new ConcurrentDictionary<string, ConcurrentHashSet<string>>();
            _publishersToSubscribers = new ConcurrentDictionary<string, ConcurrentHashSet<string>>();
        }

        /// <summary>
        /// Подписаться на пользователя <paramref name="publisher"/>
        /// </summary>
        /// <param name="subcriber">Подписчик</param>
        /// <param name="publisher">Издатель</param>
        public void Subscribe(string subcriber, string publisher)
        {
            if (subcriber == publisher)
                return;

            if (!_subscribersToPublishers.TryGetValue(subcriber, out var contacts))
            {
                contacts = new ConcurrentHashSet<string>();
                _subscribersToPublishers[subcriber] = contacts;
            }

            if (!_publishersToSubscribers.TryGetValue(publisher, out var usernames))
            {
                usernames = new ConcurrentHashSet<string>();
                _publishersToSubscribers[publisher] = usernames;
            }
            contacts.Add(publisher);
            usernames.Add(subcriber);
        }

        /// <summary>
        /// Подписаться на пользователей <paramref name="publishers"/>
        /// </summary>
        /// <param name="subcriber">Подписчик</param>
        /// <param name="publishers">Издатель</param>
        public void Subscribe(string subcriber, IEnumerable<string> publishers)
        {
            foreach (var publisher in publishers)
            {
                this.Subscribe(subcriber, publisher);
            }
        }

        /// <summary>
        /// Очистить подписки пользоватя <paramref name="username"/>
        /// </summary>
        /// <param name="username">Пользователь</param>
        public void RemoveAllSubscribes(string username)
        {
            if (_subscribersToPublishers.TryRemove(username, out var contacts))
            {
                foreach (var contact in contacts)
                    if (_publishersToSubscribers.TryGetValue(contact, out var usernames))
                    {
                        _ = usernames.TryRemove(username);
                    }
            }
        }

        /// <summary>
        /// Подписан ли пользователь <paramref name="subscriber"/> на <paramref name="publisher"/>?
        /// </summary>
        /// <param name="subscriber">Пользователь - подписчик</param>
        /// <param name="publisher">Пользователь - издатель</param>
        /// <returns></returns>
        public bool IsSubscribed(string subscriber, string publisher)
        {
            // Поиск в подписках подписчиков
            if (_subscribersToPublishers.TryGetValue(subscriber, out var contacts))
            {
                return contacts.Contains(publisher);
            }

            // Поиск в подписчиках издателей
            if (_publishersToSubscribers.TryGetValue(publisher, out var usernames))
            {
                return usernames.Contains(subscriber);
            }

            return false;
        }

        /// <summary>
        /// Отписаться от издателя <paramref name="publisher"/>
        /// </summary>
        /// <param name="subscriber">Подписчик</param>
        /// <param name="publisher">Издатель</param>
        public void Unsubscribe(string subscriber, string publisher)
        {
            // Удаление в подписках подписчиков
            if (_subscribersToPublishers.TryGetValue(subscriber, out var contacts))
            {
                _ = contacts.TryRemove(publisher);
            }

            // Удаление в подписчиках издателей
            if (_publishersToSubscribers.TryGetValue(publisher, out var usernames))
            {
                _ = usernames.TryRemove(subscriber);
            }
        }

        /// <summary>
        /// Получить список подписок/издателей пользователя <paramref name="subscriber"/>
        /// </summary>
        /// <param name="subscriber">Пользователь-подписчик</param>
        /// <returns></returns>
        public IEnumerable<string> GetSubscribes(string subscriber)
        {
            if (_subscribersToPublishers.TryGetValue(subscriber, out var publishers))
            {
                return publishers.ToArray();
            }
            return Enumerable.Empty<string>();
        }

        /// <summary>
        /// Получить список подписчиков издателя <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns></returns>
        public IEnumerable<string> GetSubscribers(string publisher)
        {
            if (_publishersToSubscribers.TryGetValue(publisher, out var subscribers))
            {
                return subscribers.ToArray();
            }
            return Enumerable.Empty<string>();
        }
    }
}