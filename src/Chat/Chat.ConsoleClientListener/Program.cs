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

            _apiManager = new APIManager(hostname);

            _apiManager.OnConnectionClosed += OnConnectionClosed;
            _apiManager.OnReconnecting += OnReconnecting;
            _apiManager.OnReconnected += OnReconnected;
            _apiManager.OnUserGetsOnline += OnUserGetsOnline;
            _apiManager.OnUserGetsOffline += OnUserGetsOffline;
            _apiManager.OnActiveUsersNotification += OnActiveUsersNotification;
            _apiManager.OnMessageReceived += OnMessageReceived;

            var loginRequest = new LoginRequestDTO()
            {
                Username = Console.ReadLine(),
                Password = Console.ReadLine()
            };

            try
            {
                var user = await _apiManager.TryLogin(loginRequest);
                Console.WriteLine("Вошёл как {0}", user.Username);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        private static Task OnConnectionClosed(Exception? ex)
        {
            if (ex != null)
            {
                Console.WriteLine("Подключение закрыто, ошибка: {0}", ex);
            }
            else Console.WriteLine("Подключение закрыто, неизвестная ошибка");

            return Task.CompletedTask;
        }

        private static Task OnReconnecting(Exception? ex)
        {
            if (ex != null)
            {
                Console.WriteLine("Попытка переподклюения, ошибка: {0}", ex);
            }
            else Console.WriteLine("Попытка переподклюения, неизвестная ошибка");

            return Task.CompletedTask;
        }

        private static Task OnReconnected(string? connection)
        {
            if (connection != null)
                Console.WriteLine("Переподключен. {0}", connection);
            else 
                Console.WriteLine("Переподключен.");

            return Task.CompletedTask;
        }

        private static void OnUserGetsOnline(string username)
        {
            Console.WriteLine("{0} подключился", username);
        }

        private static void OnUserGetsOffline(string username)
        {
            Console.WriteLine("{0} отключился", username);
        }

        private static void OnMessageReceived(MessageNotificationDTO message)
        {
            Console.WriteLine("Получено сообщение от {0}: '{1}'", message.Sender, message.Message);
        }

        private static void OnActiveUsersNotification(ActiveUsersNotificationDTO notification)
        {
            if (notification.Usernames == null || notification.Usernames.Count() == 0)
                Console.WriteLine("Пользователей в сети: 0");
            else
                Console.WriteLine("Пользователи в сети: {0}", string.Join(", ", notification.Usernames));
        }
    }
}