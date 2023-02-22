using ChatAPI.Entities;

namespace ChatAPI.Services.Interfaces
{
    public interface IUserRepository : IRepository<User, string>
    {
        public Task<User> GetByUsername(string username);
    }
}
