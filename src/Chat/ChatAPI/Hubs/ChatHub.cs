using Chat.Core.DTOs.Notifications;
using Chat.Core.Enums;
using ChatAPI.Mappings;
using Microsoft.AspNetCore.Authorization;
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

        public ChatHub(CachedUserConnectionMapper<string> connections)
        {
            _userConnections = connections;
        }

        public async override Task OnConnectedAsync()
        {
            // Определение пользователя
            string username = Context.UserIdentifier;
            if (string.IsNullOrEmpty(username))
                await base.OnDisconnectedAsync(new Exception("AccessTokenRequired!"));

            // Добавление в кэш
            _userConnections.Add(username, Context.ConnectionId);

            // Если это первая сессия пользователя
            if (_userConnections.GetConnections(username).Count() == 1)
            {
                // Уведомление подписчиков пользователя о том, что пользователь {username} подключился
                await NotifyOtherUsersNewUserStatus(Clients, username, OnlineStatus.Online);
            }

            // Уведомление пользователя {username} о списке активных пользователей
            await NotifyCallerOnlineUsersList(Clients.Caller, username);

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            // Определение пользователя
            string? username = Context.UserIdentifier;
            if (string.IsNullOrEmpty(username))
                await base.OnDisconnectedAsync(exception);

            // Удаление из кэша
            _userConnections.Remove(username, Context.ConnectionId);

            // Получение оставшихся подключений пользователя
            var otherUserConnections = _userConnections.GetConnections(username);

            // Если подключений у пользователя нет
            if (otherUserConnections == null || !otherUserConnections.Any())
            {

                // Уведомление подписчиков пользователя о том, что пользователь {username} отключился
                await NotifyOtherUsersNewUserStatus(Clients, username, OnlineStatus.Offline);
            }

            await base.OnDisconnectedAsync(exception);
        }


        private async Task NotifyOtherUsersNewUserStatus(IHubCallerClients clients, string username, OnlineStatus status)
        {
            // Получение списка всех подключений пользователя
            var userConnections = _userConnections.GetConnections(username);

            // Рассылка уведомления на все подключения за исключением подключений текущего пользователя
            switch (status)
            {
                case OnlineStatus.Online:
                    await clients.AllExcept(userConnections).SendAsync(USER_GETS_ONLINE_METHOD_NAME, username);
                    break;
                case OnlineStatus.Offline:
                    await clients.AllExcept(userConnections).SendAsync(USER_GETS_OFFLINE_METHOD_NAME, username);
                    break;
            }
        }

        private async Task NotifyCallerOnlineUsersList(ISingleClientProxy caller, string receiverUsername)
        {
            var activeUsers = new ActiveUsersNotificationDTO()
            {
                Usernames = _userConnections.GetAllKeysExcept(receiverUsername)
            };
            await caller.SendAsync(ACTIVE_USERS_METHOD_NAME, activeUsers);
        }
    }
}
