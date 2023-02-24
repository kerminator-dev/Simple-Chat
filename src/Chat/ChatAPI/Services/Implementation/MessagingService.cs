using AutoMapper;
using ChatAPI.DTOs.Notifications;
using ChatAPI.DTOs.Requests;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Hubs;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Services.Implementation
{
    public class MessagingService : IMessagingService
    {
        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessagingService(IUserService userService, IHubContext<ChatHub> hubContext)
        {
            _userService = userService;
            _hubContext = hubContext;
        }

        public async Task Send(User sender, SendMessageRequestDTO messageRequest)
        {
            if (sender.Username == messageRequest.Receiver)
                throw new ArgumentException();

            var receiver = await _userService.GetUserByUsername(messageRequest.Receiver);
            if (receiver == null)
                throw new EntityNotFoundException("Receiver not found!");

            MessageNotificationDTO message = new MessageNotificationDTO()
            {
                Sender = sender.Username,
                Receiver = receiver.Username,
                StaticKey = messageRequest.StaticKey,
                Message = messageRequest.Message,
            };

            // Отправка сообщения получателю
            await Send<MessageNotificationDTO>(receiver, "OnNewMessage", message);

            // Отправка сообщения отправителю
            await Send<MessageNotificationDTO>(sender, "OnNewMessage", message);
        }

        protected async Task Send<TMessage>(User receiver, string methodName, TMessage objectToSend)
        {
            await _hubContext.Clients.User(receiver.Username).SendAsync(methodName, objectToSend);
        }
    }
}
