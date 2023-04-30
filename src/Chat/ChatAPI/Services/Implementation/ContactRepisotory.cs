using Chat.WebAPI.Entities;
using Chat.WebAPI.Services.Interfaces;
using ChatAPI.Data;
using ChatAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.WebAPI.Services.Implementation
{
    public class ContactRepisotory : IContactRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ContactRepisotory(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(UserContact contact)
        {
            await _dbContext.Contacts.AddAsync(contact);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(UserContact contact)
        {
            _dbContext.Contacts.Remove(contact);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserContact> Get(string username)
        {
            return await _dbContext.Contacts
                                   .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<UserContact>> GetByContactUsername(string contactUsername)
        {
            return await _dbContext.Contacts
                                    .Where(c => c.ContactUsername == contactUsername)
                                    .ToListAsync();
        }

        public async Task<IEnumerable<UserContact>> GetByUsername(string username)
        {
            return await _dbContext.Contacts
                                     .Where(c => c.Username == username)
                                     .ToListAsync();
        }

        public async Task Update(UserContact contact)
        {
            _ = _dbContext.Update(contact).Entity;

            await _dbContext.SaveChangesAsync();
        }
    }
}
