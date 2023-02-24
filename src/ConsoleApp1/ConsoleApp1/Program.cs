using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SignalRConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.Write("Токен: ");
            var token = Console.ReadLine();

            ServiceProvider service = new ServiceCollection()
                .AddLogging((loggingBuilder) => loggingBuilder
                .SetMinimumLevel(LogLevel.Debug)
                )
                .BuildServiceProvider();



            var hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7133/chathub", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(token);




                })
                
                .WithAutomaticReconnect()
                .Build();

            // Register the handler here!! 
            hubConnection.On<string>("UserGetsOffline", (username) => {

                    Console.WriteLine($"{username} вышел из сети");
            });

            hubConnection.On<string>("UserGetsOnline", (username) => {

                Console.WriteLine($"{username} появился в сети");
            });

            hubConnection.On<MessageNotificationDTO>("OnNewMessage", (message) => {

                Console.WriteLine($"Сообщение от {message.Sender}: {message.Message}");
            });
            
            hubConnection.StartAsync().Wait();
            Console.ReadKey();
        }
    }

    public class MessageNotificationDTO
    {

        // Username отправителя
        public string Sender { get; set; }

        // Username получателя
        public string Receiver { get; set; }

        // Статический ключ - у всех клиентов одинаковый,
        // Шифруется перед отправкой
        // Нужен для определения - возможно ли расшифровать сообщение
        public string StaticKey { get; set; }

        // Сообщение
        public string Message { get; set; }
    }
}
