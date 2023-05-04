using Chat.Core.DTOs.Notifications;
using Chat.Core.DTOs.Requests;
using Chat.Core.DTOs.Responses;
using Chat.WebAPIClientLibrary;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Net;

namespace Chat.ConsoleClientListener
{
    internal class Program
    {
        private static APIManager _apiManager;
        private static async Task Main()
        {
            Console.Write("Хост: ");
            string hostname = Console.ReadLine();

            Console.Write("Имя пользователя: ");
            string username = Console.ReadLine();

            Console.Write("Пароль: ");
            string password = Console.ReadLine();


            _apiManager = new APIManager(hostname);

            _apiManager.OnConnectionClosed += OnConnectionClosed;
            _apiManager.OnReconnecting += OnReconnecting;
            _apiManager.OnReconnected += OnReconnected;
            _apiManager.OnUserGetsOnline += OnUserGetsOnline;
            _apiManager.OnUserGetsOffline += OnUserGetsOffline;
            _apiManager.OnActiveUsersNotification += OnActiveUsersNotification;
            _apiManager.OnMessageReceived += OnMessageReceived;
            _apiManager.OnContactDeleted += OnContactDeleted;
            _apiManager.OnContactAdded += OnContactAdded;

            var loginRequest = new LoginRequestDTO()
            {
                Username = username,
                Password = password
            };

            try
            {
                var user = await _apiManager.TryLogin(loginRequest);
                LogInformation($"Вошёл как {user.Username}");

               //Console.Write("Отправить сообщение: ");
               //var usernameReceiver = Console.ReadLine();
               //var sendMessageDTO = new SendTextMessageRequestDTO()
               //{
               //    Id = "1",
               //    Message = "Test",
               //    Receiver = usernameReceiver
               //};
               //await _apiManager.TrySendMessageAsync(sendMessageDTO);
            }
            catch (Exception ex)
            {
                LogInformation(ex.Message);
            }

            Console.ReadLine();
        }

        private static void LogInformation(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: {message}");
        }

        private static void OnContactDeleted(string contactUsername)
        {
            LogInformation($"{contactUsername} удалён из списка контактов");
        }

        private static void OnContactAdded(NewContactNotificationDTO newContact)
        {
            LogInformation($"{newContact.Username} добавлен в список контактов.");
        }

        private static Task OnConnectionClosed(Exception? ex)
        {
            if (ex != null)
            {
                LogInformation($"Подключение закрыто, ошибка: {ex}");
            }
            else LogInformation("Подключение закрыто, неизвестная ошибка");

            return Task.CompletedTask;
        }

        private static Task OnReconnecting(Exception? ex)
        {
            if (ex != null)
            {
                LogInformation($"Попытка переподклюения, ошибка: {ex}");
            }
            else LogInformation("Попытка переподклюения, неизвестная ошибка");

            return Task.CompletedTask;
        }

        private static Task OnReconnected(string? connection)
        {
            if (connection != null)
                LogInformation($"Переподключен. {connection}");
            else 
                LogInformation("Переподключен.");

            return Task.CompletedTask;
        }

        private static void OnUserGetsOnline(string username)
        {
            LogInformation($"{username} подключился");
        }

        private static void OnUserGetsOffline(string username)
        {
            LogInformation($"{username} отключился");
        }

        private static void OnMessageReceived(TextMessageNotificationDTO message)
        {
            LogInformation($"Получено сообщение от {message.Sender}: '{message.Message}'");
        }

        private static void OnActiveUsersNotification(ActiveUsersNotificationDTO notification)
        {
            if (notification.Usernames == null || notification.Usernames.Count() == 0)
                LogInformation("Пользователей в сети: 0");
            else
                LogInformation($"Пользователи в сети: {string.Join(", ", notification.Usernames)}" );
        }
    }
}