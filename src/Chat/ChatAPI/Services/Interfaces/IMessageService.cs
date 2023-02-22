using ChatAPI.DTOs.Requests;
using ChatAPI.Entities;

namespace ChatAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task Send(User sender, SendMessageRequestDTO message);
    }
}
