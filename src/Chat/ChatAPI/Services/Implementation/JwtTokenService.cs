using ChatAPI.Entities;
using ChatAPI.Models;
using ChatAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatAPI.Services.Implementation
{
    public class JwtTokenService : ITokenService
    {
        private readonly AuthenticationConfiguration _authenticationConfiguration;
        private readonly ICache<string, RefreshToken> _refreshTokenRepository;

        public JwtTokenService(AuthenticationConfiguration authenticationConfiguration, ICache<string, RefreshToken> refreshTokenRepository)
        {
            _authenticationConfiguration = authenticationConfiguration;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public string GenerateAccessToken(User user)
        {
            return GenerateToken
            (
                securityKey: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationConfiguration.AccessTokenSecret)),
                expires: DateTime.UtcNow.AddMinutes(_authenticationConfiguration.AccessTokenExpirationMinutes),
                issuer: _authenticationConfiguration.Issuer,
                audience: _authenticationConfiguration.Audience,
                claim: new Claim
                (
                    type: ClaimTypes.NameIdentifier, 
                    value: user.Username
                )
            );
        }

        public string GenerateRefreshToken(User user)
        {
            Guid g = Guid.NewGuid();
            StringBuilder sb = new StringBuilder
            (
                value: Convert.ToBase64String(g.ToByteArray())
            );

            sb.Replace("=", "");
            sb.Replace("+", "");

            var expiresAfter = TimeSpan.FromMinutes(_authenticationConfiguration.RefreshTokenExpirationMinutes);

            var refreshToken = new RefreshToken()
            {
                ExpirationDateTime = DateTime.UtcNow.Add(expiresAfter),
                Token = sb.ToString(),
                OwnerUsername = user.Username,
            };

            _refreshTokenRepository.Set
            (
                key: refreshToken.Token, 
                value: refreshToken, 
                expiresAfter: expiresAfter
            );
            return refreshToken.Token;
        }

        public bool ValidateAccessToken(string accessToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationConfiguration.AccessTokenSecret));

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _authenticationConfiguration.Issuer,
                    ValidAudience = _authenticationConfiguration.Audience,
                    IssuerSigningKey = securityKey,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            if (_refreshTokenRepository.TryGetValue(refreshToken, out var _))
            {
                return true;
            }
            return false;
        }

        private static string GenerateToken(SymmetricSecurityKey securityKey, DateTime? expires, string issuer, string audience, Claim claim = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    claim
                }),
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void RemoveRefreshToken(string refreshToken)
        {
            _refreshTokenRepository.Remove(refreshToken);
        }
    }
}
