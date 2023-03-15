using Chat.Core.DTOs.Requests;
using Chat.Core.DTOs.Responses;

namespace Chat.WebAPIClientLibrary.Services
{
    internal interface IUserManager
    {
        Task<bool> TryDeleteUser();
    }
}
