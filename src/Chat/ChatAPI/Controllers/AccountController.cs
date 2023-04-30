using ChatAPI.Services.Implementation;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        
        private readonly AuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AccountController(IUserService userService,
                                        AuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _authenticationService.RetrieveUserFromHTTPContex(HttpContext);
            if (user == null)
                return NotFound("User not found!");

            try
            {
                await _userService.DeleteUser(user);

                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
