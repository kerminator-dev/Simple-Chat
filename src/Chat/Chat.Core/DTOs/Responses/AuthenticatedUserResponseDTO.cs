namespace Chat.Core.DTOs.Responses
{
    public class AuthenticatedUserResponseDTO
    {
        public UserDTO User { get; set; }

        /// <summary>
        /// Токен доступа
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Срок действия токена доступа в минутах
        /// </summary>
        public double AccessTokenExpirationMinutes { get; set; }

        /// <summary>
        /// Токен для обновления AccessToken 
        /// 
        /// (если AccessToken потерялся или срок его действия закончился)
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Срок действия токена для обновления
        /// </summary>
        public double RefreshTokenExpirationMinutes { get; set; }

        public AuthenticatedUserResponseDTO(UserDTO user, string accessToken, double accessTokenExpirationMinutes, string refreshToken, double refreshTokenExpirationMinutes)
        {
            User = user;
            AccessToken = accessToken;
            AccessTokenExpirationMinutes = accessTokenExpirationMinutes;
            RefreshToken = refreshToken;
            RefreshTokenExpirationMinutes = refreshTokenExpirationMinutes;
        }
    }
}
