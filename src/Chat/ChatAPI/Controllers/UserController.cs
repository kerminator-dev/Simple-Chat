﻿using ChatAPI.Services.Implementation;
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


        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
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
