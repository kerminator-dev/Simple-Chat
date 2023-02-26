using AutoMapper;
using ChatAPI.DTOs;
using ChatAPI.DTOs.Requests;
using ChatAPI.DTOs.Responses;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Models;
using ChatAPI.Services.Interfaces;
using System.Security.Claims;

namespace ChatAPI.Services.Implementation
{
    public class AuthenticationService
    {
        private readonly AuthenticationConfiguration _authConfiguration;
        private readonly ICache<string, RefreshToken> _inMemoryRefreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthenticationService(AuthenticationConfiguration authenticationConfiguration,
                                      ITokenService tokenService,
                                      IPasswordHasher passwordHasher,
                                      IUserService userService,
                                      IMapper mapper,
                                      ICache<string, RefreshToken> inMemoryRefreshTokenRepository)
        {
            _authConfiguration = authenticationConfiguration;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _userService = userService;
            _mapper = mapper;
            _inMemoryRefreshTokenRepository = inMemoryRefreshTokenRepository;
        }

        public async Task<User> RetrieveUserFromHTTPContex(HttpContext context)
        {
            try
            {
                string? token = context.Request.Headers["Authorization"].FirstOrDefault();

                if (!String.IsNullOrEmpty(token))
                {
                    token = token.Replace("Bearer ", "");

                    if (_tokenService.ValidateAccessToken(token))
                    {
                        var username = context.User.Claims
                               .First(i => i.Type == ClaimTypes.NameIdentifier).Value;

                        if (!string.IsNullOrEmpty(username))
                        {
                            return await _userService.GetUserByUsername(username);
                        }
                    }
                }
            }
            catch 
            {
                return default(User);
            }

            return default(User);
        }

        public async Task<AuthenticatedUserResponseDTO> AuthenticateUser(LoginRequestDTO loginRequest)
        {
            // Поиск пользователя по username
            var user = await _userService.GetUserByUsername(loginRequest.Username);
            if (user == null)
                throw new SignInException("Wrong username or password!");

            // Проверка пароля
            bool isCorrectPassword = _passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash);
            if (!isCorrectPassword)
                throw new SignInException("Wrong username or password!");

            // Генерация токенов
            return this.GenerateTokens(user);
        }

        public async Task<AuthenticatedUserResponseDTO> RefreshToken(RefreshTokenRequestDTO refreshTokenRequest)
        {
            // Поиск токена в кэше
            if (!_inMemoryRefreshTokenRepository.TryGetValue(refreshTokenRequest.RefreshToken, out RefreshToken? token)) 
            {
                // Если токен не найден
                throw new EntityNotFoundException("Invalid refresh token!");
            }

            // Удаление старого токена из кэша
            _inMemoryRefreshTokenRepository.Remove(token.Token);

            // Поиск пользователя
            User user = await _userService.GetUserByUsername(token.OwnerUsername);
            if (user == null)
            {
                // Если пользователь не найден
                throw new EntityNotFoundException("User not found!");
            }

            // Генерация токенов
            return this.GenerateTokens(user);
        }

        private AuthenticatedUserResponseDTO GenerateTokens(User user)
        {
            string accessToken = _tokenService.GenerateAccessToken(user);
            string refreshToken = _tokenService.GenerateRefreshToken(user);
            var currentDateTime = DateTime.UtcNow;

            _inMemoryRefreshTokenRepository.Set(
                refreshToken,
                new RefreshToken()
                {
                    ExpirationDateTime = currentDateTime.AddMinutes(_authConfiguration.RefreshTokenExpirationMinutes),
                    Token = refreshToken,
                    OwnerUsername = user.Username,
                },
                expiresAfter: TimeSpan.FromMinutes(_authConfiguration.RefreshTokenExpirationMinutes)
            );

            return new AuthenticatedUserResponseDTO
            (
                user: _mapper.Map<User, UserDTO>(user),
                accessToken: accessToken,
                accessTokenExpirationMinutes: _authConfiguration.AccessTokenExpirationMinutes,
                refreshToken: refreshToken,
                refreshTokenExpirationMinutes: _authConfiguration.RefreshTokenExpirationMinutes
            );
        }
    }
}

