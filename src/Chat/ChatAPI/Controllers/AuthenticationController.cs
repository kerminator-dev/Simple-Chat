using ChatAPI.DTOs.Requests;
using ChatAPI.DTOs.Responses;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Services.Implementation;
using ChatAPI.Services.Interfaces;
using ChatAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly JwtUtils _jwtUtils;
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService, 
                                        AuthenticationService authenticationService, 
                                        IPasswordHasher passwordHasher, 
                                        JwtUtils jwtUtils)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _jwtUtils = jwtUtils;
        }

        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        /// <param name="registerRequest">Запрос на регистрацию</param>
        /// <returns></returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            bool userExists = await _userService.IsUserExists(registerRequest.Username);
            if (userExists)
            {
                return BadRequest("User with this username already exists.");
            }

            try
            {
                await  _userService.RegisterUser(registerRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Выполнить вход в аккаунт
        /// </summary>
        /// <param name="loginRequest">Запрос на вход</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                AuthenticatedUserResponseDTO response = await _authenticationService.AuthenticateUser(loginRequest);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Поменять токен
        /// </summary>
        /// <param name="refreshTokenRequest">Запрос на смену токена</param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO refreshTokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Проверка refresh-токена
            bool isValidRefreshToken = _jwtUtils.ValidateToken(refreshTokenRequest.RefreshToken);
            if (!isValidRefreshToken)
            {
                // Если токен неверный
                return Unauthorized();
            }

            try
            {
                // Выполнение аутентификации/генерации токенов
                AuthenticatedUserResponseDTO response = await _authenticationService.RefreshToken(refreshTokenRequest);

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
