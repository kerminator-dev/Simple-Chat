using ChatAPI.DTOs.Requests;
using ChatAPI.Extensions;
using ChatAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly JwtUtils _jwtUtils;
        

        public MessagesController(JwtUtils jwtUtils)
        {
            _jwtUtils = jwtUtils;
        }

        [HttpGet("Send")]
        [Authorize]
        public async Task<IActionResult> SendMessage()
        {
            if (!HttpContext.Items.TryGetValue("User", out var user))
            {
                return Unauthorized();
            }



            return Ok();
        }
    }
}
