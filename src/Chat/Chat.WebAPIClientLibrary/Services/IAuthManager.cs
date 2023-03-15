using Chat.Core.DTOs.Requests;
using Chat.Core.DTOs.Responses;

namespace Chat.WebAPIClientLibrary.Services
{
    internal interface IAuthManager
    {
        Task<AuthenticatedUserResponseDTO> TryLogin(LoginRequestDTO loginRequest);

        Task<AuthenticatedUserResponseDTO> TryRefreshToken(string refreshToken);

        Task<bool> TryRegister(RegisterRequestDTO registerRequest);
    }
}
