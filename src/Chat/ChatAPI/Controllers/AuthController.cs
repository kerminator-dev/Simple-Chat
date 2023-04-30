using Chat.Core.DTOs.Requests;
using Chat.Core.DTOs.Responses;
using Chat.WebAPI.Exceptions;
using Chat.WebAPI.Extensions;
using ChatAPI.Exceptions;
using ChatAPI.Services.Implementation;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Chat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthController(IUserService userService,
                                        AuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Обновить истёшкий access-токен и переполучить refresh-токен
        /// </summary>
        /// <param name="refreshTokenRequest">Запрос на смену токена</param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO refreshTokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetFirstError());
            }

            try
            {
                // Генерация токенов
                AuthenticatedUserResponseDTO response = await _authenticationService.RefreshToken(refreshTokenRequest);

                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        /// <param name="registerRequest">Запрос на регистрацию</param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetFirstError());
            }

            try
            {
                // Регистрация
                await _userService.RegisterUser(registerRequest);

                return new StatusCodeResult(StatusCodes.Status201Created);

            }
            catch (SignUpException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Выполнить вход в аккаунт
        /// </summary>
        /// <param name="loginRequest">Запрос на вход</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetFirstError());
            }

            try
            {
                // Авторизация
                var response = await _authenticationService.AuthenticateUser(loginRequest);

                return Ok(response);
            }
            catch (SignInException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
