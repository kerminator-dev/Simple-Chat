using Chat.WebAPI.Entities;
using Chat.WebAPI.Services.Interfaces;
using ChatAPI.Exceptions;

namespace Chat.WebAPI.Services.Implementation
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IPubSubService<string> _userPubSubService;

        public ContactService(IContactRepository contactRepository, IPubSubService<string> userPubSubService)
        {
            _contactRepository = contactRepository;
            _userPubSubService = userPubSubService;
        }

        public async Task AddContact(string username, string contactUsername)
        {
            if (username == contactUsername)
                throw new ArgumentException("You cannot add yourself to contacts!");

            var contact = new UserContact()
            {
                Username = username,
                ContactUsername = contactUsername
            };

            // Добавление в БД
            await _contactRepository.Create(contact);

            // Добавление в кэш
            _userPubSubService.Subscribe(username, contactUsername);
        }

        public async Task AddContacts(string username, IEnumerable<string> contacts)
        {
            var addContactTasks = contacts
                .Where(contact => contact != username)
                .Select(contact => AddContact(username, contact))
                .ToArray();

            await Task.WhenAll(addContactTasks);
        }

        public async Task DeleteContact(string username, string contactUsername)
        {
            var userContact = await _contactRepository.Get(username);
            if (userContact == null)
                return;

            // Удаление из БД
            await _contactRepository.Delete(userContact);

            // Удаление из кэша
            _userPubSubService.Unsubscribe(username, contactUsername);
        }

        public async Task DeleteContacts(string username, IEnumerable<string> contacts)
        {
            var deleteContactTasks = contacts
                .Where(contact => contact != username)
                .Select(contact => DeleteContact(username, contact))
                .ToArray();

            await Task.WhenAll(deleteContactTasks);
        }

        // Получить список контактов пользователя
        public async Task<IEnumerable<string>> GetAllUserContacts(string username)
        {
            // Поиск в кэше
            //var contacts = _contactPubSubService.GetSubscribes(username);
            //if (contacts != null)

            // Поиск в БД
            var contacts = await _contactRepository.GetByUsername(username);
            if (contacts == null || !contacts.Any())
                throw new EntityNotFoundException("Contacts not found!");

            return contacts.Select(c => c.ContactUsername);
        }
    }
}
