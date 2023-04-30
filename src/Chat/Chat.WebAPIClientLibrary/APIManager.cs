using Chat.Core.DTOs;
using Chat.Core.DTOs.Notifications;
using Chat.Core.DTOs.Requests;
using Chat.WebAPIClientLibrary.Exceptions.Client;
using Chat.WebAPIClientLibrary.Exceptions.Internal;
using Chat.WebAPIClientLibrary.Services.Implementation;
using Chat.WebAPIClientLibrary.Services.Interfaces;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.WebAPIClientLibrary
{
    public sealed class APIManager
    {
        // Endpoint SignalR-хаба
        private const string HUB_ENDPOINT = "Hub";
        private const string SUBSCRIBE_ON_NOTIFICATIONS_METHOD_NAME = "SubscribeOnUserNotifications";
        private const string UNSUBSCRIBE_FROM_NOTIFICATIONS_METHOD_NAME = "UnsubscribeFromUserNotifications";

        // Подключение к SignalR-хабу
        private Microsoft.AspNetCore.SignalR.Client.HubConnection _hubConnection;

        // События хаба
        /// <summary>
        /// При закрытии подключения
        /// </summary>
        public event Func<Exception?, Task>? OnConnectionClosed
        {
            add
            {
                _hubConnection.Closed += value;
            }
            remove
            {
                _hubConnection.Closed -= value;
            }
        }

        /// <summary>
        /// При успешном переподключении
        /// </summary>
        public event Func<string?, Task>? OnReconnected
        {
            add
            {
                _hubConnection.Reconnected += value;
            }
            remove
            {
                _hubConnection.Reconnected -= value;
            }
        }

        /// <summary>
        /// При попытке переподключения
        /// </summary>
        public event Func<Exception?, Task>? OnReconnecting
        {
            add
            {
                _hubConnection.Reconnecting += value;
            }
            remove
            {
                _hubConnection.Reconnecting -= value;
            }
        }

        /// <summary>
        /// При получении нового сообщения
        /// </summary>
        public event Action<TextMessageNotificationDTO> OnMessageReceived;

        /// <summary>
        /// При получении уведомления о новом активном пользователе хаба
        /// </summary>
        public event Action<string> OnUserGetsOnline;

        /// <summary>
        /// При получении уведомления о отключённом пользователе хаба
        /// </summary>
        public event Action<string> OnUserGetsOffline;

        /// <summary>
        /// При получении уведомления о списке активных пользователей хаба
        /// </summary>
        public Action<ActiveUsersNotificationDTO> OnActiveUsersNotification;

        #region Менеджеры для работы с контроллерами
        private IAuthManager _authManager;
        private IUserManager _userManager;
        private IMessagingManager _messagingManager;
        #endregion

        public APIManager(string host)
        {
            _authManager = new AuthManager(host);
            _userManager = new UserManager(host);
            _messagingManager = new MessagingManager(host);

            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{host}{HUB_ENDPOINT}", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(_authManager.AccessToken);
                    options.Transports = HttpTransportType.WebSockets;
                })
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.Reconnecting += OnHubReconnecting;

            // Подписка на события
            _hubConnection.On<string>("UserGetsOffline", (username) =>
            {
                OnUserGetsOffline?.Invoke(username);
            });

            _hubConnection.On<string>("UserGetsOnline", (username) =>
            {
                OnUserGetsOnline?.Invoke(username);
            });

            _hubConnection.On<TextMessageNotificationDTO>("NewMessage", (message) =>
            {
                OnMessageReceived?.Invoke(message);
            });

            _hubConnection.On<ActiveUsersNotificationDTO>("ActiveUsers", (message) =>
            {
                OnActiveUsersNotification?.Invoke(message);
            });
        }

        private async Task OnHubReconnecting(Exception? exception)
        {
            if (exception is HttpRequestException)
            {
                var statusCode = (exception as HttpRequestException).StatusCode;

                if (statusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    try
                    {
                        await this.TryRefreshToken();
                    }
                    catch (Exception) { }
                }
            }
        }

        public async Task SubscribeOnUserNotifications(params string[] usernames)
        {
            await _hubConnection.InvokeAsync(SUBSCRIBE_ON_NOTIFICATIONS_METHOD_NAME, usernames);
        }

        public async Task UnsubscribeFromUserNotifications(params string[] usernames)
        {
            await _hubConnection.InvokeAsync(UNSUBSCRIBE_FROM_NOTIFICATIONS_METHOD_NAME, usernames);
        }

        public async Task<bool> TryRegister(RegisterRequestDTO registerRequest)
        {
            try
            {
                return await _authManager.TryRegister(registerRequest);
            }
            catch (ClientErrorStatusCodeException ex)
            {
                throw new SignUpException(ex.Message);
            }
            catch (UnexpectedStatusCodeException)
            {
                throw new SignUpException("Unknown error!");
            }
        }

        public async Task<UserDTO> TryLogin(LoginRequestDTO loginRequest)
        {
            try
            {
                // Выполнение входа
                var user = await _authManager.TryLogin(loginRequest);

                // Подключение к хабу
                await _hubConnection.StartAsync();

                return user;
            }
            catch (ClientErrorStatusCodeException ex)
            {
                throw new SignInException(ex.Message);
            }
            catch (UnexpectedStatusCodeException)
            {
                throw new SignInException("Unknown error!");
            }
        }

        private async Task<bool> TryRefreshToken()
        {
            try
            {
                return await _authManager.TryRefreshToken();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> TrySendMessageAsync(SendTextMessageRequestDTO sendMessageRequestDTO)
        {
            try
            {
                // Отправка сообщения
                // return await _messagingManager.TrySendMessageAsync(sendMessageRequestDTO, _authManager.AccessToken);
                await _hubConnection.InvokeAsync("SendMessage", sendMessageRequestDTO);

                return true;
            }
            catch (HubException ex)
            {
                throw new Exceptions.Client.MessageNotSentException(ex.Message);
            }


            //catch (UnauthorizedStatusCodeException)
            //{
            //    try
            //    {
            //        // Переполучение токенов
            //        bool success = await this.TryRefreshToken();
            //
            //        // Повторная попытка отправки сообщения
            //        if (success)
            //            return await _messagingManager.TrySendMessageAsync(sendMessageRequestDTO, _authManager.AccessToken);
            //
            //        return false;
            //    }
            //    catch (UnauthorizedStatusCodeException ex)
            //    {
            //        throw new UnauthorizedException(ex.Message);
            //    }
            //    catch (ClientErrorStatusCodeException ex)
            //    {
            //        throw new Exceptions.Client.MessageNotSentException(ex.Message);
            //    }
            //    catch (UnexpectedStatusCodeException)
            //    {
            //        throw new Exceptions.Client.MessageNotSentException("Unknown error!");
            //    }
            //}
            //catch (ClientErrorStatusCodeException ex)
            //{
            //    throw new MessageNotSentException(ex.Message);
            //}
            //catch (UnexpectedStatusCodeException) 
            //{ 
            //    throw new MessageNotSentException("Unknown error!"); 
            //}
        }

        public async Task<bool> TryDeleteUser()
        {
            try
            {
                return await _userManager.TryDeleteUser(_authManager.AccessToken);
            }
            catch(UnauthorizedStatusCodeException)
            {
                try
                {
                    // Переполучение токенов
                    bool success = await _authManager.TryRefreshToken();

                    // Повторная попытка удаления акканта
                    if (success)
                        return await _userManager.TryDeleteUser(_authManager.AccessToken);

                    return false;
                }
                catch (UnauthorizedStatusCodeException ex)
                {
                    throw new UnauthorizedException(ex.Message);
                }
                catch (Exception)
                {
                    throw new UserNotDeletedException("Unknown error!");
                }
            }
        }
    }
}
