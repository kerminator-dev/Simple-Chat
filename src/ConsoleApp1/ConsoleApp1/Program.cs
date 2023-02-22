using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SignalRConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {



            ServiceProvider service = new ServiceCollection()
                .AddLogging((loggingBuilder) => loggingBuilder
                .SetMinimumLevel(LogLevel.Debug)
                )
                .BuildServiceProvider();

            var hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7133/chathub", options =>
                {
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                    options.AccessTokenProvider = () => Task.FromResult("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJhZG1pbjEyMyIsIm5iZiI6MTY3NjkxNDAwNSwiZXhwIjoxNjc2OTIxMjA1LCJpYXQiOjE2NzY5MTQwMDUsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwOTEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MDkxIn0.mPvSNHqsw4cmXun2h1pWivxc4aJrQoJk40cdndL3EoU");
                })
                
                .WithAutomaticReconnect()
                .Build();

            // Register the handler here!! 
            hubConnection.On<bool>("OnPremAgentStatusReceived", (isReachable) => {
                if (isReachable)
                    Console.WriteLine("Agent is reachable");
                else
                    Console.WriteLine("Agent is not reachable");
            });

            hubConnection.StartAsync().Wait();

            Console.ReadKey();
        }
    }
}
