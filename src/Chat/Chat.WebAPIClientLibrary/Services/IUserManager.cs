using Chat.Core.DTOs.Requests;
using Chat.Core.DTOs.Responses;

namespace Chat.WebAPIClientLibrary.Services
{
    public interface IUserManager
    {
        public Task<bool> TryRegister(RegisterRequestDTO registerRequest);
        public Task<AuthenticatedUserResponseDTO> TryLogin(LoginRequestDTO loginRequest);
        public Task<AuthenticatedUserResponseDTO> TryRefreshToken(string refreshToken);
    }
}
