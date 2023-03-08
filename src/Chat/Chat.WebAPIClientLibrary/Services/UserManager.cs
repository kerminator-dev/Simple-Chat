using Chat.Core.DTOs.Requests;
using Chat.Core.DTOs.Responses;
using Chat.WebAPI.Extensions;
using Chat.WebAPIClientLibrary.Exceptions;
using Chat.WebAPIClientLibrary.Extensions;
using Chat.WebAPIClientLibrary.Implementation;
using ChatAPI.Exceptions;
using System.Net;

namespace Chat.WebAPIClientLibrary.Services
{
    internal class UserManager : IUserManager
    {
        private readonly string _loginRoute;
        private readonly string _registerRoute;
        private readonly string _refreshTokenRoute;
        private readonly string _deleteUserRoute;

        public UserManager(Uri host)
        {
            // Роуты к API-методам
            _loginRoute = $"{host}api/User/Login";
            _registerRoute = $"{host}apiUser/Register";
            _refreshTokenRoute = $"{host}api/User/RefreshToken";
            _deleteUserRoute = $"{host}api/User/Delete";
        }


        public async Task<AuthenticatedUserResponseDTO> TryLogin(LoginRequestDTO loginRequest)
        {
            try
            {
                // Построение запроса
                var request = await new WebRequestBuilder(_loginRoute)
                                 .WithJsonBody<LoginRequestDTO>(loginRequest)
                                 .WithHttpMethod(HttpMethod.Post)
                                 .Build();

                // Выполнение запроса
                var response = (HttpWebResponse)await request.GetResponseAsync();

                // Обработка результата
                var authInfo = await new HttpResponseHandler<AuthenticatedUserResponseDTO>()
                                   .HandleSuccess(async (response) =>
                                   {
                                       return await response.TryDeserialize<AuthenticatedUserResponseDTO>();
                                   })
                                   .Handle(HttpStatusCode.BadRequest, async (response) =>
                                   {
                                       using (var reader = new StreamReader(response.GetResponseStream()))
                                       {
                                           var message = await reader.ReadToEndAsync();
                                           throw new SignInException(message);
                                       }

                                   })
                                   .HandleOthers((response) =>
                                   {
                                       throw new UnexpectedHttpResponseException("Не удалось войти в аккаунт!");
                                   })
                                   .HandleResponse(response);

                return authInfo;
            }
            catch (SignInException ex)
            {
                throw ex;
            }
        }

        public async Task<AuthenticatedUserResponseDTO> TryRefreshToken(string refreshToken)
        {
            var refreshTokenRequest = new RefreshTokenRequestDTO()
            {
                RefreshToken = refreshToken
            };

            try
            {
                // Построение запроса
                var request = await new WebRequestBuilder(_refreshTokenRoute)
                                 .WithJsonBody<RefreshTokenRequestDTO>(refreshTokenRequest)
                                 .WithHttpMethod(HttpMethod.Post)
                                 .Build();

                // Выполнение запроса
                var response = (HttpWebResponse)await request.GetWebResponseAsync();

                // Обработка результата
                var authInfo = await new HttpResponseHandler<AuthenticatedUserResponseDTO>()
                                  .HandleSuccess(async (response) =>
                                  {
                                      return await response.TryDeserialize<AuthenticatedUserResponseDTO>();
                                  })
                                  .HandleOthers((response) =>
                                  {
                                      throw new UnexpectedHttpResponseException("При получении нового токена произошла ошибка!");
                                  })
                                  .HandleResponse(response);


                return authInfo;
            }
            catch (UnexpectedHttpResponseException ex)
            {
                throw ex;
            }
        }

        public async Task<bool> TryRegister(RegisterRequestDTO registerRequest)
        {
            try
            {
                // Построение запроса
                var request = await new WebRequestBuilder(_registerRoute)
                                 .WithJsonBody<RegisterRequestDTO>(registerRequest)
                                 .WithHttpMethod(HttpMethod.Post)
                                 .Build();

                // Выполнение запроса
                var response = (HttpWebResponse)await request.GetResponseAsync();

                // Обработка результата
                return await new HttpResponseHandler<bool>()
                                   .HandleSuccess((response) =>
                                   {
                                       return Task.FromResult(true);
                                   })
                                   .Handle(HttpStatusCode.BadRequest, (response) =>
                                   {
                                       return Task.FromResult(false);
                                   })
                                   .HandleOthers((response) =>
                                   {
                                       throw new UnexpectedHttpResponseException("Failed to register!");
                                   })
                                   .HandleResponse(response);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
