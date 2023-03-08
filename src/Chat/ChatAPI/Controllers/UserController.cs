using Chat.Core.DTOs.Requests;
using Chat.Core.DTOs.Responses;
using ChatAPI.Exceptions;
using ChatAPI.Services.Implementation;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public UserController(IUserService userService,
                                        AuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
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
                await _userService.RegisterUser(registerRequest);

                return Ok();
            }
            catch (Exception)
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
            catch (SignInException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var user = await _authenticationService.RetrieveUserFromHTTPContex(HttpContext);
            if (user == null)
                return Unauthorized();

            try
            {
                await _userService.DeleteUser(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
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
                return BadRequest();
            }
        }
    }
}
