using Chat.Core.DTOs.Notifications;
using Chat.Core.DTOs.Requests;
using Chat.Core.Enums;
using Chat.WebAPI.Services.Interfaces;
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
        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly CachedUserConnectionMapper<string> _connections;
        private readonly IPubSubService<string> _pubSubService;

        public MessagingService(IUserService userService, IHubContext<ChatHub> hubContext, CachedUserConnectionMapper<string> connections, IPubSubService<string> pubSubService)
        {
            _userService = userService;
            _hubContext = hubContext;
            _connections = connections;
            _pubSubService = pubSubService;
        }

        public async Task SendMessage(string senderUsername, SendTextMessageRequestDTO messageRequest)
        {
            if (senderUsername == messageRequest.Receiver)
                throw new ArgumentException("You can't send message to yourself!");

            var receiver = await _userService.GetUserByUsername(messageRequest.Receiver);
            if (receiver == null)
                throw new EntityNotFoundException("Receiver not found!");

            // Если отправитель не подписан на уведомления получателя
            if (!_pubSubService.IsSubscribed(senderUsername, receiver.Username))
            {
                // Подписка на уведомления
                _pubSubService.Subscribe(senderUsername, receiver.Username);
            }

            // Если получатель не подписан на уведомления отправителя
            if (!_pubSubService.IsSubscribed(receiver.Username, senderUsername))
            {
                // Подписка на уведомления
                _pubSubService.Subscribe(receiver.Username, senderUsername);
            }

            // Сообщение-уведомление
            var message = new TextMessageNotificationDTO()
            {
                Id = messageRequest.Id,
                Sender = senderUsername,
                Receiver = receiver.Username,
                //StaticKey = messageRequest.StaticKey,
                Message = messageRequest.Message,
            };

            // Получение списка подключений получателя
            var receiverConnections = _connections.GetConnections(receiver.Username);
            if (receiverConnections == null || receiverConnections.Count() == 0)
                throw new MessageNotSentException("Receiver is not online!");

            try
            {
                // Отправка сообщения получателю
                await this.Send<TextMessageNotificationDTO>(receiverConnections, ON_NEW_MESSAGE_METHOD_NAME, message);
            }
            catch (Exception)
            {
                throw new MessageNotSentException("An error occurred while sending a message to receriver!");
            }

            // Получение списка подключений отправителя
            var senderConnections = _connections.GetConnections(senderUsername);

            // Отправка сообщения отправителю
            await this.Send<TextMessageNotificationDTO>(senderConnections, ON_NEW_MESSAGE_METHOD_NAME, message);
        }

        protected async Task Send<TMessage>(IEnumerable<string> connections, string methodName, TMessage objectToSend)
        {
            await _hubContext.Clients.Clients(connections).SendAsync(methodName, objectToSend);
        }
    }
}
