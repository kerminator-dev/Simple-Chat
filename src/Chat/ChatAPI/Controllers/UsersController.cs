using Chat.Core.DTOs.Requests;
using Chat.Core.DTOs.Responses;
using Chat.Core.Enums;
using ChatAPI.Exceptions;
using ChatAPI.Mappings;
using ChatAPI.Services.Implementation;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly CachedUserConnectionMapper<string> _hubConnections;

        public UsersController(IUserService userService,
                                        AuthenticationService authenticationService,
                                        CachedUserConnectionMapper<string> hubConnections)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _hubConnections = hubConnections;
        }

        [HttpGet("Get/{username}")]
        public async Task<IActionResult> Get(string username)
        {
            var user = await _authenticationService.RetrieveUserFromHTTPContex(HttpContext);
            if (user == null)
                return NotFound("User not found!");

            try
            {
                // Существует ли пользователь
                bool userExists = await _userService.IsUserExists(username);
                if (!userExists)
                {
                    return NotFound("User not found!");
                }

                // Подключён ли пользователь к хабу
                bool isConnectedToHub = _hubConnections.Contains(username);
                var userResponse = new UserResponseDTO
                (
                    username: username,
                    onlineStatus: isConnectedToHub ? OnlineStatus.Online : OnlineStatus.Offline
                );

                return Ok(userResponse);
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
    }
}
