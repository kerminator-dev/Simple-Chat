using ChatAPI.DTOs.Requests;
using ChatAPI.Entities;

namespace ChatAPI.Services.Interfaces
{
    public interface IMessagingService
    {
        Task Send(User sender, SendMessageRequestDTO message);
    }
}
