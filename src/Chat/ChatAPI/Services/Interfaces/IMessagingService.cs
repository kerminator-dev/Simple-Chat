using Chat.Core.DTOs.Requests;
using ChatAPI.Entities;

namespace ChatAPI.Services.Interfaces
{
    public interface IMessagingService
    {
        Task SendMessage(User sender, SendMessageRequestDTO message);
    }
}
