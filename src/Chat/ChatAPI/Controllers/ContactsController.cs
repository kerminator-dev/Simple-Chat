using Chat.Core.DTOs.Requests;
using Chat.Core.DTOs.Responses;
using Chat.WebAPI.Exceptions;
using Chat.WebAPI.Services.Interfaces;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IContactService _contactService;

        public ContactsController(AuthenticationService authenticationService, IContactService contactService)
        {
            _authenticationService = authenticationService;
            _contactService = contactService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAll()
        {

            User user = await _authenticationService.RetrieveUserFromHTTPContex(HttpContext);
            if (user == null)
                return Unauthorized("User not found!");

            try
            {
                // Получение списка всех контактов пользователя
                var contacts = await _contactService.GetAllUserContactsWithOnlineStatuses(user.Username);

                return Ok(contacts);
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

        [HttpPost("Add")]
        public async Task<IActionResult> AddContacts([FromBody] AddContactsRequestDTO addContactsRequest)
        {
            User user = await _authenticationService.RetrieveUserFromHTTPContex(HttpContext);
            if (user == null)
                return Unauthorized("User not found!");

            try
            {
                await _contactService.AddContacts(user.Username, addContactsRequest.Usernames);

                return Ok();
            }
            catch (ContactNotAddedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteContacts([FromBody] DeleteContactsRequestDTO deleteContactsRequest)
        {
            User user = await _authenticationService.RetrieveUserFromHTTPContex(HttpContext);
            if (user == null)
                return Unauthorized("User not found!");

            try
            {
                await _contactService.DeleteContacts(user.Username, deleteContactsRequest.Usernames);

                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
