using Chat.Core.DTOs.Notifications;
using Chat.Core.DTOs.Responses;
using Chat.Core.Enums;
using Chat.WebAPI.Entities;
using Chat.WebAPI.Exceptions;
using Chat.WebAPI.Services.Interfaces;
using ChatAPI.Exceptions;
using ChatAPI.Hubs;
using ChatAPI.Mappings;
using Microsoft.AspNetCore.SignalR;

namespace Chat.WebAPI.Services.Implementation
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IPubSubService<string> _userPubSubService;

        private readonly IHubContext<ChatHub> _hub;
        private readonly CachedUserConnectionMapper<string> _userConnections;

        private const string CONTACT_DELETED_METHOD_NAME = "OnContactDeleted";
        private const string CONTACT_ADDED_METHOD_NAME = "OnContactAdded";

        public ContactService(IContactRepository contactRepository, IPubSubService<string> userPubSubService, IHubContext<ChatHub> hub, CachedUserConnectionMapper<string> userConnections)
        {
            _contactRepository = contactRepository;
            _userPubSubService = userPubSubService;
            _hub = hub;
            _userConnections = userConnections;
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
            try
            {
                await _contactRepository.Create(contact);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                throw new ContactNotAddedException("Contact already exists!");
            }

            // Добавление в кэш
            _userPubSubService.Subscribe(username, contactUsername);

            // Уведомление пользователя
            await NotifyContactAdded(username, contactUsername);
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

            // Уведомление пользователя
            await NotifyContactDeleted(username, contactUsername);
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

        public async Task<UserContactsResponseDTO> GetAllUserContactsWithOnlineStatuses(string username)
        {
            // Поиск в БД
            var contacts = await _contactRepository.GetByUsername(username);
            if (contacts == null || !contacts.Any())
                throw new EntityNotFoundException("Contacts not found!");



            var contactsWithOnlineStatuses = contacts
                .Select
                (
                    contact => new UserResponseDTO
                    (
                        username: contact.ContactUsername, 
                        onlineStatus: _userConnections.Contains(contact.ContactUsername) ? OnlineStatus.Online : OnlineStatus.Offline
                    )
                )
                .ToList();

            return new UserContactsResponseDTO(contactsWithOnlineStatuses);
        }

        private async Task NotifyContactDeleted(string username, string contactUsername)
        {
            var connections = _userConnections.GetConnections(username);
            if (connections == null || !connections.Any())
                return;

            await _hub.Clients.Clients(connections).SendAsync(CONTACT_DELETED_METHOD_NAME, contactUsername);
        }

        private async Task NotifyContactAdded(string username, string contactUsername)
        {
            var connections = _userConnections.GetConnections(username);
            if (connections == null || !connections.Any())
                return;

            var contactOnline = _userConnections.Contains(contactUsername);
            var notification = new NewContactNotificationDTO()
            {
                Username = contactUsername,
                IsOnline = contactOnline
            };

            await _hub.Clients.Clients(connections).SendAsync(CONTACT_ADDED_METHOD_NAME, notification);
        }
    }
}
