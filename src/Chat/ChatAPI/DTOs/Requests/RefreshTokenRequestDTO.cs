using System.ComponentModel.DataAnnotations;

namespace ChatAPI.DTOs.Requests
{
    public class RefreshTokenRequestDTO
    {
        [Required(ErrorMessage = "Refresh token is required!")]
        public string RefreshToken { get; set; }
    }
}
