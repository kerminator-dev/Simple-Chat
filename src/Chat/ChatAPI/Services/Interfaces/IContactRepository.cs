using Chat.WebAPI.Entities;
using ChatAPI.Services.Interfaces;

namespace Chat.WebAPI.Services.Interfaces
{
    public interface IContactRepository : IRepository<UserContact, string>
    {
        Task<IEnumerable<UserContact>> GetByUsername(string username);
        Task<IEnumerable<UserContact>> GetByContactUsername(string contactUsername);
    }
}
