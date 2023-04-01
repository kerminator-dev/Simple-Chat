using Chat.Core.DTOs.Requests;
using Chat.Core.Enums;
using ChatAPI.Entities;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace ChatAPI.Services.Interfaces
{
    public interface IMessagingService
    {
        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>
        Task SendMessage(string senderUsername, SendTextMessageRequestDTO message);
    }
}
