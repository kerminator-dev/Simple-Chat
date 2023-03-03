using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Metrics;
using System.Net;

namespace SignalRConsoleApp
{
    internal class Program
    {
        private static string _accessToken;
        private static string _refreshToken;

        private static async Task Main(string[] args)
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
                    options.AccessTokenProvider = () => Task.FromResult(GenerateToken()) ;
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
                var json = webClient.UploadString(address, JsonConvert.SerializeObject(new SendRefreshTokenDTO() { RefreshToken = _refreshToken }));

                var response = JsonConvert.DeserializeObject<AuthenticatedUserResponseDTO>(json);
                _accessToken = response.AccessToken;
                _refreshToken= response.RefreshToken;
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

    public class SendRefreshTokenDTO
    {
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; } 
    }

    public class AuthenticatedUserResponseDTO
    {
        public UserDTO User { get; set; }

        /// <summary>
        /// Токен доступа
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Срок действия токена доступа в минутах
        /// </summary>
        public double AccessTokenExpirationMinutes { get; set; }

        /// <summary>
        /// Токен для обновления AccessToken 
        /// 
        /// (если AccessToken потерялся или срок его действия закончился)
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Срок действия токена для обновления
        /// </summary>
        public double RefreshTokenExpirationMinutes { get; set; }

        public AuthenticatedUserResponseDTO(UserDTO user, string accessToken, double accessTokenExpirationMinutes, string refreshToken, double refreshTokenExpirationMinutes)
        {
            User = user;
            AccessToken = accessToken;
            AccessTokenExpirationMinutes = accessTokenExpirationMinutes;
            RefreshToken = refreshToken;
            RefreshTokenExpirationMinutes = refreshTokenExpirationMinutes;
        }
    }

    public class UserDTO
    {
        public string Username { get; set; }
    }

    public class ActiveUsersNotificationDTO
    {
        [JsonProperty("activeUsers")]
        public IEnumerable<string> Usernames { get; set; }
    }

    public class MessageNotificationDTO
    {

        // Username отправителя
        public string Sender { get; set; }

        // Username получателя
        public string Receiver { get; set; }

        // Статический ключ - у всех клиентов одинаковый
        public string StaticKey { get; set; }

        // Сообщение
        public string Message { get; set; }
    }
}
