using ChatAPI.Entities;

namespace ChatAPI.Services.Interfaces
{
    public interface ITokenService
    {
        public bool ValidateAccessToken(string accessToken);
        public bool ValidateRefreshToken(string refreshToken);

        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken(User user);

        public void RemoveRefreshToken(string refreshToken);
    }
}
