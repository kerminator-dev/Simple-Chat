using System.Net;
using System;
using Chat.Core.DTOs.Notifications;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Connections;
using Newtonsoft.Json;
using Chat.Core.DTOs.Responses;
using Chat.Core.DTOs.Requests;
using Chat.WebAPIClientLibrary;

namespace Chat.ConsoleClientListener
{
    internal class Program
    {
        private static string _accessToken;
        private static string _refreshToken;

        private static async Task Main()
        {
            Console.Write("Хост: ");
            string hostname = Console.ReadLine();
            var api = new APIManager(new Uri(hostname));

            var loginRequest = new LoginRequestDTO()
            {
                Username = Console.ReadLine(),
                Password = Console.ReadLine()
            };

            if (await api.TryLogin(loginRequest))
            {
                Console.WriteLine("ВОШЁ");
            }
            else
            {
                Console.WriteLine("Nope!");
            }

            await Task.Delay(TimeSpan.FromSeconds(70));

            var message = new SendMessageRequestDTO()
            {
                Receiver = "pussy",
                Id = "FDSGA",
                Message = "SAGAS",
                StaticKey = "gsga"
            };

            if (await api.TrySendMessage(message))
            {
                Console.WriteLine("тОКЕН новый получен");
            }
            else
            {
                Console.WriteLine("Токен новый НЕ получен");
            }
        }

        private static async Task Main2(string[] args)
        {
            Console.Write("Хост: ");
            string url = Console.ReadLine();
            Console.Write("Примечание (ник или что-то такое, шоб не путаться): ");
            Console.ReadLine();
            Console.Write("Access Токен: ");
            _accessToken = Console.ReadLine();
            Console.Write("Refresh Токен: ");
            _refreshToken = Console.ReadLine();


            ServiceProvider service = new ServiceCollection()
                .AddLogging((loggingBuilder) => loggingBuilder
                .SetMinimumLevel(LogLevel.Debug)
                )
                .BuildServiceProvider();



            var hubConnection = new HubConnectionBuilder()
                .WithUrl(url, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(GenerateToken());
                    options.Transports = HttpTransportType.ServerSentEvents;
                })
                .WithAutomaticReconnect()
                .Build();
            hubConnection.Closed += HubConnection_Closed;
            hubConnection.Reconnected += HubConnection_Reconnected;
            hubConnection.Reconnecting += HubConnection_Reconnecting;

            // Register the handler here!! 
            hubConnection.On<string>("UserGetsOffline", (username) => {
                Log($"{username} вышел из сети", ConsoleColor.Magenta);
                Console.ForegroundColor = ConsoleColor.Gray;
            });

            hubConnection.On<string>("UserGetsOnline", (username) => {
                Log($"{username} появился в сети");
            });

            hubConnection.On<MessageNotificationDTO>("NewMessage", (message) => {
                Log($"Сообщение от {message.Sender}: {message.Message}");

            });


            hubConnection.On<ActiveUsersNotificationDTO>("ActiveUsers", (message) => {
                Log($"Всего активных пользователей: {message.Usernames.Count()}");
                foreach (var username in message.Usernames)
                {
                    Log($" - Пользователь {username} в сети!");
                }
            });

            try
            {
                hubConnection.StartAsync().Wait();

                Log("Соединение установлено");
            }
            catch (Exception ex)
            {
                Log(ex.ToString(), ConsoleColor.Red);
            }
            Console.ReadKey();
        }

        private static string GenerateToken()
        {
            using (var webClient = new WebClient())
            {
                Uri address = new Uri($"https://localhost:7133/api/User/RefreshToken");
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var json = webClient.UploadString(address, JsonConvert.SerializeObject(new RefreshTokenRequestDTO() { RefreshToken = _refreshToken }));

                var response = JsonConvert.DeserializeObject<AuthenticatedUserResponseDTO>(json);
                _accessToken = response.AccessToken;
                _refreshToken = response.RefreshToken;
                return response.AccessToken;
            }
        }

        private static Task HubConnection_Reconnecting(Exception? arg)
        {
            Log("Происходит переподключение к хабу", ConsoleColor.White);
            return Task.CompletedTask;
        }

        private static Task HubConnection_Reconnected(string? arg)
        {
            Log("Произошло переподключение к хабу", ConsoleColor.White);
            return Task.CompletedTask;
        }

        private static Task HubConnection_Closed(Exception? arg)
        {
            Log($"Подключение закрыто. Исключение: {arg?.Message}", ConsoleColor.Red);

            return Task.CompletedTask;
        }

        private static void Log(string message, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{DateTime.Now.ToLongTimeString()}: ");
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}