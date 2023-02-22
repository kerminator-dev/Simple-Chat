namespace ChatAPI.Models
{
    public class AuthenticationConfiguration
    {
        // Токен доступа
        public string AccessTokenSecret { get; set; }

        // Токен для обновления токена доступа
        public string RefreshTokenSecret { get; set; }

        // Срок действия токена доступа в минутах
        public double AccessTokenExpirationMinutes { get; set; }

        // Срок действия токена для обновления токена доступа
        public double RefreshTokenExpirationMinutes { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}
