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

        private readonly ConnectionMapper<string> _connections;

        public ChatHub(ConnectionMapper<string> connections)
        {
            _connections = connections;
        }

        public async override Task OnConnectedAsync()
        {
            // Определение пользователя
            string? username = Context.UserIdentifier;
            if (string.IsNullOrEmpty(username))
                return;

            // Добавление в кэш
            _connections.Add(username, Context.ConnectionId);

            // Уведомление остальных участников о том, что пользователь {username} подключился
            await NotifyOtherUsersNewUserStatus(username, OnlineStatus.Online);

            // Уведомление пользователя {username} о списке активных пользователей
            await NotifyUserOnlineUsersList(username);

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            // Определение пользователя
            string? username = Context.UserIdentifier;
            if (string.IsNullOrEmpty(username))
                return;

            // Удаление из кэша
            _connections.Remove(username, Context.ConnectionId);

            // Получение оставшихся подключений пользователя
            var otherUserConnections = _connections.GetConnections(username);

            // Если подключений у пользователя нет
            if (otherUserConnections == null || otherUserConnections.Count() == 0)
            {
                // Уведомление остальных участников о том, что пользователь {username} отключился
                await NotifyOtherUsersNewUserStatus(username, OnlineStatus.Offline);
            }

            await base.OnDisconnectedAsync(exception);
        }

        private async Task NotifyUserOnlineUsersList(string receiverUsername)
        {
            var activeUsers = new ActiveUsersNotificationDTO()
            {
                Usernames = _connections.GetAllKeysExcept(receiverUsername)
            };
            await Clients.Caller.SendAsync(ACTIVE_USERS_METHOD_NAME, activeUsers);
        }


        private async Task NotifyOtherUsersNewUserStatus(string username, OnlineStatus status)
        {
            // Получение списка всех подключений пользователя
            var userConnections = _connections.GetConnections(username);

            // Рассылка уведомления на все подключения за исключением подключений текущего пользователя
            switch (status)
            {
                case OnlineStatus.Online:
                    await Clients.AllExcept(userConnections).SendAsync(USER_GETS_ONLINE_METHOD_NAME, username);
                    break;
                case OnlineStatus.Offline:
                    await Clients.AllExcept(userConnections).SendAsync(USER_GETS_OFFLINE_METHOD_NAME, username);
                    break;
            }
        }
    }
}
