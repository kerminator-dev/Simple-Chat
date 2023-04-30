using Chat.Core.DTOs.Notifications;
using Chat.Core.DTOs.Requests;
using Chat.Core.Enums;
using Chat.WebAPI.Services.Interfaces;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Mappings;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private const string USER_GETS_ONLINE_METHOD_NAME = "UserGetsOnline";
        private const string USER_GETS_OFFLINE_METHOD_NAME = "UserGetsOffline";
        private const string ACTIVE_USERS_METHOD_NAME = "ActiveUsers";

        private readonly CachedUserConnectionMapper<string> _userConnections;
        private readonly IPubSubService<string> _pubSubService;
        private readonly IContactService _contactService;
        private readonly IMessagingService _messagingService;

        public ChatHub(CachedUserConnectionMapper<string> connections, IPubSubService<string> pubSubService, IContactService contactService, IMessagingService messagingService)
        {
            _userConnections = connections;
            _pubSubService = pubSubService;
            _contactService = contactService;
            _messagingService = messagingService;
        }

        public async override Task OnConnectedAsync()
        {
            // Определение пользователя
            string username = Context.UserIdentifier;
            if (string.IsNullOrEmpty(username))
                Context.Abort();

            // Добавление в кэш подключение пользователя
            _userConnections.Add(username, Context.ConnectionId);

            // Если это первая сессия пользователя
            if (_userConnections.GetConnections(username).Count() == 1)
            {
                try
                {
                    // Получение списка контактов пользователя
                    var userContacts = await _contactService.GetAllUserContacts(username);

                    // Автоматическая подписка пользователя на уведомления его контактов
                    _pubSubService.Subscribe(username, userContacts);
                }
                catch (Exception) { }

                // Получение подключений подписчиков подключённого пользователя
                var subscribersConnectionIDs = this.GetSubscribersConnections(username);
                // Уведомление подписчиков пользователя о том, что пользователь подключился к хабу
                await this.NotifyConnectionsNewUserStatus(subscribersConnectionIDs, username, OnlineStatus.Online);
            }

            // Уведомление пользователя {username} о списке активных пользователей
            await NotifyUserOnlineContactsList(username);

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            // Определение пользователя
            string? username = Context.UserIdentifier;
            if (string.IsNullOrEmpty(username))
                Context.Abort();

            // Удаление из кэша
            _userConnections.Remove(username, Context.ConnectionId);

            // Получение оставшихся подключений пользователя
            var otherUserConnections = _userConnections.GetConnections(username);

            // Если подключений у пользователя нет - т. е. это было последнее подключение
            if (otherUserConnections == null || !otherUserConnections.Any())
            {
                // Автоматическая отписка пользователя от уведомлений его контактов
                _pubSubService.RemoveAllSubscribes(username);

                // Получение подключений подписчиков пользователя
                var subscribersConnectionIDs = this.GetSubscribersConnections(username);
                // Уведомление подписчиков пользователя о том, что пользователь {username} отключился
                await this.NotifyConnectionsNewUserStatus(subscribersConnectionIDs, username, OnlineStatus.Offline);
            }

            await base.OnDisconnectedAsync(exception);
        }
        
        /// <summary>
        /// Отправить сообщение пользователю
        /// </summary>
        /// <param name="messageRequestDTO">Объект сообщения</param>
        /// <returns></returns>
        /// <exception cref="HubException">Исключение при попытке отправки сообщения</exception>
        public async Task SendMessage(SendTextMessageRequestDTO messageRequestDTO)
        {
            // Определение пользователя
            string? username = Context.UserIdentifier;
            if (string.IsNullOrEmpty(username))
                throw new HubException("User not found!")
                {
                    HResult = 400
                };

            try
            {
                // Отправка сообщения
                await _messagingService.SendMessage(senderUsername: username, messageRequestDTO);
            }
            catch (Exception ex) when (ex is MessageNotSentException || ex is EntityNotFoundException || ex is ArgumentException)
            {
                throw new HubException(ex.Message)
                {
                    HResult = 400
                };
            }
            catch (Exception)
            {
                throw new HubException()
                {
                    HResult = 500
                };
            }
        }

        /// <summary>
        /// Получить список подключений подписчиков пользователя
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private IEnumerable<string> GetSubscribersConnections(string username)
        {
            // Получение подписчиков пользователя
            var subscribers = _pubSubService.GetSubscribers(username);

            // Получение подключений подписчиков пользователя
            return _userConnections.GetConnections(subscribers);
        }

        private async Task NotifyConnectionsNewUserStatus(IEnumerable<string> receiversConnectionIDs, string username, OnlineStatus status)
        {
            // Рассылка уведомления на все подключения за исключением подключений текущего пользователя
            switch (status)
            {
                case OnlineStatus.Online:
                    await Clients.Clients(receiversConnectionIDs).SendAsync(USER_GETS_ONLINE_METHOD_NAME, username);
                    break;
                case OnlineStatus.Offline:
                    await Clients.Clients(receiversConnectionIDs).SendAsync(USER_GETS_OFFLINE_METHOD_NAME, username);
                    break;
            }
        }

        private async Task NotifyUserOnlineContactsList(string receiverUsername)
        {
            // Получение списка контактов пользователя
            var userSubscribes = _pubSubService.GetSubscribes(receiverUsername);
            // Поиск только тех, кто в сети
            var activeUsers = userSubscribes.Where(s => _userConnections.Contains(s));

            var activeUsersNotification = new ActiveUsersNotificationDTO()
            {
                Usernames = activeUsers
            };

            await Clients.Caller.SendAsync(ACTIVE_USERS_METHOD_NAME, activeUsersNotification);
        }
    }
}
