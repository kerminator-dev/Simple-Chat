using ChatAPI.Entities;
using ChatAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatAPI.Utils
{
    public class JwtUtils
    {
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public JwtUtils(AuthenticationConfiguration authenticationConfiguration)
        {
            _authenticationConfiguration = authenticationConfiguration;
        }

        public bool ValidateToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationConfiguration.AccessTokenSecret));


            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
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

        public string GenerateAccessToken(User user)
        {
            return GenerateToken
            (
                securityKey: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationConfiguration.AccessTokenSecret)),
                expires: DateTime.UtcNow.AddMinutes(_authenticationConfiguration.AccessTokenExpirationMinutes),
                issuer: _authenticationConfiguration.Issuer,
                audience: _authenticationConfiguration.Audience,
                claim: new Claim(ClaimTypes.NameIdentifier, user.Username)
            );
        }

        public string GenerateRefreshToken(User user)
        {
            return GenerateToken
            (
                securityKey: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationConfiguration.AccessTokenSecret)),
                expires: DateTime.UtcNow.AddMinutes(_authenticationConfiguration.RefreshTokenExpirationMinutes),
                issuer: _authenticationConfiguration.Issuer,
                audience: _authenticationConfiguration.Audience
            );
        }

        public string GetUsername(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            return "";
            // return securityToken.Claims.FirstOrDefault(c => c == ClaimTypes.NameIdentifier)?.Value;
        }

        private string GenerateToken(SymmetricSecurityKey securityKey, DateTime? expires, string issuer, string audience, Claim claim = null)
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
    }
}
