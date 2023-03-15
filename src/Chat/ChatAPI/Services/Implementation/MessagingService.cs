using Chat.Core.DTOs.Notifications;
using Chat.Core.DTOs.Requests;
using Chat.Core.Enums;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Hubs;
using ChatAPI.Mappings;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Services.Implementation
{
    public class MessagingService : IMessagingService
    {
        // Названия SignalR-Событий на стороне клиентов
        private const string ON_NEW_MESSAGE_METHOD_NAME = "NewMessage";
        private const string USER_GETS_ONLINE_METHOD_NAME = "UserGetsOnline";
        private const string USER_GETS_OFFLINE_METHOD_NAME = "UserGetsOffline";
        private const string ACTIVE_USERS_METHOD_NAME = "ActiveUsers";

        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ConnectionMapper<string> _connections;

        public MessagingService(IUserService userService, IHubContext<ChatHub> hubContext, ConnectionMapper<string> connections)
        {
            _userService = userService;
            _hubContext = hubContext;
            _connections = connections;
        }

        public async Task SendMessage(User sender, SendMessageRequestDTO messageRequest)
        {
            if (sender.Username == messageRequest.Receiver)
                throw new ArgumentException("You can't send message to yourself!");

            var receiver = await _userService.GetUserByUsername(messageRequest.Receiver);
            if (receiver == null)
                throw new EntityNotFoundException("Receiver not found!");

            // Сообщение-уведомление
            var message = new MessageNotificationDTO()
            {
                Id = messageRequest.Id,
                Sender = sender.Username,
                Receiver = receiver.Username,
                StaticKey = messageRequest.StaticKey,
                Message = messageRequest.Message,
            };

            // Получение списка подключений получателя
            var receiverConnections = _connections.GetConnections(receiver.Username);
            if (receiverConnections == null || receiverConnections.Count() == 0)
                throw new MessageNotSentException("Receiver is not online!");

            try
            {
                // Отправка сообщения получателю
                await Send<MessageNotificationDTO>(receiverConnections, ON_NEW_MESSAGE_METHOD_NAME, message);
            }
            catch (Exception)
            {
                throw new MessageNotSentException("An error occurred while sending a message to receriver!");
            }

            // Получение списка подключений отправителя
            var senderConnections = _connections.GetConnections(sender.Username);

            try
            {
                // Отправка сообщения отправителю
                await Send<MessageNotificationDTO>(senderConnections, ON_NEW_MESSAGE_METHOD_NAME, message);
            }
            catch (Exception)
            {
                throw new MessageNotSentException("An error occurred while sending a message to sender!");
            }
        }

        public async Task NotifyCallerOnlineUsersList(ISingleClientProxy caller, string receiverUsername)
        {
            var activeUsers = new ActiveUsersNotificationDTO()
            {
                Usernames = _connections.GetAllKeysExcept(receiverUsername)
            };
            await caller.SendAsync(ACTIVE_USERS_METHOD_NAME, activeUsers);
        }

        public async Task NotifyOtherUsersNewUserStatus(IHubCallerClients clients, string username, OnlineStatus status)
        {
            // Получение списка всех подключений пользователя
            var userConnections = _connections.GetConnections(username);

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

        protected async Task Send<TMessage>(IEnumerable<string> connections, string methodName, TMessage objectToSend)
        {
            await _hubContext.Clients.Clients(connections).SendAsync(methodName, objectToSend);
        }
    }
}
