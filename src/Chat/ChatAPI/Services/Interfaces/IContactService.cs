using Chat.Core.DTOs.Responses;

namespace Chat.WebAPI.Services.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<string>> GetAllUserContacts(string username);

        Task<UserContactsResponseDTO> GetAllUserContactsWithOnlineStatuses(string username);

        Task AddContact(string username, string contactUsername);

        Task AddContacts(string username, IEnumerable<string> contacts);

        Task DeleteContact(string username, string contactUsername);
        Task DeleteContacts(string username, IEnumerable<string> contacts);
    }
}
