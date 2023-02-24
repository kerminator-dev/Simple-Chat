using ChatAPI.DTOs.Requests;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Extensions;
using ChatAPI.Services.Implementation;
using ChatAPI.Services.Interfaces;
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
        private readonly AuthenticationService _authenticationService;
        private readonly IMessagingService _messagingService;

        public MessagesController(AuthenticationService authenticationService, IMessagingService messagingService)
        {
            _authenticationService = authenticationService;
            _messagingService = messagingService;
        }

        [HttpPost("Send")]
        [Authorize]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequestDTO messageRequestDTO)
        {
            User user = await _authenticationService.RetrieveUserFromHTTPContex(HttpContext);
            if (user == null)
            {
                return Unauthorized();
            }

            try
            {
                await _messagingService.Send(user, messageRequestDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
