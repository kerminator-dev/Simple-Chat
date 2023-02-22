using AutoMapper;
using ChatAPI.DTOs;
using ChatAPI.DTOs.Requests;
using ChatAPI.DTOs.Responses;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Models;
using ChatAPI.Services.Interfaces;
using ChatAPI.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChatAPI.Services.Implementation
{
    public class AuthenticationService
    {
        private readonly AuthenticationConfiguration _authConfiguration;
        private readonly ICache<string, RefreshToken> _inMemoryRefreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserService _userService;
        private readonly JwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public AuthenticationService(AuthenticationConfiguration authenticationConfiguration,
                                      JwtUtils jwtAccessUtils,
                                      IPasswordHasher passwordHasher,
                                      IUserService userService,
                                      IMapper mapper,
                                      ICache<string, RefreshToken> inMemoryRefreshTokenRepository)
        {
            _authConfiguration = authenticationConfiguration;
            _jwtUtils = jwtAccessUtils;
            _passwordHasher = passwordHasher;
            _userService = userService;
            _mapper = mapper;
            _inMemoryRefreshTokenRepository = inMemoryRefreshTokenRepository;
        }

        public async Task<AuthenticatedUserResponseDTO> AuthenticateUser(LoginRequestDTO loginRequest)
        {
            // Поиск пользователя по username
            var user = await _userService.GetUserByUsername(loginRequest.Username);
            if (user == null)
            {
                // Если username неверный
                throw new EntityNotFoundException("Wrong username or password!");
            }

            // Проверка пароля
            bool isCorrectPassword = _passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash);
            if (!isCorrectPassword)
            {
                // Если пароль неверный
                throw new Exception("Wrong username or password!");
            }

            string accessToken = _jwtUtils.GenerateAccessToken(user);
            string refreshToken = _jwtUtils.GenerateRefreshToken(user);
            var currentDateTime = DateTime.UtcNow;

            _inMemoryRefreshTokenRepository.Set(
                refreshToken,
                new RefreshToken()
                {
                    ExpirationDateTime = currentDateTime.AddMinutes(_authConfiguration.RefreshTokenExpirationMinutes),
                    Token = refreshToken,
                    Username = user.Username,
                },
                expiresAfter: TimeSpan.FromMinutes(_authConfiguration.RefreshTokenExpirationMinutes)
            );

            return new AuthenticatedUserResponseDTO
            (
               user: _mapper.Map<User, UserDTO>(user),
               accessToken: _jwtUtils.GenerateAccessToken(user),
               accessTokenExpirationMinutes: _authConfiguration.AccessTokenExpirationMinutes,
               refreshToken: _jwtUtils.GenerateRefreshToken(user),
               refreshTokenExpirationMinutes: _authConfiguration.RefreshTokenExpirationMinutes
            );
        }

        public async Task<AuthenticatedUserResponseDTO> RefreshToken(RefreshTokenRequestDTO refreshTokenRequest)
        {
            // Проверка refresh-токена
            bool isValidRefreshToken = _jwtUtils.ValidateToken(refreshTokenRequest.RefreshToken);
            if (!isValidRefreshToken)
            {
                // Если токен неверный
                throw new Exception("Invalid refresh token");
            }

            // Поиск токена в кэше
            if (!_inMemoryRefreshTokenRepository.TryGetValue(refreshTokenRequest.RefreshToken, out RefreshToken token)) 
            {
                // Если токен не найден
                throw new EntityNotFoundException("Invalid refresh token");
            }

            // Удаление старого токена из кэша
            _inMemoryRefreshTokenRepository.Remove(token.Token);

            // Поиск пользователя
            User user = await _userService.GetUserByUsername(token.Username);
            if (user == null)
            {
                // Если пользователь не найден
                throw new EntityNotFoundException("User not found");
            }

            string accessToken = _jwtUtils.GenerateAccessToken(user);
            string refreshToken = _jwtUtils.GenerateRefreshToken(user);
            var currentDateTime = DateTime.UtcNow;

            _inMemoryRefreshTokenRepository.Set(
                refreshToken,
                new RefreshToken()
                {
                    ExpirationDateTime = currentDateTime.AddMinutes(_authConfiguration.RefreshTokenExpirationMinutes),
                    Token = refreshToken,
                    Username = user.Username,
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

        public async Task<User> TryGetUserByToken(string token)
        {
            if (_jwtUtils.ValidateToken(token))
            {
                string username = _jwtUtils.GetUsername(token);
                return await _userService.GetUserByUsername(username);
            }

            return default(User);
        }

        protected AuthenticatedUserResponseDTO GenerateTokens(User user)
        {
            // Генерация токена доступа
            string accessToken = _jwtUtils.GenerateAccessToken(user);
            // Генерация токена для восстановления токена доступа
            string refreshToken = _jwtUtils.GenerateRefreshToken(user);

            var currentDateTime = DateTime.UtcNow;

            _inMemoryRefreshTokenRepository.Set(
                refreshToken, 
                new RefreshToken()
                {
                    ExpirationDateTime = currentDateTime.AddMinutes(_authConfiguration.RefreshTokenExpirationMinutes),
                    Token = refreshToken,
                    Username = user.Username,
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

