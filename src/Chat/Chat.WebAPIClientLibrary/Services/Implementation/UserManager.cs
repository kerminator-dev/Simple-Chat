using Chat.WebAPIClientLibrary.Builders;
using Chat.WebAPIClientLibrary.Exceptions.Internal;
using Chat.WebAPIClientLibrary.Extensions;
using Chat.WebAPIClientLibrary.Handlers;
using Chat.WebAPIClientLibrary.Services.Interfaces;
using System.Net;

namespace Chat.WebAPIClientLibrary.Services.Implementation
{
    internal class UserManager : IUserManager
    {
        protected readonly string _deleteUserRoute;

        public UserManager(string address)
        {
            // Роуты к API-методам
            _deleteUserRoute = $"{address}api/User/Delete";
        }

        public async Task<bool> TryDeleteUser(string accessToken)
        {
            try
            {
                // Построение запроса
                var builder = new WebRequestBuilder(_deleteUserRoute)
                                 .WithHttpMethod(HttpMethod.Delete)
                                 .WithJwtAccessToken(accessToken);

                var request = (HttpWebRequest)await builder.Build();

                // Выполнение запроса
                var response = (HttpWebResponse)await request.GetWebResponseAsync();


                // Обработка результата
                return await new HttpResponseHandler<bool>()
                        .Handle(HttpStatusCode.OK, (response) =>
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
