using Chat.Core.DTOs;
using Chat.Core.DTOs.Requests;
using Chat.Core.DTOs.Responses;
using Chat.WebAPIClientLibrary.Builders;
using Chat.WebAPIClientLibrary.Exceptions.Client;
using Chat.WebAPIClientLibrary.Exceptions.Internal;
using Chat.WebAPIClientLibrary.Extensions;
using Chat.WebAPIClientLibrary.Handlers;
using Chat.WebAPIClientLibrary.Services.Interfaces;
using System.Net;

namespace Chat.WebAPIClientLibrary.Services.Implementation
{
    internal class AuthManager : IAuthManager
    {
        // Роуты к API-методам
        protected readonly string _loginRoute;
        protected readonly string _registerRoute;
        protected readonly string _refreshTokenRoute;

        // Токены
        protected string _accessToken;
        protected string _refreshToken;

        protected DateTime _accessTokenExpiratedAt;
        public string AccessToken => _accessToken;
        //{
        //    get
        //    {
        //        if (DateTime.UtcNow > _accessTokenExpiratedAt)
        //        {
        //            try
        //            {
        //                _ = this.TryRefreshToken();
        //            }
        //            catch(Exception) { }
        //        }

        //        return _accessToken;
        //    }
        //}




        public AuthManager(string address)
        {
            _loginRoute = $"{address}api/Auth/Login";
            _registerRoute = $"{address}api/Auth/Register";
            _refreshTokenRoute = $"{address}api/Auth/RefreshToken";

            _accessToken = string.Empty;
            _refreshToken = string.Empty;
        }



        public async Task<UserDTO> TryLogin(LoginRequestDTO loginRequest)
        {
            try
            {
                // Построение запроса
                var request = await new WebRequestBuilder(_loginRoute)
                                 .WithJsonBody<LoginRequestDTO>(loginRequest)
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
                                   .HandleClientError(async (response) =>
                                   {
                                       throw new ClientErrorStatusCodeException(await response.GetBodyString());
                                   })
                                   .HandleResponse(response);

                // Обновление токенов
                UpdateTokens(authInfo);

                return authInfo.User;
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

        public async Task<bool> TryRefreshToken()
        {
            try
            {
                var refreshTokenRequest = new RefreshTokenRequestDTO()
                {
                    RefreshToken = _refreshToken
                };

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
                                  .HandleClientError(async (response) =>
                                  {
                                      throw new ClientErrorStatusCodeException(await response.GetBodyString());
                                  })
                                  .HandleResponse(response);

                // Обновление токенов
                UpdateTokens(authInfo);

                return true;
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
                var response = (HttpWebResponse)await request.GetWebResponseAsync();

                // Обработка результата
                return await new HttpResponseHandler<bool>()
                                   .HandleSuccess((response) =>
                                   {
                                       return Task.FromResult(true);
                                   })
                                   .HandleClientError(async (response) =>
                                   {
                                       throw new ClientErrorStatusCodeException(await response.GetBodyString());
                                   })
                                   .HandleResponse(response);
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

        protected void UpdateTokens(AuthenticatedUserResponseDTO authInfo)
        {
            _accessToken = authInfo.AccessToken;
            _refreshToken = authInfo.RefreshToken;

            _accessTokenExpiratedAt = DateTime.UtcNow.AddMinutes(authInfo.AccessTokenExpirationMinutes);
        }
    }
}
