using Chat.Core.DTOs.Requests;
using Chat.WebAPIClientLibrary.Exceptions;
using Chat.WebAPIClientLibrary.Services;
using ChatAPI.Exceptions;
using Microsoft.Extensions.Logging;

namespace Chat.WebAPIClientLibrary
{
    public class APIManager
    {
        protected ILogger _logger;

        private string _accessToken;
        private string _refreshToken;

        protected IUserManager _userManager;
        protected IMessagingManager _messagingManager;
        public APIManager(Uri host)
        {
            _userManager = new UserManager(host);
            _messagingManager = new MessagingManager(host);

            // Токены
            _accessToken  = string.Empty;
            _refreshToken = string.Empty;
        }

        public async Task<bool> TryRegister(RegisterRequestDTO registerRequest)
        {
            try
            {
                return await _userManager.TryRegister(registerRequest);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        /// <exception cref="UnexpectedHttpResponseException"></exception>
        public async Task<bool> TryLogin(LoginRequestDTO loginRequest)
        {
            try
            {
                var authInfo = await _userManager.TryLogin(loginRequest);

                UpdateTokens(authInfo.AccessToken, authInfo.RefreshToken);

                return true;
            }
            catch (SignInException ex)
            {
                throw ex;
            }
        }

        public async Task<bool> TryRefreshToken()
        {
            try
            {
                var authInfo = await _userManager.TryRefreshToken(_refreshToken);

                UpdateTokens(authInfo.AccessToken, authInfo.RefreshToken);

                return true;
            }
            catch (UnexpectedHttpResponseException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> TrySendMessage(SendMessageRequestDTO sendMessageRequestDTO)
        {
            try
            {
                return await _messagingManager.TrySendMessage(sendMessageRequestDTO, _accessToken);
            }
            catch (UnauthorizedException ex)
            {
                try
                {
                    // Переполучение токенов
                    bool refreshed = await this.TryRefreshToken();

                    if (refreshed)
                        return await _messagingManager.TrySendMessage(sendMessageRequestDTO, _accessToken);

                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void UpdateTokens(string accessToken, string refreshToken)
        {
            _accessToken = accessToken;
            _refreshToken = refreshToken;
        }
    }
}
