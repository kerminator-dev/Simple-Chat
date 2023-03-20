using Chat.Core.DTOs.Requests;
using Chat.WebAPIClientLibrary.Builders;
using Chat.WebAPIClientLibrary.Exceptions.Internal;
using Chat.WebAPIClientLibrary.Extensions;
using Chat.WebAPIClientLibrary.Handlers;
using Chat.WebAPIClientLibrary.Services.Interfaces;
using System.Net;

namespace Chat.WebAPIClientLibrary.Services.Implementation
{
    internal class MessagingManager : IMessagingManager
    {
        // Роуты к API-методам
        protected readonly string _sendMessageRoute;

        public MessagingManager(string address)
        {
            _sendMessageRoute = $"{address}api/Messages/Send";
        }

        public async Task<bool> TrySendMessageAsync(SendMessageRequestDTO sendMessageRequestDTO, string accessToken)
        {
            try
            {
                // Построение запроса
                var builder = new WebRequestBuilder(_sendMessageRoute)
                                 .WithHttpMethod(HttpMethod.Post)
                                 .WithJsonBody<SendMessageRequestDTO>(sendMessageRequestDTO)
                                 .WithJwtAccessToken(accessToken);

                var request = (HttpWebRequest)await builder.Build();

                // Выполнение запроса
                var response = (HttpWebResponse)await request.GetWebResponseAsync();


                // Обработка результата
                return await new HttpResponseHandler<bool>()
                        .HandleSuccess((response) =>
                        {
                            return Task.FromResult(true);
                        })
                        .Handle(HttpStatusCode.Unauthorized, async (response) =>
                        {
                            throw new UnauthorizedStatusCodeException(await response.GetBodyString());
                        })
                        .HandleClientError(async (response) =>
                        {
                            throw new ClientErrorStatusCodeException(await response.GetBodyString());
                        })
                        .HandleResponse(response);
            }
            catch (UnauthorizedStatusCodeException)
            {
                throw;
            }
            catch (ClientErrorStatusCodeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnexpectedStatusCodeException();
            }
        }
    }
}
