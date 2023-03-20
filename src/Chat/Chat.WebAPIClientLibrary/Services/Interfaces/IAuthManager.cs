using Chat.Core.DTOs;
using Chat.Core.DTOs.Requests;

namespace Chat.WebAPIClientLibrary.Services.Interfaces
{
    internal interface IAuthManager
    {
        string AccessToken { get; }

        Task<UserDTO> TryLogin(LoginRequestDTO loginRequest);
        Task<bool> TryRefreshToken();
        Task<bool> TryRegister(RegisterRequestDTO registerRequest);
    }
}
