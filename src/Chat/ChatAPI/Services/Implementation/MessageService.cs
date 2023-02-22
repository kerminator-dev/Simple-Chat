using ChatAPI.DTOs.Requests;
using ChatAPI.Entities;
using ChatAPI.Services.Interfaces;

namespace ChatAPI.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IUserRepository _userRepository;

        public MessageService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Send(User sender, SendMessageRequestDTO message)
        {
            var receiver = await _userRepository.GetByUsername(message.Receiver);

        }
    }
}
