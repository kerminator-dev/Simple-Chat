using Chat.Core.DTOs.Requests;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Services.Implementation;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public async Task<IActionResult> SendMessage([FromBody] SendTextMessageRequestDTO messageRequestDTO)
        {
            User user = await _authenticationService.RetrieveUserFromHTTPContex(HttpContext);
            if (user == null)
                return Unauthorized("User not found!");

            try
            {
                // Отправка сообщения
                await _messagingService.SendMessage(user.Username, messageRequestDTO);

                return Ok();
            }
            catch (MessageNotSentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
