using Chat.Core.DTOs.Requests;
using Chat.Core.DTOs.Responses;

namespace Chat.WebAPIClientLibrary.Services.Interfaces
{
    internal interface IUserManager
    {
        Task<bool> TryDeleteUser(string accessToken);
    }
}
