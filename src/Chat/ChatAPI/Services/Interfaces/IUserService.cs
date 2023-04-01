using Chat.Core.DTOs.Requests;
using ChatAPI.Entities;

namespace ChatAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByUsername(string username);
        Task RegisterUser(RegisterRequestDTO registerRequest);
        Task DeleteUser(User user);
        Task UpdateUser(User user);
        Task<bool> IsUserExists(string username);
        Task<IList<string>> GetExistingUsernames(IList<string> usernames);
    }
}
