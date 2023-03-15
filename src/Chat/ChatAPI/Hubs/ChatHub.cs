using Chat.Core.DTOs.Notifications;
using Chat.Core.Enums;
using ChatAPI.Mappings;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessagingService messagingService;

        private readonly ConnectionMapper<string> _connections;

        public ChatHub(ConnectionMapper<string> connections, IMessagingService messagingService)
        {
            _connections = connections;
            this.messagingService = messagingService;
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
            await messagingService.NotifyOtherUsersNewUserStatus(Clients, username, OnlineStatus.Online);

            // Уведомление пользователя {username} о списке активных пользователей
            await messagingService.NotifyCallerOnlineUsersList(Clients.Caller, username);

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
                await messagingService.NotifyOtherUsersNewUserStatus(Clients, username, OnlineStatus.Offline);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
