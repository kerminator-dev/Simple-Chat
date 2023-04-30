using Chat.Core.DTOs.Requests;
using ChatAPI.Exceptions;
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

        public UsersController(IUserService userService,
                                        AuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromBody] GetUserRequestDTO getUserRequest)
        {
            var user = await _authenticationService.RetrieveUserFromHTTPContex(HttpContext);
            if (user == null)
                return NotFound("User not found!");

            try
            {
                var userDTO = await _userService.GetUserByUsername(getUserRequest.Username);

                return Ok();
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
