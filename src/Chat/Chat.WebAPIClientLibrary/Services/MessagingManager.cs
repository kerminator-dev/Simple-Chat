using Chat.Core.DTOs.Requests;
using Chat.WebAPI.Extensions;
using Chat.WebAPIClientLibrary.Exceptions;
using Chat.WebAPIClientLibrary.Implementation;
using System.Net;

namespace Chat.WebAPIClientLibrary.Services
{
    public class MessagingManager : IMessagingManager
    {
        protected readonly string _sendMessageRoute;
        public MessagingManager(Uri host)
        {
            // Роуты к API-методам
            _sendMessageRoute = $"{host}api/Messages/Send";
        }

        public async Task<bool> TrySendMessage(SendMessageRequestDTO sendMessageRequestDTO, string accessToken)
        {
            try
            {
                // Построение запроса
                var builder = new WebRequestBuilder(_sendMessageRoute)
                                 .WithHttpMethod(HttpMethod.Post)
                                 .WithJsonBody<SendMessageRequestDTO>(sendMessageRequestDTO)
                                 .WithJwtAccessToken(accessToken);

                var request = (HttpWebRequest)await builder.Build();

                HttpWebResponse response = default(HttpWebResponse);


                // Выполнение запроса
                response = (HttpWebResponse)await request.GetWebResponseAsync();


                // Обработка результата
                return await new HttpResponseHandler<bool>()
                        .Handle(HttpStatusCode.OK, (response) =>
                        {
                            return Task.FromResult(true);
                        })
                        .Handle(HttpStatusCode.Unauthorized, (response) =>
                        {
                            throw new UnauthorizedException();
                        })
                        .HandleResponse(response);
            }
            catch (UnauthorizedException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
