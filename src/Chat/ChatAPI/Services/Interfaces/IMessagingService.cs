using Chat.Core.DTOs.Requests;
using Chat.Core.Enums;
using ChatAPI.Entities;
using Microsoft.AspNetCore.SignalR;

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
        Task SendMessage(User sender, SendMessageRequestDTO message);

        /// <summary>
        /// Уведомить пользоватя о активных пользователях Хаба
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="receiverUsername"></param>
        /// <returns></returns>
        Task NotifyCallerOnlineUsersList(ISingleClientProxy caller, string receiverUsername);

        /// <summary>
        /// Уведомить остальных клиентов о подключении/отключении пользователя хаба
        /// </summary>
        /// <param name="clients">Клиенты хаба</param>
        /// <param name="username">Имя пользователя с изменившимся статусом</param>
        /// <param name="status">Статус подключения (Online/Offline)</param>
        /// <returns></returns>
        Task NotifyOtherUsersNewUserStatus(IHubCallerClients clients, string username, OnlineStatus status);
    }
}
