using ChatAPI.Data;
using ChatAPI.Entities;
using ChatAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.WebAPI.Services.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(User user)
        {
            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> Get(string username)
        {
            return await _dbContext
                    .Users
                    .FirstOrDefaultAsync(u => u.Username == username);
        }

        public Task<User> FindById(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public async Task Update(User user)
        {
            var result = _dbContext.Update(user).Entity;

            await _dbContext.SaveChangesAsync();
        }
    }
}
