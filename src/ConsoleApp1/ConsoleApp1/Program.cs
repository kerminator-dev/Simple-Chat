using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SignalRConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.Write("Хост: ");
            string url = Console.ReadLine();
            Console.Write("Примечание (ник или что-то такое, шоб не путаться): ");
            Console.ReadLine();
            Console.Write("Токен: ");
            var token = Console.ReadLine();

            ServiceProvider service = new ServiceCollection()
                .AddLogging((loggingBuilder) => loggingBuilder
                .SetMinimumLevel(LogLevel.Debug)
                )
                
                .BuildServiceProvider();



            var hubConnection = new HubConnectionBuilder()
                .WithUrl(url, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(token);
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                })
                .WithAutomaticReconnect()
                .Build();

            // Register the handler here!! 
            hubConnection.On<string>("UserGetsOffline", (username) => {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{username} вышел из сети");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
            });

            hubConnection.On<string>("UserGetsOnline", (username) => {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{DateTime.Now}: {username} появился в сети");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
            });

            hubConnection.On<MessageNotificationDTO>("NewMessage", (message) => {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{DateTime.Now}: Сообщение от {message.Sender}: {message.Message}");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
            });


            hubConnection.On<ActiveUsersNotificationDTO>("ActiveUsers", (message) => {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Всего активных пользователей: {message.Usernames.Count()}");
                foreach (var username in message.Usernames)
                {
                    Console.WriteLine($"- Пользователь {username} в сети!");
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
            });

            try
            {
                hubConnection.StartAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.ReadKey();
        }
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
